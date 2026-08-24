Imports System.Data

Public Class Paste
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sPasteObject As String
    Dim sPasteCatalog As String
    Dim sObjectType As String

    Function getNewPkey() As String

        Dim sSqlQuery As String
        Dim oTempConn As New CConnection
        Dim sNewPkey As String
        Dim dbDataSet As DataSet

        If Application("gemAppMode") = "Access" Then
            sSqlQuery = "SELECT T_CAT_MAINOPTION.PCVALUE FROM(T_CAT_MAINOPTION) WHERE (((T_CAT_MAINOPTION.PCNAME)='PLASTSEQNUMBER'))"
        Else
            sSqlQuery = "SELECT PKEY_SEQNUMBER.NEXTVAL FROM DUAL"
        End If

        If Application("gemAppMode") = "Access" Then
            oTempConn.Open_Access_Connection(Application("ODBCNAME"))
        Else
            oTempConn.Open_Oracle_Connection(Application("ODBCNAME"))
        End If

        dbDataSet = New DataSet
        oTempConn.Open_Dataset(sSqlQuery, dbDataSet)
        sNewPkey = Application("PkeyPrefix") & "" & dbDataSet.Tables(0).Rows(0).Item(0)
        dbDataSet = Nothing

        'only for access
        If Application("gemAppMode") = "Access" Then
            dbDataSet = New DataSet
            sSqlQuery = "UPDATE T_CAT_MAINOPTION SET T_CAT_MAINOPTION.PCVALUE = PCVALUE+1 WHERE (((T_CAT_MAINOPTION.PCNAME)='PLASTSEQNUMBER'))"
            oTempConn.Open_Dataset(sSqlQuery, dbDataSet)
            dbDataSet = Nothing
        End If

        oTempConn.Close_Connection()
        oTempConn = Nothing
        getNewPkey = sNewPkey

    End Function

    Sub PasteObject(ByVal iPasteOption As Integer)
        Dim sAction As String = Session("Action") & ""
        Dim sObjects As String = Session("Objects") & ""
        Dim oConn As CConnection
        Dim dbDataSet As DataSet
        Dim sSqlQuery As String
        Dim oAclPermissions As cACLPermissions
        Dim oLWObjectTypes As New LWObjectTypes
        Dim sObject As String()
        Dim iObjects As Integer
        Dim sFatherObj As String
        Dim oColumn As DataColumn
        Dim sColumns As String
        Dim sNewPkey As String
        Dim myDB As ADODB.Connection
        Dim myRS As ADODB.Recordset

        Dim sTableName As String
        Dim sWebLink As String

        oConn = New CConnection
        oConn.Open_Oracle_Connection(Application("ODBCNAME"))

        If sAction = "" Or sObjects = "" Then
            'ERR - not did copy or cut before
        Else
            oAclPermissions = New cACLPermissions(Request.ServerVariables("LOGON_USER"), Application("ODBCNAME"), Application("gemAppMode"), "", oConn)
            If CBool(oAclPermissions.HasPermission(Session("PasteObject"), "P_MODIFY")) Then
                Select Case sAction
                    Case "Copy"
                        sObject = Split(sObjects, "|")
                        For iObjects = 0 To sObject.Length - 2
                            If sObject(iObjects) <> "" Then

                                dbDataSet = New DataSet
                                'sSqlQuery = "SELECT * FROM T_CAT_NODES WHERE PKEY='" & sObject(iObjects) & "'"
                                sSqlQuery = "SELECT OBJECTS.PKEY,PHEBDESC,POBJECTCLASS, ptypename FROM (SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".PKEY, " & oLWObjectTypes.emPartTable & ".PHEBDESC,10 AS POBJECTCLASS, " & oLWObjectTypes.emObjectTypesTable & ".ptypename FROM " & oLWObjectTypes.emPartTable & "," & oLWObjectTypes.emObjectTypesTable & " WHERE " & oLWObjectTypes.emObjectTypesTable & ".pkey(+) = " & oLWObjectTypes.emPartTable & ".pkeytype" & " and " & oLWObjectTypes.emPartTable & ".PKEY='" & sObject(iObjects) & "' union " _
                                                            & "SELECT DISTINCT " & oLWObjectTypes.emWinObjectTable & ".PKEY, " & oLWObjectTypes.emWinObjectTable & ".PFILEMODIFIEDDATE || ' - ' || " & oLWObjectTypes.emWinObjectTable & ".PDOCNUMBER || ' - ' ||" & oLWObjectTypes.emWinObjectTable & ".PHEBDESC AS PHEBDESC,18 AS POBJECTCLASS , ptypename FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emObjectTypesTable & " WHERE " & oLWObjectTypes.emObjectTypesTable & ".pkey(+) = " & oLWObjectTypes.emWinObjectTable & ".pkeytype" & " and " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sObject(iObjects) & "' union " _
                                                            & "SELECT DISTINCT " & oLWObjectTypes.emPicturePropertyTable & ".PKEY, " & oLWObjectTypes.emPicturePropertyTable & ".PHEBDESC,3 AS POBJECTCLASS , null FROM " & oLWObjectTypes.emPicturePropertyTable & " WHERE " & oLWObjectTypes.emPicturePropertyTable & ".PKEY='" & sObject(iObjects) & "') OBJECTS "
                                oConn.Open_Dataset(sSqlQuery, dbDataSet)

                                If dbDataSet.Tables(0).Rows.Count > 0 Then
                                    sObjectType = dbDataSet.Tables(0).Rows(0)("POBJECTCLASS") & ""
                                End If

                                Select Case sObjectType
                                    Case oLWObjectTypes.emPart
                                        sTableName = oLWObjectTypes.emPartTable
                                    Case oLWObjectTypes.emWinObject
                                        sTableName = oLWObjectTypes.emWinObjectTable
                                    Case Else
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing
                                        oConn.Close_Connection()
                                        oConn = Nothing
                                        Exit Sub
                                End Select

                                dbDataSet.Dispose()

                                Select Case iPasteOption
                                    Case 0
                                        '0 - paste

                                        ''''''''delete already rows with the same pkey/pparentkey in t_cat_nodes.
                                        dbDataSet = New DataSet
                                        sSqlQuery = "DELETE FROM T_CAT_NODES WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("PasteObject") & "' AND PGLOBALSTATUS=4"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)

                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        dbDataSet = New DataSet
                                        sSqlQuery = "select * from t_cat_nodes where pkey='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        'check if nodes exist - yes:
                                        If dbDataSet.Tables(0).Rows.Count > 0 Then
                                            If Session("FromReport") = "true" Then
                                                Session("Father") = dbDataSet.Tables(0).Rows(0)("PPARENTKEY")
                                            End If
                                            dbDataSet.Dispose()
                                            dbDataSet = Nothing

                                            dbDataSet = New DataSet

                                            If cBoxKeepOccur.Checked Then
                                                sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20) (SELECT PKEY,'" & Session("PasteObject") & "',POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20 FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("Father") & "')"
                                            Else
                                                sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS) (SELECT PKEY,'" & Session("PasteObject") & "',POBJECTTYPE, PGLOBALSTATUS FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("Father") & "')"
                                            End If
                                            oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                            dbDataSet.Dispose()
                                            dbDataSet = Nothing
                                        Else
                                            'no - insert to nodes manually.
                                            dbDataSet.Dispose()
                                            dbDataSet = Nothing

                                            dbDataSet = New DataSet
                                            sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS) VALUES ('" & sObject(iObjects) & "','" & Session("PasteObject") & "','" & sObjectType & "', 0)"
                                            oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                            dbDataSet.Dispose()
                                            dbDataSet = Nothing
                                        End If


                                        'pkeychilds check
                                        Dim oPkeyChildsManage As New CPKeyChildsManage(Application("ODBCNAME"))
                                        oPkeyChildsManage.UpdatePkeyChilds(sObject(iObjects), 1)
                                        oPkeyChildsManage = Nothing

                                        'run handler
                                        Dim oHandlers As CHandlers
                                        'Handler check
                                        oHandlers = New CHandlers(Application("ODBCNAME"))

                                        oHandlers.ObjectEvents("PASTEOBJECT", sObject(iObjects), Session("PasteObject"), Session("PasteCatalog"), "Copy")

                                        oHandlers = Nothing
                                        'End Handlers

                                    Case 1
                                        '1 - paste as duplicate

                                        ''''''''''myDB = New ADODB.Connection
                                        ''''''''''myRS = New ADODB.Recordset

                                        ''''''''''myDB.Open(Application("ODBCNAME"))
                                        ''''''''''myRS.Open(sTableName, myDB, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockPessimistic)
                                        ''''''''''myRS.AddNew()

                                        ''''''''''dbDataSet = New DataSet
                                        ''''''''''sSqlQuery = "SELECT * FROM " & sTableName & " WHERE PKEY='" & sObject(iObjects) & "'"
                                        ''''''''''oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        ''''''''''For Each oColumn In dbDataSet.Tables(0).Columns
                                        ''''''''''    If LCase(oColumn.ColumnName) <> "pkey" And LCase(oColumn.ColumnName) <> "pkeypage" And LCase(oColumn.ColumnName) <> "pkeycatalog" Then
                                        ''''''''''        myRS.Fields(oColumn.ColumnName).Value = dbDataSet.Tables(0).Rows(0).Item(oColumn.ColumnName)
                                        ''''''''''        'sColumns = sColumns & oColumn.ColumnName & ", "
                                        ''''''''''    End If
                                        ''''''''''Next

                                        '''''''''''sColumns = Mid(sColumns, 1, sColumns.Length - 2)
                                        ''''''''''dbDataSet.Dispose()
                                        ''''''''''dbDataSet = Nothing

                                        ''''''''''sNewPkey = getNewPkey()
                                        ''''''''''myRS.Fields("PKEY").Value = sNewPkey
                                        ''''''''''myRS.Fields("PKEYPAGE").Value = Session("PasteObject")
                                        ''''''''''myRS.Fields("PKEYCATALOG").Value = Session("PasteCatalog")
                                        ''''''''''myRS.Update()

                                        ''''''''''myRS.Close()
                                        ''''''''''myRS = Nothing

                                        ''''''''''myDB.Close()
                                        ''''''''''myDB = Nothing


                                        dbDataSet = New DataSet
                                        sSqlQuery = "SELECT * FROM " & sTableName & " WHERE PKEY='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        sColumns = ""
                                        For Each oColumn In dbDataSet.Tables(0).Columns
                                            If LCase(oColumn.ColumnName) <> "pkey" And LCase(oColumn.ColumnName) <> "pkeypage" And LCase(oColumn.ColumnName) <> "pkeycatalog" Then
                                                sColumns = sColumns & oColumn.ColumnName & ", "
                                            End If
                                        Next

                                        sColumns = Mid(sColumns, 1, sColumns.Length - 2)
                                        sNewPkey = getNewPkey()
                                        sSqlQuery = "INSERT INTO " & sTableName & "(PKEY,PKEYPAGE, PKEYCATALOG, " & sColumns & ") SELECT '" & sNewPkey & "','" & Session("PasteObject") & "','" & Session("PasteCatalog") & "'," & sColumns & " FROM " & sTableName & " WHERE PKEY='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        ''''''''myRS.Fields("PKEY").Value = sNewPkey
                                        ''''''''myRS.Fields("PKEYPAGE").Value = Session("PasteObject")
                                        ''''''''myRS.Fields("PKEYCATALOG").Value = Session("PasteCatalog")
                                        ''''''''myRS.Update()


                                        dbDataSet = New DataSet
                                        sSqlQuery = "select * from t_cat_nodes where pkey='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        'check if nodes exist - yes:
                                        If dbDataSet.Tables(0).Rows.Count > 0 Then
                                            If Session("FromReport") = "true" Then
                                                Session("Father") = dbDataSet.Tables(0).Rows(0)("PPARENTKEY")
                                            End If
                                        End If

                                        If cBoxKeepOccur.Checked Then
                                            sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20) (SELECT '" & sNewPkey & "','" & Session("PasteObject") & "',POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20 FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("Father") & "')"
                                        Else
                                            sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS) (SELECT '" & sNewPkey & "','" & Session("PasteObject") & "',POBJECTTYPE, PGLOBALSTATUS FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("Father") & "')"
                                        End If
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing


                                        'pkeychilds check
                                        Dim oPkeyChildsManage As New CPKeyChildsManage(Application("ODBCNAME"))
                                        oPkeyChildsManage.UpdatePkeyChilds(sObject(iObjects), 1)
                                        oPkeyChildsManage = Nothing

                                        'run handler
                                        Dim oHandlers As CHandlers
                                        'Handler check
                                        oHandlers = New CHandlers(Application("ODBCNAME"))

                                        Session.Add("PasteAsDup", True)
                                        oHandlers.ObjectEvents("ADDOBJECT", sNewPkey, Session("PasteObject"), Session("PasteCatalog"))
                                        Session.Remove("PasteAsDup")

                                        oHandlers = Nothing
                                        'End Handlers

                                    Case 2
                                        '2 - deep paste as duplicate

                                        'bOnlyForHandlerUse = True deletes the paste duplicate objects(but runs the handler for them before deletion) (only in paste as duplicate) -
                                        Dim bOnlyForHandlerUse As String = Request.QueryString("OnlyForHandlerUse")
                                        Dim oDeletePkeys As New ArrayList

                                        'run handler
                                        Dim oHandlers As CHandlers
                                        'Handler check
                                        oHandlers = New CHandlers(Application("ODBCNAME"))

                                        dbDataSet = New DataSet
                                        sSqlQuery = "SELECT * FROM " & sTableName & " WHERE PKEY='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        sColumns = ""
                                        For Each oColumn In dbDataSet.Tables(0).Columns
                                            If LCase(oColumn.ColumnName) <> "pkey" And LCase(oColumn.ColumnName) <> "pkeypage" And LCase(oColumn.ColumnName) <> "pkeycatalog" Then
                                                sColumns = sColumns & oColumn.ColumnName & ", "
                                            End If
                                        Next

                                        sColumns = Mid(sColumns, 1, sColumns.Length - 2)
                                        sNewPkey = getNewPkey()
                                        sSqlQuery = "INSERT INTO " & sTableName & "(PKEY,PKEYPAGE, PKEYCATALOG, " & sColumns & ") SELECT '" & sNewPkey & "','" & Session("PasteObject") & "','" & Session("PasteCatalog") & "'," & sColumns & " FROM " & sTableName & " WHERE PKEY='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing
                                        Dim sOrgNewPkey As String = sNewPkey
                                        dbDataSet = New DataSet
                                        sSqlQuery = "select * from t_cat_nodes where pkey='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        'check if nodes exist - yes:
                                        If dbDataSet.Tables(0).Rows.Count > 0 Then
                                            If Session("FromReport") = "true" Then
                                                Session("Father") = dbDataSet.Tables(0).Rows(0)("PPARENTKEY")
                                            End If
                                        End If

                                        If cBoxKeepOccur.Checked Then
                                            sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20) (SELECT '" & sNewPkey & "','" & Session("PasteObject") & "',POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20 FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("Father") & "')"
                                        Else
                                            sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS) (SELECT '" & sNewPkey & "','" & Session("PasteObject") & "',POBJECTTYPE, PGLOBALSTATUS FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("Father") & "')"
                                        End If
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        'pkeychilds check
                                        Dim oPkeyChildsManage As New CPKeyChildsManage(Application("ODBCNAME"))
                                        oPkeyChildsManage.UpdatePkeyChilds(sObject(iObjects), 1)
                                        oPkeyChildsManage = Nothing

                                        Dim alEffectedPkeys As New ArrayList
                                        Session.Add("PasteAsDup", True)
                                        oHandlers.ObjectEvents("ADDOBJECT", sOrgNewPkey, Session("PasteObject"), Session("PasteCatalog"), "DUPLICATE", alEffectedPkeys)

                                        'now for all child nodes (recursive call)
                                        Dim iNewObject As Integer
                                        Dim jNewObject As Integer
                                        Dim kNewObject As Integer

                                        'hs contains the old pkey with the new one (for nodes)
                                        Dim hs As New Hashtable
                                        hs.Add(sNewPkey, sObject(iObjects))
                                        oDeletePkeys.Add(sOrgNewPkey)

                                        For jNewObject = 0 To alEffectedPkeys.Count - 1
                                            hs.Add(alEffectedPkeys(jNewObject), sObject(iObjects))
                                        Next

                                        'duplicate each row and add to hashtable
                                        dbDataSet = New DataSet
                                        sSqlQuery = "select distinct * from t_cat_part where pkey in (select pkey from (select pkey,pparentkey from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sObject(iObjects) & "') t1 where t1.pkey<>'" & sObject(iObjects) & "')"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)

                                        Dim dbDataSetNewObject As DataSet

                                        'for the original copy (copy all recursive items!)
                                        For iNewObject = 0 To dbDataSet.Tables(0).Rows.Count - 1
                                            dbDataSetNewObject = New DataSet
                                            sNewPkey = getNewPkey()
                                            sSqlQuery = "INSERT INTO " & sTableName & "(PKEY,PKEYPAGE, PKEYCATALOG, " & sColumns & ") SELECT '" & sNewPkey & "','','" & Session("PasteCatalog") & "'," & sColumns & " FROM " & sTableName & " WHERE PKEY='" & dbDataSet.Tables(0).Rows(iNewObject).Item("pkey") & "'"
                                            oConn.Open_Dataset(sSqlQuery, dbDataSetNewObject)
                                            dbDataSetNewObject.Dispose()
                                            dbDataSetNewObject = Nothing
                                            If Not hs.ContainsKey(sNewPkey) Then
                                                hs.Add(sNewPkey, dbDataSet.Tables(0).Rows(iNewObject).Item("pkey"))
                                                oDeletePkeys.Add(sNewPkey)
                                            End If
                                        Next

                                        'for the duplicate rows from handler (copy all recursive items for each new instance!)
                                        For jNewObject = 0 To alEffectedPkeys.Count - 1
                                            For iNewObject = 0 To dbDataSet.Tables(0).Rows.Count - 1
                                                dbDataSetNewObject = New DataSet
                                                sNewPkey = getNewPkey()
                                                sSqlQuery = "INSERT INTO " & sTableName & "(PKEY,PKEYPAGE, PKEYCATALOG, " & sColumns & ") SELECT '" & sNewPkey & "','','" & Session("PasteCatalog") & "'," & sColumns & " FROM " & sTableName & " WHERE PKEY='" & dbDataSet.Tables(0).Rows(iNewObject).Item("pkey") & "'"
                                                oConn.Open_Dataset(sSqlQuery, dbDataSetNewObject)
                                                dbDataSetNewObject.Dispose()
                                                dbDataSetNewObject = Nothing
                                                If Not hs.ContainsKey(sNewPkey) Then
                                                    hs.Add(sNewPkey, dbDataSet.Tables(0).Rows(iNewObject).Item("pkey"))
                                                End If
                                            Next
                                        Next

                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        'add to nodes the new objects with relations
                                        Dim sOldPkeyFromNodes As String
                                        Dim sNewPkeyFromNodes As String
                                        Dim sOldParentkeyFromNodes As String
                                        Dim sNewParentkeyFromNodes As String

                                        sSqlQuery = "select * from (select * from t_cat_nodes where pglobalstatus<>4 and pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pkey='" & sObject(iObjects) & "') t1 where t1.pkey<>'" & sObject(iObjects) & "'"
                                        dbDataSet = New DataSet
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        For iNewObject = 0 To dbDataSet.Tables(0).Rows.Count - 1
                                            sOldPkeyFromNodes = dbDataSet.Tables(0).Rows(iNewObject).Item("pkey")
                                            sOldParentkeyFromNodes = dbDataSet.Tables(0).Rows(iNewObject).Item("pparentkey")
                                            If hs.ContainsValue(sOldPkeyFromNodes) And hs.ContainsValue(sOldParentkeyFromNodes) Then
                                                'for each element that duplicate (check the hashtable)
                                                Dim en As IDictionaryEnumerator = hs.GetEnumerator
                                                Dim alTempPkey As New ArrayList
                                                Dim alTempParentPkey As New ArrayList
                                                While en.MoveNext
                                                    If en.Value = sOldPkeyFromNodes Then
                                                        alTempPkey.Add(en.Key)
                                                    End If
                                                    If en.Value = sOldParentkeyFromNodes Then
                                                        alTempParentPkey.Add(en.Key)
                                                    End If
                                                End While

                                                For jNewObject = 0 To alTempPkey.Count - 1
                                                    sNewPkeyFromNodes = alTempPkey(jNewObject)
                                                    sNewParentkeyFromNodes = alTempParentPkey(jNewObject)

                                                    'insert to nodes
                                                    If cBoxKeepOccur.Checked Then
                                                        sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20) (SELECT '" & sNewPkeyFromNodes & "','" & sNewParentkeyFromNodes & "',POBJECTTYPE, PGLOBALSTATUS, PNPROP1, PNPROP2, PNPROP3, PNPROP4, PNPROP5, PNPROP6, PNPROP7, PNPROP8, PNPROP9, PNPROP10, PNPROP11, PNPROP12, PNPROP13, PNPROP14, PNPROP15, PNPROP16, PNPROP17, PNPROP18, PNPROP19, PNPROP20 FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sOldPkeyFromNodes & "' AND PPARENTKEY='" & sOldParentkeyFromNodes & "')"
                                                    Else
                                                        sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS) (SELECT '" & sNewPkeyFromNodes & "','" & sNewParentkeyFromNodes & "',POBJECTTYPE, PGLOBALSTATUS FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & sOldPkeyFromNodes & "' AND PPARENTKEY='" & sOldParentkeyFromNodes & "')"
                                                    End If
                                                    dbDataSetNewObject = New DataSet
                                                    oConn.Open_Dataset(sSqlQuery, dbDataSetNewObject)
                                                    dbDataSetNewObject.Dispose()
                                                    dbDataSetNewObject = Nothing

                                                    'udpate pkeypage from part.
                                                    sSqlQuery = "update t_cat_part set pkeypage='" & sNewParentkeyFromNodes & "' where pkey='" & sNewPkeyFromNodes & "'"
                                                    dbDataSetNewObject = New DataSet
                                                    oConn.Open_Dataset(sSqlQuery, dbDataSetNewObject)
                                                    dbDataSetNewObject.Dispose()
                                                    dbDataSetNewObject = Nothing

                                                    oHandlers.ObjectEvents("ADDOBJECT", sNewPkeyFromNodes, sNewParentkeyFromNodes, Session("PasteCatalog"), "DUPLICATE")
                                                    oDeletePkeys.Add(sNewPkeyFromNodes)
                                                Next
                                                alTempPkey.Clear()
                                                alTempPkey = Nothing
                                                alTempParentPkey.Clear()
                                                alTempParentPkey = Nothing
                                            End If
                                        Next

                                        'if we do the paste only for handler run - delete the new objects
                                        Dim iDeletePkeys As Integer
                                        Dim dsDeletePkeys As DataSet

                                        If bOnlyForHandlerUse.ToLower() = "true" Then
                                            For iDeletePkeys = 0 To oDeletePkeys.Count - 1
                                                dsDeletePkeys = New DataSet
                                                sSqlQuery = "DELETE FROM " & oLWObjectTypes.emNodesTable & " WHERE PKEY='" & oDeletePkeys(iDeletePkeys) & "'"
                                                oConn.Open_Dataset(sSqlQuery, dsDeletePkeys)
                                                sSqlQuery = "DELETE FROM " & sTableName & " WHERE PKEY='" & oDeletePkeys(iDeletePkeys) & "'"
                                                oConn.Open_Dataset(sSqlQuery, dsDeletePkeys)
                                                dsDeletePkeys.Dispose()
                                                dsDeletePkeys = Nothing
                                            Next
                                            'delete all objects that has relation to the main part (father) - because the handler runs and duplicate also the main father child(in all levels) objects.
                                            dsDeletePkeys = New DataSet
                                            sSqlQuery = "DELETE FROM " & sTableName & " WHERE PKEY IN (SELECT PKEY FROM " & oLWObjectTypes.emNodesTable & " where pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pparentkey='" & sOrgNewPkey & "')"
                                            oConn.Open_Dataset(sSqlQuery, dsDeletePkeys)
                                            sSqlQuery = "DELETE FROM " & oLWObjectTypes.emNodesTable & " nodes where EXISTS  (SELECT * FROM t_cat_nodes where pobjecttype=10 connect by prior t_Cat_nodes.pkey=t_Cat_nodes.PPARENTKEY start with pparentkey='" & sOrgNewPkey & "' and nodes.pkey=t_cat_nodes.pkey and nodes.pparentkey=t_cat_nodes.pparentkey)"
                                            oConn.Open_Dataset(sSqlQuery, dsDeletePkeys)
                                            dsDeletePkeys.Dispose()
                                            dsDeletePkeys = Nothing
                                        End If

                                        Session.Remove("PasteAsDup")
                                        oHandlers = Nothing
                                        'End Handlers

                                    Case 3
                                        '3 - paste as weblink(generalink)
                                        sNewPkey = getNewPkey()
                                        'Insert into nodes weblink
                                        dbDataSet = New DataSet
                                        sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS) VALUES ('" & sNewPkey & "','" & Session("PasteObject") & "',10, 0)"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        If sTableName = oLWObjectTypes.emPartTable Then
                                            sWebLink = "'../OnlineSearchRedirect.aspx?pkey=" & sObject(iObjects) & "&pparentkey=" & Session("Father") & "&pkeycatalog=" & Session("CutCopyCatalog") & "'"
                                        Else
                                            'TODO:
                                            'need to cOMPLETE - NETWORK FILE OR NOT!!!! - 21/04/05
                                            sWebLink = "PORGFILENAME"
                                        End If

                                        'Insert into part (as weblink)
                                        dbDataSet = New DataSet
                                        sSqlQuery = "INSERT INTO " & oLWObjectTypes.emPartTable & "(PKEY,PKEYCATALOG,PKEYPAGE,PKEYCHAPTER, pparttemplate, PPROP6, PHEBDESC, PPROP5, PENGDESC, PPROP1, PPROP2, PKEYTYPE, PPROP10) " _
                                                & "SELECT '" & sNewPkey & "','" & Session("PasteCatalog") & "','" & Session("PasteObject") & "','', 'normal_WebLink_E.html', PKEYTYPE, PHEBDESC, PPROP5, PENGDESC, " & sWebLink & ", '_top', '43',PPROP10 FROM " & sTableName & " WHERE PKEY='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        'pkeychilds check
                                        Dim oPkeyChildsManage As New CPKeyChildsManage(Application("ODBCNAME"))
                                        oPkeyChildsManage.UpdatePkeyChilds(sObject(iObjects), 1)
                                        oPkeyChildsManage = Nothing

                                    Case 4
                                        '4 - paste as weblink(treelink)
                                        sNewPkey = getNewPkey()
                                        'Insert into nodes weblink
                                        dbDataSet = New DataSet
                                        sSqlQuery = "INSERT INTO " & oLWObjectTypes.emNodesTable & "(PKEY, PPARENTKEY, POBJECTTYPE, PGLOBALSTATUS) VALUES ('" & sNewPkey & "','" & Session("PasteObject") & "',10, 0)"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        If sTableName = oLWObjectTypes.emPartTable Then
                                            sWebLink = "'../OnlineSearchRedirect.aspx?pkey=" & sObject(iObjects) & "&pparentkey=" & Session("Father") & "&pkeycatalog=" & Session("CutCopyCatalog") & "'"
                                        Else
                                            sWebLink = "PORGFILENAME"
                                        End If

                                        'Insert into part (as weblink)
                                        dbDataSet = New DataSet
                                        sSqlQuery = "INSERT INTO " & oLWObjectTypes.emPartTable & "(PKEY,PKEYCATALOG,PKEYPAGE,PKEYCHAPTER, pparttemplate, PPROP6, PHEBDESC, PPROP5, PENGDESC, PPROP1, PPROP2, PKEYTYPE) " _
                                                & "SELECT '" & sNewPkey & "','" & Session("PasteCatalog") & "','" & Session("PasteObject") & "','', 'normal_WebLink_E.html', PKEYTYPE, PHEBDESC, PPROP5, PENGDESC, " & sWebLink & ", '_blank', '38' FROM " & sTableName & " WHERE PKEY='" & sObject(iObjects) & "'"
                                        oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                        dbDataSet.Dispose()
                                        dbDataSet = Nothing

                                        'pkeychilds check
                                        Dim oPkeyChildsManage As New CPKeyChildsManage(Application("ODBCNAME"))
                                        oPkeyChildsManage.UpdatePkeyChilds(sObject(iObjects), 1)
                                        oPkeyChildsManage = Nothing

                                End Select

                                '''''dbDataSet = New DataSet
                                '''''sSqlQuery = "INSERT INTO " & oLWObjectTypes.emPartTable & "(PKEY,PKEYPAGE," & sColumns & ") SELECT '" & sNewPkey & "' AS PKEY,'" & Session("PasteObject") & "' AS PKEYPAGE," & sColumns & " FROM " & oLWObjectTypes.emPartTable & " WHERE PKEY='" & sObject(iObjects) & "'"
                                '''''oConn.Open_Dataset(sSqlQuery, dbDataSet)

                                '''''dbDataSet.Dispose()
                                '''''dbDataSet = Nothing

                            End If
                        Next
                    Case "Cut"
                        sFatherObj = Session("Father")
                        sObject = Split(sObjects, "|")
                        For iObjects = 0 To sObject.Length - 2
                            If sObject(iObjects) <> "" Then
                                dbDataSet = New DataSet
                                sSqlQuery = "UPDATE " & oLWObjectTypes.emNodesTable & " SET PKEY='" & sObject(iObjects) & "', PPARENTKEY='" & Session("PasteObject") & "' WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & sFatherObj & "'"
                                oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                dbDataSet.Dispose()
                                dbDataSet = Nothing
                                If Not cBoxKeepOccur.Checked Then
                                    dbDataSet = New DataSet
                                    sSqlQuery = "UPDATE " & oLWObjectTypes.emNodesTable & " SET PPROP1=null, PPROP2=null, PPROP3=null, PPROP4=null, PPROP5=null, PPROP6=null, PPROP7=null, PPROP8=null, PPROP9=null, PPROP10=null, PPROP11=null, PPROP12=null, PPROP13=null, PPROP14=null, PPROP15=null, PPROP16=null, PPROP17=null, PPROP18=null, PPROP19=null, PPROP20=null WHERE PKEY='" & sObject(iObjects) & "' AND PPARENTKEY='" & Session("PasteObject") & "' "
                                    oConn.Open_Dataset(sSqlQuery, dbDataSet)
                                    dbDataSet.Dispose()
                                    dbDataSet = Nothing
                                End If
                                Dim oHandlers As CHandlers
                                'Handler check
                                oHandlers = New CHandlers(Application("ODBCNAME"))
                                oHandlers.ObjectEvents("PASTEOBJECT", sObject(iObjects), Session("PasteObject"), Session("PasteCatalog"), "Cut")
                                oHandlers = Nothing
                            End If
                        Next
                End Select
                oAclPermissions.Dispose()
                oAclPermissions = Nothing
            Else
                'no permission to edit

            End If
        End If

        oConn.Close_Connection()
        oConn = Nothing

        oLWObjectTypes = Nothing

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not Page.IsPostBack Then

            If Request.QueryString("KeepOccur") <> "" Then
                cBoxKeepOccur.Checked = CBool(Request.QueryString("KeepOccur"))
                cBoxKeepOccur.Enabled = False
            End If

            Dim bIsCircularReference As Boolean = False
            Dim bIsPermitPasteObjects As Boolean = True 'is class & type are the same as querystring request - class&typeName

            'deep paste not available
            'lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_2').disabled=true;</script>"

            'check if the clipboard is empty:
            If Session("Objects") & "" = "" Then
                btnPaste.Enabled = False
                lblError.Text = "Your clipboard is empty. Please perform copy or cut before performing paste."
                lblError.Visible = True
                lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_0').disabled=true;</script>"
                lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_1').disabled=true;</script>"
                lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_2').disabled=true;</script>"
                lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_3').disabled=true;</script>"
                lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_4').disabled=true;</script>"
                Exit Sub
            End If

            If Request.QueryString("Pkey") <> "" Then

                Dim sSqlQuery As String
                Dim sObject() As String
                Dim iObjects As Integer
                Dim sObjects As String = Session("Objects") & ""
                Dim oTempConn As New CConnection
                Dim dbDataSet As DataSet
                Dim dbRow() As DataRow
                Dim oLWObjectTypes As New LWObjectTypes

                sPasteObject = Request.QueryString("Pkey")
                sPasteCatalog = Request.QueryString("PasteCatalog")
                Session("PasteObject") = sPasteObject
                Session("PasteCatalog") = sPasteCatalog

                sObject = Split(sObjects, "|")

                For iObjects = 0 To sObject.Length - 2
                    If sObject(iObjects) <> "" Then


                        'check for Circular Reference
                        sSqlQuery = "(select  pkey from T_CAT_NODES where (pglobalstatus <> 4) connect by prior T_CAT_NODES.pkey=T_CAT_NODES.pparentkey start with T_CAT_NODES.pkey='" & sObject(iObjects) & "')" _
                                & " union (select '" & sObject(iObjects) & "' FROM DUAL) " _
                                & " union (select pparentkey from t_cat_nodes where pkey='" & sObject(iObjects) & "' and pglobalstatus<>4) "

                        '& "union " _
                        '& "(select  pkey from T_CAT_NODES where (pglobalstatus <> 4) connect by prior T_CAT_NODES.pparentkey=T_CAT_NODES.pkey start with T_CAT_NODES.pkey='" & sObject(iObjects) & "')"

                        If Application("gemAppMode") = "Access" Then
                            oTempConn.Open_Access_Connection(Application("ODBCNAME"))
                        Else
                            oTempConn.Open_Oracle_Connection(Application("ODBCNAME"))
                        End If

                        dbDataSet = New DataSet
                        oTempConn.Open_Dataset(sSqlQuery, dbDataSet)

                        dbRow = dbDataSet.Tables(0).Select("pkey='" & sPasteObject & "'")
                        If dbRow.Length > 0 Then
                            'CircularReference
                            'btnPaste.Enabled = False
                            lblError.Visible = True
                            lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_0').disabled=true;</script>"
                            rdbPaste.SelectedIndex = 1
                            oTempConn.Close_Connection()
                            bIsCircularReference = True
                            dbDataSet.Dispose()
                            dbDataSet = Nothing
                            Exit For
                        End If

                        dbDataSet.Dispose()
                        dbDataSet = Nothing

                        'add name of object to the table
                        sSqlQuery = "SELECT OBJECTS.PKEY,PHEBDESC,POBJECTCLASS, ptypename FROM (SELECT DISTINCT " & oLWObjectTypes.emPartTable & ".PKEY, " & oLWObjectTypes.emPartTable & ".PHEBDESC,10 AS POBJECTCLASS, " & oLWObjectTypes.emObjectTypesTable & ".ptypename FROM " & oLWObjectTypes.emPartTable & "," & oLWObjectTypes.emObjectTypesTable & " WHERE " & oLWObjectTypes.emObjectTypesTable & ".pkey(+) = " & oLWObjectTypes.emPartTable & ".pkeytype" & " and " & oLWObjectTypes.emPartTable & ".PKEY='" & sObject(iObjects) & "' union " _
                                                            & "SELECT DISTINCT " & oLWObjectTypes.emWinObjectTable & ".PKEY, " & oLWObjectTypes.emWinObjectTable & ".PFILEMODIFIEDDATE || ' - ' || " & oLWObjectTypes.emWinObjectTable & ".PDOCNUMBER || ' - ' ||" & oLWObjectTypes.emWinObjectTable & ".PHEBDESC AS PHEBDESC,18 AS POBJECTCLASS , ptypename FROM " & oLWObjectTypes.emWinObjectTable & ", " & oLWObjectTypes.emObjectTypesTable & " WHERE " & oLWObjectTypes.emObjectTypesTable & ".pkey(+) = " & oLWObjectTypes.emWinObjectTable & ".pkeytype" & " and " & oLWObjectTypes.emWinObjectTable & ".PKEY='" & sObject(iObjects) & "' union " _
                                                            & "SELECT DISTINCT " & oLWObjectTypes.emPicturePropertyTable & ".PKEY, " & oLWObjectTypes.emPicturePropertyTable & ".PHEBDESC,3 AS POBJECTCLASS , null FROM " & oLWObjectTypes.emPicturePropertyTable & " WHERE " & oLWObjectTypes.emPicturePropertyTable & ".PKEY='" & sObject(iObjects) & "') OBJECTS "

                        dbDataSet = New DataSet
                        oTempConn.Open_Dataset(sSqlQuery, dbDataSet)
                        Dim oTableRow As New TableRow
                        Dim oTableCell As New TableCell
                        If dbDataSet.Tables(0).Rows.Count > 0 Then
                            'check if object is the same object that the user send via querystring - class&typeName
                            If Request.QueryString("Class") <> "" Then
                                If InStr(Request.QueryString("Class"), "|" & dbDataSet.Tables(0).Rows(0).Item("POBJECTCLASS") & "|") = 0 Then
                                    bIsPermitPasteObjects = False
                                End If
                            End If

                            If Request.QueryString("typeName") <> "" Then
                                If InStr(Request.QueryString("typeName"), "|" & dbDataSet.Tables(0).Rows(0).Item("ptypename") & "|") = 0 Then
                                    bIsPermitPasteObjects = False
                                End If
                            End If

                            If Not bIsPermitPasteObjects Then
                                lblError.Text = "Paste is not allowed because of the object type."
                                lblError.Visible = True
                                dbDataSet.Dispose()
                                dbDataSet = Nothing
                                oTempConn.Close_Connection()
                                btnPaste.Enabled = False
                                Exit For
                            End If

                            tblClipboardObjects.Visible = True
                            oTableCell.Width = New Unit(1, UnitType.Percentage)
                            oTableCell.HorizontalAlign = HorizontalAlign.Center
                            oTableCell.Text = "<IMG id=typeitem height=22 alt=""" & dbDataSet.Tables(0).Rows(0).Item("ptypename") & """ src=""IconTypes/" & dbDataSet.Tables(0).Rows(0).Item("ptypename") & "" & ".gif"">"
                            oTableRow.Cells.Add(oTableCell)
                            oTableCell = New TableCell
                            oTableCell.HorizontalAlign = HorizontalAlign.Left
                            oTableCell.Text = dbDataSet.Tables(0).Rows(0).Item("PHEBDESC") & ""
                            oTableRow.Cells.Add(oTableCell)
                            tblClipboardObjects.Rows.Add(oTableRow)

                        End If

                        dbDataSet.Dispose()
                        dbDataSet = Nothing

                        oTempConn.Close_Connection()

                    End If
                Next

                oLWObjectTypes = Nothing

                If Session("Action") = "Cut" Then
                    If Not bIsCircularReference And bIsPermitPasteObjects Then
                        'cut - only regular paste.
                        btnPaste_Click(sender, e)
                    Else
                        btnPaste.Enabled = False
                        lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_0').disabled=true;</script>"
                        lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_1').disabled=true;</script>"
                        lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_2').disabled=true;</script>"
                        lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_3').disabled=true;</script>"
                        lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_4').disabled=true;</script>"
                        tblClipboardObjects.Visible = False
                        Exit Sub
                    End If
                Else
                    'check if doing force paste via querystring:
                    If Request.QueryString("PasteOption") <> "" Then
                        rdbPaste.SelectedIndex = Request.QueryString("PasteOption")
                        If ((rdbPaste.SelectedIndex = 0 Or rdbPaste.SelectedIndex = 3) And bIsCircularReference) Or Not bIsPermitPasteObjects Then
                            btnPaste.Enabled = False
                            tblClipboardObjects.Visible = False
                            Exit Sub
                        Else
                            btnPaste_Click(sender, e)
                        End If
                    Else
                        If Request.QueryString("DisablePasteOptions") <> "" Then

                            Dim sItemDisablePasteOptions As String
                            For Each sItemDisablePasteOptions In Request.QueryString("DisablePasteOptions").Split("|")
                                If sItemDisablePasteOptions <> "" Then
                                    lblScript.Text = lblScript.Text & "<script language=""JavaScript"">document.getElementById('rdbPaste_" & sItemDisablePasteOptions & "').disabled=true;</script>"
                                End If
                            Next

                        End If
                    End If

                    Dim iPasteOptions As Integer

                    For iPasteOptions = rdbPaste.Items.Count - 1 To 0 Step -1
                        lblScript.Text = lblScript.Text & "<script language=""JavaScript"">if (document.getElementById('rdbPaste_" & iPasteOptions & "').disabled==false) {document.getElementById('rdbPaste_" & iPasteOptions & "').checked=true;}</script>"
                    Next

                End If

                oTempConn = Nothing

            Else

                'paste screen

            End If

        End If

        btnPaste.Attributes.Add("onClick", "JavaScript:this.style.cursor='wait';this.disabled;")

    End Sub

    Private Sub btnPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaste.Click

        Dim iSelectedValue As Integer = 0

        If rdbPaste.SelectedValue <> "" Or Session("Action") = "Cut" Then
            If rdbPaste.SelectedValue <> "" Then
                iSelectedValue = rdbPaste.SelectedValue
            End If
            '0 - paste
            '1 - paste as duplicate
            '2 - deep paste as duplicate
            '3 - paste as weblink
            PasteObject(iSelectedValue)
        End If

        If sObjectType = "18" Then
            Response.Write("<script language=""JavaScript"">try {if (window.opener.top.frames.length != 0) {window.opener.top.frames('Index').RefreshFrames();window.close();}} catch(e){}</script>")
        Else
            If Session("CutCopySelf") = "false" Then
                Response.Write("<script language=""JavaScript"">try {if (window.opener.top.frames.length != 0) {window.opener.top.frames('Index').UpdateNode('" & Session("PasteObject") & "','" & Request.QueryString("ParentKey") & "');window.opener.top.frames('Index').RefreshFrames();window.close();}} catch(e){alert('no refresh error');}</script>")
            Else
                Response.Write("<script language=""JavaScript"">try {if (window.opener.top.frames.length != 0) {window.opener.top.frames('Index').UpdateNode('" & Session("ParentKey") & "','na');window.opener.top.frames('Index').RefreshFrames();window.close();}} catch(e){alert('no refresh error2');}</script>")
            End If
        End If
        Response.End()

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Response.Write("<script language=""JavaScript"">window.close();</script>")
    End Sub
End Class
