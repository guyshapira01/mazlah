Imports System.IO
Imports System.Data

Public Class CGenerateCatalogOffline

    Private PublishOfflineDirectory As String
    Private gemAppMode As String
    Private ODBCNAME As String
    Private ImageMapManage As String
    Private VolumeDirectory As String
    Private rootWebPath As String
    Private sLogonUser As String
    Private sAbsoluteURI As String
    Private oSession As System.Web.SessionState.HttpSessionState

    Sub New(ByVal PublishOfflineDirectory As String, ByVal gemAppMode As String, ByVal ODBCNAME As String, ByVal ImageMapManage As String, ByVal VolumeDirectory As String, ByVal rootWebPath As String, ByVal sLogonUser As String, ByVal sAbsoluteURI As String, ByRef oSession As System.Web.SessionState.HttpSessionState)

        Me.PublishOfflineDirectory = PublishOfflineDirectory
        Me.gemAppMode = gemAppMode
        Me.ODBCNAME = ODBCNAME
        Me.ImageMapManage = ImageMapManage
        Me.VolumeDirectory = VolumeDirectory
        Me.rootWebPath = rootWebPath
        Me.sLogonUser = sLogonUser
        Me.sAbsoluteURI = sAbsoluteURI
        Me.oSession = oSession

    End Sub

    Sub copyFilesInDirectory(ByVal sSourceDirectoryPath As String, ByVal sDestinationDirectoryPath As String, ByVal sExtenstionToCopy As String)

        Dim oFiles() As String
        Dim sFile As String
        Dim oDirectory As Directory
        Dim oFile As File
        Dim sFileName As String

        oFiles = oDirectory.GetFiles(sSourceDirectoryPath, sExtenstionToCopy)

        For Each sFile In oFiles
            sFileName = getFileName(sFile)
            oFile.Copy(sFile, sDestinationDirectoryPath & sFileName, True)
            oFile.SetAttributes(sDestinationDirectoryPath & sFileName, FileAttributes.Normal)
        Next

    End Sub
    Function GenerateCatalogOffline(ByVal sGenerateCatalogOfflinePkey As String) As String

        Dim sTemplate As String
        Dim sPkey As String
        Dim sType As String = "10"
        Dim sOutput As String = ""
        Dim oCHtmlGenerator As CHtmlGenerator
        Dim oAclPermissions As cACLPermissions
        Dim oLWObjectTypes As New LWObjectTypes
        Dim oGetFrames As New GetFrames
        Dim sFrames As String
        Dim filePath As String
        Dim sTemplateContent As String
        Dim sTemplateClean As String
        Dim oFsr As StreamReader
        Dim sSqlQuery As String
        Dim oConn As New CConnection
        Dim dbDataSet As New DataSet
        Dim sTemplatesString As String
        Dim sTemplateString() As String
        Dim oDRow As DataRow
        Dim frameNumber As Integer
        Dim i As Integer
        Dim outputFile As File
        Dim outputFileSW As StreamWriter
        Dim sPublishOfflineDirectory As String = PublishOfflineDirectory
        Dim sPublishOfflineDirectoryWithCatalogs = sPublishOfflineDirectory & "Catalogs\"
        Dim oDirectory As Directory
        Dim oFile As File
        Dim sError As String

        If oDirectory.Exists(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey) Then
            oDirectory.Move(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey, sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "_" & Replace(Replace(Now, "/", ","), ":", "-"))
        End If

        'create directory for publising
        oDirectory.CreateDirectory(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey)

        If gemAppMode = "Access" Then
            oConn.Open_Access_Connection(ODBCNAME)
        Else
            oConn.Open_Oracle_Connection(ODBCNAME)
        End If

        sSqlQuery = "select t_cat_part.pkey, pparttemplate, pobjecttype from t_cat_part, " _
                  & "(select pkey,pobjecttype from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sGenerateCatalogOfflinePkey & "') PartTree " _
                  & "where(PartTree.pkey = t_cat_part.pkey)"

        oConn.Open_Dataset(sSqlQuery, dbDataSet, "Parts")


        sSqlQuery = "select t_cat_picture_property.pkey,ppicturekey, pfileextension from t_cat_picture_property, " _
                 & "(select pkey,pobjecttype from t_cat_nodes where pglobalstatus<>4 and pobjecttype=3 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sGenerateCatalogOfflinePkey & "') " _
                 & "PartTree where(PartTree.pkey = t_cat_picture_property.pkey)"

        oConn.Open_Dataset(sSqlQuery, dbDataSet, "Pictures")


        sSqlQuery = "SELECT PKEY, PINDEXTEMPLATE FROM " & oLWObjectTypes.emCatalogTable & " WHERE PKEY='" & sGenerateCatalogOfflinePkey & "'"

        oConn.Open_Dataset(sSqlQuery, dbDataSet, "IndexPublish")


        sSqlQuery = "select t_cat_winobject.pkey,t_cat_winobject.pdocpath, t_cat_winobject.plinktonetworkfile, t_cat_winobject.pfileextension, t_cat_winobject.porgfilename from t_cat_winobject, " _
                 & "(select pkey,pobjecttype from t_cat_nodes where pglobalstatus<>4 and pobjecttype=18 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sGenerateCatalogOfflinePkey & "') " _
                 & "PartTree where(PartTree.pkey = t_cat_winobject.pkey)"

        oConn.Open_Dataset(sSqlQuery, dbDataSet, "WinObject")

        '1. generate parts
        Dim totalCount As Integer = dbDataSet.Tables(0).Rows.Count + dbDataSet.Tables(1).Rows.Count + dbDataSet.Tables(3).Rows.Count
        Dim counter As Integer = 0
        For Each oDRow In dbDataSet.Tables(0).Rows

            frameNumber = 0
            filePath = oDRow.Item("pparttemplate")
            sPkey = oDRow.Item("pkey")
            sType = oDRow.Item("pobjecttype")

            oFsr = File.OpenText(rootWebPath & "Templates\" & filePath)
            sTemplateContent = oFsr.ReadToEnd()
            oFsr.Close()

            sTemplateClean = oGetFrames.RemoveNotes(sTemplateContent)
            sTemplatesString = oGetFrames.ParseInner(sTemplateClean, False)

            sTemplateString = sTemplatesString.Split("|")

            For i = 0 To sTemplateString.Length - 1

                sTemplate = sTemplateString(i)
                If sTemplate <> "" Then

                    frameNumber = frameNumber + 1
                    If sTemplate.IndexOf(")") > 0 Then
                        sTemplate = sTemplate.Substring(sTemplate.IndexOf(")") + 1)
                    End If

                    oCHtmlGenerator = New CHtmlGenerator(rootWebPath & "Templates\" & sTemplate, True, ODBCNAME, sType, sLogonUser, gemAppMode, ImageMapManage, oConn)
                    oCHtmlGenerator.WorkMode = CHtmlGenerator.eWorkMode.OfflineMode
                    oCHtmlGenerator.CurrentURL = sAbsoluteURI & "&Pkey=" & sPkey & "&ttt=ttt"

                    Dim sParentKey As String = sGenerateCatalogOfflinePkey

                    If (sGenerateCatalogOfflinePkey = "") Then
                        oCHtmlGenerator.SCatalog = ""
                    Else
                        oCHtmlGenerator.SCatalog = sGenerateCatalogOfflinePkey
                        oCHtmlGenerator.SParent = sParentKey
                    End If

                    oAclPermissions = New cACLPermissions(sLogonUser, ODBCNAME, gemAppMode, sType, oConn)

                    If CBool(oAclPermissions.HasPermission(sPkey, "P_SHOW")) Then

                        Select Case sType
                            Case oLWObjectTypes.emPage
                                sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPageTable & ".*,  " & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM T_CAT_NODES, " & oLWObjectTypes.emPageTable & "," & oLWObjectTypes.emCatalogTable & " WHERE " & oLWObjectTypes.emPageTable & ".PKEY='" & sPkey & "' T_CAT_NODES.PKEY=T_CAT_PAGE.PKEY AND T_CAT_NODES.PPARENTKEY='" & sParentKey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPageTable & ".PKEYCATALOG")
                            Case oLWObjectTypes.emPart
                                If gemAppMode = "Access" Then
                                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkey & "'")
                                Else
                                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".* , " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM T_CAT_NODES, " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkey & "' AND T_CAT_NODES.PKEY=T_CAT_PART.PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & sParentKey & "' AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY(+)='" & oCHtmlGenerator.SCatalog & "' AND " & oLWObjectTypes.emStatusTable & ".pkeycatalog(+)='" & oCHtmlGenerator.SCatalog & "'")
                                End If
                            Case oLWObjectTypes.emPicture
                                sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPicturePropertyTable & ".* ," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM T_CAT_NODES, " & oLWObjectTypes.emPicturePropertyTable & "," & oLWObjectTypes.emCatalogTable & " WHERE " & oLWObjectTypes.emPicturePropertyTable & ".PKEY='" & sPkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPicturePropertyTable & ".PKEYCATALOG")
                            Case oLWObjectTypes.emCatalog
                                sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emCatalogTable & ".*, " & oLWObjectTypes.emCatalogTable & "_1.PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emCatalogTable & ", " & oLWObjectTypes.emCatalogTable & " " & oLWObjectTypes.emCatalogTable & "_1 WHERE " & oLWObjectTypes.emCatalogTable & ".PKEY='" & sPkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emCatalogTable & "_1.PKEY")
                            Case oLWObjectTypes.emWinObject
                                If gemAppMode = "Access" Then
                                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emWinObjectTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emWinObjectTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emWinObjectTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkey & "'")
                                Else
                                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emWinObjectTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM T_CAT_NODES, " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkey & "' AND T_CAT_WINOBJECT.PKEY=T_CAT_NODES.PKEY AND T_CAT_NODES.PPARENTKEY='" & sParentKey & "' AND " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY(+)='" & oCHtmlGenerator.SCatalog & "' AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & oCHtmlGenerator.SCatalog & "'")
                                End If
                        End Select

                        If InStr(sTemplate, "xml", CompareMethod.Text) Then
                            outputFileSW = outputFile.CreateText(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & oDRow.Item("PKEY") & "_" & frameNumber & ".xml")
                        Else
                            outputFileSW = outputFile.CreateText(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & oDRow.Item("PKEY") & "_" & frameNumber & ".html")
                        End If

                        outputFileSW.Write(sOutput)

                        outputFileSW.Close()

                    Else

                        If InStr(sTemplate, "xml", CompareMethod.Text) Then
                            outputFileSW = outputFile.CreateText(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & oDRow.Item("PKEY") & "_" & frameNumber & ".xml")
                        Else
                            outputFileSW = outputFile.CreateText(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & oDRow.Item("PKEY") & "_" & frameNumber & ".html")
                        End If

                        outputFileSW.Write("<script language=""JavaScript"">alert('You dont have permission to view this page');if (history.length==0)  {window.close();}   else    {history.back();}</script>")

                        outputFileSW.Close()

                    End If
                    oAclPermissions.Dispose()
                    oAclPermissions = Nothing
                    oCHtmlGenerator.Dispose()
                    oCHtmlGenerator = Nothing

                End If

            Next

            'for normal itself
            oCHtmlGenerator = New CHtmlGenerator(rootWebPath & "Templates\" & filePath, True, ODBCNAME, sType, sLogonUser, gemAppMode, ImageMapManage, oConn)
            oCHtmlGenerator.WorkMode = CHtmlGenerator.eWorkMode.OfflineMode
            oCHtmlGenerator.CurrentURL = sAbsoluteURI & "&Pkey=" & sPkey & "&ttt=ttt"

            If (sGenerateCatalogOfflinePkey = "") Then
                oCHtmlGenerator.SCatalog = ""
            Else
                oCHtmlGenerator.SCatalog = sGenerateCatalogOfflinePkey
            End If

            Select Case sType
                Case oLWObjectTypes.emPage
                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPageTable & ".*, " & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emPageTable & "," & oLWObjectTypes.emCatalogTable & " WHERE " & oLWObjectTypes.emPageTable & ".PKEY='" & sPkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPageTable & ".PKEYCATALOG")
                Case oLWObjectTypes.emPart
                    If gemAppMode = "Access" Then
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkey & "'")
                    Else
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & sPkey & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY(+)='" & oCHtmlGenerator.SCatalog & "'")
                    End If
                Case oLWObjectTypes.emPicture
                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emPicturePropertyTable & ".*," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emPicturePropertyTable & "," & oLWObjectTypes.emCatalogTable & " WHERE " & oLWObjectTypes.emPicturePropertyTable & ".PKEY='" & sPkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPicturePropertyTable & ".PKEYCATALOG")
                Case oLWObjectTypes.emCatalog
                    sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emCatalogTable & ".*, " & oLWObjectTypes.emCatalogTable & "_1.PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emCatalogTable & ", " & oLWObjectTypes.emCatalogTable & " " & oLWObjectTypes.emCatalogTable & "_1 WHERE " & oLWObjectTypes.emCatalogTable & ".PKEY='" & sPkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emCatalogTable & "_1.PKEY")
                Case oLWObjectTypes.emWinObject
                    If gemAppMode = "Access" Then
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emWinObjectTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emWinObjectTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emWinObjectTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkey & "'")
                    Else
                        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emWinObjectTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sPkey & "'  AND " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY(+)='" & oCHtmlGenerator.SCatalog & "' AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & oCHtmlGenerator.SCatalog & "'")
                    End If
            End Select

            outputFileSW = outputFile.CreateText(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & oDRow.Item("PKEY") & ".html")
            outputFileSW.Write(sOutput)

            outputFileSW.Close()

            oCHtmlGenerator.Dispose()
            oCHtmlGenerator = Nothing

            counter = counter + 1
            oSession("Precent") = CInt((counter * 100) / totalCount).ToString()
        Next


        oGetFrames = Nothing

        'index publish

        oCHtmlGenerator = New CHtmlGenerator(rootWebPath & "Templates\" & dbDataSet.Tables(2).Rows(0).Item("PINDEXTEMPLATE"), True, ODBCNAME, 1, sLogonUser, gemAppMode, ImageMapManage, oConn)
        oCHtmlGenerator.WorkMode = CHtmlGenerator.eWorkMode.OfflineMode
        oCHtmlGenerator.CurrentURL = sAbsoluteURI & "&Pkey=" & dbDataSet.Tables(2).Rows(0).Item("PKEY") & "&ttt=ttt"

        If (sGenerateCatalogOfflinePkey = "") Then
            oCHtmlGenerator.SCatalog = ""
        Else
            oCHtmlGenerator.SCatalog = sGenerateCatalogOfflinePkey
        End If

        sOutput = oCHtmlGenerator.GetOutput("SELECT " & oLWObjectTypes.emCatalogTable & ".*, " & oLWObjectTypes.emCatalogTable & "_1.PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emCatalogTable & ", " & oLWObjectTypes.emCatalogTable & " " & oLWObjectTypes.emCatalogTable & "_1 WHERE " & oLWObjectTypes.emCatalogTable & ".PKEY='" & sGenerateCatalogOfflinePkey & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emCatalogTable & "_1.PKEY")

        outputFileSW = outputFile.CreateText(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & dbDataSet.Tables(2).Rows(0).Item("PINDEXTEMPLATE"))
        outputFileSW.Write(sOutput)

        outputFileSW.Close()

        oCHtmlGenerator.Dispose()
        oCHtmlGenerator = Nothing

        'copy all templates to directory
        Dim oFiles() As String
        Dim sFile As String
        Dim sFileName As String
        Dim sFilePath As String
        Dim oDirectories() As String
        Dim sDirectory As String
        Dim sDirectoryName As String
        Dim sFileExt As String

        'copy from templates directory
        copyFilesInDirectory(rootWebPath & "templates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.xsl")
        copyFilesInDirectory(rootWebPath & "templates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.js")
        copyFilesInDirectory(rootWebPath & "templates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.vbs")
        copyFilesInDirectory(rootWebPath & "templates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.gif")
        copyFilesInDirectory(rootWebPath & "templates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.jpg")
        copyFilesInDirectory(rootWebPath & "templates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.css")

        'copy from offlinetemplates directory
        copyFilesInDirectory(rootWebPath & "OfflineTemplates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.xsl")
        copyFilesInDirectory(rootWebPath & "OfflineTemplates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.js")
        copyFilesInDirectory(rootWebPath & "OfflineTemplates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.vbs")
        copyFilesInDirectory(rootWebPath & "OfflineTemplates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.gif")
        copyFilesInDirectory(rootWebPath & "OfflineTemplates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.jpg")
        copyFilesInDirectory(rootWebPath & "OfflineTemplates", sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\", "*.css")


        oDirectories = oDirectory.GetDirectories(rootWebPath & "Templates")

        For Each sDirectory In oDirectories

            oFiles = oDirectory.GetFiles(sDirectory)
            sDirectoryName = getFileName(sDirectory)

            oDirectory.CreateDirectory(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & sDirectoryName)

            copyFilesInDirectory(sDirectory, sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\" & sDirectoryName & "\", "*.*")

        Next

        Dim sVolumeDirectory As String = VolumeDirectory

        'copy all images

        oDirectory.CreateDirectory(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\Images\")

        For Each oDRow In dbDataSet.Tables(1).Rows

            sFileName = oDRow.Item("ppicturekey")
            If oDRow.Item("pfileextension") = 1 Then
                sFileName = sFileName & ".gif"
            Else
                sFileName = sFileName & ".jpg"
            End If

            sFilePath = sVolumeDirectory & sFileName
            If oFile.Exists(sFilePath) Then
                oFile.Copy(sFilePath, sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\Images\" & sFileName)
            Else
                sError = sError & "File " & sFilePath & " Is Missing.<br>"
            End If

            counter = counter + 1
            oSession("Precent") = CInt((counter * 100) / totalCount).ToString()
        Next

        'copy all winobject

        oDirectory.CreateDirectory(sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\Object\")


        For Each oDRow In dbDataSet.Tables(3).Rows

            If oDRow.Item("plinktonetworkfile") = oLWObjectTypes.emYes Then
                sFilePath = oDRow.Item("porgfilename")
            Else
                sFilePath = sVolumeDirectory & "Catalog\" & sGenerateCatalogOfflinePkey & "\Object\" & oDRow.Item("pkey") & "." & oDRow.Item("pfileextension")
            End If

            sFileName = getFileName(sFilePath)
            If oFile.Exists(sFilePath) Then
                oFile.Copy(sFilePath, sPublishOfflineDirectoryWithCatalogs & sGenerateCatalogOfflinePkey & "\Object\" & sFileName)
            Else
                sError = sError & "File " & sFilePath & " Is Missing.<br>"
            End If

            counter = counter + 1
            oSession("Precent") = CInt((counter * 100) / totalCount).ToString()
        Next

        dbDataSet.Dispose()
        dbDataSet = Nothing

        oConn.Close_Connection()
        oConn = Nothing
        oFsr = Nothing
        oLWObjectTypes = Nothing

    End Function

    Public Function PublishCatalogs(ByVal sPkeyCatalogs As String) As String

        Dim sGenerateCatalogsOfflinePkey As String = sPkeyCatalogs
        Dim sGenerateCatalogs() As String
        Dim sError As String
        Dim i As Integer

        If sGenerateCatalogsOfflinePkey = "" Then
            Exit Function
        End If

        'NEED TO DEBUG.
        sGenerateCatalogs = sGenerateCatalogsOfflinePkey.Split("|")

        For i = 0 To sGenerateCatalogs.Length - 1
            If sGenerateCatalogs(i) <> "" Then
                sError = sError & "<br><h2>Publishing Catalog:" & sGenerateCatalogs(i) & "</h2><br>"
                sError = sError & GenerateCatalogOffline(sGenerateCatalogs(i))
            End If
        Next

        PublishCatalogs = sError

    End Function

    Public Function PublishCatalogsForCD(ByVal sCDPkey As String) As String

        Dim sSqlCDQuery As String
        Dim dbCDSet As New DataSet
        Dim oConnCD As New CConnection
        Dim i As Integer
        Dim sCatalog As String
        Dim sError As String

        oSession.Add("percent", "1234567")

        sSqlCDQuery = "SELECT * FROM T_CAT_CD_CATALOG WHERE PCDKEY='" & sCDPkey & "'"

        If gemAppMode = "Access" Then
            oConnCD.Open_Access_Connection(ODBCNAME)
        Else
            oConnCD.Open_Oracle_Connection(ODBCNAME)
        End If

        oConnCD.Open_Dataset(sSqlCDQuery, dbCDSet)
        oConnCD.Close_Connection()
        oConnCD = Nothing
        If dbCDSet.Tables(0).Rows.Count > 0 Then
            For i = 0 To dbCDSet.Tables(0).Rows.Count - 1
                sCatalog = dbCDSet.Tables(0).Rows(i)("PKEY") & ""
                sError = sError & "<br><h2>Publishing Catalog:" & sCatalog & "</h2><br>"
                sError = sError & GenerateCatalogOffline(sCatalog)
            Next
        End If

        dbCDSet.Dispose()
        dbCDSet = Nothing
        PublishCatalogsForCD = sError

    End Function

    Private Function getFileExtension(ByVal sFileName As String)
        Dim iPos As Integer

        iPos = InStrRev(sFileName, ".")
        If iPos > 0 Then
            getFileExtension = Mid(sFileName, iPos + 1)
        Else
            getFileExtension = ""
        End If

    End Function

    Private Function getFileName(ByVal sFilePath As String)
        Dim iPos As Integer

        iPos = InStrRev(sFilePath, "\")
        If iPos > 0 Then
            getFileName = Mid(sFilePath, iPos + 1)
        Else
            getFileName = sFilePath
        End If

    End Function

End Class
