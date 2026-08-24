Imports System.IO

Public Class CHtmlGenerator
    Implements IDisposable

    '===================================================================
    'PURPOSE:  HTML page generator.
    '
    'REVISIONS:
    '    rv date     who pr        reason for change
    '    30/12/03    Royi Alush    Created
    '
    'NOTES:
    '
    '
    '====================================================================

    Enum eWorkMode
        OfflineMode = 1
        OnlineMode = 2
    End Enum

    Private xmlOutput As String
    Private sTemplateContent As String
    Private StartDelimeter As String = "<#"
    Private EndDelimeter As String = "#>"
    Private sODBCName As String
    Private oConn As CConnection
    Private dbDataSet As Data.DataSet
    Private dbDataSetChilds As Data.DataSet
    Private dbDataSetPartLabels As Data.DataSet
    Private oLWObjectTypes As New LWObjectTypes
    Private sObjType As String
    Private sIndexFile As String
    Private sCatalogName As String
    Private sAuthentication As String
    Private oAuthentication As cAuthentication
    Private gemAppMode As String
    Private sImageMapManage As String
    Private sCurrentURL As String
    Private sWorkMode As eWorkMode = eWorkMode.OnlineMode
    Private iFrameNumber As Integer = 1 'for use only in offline mode.
    Private pImportKeyProp As String = ""
    Private pImportKeyValue As String = ""
    Private pLatestDocRevisionPkey As String = ""
    Private pLatestItemRevisionPkey As String = ""
    Private iCurrentLoopCount As Integer = 0
    Private isConnectionShared As Boolean = False

    Public SCatalog As String
    Public SParent As String
    Public SessionProp As String = ""
    Public ServerObj As HttpServerUtility

    Public Sub New(ByVal filePath As String, ByVal bFile As Boolean, ByVal sODBCName As String, ByVal sObjType As String, ByVal sAuthorized As String, ByVal gemAppMode As String, ByVal sImageMapManage As String, Optional ByRef oConn As CConnection = Nothing)
        Dim oFsr As StreamReader
        oFsr = File.OpenText(filePath)
        sTemplateContent = oFsr.ReadToEnd()
        oFsr.Close()
        oFsr = Nothing
        Me.sODBCName = sODBCName
        Me.sObjType = sObjType
        Me.sAuthentication = sAuthorized
        Me.gemAppMode = gemAppMode
        Me.sImageMapManage = sImageMapManage
        If Not oConn Is Nothing Then
            isConnectionShared = True
            Me.oConn = oConn
        Else
            'Open oracle connection
            Me.oConn = New CConnection
            If Me.gemAppMode = "Access" Then
                Me.oConn.Open_Access_Connection(sODBCName)
            Else
                Me.oConn.Open_Oracle_Connection(sODBCName)
            End If
        End If
        Me.oAuthentication = New cAuthentication(sAuthorized, sODBCName, gemAppMode, oConn)
    End Sub

    Public Sub New(ByVal sTemplate As String, ByVal sODBCName As String, ByVal sObjType As String, ByVal sAuthorized As String, ByVal gemAppMode As String, ByVal sImageMapManage As String, Optional ByRef oConn As CConnection = Nothing)
        sTemplateContent = sTemplate
        Me.sODBCName = sODBCName
        Me.sObjType = sObjType
        Me.sAuthentication = sAuthorized
        Me.gemAppMode = gemAppMode
        Me.sImageMapManage = sImageMapManage
        If Not oConn Is Nothing Then
            isConnectionShared = True
            Me.oConn = oConn
        Else
            'Open oracle connection
            Me.oConn = New CConnection
            If Me.gemAppMode = "Access" Then
                Me.oConn.Open_Access_Connection(sODBCName)
            Else
                Me.oConn.Open_Oracle_Connection(sODBCName)
            End If
        End If
        Me.oAuthentication = New cAuthentication(sAuthorized, sODBCName, gemAppMode, oConn)
    End Sub


    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            ' Free other state (managed objects).
            If Not isConnectionShared Then
                oConn.Close_Connection()
                oConn = Nothing
            End If
            oLWObjectTypes = Nothing
            dbDataSet = Nothing
            dbDataSetChilds = Nothing
            If Not IsNothing(dbDataSetPartLabels) Then
                dbDataSetPartLabels.Dispose()
            End If
            dbDataSetPartLabels = Nothing
        End If
        ' Free your own state (unmanaged objects).
        ' Set large fields to null.
    End Sub

    Protected Overrides Sub Finalize()
        ' Simply call Dispose(False).
        Dispose(False)
    End Sub

    Public Function GetOutput(ByVal sSqlQuery As String, Optional ByVal dbPredefinedDataSet As Data.DataSet = Nothing) As String

        Dim firstPos As Integer = 1
        Dim lastPos As Integer
        Dim tagName As String
        Dim iStartLoopPos As Integer
        Dim iEndLoopPos As Integer
        Dim sLoopTemplate As String
        Dim sOutput As String
        Dim oParts As CHtmlGenerator
        Dim oWinObjects As CHtmlGenerator
        Dim oPictureProperty As CHtmlGenerator
        Dim i As Integer
        Dim sSqlQueryChilds As String
        Dim oDRow As Data.DataRow
        Dim sFrameName As String
        Dim sSimpleTagText As String
        Dim sPropName As String
        Dim oAclPermissions As cACLPermissions
        Dim dTotal As Double
        Dim dStartDate As Date
        Dim sTempObjectClass As String = ""
        Dim bDatasetMode As Boolean = False

        dStartDate = Now()
        'dTotal = Timer()


        oAclPermissions = New cACLPermissions(sAuthentication, sODBCName, gemAppMode, sObjType, oConn)
		'GetOutput="<b>" + sSqlQuery + "</b>"
		'exit function
        'Open command
        If IsNothing(dbPredefinedDataSet) Then
            dbDataSet = New Data.DataSet
            oConn.Open_Dataset(sSqlQuery, dbDataSet)
        Else
            dbDataSet = dbPredefinedDataSet
            bDatasetMode = True
        End If

        iCurrentLoopCount = dbDataSet.Tables(0).Rows.Count

        If dbDataSet.Tables(0).Rows.Count > 0 Then

            'cache partlabels or documentlabels
            If Me.sObjType = oLWObjectTypes.emPart Then
                Dim sSqlPartLabel As String
                dbDataSetPartLabels = New Data.DataSet
                sSqlPartLabel = "SELECT PKEY,PHEBDESC,PENGDESC FROM " & oLWObjectTypes.emPartLabelsTable & " WHERE PKEYCATALOG='" & SCatalog & "'"
                oConn.Open_Dataset(sSqlPartLabel, dbDataSetPartLabels, "PartLabel")
			ElseIf Me.sObjType = oLWObjectTypes.emCatalog Then
                If InStr(dbDataSet.Tables(0).Rows(0)("PDIRECTION").ToString.ToUpper, "LTR") Then
                    System.Web.HttpContext.Current.Session("dir" + dbDataSet.Tables(0).Rows(0)("PKEY")) = "ltr"
                Else
                    System.Web.HttpContext.Current.Session("dir" + dbDataSet.Tables(0).Rows(0)("PKEY")) = "rtl"
                End If
            End If
        End If

        Dim iRows As Integer
        Dim sXmlOutputTemp As String
        Dim bIsPermit As Boolean = True

        For iRows = 0 To dbDataSet.Tables(0).Rows.Count - 1
            'acl permissions checks only few objecttypes.
            If Me.sObjType = oLWObjectTypes.emCatalog Or Me.sObjType = oLWObjectTypes.emPage Or Me.sObjType = oLWObjectTypes.emPart Or Me.sObjType = oLWObjectTypes.emWinObject Then
                If bDatasetMode Then
                    Try
                        sTempObjectClass = dbDataSet.Tables(0).Rows(iRows).Item("POBJECTTYPE") & ""
                    Catch ex As Exception
                        sTempObjectClass = ""
                    End Try
                End If
                bIsPermit = CBool(oAclPermissions.HasPermission(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), "P_SHOW", sTempObjectClass))
            End If

            If bIsPermit Then

                sXmlOutputTemp = sTemplateContent

                firstPos = InStr(1, sXmlOutputTemp, StartDelimeter, CompareMethod.Text)

                While firstPos <> 0

                    lastPos = InStr(firstPos, sXmlOutputTemp, EndDelimeter, CompareMethod.Text)
                    tagName = Mid(sXmlOutputTemp, firstPos + Len(StartDelimeter), lastPos - firstPos - Len(EndDelimeter))
                    Select Case tagName

                        Case "WEBLINK_LOOP_BEGIN"
                            'search for end tag PARTS_LOOP_END and send.
                            iStartLoopPos = lastPos + Len(EndDelimeter)
                            iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "WEBLINK_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)
                            sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndLoopPos + Len(StartDelimeter) + Len(EndDelimeter) + Len("WEBLINK_LOOP_END"))

                            'Open command - get childs
                            dbDataSetChilds = New Data.DataSet
                            sSqlQueryChilds = "SELECT " & oLWObjectTypes.emNodesTable & ".PKEY FROM " & oLWObjectTypes.emNodesTable & ", " & oLWObjectTypes.emPartTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") &  "' AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY "
                            oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                            'check if we need to put the childs in sort order.
                            Dim sPkeyChilds As String
                            Dim aPkeyChilds() As String
                            '''Dim aPkeyChildsAlreadyOutput As New ArrayList
                            '''Dim iPkeyChildsAlreadyOutput As Integer
                            '''aPkeyChildsAlreadyOutput.Clear()
                            '''iPkeyChildsAlreadyOutput = 0
                            '''Dim aPkeyOrderBy As New ArrayList

                            If Not IsDBNull(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) Then
                                If Len(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) > 0 Then
                                    sPkeyChilds = dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")
                                    aPkeyChilds = Split(sPkeyChilds, "|")
                                End If
                            End If

                            '''Dim iRow As Integer
                            '''Dim iRow2 As Integer
                            '''Dim iMaxChildsAlreadyOutput As Integer

                            '''Dim bFound As Boolean

                            '''If IsNothing(aPkeyChilds) Then
                            '''    For Each oDRow In dbDataSetChilds.Tables(0).Rows
                            '''        If CBool(oAclPermissions.HasPermission(oDRow.Item("PKEY"), "P_Show")) Then
                            '''            aPkeyOrderBy.Add(oDRow.Item("PKEY"))
                            '''            'Add part from the template
                            '''            'oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''            'If gemAppMode = "Access" Then
                            '''            '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'")
                            '''            'Else
                            '''            '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''            'End If
                            '''            'sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''            'oParts.Dispose()
                            '''            'oParts = Nothing
                            '''            'firstPos = firstPos + Len(sOutput)
                            '''        End If
                            '''        'end adding
                            '''    Next
                            '''Else

                            '''    Dim oDRowChilds() As DataRow

                            '''    For iRow2 = 0 To aPkeyChilds.Length - 1
                            '''        oDRowChilds = dbDataSetChilds.Tables(0).Select("PKEY = '" & aPkeyChilds(iRow2) & "'")
                            '''        If oDRowChilds.Length > 0 Then
                            '''            If CBool(oAclPermissions.HasPermission(aPkeyChilds(iRow2), "P_Show")) Then
                            '''                aPkeyOrderBy.Add(aPkeyChilds(iRow2))
                            '''                'Add part from the template
                            '''                'oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''                'If gemAppMode = "Access" Then
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & aPkeyChilds(iRow2) & "'")
                            '''                'Else
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & aPkeyChilds(iRow2) & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''                'End If
                            '''                'sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''                'oParts.Dispose()
                            '''                'oParts = Nothing
                            '''                'firstPos = firstPos + Len(sOutput)
                            '''                'end adding
                            '''                'ReDim Preserve aPkeyChildsAlreadyOutput(iPkeyChildsAlreadyOutput)
                            '''                aPkeyChildsAlreadyOutput.Add(dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY"))
                            '''                iPkeyChildsAlreadyOutput += 1
                            '''            End If
                            '''        End If
                            '''    Next

                            '''    If IsNothing(aPkeyChildsAlreadyOutput) Then
                            '''        iMaxChildsAlreadyOutput = 0
                            '''    Else
                            '''        iMaxChildsAlreadyOutput = aPkeyChildsAlreadyOutput.Count
                            '''    End If

                            '''    'for each child that not exist in pkeychilds.
                            '''    For iRow = 0 To dbDataSetChilds.Tables(0).Rows.Count - 1
                            '''        bFound = False
                            '''        For iRow2 = 0 To iMaxChildsAlreadyOutput - 1
                            '''            If aPkeyChildsAlreadyOutput(iRow2) = dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY") Then
                            '''                bFound = True
                            '''                Exit For
                            '''            End If
                            '''        Next
                            '''        If Not bFound Then
                            '''            If CBool(oAclPermissions.HasPermission(dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY"), "P_Show")) Then
                            '''                aPkeyOrderBy.Add(dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY"))
                            '''                ''Add part from the template
                            '''                'oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''                'If gemAppMode = "Access" Then
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY") & "'")
                            '''                'Else
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY") & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''                'End If
                            '''                'sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''                'oParts.Dispose()
                            '''                'oParts = Nothing
                            '''                'firstPos = firstPos + Len(sOutput)
                            '''                ''end adding
                            '''            End If
                            '''        End If
                            '''    Next
                            '''End If


                            ''''For Each oDRow In dbDataSetChilds.Tables(0).Rows
                            ''''    'Add part from the template
                            ''''    oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode)
                            ''''    If gemAppMode = "Access" Then
                            ''''        sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM (" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'")
                            ''''    Else
                            ''''        sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            ''''    End If
                            ''''    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            ''''    oParts = Nothing
                            ''''    firstPos = firstPos + Len(sOutput)
                            ''''    'end adding
                            ''''Next

                            '''dbDataSetChilds = Nothing
                            '''aPkeyChildsAlreadyOutput.Clear()
                            '''aPkeyChildsAlreadyOutput = Nothing

                            ''''check if we have at least on child
                            '''If aPkeyOrderBy.Count > 0 Then
                            '''    Dim sINStatement As String
                            '''    Dim iINStatement As Integer
                            '''    sINStatement = ""
                            '''    For iINStatement = 0 To aPkeyOrderBy.Count - 1
                            '''        sINStatement = sINStatement & "'" & aPkeyOrderBy(iINStatement) & "',"
                            '''    Next
                            '''    sINStatement = Mid(sINStatement, 1, Len(sINStatement) - 1)

                            '''    oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''    If gemAppMode = "Access" Then
                            '''        sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")")
                            '''    Else
                            '''        sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''    End If
                            '''    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''    oParts.Dispose()
                            '''    oParts = Nothing
                            '''    firstPos = firstPos + Len(sOutput)
                            '''    'end adding
                            '''End If

                            'check if we have at least on child
                            If dbDataSetChilds.Tables(0).Rows.Count > 0 Then
                                Dim sSubQueryStatement As String
                                Dim iSubQueryStatement As Integer
                                If Not aPkeyChilds Is Nothing Then
                                    sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,0 AS PORDER from dual union "
                                    For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                        sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "'," & iSubQueryStatement & " from dual union "
                                    Next
                                    sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                                Else
                                    sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                                End If
                                oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                oParts.sWorkMode = sWorkMode
                                oParts.SCatalog = SCatalog
                                oParts.SParent = SParent
                                If gemAppMode = "Access" Then
                                    'sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")")
                                Else
                                    sOutput = oParts.GetOutput("SELECT DISTINCT tmpChilds.PORDER," & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & ", " & oLWObjectTypes.emNodesTable & ", " & sSubQueryStatement & " tmpChilds WHERE " & oLWObjectTypes.emPartTable & ".PKEY=tmpChilds.PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+)  AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "' AND PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY ORDER BY tmpChilds.PORDER")
                                End If
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                oParts.Dispose()
                                oParts = Nothing
                                firstPos = firstPos + Len(sOutput)
                                'end adding
                            End If

                            dbDataSetChilds.Dispose()
                            dbDataSetChilds = Nothing

                            '''aPkeyOrderBy.Clear()
                            '''aPkeyOrderBy = Nothing

                        Case "PARTS_LOOP_BEGIN_HOTSPOT"
                            'search for end tag PARTS_LOOP_END and send.
                            iStartLoopPos = lastPos + Len(EndDelimeter)
                            iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "PARTS_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)
                            sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndLoopPos + Len(StartDelimeter) + Len(EndDelimeter) + Len("PARTS_LOOP_END"))

                            'Open command - get childs
                            dbDataSetChilds = New Data.DataSet

                            'sSqlQueryChilds = "SELECT DISTINCT PKEYPART AS PKEY," & oLWObjectTypes.emLinkTable & ".PBALOONNUMBER FROM " & oLWObjectTypes.emBindLinkTable & "," & oLWObjectTypes.emLinkTable & "," & oLWObjectTypes.emNodesTable & " WHERE " & oLWObjectTypes.emBindLinkTable & ".PKEYLINK =" & oLWObjectTypes.emLinkTable & ".PKEY AND " & oLWObjectTypes.emLinkTable & ".PKEYFIGURE=" & oLWObjectTypes.emNodesTable & ".PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND " & oLWObjectTypes.emNodesTable & ".POBJECTTYPE=3 AND " & oLWObjectTypes.emNodesTable & ".PGLOBALSTATUS<>4 ORDER BY " & oLWObjectTypes.emLinkTable & ".PBALOONNUMBER"
                            sSqlQueryChilds = "SELECT DISTINCT PKEYPART AS PKEY,T_CAT_LINK.PBALOONNUMBER " _
                                            & "FROM T_CAT_BIND_LINK, T_CAT_LINK, T_CAT_NODES " _
                                            & "WHERE T_CAT_BIND_LINK.PKEYLINK =T_CAT_LINK.PKEY AND " _
                                            & "T_CAT_LINK.PKEY = T_CAT_NODES.PKEY " _
                                            & "AND T_CAT_NODES.PPARENTKEY IN (SELECT PKEY FROM T_CAT_NODES WHERE PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=3 AND PGLOBALSTATUS<>4) " _
                                            & "AND T_CAT_NODES.POBJECTTYPE=5 " _
                                            & "AND T_CAT_NODES.PGLOBALSTATUS<>4 " _
                                            & "ORDER BY T_CAT_LINK.PBALOONNUMBER "

                            oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                            'check if we need to put the childs in sort order.
                            Dim sPkeyChilds As String
                            Dim aPkeyChilds() As String
                            'Dim aPkeyChildsAlreadyOutput As New ArrayList
                            'Dim iPkeyChildsAlreadyOutput As Integer
                            'iPkeyChildsAlreadyOutput = 0
                            'aPkeyChildsAlreadyOutput.Clear()
                            'Dim aPkeyOrderBy As New ArrayList

                            'don't order by father PKEYCHILDS because orbotech wants to do order by pbaloonnumber.
                            Dim iChilds As Integer
                            For iChilds = 0 To dbDataSetChilds.Tables(0).Rows.Count - 1
                                ReDim Preserve aPkeyChilds(iChilds)
                                aPkeyChilds.SetValue(dbDataSetChilds.Tables(0).Rows(iChilds).Item(0), iChilds)
                                'sPkeyChilds = dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")
                                'aPkeyChilds = Split(sPkeyChilds, "|")
                            Next
                            'If Not IsDBNull(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) Then
                            '    If Len(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) > 0 Then
                            '        sPkeyChilds = dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")
                            '        aPkeyChilds = Split(sPkeyChilds, "|")
                            '    End If
                            'End If

                            '''Dim iRow As Integer
                            '''Dim iRow2 As Integer
                            '''Dim iMaxChildsAlreadyOutput As Integer

                            '''Dim bFound As Boolean

                            '''If IsNothing(aPkeyChilds) Then
                            '''    For Each oDRow In dbDataSetChilds.Tables(0).Rows
                            '''        If CBool(oAclPermissions.HasPermission(oDRow.Item("PKEY"), "P_Show")) Then
                            '''            aPkeyOrderBy.Add(oDRow.Item("PKEY"))
                            '''            'Add part from the template
                            '''            'oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''            'If gemAppMode = "Access" Then
                            '''            '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'")
                            '''            'Else
                            '''            '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''            'End If
                            '''            'sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''            'oParts.Dispose()
                            '''            'oParts = Nothing
                            '''            'firstPos = firstPos + Len(sOutput)
                            '''        End If
                            '''        'end adding
                            '''    Next
                            '''Else

                            '''    Dim oDRowChilds() As DataRow

                            '''    For iRow2 = 0 To aPkeyChilds.Length - 1
                            '''        oDRowChilds = dbDataSetChilds.Tables(0).Select("PKEY = '" & aPkeyChilds(iRow2) & "'")
                            '''        If oDRowChilds.Length > 0 Then
                            '''            If CBool(oAclPermissions.HasPermission(aPkeyChilds(iRow2), "P_Show")) Then
                            '''                aPkeyOrderBy.Add(aPkeyChilds(iRow2))
                            '''                'Add part from the template
                            '''                'oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''                'If gemAppMode = "Access" Then
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & aPkeyChilds(iRow2) & "'")
                            '''                'Else
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & aPkeyChilds(iRow2) & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''                'End If
                            '''                'sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''                'oParts.Dispose()
                            '''                'oParts = Nothing
                            '''                'firstPos = firstPos + Len(sOutput)
                            '''                'end adding
                            '''                'ReDim Preserve aPkeyChildsAlreadyOutput(iPkeyChildsAlreadyOutput)
                            '''                aPkeyChildsAlreadyOutput.Add(dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY"))
                            '''                iPkeyChildsAlreadyOutput += 1
                            '''            End If
                            '''        End If
                            '''    Next

                            '''    If IsNothing(aPkeyChildsAlreadyOutput) Then
                            '''        iMaxChildsAlreadyOutput = 0
                            '''    Else
                            '''        iMaxChildsAlreadyOutput = aPkeyChildsAlreadyOutput.Count
                            '''    End If

                            '''    'for each child that not exist in pkeychilds.
                            '''    For iRow = 0 To dbDataSetChilds.Tables(0).Rows.Count - 1
                            '''        bFound = False
                            '''        For iRow2 = 0 To iMaxChildsAlreadyOutput - 1
                            '''            If aPkeyChildsAlreadyOutput(iRow2) = dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY") Then
                            '''                bFound = True
                            '''                Exit For
                            '''            End If
                            '''        Next
                            '''        If Not bFound Then
                            '''            If CBool(oAclPermissions.HasPermission(dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY"), "P_Show")) Then
                            '''                aPkeyOrderBy.Add(dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY"))
                            '''                'Add part from the template
                            '''                'oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''                'If gemAppMode = "Access" Then
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY") & "'")
                            '''                'Else
                            '''                '    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY") & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''                'End If
                            '''                'sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''                'oParts.Dispose()
                            '''                'oParts = Nothing
                            '''                'firstPos = firstPos + Len(sOutput)
                            '''                'end adding
                            '''            End If
                            '''        End If
                            '''    Next

                            '''End If


                            ''''For Each oDRow In dbDataSetChilds.Tables(0).Rows
                            ''''    'Add part from the template
                            ''''    oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode)
                            ''''    If gemAppMode = "Access" Then
                            ''''        sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM (" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'")
                            ''''    Else
                            ''''        sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & oDRow.Item("PKEY") & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            ''''    End If
                            ''''    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            ''''    oParts = Nothing
                            ''''    firstPos = firstPos + Len(sOutput)
                            ''''    'end adding
                            ''''Next

                            '''dbDataSetChilds = Nothing
                            '''aPkeyChildsAlreadyOutput.Clear()
                            '''aPkeyChildsAlreadyOutput = Nothing

                            'check if we have at least on child

                            If dbDataSetChilds.Tables(0).Rows.Count > 0 Then
                                Dim sSubQueryStatement As String
                                Dim iSubQueryStatement As Integer
                                If Not aPkeyChilds Is Nothing Then
                                    sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,'" & dbDataSetChilds.Tables(0).Rows(0).Item(1) & "' AS PORDER from dual union "
                                    For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                        sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "','" & dbDataSetChilds.Tables(0).Rows(iSubQueryStatement).Item(1) & "' from dual union "
                                    Next
                                    sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                                Else
                                    sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                                End If
                                oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                oParts.sWorkMode = sWorkMode
                                oParts.SCatalog = SCatalog
                                oParts.SParent = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
                                If gemAppMode = "Access" Then
                                    'sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")")
                                Else
                                    sOutput = oParts.GetOutput("SELECT DISTINCT tmpChilds.PORDER," & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & ", " & oLWObjectTypes.emNodesTable & ", " & sSubQueryStatement & " tmpChilds WHERE " & oLWObjectTypes.emPartTable & ".PKEY=tmpChilds.PKEY AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+)  AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY(+)='" & SCatalog & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY AND (" & oLWObjectTypes.emPartTable & ".PKEYTYPE<>'43' OR " & oLWObjectTypes.emPartTable & ".PKEYTYPE IS NULL) ORDER BY tmpChilds.PORDER")
                                End If
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                oParts.Dispose()
                                oParts = Nothing
                                firstPos = firstPos + Len(sOutput)
                                'end adding
                            End If


                            '''If aPkeyOrderBy.Count > 0 Then
                            '''    Dim sINStatement As String
                            '''    Dim iINStatement As Integer
                            '''    sINStatement = ""
                            '''    For iINStatement = 0 To aPkeyOrderBy.Count - 1
                            '''        sINStatement = sINStatement & "'" & aPkeyOrderBy(iINStatement) & "',"
                            '''    Next
                            '''    sINStatement = Mid(sINStatement, 1, Len(sINStatement) - 1)

                            '''    oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage)
                            '''    If gemAppMode = "Access" Then
                            '''        sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")")
                            '''    Else
                            '''        sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG")
                            '''    End If
                            '''    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                            '''    oParts.Dispose()
                            '''    oParts = Nothing
                            '''    firstPos = firstPos + Len(sOutput)
                            '''    'end adding
                            '''End If

                            dbDataSetChilds.Dispose()
                            dbDataSetChilds = Nothing
                            ''aPkeyOrderBy.Clear()
                            ''aPkeyOrderBy = Nothing

                        Case "DOCUMENT_LOOP_BEGIN"
                            'search for end tag DOCUMENTS_LOOP_END and send.

                            iStartLoopPos = lastPos + Len(EndDelimeter)
                            iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "DOCUMENT_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)
                            sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndLoopPos + Len(StartDelimeter) + Len(EndDelimeter) + Len("DOCUMENT_LOOP_END"))

                            'Open command - get documents
                            dbDataSetChilds = New Data.DataSet
                            sSqlQueryChilds = "SELECT PKEY FROM " & oLWObjectTypes.emNodesTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emWinObject & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                            'check if we need to put the childs in sort order.
                            Dim sPkeyChilds As String
                            Dim aPkeyChilds() As String

                            If Not IsDBNull(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) Then
                                If Len(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) > 0 Then
                                    sPkeyChilds = dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")
                                    aPkeyChilds = Split(sPkeyChilds, "|")
                                End If
                            End If


                            'check if we have at least on child
                            If dbDataSetChilds.Tables(0).Rows.Count > 0 Then
                                Dim sSubQueryStatement As String
                                Dim iSubQueryStatement As Integer
                                If Not aPkeyChilds Is Nothing Then
                                    sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,0 AS PORDER from dual union "
                                    For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                        sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "'," & iSubQueryStatement & " from dual union "
                                    Next
                                    sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                                Else
                                    sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                                End If
                                oWinObjects = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emWinObject, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                oWinObjects.sWorkMode = sWorkMode
                                oWinObjects.SCatalog = SCatalog
                                oWinObjects.SParent = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
                                If gemAppMode = "Access" Then
                                    'sOutput = oWinObjects.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emWinObjectTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emWinObjectTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emWinObjectTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emWinObjectTable & ".PKEY IN (" & sINStatement & ")")
                                Else
                                    sOutput = oWinObjects.GetOutput("SELECT DISTINCT tmpChilds.PORDER, " & oLWObjectTypes.emWinObjectTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME, " & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & ", " & oLWObjectTypes.emNodesTable & " ," & sSubQueryStatement & " tmpChilds WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emWinObject & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emNodesTable & ".PKEY=" & oLWObjectTypes.emWinObjectTable & ".PKEY AND " & oLWObjectTypes.emWinObjectTable & ".PKEY=tmpChilds.PKEY(+) AND " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+)  AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "' ORDER BY tmpChilds.PORDER")
                                End If
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                oWinObjects.Dispose()
                                oWinObjects = Nothing
                                firstPos = firstPos + Len(sOutput)
                                'end adding
                            End If

                            dbDataSetChilds.Dispose()
                            dbDataSetChilds = Nothing

                            '''aPkeyOrderBy.Clear()
                            '''aPkeyOrderBy = Nothing

                        Case "PARENT_PART_BEGIN"

                            'search for end tag PARENT_PART_END and send.
                            iStartLoopPos = lastPos + Len(EndDelimeter)
                            iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "PARENT_PART_END", CompareMethod.Text) - Len(StartDelimeter)
                            sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndLoopPos + Len(StartDelimeter) + Len(EndDelimeter) + Len("PARENT_PART_BEGIN"))

                            ''''Open command - get childs
                            dbDataSetChilds = New Data.DataSet
                            'sSqlQueryChilds = "SELECT PPARENTKEY FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY = '" & SParent & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
                            sSqlQueryChilds = "SELECT PPARENTKEY FROM t_cat_nodes WHERE PKEY = '" & SParent & "' AND POBJECTTYPE=10 AND PGLOBALSTATUS<>4 and pparentkey in (select pkey from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & SCatalog & "')"
                            oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)
                            Dim gParent As String = ""
                            If dbDataSetChilds.Tables(0).Rows.Count > 0 Then
                                gParent = dbDataSetChilds.Tables(0).Rows(0)(0) & ""
                            End If
                            ''''for each child that not exist in pkeychilds.
                            '''Dim iRow As Integer

                            '''For iRow = 0 To dbDataSetChilds.Tables(0).Rows.Count - 1
                            '''    'check if the part is not at root level.
                            '''    If dbDataSetChilds.Tables(0).Rows(iRow).Item("PPARENTKEY") <> SCatalog Then
                            If CBool(oAclPermissions.HasPermission(SParent, "P_Show")) Then
                                'Add part from the template
                                oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                oParts.sWorkMode = sWorkMode
                                oParts.SCatalog = SCatalog
                                oParts.SParent = gParent
                                If gemAppMode = "Access" Then
                                    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "') LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & SParent & "'")
                                Else
                                    sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & "," & oLWObjectTypes.emNodesTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEY='" & SParent & "'  AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "' AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY=" & oLWObjectTypes.emPartTable & ".PKEY AND " & oLWObjectTypes.emNodesTable & ".PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' ")
                                End If
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                oParts.Dispose()
                                oParts = Nothing
                                firstPos = firstPos + Len(sOutput)
                                ''end adding
                                '''End If
                            End If
                            '''Next

                            dbDataSetChilds = Nothing

                        Case "CATALOG_LOOP_BEGIN"

                            'search for end tag CATALOG_LOOP_END and send.
                            iStartLoopPos = lastPos + Len(EndDelimeter)
                            iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "CATALOG_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)
                            sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndLoopPos + Len(StartDelimeter) + Len(EndDelimeter) + Len("CATALOG_LOOP_BEGIN"))

                            'Open command - get childs
                            dbDataSetChilds = New Data.DataSet
                            sSqlQueryChilds = "SELECT PKEY FROM " & oLWObjectTypes.emCD_CATALOG & " WHERE PCDKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' "
                            oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                            'for each child that not exist in pkeychilds.
                            Dim iRow As Integer

                            For iRow = 0 To dbDataSetChilds.Tables(0).Rows.Count - 1
                                'check if the part is not at root level.
                                If CBool(oAclPermissions.HasPermission(dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY"), "P_Show")) Then
                                    'Add part from the template
                                    Dim oCatalog As CHtmlGenerator
                                    oCatalog = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emCatalog, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                    oCatalog.sWorkMode = sWorkMode
                                    oCatalog.SCatalog = SCatalog
                                    oCatalog.SParent = SParent
                                    sOutput = oCatalog.GetOutput("SELECT " & oLWObjectTypes.emCatalogTable & ".* ," & oLWObjectTypes.emCatalogTable & ".PHEBDESC Catalog_Name FROM " & oLWObjectTypes.emCatalogTable & ", " & oLWObjectTypes.emNodesTable & " WHERE T_CAT_CATALOG.PKEY=T_CAT_NODES.PKEY AND T_CAT_CATALOG.PKEY='" & dbDataSetChilds.Tables(0).Rows(iRow).Item("PKEY") & "'")
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                    oCatalog.Dispose()
                                    oCatalog = Nothing
                                    firstPos = firstPos + Len(sOutput)
                                    'end adding
                                End If
                            Next

                            dbDataSetChilds = Nothing

                        Case "IMAGEMAPLINKONLY"

                            If sWorkMode = eWorkMode.OnlineMode Then
                                If sImageMapManage = "PerCatalog" Then
                                    sSimpleTagText = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & SCatalog & "/" & dbDataSet.Tables(0).Rows(iRows).Item("PPICTUREKEY") & "." & ConvertFromIntToExtension(dbDataSet.Tables(0).Rows(iRows).Item("PFILEEXTENSION"))
                                Else
                                    sSimpleTagText = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & dbDataSet.Tables(0).Rows(iRows).Item("PPICTUREKEY") & "." & ConvertFromIntToExtension(dbDataSet.Tables(0).Rows(iRows).Item("PFILEEXTENSION"))
                                End If
                            Else
                                sSimpleTagText = "Images/" & dbDataSet.Tables(0).Rows(iRows).Item("PPICTUREKEY") & "." & ConvertFromIntToExtension(dbDataSet.Tables(0).Rows(iRows).Item("PFILEEXTENSION"))
                            End If

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " INDEX "
                            Dim tmpDbDataset As New Data.DataSet
                            Dim sSqlQueryTemplate As String

                            If (SCatalog = "") Then
                                sSqlQueryTemplate = "select PINDEXTEMPLATE from " & oLWObjectTypes.emCatalogTable & " where pkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "'"
                            Else
                                sSqlQueryTemplate = "select PINDEXTEMPLATE from " & oLWObjectTypes.emCatalogTable & " where pkey='" & SCatalog & "'"
                            End If

                            oConn.Open_Dataset(sSqlQueryTemplate, tmpDbDataset)

                            sFrameName = tmpDbDataset.Tables(0).Rows(0).Item("PINDEXTEMPLATE")

                            tmpDbDataset.Dispose()
                            tmpDbDataset = Nothing

                            If sWorkMode = eWorkMode.OfflineMode Then
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sFrameName & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            Else
                                If (SCatalog = "") Then
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & """ShowTemplate.aspx?Template=" & sFrameName & "&amp;Pkey=" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "&amp;PkeyCatalog=" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "&amp;ParentKey=r&amp;Type=" & oLWObjectTypes.emCatalog & """" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                Else
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & """ShowTemplate.aspx?Template=" & sFrameName & "&amp;Pkey=" & SCatalog & "&amp;PkeyCatalog=" & SCatalog & "&amp;ParentKey=r&amp;Type=" & oLWObjectTypes.emCatalog & """" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                End If
                            End If

                        Case "Index"

                            Dim dbDatasetRelation As New Data.DataSet
                            Dim sSqlQueryRelations As String
							Dim sCatalogKey As String
                            sCatalogKey = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
                            If gemAppMode = "Access" Then
                                sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE, T_CAT_PAGE.PKEYCHILDS AS PkeyChilds " _
                                                & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                                & "WHERE (((T_CAT_PAGE.PKEYCATALOG)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                                & "union " _
                                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE, T_CAT_PART.PKEYCHILDS AS PkeyChilds " _
                                                & "FROM (T_CAT_PART INNER JOIN T_CAT_NODES ON T_CAT_PART.PKEY = T_CAT_NODES.PKEY ) LEFT JOIN T_CAT_OBJECTTYPES ON T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY " _
                                                & "WHERE (((T_CAT_PART.PKEYCATALOG)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "')) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                                & "union " _
                                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds " _
                                                & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                                & "WHERE (((T_CAT_FOLDER.PKEYCATALOG)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') AND T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "'  OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                                & "union " _
                                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds  " _
                                                & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                                & "WHERE (((T_CAT_CATALOG.PKEY)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                                & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
								sSqlQueryRelations = "SELECT T_CAT_CATALOG_PKEYS.PKEY, T_CAT_CATALOG_PKEYS.PPARENTKEY, T_CAT_CATALOG_PKEYS.POBJECTTYPE, T_CAT_CATALOG_PKEYS.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE, T_CAT_PAGE.PKEYCHILDS AS PkeyChilds , T_CAT_PAGE.PPAGENUMBER  " _
                                    & "FROM T_CAT_PAGE,T_CAT_CATALOG_PKEYS " _
                                     & "WHERE(T_CAT_PAGE.PKEY = T_CAT_CATALOG_PKEYS.PKEY) " _
                                     & " and pobjecttype=2 and T_CAT_CATALOG_PKEYS.PKEYCATALOG='" & sCatalogKey & "' and (T_CAT_PAGE.PPUBLISH<>'no' OR T_CAT_PAGE.PPUBLISH IS NULL)" _
                                     & "union " _
                                     & "SELECT T_CAT_CATALOG_PKEYS.PKEY, T_CAT_CATALOG_PKEYS.PPARENTKEY, T_CAT_CATALOG_PKEYS.POBJECTTYPE, T_CAT_CATALOG_PKEYS.PGLOBALSTATUS, T_CAT_PART.PHEBDESC, T_CAT_OBJECTTYPES.PTYPENAME, T_CAT_PART.PPARTTEMPLATE, T_CAT_PART.PKEYCHILDS AS PkeyChilds, '' AS PPAGENUMBER " _
                                     & "FROM (T_CAT_PART INNER JOIN T_CAT_CATALOG_PKEYS ON T_CAT_PART.PKEY = T_CAT_CATALOG_PKEYS.PKEY) LEFT JOIN T_CAT_OBJECTTYPES ON T_CAT_PART.PKEYTYPE = T_CAT_OBJECTTYPES.PKEY " _
                                     & "WHERE (((T_CAT_OBJECTTYPES.PTYPENAME)<>'GeneralLink' Or (T_CAT_OBJECTTYPES.PTYPENAME) Is Null) AND ((T_CAT_PART.PKEY)=[T_CAT_CATALOG_PKEYS].[PKEY]) AND ((T_CAT_CATALOG_PKEYS.[pobjecttype])=10) AND ((T_CAT_CATALOG_PKEYS.PKEYCATALOG)='" & sCatalogKey & "') AND ((T_CAT_PART.PKEYTYPE)=[T_CAT_OBJECTTYPES].[PKEY]) AND ((T_CAT_PART.PPUBLISH)<>'no' Or (T_CAT_PART.PPUBLISH) Is Null)) " _
                                     & "union " _
                                     & "SELECT T_CAT_CATALOG_PKEYS.PKEY, T_CAT_CATALOG_PKEYS.PPARENTKEY, T_CAT_CATALOG_PKEYS.POBJECTTYPE, T_CAT_CATALOG_PKEYS.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds , '' as PPAGENUMBER  " _
                                     & "FROM T_CAT_FOLDER, T_CAT_CATALOG_PKEYS " _
                                     & "WHERE T_CAT_FOLDER.PKEY = T_CAT_CATALOG_PKEYS.PKEY and T_CAT_CATALOG_PKEYS.PKEYCATALOG='" & sCatalogKey & "' and T_CAT_CATALOG_PKEYS.pobjecttype=11 AND (T_CAT_FOLDER.PPUBLISH<>'no' OR T_CAT_FOLDER.PPUBLISH IS NULL )  " _
                                     & "union " _
                                     & "SELECT T_CAT_CATALOG_PKEYS.PKEY, T_CAT_CATALOG_PKEYS.PPARENTKEY, T_CAT_CATALOG_PKEYS.POBJECTTYPE, T_CAT_CATALOG_PKEYS.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds , '' as PPAGENUMBER " _
                                     & "FROM T_CAT_CATALOG ,T_CAT_CATALOG_PKEYS " _
                                     & "WHERE T_CAT_CATALOG.PKEY='MMa201301' and T_CAT_CATALOG.PKEY = T_CAT_CATALOG_PKEYS.PKEY"

                            Else
                                sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE, T_CAT_PAGE.PKEYCHILDS AS PkeyChilds , T_CAT_PAGE.PPAGENUMBER  " _
                                                & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                                & "WHERE (((T_CAT_PAGE.PKEY IN (SELECT distinct PKEY FROM T_CAT_NODES where  pglobalstatus<>4 and pobjecttype=2 CONNECT BY PRIOR T_CAT_NODES.PKEY=T_CAT_NODES.PPARENTKEY START WITH PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'))) AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                                & "union " _
                                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE, T_CAT_PART.PKEYCHILDS AS PkeyChilds , '' as PPAGENUMBER " _
                                                & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                                & "WHERE (((T_CAT_PART.PKEY IN (SELECT distinct PKEY FROM T_CAT_NODES where  pglobalstatus<>4 and pobjecttype=11 CONNECT BY PRIOR T_CAT_NODES.PKEY=T_CAT_NODES.PPARENTKEY START WITH PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'))) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+)) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                                & "union " _
                                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds , '' as PPAGENUMBER " _
                                                & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                                & "WHERE (((T_CAT_FOLDER.PKEY IN (SELECT distinct PKEY FROM T_CAT_NODES where  pglobalstatus<>4 and pobjecttype=11 CONNECT BY PRIOR T_CAT_NODES.PKEY=T_CAT_NODES.PPARENTKEY START WITH PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'))) AND T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                                & "union " _
                                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds , '' as PPAGENUMBER " _
                                                & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                                & "WHERE (((T_CAT_CATALOG.PKEY)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                                & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
                            End If

                            oConn.Open_Dataset(sSqlQueryRelations, dbDatasetRelation)

                            sSimpleTagText = WriteIndexFrame(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), dbDatasetRelation, 1, 0)

                            dbDatasetRelation = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))




                        Case "INDEX"

                            Dim tmpDbDataset As New Data.DataSet
                            Dim sSqlQueryTemplate As String

                            If (SCatalog = "") Then
                                sSqlQueryTemplate = "select PINDEXTEMPLATE from " & oLWObjectTypes.emCatalogTable & " where pkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "'"
                            Else
                                sSqlQueryTemplate = "select PINDEXTEMPLATE from " & oLWObjectTypes.emCatalogTable & " where pkey='" & SCatalog & "'"
                            End If
                            oConn.Open_Dataset(sSqlQueryTemplate, tmpDbDataset)

                            sFrameName = tmpDbDataset.Tables(0).Rows(0).Item("PINDEXTEMPLATE")

                            tmpDbDataset.Dispose()
                            tmpDbDataset = Nothing

                            If sWorkMode = eWorkMode.OfflineMode Then
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sFrameName & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            Else
                                If (SCatalog = "") Then
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "ShowTemplate.aspx?Template=" & sFrameName & "&amp;Pkey=" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "&amp;PkeyCatalog=" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "&amp;ParentKey=r&amp;Type=" & oLWObjectTypes.emCatalog & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                Else
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "ShowTemplate.aspx?Template=" & sFrameName & "&amp;Pkey=" & SCatalog & "&amp;PkeyCatalog=" & SCatalog & "&amp;ParentKey=r&amp;Type=" & oLWObjectTypes.emCatalog & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                End If
                            End If

                        Case "PREF_RET"

                            Dim pPrefs As New PersonalPref(sODBCName)
                            Dim parsedUser As String
                            Dim dbPrefbase As Data.DataSet
                            parsedUser = sAuthentication.Substring(sAuthentication.LastIndexOf("\") + 1)
                            Dim pos As Integer = sCurrentURL.IndexOf("&Pkey=")
                            Dim sCurrentPkey As String = sCurrentURL.Substring(pos + 6, sCurrentURL.IndexOf("&", pos + 1) - pos - 6)
                            Dim sCatPkey As String = sCurrentURL.Substring(sCurrentURL.IndexOf("&PkeyCatalog=") + 13, sCurrentURL.Length - sCurrentURL.IndexOf("&PkeyCatalog") - 13)
                            dbPrefbase = pPrefs.GetPrefs("USER", parsedUser, sCatPkey, "10", "", "PART", "IS", sCurrentPkey, "", "PPREFS")

                            If dbPrefbase.Tables(0).Rows.Count > 0 Then
                                Dim outString As String = ""
                                Dim t As Integer
                                Dim isCust As Boolean = False
                                For t = 0 To dbPrefbase.Tables(0).Rows.Count - 1
                                    'boolean if accessor user
                                    If dbPrefbase.Tables(0).Rows(t)("ACCESSOR") = "USER" Then
                                        isCust = True
                                    End If
                                    Dim pref As String = "<" & dbPrefbase.Tables(0).Rows(t)("PREF") & ">"
                                    Dim prefend As String = "</" & dbPrefbase.Tables(0).Rows(t)("PREF") & ">"
                                    Dim prefvalue As String = dbPrefbase.Tables(0).Rows(t)("PREFVALUE")
                                    outString = vbCrLf & outString & pref & prefvalue & prefend & vbCrLf
                                Next
                                If isCust Then
                                    outString = vbCrLf & outString & "<PVIEW>" & "True" & "</PVIEW>" & vbCrLf
                                Else
                                    outString = vbCrLf & outString & "<PVIEW>" & "False" & "</PVIEW>" & vbCrLf
                                End If
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & outString & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            Else
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "none" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            End If

                        Case "WEBEDITOR"

                            If sWorkMode = eWorkMode.OnlineMode Then
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "True" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            Else
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "False" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            End If

                        Case "TodayDate"

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & FormatDateTime(Now(), vbShortDate) & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "ObjectLevel"

                            Dim tmpDbDatasetLevel As New Data.DataSet
                            Dim sSqlQueryLevel As String
                            Dim sLevelName As String

                            sSqlQueryLevel = "SELECT COUNT(PKEY)-2 AS PARTLEVEL FROM T_CAT_NODES CONNECT BY PRIOR T_CAT_NODES.PPARENTKEY=T_CAT_NODES.PKEY START WITH PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'"
                            oConn.Open_Dataset(sSqlQueryLevel, tmpDbDatasetLevel)

                            sLevelName = tmpDbDatasetLevel.Tables(0).Rows(iRows).Item("PARTLEVEL")

                            tmpDbDatasetLevel.Dispose()
                            tmpDbDatasetLevel = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sLevelName & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "LWE-AccessLevel"

                            Dim sAuthenticationString As String
                            'For i = LBound(oAuthentication.Permissions) To UBound(oAuthentication.Permissions)
                            '    sAuthenticationString = sAuthenticationString & "<AccessLevel>" & oAuthentication.Permissions(i) & "</AccessLevel>"
                            'Next

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sAuthenticationString & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "LWE-Permissions"

                            Dim sAuthenticationString As String = ""
                            Dim sAuthenticationValues(6) As String
                            Dim bIsPermitPermission As Boolean
                            Dim iAclPer As Integer

                            sAuthenticationValues(0) = "P_SHOW"
                            sAuthenticationValues(1) = "P_READ"
                            sAuthenticationValues(2) = "P_MODIFY"
                            sAuthenticationValues(3) = "P_NEW"
                            sAuthenticationValues(4) = "P_DELETE"
                            sAuthenticationValues(5) = "P_CHANGESTATUS"
                            sAuthenticationValues(6) = "P_REVISE"

                            For iAclPer = 0 To 6
                                bIsPermitPermission = oAclPermissions.HasPermission(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), sAuthenticationValues(iAclPer))
                                If bIsPermitPermission Then
                                    sAuthenticationString = sAuthenticationString & "<Permission>" & Mid(sAuthenticationValues(iAclPer), 3) & "</Permission>" & vbCrLf
                                End If
                            Next

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sAuthenticationString & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "UserLogon"

                            Dim sAuthenticationString As String
                            'sAuthenticationString = oAuthentication.LogonUser

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sAuthenticationString & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "FullUserLogon"

                            Dim sFullAuthenticationString As String
                            'sFullAuthenticationString = oAuthentication.FullLogonUser

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sFullAuthenticationString & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "PART PROP=ITEM_ID"

                            Dim tmpDbDatasetItemID As New Data.DataSet
                            Dim sItemID As String
                            Dim sTempItemID As String
                            Dim sSqlQueryItemID As String

                            sSqlQueryItemID = "select PMAKAT from " & oLWObjectTypes.emMakakTable & " where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'"
                            oConn.Open_Dataset(sSqlQueryItemID, tmpDbDatasetItemID)


                            If tmpDbDatasetItemID.Tables(0).Rows.Count > 0 Then
                                Dim n As Integer
                                sTempItemID = tmpDbDatasetItemID.Tables(0).Rows(0).Item("PMAKAT") & ""
								if len(sTempItemID)>8 then
									sTempItemID = sTempItemID.Substring(0, 4) & "-" & sTempItemID.Substring(4, (sTempItemID.Length - 4))
								end if
                                sItemID = sTempItemID
                                For n = 1 To tmpDbDatasetItemID.Tables(0).Rows.Count - 1
                                    sItemID += "</CatalogNum><CatalogNum>"
                                    sTempItemID = tmpDbDatasetItemID.Tables(0).Rows(n).Item("PMAKAT") & ""
                                    if len(sTempItemID)>8 then
										sTempItemID = sTempItemID.Substring(0, 4) & "-" & sTempItemID.Substring(4, (sTempItemID.Length - 4))
									end if
                                    sItemID += sTempItemID
                                Next
                            Else
                                sItemID = ""
                            End If

                            tmpDbDatasetItemID.Dispose()
                            tmpDbDatasetItemID = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sItemID & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "PART PROP=VENDOR_NUMBER"

                            Dim tmpDbDatasetVendor As New Data.DataSet
                            Dim sVendorID As String
                            Dim sSqlQueryVendorID As String

                            sSqlQueryVendorID = "select PVENDOR_NUMBER from " & oLWObjectTypes.emVendorTable & " where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'"
                            oConn.Open_Dataset(sSqlQueryVendorID, tmpDbDatasetVendor)

                            If tmpDbDatasetVendor.Tables(0).Rows.Count > 0 Then
                                Dim n As Integer
                                sVendorID =  tmpDbDatasetVendor.Tables(0).Rows(0).Item("PVENDOR_NUMBER") & ""
								If tmpDbDatasetVendor.Tables(0).Rows.Count>1 Then
									sVendorID= tmpDbDatasetVendor.Tables(0).Rows(0).Item("PVENDOR_NUMBER") & "]]>"
								End If
                                For n = 1 To tmpDbDatasetVendor.Tables(0).Rows.Count - 1
                                    ''sVendorID += "]]></ManufacturerNum><ManufacturerNum><![CDATA[" & tmpDbDatasetVendor.Tables(0).Rows(n).Item("PVENDOR_NUMBER") & ""
									sVendorID += "</ManufacturerNum><ManufacturerNum><![CDATA[" & tmpDbDatasetVendor.Tables(0).Rows(n).Item("PVENDOR_NUMBER") & "]]>"
                                Next
								If tmpDbDatasetVendor.Tables(0).Rows.Count>1 Then
									sVendorID= Left(sVendorID,sVendorID.Length-3)
								End If

                            Else
                                sVendorID = ""
                            End If

                            tmpDbDatasetVendor.Dispose()
                            tmpDbDatasetVendor = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sVendorID & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "PART PROP=VENDOR_CODE"

                            Dim tmpDbDatasetVendor As New Data.DataSet
                            Dim sVendorID As String
                            Dim sSqlQueryVendorID As String

                            sSqlQueryVendorID = "select PVENDOR_CODE from " & oLWObjectTypes.emVendorTable & " where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'"
                            oConn.Open_Dataset(sSqlQueryVendorID, tmpDbDatasetVendor)


                            If tmpDbDatasetVendor.Tables(0).Rows.Count > 0 Then
                                Dim n As Integer
                                sVendorID = tmpDbDatasetVendor.Tables(0).Rows(0).Item("PVENDOR_CODE") & ""
								If tmpDbDatasetVendor.Tables(0).Rows.Count>1 Then
									sVendorID= tmpDbDatasetVendor.Tables(0).Rows(0).Item("PVENDOR_CODE") & "]]>"
								End If
								''If tmpDbDatasetVendor.Tables(0).Rows.Count=1
								''	sVendorID=sVendorID & "]]>"
								''End If
                                For n = 1 To tmpDbDatasetVendor.Tables(0).Rows.Count - 1
                                    sVendorID += "</ManufacturerCode><ManufacturerCode><![CDATA[" & tmpDbDatasetVendor.Tables(0).Rows(n).Item("PVENDOR_CODE") & "]]>"
									''sVendorID += "]]></ManufacturerCode><ManufacturerCode><![CDATA[" & tmpDbDatasetVendor.Tables(0).Rows(n).Item("PVENDOR_CODE") & ""

                                Next
							If tmpDbDatasetVendor.Tables(0).Rows.Count>1 Then
									sVendorID= Left(sVendorID,sVendorID.Length-3)
								End If
                            Else
                                sVendorID = ""
                            End If

                            tmpDbDatasetVendor.Dispose()
                            tmpDbDatasetVendor = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sVendorID & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "OwningUserName"

                            Dim tmpDbDatasetUsers As New Data.DataSet
                            Dim sUserName As String
                            Dim sSqlQueryUsers As String

                            sSqlQueryUsers = "select PUSERNAME from " & oLWObjectTypes.emUsersTable & " where pkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYUSER") & "'"
                            oConn.Open_Dataset(sSqlQueryUsers, tmpDbDatasetUsers)

                            If tmpDbDatasetUsers.Tables(0).Rows.Count > 0 Then
                                sUserName = tmpDbDatasetUsers.Tables(0).Rows(0).Item("PUSERNAME") & ""
                            Else
                                sUserName = ""
                            End If

                            tmpDbDatasetUsers.Dispose()
                            tmpDbDatasetUsers = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sUserName & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "LWWORKINGDIRECTORY"

                            Dim sWorkPath As String
                            sWorkPath = System.Web.HttpContext.Current.Application("MappedWorkingDrive") & ""

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sWorkPath & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "CURRENTLOOPCOUNT"

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & iCurrentLoopCount.ToString() & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "GETFILENAME"

                            Dim sFileName As String = getFileName(dbDataSet.Tables(0).Rows(iRows).Item("PORGFILENAME") & "")

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sFileName & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "WebSharingFilePath" 'works only in copy and link to network.
                            Dim sFilePath As String = dbDataSet.Tables(0).Rows(iRows).Item("PORGFILENAME") & ""
                            If sFilePath.IndexOf("\") > 0 Then
                                sFilePath = sFilePath.Substring(sFilePath.IndexOf("\"))
                                sFilePath = "/" & System.Web.HttpContext.Current.Application("WebSharingMappedDriveName") & sFilePath
                            End If

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sFilePath & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "PIMPORTKEYPROP"

                            Dim tmpDbDatasetImportKey As New Data.DataSet
                            Dim sSqlKeyQuery As String = "SELECT * FROM T_CAT_PART_LABLES WHERE PKEYCATALOG='" & SCatalog & "' AND PIMPORTKEY=1"


                            oConn.Open_Dataset(sSqlKeyQuery, tmpDbDatasetImportKey)

                            If tmpDbDatasetImportKey.Tables(0).Rows.Count > 0 Then
                                pImportKeyProp = tmpDbDatasetImportKey.Tables(0).Rows(0).Item("PKEY") & ""
                            Else
                                pImportKeyProp = ""
                            End If

                            tmpDbDatasetImportKey.Dispose()
                            tmpDbDatasetImportKey = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & pImportKeyProp & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))


                        Case "USEROBJECTPKEY"

                            Dim tmpdbDataSetUser As New Data.DataSet
                            Dim sSqlUserQuery As String = "SELECT t_cat_part.pkey,t_cat_nodes.pparentkey,t_cat_part.pkeycatalog FROM T_CAT_PART,t_cat_nodes,t_cat_objecttypes WHERE t_cat_part.pkeytype=t_cat_objecttypes.pkey and t_cat_objecttypes.ptypename='User' and t_cat_part.pkey=t_cat_nodes.pkey and pglobalstatus<>4 and t_cat_part.phebdesc='" & oAuthentication.FullLogonUser & "'"


                            oConn.Open_Dataset(sSqlUserQuery, tmpdbDataSetUser)
                            Dim sUserPkey As String = ""
                            Dim sUserParentPkey As String = ""
                            Dim sUserCatalogPkey As String = ""
                            If tmpdbDataSetUser.Tables(0).Rows.Count > 0 Then
                                sUserPkey = tmpdbDataSetUser.Tables(0).Rows(0).Item("PKEY") & ""
                                sUserParentPkey = tmpdbDataSetUser.Tables(0).Rows(0).Item("PPARENTKEY") & ""
                                sUserCatalogPkey = tmpdbDataSetUser.Tables(0).Rows(0).Item("PKEYCATALOG") & ""
                            End If

                            tmpdbDataSetUser.Dispose()
                            tmpdbDataSetUser = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sUserPkey & "|" & sUserParentPkey & "|" & sUserCatalogPkey & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "PIMPORTKEYVALUE"

                            If pImportKeyProp <> "" Then

                                Dim tmpDbDatasetImportKey As New Data.DataSet
                                Dim sSqlKeyQuery As String = "SELECT " & pImportKeyProp & " FROM T_CAT_PART WHERE PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'"
                                Dim pImportKeyValue = ""

                                oConn.Open_Dataset(sSqlKeyQuery, tmpDbDatasetImportKey)

                                If tmpDbDatasetImportKey.Tables(0).Rows.Count > 0 Then
                                    pImportKeyValue = tmpDbDatasetImportKey.Tables(0).Rows(0).Item(pImportKeyProp) & ""
                                Else
                                    pImportKeyValue = ""
                                End If

                                tmpDbDatasetImportKey.Dispose()
                                tmpDbDatasetImportKey = Nothing

                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & pImportKeyValue & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            Else
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            End If


                        Case "LATESTDOCREVISION"

                            Dim tmpDbDataset As New Data.DataSet
                            Dim sSqlKeyQuery As String = "select pkey,prevision from t_cat_winobject where pdocid in (select pdocid from t_cat_winobject where pkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') order by PREVISION desc"
                            Dim pValue = ""

                            oConn.Open_Dataset(sSqlKeyQuery, tmpDbDataset)

                            If tmpDbDataset.Tables(0).Rows.Count > 0 Then
                                pValue = tmpDbDataset.Tables(0).Rows(0).Item("PREVISION") & ""
                                pLatestDocRevisionPkey = tmpDbDataset.Tables(0).Rows(0).Item("PKEY") & ""
                            Else
                                pValue = ""
                            End If

                            tmpDbDataset.Dispose()
                            tmpDbDataset = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & pValue & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "LATESTDOCREVISIONPKEY"

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & pLatestDocRevisionPkey & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "LATESTITEMREVISION"

                            If pImportKeyProp <> "" Then

                                Dim tmpDbDataset As New Data.DataSet
                                Dim sSqlKeyQuery As String = "select pkey,prevision from t_cat_part where " & pImportKeyProp & " in (select " & pImportKeyProp & " from t_cat_part where pkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') order by PREVISION desc"
                                Dim pValue As String
                                oConn.Open_Dataset(sSqlKeyQuery, tmpDbDataset)

                                If tmpDbDataset.Tables(0).Rows.Count > 0 Then
                                    pValue = tmpDbDataset.Tables(0).Rows(0).Item("PREVISION") & ""
                                    pLatestItemRevisionPkey = tmpDbDataset.Tables(0).Rows(0).Item("PKEY") & ""
                                Else
                                    pValue = ""
                                End If

                                tmpDbDataset.Dispose()
                                tmpDbDataset = Nothing

                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & pValue & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            Else
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            End If

                        Case "LATESTITEMREVISIONPKEY"

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & pLatestItemRevisionPkey & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "UserLogonDescription"

                            Dim tmpDbDatasetUserLogonDescription As New Data.DataSet
                            Dim sUserLogonDescription As String
                            Dim sSqlQueryUserLogonDescription As String

                            sSqlQueryUserLogonDescription = "select PUSERDESCRIPTION from " & oLWObjectTypes.emUsersTable & " where pkey='" & oAuthentication.LogonUser & "'"
                            oConn.Open_Dataset(sSqlQueryUserLogonDescription, tmpDbDatasetUserLogonDescription)

                            If tmpDbDatasetUserLogonDescription.Tables(0).Rows.Count > 0 Then
                                sUserLogonDescription = tmpDbDatasetUserLogonDescription.Tables(0).Rows(0).Item("PUSERDESCRIPTION") & ""
                            Else
                                sUserLogonDescription = ""
                            End If

                            tmpDbDatasetUserLogonDescription.Dispose()
                            tmpDbDatasetUserLogonDescription = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sUserLogonDescription & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "OwningUserNameDescription"

                            Dim tmpDbDatasetOwningUserNameDescription As New Data.DataSet
                            Dim sOwningUserNameDescription As String
                            Dim sSqlQueryOwningUserNameDescription As String

                            sSqlQueryOwningUserNameDescription = "select PUSERDESCRIPTION from " & oLWObjectTypes.emUsersTable & " where pkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYUSER") & "'"
                            oConn.Open_Dataset(sSqlQueryOwningUserNameDescription, tmpDbDatasetOwningUserNameDescription)

                            If tmpDbDatasetOwningUserNameDescription.Tables(0).Rows.Count > 0 Then
                                sOwningUserNameDescription = tmpDbDatasetOwningUserNameDescription.Tables(0).Rows(0).Item("PUSERDESCRIPTION") & ""
                            Else
                                sOwningUserNameDescription = ""
                            End If

                            tmpDbDatasetOwningUserNameDescription.Dispose()
                            tmpDbDatasetOwningUserNameDescription = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOwningUserNameDescription & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "ACLPermissionsName"

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & oAclPermissions.HasPermission(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), "P_Name") & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_1 "

                            Dim tmpDbDatasetText1 As New Data.DataSet
                            Dim sText1Content As String = ""
                            Dim sSqlQueryText1 As String
                            Dim odRowText1 As Data.DataRow

                            sSqlQueryText1 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=1 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            sText1Content=sSqlQueryText1
							oConn.Open_Dataset(sSqlQueryText1, tmpDbDatasetText1)
							
                            If tmpDbDatasetText1.Tables.Count>0 AndAlso tmpDbDatasetText1.Tables(0).Rows.Count > 0 Then
                                For Each odRowText1 In tmpDbDatasetText1.Tables(0).Rows
                                    sText1Content = sText1Content & "<div id=""TextObject" + odRowText1.Item("PENGDESC") + """>"
                                    sText1Content = sText1Content & HttpContext.Current.Server.HtmlDecode(odRowText1.Item("pcontent")) & ""
                                    sText1Content = sText1Content + "</div>"
									sText1Content=""
                                Next
                            Else
                                sText1Content = ""
                            End If

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText1Content, ".html") Then
                                    iEnd = InStr(1, sText1Content, ".html")
                                    iStart = InStrRev(sText1Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText1Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        'sText1Content = Mid(sText1Content, 1, iStart) & "../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText1Content, iStart + 1, iEnd - iStart - 1) & Mid(sText1Content, iEnd + 5)
                                        sText1Content = Mid(sText1Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText1Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText1Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText1Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            tmpDbDatasetText1.Dispose()
                            tmpDbDatasetText1 = Nothing
							'sText1Content=""
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText1Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_2 "

                            Dim tmpDbDatasetText2 As New Data.DataSet
                            Dim sText2Content As String = ""
                            Dim sSqlQueryText2 As String
                            Dim odRowText2 As Data.DataRow

                            sSqlQueryText2 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=2 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText2, tmpDbDatasetText2)

                            If tmpDbDatasetText2.Tables.Count>0 andalso tmpDbDatasetText2.Tables(0).Rows.Count > 0 Then
                                For Each odRowText2 In tmpDbDatasetText2.Tables(0).Rows
                                    sText2Content = sText2Content & "<div id=""TextObject" + odRowText2.Item("PENGDESC") + """>"
                                    sText2Content = sText2Content & HttpContext.Current.Server.HtmlDecode(odRowText2.Item("pcontent")) & ""
                                    sText2Content = sText2Content + "</div>"
                                Next
                            Else
                                sText2Content = ""
                            End If

                            tmpDbDatasetText2.Dispose()
                            tmpDbDatasetText2 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText2Content, ".html") Then
                                    iEnd = InStr(1, sText2Content, ".html")
                                    iStart = InStrRev(sText2Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText2Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        sText2Content = Mid(sText2Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText2Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText2Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText2Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While
sText2Content=sText2Content.Replace("src=""","src=""http://www.catalog.idf/Images/TextImages/")
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText2Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_3 "

                            Dim tmpDbDatasetText3 As New Data.DataSet
                            Dim sText3Content As String = ""
                            Dim sSqlQueryText3 As String
                            Dim odRowText3 As Data.DataRow

                            sSqlQueryText3 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=3 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText3, tmpDbDatasetText3)

                            If tmpDbDatasetText3.Tables.Count > 0 AndAlso tmpDbDatasetText3.Tables(0).Rows.Count > 0 Then
                                For Each odRowText3 In tmpDbDatasetText3.Tables(0).Rows
                                    sText3Content = sText3Content & "<div id=""TextObject" + odRowText3.Item("PENGDESC") + """>"
                                    sText3Content = sText3Content & HttpContext.Current.Server.HtmlDecode(odRowText3.Item("pcontent")) & ""
                                    sText3Content = sText3Content + "</div>"
                                Next
                            Else
                                sText3Content = ""
                            End If

                            tmpDbDatasetText3.Dispose()
                            tmpDbDatasetText3 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText3Content, ".html") Then
                                    iEnd = InStr(1, sText3Content, ".html")
                                    iStart = InStrRev(sText3Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText3Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        sText3Content = Mid(sText3Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText3Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText3Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText3Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText3Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_4 "

                            Dim tmpDbDatasetText4 As New Data.DataSet
                            Dim sText4Content As String = ""
                            Dim sSqlQueryText4 As String
                            Dim odRowText4 As Data.DataRow

                            sSqlQueryText4 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=4 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText4, tmpDbDatasetText4)

                            If  tmpDbDatasetText4.Tables.Count > 0 AndAlso tmpDbDatasetText4.Tables(0).Rows.Count > 0 Then
                                For Each odRowText4 In tmpDbDatasetText4.Tables(0).Rows
                                    sText4Content = sText4Content & "<div id=""TextObject" + odRowText4.Item("PENGDESC") + """>"
                                    sText4Content = sText4Content & HttpContext.Current.Server.HtmlDecode(odRowText4.Item("pcontent")) & ""
                                    sText4Content = sText4Content + "</div>"
                                Next
                            Else
                                sText4Content = ""
                            End If

                            tmpDbDatasetText4.Dispose()
                            tmpDbDatasetText4 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText4Content, ".html") Then
                                    iEnd = InStr(1, sText4Content, ".html")
                                    iStart = InStrRev(sText4Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText4Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        sText4Content = Mid(sText4Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText4Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText4Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText4Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText4Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_5 "

                            Dim tmpDbDatasetText5 As New Data.DataSet
                            Dim sText5Content As String = ""
                            Dim sSqlQueryText5 As String
                            Dim odRowText5 As Data.DataRow

                            sSqlQueryText5 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=5 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText5, tmpDbDatasetText5)

                            If  tmpDbDatasetText5.Tables.Count > 0 AndAlso tmpDbDatasetText5.Tables(0).Rows.Count > 0 Then
                                For Each odRowText5 In tmpDbDatasetText5.Tables(0).Rows
                                    sText5Content = sText5Content & "<div id=""TextObject" + odRowText5.Item("PENGDESC") + """>"
                                    sText5Content = sText5Content & HttpContext.Current.Server.HtmlDecode(odRowText5.Item("pcontent")) & ""
                                    sText5Content = sText5Content + "</div>"
                                Next
                            Else
                                sText5Content = ""
                            End If

                            tmpDbDatasetText5.Dispose()
                            tmpDbDatasetText5 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText5Content, ".html") Then
                                    iEnd = InStr(1, sText5Content, ".html")
                                    iStart = InStrRev(sText5Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText5Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        sText5Content = Mid(sText5Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText5Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText5Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText5Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText5Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_6 "

                            Dim tmpDbDatasetText6 As New Data.DataSet
                            Dim sText6Content As String = ""
                            Dim sSqlQueryText6 As String
                            Dim odRowText6 As Data.DataRow

                            sSqlQueryText6 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=6 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText6, tmpDbDatasetText6)

                            If  tmpDbDatasetText6.Tables.Count > 0 AndAlso tmpDbDatasetText6.Tables(0).Rows.Count > 0 Then
                                For Each odRowText6 In tmpDbDatasetText6.Tables(0).Rows
                                    sText6Content = sText6Content & "<div id=""TextObject" + odRowText6.Item("PENGDESC") + """>"
                                    sText6Content = sText6Content & HttpContext.Current.Server.HtmlDecode(odRowText6.Item("pcontent")) & ""
                                    sText6Content = sText6Content + "</div>"
                                Next
                            Else
                                sText6Content = ""
                            End If

                            tmpDbDatasetText6.Dispose()
                            tmpDbDatasetText6 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText6Content, ".html") Then
                                    iEnd = InStr(1, sText6Content, ".html")
                                    iStart = InStrRev(sText6Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText6Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        sText6Content = Mid(sText6Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText6Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText6Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText6Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText6Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_7 "

                            Dim tmpDbDatasetText7 As New Data.DataSet
                            Dim sText7Content As String = ""
                            Dim sSqlQueryText7 As String
                            Dim odRowText7 As Data.DataRow

                            sSqlQueryText7 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=7 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText7, tmpDbDatasetText7)

                            If  tmpDbDatasetText7.Tables.Count > 0 AndAlso tmpDbDatasetText7.Tables(0).Rows.Count > 0 Then
                                For Each odRowText7 In tmpDbDatasetText7.Tables(0).Rows
                                    sText7Content = sText7Content & "<div id=""TextObject" + odRowText7.Item("PENGDESC") + """>"
                                    sText7Content = sText7Content & HttpContext.Current.Server.HtmlDecode(odRowText7.Item("pcontent")) & ""
                                    sText7Content = sText7Content + "</div>"
                                Next
                            Else
                                sText7Content = ""
                            End If

                            tmpDbDatasetText7.Dispose()
                            tmpDbDatasetText7 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText7Content, ".html") Then
                                    iEnd = InStr(1, sText7Content, ".html")
                                    iStart = InStrRev(sText7Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText7Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        sText7Content = Mid(sText7Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText7Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText7Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText7Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText7Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_8 "

                            Dim tmpDbDatasetText8 As New Data.DataSet
                            Dim sText8Content As String = ""
                            Dim sSqlQueryText8 As String
                            Dim odRowText8 As Data.DataRow

                            sSqlQueryText8 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=8 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText8, tmpDbDatasetText8)

                            If  tmpDbDatasetText8.Tables.Count > 0 AndAlso tmpDbDatasetText8.Tables(0).Rows.Count > 0 Then
                                For Each odRowText8 In tmpDbDatasetText8.Tables(0).Rows
                                    sText8Content = sText8Content & "<div id=""TextObject" + odRowText8.Item("PENGDESC") + """>"
                                    sText8Content = sText8Content & HttpContext.Current.Server.HtmlDecode(odRowText8.Item("pcontent")) & ""
                                    sText8Content = sText8Content + "</div>"
                                Next

                            Else
                                sText8Content = ""
                            End If

                            tmpDbDatasetText8.Dispose()
                            tmpDbDatasetText8 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText8Content, ".html") Then
                                    iEnd = InStr(1, sText8Content, ".html")
                                    iStart = InStrRev(sText8Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText8Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        'sText8Content = Mid(sText8Content, 1, iStart) & "../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText8Content, iStart + 1, iEnd - iStart - 1) & Mid(sText8Content, iEnd + 5)
                                        sText8Content = Mid(sText8Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText8Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText8Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText8Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText8Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " TEXT_9 "

                            Dim tmpDbDatasetText9 As New Data.DataSet
                            Dim sText9Content As String = ""
                            Dim sSqlQueryText9 As String
                            Dim odRowText9 As Data.DataRow

                            sSqlQueryText9 = "select pcontent,PENGDESC from t_cat_text,t_cat_nodes where pparentkey='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' and pobjecttype=6 and t_cat_nodes.pkey=t_cat_text.pkey and t_cat_text.pposition=9 and pglobalstatus<>" & oLWObjectTypes.emDeleted
                            oConn.Open_Dataset(sSqlQueryText9, tmpDbDatasetText9)

                            If  tmpDbDatasetText9.Tables.Count > 0 AndAlso tmpDbDatasetText9.Tables(0).Rows.Count > 0 Then
                                For Each odRowText9 In tmpDbDatasetText9.Tables(0).Rows
                                    sText9Content = sText9Content & "<div id=""TextObject" + odRowText9.Item("PENGDESC") + """>"
                                    sText9Content = sText9Content & HttpContext.Current.Server.HtmlDecode(odRowText9.Item("pcontent")) & ""
                                    sText9Content = sText9Content + "</div>"
                                Next
                            Else
                                sText9Content = ""
                            End If

                            tmpDbDatasetText9.Dispose()
                            tmpDbDatasetText9 = Nothing

                            'replace the html links to online links.
                            Dim iStart As Integer = 1
                            Dim iEnd As Integer
                            While True
                                If InStr(iStart, sText9Content, ".html") Then
                                    iEnd = InStr(1, sText9Content, ".html")
                                    iStart = InStrRev(sText9Content, """", iEnd)
                                    'check if its to external site of internal linkware page
                                    If Not (InStr(Mid(sText9Content, iStart + 1, iEnd - iStart - 1), "http")) Then
                                        sText9Content = Mid(sText9Content, 1, iStart) & "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & Mid(sText9Content, iStart + 1, iEnd - iStart - 1) & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & Mid(sText9Content, iStart + 1, iEnd - iStart - 1) & "&PkeyCatalog=" & SCatalog & "';" & Mid(sText9Content, iEnd + 5)
                                    End If
                                Else
                                    Exit While
                                End If
                            End While

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sText9Content & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " IMAGE "
                            sLoopTemplate = "<#IMAGEMAPLINKONLY#>"

                            'Open command - get pictureproperty
                            dbDataSetChilds = New Data.DataSet
                            sSqlQueryChilds = "SELECT PKEY FROM " & oLWObjectTypes.emNodesTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPicture 
                            oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                            If dbDataSetChilds.Tables(0).Rows.Count = 0 Then
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            End If

                            For Each oDRow In dbDataSetChilds.Tables(0).Rows
                                'Add pictureproperty from the template
                                oPictureProperty = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPicture, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                oPictureProperty.sWorkMode = sWorkMode
                                oPictureProperty.SCatalog = SCatalog
                                oPictureProperty.SParent = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
                                sOutput = oPictureProperty.GetOutput("SELECT " & oLWObjectTypes.emPicturePropertyTable & ".* FROM " & oLWObjectTypes.emPicturePropertyTable & "," & oLWObjectTypes.emNodesTable & " WHERE " & oLWObjectTypes.emPicturePropertyTable & ".PKEY='" & oDRow.Item("PKEY") & "' AND " & oLWObjectTypes.emPicturePropertyTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'")
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                oPictureProperty.Dispose()
                                oPictureProperty = Nothing
                                firstPos = firstPos + Len(sOutput)
                                'end adding
                            Next

                            dbDataSetChilds = Nothing

                            'Case "PartLabels"
                            '    Dim sSqlPartLabels As String
                            '    'Open command - get partlabels
                            '    dbDataSetPartLabels = New DataSet
                            '    sSqlPartLabels = "SELECT PKEY,PHEBDESC,PENGDESC FROM " & oLWObjectTypes.emPartLabelsTable & " WHERE PKEYCATALOG='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "'"
                            '    oConn.Open_Dataset(sSqlPartLabels, dbDataSetPartLabels, "PartLabels")
                            '    Dim xmlDoc As New System.Xml.XmlDataDocument(dbDataSetPartLabels)

                            '    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & xmlDoc.OuterXml() & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            '    xmlDoc = Nothing
                            '    dbDataSetPartLabels.Dispose()
                            '    dbDataSetPartLabels = Nothing


                        Case "IndexDMC"

                            If sWorkMode = eWorkMode.OnlineMode Then
                                Dim dbDatasetRelation As New Data.DataSet

                                'Dim sSqlQueryRelations As String
                                'oConn.Open_Dataset(sSqlQueryRelations, dbDatasetRelation)

                                If (SCatalog = "") Then
                                    sSimpleTagText = WriteIndexFrameDmc(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), dbDatasetRelation, 10, 0, oConn)
                                Else
                                    sSimpleTagText = WriteIndexFrameDmc(SCatalog, dbDatasetRelation, 10, 0, oConn)
                                End If
                                'dbDatasetRelation = Nothing
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            Else
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "<#Index#>" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                            End If

                        Case "REFERENCEURL"
                            Dim dbDatasetReference As New Data.DataSet

                            Dim sSqlQueryReference As String
                            sSqlQueryReference = "SELECT PREFERENCEURL FROM " & oLWObjectTypes.emCatalogTable & " WHERE PKEY='" & SCatalog & "'"
                            oConn.Open_Dataset(sSqlQueryReference, dbDatasetReference)

                            If dbDatasetReference.Tables(0).Rows.Count > 0 Then
                                sSimpleTagText = dbDatasetReference.Tables(0).Rows(0).Item(0) & ""
                            Else
                                sSimpleTagText = ""
                            End If

                            dbDatasetReference = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))


                        Case "CATALOG_DIRECTORY"

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "<a target=""_blank"" href=""Catalogs/" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "/" & dbDataSet.Tables(0).Rows(iRows).Item("PINDEXTEMPLATE") & """>" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " CD_DATE "

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Now & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "PARENTKEY"

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & SParent & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case " CD_NAME "

                            'Open command - get childs
                            Dim dbDataSetCDName As New Data.DataSet
                            Dim sCDName As String = ""

                            sSqlQueryChilds = "SELECT PCDNAME FROM " & oLWObjectTypes.emCD_DATA & " WHERE PKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' "
                            oConn.Open_Dataset(sSqlQueryChilds, dbDataSetCDName)

                            If dbDataSetCDName.Tables(0).Rows.Count > 0 Then
                                sCDName = dbDataSetCDName.Tables(0).Rows(0).Item(0) & ""
                            End If

                            dbDataSetCDName.Dispose()
                            dbDataSetCDName = Nothing

                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sCDName & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "CURRENT_LOOP_BEGIN"

                            'search for end tag CURRENT_LOOP_END and send.
                            iStartLoopPos = lastPos + Len(EndDelimeter)
                            iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "CURRENT_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)
                            sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndLoopPos + Len(StartDelimeter) + Len(EndDelimeter) + Len("CURRENT_LOOP_END"))

                            'check if we need to put the childs in sort order.

                            Dim sSqlTemp As String = "select pkeychilds from t_cat_part where pkey in (select pparentkey from (select  * from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.PKEY=t_Cat_nodes.PPARENTKEY start with t_cat_nodes.pkey='" & SCatalog & "') t1,t_cat_part where t1.pkey=t_cat_part.pkey and pkeytype=64 and rownum<2)"

                            Dim dbTempData As New Data.DataSet
                            oConn.Open_Dataset(sSqlTemp, dbTempData)

                            Dim sPkeyChilds As String
                            Dim aPkeyChilds() As String

                            If Not IsDBNull(dbTempData.Tables(0).Rows(iRows).Item("PKEYCHILDS")) Then
                                If Len(dbTempData.Tables(0).Rows(iRows).Item("PKEYCHILDS")) > 0 Then
                                    sPkeyChilds = dbTempData.Tables(0).Rows(iRows).Item("PKEYCHILDS")
                                    aPkeyChilds = Split(sPkeyChilds, "|")
                                End If
                            End If


                            Dim sSubQueryStatement As String
                            Dim iSubQueryStatement As Integer
                            If Not aPkeyChilds Is Nothing Then
                                sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,0 AS PORDER from dual union "
                                For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                    sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "'," & iSubQueryStatement & " from dual union "
                                Next
                                sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                            Else
                                sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                            End If

                            Dim iRowsCurrent As Integer
                            For iRowsCurrent = 0 To dbDataSet.Tables(0).Rows.Count - 1
                                oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                oParts.sWorkMode = sWorkMode
                                oParts.SCatalog = SCatalog
                                oParts.SParent = dbDataSet.Tables(0).Rows(iRowsCurrent).Item("PPARENTKEY")
                                'sOutput = oParts.GetOutput("SELECT " & oLWObjectTypes.emPartTable & ".*, T_CAT_NODES.PNPROP1 AS OCPROP1, T_CAT_NODES.PNPROP2 AS OCPROP2, T_CAT_NODES.PNPROP3 AS OCPROP3, T_CAT_NODES.PNPROP4 AS OCPROP4, T_CAT_NODES.PNPROP5 AS OCPROP5, T_CAT_NODES.PNPROP6 AS OCPROP6, T_CAT_NODES.PNPROP7 AS OCPROP7, T_CAT_NODES.PNPROP8 AS OCPROP8, T_CAT_NODES.PNPROP9 AS OCPROP9, T_CAT_NODES.PNPROP10 AS OCPROP10, T_CAT_NODES.PNPROP11 AS OCPROP11, T_CAT_NODES.PNPROP12 AS OCPROP12, T_CAT_NODES.PNPROP13 AS OCPROP13, T_CAT_NODES.PNPROP14 AS OCPROP14, T_CAT_NODES.PNPROP15 AS OCPROP15, T_CAT_NODES.PNPROP16 AS OCPROP16, T_CAT_NODES.PNPROP17 AS OCPROP17, T_CAT_NODES.PNPROP18 AS OCPROP18, T_CAT_NODES.PNPROP19 AS OCPROP19, T_CAT_NODES.PNPROP20 AS OCPROP20," & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & ", " & oLWObjectTypes.emNodesTable & " WHERE " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "' AND t_cat_part.pkey = '" & dbDataSet.Tables(0).Rows(iRowsCurrent).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY AND (" & oLWObjectTypes.emPartTable & ".PKEYTYPE<>'43' OR " & oLWObjectTypes.emPartTable & ".PKEYTYPE IS NULL)")
                                sOutput = oParts.GetOutput("select * from (select  * from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.PKEY=t_Cat_nodes.PPARENTKEY start with t_cat_nodes.pkey='" & SCatalog & "') t1,t_cat_part, " & sSubQueryStatement & " tmpChilds where t1.pkey=t_cat_part.pkey and pkeytype=64 and t_cat_part.PKEY=tmpChilds.PKEY(+) ORDER BY tmpChilds.PORDER DESC")
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                oParts.Dispose()
                                oParts = Nothing
                                firstPos = firstPos + Len(sOutput)

                            Next

                            'end adding

                        Case "USEREXPDATE"
                            Dim sSqlExpiration As String
                            Dim oDbDatasetExpiration As New Data.DataSet
                            Dim oConnExpiration As New CConnection
                            Dim dExpDate As Date

                            sSqlExpiration = "select PUSEREXPDATE from t_cat_users where pkey='" & sAuthentication & "'"

                            oConnExpiration.Open_Oracle_Connection(sODBCName)
                            oConnExpiration.Open_Dataset(sSqlExpiration, oDbDatasetExpiration)
                            oConnExpiration.Close_Connection()
                            If (oDbDatasetExpiration.Tables(0).Rows.Count > 0) Then
                                If Not IsDBNull(oDbDatasetExpiration.Tables(0).Rows(0)(0)) Then
                                    'maybe date format not right...
                                    Try
                                        dExpDate = CDate(oDbDatasetExpiration.Tables(0).Rows(0)(0))
                                    Catch ex As Exception
                                        dExpDate = Now.AddDays(-1)
                                    End Try
                                Else
                                    dExpDate = Now.AddDays(-1)
                                End If
                            Else
                                dExpDate = Now.AddDays(-1)
                            End If
                            oDbDatasetExpiration.Dispose()
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & dExpDate.ToShortDateString & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case "SIMPLEMODIFIEDDATE"
                            sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Left(dbDataSet.Tables(0).Rows(iRows).Item("PUPDATEDATE"), 10) & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                        Case Else

                            'check if we have a frame tag or simple tag
                            If InStr(tagName, "FRAME", CompareMethod.Text) Then
                                iStartLoopPos = InStr(tagName, "SOURCE_FILE=") + Len("SOURCE_FILE=")
                                iEndLoopPos = InStr(iStartLoopPos, tagName, " ", CompareMethod.Text)
                                If iEndLoopPos = 0 Then
                                    sFrameName = Mid(tagName, iStartLoopPos)
                                Else
                                    sFrameName = Mid(tagName, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                                End If

                                If sWorkMode = eWorkMode.OnlineMode Then
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & """ShowTemplate.aspx?Template=" & sFrameName & "&amp;Pkey=" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "&amp;ParentKey=" & SParent & "&amp;Type=" & sObjType & "&amp;PkeyCatalog=" & SCatalog & """" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                Else
                                    If InStr(sFrameName, ".xml") > 0 Then
                                        sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & """" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "_" & iFrameNumber & ".xml""" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                    Else
                                        sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & """" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "_" & iFrameNumber & ".html""" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                    End If
                                    iFrameNumber = iFrameNumber + 1
                                End If
                            ElseIf InStr(tagName, "Index PART PROP=", CompareMethod.Text) Then
                                'check if we have a index with propnum.
                                sPropName = Right(tagName, Len(tagName) - InStrRev(tagName, "="))

                                Dim dbDatasetRelation As New Data.DataSet
                                Dim sSqlQueryRelations As String

                                If gemAppMode = "Access" Then
                                    sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE ,'' as " & sPropName & ", T_CAT_PAGE.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                                    & "WHERE (((T_CAT_PAGE.PKEYCATALOG)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "'  OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE,T_CAT_PART." & sPropName & ", T_CAT_PART.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM (T_CAT_PART INNER JOIN T_CAT_NODES ON T_CAT_PART.PKEY = T_CAT_NODES.PKEY ) LEFT JOIN T_CAT_OBJECTTYPES ON T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY " _
                                                    & "WHERE (((T_CAT_PART.PKEYCATALOG)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "')) AND ( T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sPropName & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                                    & "WHERE (((T_CAT_FOLDER.PKEYCATALOG)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') AND T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sPropName & ", T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                                    & "WHERE (((T_CAT_CATALOG.PKEY)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                                    & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
                                Else
                                    sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE ,'' as " & sPropName & ", T_CAT_PAGE.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                                    & "WHERE (((T_CAT_PAGE.PKEY IN (SELECT distinct PKEY FROM T_CAT_NODES where  pglobalstatus<>4 and pobjecttype=2 CONNECT BY PRIOR T_CAT_NODES.PKEY=T_CAT_NODES.PPARENTKEY START WITH PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'))) AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE,T_CAT_PART." & sPropName & ", T_CAT_PART.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                                    & "WHERE (((T_CAT_PART.PKEY IN (SELECT distinct PKEY FROM T_CAT_NODES where  pglobalstatus<>4 and pobjecttype=10 CONNECT BY PRIOR T_CAT_NODES.PKEY=T_CAT_NODES.PPARENTKEY START WITH PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'))) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+)) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sPropName & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                                    & "WHERE (((T_CAT_FOLDER.PKEY IN (SELECT distinct PKEY FROM T_CAT_NODES where  pglobalstatus<>4 and pobjecttype=11 CONNECT BY PRIOR T_CAT_NODES.PKEY=T_CAT_NODES.PPARENTKEY START WITH PKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'))) AND T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sPropName & ", T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                                    & "WHERE (((T_CAT_CATALOG.PKEY)='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "') and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                                    & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
                                End If

                                oConn.Open_Dataset(sSqlQueryRelations, dbDatasetRelation)

                                sSimpleTagText = WriteIndexFrame(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), dbDatasetRelation, 1, 0, sPropName)

                                dbDatasetRelation = Nothing

                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                                'ElseIf InStr(tagName, "INDEX", CompareMethod.Text) Then
                                '    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                            ElseIf InStr(tagName, "IndexDMC PART PROP=", CompareMethod.Text) Then
                                If sWorkMode = eWorkMode.OnlineMode Then
                                    'check if we have a index with propnum.
                                    sPropName = Right(tagName, Len(tagName) - InStrRev(tagName, "="))
                                    SessionProp = sPropName

                                    Dim dbDatasetRelation As New Data.DataSet
                                    Dim sSqlQueryRelations As String

                                    If (SCatalog = "") Then
                                        sSimpleTagText = WriteIndexFrameDmc(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), dbDatasetRelation, 10, 0, oConn, sPropName)
                                    Else
                                        sSimpleTagText = WriteIndexFrameDmc(SCatalog, dbDatasetRelation, 10, 0, oConn, sPropName)
                                    End If

                                    dbDatasetRelation = Nothing

                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                Else
                                    sPropName = Right(tagName, Len(tagName) - InStrRev(tagName, "="))
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "<#Index PART PROP=" & sPropName & "#>" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                End If


                            ElseIf InStr(tagName, "PICTURE_NAME", CompareMethod.Text) Then

                                'Open command - get pictureproperty
                                dbDataSetChilds = New Data.DataSet
                                sSqlQueryChilds = "SELECT PKEY FROM " & oLWObjectTypes.emNodesTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPicture 
                                oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                                If dbDataSetChilds.Tables(0).Rows.Count = 0 Then
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                Else
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "pic_" & dbDataSetChilds.Tables(0).Rows(0).Item("PKEY") & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                End If

                                dbDataSetChilds.Dispose()
                                dbDataSetChilds = Nothing

                            ElseIf InStr(tagName, "IMAGEMAP_NAME", CompareMethod.Text) Then
                                'Open command - get pictureproperty
                                dbDataSetChilds = New Data.DataSet
                                sSqlQueryChilds = "SELECT PKEY FROM " & oLWObjectTypes.emNodesTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPicture 
                                oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                                If dbDataSetChilds.Tables(0).Rows.Count = 0 Then
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                Else
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "map_" & dbDataSetChilds.Tables(0).Rows(0).Item("PKEY") & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                End If

                                dbDataSetChilds.Dispose()
                                dbDataSetChilds = Nothing

                            ElseIf InStr(tagName, "IMAGEMAP", CompareMethod.Text) Then

                                Dim aParameters() As String
                                Dim iParameter As Integer
                                Dim sPopupLabel1 As String
                                Dim sPopupLabel2 As String
                                Dim sPopupLabel3 As String
                                Dim sPopupProperty1 As String
                                Dim sPopupProperty2 As String
                                Dim sPopupProperty3 As String

                                aParameters = Split(tagName, " ")
                                'default propery 1 is hebrew name
                                sPopupProperty1 = "PHEBDESC"
                                sPopupProperty2 = "PMAKAT"
                                For iParameter = 3 To aParameters.Length - 1
                                    If InStr(aParameters(iParameter), "=") > 0 Then
                                        If Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1) = "LABLE1" Then
                                            sPopupLabel1 = Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                            sPopupLabel1 = Mid(sPopupLabel1, 2, sPopupLabel1.Length - 2)
                                        End If
                                        If Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1) = "LABLE2" Then
                                            sPopupLabel2 = Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                            sPopupLabel2 = Mid(sPopupLabel2, 2, sPopupLabel2.Length - 2)
                                        End If
                                        If Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1) = "LABLE3" Then
                                            sPopupLabel3 = Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                            sPopupLabel3 = Mid(sPopupLabel3, 2, sPopupLabel3.Length - 2)
                                        End If
                                        If Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1) = "PROP2" Then
                                            sPopupProperty2 = "PPROP" & Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                        End If
                                        If Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1) = "PROP3" Then
                                            sPopupProperty3 = "PPROP" & Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                        End If
                                    End If
                                Next

                                'declare source image path.
                                Dim srcLink As String

                                If sWorkMode = eWorkMode.OfflineMode Then
                                    srcLink = "Images/" & dbDataSet.Tables(0).Rows(iRows).Item("PPICTUREKEY") & "." & ConvertFromIntToExtension(dbDataSet.Tables(0).Rows(iRows).Item("PFILEEXTENSION"))
                                Else
                                    If sImageMapManage = "PerCatalog" Then
                                        srcLink = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & dbDataSet.Tables(0).Rows(iRows).Item("PKEYCATALOG") & "/" & dbDataSet.Tables(0).Rows(iRows).Item("PPICTUREKEY") & "." & ConvertFromIntToExtension(dbDataSet.Tables(0).Rows(iRows).Item("PFILEEXTENSION"))
                                    Else
                                        srcLink = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & dbDataSet.Tables(0).Rows(iRows).Item("PPICTUREKEY") & "." & ConvertFromIntToExtension(dbDataSet.Tables(0).Rows(iRows).Item("PFILEEXTENSION"))
                                    End If
                                End If

                                sSimpleTagText = "<div id=""ImageMapDIV"" style=""Z-INDEX:0""><img src=""" & srcLink & """ id=""myimg"" USEMAP=""#map_" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & """ name=""pic_" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & """/></div>"

                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & GenerateMap_HotSpots(dbDataSet.Tables(0).Rows(iRows).Item("PKEY"), sPopupLabel1, sPopupLabel2, sPopupLabel3, sPopupProperty1, sPopupProperty2, sPopupProperty3) & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter)) & sSimpleTagText

                            ElseIf InStr(tagName, "MAP ORDINAL", CompareMethod.Text) Then

                                sLoopTemplate = "<#IMAGEMAP " & tagName & "#>"

                                'Open command - get pictureproperty
                                dbDataSetChilds = New Data.DataSet
                                sSqlQueryChilds = "SELECT PKEY FROM " & oLWObjectTypes.emNodesTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPicture 
                                oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                                If dbDataSetChilds.Tables(0).Rows.Count = 0 Then
                                    'delete the tag
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                End If


                                For Each oDRow In dbDataSetChilds.Tables(0).Rows
                                    'Add pictureproperty from the template
                                    oPictureProperty = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPicture, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                    oPictureProperty.sWorkMode = sWorkMode
                                    oPictureProperty.SCatalog = SCatalog
                                    oPictureProperty.SParent = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
                                    sOutput = oPictureProperty.GetOutput("SELECT " & oLWObjectTypes.emPicturePropertyTable & ".* FROM " & oLWObjectTypes.emPicturePropertyTable & "," & oLWObjectTypes.emNodesTable & " WHERE " & oLWObjectTypes.emPicturePropertyTable & ".PKEY='" & oDRow.Item("PKEY") & "' AND " & oLWObjectTypes.emPicturePropertyTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'")
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                    oPictureProperty.Dispose()
                                    oPictureProperty = Nothing
                                    firstPos = firstPos + Len(sOutput)
                                    'end adding
                                Next

                                dbDataSetChilds = Nothing

                            ElseIf InStr(tagName, "Text_", CompareMethod.Text) Then
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & "" & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                            ElseIf InStr(tagName, "PARTLABEL", CompareMethod.Text) Then
                                'check if we have a index with propnum.
                                sPropName = Right(tagName, Len(tagName) - InStrRev(tagName, "="))

                                Dim sSqlPartLabel As String
                                Dim oDRowLabel() As Data.DataRow
                                'Open command - get partlabels
                                oDRowLabel = dbDataSetPartLabels.Tables(0).Select("PKEY = '" & sPropName & "'")

                                If oDRowLabel.Length > 0 Then
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & oDRowLabel(0).Item("PENGDESC") & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                End If

                            ElseIf InStr(tagName, "DOCUMENTPATH", CompareMethod.Text) Then
                                If dbDataSet.Tables(0).Rows(iRows).Item("pLinkToNetworkFile") = oLWObjectTypes.emNo Then
                                    If sWorkMode = eWorkMode.OnlineMode Then
                                        'local(via catalog websharing)
                                        If sImageMapManage = "Central" Then
                                            sSimpleTagText = "/catalog/catalog/" & dbDataSet.Tables(0).Rows(iRows).Item("pKeyCatalog") & "/object/" & dbDataSet.Tables(0).Rows(iRows).Item("pDocPath")
                                        Else
                                            sSimpleTagText = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & dbDataSet.Tables(0).Rows(iRows).Item("pKeyCatalog") & "/object/" & dbDataSet.Tables(0).Rows(iRows).Item("pDocPath")
                                        End If
                                    Else
                                        sSimpleTagText = "object/" & dbDataSet.Tables(0).Rows(iRows).Item("pDocPath")
                                    End If
                                Else
                                    If sWorkMode = eWorkMode.OnlineMode Then
                                        'Net
                                        sSimpleTagText = dbDataSet.Tables(0).Rows(iRows).Item("pOrgFileName")
                                    Else
                                        'check if we want to publish network files to offline....
                                        sSimpleTagText = "object/" & getFileName(dbDataSet.Tables(0).Rows(iRows).Item("pOrgFileName"))
                                    End If
                                End If

                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                            ElseIf InStr(tagName, "FILE_OBJECT", CompareMethod.Text) Then

                                Dim dbDataSetFileObject As Data.DataSet
                                Dim sSqlQueryFileObject As String
                                Dim sFilePath As String

                                dbDataSetFileObject = New Data.DataSet
                                sSqlQueryFileObject = "SELECT PLINKTONETWORKFILE,PKEYCATALOG,PDOCPATH,PORGFILENAME FROM " & oLWObjectTypes.emWinObjectTable & " ," & oLWObjectTypes.emNodesTable & " WHERE " & oLWObjectTypes.emNodesTable & ".PKEY=" & oLWObjectTypes.emWinObjectTable & ".PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND " & oLWObjectTypes.emNodesTable & ".POBJECTTYPE=" & oLWObjectTypes.emWinObject & " AND " & oLWObjectTypes.emNodesTable & ".PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
                                oConn.Open_Dataset(sSqlQueryFileObject, dbDataSetFileObject)

                                If dbDataSetFileObject.Tables(0).Rows.Count > 0 Then
                                    If dbDataSetFileObject.Tables(0).Rows(0).Item("pLinkToNetworkFile") = oLWObjectTypes.emNo Then
                                        If sWorkMode = eWorkMode.OnlineMode Then
                                            'local(via catalog websharing)
                                            If sImageMapManage = "Central" Then
                                                sFilePath = "/catalog/catalog/" & dbDataSetFileObject.Tables(0).Rows(0).Item("pKeyCatalog") & "/object/" & dbDataSetFileObject.Tables(0).Rows(0).Item("pDocPath")
                                            Else
                                                sFilePath = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & dbDataSetFileObject.Tables(0).Rows(0).Item("pKeyCatalog") & "/object/" & dbDataSetFileObject.Tables(0).Rows(0).Item("pDocPath")
                                            End If
                                        Else
                                            sFilePath = "object/" & dbDataSetFileObject.Tables(0).Rows(0).Item("pDocPath")
                                        End If
                                    Else
                                        If sWorkMode = eWorkMode.OnlineMode Then
                                            'Net
                                            sFilePath = dbDataSetFileObject.Tables(0).Rows(0).Item("pOrgFileName")
                                        Else
                                            'check if we publish files from network
                                            sFilePath = "object/" & getFileName(dbDataSetFileObject.Tables(0).Rows(0).Item("pOrgFileName"))
                                        End If

                                    End If
                                End If

                                dbDataSetFileObject.Dispose()
                                dbDataSetFileObject = Nothing

                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sFilePath & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                            ElseIf InStr(tagName, "SITE_HEADER", CompareMethod.Text) Then

                                Dim sFileName As String = Right(tagName, Len(tagName) - InStrRev(tagName, "="))
                                sFileName = ServerObj.MapPath(sFileName)
                                'sFileName = "c:\inetpub\wwwroot\webeditor\templates\" & sFileName
                                Dim oSiteHeader As New CHtmlGenerator(sFileName, True, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                oSiteHeader.sWorkMode = sWorkMode
                                oSiteHeader.SCatalog = SCatalog
                                oSiteHeader.SParent = SParent
                                sOutput = oSiteHeader.GetOutput("select * from (select  * from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.PKEY=t_Cat_nodes.PPARENTKEY start with t_cat_nodes.pkey='" & SCatalog & "') t1,t_cat_part where t1.pkey=t_cat_part.pkey and pkeytype=64 and rownum<2")
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))
                                oSiteHeader.Dispose()
                                oSiteHeader = Nothing
                                firstPos = firstPos + Len(sOutput)

                            ElseIf InStr(tagName, "PARTS_LOOP_BEGIN", CompareMethod.Text) Then

                                Dim aParameters() As String
                                Dim iParameter As Integer
                                Dim sPopupLabel1 As String
                                Dim sPopupLabel2 As String
                                Dim sPopupLabel3 As String
                                Dim sPopupProperty1 As String
                                Dim sPopupProperty2 As String
                                Dim sPopupProperty3 As String

                                'search for end tag PARTS_LOOP_END and send.
                                iStartLoopPos = lastPos + Len(EndDelimeter)
                                iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "PARTS_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)
                                sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndLoopPos - iStartLoopPos)
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndLoopPos + Len(StartDelimeter) + Len(EndDelimeter) + Len("PARTS_LOOP_END"))



                                aParameters = Split(tagName, " ")
                                'default propery 1 is hebrew name
                                For iParameter = 1 To aParameters.Length - 1
                                    If InStr(aParameters(iParameter), "=") > 0 Then
                                        If Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1) = "TYPE" Then
                                            sPopupLabel1 = Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                            'sPopupLabel1 = Mid(sPopupLabel1, 2, sPopupLabel1.Length - 2)
                                        End If
                                        If Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1) = "ITEMS" Then
                                            sPopupLabel2 = Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                            'sPopupLabel2 = Mid(sPopupLabel2, 2, sPopupLabel2.Length - 2)
                                        End If
                                    End If
                                Next

                                'Open command - get childs
                                dbDataSetChilds = New Data.DataSet

                                sSqlQueryChilds = "SELECT " & oLWObjectTypes.emNodesTable & ".PKEY FROM " & oLWObjectTypes.emNodesTable & ", " & oLWObjectTypes.emPartTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY " 

                                If sPopupLabel1 <> "" Then
                                    sSqlQueryChilds = sSqlQueryChilds & " And " & oLWObjectTypes.emPartTable & ".PKEYTYPE='" & sPopupLabel1 & "'"
									
                                End If

                                If sPopupLabel2 <> "" Then
                                    sSqlQueryChilds = sSqlQueryChilds & " And rownum <" & sPopupLabel2
									
                                End If



                                oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                                ''''check if we need to put the childs in sort order.
                                Dim sPkeyChilds As String
                                Dim aPkeyChilds() As String

                                If Not IsDBNull(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) Then
                                    If Len(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) > 0 Then
                                        sPkeyChilds = dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")
                                        aPkeyChilds = Split(sPkeyChilds, "|")
                                    End If
                                End If

                                'Add part from the template

                                'check if we have at least on child
                                If dbDataSetChilds.Tables(0).Rows.Count > 0 Then
                                    Dim sSubQueryStatement As String
                                    Dim iSubQueryStatement As Integer
                                    If Not aPkeyChilds Is Nothing Then
                                        sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,0 AS PORDER from dual union "
                                        For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                            sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "'," & iSubQueryStatement & " from dual union "
                                        Next
                                        sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                                    Else
                                        sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                                    End If
                                    oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                    oParts.sWorkMode = sWorkMode
                                    oParts.SCatalog = SCatalog
                                    oParts.SParent = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
                                    If gemAppMode = "Access" Then
                                        'sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")")
										sOutput = oParts.GetOutput("SELECT DISTINCT T_CAT_PART.PBALOONNUMBER AS PORDER, T_CAT_PART.*, T_CAT_CATALOG.PHEBDESC AS Catalog_Name FROM (T_CAT_PART LEFT JOIN T_CAT_CATALOG ON T_CAT_PART.PKEYCATALOG = T_CAT_CATALOG.PKEY) INNER JOIN T_CAT_NODES ON T_CAT_PART.PKEY = T_CAT_NODES.PKEY WHERE T_CAT_NODES.PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "'")
                                    Else
                                       	sSqlQueryChilds = "SELECT DISTINCT tmpChilds.PORDER,Replace(T_CAT_PART.PHEBDESC,chr(253),'') PHEBDESC," & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & ", " & oLWObjectTypes.emNodesTable & ", " & sSubQueryStatement & " tmpChilds WHERE " & oLWObjectTypes.emPartTable & ".PKEY=tmpChilds.PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "' AND PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND (" & oLWObjectTypes.emPartTable & ".PKEYTYPE<>'43' OR " & oLWObjectTypes.emPartTable & ".PKEYTYPE IS NULL) "
										
                                        If sPopupLabel1 <> "" Then
                                            sSqlQueryChilds = sSqlQueryChilds & " And " & oLWObjectTypes.emPartTable & ".PKEYTYPE='" & sPopupLabel1 & "'"
											
										End If

                                        If sPopupLabel2 <> "" Then
                                            sSqlQueryChilds = sSqlQueryChilds & " And rownum <" & sPopupLabel2
                                        End If

                                        If aPkeyChilds Is Nothing Then
											sSqlQueryChilds = sSqlQueryChilds & " ORDER BY lpad(trim(PBALOONNUMBER),4,'0')"
										Else
                                            sSqlQueryChilds = sSqlQueryChilds & " ORDER BY PORDER"
                                        End If
                                        
                                        If sPopupLabel1 <> "" Or sPopupLabel2 <> "" Then
                                            sSqlQueryChilds = sSqlQueryChilds & " desc, pupdatedate desc"
                                        End If

                                        sOutput = oParts.GetOutput(sSqlQueryChilds)
                                    End If
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                    oParts.Dispose()
                                    oParts = Nothing
                                    firstPos = firstPos + Len(sOutput)
                                    'end adding
                                End If

                                dbDataSetChilds.Dispose()
                                dbDataSetChilds = Nothing

                            ElseIf InStr(tagName, "OBJECTS_LOOP_BEGIN", CompareMethod.Text) Then

                                Dim aParameters() As String
                                Dim iParameter As Integer
                                Dim sPopupLabel1 As String
                                Dim sPopupLabel2 As String
                                Dim sPopupLabel3 As String
                                Dim sPopupProperty1 As String
                                Dim sPopupProperty2 As String
                                Dim sPopupProperty3 As String

                                'search for end tag PARTS_LOOP_END and send.
                                iStartLoopPos = lastPos + Len(EndDelimeter)
                                iEndLoopPos = InStr(iStartLoopPos, sXmlOutputTemp, "OBJECTS_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)

                                'solve objects_loop_begin recursive call
                                Dim iNumOfLoops As Integer = 0
                                Dim bWhile As Boolean = True

                                Dim iStartPosTemp As Integer = -1
                                Dim origStartPos As Integer = iStartLoopPos - tagName.Length - StartDelimeter.Length - EndDelimeter.Length - 1
                                Dim iEndPosTemp As Integer = iEndLoopPos

                                '5 = <# #>
                                iStartPosTemp = sXmlOutputTemp.LastIndexOf("<#OBJECTS_LOOP_BEGIN", iEndPosTemp)
                                While bWhile

                                    If iStartPosTemp = origStartPos Then
                                        bWhile = False
                                    Else
                                        iEndPosTemp = InStr(iEndPosTemp + StartDelimeter.Length + 1, sXmlOutputTemp, "OBJECTS_LOOP_END", CompareMethod.Text) - Len(StartDelimeter)
                                        iStartPosTemp = sXmlOutputTemp.LastIndexOf("<#OBJECTS_LOOP_BEGIN", iStartPosTemp - 1)
                                    End If
                                End While


                                sLoopTemplate = Mid(sXmlOutputTemp, iStartLoopPos, iEndPosTemp - iStartLoopPos)
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & Mid(sXmlOutputTemp, iEndPosTemp + Len(StartDelimeter) + Len(EndDelimeter) + Len("OBJECTS_LOOP_END"))



                                aParameters = Split(tagName, " ")
                                'default propery 1 is hebrew name
                                Dim sOptionName As String
                                Dim sOptionValue As String
                                Dim sItemTypes() As String
                                Dim sDocumentTypes() As String
                                Dim sItemTypesSQL As String = ""
                                Dim sItemTypesAndSQL As String = ""
                                Dim sItemTypesOrSQL As String = " AND ("
                                Dim sDocumentTypesSQL As String = ""
                                Dim sDocumentTypesAndSQL As String = ""
                                Dim sDocumentTypesOrSQL As String = " AND ("
                                Dim sOrderField As String = "PORDER"
                                For iParameter = 1 To aParameters.Length - 1
                                    If InStr(aParameters(iParameter), "=") > 0 Then
                                        sOptionName = Left(aParameters(iParameter), InStr(aParameters(iParameter), "=") - 1)
                                        sOptionValue = Mid(aParameters(iParameter), InStr(aParameters(iParameter), "=") + 1)
                                        Select Case sOptionName
                                            Case "ITYPE"
                                                sItemTypes = Split(sOptionValue, "|")
                                                For i = 0 To sItemTypes.Length - 1
                                                    If (sItemTypes(i).StartsWith("-")) Then
                                                        sItemTypesAndSQL = sItemTypesAndSQL & " AND T_CAT_PART.PKEYTYPE<>" & sItemTypes(i).Substring(1)
                                                    Else
                                                        sItemTypesOrSQL = sItemTypesOrSQL & " T_CAT_PART.PKEYTYPE=" & sItemTypes(i) & " OR "
                                                    End If
                                                Next
                                                sItemTypesSQL = sItemTypesAndSQL
                                                If sItemTypesOrSQL <> " AND (" Then
                                                    sItemTypesSQL = sItemTypesSQL & sItemTypesOrSQL.Remove(sItemTypesOrSQL.Length - 4, 3) & ")"
                                                End If
                                            Case "DTYPE"
                                                sDocumentTypes = Split(sOptionValue, "|")
                                                For i = 0 To sDocumentTypes.Length - 1
                                                    If (sDocumentTypes(i).StartsWith("-")) Then
                                                        sDocumentTypesAndSQL = sDocumentTypesAndSQL & " AND T_CAT_WINOBJECT.PKEYTYPE<>" & sDocumentTypes(i).Substring(1)
                                                    Else
                                                        sDocumentTypesOrSQL = sDocumentTypesOrSQL & " T_CAT_WINOBJECT.PKEYTYPE=" & sDocumentTypes(i) & " OR "
                                                    End If
                                                Next
                                                sDocumentTypesSQL = sDocumentTypesAndSQL
                                                If sDocumentTypesOrSQL <> " AND (" Then
                                                    sDocumentTypesSQL = sDocumentTypesSQL & sDocumentTypesOrSQL.Remove(sDocumentTypesOrSQL.Length - 4, 3) & ")"
                                                End If
                                            Case "ORDER"
                                                If Not sOptionValue.IndexOf("_") = -1 Then
                                                    sOptionValue = sOptionValue.Replace("_", " ")
                                                End If
                                                sOrderField = sOptionValue
                                            Case Else
                                        End Select
                                    End If
                                Next

                                'Open command - get childs
                                dbDataSetChilds = New Data.DataSet

                                sSqlQueryChilds = "SELECT " & oLWObjectTypes.emNodesTable & ".PKEY FROM " & oLWObjectTypes.emNodesTable & ", " & oLWObjectTypes.emPartTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY "
                                oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                                sSqlQueryChilds = "SELECT " & oLWObjectTypes.emNodesTable & ".PKEY FROM " & oLWObjectTypes.emNodesTable & ", " & oLWObjectTypes.emWinObjectTable & " WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emWinObject & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emWinObjectTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY "
                                oConn.Open_Dataset(sSqlQueryChilds, dbDataSetChilds)

                                ''''check if we need to put the childs in sort order.
                                Dim sPkeyChilds As String
                                Dim aPkeyChilds() As String

                                If Not IsDBNull(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) Then
                                    If Len(dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")) > 0 Then
                                        sPkeyChilds = dbDataSet.Tables(0).Rows(iRows).Item("PKEYCHILDS")
                                        aPkeyChilds = Split(sPkeyChilds, "|")
                                    End If
                                End If

                                'Add part from the template

                                'check if we have at least on child
                                If dbDataSetChilds.Tables(0).Rows.Count > 0 Then
                                    Dim sSubQueryStatement As String
                                    Dim iSubQueryStatement As Integer
                                    Dim oDbPredefinedDataset As New Data.DataSet

                                    If Not aPkeyChilds Is Nothing Then
                                        sSubQueryStatement = "SELECT '" & aPkeyChilds(0) & "' AS PKEY ,0 AS PORDER from dual union "
                                        For iSubQueryStatement = 1 To aPkeyChilds.Length - 1
                                            sSubQueryStatement = sSubQueryStatement & " SELECT '" & aPkeyChilds(iSubQueryStatement) & "'," & iSubQueryStatement & " from dual union "
                                        Next
                                        sSubQueryStatement = "(" & Mid(sSubQueryStatement, 1, Len(sSubQueryStatement) - 6) & ")"
                                    Else
                                        sSubQueryStatement = "(SELECT '0' AS PKEY ,0 AS PORDER from dual)"
                                    End If
                                    oParts = New CHtmlGenerator(sLoopTemplate, sODBCName, oLWObjectTypes.emPart, sAuthentication, gemAppMode, sImageMapManage, oConn)
                                    oParts.sWorkMode = sWorkMode
                                    oParts.SCatalog = SCatalog
                                    oParts.SParent = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
                                    If gemAppMode = "Access" Then
                                        'sOutput = oParts.GetOutput("SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".*, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM ((" & oLWObjectTypes.emPartTable & " INNER JOIN " & oLWObjectTypes.emCatalogTable & " ON " & oLWObjectTypes.emCatalogTable & ".PKEY=" & oLWObjectTypes.emPartTable & ".PKEYCATALOG) LEFT JOIN " & oLWObjectTypes.emObjectTypesTable & " ON " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY) LEFT JOIN " & oLWObjectTypes.emStatusTable & " ON " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY WHERE " & oLWObjectTypes.emPartTable & ".PKEY IN (" & sINStatement & ")")
                                    Else
                                        sSqlQueryChilds = "SELECT DISTINCT tmpChilds.PORDER," & oLWObjectTypes.emPartTable & ".*, T_CAT_NODES.POBJECTTYPE, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME," & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emPartTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & ", " & oLWObjectTypes.emNodesTable & ", " & sSubQueryStatement & " tmpChilds WHERE " & oLWObjectTypes.emPartTable & ".PKEY=tmpChilds.PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emPartTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+) AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "' AND PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emPart & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emPartTable & ".PKEY=" & oLWObjectTypes.emNodesTable & ".PKEY AND " & oLWObjectTypes.emNodesTable & ".PPARENTKEY='" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND (" & oLWObjectTypes.emPartTable & ".PKEYTYPE<>'43' OR " & oLWObjectTypes.emPartTable & ".PKEYTYPE IS NULL) "
                                        If sItemTypesSQL <> "" Then
                                            sSqlQueryChilds = sSqlQueryChilds & sItemTypesSQL
                                        End If
                                        sSqlQueryChilds = sSqlQueryChilds & " ORDER BY tmpChilds.PORDER"
                                        oConn.Open_Dataset(sSqlQueryChilds, oDbPredefinedDataset)
                                        sSqlQueryChilds = "SELECT DISTINCT tmpChilds.PORDER, " & oLWObjectTypes.emWinObjectTable & ".*, T_CAT_NODES.POBJECTTYPE, " & oLWObjectTypes.emObjectTypesTable & ".PTYPENAME, " & oLWObjectTypes.emCatalogTable & ".PHEBDESC AS Catalog_Name," & oLWObjectTypes.emStatusTable & ".PSTATUS AS Status_Name FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emObjectTypesTable & "," & oLWObjectTypes.emCatalogTable & "," & oLWObjectTypes.emStatusTable & ", " & oLWObjectTypes.emNodesTable & " ," & sSubQueryStatement & " tmpChilds WHERE PPARENTKEY = '" & dbDataSet.Tables(0).Rows(iRows).Item("PKEY") & "' AND POBJECTTYPE=" & oLWObjectTypes.emWinObject & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & " AND " & oLWObjectTypes.emNodesTable & ".PKEY=" & oLWObjectTypes.emWinObjectTable & ".PKEY AND " & oLWObjectTypes.emWinObjectTable & ".PKEY=tmpChilds.PKEY(+) AND " & oLWObjectTypes.emWinObjectTable & ".PKEYTYPE=" & oLWObjectTypes.emObjectTypesTable & ".PKEY(+) AND " & oLWObjectTypes.emWinObjectTable & ".PSTATUS=" & oLWObjectTypes.emStatusTable & ".PKEY(+)  AND " & oLWObjectTypes.emStatusTable & ".PKEYCATALOG(+)='" & SCatalog & "' AND " & oLWObjectTypes.emCatalogTable & ".PKEY='" & SCatalog & "' "
                                        If sDocumentTypesSQL <> "" Then
                                            sSqlQueryChilds = sSqlQueryChilds & sDocumentTypesSQL
                                        End If
                                        sSqlQueryChilds = sSqlQueryChilds & " ORDER BY tmpChilds.PORDER"
                                        oConn.Open_Dataset(sSqlQueryChilds, oDbPredefinedDataset)
                                        oDbPredefinedDataset = SortDataset(oDbPredefinedDataset, sOrderField)
                                        sOutput = oParts.GetOutput("", oDbPredefinedDataset)
                                    End If
                                    sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sOutput & Mid(sXmlOutputTemp, firstPos)
                                    oParts.Dispose()
                                    oParts = Nothing
                                    firstPos = firstPos + Len(sOutput)
                                    'end adding
                                End If

                                dbDataSetChilds.Dispose()
                                dbDataSetChilds = Nothing

                            Else
                                'for simple tags....

                                sSimpleTagText = ""

                                If Not IsDBNull(dbDataSet.Tables(0).Rows(iRows).Item(ConvertFromTagToColumnName(tagName))) Then
                                    sSimpleTagText = dbDataSet.Tables(0).Rows(iRows).Item(ConvertFromTagToColumnName(tagName))

                                    If (ConvertFromTagToColumnName(tagName) = "PNEXT" Or ConvertFromTagToColumnName(tagName) = "PPREV") Then
                                        If (sSimpleTagText.ToString <> "") Then
                                            Dim tmpDbDataset As New Data.DataSet
                                            Dim sSqlQueryTemplate As String
                                            sSqlQueryTemplate = "SELECT T_CAT_PAGE.PPAGETEMPLATE,T_CAT_PAGE.PKEYCATALOG,T_CAT_NODES.PPARENTKEY FROM T_CAT_PAGE,T_CAT_NODES WHERE T_CAT_PAGE.PKEY=T_CAT_NODES.PKEY and T_CAT_PAGE.PKEY='" & sSimpleTagText.ToString & "'"
                                            'Else
                                            '    sSqlQueryTemplate = "select PINDEXTEMPLATE from " & oLWObjectTypes.emCatalogTable & " where pkey='" & SCatalog & "'"


                                            oConn.Open_Dataset(sSqlQueryTemplate, tmpDbDataset)
                                            If (tmpDbDataset.Tables(0).Rows.Count > 0) Then

                                                sSimpleTagText = "ShowTemplate.aspx?Template=" & tmpDbDataset.Tables(0).Rows(0).Item("PPAGETEMPLATE") & "&amp;Pkey=" & sSimpleTagText & "&amp;ParentKey=" & tmpDbDataset.Tables(0).Rows(0).Item("PPARENTKEY") & "&amp;Type=2&amp;PkeyCatalog=" & tmpDbDataset.Tables(0).Rows(0).Item("PKEYCATALOG")

                                                tmpDbDataset.Dispose()
                                                tmpDbDataset = Nothing

                                            End If
                                        End If
									end if
                                        '                      ShowTemplate.aspx?Template=normal.html&         Pkey=TOPF1a137606&              ParentKey=TOPF1a136867  &Type=2&                                 PkeyCatalog=LWMM1a600001
                                 End If
                                sXmlOutputTemp = Mid(sXmlOutputTemp, 1, firstPos - 1) & sSimpleTagText.Trim & Mid(sXmlOutputTemp, lastPos + Len(EndDelimeter))

                            End If

                    End Select

                    firstPos = InStr(1, sXmlOutputTemp, StartDelimeter, CompareMethod.Text)

                End While
                'add the new object node to the xmloutput variable
                xmlOutput = xmlOutput & sXmlOutputTemp
            End If

        Next

        oAclPermissions.Dispose()
        oAclPermissions = Nothing

        If (InStrRev(xmlOutput, "</") - 2) > 0 And InStrRev(xmlOutput, "</") - 2 Then
            'xmlOutput = Mid(xmlOutput, 1, InStrRev(xmlOutput, "</") - 2) & "<TotalTime>" & (Timer() - dTotal) & "</TotalTime>" & vbCrLf & "<StartTime>" & dStartDate & "</StartTime>" & vbCrLf & "<FinishTime>" & Now() & "</FinishTime>" & vbCrLf & Mid(xmlOutput, InStrRev(xmlOutput, "</") - 2)
        End If

        GetOutput = xmlOutput

    End Function

    Public Function RGB2BGR(ByVal c As Long) As Long
        Dim RV As Byte
        Dim GV As Byte
        Dim BV As Byte

        RV = (c And &HFF)
        GV = (c And &HFFFF&) \ &H100
        BV = (c \ &H10000)

        RGB2BGR = (((&H100& * GV) Or BV) Or (&H10000 * GV))
    End Function

    Public Function BGR2RGB(ByVal BGR) As String
        Dim Mask As Long
        Mask = (BGR And &HFF) Xor (BGR \ &H10000)
        BGR2RGB = Hex(BGR Xor (Mask * &H10001))
    End Function

    
	
	Function GenerateMap_HotSpots(ByVal sImageMapPkey As String, ByVal sLabel1 As String, ByVal sLabel2 As String, ByVal sLabel3 As String, ByVal sProperty1 As String, ByVal sProperty2 As String, ByVal sProperty3 As String) As String

        Dim oDbDataSetLinks As New Data.DataSet
        Dim sSqlQueryLinks As String
        Dim sMapHtmlOutput As String

        'gets all the links (areas)
        sSqlQueryLinks = "SELECT T_CAT_LINK.PKEY,PHEBDESC, PLINKTYPE, PURL, PHOTSPOTGRAPHICTYPE, PUSERREMARK, PBALOONNUMBER " _
                & " FROM  T_CAT_LINK INNER JOIN T_CAT_NODES ON T_CAT_LINK.PKEY = T_CAT_NODES.PKEY " _
                & " WHERE T_CAT_NODES.PPARENTKEY='" & sImageMapPkey & "'"

        oConn.Open_Dataset(sSqlQueryLinks, oDbDataSetLinks)
        'check if there is at least one hotspot
        If oDbDataSetLinks.Tables(0).Rows.Count > 0 Then
            'insert MAP (hotspot)
            Dim sPkeyLink As String
			Dim sLinkName as string
            Dim sPLinkType As Integer
            Dim sCoords As String = ""
            Dim oDbDataSetCoords As Data.DataSet
            Dim sSqlQueryCoords As String
            Dim oItemLink As Data.DataRow
            Dim oItemCoord As Data.DataRow
            Dim sLinkedParts As String
            Dim oDbDataSetLinkedParts As Data.DataSet
            Dim oItemLinkedPart As Data.DataRow
            Dim sSqlQueryLinkedParts As String
            Dim sOnMouseOverHTML As String
            Dim sOnMouseOutHTML As String
            Dim sShape As String
            Dim sPLinkPartKey As String
            Dim sHrefLinkPartKey As String
            Dim sVmlGraphicHotSpot As String
            Dim sVmlGraphicHotSpotCoords As String
            Dim sVmlGraphicHotSpotColor As String
            Dim sVmlGraphicHotSpotWeight As String
            Dim sPhotoGraphicType As String
            Dim oDbDataSetSubPicture As Data.DataSet
            Dim sSqlSubPicture As String
            Dim sSubPictureName As String
            Dim oDbDataSetSubText As Data.DataSet
            Dim sSqlSubText As String
            Dim sSubText As String
            Dim sUserRemarks As String
            Dim sSqlPopupAdditional As String 'addition for the sql statement for other properties.
            Dim sPopupText As String="" 'popup text.

            'declares for ballon
            Dim PX As Integer
            Dim PY As Integer
            Dim j As Integer
            Dim Rbig As Long, r As Long, Dx As Long, Dy As Long
            Dim sLinkBalloonNumber As String

            'insert map
            sMapHtmlOutput = "<MAP NAME=""map_" & sImageMapPkey & """>"
            sVmlGraphicHotSpot = ""
            'for each link
            For Each oItemLink In oDbDataSetLinks.Tables(0).Rows
                'pkeylink
                sPkeyLink = oItemLink.Item("PKEY")
				'link name
				sLinkName=oItemLink.Item("PHEBDESC")
                'linktype - 1-Poly, 2-Circle, 3-Rect, 8-Arrow
                sPLinkType = oItemLink.Item("PLINKTYPE")
                'link url.
                sHrefLinkPartKey = oItemLink.Item("PURL") & ""
                'is graphichotspot: 1-no.
                If IsDBNull(oItemLink.Item("PHOTSPOTGRAPHICTYPE")) Then
                    sPhotoGraphicType = 1
                Else
                    sPhotoGraphicType = oItemLink.Item("PHOTSPOTGRAPHICTYPE")
                End If
                'onmouseover area methods
                sOnMouseOverHTML = "JavaScript:"
                'onmouseout area methods
                sOnMouseOutHTML = "JavaScript:"
                'remark for user when put the mouser on the hotspot
                sUserRemarks = oItemLink.Item("PUSERREMARK") & ""
                'area coords
                sCoords = ""
                'vml coords
                sVmlGraphicHotSpotCoords = ""
                'vml color
                sVmlGraphicHotSpotColor = ""
                'area shape. (RECT/CIRCLE...)
                sShape = ""
                'vml weight
                sVmlGraphicHotSpotWeight = ""
                'baloon number text
                sLinkBalloonNumber = oItemLink.Item("PBALOONNUMBER").ToString

                'change the url (if exist)
                If sHrefLinkPartKey <> "" Then
                    'check if put an outside link or linkware link
                    If InStr(sHrefLinkPartKey, "Page_") Or InStr(sHrefLinkPartKey, "Part_") Then
                        'sHrefLinkPartKey = "JavaScript:parent.location.href='../OnlineSearchRedirect.aspx?pkey=" & Replace(Replace(Replace(sHrefLinkPartKey, "Page_", ""), "Part_", ""), ".html", "") & "';"
                        Dim keyh As String = Replace(Replace(Replace(sHrefLinkPartKey, "Page_", ""), "Part_", ""), ".html", "")
                        sHrefLinkPartKey = "JavaScript:if (top.frames['Index']!= null) top.frames('Index').LinkSwitch('" & keyh & "'); else top.document.location.href='../OnlineSearchRedirect.aspx?Pkey=" & keyh & "&PkeyCatalog=" & SCatalog & "';"
                    End If
                Else
                    sHrefLinkPartKey = "JavaScript:void(0);"
                End If

                'check if the user write an remark on hotspot.
                If sUserRemarks <> "" Then
                    sUserRemarks = "|" & sUserRemarks & "^Remark"
                End If

                'build the addition columns that we need to the query.
                If sProperty1 <> "" Then
                    sSqlPopupAdditional = oLWObjectTypes.emPartTable & "." & sProperty1 & ", "
					'sSqlPopupAdditional = " Replace(" & oLWObjectTypes.emPartTable & "." & sProperty1 & ",chr(253),'') AS " & sProperty1 & " , "
                End If
                If sProperty2 <> "" Then
                    sProperty2 = sProperty2
                    sSqlPopupAdditional = sSqlPopupAdditional & oLWObjectTypes.emMakakTable & "." & sProperty2 & ", "
                End If
                If sProperty3 <> "" Then
                    sProperty3 = sProperty3
                    sSqlPopupAdditional = sSqlPopupAdditional & oLWObjectTypes.emPartTable & "." & sProperty3 & ", "
                End If

                'there is at least one field for popup.
                sSqlPopupAdditional = Left(sSqlPopupAdditional, sSqlPopupAdditional.Length - 2)

                'bring the linked parts of the specific hotspot.
                sSqlQueryLinkedParts = "SELECT " & oLWObjectTypes.emBindLinkTable & ".PKEYLINK, " & oLWObjectTypes.emBindLinkTable & ".PKEYPART, " & sSqlPopupAdditional _
                                    & " FROM " & oLWObjectTypes.emBindLinkTable & "," & oLWObjectTypes.emPartTable & "," & oLWObjectTypes.emMakakTable & "," & oLWObjectTypes.emNodesTable & " " _
                                    & " WHERE (( " & oLWObjectTypes.emBindLinkTable & ".PKEYLINK=" & oLWObjectTypes.emNodesTable & ".PKEY) " _
                                    & "AND (" & oLWObjectTypes.emNodesTable & ".PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted _
                                    & ") AND (" & oLWObjectTypes.emBindLinkTable & ".PKEYPART = " & oLWObjectTypes.emPartTable & ".PKEY)" _
                                    & " AND (" & oLWObjectTypes.emPartTable & ".PKEY = " & oLWObjectTypes.emMakakTable & ".PPARENTKEY(+))" _
                                    & " AND ((" & oLWObjectTypes.emBindLinkTable & ".PKEYLINK)='" & sPkeyLink & "'))"
				
				sSqlQueryLinkedParts = "SELECT T_CAT_BIND_LINK.PKEYLINK, T_CAT_BIND_LINK.PKEYPART,  " & sSqlPopupAdditional _
										& " FROM (T_CAT_BIND_LINK INNER JOIN (T_CAT_PART LEFT JOIN T_CAT_MAKAT ON T_CAT_PART.PKEY = T_CAT_MAKAT.PPARENTKEY) ON T_CAT_BIND_LINK.PKEYPART = T_CAT_PART.PKEY) INNER JOIN T_CAT_NODES ON T_CAT_BIND_LINK.PKEYLINK = T_CAT_NODES.PKEY" _
										& " WHERE T_CAT_BIND_LINK.PKEYLINK='" & sPkeyLink & "'" ' AND T_CAT_NODES.PGLOBALSTATUS<>4"
				
                oDbDataSetLinkedParts = New Data.DataSet
                oConn.Open_Dataset(sSqlQueryLinkedParts, oDbDataSetLinkedParts)
				'sOnMouseOverHTML=sOnMouseOverHTML & "," & sSqlQueryLinkedParts & " " & sODBCName
				'sOnMouseOverHTML = oDbDataSetLinkedParts.Tables.Count.ToString()
                'check if there are linked parts.
				If oDbDataSetLinkedParts.Tables.Count > 0 then
				
	                If oDbDataSetLinkedParts.Tables(0).Rows.Count > 0 Then
						
	                    For Each oItemLinkedPart In oDbDataSetLinkedParts.Tables(0).Rows
	                        If sProperty1 <> "" Then
	                            If oItemLinkedPart.Item(sProperty1) & "" <> "" Then
	                                sPopupText = oItemLinkedPart.Item(sProperty1) & "^" & sLabel1 & "|"
	                            End If
	                        End If

	                        If sProperty2 <> "" Then
	                            If oItemLinkedPart.Item(sProperty2) & "" <> "" Then
	                                sPopupText = sPopupText & oItemLinkedPart.Item(sProperty2) & "^" & sLabel2 & "|"
	                            End If
	                        End If

	                        If sProperty3 <> "" Then
	                            If oItemLinkedPart.Item(sProperty3) & "" <> "" Then
	                                sPopupText = sPopupText & oItemLinkedPart.Item(sProperty3) & "^" & sLabel3 & "|"
	                            End If
	                        End If

	                        'there is at least one field for popup.
							if sPopupText.Length>0 then
								sPopupText = Left(sPopupText, sPopupText.Length - 1)
							end if
	                        sOnMouseOverHTML = sOnMouseOverHTML & "ToolTipMultiLine('" & Replace(Replace(Replace(Replace(sPopupText & "", """", "&quot;"), "'", "\'"), Chr(13), ""), Chr(10), "") & Replace(Replace(Replace(Replace(sUserRemarks & "", """", "&quot;"), Chr(13), ""), Chr(10), ""), "'", "\'") & "'); highlight('" & oItemLinkedPart.Item("PKEYPART") & "');Focus('" & oItemLinkedPart.Item("PKEYPART") & "');"
	                        sOnMouseOutHTML = sOnMouseOutHTML & "HideToolTip();unlight('" & oItemLinkedPart.Item("PKEYPART") & "');"
	                        sPLinkPartKey = oItemLinkedPart.Item("PKEYPART")
	                    Next
	                Else
	                    'if there isn't linked parts but there is a url or remark...
	                    If sUserRemarks <> "" Then
	                        'mid - remove the '|' character
	                        sOnMouseOverHTML = sOnMouseOverHTML & "ToolTipMultiLine('" & Replace(Replace(Replace(Replace(Mid(sUserRemarks, 2) & "", """", "&quot;"), "'", "\'"), Chr(13), ""), Chr(10), "") & "'); ;"
	                        sOnMouseOutHTML = sOnMouseOutHTML & "HideToolTip();"
	                        sPLinkPartKey = sPkeyLink
	                    Else
	                        sOnMouseOverHTML = sOnMouseOverHTML & "ToolTipMultiLine(''); Focus('" & sPkeyLink & "');"
	                        sOnMouseOutHTML = sOnMouseOutHTML & "HideToolTip();"
	                        sPLinkPartKey = sPkeyLink
	                    End If
	                End If
				end if
                oDbDataSetLinkedParts.Dispose()
                oDbDataSetLinkedParts = Nothing

                'open dataset and get coords.
                sSqlQueryCoords = "SELECT PKEY, PPARENTKEY, PX, PY, PORDER, PIMPORTSTATUS " _
                                & " FROM " & oLWObjectTypes.emPointTable _
                                & " WHERE (((PPARENTKEY)='" & sPkeyLink & "')) "' _
                                '& " ORDER BY PORDER "
                oDbDataSetCoords = New Data.DataSet
                oConn.Open_Dataset(sSqlQueryCoords, oDbDataSetCoords)
                Try
                    '1-regular hotspot
                    If sPhotoGraphicType <> 1 Then
                        'get color
                        oConn.Open_Dataset("select * from t_cat_arrow where pkey='" & sPkeyLink & "'", oDbDataSetCoords, "GraphicColor")
                        'to subpicture there isnt color for example
                        If oDbDataSetCoords.Tables("GraphicColor").Rows.Count > 0 Then
                            sVmlGraphicHotSpotColor = oDbDataSetCoords.Tables("GraphicColor").Rows(0).Item("PARROWCOLOR") & ""
                            sVmlGraphicHotSpotColor = BGR2RGB(sVmlGraphicHotSpotColor)
                            'check if FF(blue) then replace it with blue.
                            If sVmlGraphicHotSpotColor = "FF" Then
                                sVmlGraphicHotSpotColor = "0000FF"
                            End If
                            sVmlGraphicHotSpotWeight = oDbDataSetCoords.Tables("GraphicColor").Rows(0).Item("PARROWTHICKNESS") & ""
                        End If
                    End If

                    'build the coords for area and for graphic hotspot.
                    Dim DivConstMax As Double = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings("DivConstMax"))
                    Dim DivConstMin As Double = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings("DivConstMin"))
                    Dim ResizeConst As Double = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings("ResizeConst")) / 100

                    Select Case sPLinkType
                        Case oLWObjectTypes.emRect
                            sShape = "RECT"
                            For Each oItemCoord In oDbDataSetCoords.Tables(0).Rows
                                sCoords = sCoords & CStr(CInt(CInt(oItemCoord.Item("PX")) / DivConstMax) * ResizeConst) & "," & CStr(CInt(CInt(oItemCoord.Item("PY")) / DivConstMax * ResizeConst)) & ","
                            Next
                            sCoords = Mid(sCoords, 1, sCoords.Length - 1)
                            'if graphic hotspot - add it to the variable that store the vml code (all areas!!)
                            If sPhotoGraphicType <> 1 Then
                                sVmlGraphicHotSpotCoords = "width: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "; height: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PY")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & "; left:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax) * ResizeConst) & "; top:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax) * ResizeConst) & "; position:absolute"
                                sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:rect style=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """><v:fill opacity=""0.0""/></v:rect>" & vbCrLf
                            End If
                        Case oLWObjectTypes.emCircle
                            sShape = "CIRCLE"
                            sCoords = CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax * ResizeConst))
                            'if graphic hotspot - add it to the variable that store the vml code (all areas!!)
                            If sPhotoGraphicType <> 1 Then
                                sVmlGraphicHotSpotCoords = "width: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMin * ResizeConst)) & "; height: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMin * ResizeConst)) & "; left:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax * ResizeConst)) & "; top:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax * ResizeConst)) & "; position:absolute"
                                sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:oval style=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """><v:fill opacity=""0.0""/></v:oval>" & vbCrLf
                            End If
                        Case oLWObjectTypes.emPoly
                            sShape = "POLY"
                            For Each oItemCoord In oDbDataSetCoords.Tables(0).Rows
                                sCoords = sCoords & CStr(CInt(CInt(oItemCoord.Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oItemCoord.Item("PY")) / DivConstMax * ResizeConst)) & ","
                                sVmlGraphicHotSpotCoords = sVmlGraphicHotSpotCoords & CStr(CInt(CInt(oItemCoord.Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oItemCoord.Item("PY")) / DivConstMax * ResizeConst)) & " "
                            Next
                            'fix the last line
                            sCoords = sCoords & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & ","
                            sVmlGraphicHotSpotCoords = sVmlGraphicHotSpotCoords & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & " "

                            sCoords = Mid(sCoords, 1, sCoords.Length - 1)
                            'if graphic hotspot - add it to the variable that store the vml code (all areas!!)
                            If sPhotoGraphicType <> 1 Then
                                sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:polyline points=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """><v:fill opacity=""0.0""/></v:polyline>" & vbCrLf
                            End If
                        Case oLWObjectTypes.emArrow
                            sShape = "POLY"
                            For Each oItemCoord In oDbDataSetCoords.Tables(0).Rows
                                sVmlGraphicHotSpotCoords = sVmlGraphicHotSpotCoords & CStr(CInt(CInt(oItemCoord.Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oItemCoord.Item("PY")) / DivConstMax * ResizeConst)) & " "
                            Next
                            sVmlGraphicHotSpotCoords = sVmlGraphicHotSpotCoords & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(oDbDataSetCoords.Tables(0).Rows.Count - 3).Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(oDbDataSetCoords.Tables(0).Rows.Count - 3).Item("PY")) / DivConstMax * ResizeConst)) & " "
                            sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:polyline points=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """><v:fill opacity=""0.0""/></v:polyline>" & vbCrLf
                        Case oLWObjectTypes.emSubPicture
                            sSqlSubPicture = "SELECT * FROM " & oLWObjectTypes.emSubPictureTable & " WHERE PKEY='" & sPkeyLink & "'"
                            oDbDataSetSubPicture = New Data.DataSet
                            oConn.Open_Dataset(sSqlSubPicture, oDbDataSetSubPicture)
                            sSubPictureName = oDbDataSetSubPicture.Tables(0).Rows(0).Item("PPICTUREKEY")
                            'put in subpicture name the full path of picture accords with the manage of volume (per catalog or not)
                            If sImageMapManage = "PerCatalog" Then
                                sSubPictureName = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & dbDataSet.Tables(0).Rows(0).Item("PKEYCATALOG") & "/" & sSubPictureName
                            Else
                                sSubPictureName = System.Configuration.ConfigurationManager.AppSettings("ImagesPass") & sSubPictureName
                            End If
                            sVmlGraphicHotSpotCoords = "width: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "; height: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PY")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & "; left:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "; top:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & "; position:absolute"
                            sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:rect style=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """><v:imagedata src=""" & sSubPictureName & """/></v:rect>" & vbCrLf
                        Case oLWObjectTypes.emText
                            sSqlSubText = "SELECT * FROM " & oLWObjectTypes.emSubTextTable & " WHERE PKEY='" & sPkeyLink & "'"
                            oDbDataSetSubText = New Data.DataSet
                            oConn.Open_Dataset(sSqlSubText, oDbDataSetSubText)
                            sSubText = oDbDataSetSubText.Tables(0).Rows(0).Item("PTEXT")
                            sVmlGraphicHotSpotCoords = "width: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "; height: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PY")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & "; left:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "; top:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PY")) / DivConstMax * ResizeConst)) & "; position:absolute"
                            sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:rect style=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """ stroked=""False""><v:fill opacity=""0.0""/><v:textbox>" & ConvertRtfToHtml(sSubText) & "</v:textbox></v:rect>" & vbCrLf
                        Case oLWObjectTypes.emBalloon
                            sShape = "CIRCLE"
                            sCoords = CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PY")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst))
                            sSubText = sLinkBalloonNumber
                            'if graphic hotspot - add it to the variable that store the vml code (all areas!!)
                            If sPhotoGraphicType <> 1 Then
                                sVmlGraphicHotSpotCoords = "width: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMin * ResizeConst)) & "; height: " & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMin * ResizeConst)) & "; left:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PX")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "; top:" & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(1).Item("PY")) / DivConstMax * ResizeConst) - CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)) & "; position:absolute"
                                sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:oval style=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """><v:fill opacity=""0.0""/><v:textbox inset=""0px, 0px, 0px, 0px"">" & "<font color=" & sVmlGraphicHotSpotColor & "><b>" & sSubText & "</b></font></v:textbox></v:oval>" & vbCrLf
                            End If
                            ' next line is here cos balloon has 2 shapes: circle & poly
                            sMapHtmlOutput = sMapHtmlOutput & "<AREA Shape=" & sShape & " id=""area_" & sPLinkPartKey & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ COORDS=" & sCoords & " href=""" & sHrefLinkPartKey & """ > " & vbCrLf

                            sVmlGraphicHotSpotCoords = ""
                            sShape = "POLY"
                            For j = 1 To oDbDataSetCoords.Tables(0).Rows.Count - 1
                                If j = 1 Then

                                    r = CInt(CInt(oDbDataSetCoords.Tables(0).Rows(0).Item("PX")) / DivConstMax * ResizeConst)
                                    Dx = CInt(CInt(oDbDataSetCoords.Tables(0).Rows(j + 1).Item("PX")) - CInt(oDbDataSetCoords.Tables(0).Rows(j).Item("PX"))) / DivConstMax * ResizeConst
                                    Dy = CInt(CInt(oDbDataSetCoords.Tables(0).Rows(j + 1).Item("PY")) - CInt(oDbDataSetCoords.Tables(0).Rows(j).Item("PY"))) / DivConstMax * ResizeConst
                                    Rbig = ((Dx) ^ 2 + (Dy) ^ 2) ^ 0.5
                                    PX = CInt(CInt(oDbDataSetCoords.Tables(0).Rows(j).Item("PX")) / DivConstMax * ResizeConst) + r * (Dx / Rbig)
                                    PY = CInt(CInt(oDbDataSetCoords.Tables(0).Rows(j).Item("PY")) / DivConstMax * ResizeConst) + r * (Dy / Rbig)

                                Else
                                    PX = CInt(CInt(oDbDataSetCoords.Tables(0).Rows(j).Item("PX")) / DivConstMax * ResizeConst)
                                    PY = CInt(CInt(oDbDataSetCoords.Tables(0).Rows(j).Item("PY")) / DivConstMax * ResizeConst)
                                End If
                                sVmlGraphicHotSpotCoords = sVmlGraphicHotSpotCoords & CStr(PX) & "," & CStr(PY) & " "

                            Next

                            sVmlGraphicHotSpotCoords = sVmlGraphicHotSpotCoords & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(oDbDataSetCoords.Tables(0).Rows.Count - 3).Item("PX")) / DivConstMax * ResizeConst)) & "," & CStr(CInt(CInt(oDbDataSetCoords.Tables(0).Rows(oDbDataSetCoords.Tables(0).Rows.Count - 3).Item("PY")) / DivConstMax * ResizeConst)) & " "
                            If Rbig > r Then
                                sVmlGraphicHotSpot = sVmlGraphicHotSpot & "<v:polyline points=""" & sVmlGraphicHotSpotCoords & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ href=""" & sHrefLinkPartKey & """ fillcolor=""black"" strokecolor=""#" & sVmlGraphicHotSpotColor & """ StrokeWeight=""" & sVmlGraphicHotSpotWeight & """><v:fill opacity=""0.0""/></v:polyline>" & vbCrLf
                            End If

                    End Select
                    sMapHtmlOutput = sMapHtmlOutput & "<AREA Shape=""" & sShape & """ id=""area_" & sPkeyLink & """ name=""" & sPLinkPartKey & "_" & sLinkName & """ onmouseover=""" & sOnMouseOverHTML & """ onmouseout=""" & sOnMouseOutHTML & """ COORDS=" & sCoords & " href=""" & sHrefLinkPartKey & """ > " & vbCrLf
                Catch ex As Exception
                    'error in hotspot....
                End Try
                oDbDataSetCoords.Dispose()
                oDbDataSetCoords = Nothing
            Next

            sMapHtmlOutput = sMapHtmlOutput & "</MAP><style>v\:* { behavior: url(#default#VML); }</style><xml:namespace ns=""urn:schemas-microsoft-com:vml"" prefix=""v""/>"

            If (sVmlGraphicHotSpot <> "") Then
                sMapHtmlOutput = sMapHtmlOutput & "<style>v\:* { behavior: url(#default#VML); }</style><xml:namespace ns=""urn:schemas-microsoft-com:vml"" prefix=""v""/><div id=""VmlDiv"" dir=""ltr"" style=""position:absolute;z-index:1"">" & sVmlGraphicHotSpot & "</div>" & vbCrLf & "<script language=""JavaScript"">window.onload=initVML;" & vbCrLf & "function initVML(){" & vbCrLf & "mySpanDim = getDim(document.getElementById(""ImageMapDIV""));VmlDiv.style.top = mySpanDim.y;VmlDiv.style.left = mySpanDim.x;" & vbCrLf & "}" & vbCrLf & "function getDim(el){" & vbCrLf & "for (var lx=0,ly=0;el!=null;" & vbCrLf & "lx+=el.offsetLeft,ly+=el.offsetTop,el=el.offsetParent);" & vbCrLf & "return {x:lx,y:ly}" & vbCrLf & "}" & vbCrLf & "</script>" & vbCrLf
            End If

            ' sMapHtmlOutput = sMapHtmlOutput & vbCrLf & "<script language=""JavaScript"">window.onload=initVML;" & vbCrLf & "function initVML(){" & vbCrLf & "mySpanDim = getDim(document.getElementById(""ImageMapDIV""));VmlDiv.style.top = mySpanDim.y;VmlDiv.style.left = mySpanDim.x;" & vbCrLf & "}" & vbCrLf & "function getDim(el){" & vbCrLf & "for (var lx=0,ly=0;el!=null;" & vbCrLf & "lx+=el.offsetLeft,ly+=el.offsetTop,el=el.offsetParent);" & vbCrLf & "return {x:lx,y:ly}" & vbCrLf & "}" & vbCrLf & "</script>" & vbCrLf
            '& "<SCRIPT FOR=window EVENT=onscroll>alert(1);initVML();</SCRIPT><SCRIPT FOR=window EVENT=onresizeend>alert(1);initVML();</SCRIPT><SCRIPT FOR=window EVENT=onmoveend>alert(1);initVML();</SCRIPT>"
        Else
            sMapHtmlOutput = "<MAP NAME=""map_" & sImageMapPkey & """>"
        End If

        oDbDataSetLinks.Dispose()
        oDbDataSetLinks = Nothing

        GenerateMap_HotSpots = sMapHtmlOutput

    End Function
	
    Function ConvertFromTagToColumnName(ByVal sTagName As String) As String

        'for new tags
        If InStr(sTagName, "PART PROP") Then
            sTagName = Mid(sTagName, InStr(sTagName, "=") + 1)
            Select Case sTagName
                Case "ITEM_ID"
                    ConvertFromTagToColumnName = "PKEY"
                Case "VENDOR_NUMBER"
                    ConvertFromTagToColumnName = "PKEY"
                Case "VENDOR_CODE"
                    ConvertFromTagToColumnName = "PKEY"
                Case "ITEM_OBJECT_TYPE"
                    ConvertFromTagToColumnName = "PTYPENAME"
                Case "ITEM_STATUS"
                    ConvertFromTagToColumnName = "PSTATUS"
                Case "PBALOONNUMBER"
                    ConvertFromTagToColumnName = "PBALOONNUMBER"
                Case "STATUSNAME"
                    ConvertFromTagToColumnName = "Status_Name"
                Case "PHEBDESC"
                    ConvertFromTagToColumnName = "PHEBDESC"
                Case "PENGDESC"
                    ConvertFromTagToColumnName = "PENGDESC"
                Case "PQTY"
                    ConvertFromTagToColumnName = "PQTY"
                Case "PURL"
                    ConvertFromTagToColumnName = "PURL"
                Case "PSOURCE"
                    ConvertFromTagToColumnName = "PSOURCE"
                Case "PREVISION"
                    ConvertFromTagToColumnName = "PREVISION"
                Case "PCREATEDATE"
                    ConvertFromTagToColumnName = "PCREATEDATE"
                Case "PUPDATEDATE"
                    ConvertFromTagToColumnName = "PUPDATEDATE"
                Case "PPUBLISH"
                    ConvertFromTagToColumnName = "PPUBLISH"
                Case "PKEY"
                    ConvertFromTagToColumnName = "PKEY"
                Case "PKEYCATALOG"
                    ConvertFromTagToColumnName = "PKEYCATALOG"
                Case Else
                    ConvertFromTagToColumnName = sTagName
            End Select
        ElseIf InStr(sTagName, "OCCUR PROP") Then
            sTagName = Mid(sTagName, InStr(sTagName, "=") + 1)
            Select Case sTagName
                Case "PPROP1"
                    ConvertFromTagToColumnName = "OCPROP1"
                Case "PPROP2"
                    ConvertFromTagToColumnName = "OCPROP2"
                Case "PPROP3"
                    ConvertFromTagToColumnName = "OCPROP3"
                Case "PPROP4"
                    ConvertFromTagToColumnName = "OCPROP4"
                Case "PPROP5"
                    ConvertFromTagToColumnName = "OCPROP5"
                Case "PPROP6"
                    ConvertFromTagToColumnName = "OCPROP6"
                Case "PPROP7"
                    ConvertFromTagToColumnName = "OCPROP7"
                Case "PPROP8"
                    ConvertFromTagToColumnName = "OCPROP8"
                Case "PPROP9"
                    ConvertFromTagToColumnName = "OCPROP9"
                Case "PPROP10"
                    ConvertFromTagToColumnName = "OCPROP10"
                Case "PPROP11"
                    ConvertFromTagToColumnName = "OCPROP11"
                Case "PPROP12"
                    ConvertFromTagToColumnName = "OCPROP12"
                Case "PPROP13"
                    ConvertFromTagToColumnName = "OCPROP13"
                Case "PPROP14"
                    ConvertFromTagToColumnName = "OCPROP14"
                Case "PPROP15"
                    ConvertFromTagToColumnName = "OCPROP15"
                Case "PPROP16"
                    ConvertFromTagToColumnName = "OCPROP16"
                Case "PPROP17"
                    ConvertFromTagToColumnName = "OCPROP17"
                Case "PPROP18"
                    ConvertFromTagToColumnName = "OCPROP18"
                Case "PPROP19"
                    ConvertFromTagToColumnName = "OCPROP19"
                Case "PPROP20"
                    ConvertFromTagToColumnName = "OCPROP20"
            End Select
        Else
            'for old tage
            Select Case sTagName
                'Part Tags
                Case "PART_DATA PROP_NUM=4"
                    ConvertFromTagToColumnName = "PQTY"
                Case "PART_DATA PROP_NUM=2"
                    ConvertFromTagToColumnName = "PHEBDESC"
                Case "PART_DATA PROP_NUM=6"
                    ConvertFromTagToColumnName = "PQTY"
                Case "PART_DATA PROP_NUM=5"
                    ConvertFromTagToColumnName = "PQTY"
                Case "PART_DATA PROP_NUM=1"
                    ConvertFromTagToColumnName = "PQTY"
                Case "PART_DATA PROP_NUM=0"
                    ConvertFromTagToColumnName = "PBALOONNUMBER"
                Case "PART_DATA PROP_NUM=7"
                    ConvertFromTagToColumnName = "PPROP1"
                Case "PART_DATA PROP_NUM=8"
                    ConvertFromTagToColumnName = "PPROP2"
                Case "PART_DATA PROP_NUM=9"
                    ConvertFromTagToColumnName = "PPROP3"
                Case "PART_DATA PROP_NUM=10"
                    ConvertFromTagToColumnName = "PPROP4"
                Case "PART_DATA PROP_NUM=11"
                    ConvertFromTagToColumnName = "PPROP5"
                Case "PART_DATA PROP_NUM=12"
                    ConvertFromTagToColumnName = "PPROP6"
                Case "PART_DATA PROP_NUM=13"
                    ConvertFromTagToColumnName = "PPROP7"
                Case "PART_DATA PROP_NUM=14"
                    ConvertFromTagToColumnName = "PPROP8"
                Case "PART_DATA PROP_NUM=15"
                    ConvertFromTagToColumnName = "PPROP9"
                Case "PART_DATA PROP_NUM=16"
                    ConvertFromTagToColumnName = "PPROP10"
                Case "PART_DATA PROP_NUM=31"
                    ConvertFromTagToColumnName = "PPROP11"
                Case "PART_DATA PROP_NUM=32"
                    ConvertFromTagToColumnName = "PPROP12"
                Case "PART_DATA PROP_NUM=33"
                    ConvertFromTagToColumnName = "PPROP13"
                Case "PART_DATA PROP_NUM=34"
                    ConvertFromTagToColumnName = "PPROP14"
                Case "PART_DATA PROP_NUM=35"
                    ConvertFromTagToColumnName = "PPROP15"
                Case "PART_DATA PROP_NUM=36"
                    ConvertFromTagToColumnName = "PPROP16"
                Case "PART_DATA PROP_NUM=37"
                    ConvertFromTagToColumnName = "PPROP17"
                Case "PART_DATA PROP_NUM=38"
                    ConvertFromTagToColumnName = "PPROP18"
                Case "PART_DATA PROP_NUM=39"
                    ConvertFromTagToColumnName = "PPROP19"
                Case "PART_DATA PROP_NUM=40"
                    ConvertFromTagToColumnName = "PPROP20"
                Case "PART_DATA PROP_NUM=44"
                    ConvertFromTagToColumnName = "PPROP21"
                Case "PART_DATA PROP_NUM=45"
                    ConvertFromTagToColumnName = "PPROP22"
                Case "PART_DATA PROP_NUM=46"
                    ConvertFromTagToColumnName = "PPROP23"
                Case "PART_DATA PROP_NUM=47"
                    ConvertFromTagToColumnName = "PPROP24"
                Case "PART_DATA PROP_NUM=48"
                    ConvertFromTagToColumnName = "PPROP25"
                Case "PART_DATA PROP_NUM=49"
                    ConvertFromTagToColumnName = "PPROP26"
                Case "PART_DATA PROP_NUM=50"
                    ConvertFromTagToColumnName = "PPROP27"
                Case "PART_DATA PROP_NUM=51"
                    ConvertFromTagToColumnName = "PPROP28"
                Case "PART_DATA PROP_NUM=52"
                    ConvertFromTagToColumnName = "PPROP29"
                Case "PART_DATA PROP_NUM=53"
                    ConvertFromTagToColumnName = "PPROP30"
                Case "PART_DATA PROP_NUM=54"
                    ConvertFromTagToColumnName = "PPROP31"
                Case "PART_DATA PROP_NUM=55"
                    ConvertFromTagToColumnName = "PPROP32"
                Case "PART_DATA PROP_NUM=56"
                    ConvertFromTagToColumnName = "PPROP33"
                Case "PART_DATA PROP_NUM=57"
                    ConvertFromTagToColumnName = "PPROP34"
                Case "PART_DATA PROP_NUM=58"
                    ConvertFromTagToColumnName = "PPROP35"
                Case "PART_DATA PROP_NUM=59"
                    ConvertFromTagToColumnName = "PPROP36"
                Case "PART_DATA PROP_NUM=60"
                    ConvertFromTagToColumnName = "PPROP37"
                Case "PART_DATA PROP_NUM=61"
                    ConvertFromTagToColumnName = "PPROP38"
                Case "PART_DATA PROP_NUM=62"
                    ConvertFromTagToColumnName = "PPROP39"
                Case "PART_DATA PROP_NUM=63"
                    ConvertFromTagToColumnName = "PPROP40"
                Case "PART_DATA PROP_NUM=64"
                    ConvertFromTagToColumnName = "PPROP41"
                Case "PART_DATA PROP_NUM=65"
                    ConvertFromTagToColumnName = "PPROP42"
                Case "PART_DATA PROP_NUM=66"
                    ConvertFromTagToColumnName = "PPROP43"
                Case "PART_DATA PROP_NUM=67"
                    ConvertFromTagToColumnName = "PPROP44"
                Case "PART_DATA PROP_NUM=68"
                    ConvertFromTagToColumnName = "PPROP45"
                Case "PART_DATA PROP_NUM=69"
                    ConvertFromTagToColumnName = "PPROP46"
                Case "PART_DATA PROP_NUM=70"
                    ConvertFromTagToColumnName = "PPROP47"
                Case "PART_DATA PROP_NUM=71"
                    ConvertFromTagToColumnName = "PPROP48"
                Case "PART_DATA PROP_NUM=72"
                    ConvertFromTagToColumnName = "PPROP49"
                Case "PART_DATA PROP_NUM=73"
                    ConvertFromTagToColumnName = "PPROP50"
                Case "PART_DATA PROP_NUM=74"
                    ConvertFromTagToColumnName = "PPROP51"
                Case "PART_DATA PROP_NUM=75"
                    ConvertFromTagToColumnName = "PPROP52"
                Case "PART_DATA PROP_NUM=76"
                    ConvertFromTagToColumnName = "PPROP53"
                Case "PART_DATA PROP_NUM=77"
                    ConvertFromTagToColumnName = "PPROP54"
                Case "PART_DATA PROP_NUM=78"
                    ConvertFromTagToColumnName = "PPROP55"
                Case "PART_DATA PROP_NUM=79"
                    ConvertFromTagToColumnName = "PPROP56"
                Case "PART_DATA PROP_NUM=80"
                    ConvertFromTagToColumnName = "PPROP57"
                Case "PART_DATA PROP_NUM=81"
                    ConvertFromTagToColumnName = "PPROP58"
                Case "PART_DATA PROP_NUM=82"
                    ConvertFromTagToColumnName = "PPROP59"
                Case "PART_DATA PROP_NUM=83"
                    ConvertFromTagToColumnName = "PPROP60"
                Case "PART_DATA PROP_NUM=84"
                    ConvertFromTagToColumnName = "PPROP61"
                Case "PART_DATA PROP_NUM=85"
                    ConvertFromTagToColumnName = "PPROP62"
                Case "PART_DATA PROP_NUM=86"
                    ConvertFromTagToColumnName = "PPROP63"
                Case "PART_DATA PROP_NUM=87"
                    ConvertFromTagToColumnName = "PPROP64"
                Case "PART_DATA PROP_NUM=88"
                    ConvertFromTagToColumnName = "PPROP65"
                Case "PART_DATA PROP_NUM=89"
                    ConvertFromTagToColumnName = "PPROP66"
                Case "PART_DATA PROP_NUM=90"
                    ConvertFromTagToColumnName = "PPROP67"
                Case "PART_DATA PROP_NUM=91"
                    ConvertFromTagToColumnName = "PPROP68"
                Case "PART_DATA PROP_NUM=92"
                    ConvertFromTagToColumnName = "PPROP69"
                Case "PART_DATA PROP_NUM=93"
                    ConvertFromTagToColumnName = "PPROP70"
                Case "PART_DATA PROP_NUM=94"
                    ConvertFromTagToColumnName = "PPROP71"
                Case "PART_DATA PROP_NUM=95"
                    ConvertFromTagToColumnName = "PPROP72"
                Case "PART_DATA PROP_NUM=96"
                    ConvertFromTagToColumnName = "PPROP73"
                Case "PART_DATA PROP_NUM=97"
                    ConvertFromTagToColumnName = "PPROP74"
                Case "PART_DATA PROP_NUM=98"
                    ConvertFromTagToColumnName = "PPROP75"
                Case "PART_DATA PROP_NUM=99"
                    ConvertFromTagToColumnName = "PPROP76"
                Case "PART_DATA PROP_NUM=100"
                    ConvertFromTagToColumnName = "PPROP77"
                Case "PART_DATA PROP_NUM=101"
                    ConvertFromTagToColumnName = "PPROP78"
                Case "PART_DATA PROP_NUM=102"
                    ConvertFromTagToColumnName = "PPROP79"
                Case "PART_DATA PROP_NUM=103"
                    ConvertFromTagToColumnName = "PPROP80"
                Case "PART_DATA PROP_NUM=104"
                    ConvertFromTagToColumnName = "PPROP81"
                Case "PART_DATA PROP_NUM=105"
                    ConvertFromTagToColumnName = "PPROP82"
                Case "PART_DATA PROP_NUM=106"
                    ConvertFromTagToColumnName = "PPROP83"
                Case "PART_DATA PROP_NUM=107"
                    ConvertFromTagToColumnName = "PPROP84"
                Case "PART_DATA PROP_NUM=108"
                    ConvertFromTagToColumnName = "PPROP85"
                Case "PART_DATA PROP_NUM=109"
                    ConvertFromTagToColumnName = "PPROP86"
                Case "PART_DATA PROP_NUM=110"
                    ConvertFromTagToColumnName = "PPROP87"
                Case "PART_DATA PROP_NUM=111"
                    ConvertFromTagToColumnName = "PPROP88"
                Case "PART_DATA PROP_NUM=112"
                    ConvertFromTagToColumnName = "PPROP89"
                Case "PART_DATA PROP_NUM=113"
                    ConvertFromTagToColumnName = "PPROP90"
                Case "PART_DATA PROP_NUM=114"
                    ConvertFromTagToColumnName = "PPROP91"
                Case "PART_DATA PROP_NUM=115"
                    ConvertFromTagToColumnName = "PPROP92"
                Case "PART_DATA PROP_NUM=116"
                    ConvertFromTagToColumnName = "PPROP93"
                Case "PART_DATA PROP_NUM=117"
                    ConvertFromTagToColumnName = "PPROP94"
                Case "PART_DATA PROP_NUM=118"
                    ConvertFromTagToColumnName = "PPROP95"
                Case "PART_DATA PROP_NUM=119"
                    ConvertFromTagToColumnName = "PPROP96"
                Case "PART_DATA PROP_NUM=120"
                    ConvertFromTagToColumnName = "PPROP97"
                Case "PART_DATA PROP_NUM=121"
                    ConvertFromTagToColumnName = "PPROP98"
                Case "PART_DATA PROP_NUM=122"
                    ConvertFromTagToColumnName = "PPROP99"
                Case "PART_DATA PROP_NUM=123"
                    ConvertFromTagToColumnName = "PPROP100"
                Case "PART_DATA PROP_NUM=124"
                    ConvertFromTagToColumnName = "PPROP101"
                Case "PART_DATA PROP_NUM=125"
                    ConvertFromTagToColumnName = "PPROP102"
                Case "PART_DATA PROP_NUM=126"
                    ConvertFromTagToColumnName = "PPROP103"
                Case "PART_DATA PROP_NUM=127"
                    ConvertFromTagToColumnName = "PPROP104"
                Case "PART_DATA PROP_NUM=128"
                    ConvertFromTagToColumnName = "PPROP105"
                Case "PART_DATA PROP_NUM=129"
                    ConvertFromTagToColumnName = "PPROP106"
                Case "PART_DATA PROP_NUM=130"
                    ConvertFromTagToColumnName = "PPROP107"
                Case "PART_DATA PROP_NUM=131"
                    ConvertFromTagToColumnName = "PPROP108"
                Case "PART_DATA PROP_NUM=132"
                    ConvertFromTagToColumnName = "PPROP109"
                Case "PART_DATA PROP_NUM=133"
                    ConvertFromTagToColumnName = "PPROP110"
                Case "PART_DATA PROP_NUM=41"
                    ConvertFromTagToColumnName = "PREVISION"
                Case "PART_DATA PROP_NUM=42"
                    ConvertFromTagToColumnName = "PKEYTYPE"
                Case "PART_DATA PROP_NUM=3"
                    ConvertFromTagToColumnName = "PENGDESC"
                Case "PART_DATA PROP_NUM=17"
                    ConvertFromTagToColumnName = "PNAMAITEMID"
                Case "PART_DATA PROP_NUM=18"
                    ConvertFromTagToColumnName = "PCONFIGITEMID"
                Case "PART_DATA PROP_NUM=19"
                    ConvertFromTagToColumnName = "PTEKEN"
                Case "PART_DATA PROP_NUM=20"
                    ConvertFromTagToColumnName = "PREPAIRINGLEVEL"
                Case "PART_DATA PROP_NUM=21"
                    ConvertFromTagToColumnName = "PNAMAPARTNAME"
                Case "PART_DATA PROP_NUM=22"
                    ConvertFromTagToColumnName = "PTOOLID"
                Case "PART_DATA PROP_NUM=23"
                    ConvertFromTagToColumnName = "PNAMAREV"
                Case "PART_DATA PROP_NUM=24"
                    ConvertFromTagToColumnName = "PHARKAVA"
                Case "PART_DATA PROP_NUM=25"
                    ConvertFromTagToColumnName = "PKEY"
                Case "PART_DATA PROP_NUM=30"
                    ConvertFromTagToColumnName = "PNAMAREV"
                Case "PART_DATA PROP_NUM=43"
                    ConvertFromTagToColumnName = "PHARKAVA"
                Case "ITEMOBJECTTYPE"
                    ConvertFromTagToColumnName = "PTYPENAME"
                Case "OwningUser"
                    ConvertFromTagToColumnName = "PKEYUSER"
                Case "TEMPLATENAME"
                    ConvertFromTagToColumnName = "PPARTTEMPLATE"
                    'document tags
                Case "DOCUMENTPATH"
                    ConvertFromTagToColumnName = "PORGFILENAME"
                Case "DOCUMENTNAME", "OBJECT_NAME"
                    ConvertFromTagToColumnName = "PHEBDESC"
                Case "DOCUMENTDESCRIPTION"
                    ConvertFromTagToColumnName = "PENGDESC"
                Case "DOCUMENTREVISION"
                    ConvertFromTagToColumnName = "PREVISION"
                Case "DOCUMENTID"
                    ConvertFromTagToColumnName = "PDOCNUMBER"
                Case "DOCUMENTNUMBER"
                    ConvertFromTagToColumnName = "PDOCID"
                Case "DOCUMENTSUFFIX"
                    ConvertFromTagToColumnName = "PSUFFIX"
                Case "DOCUMENTCREATEDATE"
                    ConvertFromTagToColumnName = "PCREATEDATE"
                Case "DOCUMENTFILECREATEDATE"
                    ConvertFromTagToColumnName = "PFILECREATEDDATE"
                Case "DOCUMENTAUTHOR"
                    ConvertFromTagToColumnName = "PKEYUSER"
                Case "PFILEEXTENSION"
                    ConvertFromTagToColumnName = "PFILEEXTENSION"
                Case "PLINKTONETWORKFILE"
                    ConvertFromTagToColumnName = "PLINKTONETWORKFILE"
                Case "PSTATUS"
                    ConvertFromTagToColumnName = "PSTATUS"
                Case "DOCUMENTMODIFIEDATE"
                    ConvertFromTagToColumnName = "PUPDATEDATE"
                Case "DOCUMENTFILEMODIFIEDDATE"
                    ConvertFromTagToColumnName = "PFILEMODIFIEDDATE"
                Case "DOCUMENTMODIFIERNAME"
                    ConvertFromTagToColumnName = "PKEYUSER"
                Case "DOCUMENTSYSTEMID"
                    ConvertFromTagToColumnName = "PKEY"
                Case "DOCUMENTPROP1"
                    ConvertFromTagToColumnName = "PPROP1"
                Case "DOCUMENTPROP2"
                    ConvertFromTagToColumnName = "PPROP2"
                Case "DOCUMENTPROP3"
                    ConvertFromTagToColumnName = "PPROP3"
                Case "DOCUMENTPROP4"
                    ConvertFromTagToColumnName = "PPROP4"
                Case "DOCUMENTPROP5"
                    ConvertFromTagToColumnName = "PPROP5"
                Case "DOCUMENTPROP6"
                    ConvertFromTagToColumnName = "PPROP6"
                Case "DOCUMENTPROP7"
                    ConvertFromTagToColumnName = "PPROP7"
                Case "DOCUMENTPROP8"
                    ConvertFromTagToColumnName = "PPROP8"
                Case "DOCUMENTPROP9"
                    ConvertFromTagToColumnName = "PPROP9"
                Case "DOCUMENTPROP10"
                    ConvertFromTagToColumnName = "PPROP10"
                Case "DOCUMENTSTATUS"
                    ConvertFromTagToColumnName = "PSTATUS"
                Case "DOCUMENTDESCRIPTION", "OBJECT_DESC"
                    ConvertFromTagToColumnName = "PENGDESC"
                Case "DOCUMENTOBJECTTYPE"
                    ConvertFromTagToColumnName = "PTYPENAME"
                Case "DOCUMENTCHECKOUTUSER"
                    ConvertFromTagToColumnName = "PCHECKOUTUSER"
                Case "DOCUMENTCOMMENT"
                    ConvertFromTagToColumnName = "PCOMMENT"
                Case "PORGFILENAME"
                    ConvertFromTagToColumnName = "PORGFILENAME"
                Case "PPUBLISH"
                    ConvertFromTagToColumnName = "PPUBLISH"
                Case "PMULTIPLEREFERENCES"
                    ConvertFromTagToColumnName = "PMULTIPLEREFERENCES"
                Case "STATUSNAME"
                    ConvertFromTagToColumnName = "Status_Name"
                Case "PKEYCATALOG"
                    ConvertFromTagToColumnName = "PKEYCATALOG"
                Case "PKEYPAGE"
                    ConvertFromTagToColumnName = "PKEYPAGE"
                Case "PDOCPATH"
                    ConvertFromTagToColumnName = "PDOCPATH"
                    'catalog tags
                Case " CATALOG_NAME "
                    ConvertFromTagToColumnName = "PHEBDESC"
                Case " REVISION "
                    ConvertFromTagToColumnName = "PREVISION"
                Case "PAGE_HEB_DESC"
                    ConvertFromTagToColumnName = "PHEBDESC"
                    'header tags
                Case "CATALOG_NAME"
                    ConvertFromTagToColumnName = "Catalog_Name"
                Case "PAGE_NUM"
                    ConvertFromTagToColumnName = "PPAGENUMBER"
                Case "CHAPTER_NAME"
                    ConvertFromTagToColumnName = "FOLDERNAME"
                Case "NEXT"
                    ConvertFromTagToColumnName = "PNEXT"
                Case "PREV"
                    ConvertFromTagToColumnName = "PPREV"
                Case "PUPDATEDATE"
                    ConvertFromTagToColumnName = "PUPDATEDATE"
                    'catalog loop tags:
                    'Case "CATALOG_NAME"
                    '    ConvertFromTagToColumnName = "PHEBDESC"
                Case "CATALOG_MAKAT"
                    ConvertFromTagToColumnName = "PCATALOGMAKAT"

                Case Else
                    ConvertFromTagToColumnName = "PKEY"
            End Select
        End If


    End Function

    Function WriteIndexFrame(ByVal pkey As String, ByVal dbDatasetRelation As Data.DataSet, ByVal pObjectTypeTable As String, ByVal level As Integer, Optional ByVal sItemProp As String = "")

        '<Name>Customer</Name>
        '<ID>TSGa4885</ID>
        '<Level>2</Level>
        '<ObjectClass>Item</ObjectClass>
        '<Type>Folder</Type>
        '<ItemProp></ItemProp>
        '<ObjType></ObjType>
        '<Template></Template>

        'SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS
        'FROM T_CAT_PAGE,T_CAT_NODES 
        'WHERE (((T_CAT_PAGE.PKEYCATALOG)='LWa1066') AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY)
        'union
        'SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS
        'FROM T_CAT_PART,T_CAT_NODES
        'WHERE (((T_CAT_PART.PKEYCATALOG)='LWa1066') AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY)
        'union
        'SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS
        'FROM T_CAT_FOLDER, T_CAT_NODES
        'WHERE (((T_CAT_FOLDER.PKEYCATALOG)='LWa1066') AND T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY)
        'union
        'SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS
        'FROM T_CAT_CATALOG ,T_CAT_NODES 
        'WHERE (((T_CAT_CATALOG.PKEY)='LWa1066') and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)


        Dim dTotal As Double
        Dim dStartDate As Date
        dStartDate = Now()
        'dTotal = New Timer())

        Dim sSqlQuery As String
        Dim sOutput As String

        'Dim dbDataSetDetails As New DataSet
        'Dim dbNodesDetails As New DataSet
        Dim oDRow() As Data.DataRow

        Dim sName As String
        Dim sID As String
        Dim sLevel As String
        Dim sObjectClass As String
        Dim sType As String
        Dim sItemPropValue As String
        Dim sTemplate As String
        Dim sXmlNode As String
        Dim sOutputXml As String
        Dim sSpace As Integer
        Dim sSpaces As String = ""
        Dim sPageNum As String = ""
        'Dim strChilds As String = ""



        'must return only one record(pkey)
        oDRow = dbDatasetRelation.Tables(0).Select("PKEY = '" & pkey & "'")

        If (oDRow.Length > 0) Then
            If (Not IsDBNull(oDRow(0).Item(ConvertFromTagToColumnName("PART_DATA PROP_NUM=2")))) Then
                sName = oDRow(0).Item(ConvertFromTagToColumnName("PART_DATA PROP_NUM=2"))
            Else
                sName = ""
            End If
        Else
            sName = ""
        End If
        If (oDRow.Length > 0) Then
            sID = oDRow(0).Item(ConvertFromTagToColumnName("PART_DATA PROP_NUM=25"))
            sLevel = level
            Select Case pObjectTypeTable
                Case oLWObjectTypes.emCatalog
                    sObjectClass = "Item"
                Case oLWObjectTypes.emPage
                    sObjectClass = "Page"
                Case oLWObjectTypes.emPart
                    sObjectClass = "Item"
                Case oLWObjectTypes.emPicture
                    sObjectClass = "Picture"
                Case oLWObjectTypes.emWinObject
                    sObjectClass = "WinObject"
                Case oLWObjectTypes.emFolder
                    sObjectClass = "Folder"

            End Select
        Else
            sObjectClass = ""
        End If
        If pObjectTypeTable = oLWObjectTypes.emPart Then
            If Not IsDBNull(oDRow(0).Item("PTYPENAME")) Then
                sType = oDRow(0).Item("PTYPENAME")
            Else
                sType = "Item"
            End If
        ElseIf pObjectTypeTable = oLWObjectTypes.emPage Then
            sType = "Page"
            If Not IsDBNull(oDRow(0).Item("PPAGENUMBER")) Then
                sPageNum = oDRow(0).Item("PPAGENUMBER")
                'strChilds =  strChilds & 
            End If
        Else
            sType = "Folder"
        End If

        If pObjectTypeTable = oLWObjectTypes.emPart Then
            sTemplate = oDRow(0).Item("PPARTTEMPLATE") & ""
            If sItemProp <> "" Then
                If Not IsDBNull(oDRow(0).Item(sItemProp)) Then
                    sItemPropValue = oDRow(0).Item(sItemProp)
                Else
                    sItemPropValue = ""
                End If
            Else
                sItemPropValue = ""
            End If
        ElseIf pObjectTypeTable = oLWObjectTypes.emPage Then
            sTemplate = oDRow(0).Item("PPARTTEMPLATE") & ""
            sItemPropValue = ""
        Else
            sTemplate = ""
            sItemPropValue = ""
        End If

        For sSpace = 1 To level * 10
            sSpaces = sSpaces & " "
        Next
        Dim SParentKey As String = ""
        If (oDRow.Length > 0) Then
            SParentKey = oDRow(0).Item("PPARENTKEY")
        End If


        sXmlNode = sSpaces & "<Name><![CDATA[" & sName & "]]></Name>" & vbCrLf _
                & sSpaces & "<PageNum>" & sPageNum & "</PageNum>" & vbCrLf _
                & sSpaces & "<ID>" & sID & "</ID>" & vbCrLf _
                & sSpaces & "<Level>" & sLevel & "</Level>" & vbCrLf _
                & sSpaces & "<ObjectClass>" & sObjectClass & "</ObjectClass>" & vbCrLf _
                & sSpaces & "<Type>" & sType & "</Type>" & vbCrLf _
                & sSpaces & "<Template>" & sTemplate & "</Template>" & vbCrLf _
                & sSpaces & "<ObjType>" & pObjectTypeTable & "</ObjType>" & vbCrLf _
                & sSpaces & "<ItemProp>" & sItemPropValue & "</ItemProp>" & vbCrLf _
                & sSpaces & "<ParentKey>" & SParentKey & "</ParentKey>" & vbCrLf _
 & "<StartTime>" & dStartDate & "</StartTime>" & vbCrLf & "<FinishTime>" & Now() & "</FinishTime>"
        '& sSpaces & "<TotalTime>" & (Timer() - dTotal) & "</TotalTime>" & vbCrLf

        sOutputXml = vbCrLf & sSpaces & "<Object>" & vbCrLf & sXmlNode

        'ARMY FIX - if object type is page - not needed to output the childs of page to the index.xml
        If sObjectClass <> "Page" Then
            'nodes - for each child
            'sSqlQuery = "select * from " & oLWObjectTypes.emNodesTable & " where pparentkey='" & pkey & "' and pobjecttype<>5 and pobjecttype<>3 and pobjecttype<>18 and pobjecttype<>6 " & " AND PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted
            'oConn.Open_Dataset(sSqlQuery, dbNodesDetails)


            'check if we need to put the childs in sort order.
            Dim sPkeyChilds As String
            Dim aPkeyChilds() As String
            Dim aPkeyChildsAlreadyOutput As New ArrayList
            Dim iPkeyChildsAlreadyOutput As Integer
            iPkeyChildsAlreadyOutput = 0
            aPkeyChildsAlreadyOutput.Clear()

            If ((oDRow.Length > 0) AndAlso (Not IsDBNull(oDRow(0).Item("PKEYCHILDS")))) Then
                If Len(oDRow(0).Item("PKEYCHILDS")) > 0 Then
                    sPkeyChilds = oDRow(0).Item("PKEYCHILDS")
                    aPkeyChilds = Split(sPkeyChilds, "|")
                End If
            End If

            'can return more than one record(pkey)
            oDRow = dbDatasetRelation.Tables(0).Select("PPARENTKEY = '" & pkey & "'")

            Dim iRow As Integer
            Dim iRow2 As Integer
            Dim iMaxChildsAlreadyOutput As Integer

            Dim bFound As Boolean

            If IsNothing(aPkeyChilds) Then
                For iRow = 0 To oDRow.Length - 1
                    sOutputXml = sOutputXml & WriteIndexFrame(oDRow(iRow).Item("PKEY"), dbDatasetRelation, oDRow(iRow).Item("POBJECTTYPE"), level + 1, sItemProp) & vbCrLf
                Next
            Else
                For iRow2 = 0 To aPkeyChilds.Length - 1
                    For iRow = 0 To oDRow.Length - 1
                        If aPkeyChilds(iRow2) = oDRow(iRow).Item("PKEY") Then
                            sOutputXml = sOutputXml & WriteIndexFrame(oDRow(iRow).Item("PKEY"), dbDatasetRelation, oDRow(iRow).Item("POBJECTTYPE"), level + 1, sItemProp) & vbCrLf
                            'ReDim Preserve aPkeyChildsAlreadyOutput(iPkeyChildsAlreadyOutput)
                            aPkeyChildsAlreadyOutput.Add(oDRow(iRow).Item("PKEY"))
                            iPkeyChildsAlreadyOutput += 1
                            Exit For
                        End If
                    Next
                Next

                If IsNothing(aPkeyChildsAlreadyOutput) Then
                    iMaxChildsAlreadyOutput = 0
                Else
                    iMaxChildsAlreadyOutput = aPkeyChildsAlreadyOutput.Count
                End If

                'for each child that not exist in pkeychilds.
                REM For iRow = 0 To oDRow.Length - 1
                    REM bFound = False
                    REM For iRow2 = 0 To iMaxChildsAlreadyOutput - 1
                        REM If aPkeyChildsAlreadyOutput(iRow2) = oDRow(iRow).Item("PKEY") Then
                            REM bFound = True
                            REM Exit For
                        REM End If
                    REM Next
                    REM If Not bFound Then
                        REM sOutputXml = sOutputXml & WriteIndexFrame(oDRow(iRow).Item("PKEY"), dbDatasetRelation, oDRow(iRow).Item("POBJECTTYPE"), level + 1, sItemProp) & vbCrLf
                    REM End If
                REM Next

            End If

            'dbNodesDetails.Dispose()
            'dbNodesDetails = Nothing
            aPkeyChildsAlreadyOutput.Clear()
            aPkeyChildsAlreadyOutput = Nothing

        End If

        sOutputXml = sOutputXml & sSpaces & "</Object>"

        WriteIndexFrame = sOutputXml

    End Function

    Function WriteIndexFrameDmc(ByVal sPkey As String, ByVal dbDatasetRelation As Data.DataSet, ByVal pObjectTypeTable As String, ByVal level As Integer, ByVal oConn As CConnection, Optional ByVal sItemProp As String = "") As String



        Dim sSqlQuery As String
        Dim sOutput As String
        'Dim dbDataSetDetails As New DataSet
        'Dim dbNodesDetails As New DataSet
        Dim oDRow As Data.DataRowCollection
        Dim oLWObjectTypes As New LWObjectTypes
        Dim sLevel As String
        Dim sObjectClass As String
        Dim sType As String
        Dim sItemPropValue As String
        Dim sTemplate As String
        Dim sOutputXml As String
        Dim sSpace As Integer
        Dim dbDatasetSubNodes As Data.DataSet
        Dim dbDatasetChilds As Data.DataSet
        Dim oDRowChilds As Data.DataRowCollection
        Dim sSqlQueryRelations As String
        Dim sSqlQuerySubNodes As String
        Dim sSqlQuerySortChilds As String

        Dim sItemProp2 As String = ""

        Dim pPrefs As New PersonalPref(Me.sODBCName)
        Dim dbPrefset As New Data.DataSet
        dbPrefset = pPrefs.GetPrefsByObject("GROUP", "Everyone", Me.SCatalog, "", "", "", "", "", "PropToShowInIndex", "ITEMPROP")
        If dbPrefset.Tables(0).Rows.Count > 0 Then
            sItemProp2 = dbPrefset.Tables(0).Rows(0)("PREFVALUE") & ""
        End If


        If (sItemProp = "") And (sItemProp2 = "") Then

            sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE, T_CAT_PAGE.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                                    & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE, T_CAT_PART.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                                    & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                                    & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                                    & "union " _
                                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds  " _
                                                    & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                                    & "WHERE (T_CAT_CATALOG.PKEY ='" & sPkey & ")' and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                                    & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted

            sSqlQuerySubNodes = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE,T_CAT_PAGE.PKEYCHILDS AS PkeyChilds " _
                                & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'0' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                & "union " _
                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE ,T_CAT_PART.PKEYCHILDS AS PkeyChilds " _
                                & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'0' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                & "union " _
                                & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'0' OR T_CAT_FOLDER.PPUBLISH IS NULL )) " _
                                & "WHERE PGLOBALSTATUS<>4"



        ElseIf (sItemProp2 <> "") And (sItemProp <> "") Then
            sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE,'' as " & sItemProp & ",'' as " & sItemProp2 & ", T_CAT_PAGE.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE,T_CAT_PART." & sItemProp & ",T_CAT_PART." & sItemProp2 & ", T_CAT_PART.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp & ",'' as " & sItemProp2 & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp & ",''as " & sItemProp2 & ", T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                            & "WHERE (T_CAT_CATALOG.PKEY ='" & sPkey & ")' and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                            & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted

            sSqlQuerySubNodes = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE,'' as " & sItemProp & ",'' as " & sItemProp2 & " ,T_CAT_PAGE.PKEYCHILDS AS PkeyChilds " _
                               & "FROM T_CAT_PAGE,T_CAT_NODES " _
                               & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'0' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                               & "union " _
                               & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE ,T_CAT_PART." & sItemProp & ",T_CAT_PART." & sItemProp2 & " ,T_CAT_PART.PKEYCHILDS AS PkeyChilds " _
                               & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                               & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'0' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                               & "union " _
                               & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp & ",'' as " & sItemProp2 & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                               & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                               & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'0' OR T_CAT_FOLDER.PPUBLISH IS NULL )) " _
                               & "WHERE PGLOBALSTATUS<>4"


        ElseIf (sItemProp2 = "") Then
            sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE,'' as " & sItemProp & ", T_CAT_PAGE.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE,T_CAT_PART." & sItemProp & ", T_CAT_PART.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp & ", T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                            & "WHERE (T_CAT_CATALOG.PKEY ='" & sPkey & ")' and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                            & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted

            sSqlQuerySubNodes = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE,'' as " & sItemProp & ",T_CAT_PAGE.PKEYCHILDS AS PkeyChilds " _
                                    & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                    & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'0' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                    & "union " _
                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE,T_CAT_PART." & sItemProp & ",T_CAT_PART.PKEYCHILDS AS PkeyChilds " _
                                    & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                    & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'0' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                    & "union " _
                                    & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                    & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                    & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'0' OR T_CAT_FOLDER.PPUBLISH IS NULL )) " _
                                    & "WHERE PGLOBALSTATUS<>4"



        ElseIf (sItemProp = "") Then

            sSqlQueryRelations = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE,'' as " & sItemProp2 & ", T_CAT_PAGE.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_PAGE,T_CAT_NODES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE,T_CAT_PART." & sItemProp2 & ", T_CAT_PART.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp2 & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                                            & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL ) " _
                                            & "union " _
                                            & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_CATALOG.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp2 & ", T_CAT_CATALOG.PKEYCHILDS AS PkeyChilds  " _
                                            & "FROM T_CAT_CATALOG ,T_CAT_NODES " _
                                            & "WHERE (T_CAT_CATALOG.PKEY ='" & sPkey & ")' and T_CAT_CATALOG.PKEY = T_CAT_NODES.PKEY)) " _
                                            & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted

            sSqlQuerySubNodes = "SELECT * FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, T_CAT_PAGE.PPAGETEMPLATE as PPARTTEMPLATE,'' as " & sItemProp2 & " ,T_CAT_PAGE.PKEYCHILDS AS PkeyChilds " _
                               & "FROM T_CAT_PAGE,T_CAT_NODES " _
                               & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'0' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
                               & "union " _
                               & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE,T_CAT_PART." & sItemProp2 & " ,T_CAT_PART.PKEYCHILDS AS PkeyChilds " _
                               & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
                               & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'0' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
                               & "union " _
                               & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE,'' as " & sItemProp2 & ", T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
                               & "FROM T_CAT_FOLDER, T_CAT_NODES " _
                               & "WHERE (T_CAT_NODES.pkey in (select pkey from t_cat_nodes where pparentkey in (select pkey from t_cat_nodes where pparentkey= '" & sPkey & "' )) AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'0' OR T_CAT_FOLDER.PPUBLISH IS NULL )) " _
                               & "WHERE PGLOBALSTATUS<>4"


        End If

        ''sSqlQuerySubNodes = "select pparentkey,count(pparentkey) from t_cat_nodes where pobjecttype = 10 and pglobalstatus <> 4 and pparentkey in(SELECT pkey FROM (SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PAGE.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_PAGE.PKEYCHILDS AS PkeyChilds  " _
        ''                                        & "FROM T_CAT_PAGE,T_CAT_NODES " _
        ''                                        & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_PAGE.PKEY = T_CAT_NODES.PKEY AND pglobalstatus<>4 and pobjecttype=10) AND (T_CAT_PAGE.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PAGE.PPUBLISH IS NULL ) " _
        ''                                        & "union " _
        ''                                        & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_PART.PHEBDESC ,T_CAT_OBJECTTYPES.PTYPENAME,T_CAT_PART.PPARTTEMPLATE, T_CAT_PART.PKEYCHILDS AS PkeyChilds  " _
        ''                                        & "FROM T_CAT_PART,T_CAT_NODES, T_CAT_OBJECTTYPES " _
        ''                                        & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND pglobalstatus<>4 and pobjecttype=10 ) AND T_CAT_PART.PKEY = T_CAT_NODES.PKEY AND T_CAT_PART.PKEYTYPE=T_CAT_OBJECTTYPES.PKEY(+) AND (T_CAT_PART.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_PART.PPUBLISH IS NULL ) AND (T_CAT_OBJECTTYPES.PTYPENAME<>'GeneralLink' OR T_CAT_OBJECTTYPES.PTYPENAME IS NULL) " _
        ''                                        & "union " _
        ''                                        & "SELECT T_CAT_NODES.PKEY, T_CAT_NODES.PPARENTKEY, T_CAT_NODES.POBJECTTYPE, T_CAT_NODES.PGLOBALSTATUS,T_CAT_FOLDER.PHEBDESC ,  '' as PTYPENAME, '' as PPARTTEMPLATE, T_CAT_FOLDER.PKEYCHILDS AS PkeyChilds  " _
        ''                                        & "FROM T_CAT_FOLDER, T_CAT_NODES " _
        ''                                        & "WHERE (T_CAT_NODES.PPARENTKEY = '" & sPkey & "' AND T_CAT_NODES.pglobalstatus<>4 and T_CAT_NODES.pobjecttype=10) AND (T_CAT_FOLDER.PKEY = T_CAT_NODES.PKEY) AND (T_CAT_FOLDER.PPUBLISH<>'" & oLWObjectTypes.emNo & "' OR T_CAT_FOLDER.PPUBLISH IS NULL )) " _
        ''                                        & "WHERE PGLOBALSTATUS<>" & oLWObjectTypes.emDeleted & ") group by pparentkey"



        'Open command



        sSqlQuerySortChilds = "select T_CAT_CATALOG.PKEYCHILDS from T_CAT_CATALOG where PKEY = '" & sPkey & "'"


        dbDatasetSubNodes = New Data.DataSet
        dbDatasetChilds = New Data.DataSet

        oConn.Open_Dataset(sSqlQueryRelations, dbDatasetRelation)
        oConn.Open_Dataset(sSqlQuerySubNodes, dbDatasetSubNodes)
        oConn.Open_Dataset(sSqlQuerySortChilds, dbDatasetChilds)

        oDRow = dbDatasetRelation.Tables(0).Rows
        oDRowChilds = dbDatasetChilds.Tables(0).Rows

        sLevel = level + 1

        Dim sPkeyChilds As String
        Dim aPkeyChilds() As String
        Dim aPkeyChildsAlreadyOutput As New ArrayList
        Dim iPkeyChildsAlreadyOutput As Integer
        iPkeyChildsAlreadyOutput = 0
        aPkeyChildsAlreadyOutput.Clear()

        If Not IsDBNull(oDRowChilds(0).Item("PKEYCHILDS")) Then
            If Len(oDRowChilds(0).Item("PKEYCHILDS")) > 0 Then
                sPkeyChilds = oDRowChilds(0).Item("PKEYCHILDS")
                aPkeyChilds = Split(sPkeyChilds, "|")
            End If

        End If

        'sOutputXml = "<?xml version=""1.0"" encoding=""UTF-8""?>" & vbCrLf & vbCrLf & "<TreeIndex>" & vbCrLf
        Dim iRow As Integer
        Dim iRow2 As Integer
        Dim iMaxChildsAlreadyOutput As Integer

        Dim bFound As Boolean

        If IsNothing(aPkeyChilds) Then
            For iRow = 0 To oDRow.Count - 1
                sOutputXml = sOutputXml & InnerWriteDmc(oDRow(iRow), sLevel, pObjectTypeTable, dbDatasetSubNodes, sItemProp, sItemProp2) & vbCrLf
            Next
        Else

            For iRow2 = 0 To aPkeyChilds.Length - 1
                For iRow = 0 To oDRow.Count - 1
                    If aPkeyChilds(iRow2) = oDRow(iRow).Item("PKEY") Then
                        sOutputXml = sOutputXml & InnerWriteDmc(oDRow(iRow), sLevel, pObjectTypeTable, dbDatasetSubNodes, sItemProp, sItemProp2) & vbCrLf
                        'ReDim Preserve aPkeyChildsAlreadyOutput(iPkeyChildsAlreadyOutput)
                        aPkeyChildsAlreadyOutput.Add(oDRow(iRow).Item("PKEY"))
                        iPkeyChildsAlreadyOutput += 1
                        Exit For
                    End If
                Next
            Next



            If IsNothing(aPkeyChildsAlreadyOutput) Then
                iMaxChildsAlreadyOutput = 0
            Else
                iMaxChildsAlreadyOutput = aPkeyChildsAlreadyOutput.Count
            End If

            'for each child that not exist in pkeychilds.

            For iRow = 0 To oDRow.Count - 1
                bFound = False
                For iRow2 = 0 To iMaxChildsAlreadyOutput - 1
                    If aPkeyChildsAlreadyOutput(iRow2) = oDRow(iRow).Item("PKEY") Then
                        bFound = True
                        Exit For
                    End If
                Next
                If Not bFound Then
                    sOutputXml = sOutputXml & InnerWriteDmc(oDRow(iRow), sLevel, pObjectTypeTable, dbDatasetSubNodes, sItemProp, sItemProp2) & vbCrLf
                End If
            Next
        End If

        sOutputXml = sOutputXml & vbCrLf

        WriteIndexFrameDmc = sOutputXml



    End Function

    Function InnerWriteDmc(ByVal CurKey As Data.DataRow, ByVal CurLev As String, ByVal CurType As String, ByVal dbDatasetSubNodes As Data.DataSet, ByVal sItemProp As String, ByVal sItemProp2 As String) As String



        Dim sSpaces As String = ""

        Dim sName As String

        Dim sID As String



        Dim dTotal As Double

        Dim dStartDate As Date

        dStartDate = Now()

        'dTotal = Timer()



        Dim oLWObjectTypes As New LWObjectTypes

        Dim sObjectClass As String

        Dim sType As String

        Dim sItemPropValue As String
        Dim sItemPropValue2 As String

        Dim sTemplate As String

        Dim SubNodes As Boolean = False



        Dim sXmlNode As String

        Dim sXmlNodeSon As String

        Dim sOutputXml As String = ""



        If Not IsDBNull(CurKey.Item(ConvertFromTagToColumnName("PART_DATA PROP_NUM=2"))) Then

            sName = CurKey.Item(ConvertFromTagToColumnName("PART_DATA PROP_NUM=2"))

        Else

            sName = ""

        End If



        sID = CurKey.Item(ConvertFromTagToColumnName("PART_DATA PROP_NUM=25"))





        Select Case CurType

            Case oLWObjectTypes.emCatalog

                sObjectClass = "Item"

            Case oLWObjectTypes.emPage

                sObjectClass = "Page"

            Case oLWObjectTypes.emPart

                sObjectClass = "Item"

            Case oLWObjectTypes.emPicture

                sObjectClass = "Picture"

            Case oLWObjectTypes.emWinObject

                sObjectClass = "WinObject"

            Case oLWObjectTypes.emFolder

                sObjectClass = "Folder"

        End Select



        If CurType = oLWObjectTypes.emPart Then

            If Not IsDBNull(CurKey.Item("PTYPENAME")) Then

                sType = CurKey.Item("PTYPENAME")

            Else

                sType = "Item"

            End If

        ElseIf CurType = oLWObjectTypes.emPage Then

            sType = "Page"

        Else

            sType = "Folder"

        End If



        If CurType = oLWObjectTypes.emPart Then

            sTemplate = CurKey.Item("PPARTTEMPLATE") & ""

            If sItemProp <> "" Then

                If Not IsDBNull(CurKey.Item(sItemProp)) Then

                    sItemPropValue = CurKey.Item(sItemProp)

                Else

                    sItemPropValue = ""

                End If

            ElseIf CurType = oLWObjectTypes.emPart Then

                sTemplate = CurKey.Item("PPARTTEMPLATE") & ""
                sItemPropValue = ""

            Else

                sItemPropValue = ""

            End If
            If sItemProp2 <> "" Then

                If Not IsDBNull(CurKey.Item(sItemProp2)) Then

                    sItemPropValue2 = CurKey.Item(sItemProp2)

                Else

                    sItemPropValue2 = ""

                End If

            Else

                sItemPropValue2 = ""

            End If

        ElseIf CurType = oLWObjectTypes.emPage Then

            sTemplate = CurKey.Item("PPARTTEMPLATE") & ""

            sItemPropValue = ""

            sItemPropValue2 = ""

        Else

            sTemplate = ""

            sItemPropValue = ""

            sItemPropValue2 = ""

        End If

        Dim sPropCheck As String
        Dim sPropValueCheck As String
        Dim sCurPropValue As String
        Dim sPkeyType As String = ConvertFromTypeNameToPKey(sType)
        Dim bShowInIndex As Boolean = True
        Dim dbPrefSet As New Data.DataSet
        Dim pPrefs As New PersonalPref(Me.sODBCName)

        dbPrefSet = pPrefs.GetPrefs("USER", sAuthentication, Me.SCatalog, CurType, sPkeyType, "", "", "", "ShowInIndex", "INDEXFILTER")
        If dbPrefSet.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To dbPrefSet.Tables(0).Rows.Count - 1
                sPropCheck = dbPrefSet.Tables(0).Rows(i)("PROP") & ""
                sPropValueCheck = dbPrefSet.Tables(0).Rows(i)("PROPVALUE") & ""
                Select Case sPropCheck
                    Case "ItemProp"
                        sCurPropValue = sItemPropValue
                    Case "ItemProp2"
                        sCurPropValue = sItemPropValue2
                    Case Else
                        sCurPropValue = CurKey(sPropCheck) & ""
                End Select

                If sCurPropValue = sPropValueCheck Then
                    bShowInIndex = False
                    Exit For
                End If
            Next
        End If


        If bShowInIndex = True Then
            Dim nRow As Data.DataRow() = dbDatasetSubNodes.Tables(0).Select("pparentkey = '" & sID & "'")
            Dim j As Integer
            Dim bShowChilds As Boolean = False
            Dim sPkeyTypeChild As String
            Dim sCurTypeChild As String
            If nRow.Length > 0 Then
                bShowChilds = True
                For j = 0 To nRow.Length - 1
                    sPkeyTypeChild = ConvertFromTypeNameToPKey(nRow(j)("PTYPENAME") & "")
                    sCurTypeChild = nRow(j)("POBJECTTYPE") & ""
                    dbPrefSet = pPrefs.GetPrefs("USER", sAuthentication, Me.SCatalog, sCurTypeChild, sPkeyTypeChild, "", "", "", "ShowInIndex", "INDEXFILTER")
                    If dbPrefSet.Tables(0).Rows.Count > 0 Then
                        Dim i As Integer
                        For i = 0 To dbPrefSet.Tables(0).Rows.Count - 1
                            sPropCheck = dbPrefSet.Tables(0).Rows(i)("PROP") & ""
                            sPropValueCheck = dbPrefSet.Tables(0).Rows(i)("PROPVALUE") & ""

                            Select Case sPropCheck
                                Case "ItemProp"
                                    sCurPropValue = nRow(j)(sItemProp) & ""
                                Case "ItemProp2"
                                    sCurPropValue = nRow(j)(sItemProp2) & ""
                                Case Else
                                    If Not IsDBNull(nRow(j)(sPropCheck)) Then
                                        sCurPropValue = nRow(j)(sPropCheck) & ""
                                    End If
                            End Select

                            If sCurPropValue = sPropValueCheck Then
                                bShowChilds = False
                                Exit For
                            Else
                                bShowChilds = True
                            End If
                        Next
                    Else
                        bShowChilds = True
                    End If
                    If bShowChilds = True Then
                        Exit For
                    End If
                Next

                If bShowChilds = True Then
                    SubNodes = True
                Else
                    SubNodes = False
                End If
            Else
                SubNodes = False
            End If


            ''Dim nRow As DataRow() = dbDatasetSubNodes.Tables(0).Select("pparentkey = '" & sID & "'")

            ''If (nRow.Length > 0) Then

            ''    SubNodes = True

            ''Else

            ''    SubNodes = False

            ''End If

            Dim SParentKey As String = CurKey.Item("PPARENTKEY")

            If Not SubNodes Then

                sXmlNode = sSpaces & "<Name><![CDATA[" & sName & "]]></Name>" & vbCrLf _
                        & sSpaces & "<ID>" & sID & "</ID>" & vbCrLf _
                        & sSpaces & "<Level>" & CurLev & "</Level>" & vbCrLf _
                        & sSpaces & "<ObjectClass>" & sObjectClass & "</ObjectClass>" & vbCrLf _
                        & sSpaces & "<Type>" & sType & "</Type>" & vbCrLf _
                        & sSpaces & "<Template>" & sTemplate & "</Template>" & vbCrLf _
                        & sSpaces & "<ObjType>" & CurType & "</ObjType>" & vbCrLf _
                        & sSpaces & "<ItemProp><![CDATA[" & sItemPropValue & "]]></ItemProp>" & vbCrLf _
                        & sSpaces & "<ItemProp2><![CDATA[" & sItemPropValue2 & "]]></ItemProp2>" & vbCrLf _
                        & sSpaces & "<ParentKey>" & SParentKey & "</ParentKey>" & vbCrLf _
& "<StartTime>" & dStartDate & "</StartTime>" & vbCrLf & "<FinishTime>" & Now() & "</FinishTime>"
                ' & sSpaces & "<TotalTime>" & (Timer() - dTotal) & "</TotalTime>" & vbCrLf



                sOutputXml = sOutputXml & vbCrLf & sSpaces & "<Object>" & vbCrLf & sXmlNode & sSpaces & "</Object>"

            Else



                sXmlNode = sSpaces & "<Name><![CDATA[" & sName & "]]></Name>" & vbCrLf _
                        & sSpaces & "<ID>" & sID & "</ID>" & vbCrLf _
                        & sSpaces & "<Level>" & CurLev & "</Level>" & vbCrLf _
                        & sSpaces & "<ObjectClass>" & sObjectClass & "</ObjectClass>" & vbCrLf _
                        & sSpaces & "<Type>" & sType & "</Type>" & vbCrLf _
                        & sSpaces & "<Template>" & sTemplate & "</Template>" & vbCrLf _
                        & sSpaces & "<ObjType>" & CurType & "</ObjType>" & vbCrLf _
                        & sSpaces & "<ItemProp><![CDATA[" & sItemPropValue & "]]></ItemProp>" & vbCrLf _
                        & sSpaces & "<ItemProp2><![CDATA[" & sItemPropValue2 & "]]></ItemProp2>" & vbCrLf _
                        & sSpaces & "<ParentKey>" & SParentKey & "</ParentKey>" & vbCrLf _
& "<StartTime>" & dStartDate & "</StartTime>" & vbCrLf & "<FinishTime>" & Now() & "</FinishTime>"
                '& sSpaces & "<TotalTime>" & (Timer() - dTotal) & "</TotalTime>" & vbCrLf



                Dim tLevel As Integer = Integer.Parse(CurLev) + 1



                sXmlNodeSon = sSpaces & sSpaces & "<Name><![CDATA[" & "Empty Node" & "]]></Name>" & vbCrLf _
                        & sSpaces & sSpaces & "<ID>" & sID & "</ID>" & vbCrLf _
                        & sSpaces & sSpaces & "<Level>" & tLevel.ToString & "</Level>" & vbCrLf _
                        & sSpaces & sSpaces & "<ObjectClass>" & sObjectClass & "</ObjectClass>" & vbCrLf _
                        & sSpaces & sSpaces & "<Type>" & sType & "</Type>" & vbCrLf _
                        & sSpaces & sSpaces & "<Template>" & sTemplate & "</Template>" & vbCrLf _
                        & sSpaces & sSpaces & "<ObjType>" & CurType & "</ObjType>" & vbCrLf _
                        & sSpaces & sSpaces & "<ItemProp><![CDATA[" & sItemPropValue & "]]></ItemProp>" & vbCrLf _
                        & sSpaces & sSpaces & "<ItemProp2><![CDATA[" & sItemPropValue2 & "]]></ItemProp2>" & vbCrLf _
                        & sSpaces & sSpaces & "<ParentKey>" & SParentKey & "</ParentKey>" & vbCrLf _
& "<StartTime>" & dStartDate & "</StartTime>" & vbCrLf & "<FinishTime>" & Now() & "</FinishTime>"
                '& sSpaces & sSpaces & "<TotalTime>" & (Timer() - dTotal) & "</TotalTime>" & vbCrLf



                sOutputXml = sOutputXml & vbCrLf & sSpaces & "<Object>" & vbCrLf & sXmlNode & vbCrLf & "<Object>" & sXmlNodeSon & sSpaces & "</Object>" & vbCrLf & "</Object>"

            End If
        End If

        pPrefs = Nothing

        InnerWriteDmc = sOutputXml

    End Function

    Private Function ConvertFromTypeNameToPKey(ByVal sPTypeName As String) As String

        'Dim oConns As New CConnection
        Dim dbDatasetcon As New Data.DataSet
        Dim sSqlCon As String = "SELECT * FROM T_CAT_OBJECTTYPES WHERE PTYPENAME='" & sPTypeName & "'"

        'oConns.Open_Oracle_Connection(SODBCNAMEG)
        oConn.Open_Dataset(sSqlCon, dbDatasetcon)

        'oConns.Close_Connection()

        If dbDatasetcon.Tables(0).Rows.Count > 0 Then
            ConvertFromTypeNameToPKey = dbDatasetcon.Tables(0).Rows(0)("PKEY") & ""
        Else
            ConvertFromTypeNameToPKey = ""
        End If

    End Function

    'this function gets object type and return the table of the object.
    Function ConverFromObjectTypeToTableName(ByVal sObjectTypeName As String) As String
        Select Case sObjectTypeName
            Case oLWObjectTypes.emCatalog
                ConverFromObjectTypeToTableName = oLWObjectTypes.emCatalogTable
            Case oLWObjectTypes.emPage
                ConverFromObjectTypeToTableName = oLWObjectTypes.emPageTable
            Case oLWObjectTypes.emPart
                ConverFromObjectTypeToTableName = oLWObjectTypes.emPartTable
            Case oLWObjectTypes.emPicture
                ConverFromObjectTypeToTableName = oLWObjectTypes.emPicturePropertyTable
            Case oLWObjectTypes.emWinObject
                ConverFromObjectTypeToTableName = oLWObjectTypes.emWinObjectTable
        End Select
    End Function

    Function ConvertFromIntToExtension(ByVal iFileExtension As Integer)
        Select Case iFileExtension
            Case 0
                ConvertFromIntToExtension = "jpg"
            Case 1
                ConvertFromIntToExtension = "gif"
        End Select
    End Function

    Public Property CatalogName()
        Get
            CatalogName = sCatalogName
        End Get
        Set(ByVal Value)
            sCatalogName = Value
        End Set
    End Property

    Public Property IndexFile()
        Get
            IndexFile = sIndexFile
        End Get
        Set(ByVal Value)
            sIndexFile = Value
        End Set
    End Property

    Public Property CurrentURL() As String
        Get
            Return Me.sCurrentURL
        End Get
        Set(ByVal Value As String)
            Me.sCurrentURL = Value
        End Set
    End Property

    Public Property WorkMode() As eWorkMode
        Get
            Return Me.sWorkMode
        End Get
        Set(ByVal Value As eWorkMode)
            Me.sWorkMode = Value
        End Set
    End Property

    Private Function getFileName(ByVal sFilePath As String)
        Dim iPos As Integer

        iPos = InStrRev(sFilePath, "\")
        If iPos > 0 Then
            getFileName = Mid(sFilePath, iPos + 1)
        Else
            getFileName = sFilePath
        End If

    End Function

    Function SortDataset(ByVal oDbPredefinedDataset As Data.DataSet, ByVal sFieldName As String) As Data.DataSet

        Dim dv As Data.DataView
        dv = oDbPredefinedDataset.Tables(0).DefaultView
        dv.Sort = sFieldName
        dv.ApplyDefaultSort = True

        Dim obNewDt As New Data.DataTable
        obNewDt = oDbPredefinedDataset.Tables(0).Clone()

        Dim idx As Integer = 0
        Dim strColNames() As String

        'foreach (DataColumn col in obNewDt.Columns)
        '{
        '	strColNames[idx++] = col.ColumnName;
        '''}

        Dim viewEnumerator As IEnumerator = dv.GetEnumerator()
        Dim dr As Data.DataRow
        Do While (viewEnumerator.MoveNext())
            Dim drv As Data.DataRowView = DirectCast(viewEnumerator.Current, Data.DataRowView)
            dr = obNewDt.LoadDataRow(drv.Row.ItemArray, True)
        Loop


        Dim newDataset As New Data.DataSet
        newDataset.Tables.Add(obNewDt)

        SortDataset = newDataset
        '       Try
        '		foreach (string strName in strColNames)
        '		{
        '			dr[strName] = drv[strName];
        '		}
        '	catch (Exception ex)
        '	{
        '		Trace.WriteLine(ex.Message);
        '	}
        '	obNewDt.Rows.Add(dr);
        ''''}

        'return obNewDt;


    End Function
    '''''''Private Function ExcelExport()
    '''''''    Dim aaa As New DataView
    '''''''    oConn.Open_Dataset("SELECT * FROM T_CAT_PART WHERE PKEY='137213a9'", dbDataSetChilds)
    '''''''    oConn.Open_Dataset("SELECT * FROM T_CAT_WINOBJECT WHERE PKEY='LWWEa129321'", dbDataSetChilds)

    '''''''    Dim aa As String
    '''''''    Dim j As Integer

    '''''''    For i = 0 To dbDataSetChilds.Tables(0).Columns.Count - 1
    '''''''        aa = aa & dbDataSetChilds.Tables(0).Columns(i).ColumnName & ","
    '''''''    Next

    '''''''    aa = Mid(aa, 1, aa.Length - 1) & vbCrLf

    '''''''    For i = 0 To dbDataSetChilds.Tables(0).Rows.Count - 1
    '''''''        For j = 0 To dbDataSetChilds.Tables(0).Columns.Count - 1
    '''''''            aa = aa & dbDataSetChilds.Tables(0).Rows.Item(i).Item(j) & ","
    '''''''        Next
    '''''''        aa = Mid(aa, 1, aa.Length - 1) & vbCrLf
    '''''''    Next

    '''''''    System.Web.HttpContext.Current.Response.Write(aa)

    '''''''End Function

End Class
