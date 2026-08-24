Imports System.IO

Public Class GenerateMDB

    Dim sODBCName As String = ""
    Dim sCatalog As String = ""
    Dim sCDPkey As String = ""
    Dim sCDDir As String = ""
    Dim sCDName As String = ""
    Dim sPublishDir As String = ""
    Dim sServerPath As String = ""
    Dim sMDBPath As String = ""
    Dim tables As New LWObjectTypes
    Dim oConn As CConnection
    Dim sLogonUser As String
    Dim sgemAppMode As String
    Dim sImageMapManage As String
    Dim oSession As System.Web.SessionState.HttpSessionState
    Dim iTotalCount As Integer = 0
    Dim iCounter As Integer = 0
    Dim iInitCount = 0
    'Dim progPath As String = "c:\inetpub\wwwroot\webeditor\tomer.txt"
    'Dim fs As FileStream


    Public Sub New(ByVal sODBCName As String, ByVal sCDPkey As String, ByVal sCDDir As String, ByRef sPublishDir As String, ByRef sServerPath As String, ByVal sLogonUser As String, ByVal sgemAppMode As String, ByVal sImageMapManage As String, ByRef oSession As System.Web.SessionState.HttpSessionState)

        Me.sCDPkey = sCDPkey
        Me.sCDDir = sCDDir
        Me.sPublishDir = sPublishDir
        Me.sServerPath = sServerPath
        Me.sODBCName = sODBCName
        Me.sLogonUser = sLogonUser
        Me.sgemAppMode = sgemAppMode
        Me.sImageMapManage = sImageMapManage
        Me.oSession = oSession

        ''progPath = progPath & oSession.SessionID.Substring(0, 5) & ".txt"
        '' Delete the file if it exists.

        'fs = File.Create(progPath)
        'fs.Close()


        'If File.Exists(progPath) = False Then
        '    fs = File.Create(progPath)
        '    fs.Close()
        '    Me.iInitCount = 0
        'Else
        '    Dim sr As StreamReader = File.OpenText(progPath)
        '    Me.iInitCount = CInt(sr.ReadLine())
        '    sr.Close()
        'End If

        'System.Web.HttpContext.Current.Session.Add("ppp", "ppp")
        'If Not oResponse.Cookies("Precent") Is Nothing Then
        '    If oResponse.Cookies("Precent").Value = "" Then

        '    End If
        '    'Me.iInitCount = CInt(oResponse.Cookies("Precent").Value)
        'End If

    End Sub
    Private Function BuildCD(ByRef cdName As String)

        Dim oFiles As New CFileOps
        If Not Directory.Exists(sCDDir & "\" & cdName) Then
            Directory.CreateDirectory(sCDDir & "\" & cdName)
            oFiles.CopyDir(sServerPath & "CDRootFiles", sCDDir & "\" & cdName)
        End If
        If Directory.Exists(sPublishDir & "Catalogs\" & sCatalog) And Not Directory.Exists(sCDDir & "\" & cdName & "\Catalogs\" & sCatalog) Then
            Directory.CreateDirectory(sCDDir & "\" & cdName & "\Catalogs\" & sCatalog)
            oFiles.CopyDir(sPublishDir & "Catalogs\" & sCatalog, sCDDir & "\" & cdName & "\Catalogs\" & sCatalog)
        End If

        sMDBPath = sCDDir & "\" & cdName & "\MDB\Catalogs.mdb"
        File.SetAttributes(sMDBPath, FileAttributes.Normal)

    End Function

    Public Function PublishCD()

        'Use on caling class

        'Dim gen As New GenerateMDB(Application("ODBCNAME"),"cdpkey", Application("PublishCDDirectory"), Application("PublishOfflineDirectory"), Server.MapPath(".\"))
        'gen.PublishCD()

        Dim sSqlCDQuery As String = ""
        Dim dbCDSet As New Data.DataSet
        Dim i As Integer
        Dim sSqlSumQuery As String = ""
        sSqlCDQuery = "SELECT * FROM T_CAT_CD_DATA WHERE PKEY='" & sCDPkey & "'"

        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCName)
        oConn.Open_Dataset(sSqlCDQuery, dbCDSet)
        If dbCDSet.Tables(0).Rows.Count > 0 Then
            sCDName = dbCDSet.Tables(0).Rows(0)("PCDNAME") & ""
        Else
            oConn.Close_Connection()
            Exit Function
        End If

        dbCDSet.Reset()
        sSqlCDQuery = "SELECT * FROM T_CAT_CD_CATALOG WHERE PCDKEY='" & sCDPkey & "'"
        oConn.Open_Dataset(sSqlCDQuery, dbCDSet)
        oConn.Close_Connection()
        oConn = Nothing

        Dim sMdbPathToOverwrite As String = sCDDir & "\" & sCDName & "\MDB\Catalogs.mdb"
        If File.Exists(sMdbPathToOverwrite) Then
            File.Copy(sServerPath & "CDRootFiles\MDB\Catalogs.mdb", sMdbPathToOverwrite, True)
        End If

        If dbCDSet.Tables(0).Rows.Count > 0 Then
            For i = 0 To dbCDSet.Tables(0).Rows.Count - 1
                Dim dbtset As New Data.DataSet
                sCatalog = dbCDSet.Tables(0).Rows(i)("PKEY") & ""
                sSqlSumQuery = buildSumSql(sCatalog)
                oConn = New CConnection
                oConn.Open_Oracle_Connection(sODBCName)
                oConn.Open_Dataset(sSqlSumQuery, dbtset)
                oConn.Close_Connection()
                oConn = Nothing
                If dbtset.Tables(0).Rows.Count > 0 Then
                    iTotalCount = iTotalCount + dbtset.Tables(0).Rows(0)(0)
                End If
                dbtset.Dispose()
            Next
        End If

        If dbCDSet.Tables(0).Rows.Count > 0 Then
            For i = 0 To dbCDSet.Tables(0).Rows.Count - 1
                sCatalog = dbCDSet.Tables(0).Rows(i)("PKEY") & ""
                BuildCD(sCDName)
                GenerateTable(tables.emNodesTable)   '500
                GenerateTable(tables.emCatalogTable) '1
                GenerateTable(tables.emDocumentLabelsTable)
                GenerateTable(tables.emFamilyTable)
                GenerateTable(tables.emFolderTable) 'w
                GenerateTable(tables.emLinkTable) 'z
                GenerateTable(tables.emMakakTable)
                GenerateTable(tables.emObjectTypesTable)
                GenerateTable(tables.emPageTable) 'v
                GenerateTable(tables.emPartTable) ' 500-x-y-z-w-v-p
                GenerateTable(tables.emPartLabelsTable)
                GenerateTable(tables.emPicturePropertyTable) 'y
                GenerateTable(tables.emStatusTable)
                GenerateTable(tables.emTextTable) ' p
                GenerateTable(tables.emVendorTable)
                GenerateTable(tables.emWinObjectTable) 'x
            Next
        End If

        dbCDSet.Dispose()
        dbCDSet = Nothing

        'start html
        File.Copy(sMDBPath, "C:\Temp\Catalogs.mdb", True)
        GenerateStart()

    End Function

    Sub GenerateStart()

        Dim oLWObjectTypes As New LWObjectTypes
        Dim oFile As File
        Dim oCHtmlGenerator As New CHtmlGenerator(sServerPath & "Templates\start.html", True, sODBCName, oLWObjectTypes.emCatalog, sLogonUser, sgemAppMode, sImageMapManage, oConn)
        Dim sOutput As String
        Dim outputfileSW As StreamWriter

        sOutput = oCHtmlGenerator.GetOutput("SELECT * FROM T_CAT_CD_DATA WHERE PKEY='" & sCDPkey & "'")

        outputfileSW = oFile.CreateText(sCDDir & "\" & sCDName & "\start.html")
        outputfileSW.Write(sOutput)
        outputfileSW.Close()

        oCHtmlGenerator.Dispose()
        oCHtmlGenerator = Nothing
        oLWObjectTypes = Nothing

    End Sub
    Private Function GenerateTable(ByRef tName As String) As Data.DataSet

        Dim pszSQLQuery As String
        Dim gccPParentKey As String = "PPARENTKEY"
        Dim gccPKey As String = "PKEY"
        Dim gccPKeyCatalog As String = "PKEYCATALOG"
        Dim dbRetSet As New Data.DataSet

        Select Case tName
            Case tables.emPartTable
                pszSQLQuery = "SELECT distinct " & tables.emPartTable & ".* " _
                & " FROM " & tables.emPartTable & ",(select * from " & tables.emNodesTable & _
                " where PGlobalStatus<>4 and pobjecttype=10 connect by prior " & tables.emNodesTable & "." & gccPKey & "=" & tables.emNodesTable & "." & gccPParentKey & _
                " start with " & gccPKey & "='" & sCatalog & "') T1" _
                & " WHERE " _
                & tables.emPartTable & "." & gccPKey & " = T1." & gccPKey

            Case tables.emFolderTable
                pszSQLQuery = "SELECT * FROM " & tables.emFolderTable & "," & tables.emNodesTable _
                & " WHERE " _
                & tables.emFolderTable & "." & gccPKeyCatalog & " = " _
                & "'" & sCatalog & "'" _
                & " AND " & tables.emFolderTable & "." & gccPKey & " = " _
                & tables.emNodesTable & "." & gccPKey _
                & " AND " & tables.emNodesTable & ".PGlobalStatus <> " _
                & tables.emDeleted & " AND " & tables.emNodesTable & ".POBJECTTYPE" _
                & " = " & tables.emFolder

            Case tables.emCatalogTable
                pszSQLQuery = " SELECT " & tables.emCatalogTable & ".* FROM " & tables.emCatalogTable & "," & tables.emNodesTable _
            & " WHERE " _
            & tables.emCatalogTable & "." & gccPKey & " = " _
            & "'" & sCatalog & "'" _
            & " AND " & tables.emCatalogTable & "." & gccPKey & " = " _
            & tables.emNodesTable & "." & gccPKey _
            & " AND " & tables.emNodesTable & ".PGlobalStatus <> " _
            & tables.emDeleted & " AND " & tables.emNodesTable & ".POBJECTTYPE" _
            & " = " & tables.emCatalog

            Case tables.emTextTable
                pszSQLQuery = "SELECT " & tables.emTextTable & ".* FROM " & tables.emTextTable & "," & tables.emNodesTable _
            & " WHERE " _
            & tables.emTextTable & "." & gccPKeyCatalog & " = " _
            & "'" & sCatalog & "'" _
            & " AND " & tables.emTextTable & "." & gccPKey & " = " _
            & tables.emNodesTable & "." & gccPKey _
            & " AND " & tables.emNodesTable & ".PGlobalStatus <> " _
            & tables.emDeleted & " AND " & tables.emNodesTable & ".POBJECTTYPE" _
            & " = " & tables.emText

            Case tables.emPicturePropertyTable
                pszSQLQuery = "SELECT " & tables.emPicturePropertyTable & ".*" _
                & " FROM " & tables.emPicturePropertyTable & "," _
                & tables.emNodesTable _
                & " WHERE " _
                & tables.emPicturePropertyTable & "." & gccPKeyCatalog & " = " _
                & "'" & sCatalog & "'" _
                & " AND " & tables.emPicturePropertyTable & "." & gccPKey & " = " _
                & tables.emNodesTable & "." & gccPKey _
                & " AND " & tables.emNodesTable & ".PGlobalStatus <> " _
                & tables.emDeleted & " AND " & tables.emNodesTable & ".POBJECTTYPE" _
                & " = " & tables.emPicture

            Case tables.emWinObjectTable
                pszSQLQuery = "SELECT distinct " & tables.emWinObjectTable & ".*" _
                & " FROM " & tables.emWinObjectTable & "," _
                & tables.emNodesTable _
                & " WHERE " _
                & tables.emWinObjectTable & "." & gccPKeyCatalog & " = " _
                & "'" & sCatalog & "'" _
                & " AND " & tables.emWinObjectTable & "." & gccPKey & " = " _
                & tables.emNodesTable & "." & gccPKey _
                & " AND " & tables.emNodesTable & ".PGlobalStatus <> " _
                & tables.emDeleted & " AND " & tables.emNodesTable & ".POBJECTTYPE" _
                & " = " & tables.emWinObject

            Case tables.emPageTable
                pszSQLQuery = "SELECT " & tables.emPageTable & ".* FROM " & tables.emPageTable & "," & tables.emNodesTable _
            & " WHERE " _
            & tables.emPageTable & "." & gccPKeyCatalog & " = " _
            & "'" & sCatalog & "'" _
            & " AND " & tables.emPageTable & "." & gccPKey & " = " _
            & tables.emNodesTable & "." & gccPKey _
            & " AND " & tables.emNodesTable & ".PGlobalStatus <> " _
            & tables.emDeleted & " AND " & tables.emNodesTable & ".POBJECTTYPE" _
            & " = " & tables.emPage

            Case tables.emLinkTable
                pszSQLQuery = "SELECT " & tables.emLinkTable & ".* FROM " & tables.emLinkTable & "," & tables.emNodesTable _
            & " WHERE " _
            & tables.emLinkTable & "." & gccPKeyCatalog & " = " _
            & "'" & sCatalog & "'" _
            & " AND " & tables.emLinkTable & "." & gccPKey & " = " _
            & tables.emNodesTable & "." & gccPKey _
            & " AND " & tables.emNodesTable & ".PGlobalStatus <> " _
            & tables.emDeleted & " AND " & tables.emNodesTable & ".POBJECTTYPE" _
            & " = " & tables.emLink

            Case tables.emPartLabelsTable
                pszSQLQuery = "SELECT " & tables.emPartLabelsTable & ".* FROM " & tables.emPartLabelsTable & " WHERE PKEYCATALOG=" & "'" & sCatalog & "' OR PKEYCATALOG='default'"

            Case tables.emObjectTypesTable
                pszSQLQuery = "SELECT " & tables.emObjectTypesTable & ".* FROM " & tables.emObjectTypesTable & " WHERE PKEYCATALOG=" & "'" & "default" & "'"

            Case tables.emDocumentLabelsTable
                pszSQLQuery = "SELECT " & tables.emDocumentLabelsTable & ".* FROM " & tables.emDocumentLabelsTable & " WHERE PKEYCATALOG=" & "'" & sCatalog & "' OR PKEYCATALOG='default'"

            Case tables.emStatusTable
                pszSQLQuery = "SELECT " & tables.emStatusTable & ".* " & " FROM " & tables.emStatusTable & " WHERE PKEYCATALOG=" & "'" & sCatalog & "' OR PKEYCATALOG='default'"

            Case tables.emNodesTable
                pszSQLQuery = "select distinct * from t_cat_nodes where pglobalstatus<>4 and pobjecttype<>1 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "'"

            Case tables.emMakakTable
                pszSQLQuery = "select distinct * from t_cat_makat where pparentkey in (select pkey from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "')"

            Case tables.emVendorTable
                pszSQLQuery = "select distinct * from t_cat_vendors where pparentkey in (select pkey from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "')"

            Case tables.emFamilyTable
                pszSQLQuery = "select * from t_cat_family"

        End Select

        oConn = New CConnection
        oConn.Open_Oracle_Connection(sODBCName)
        oConn.Open_Dataset(pszSQLQuery, dbRetSet)
        oConn.Close_Connection()
        oConn = Nothing

        If dbRetSet.Tables(0).Rows.Count > 0 Then
            InsertTableMDB(dbRetSet, tName)
        End If

        dbRetSet.Dispose()
        dbRetSet = Nothing

    End Function

    Private Function InsertTableMDB(ByRef table As Data.DataSet, ByRef tname As String)

        Dim conString As String = "Driver={Microsoft Access Driver (*.mdb)};Dbq=" & sMDBPath & ";Uid=Admin;Pwd=;"
        Dim mdbDataSet As Data.DataSet
        'oConn.Open_Access_Connection(conString)
        Dim tableName = tname
        Dim sSqlCheck As String = ""
        Dim sSqlInsert1 As String = "INSERT INTO " & tname & " ("
        Dim sSqlInsert2 As String = " VALUES ("

        Dim i As Integer
        For i = 0 To table.Tables(0).Columns.Count - 1
            sSqlInsert1 = sSqlInsert1 & table.Tables(0).Columns(i).ColumnName & ","
        Next

        sSqlInsert1 = sSqlInsert1.Remove(sSqlInsert1.Length - 1, 1)
        sSqlInsert1 = sSqlInsert1 & ")"

        For i = 0 To table.Tables(0).Rows.Count - 1
            Dim j As Integer
            Dim sValue As String
            For j = 0 To table.Tables(0).Columns.Count - 1

                If table.Tables(0).Columns(j).ColumnName = "PKEYCATALOG" Then
                    sSqlInsert2 = sSqlInsert2 & "'" & sCatalog & "',"
                Else
                    sValue = table.Tables(0).Rows(i)(j) & ""
                    sValue = sValue.Replace("'", "''")

                    Dim colDataType As String = table.Tables(0).Columns(j).DataType.ToString()
                    Select Case colDataType
                        Case "System.Decimal"
                            If sValue = "" Then
                                sSqlInsert2 = sSqlInsert2 & 0 & ","
                            Else
                                sSqlInsert2 = sSqlInsert2 & sValue & ","
                            End If
                        Case "System.DateTime"
                            If sValue = "" Then
                                sSqlInsert2 = sSqlInsert2 & "null" & ","
                            Else
                                sSqlInsert2 = sSqlInsert2 & "'" & sValue & "',"
                            End If
                        Case Else
                            sSqlInsert2 = sSqlInsert2 & "'" & sValue & "',"
                    End Select
                End If

            Next
            sSqlInsert2 = sSqlInsert2.Remove(sSqlInsert2.Length - 1, 1)
            sSqlInsert2 = sSqlInsert2 & ")"
            mdbDataSet = New Data.DataSet

            If tableName = tables.emFolderTable Or tableName = tables.emLinkTable Or tableName = tables.emPartTable Or tableName = tables.emPageTable Or tableName = tables.emTextTable Or tableName = tables.emWinObjectTable Or tableName = tables.emPicturePropertyTable Then
                sSqlCheck = "Select * from " & tableName & " where pkey='" & table.Tables(0).Rows(i)("PKEY") & "'"
            ElseIf tableName = tables.emNodesTable Then
                sSqlCheck = "Select * from " & tableName & " where pkey='" & table.Tables(0).Rows(i)("PKEY") & "' and PPARENTKEY='" & table.Tables(0).Rows(i)("PPARENTKEY") & "'"
            ElseIf tableName = tables.emObjectTypesTable Then
                sSqlCheck = "Select * from " & tableName & " where pnum=" & table.Tables(0).Rows(i)("PNUM")
            Else
                sSqlCheck = ""
            End If

            Dim bInsert As Boolean = True
            If sSqlCheck <> "" Then
                Dim dbtemp As New Data.DataSet
                oConn = New CConnection
                oConn.Open_Access_Connection(conString)
                oConn.Open_Dataset(sSqlCheck, dbtemp)
                If dbtemp.Tables(0).Rows.Count > 0 Then
                    'If dbtemp.Tables(0).Rows(0)("PKEY") = table.Tables(0).Rows(i)("PKEY") Then
                    bInsert = False
                    'Else
                    'bInsert = True
                    'End If
                Else
                    bInsert = True
                End If
                oConn.Close_Connection()
                oConn = Nothing
                dbtemp.Dispose()
            Else
                bInsert = True
            End If

            If bInsert Then
                oConn = New CConnection
                oConn.Open_Access_Connection(conString)
                oConn.Open_Dataset(sSqlInsert1 & sSqlInsert2, mdbDataSet)
                oConn.Close_Connection()
                oConn = Nothing
                mdbDataSet.Dispose()
                mdbDataSet = Nothing
            End If
            sSqlInsert2 = " VALUES ("

            iCounter = iCounter + 1
            Dim iSes As Integer
            iSes = iInitCount + CInt((iCounter * 100) / iTotalCount)

            'Dim info As Byte() = _
            'New System.Text.UTF8Encoding(True).GetBytes(iSes.ToString())
            'fs = File.OpenWrite(progPath)
            'fs.Write(info, 0, info.Length)
            'fs.Close()

            'oResponse.Cookies("Precent").Value = iSes.ToString()

        Next

        'oConn.Close_Connection()
        'oConn = Nothing

    End Function
    Private Function buildSumSql(ByVal sCatalog As String)

        Dim sSqlSumQuery As String = ""

        sSqlSumQuery = "select sum(aa) from (" _
        & "(select count(pkey || '1') aa from (select distinct * from t_cat_nodes where pglobalstatus<>4 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "')) " _
        & "union " _
        & "(select count(pkey || '2') from t_cat_document_labels where pkeycatalog='default' or pkeycatalog='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '3')  from t_cat_family) " _
        & "union " _
        & "(select count(pkey || '4') from t_cat_catalog where pkey='" & sCatalog & "') " _
        & "union " _
        & "(select count(pmakat || '5')  from t_cat_makat where pparentkey in (select pkey from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') ) " _
        & "union " _
        & "(select count(pkey || '6')  from t_cat_objecttypes where pkeycatalog='default' or pkeycatalog='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '7')  from t_cat_part_lables where pkeycatalog='default' or pkeycatalog='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '8')  from t_cat_status where pkeycatalog='default' or pkeycatalog='" & sCatalog & "') " _
        & "union " _
        & "(select count(PVENDOR_NUMBER || '9')   from t_cat_vendors where pparentkey in (select pkey from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') ) " _
        & "union " _
        & "(select count(pkey || '10')  from t_cat_nodes where pglobalstatus<>4 and pobjecttype=18 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '11')  from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '12')  from t_cat_nodes where pglobalstatus<>4 and pobjecttype=11 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '13')  from t_cat_nodes where pglobalstatus<>4 and pobjecttype=2 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '14')  from t_cat_nodes where pglobalstatus<>4 and pobjecttype=3 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '15')  from t_cat_nodes where pglobalstatus<>4 and pobjecttype=6 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') " _
        & "union " _
        & "(select count(pkey || '16')   from t_cat_link where pkey in (select pkey from t_cat_nodes where pglobalstatus<>4 and pobjecttype=5 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sCatalog & "') ))"

        buildSumSql = sSqlSumQuery

    End Function

End Class
