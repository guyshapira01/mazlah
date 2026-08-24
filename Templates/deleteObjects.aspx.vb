Public Class deleteFiles
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

    Dim oConn As CConnection
    Dim catalogKey As String
    Dim parentKey As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim sDeletedObjects As String
        Dim sObjects As String()
        Dim iObjects As Integer
        Dim oAclPermissions As cACLPermissions
        Dim bDeletePermanently As String

        catalogKey = Request.QueryString("PkeyCatalog") & ""
        If Request.QueryString("Self").ToLower() = "true" Then
            parentKey = Request.QueryString("Parent") & ""
        Else
            parentKey = Request.QueryString("Pkey") & ""
        End If
        bDeletePermanently = Request.QueryString("DeletePermanently") & ""

        'Open oracle connection
        oConn = New CConnection
        If Application("gemAppMode") = "Access" Then
            oConn.Open_Access_Connection(Application("ODBCNAME"))
        Else
            oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        End If

        sDeletedObjects = Request.QueryString("Objects")
        sObjects = Split(sDeletedObjects, "|")

        Dim sSqlQuery As String
        Dim dbCountDeleteDataSet As Data.DataSet

        For iObjects = 0 To sObjects.Length - 2
            If sObjects(iObjects) <> "" Then
                oAclPermissions = New cACLPermissions(Request.ServerVariables("LOGON_USER"), Application("ODBCNAME"), Application("gemAppMode"), "", oConn)
                If CBool(oAclPermissions.HasPermission(sObjects(iObjects), "P_Delete")) Then
                    If bDeletePermanently = "True" Then
                        DeleteObject(sObjects(iObjects))
                        DeleteObjectsRec(sObjects(iObjects))

                    ElseIf bDeletePermanently = "Childs" Then
                        DeleteObject(sObjects(iObjects), parentKey)
                        Dim dbDataSet As Data.DataSet
                        Dim sSqlQuery2 As String
                        Dim iRow As Integer

                        sSqlQuery2 = "SELECT PKEY FROM T_CAT_NODES WHERE PKEY='" & sObjects(iObjects) & "' AND PGLOBALSTATUS<>4"

                        'Open command
                        dbDataSet = New Data.DataSet
                        oConn.Open_Dataset(sSqlQuery2, dbDataSet)
                        If dbDataSet.Tables(0).Rows.Count = 0 Then
                            DeleteObjectsRec(sObjects(iObjects))
                        End If

                        'For iRow = 0 To dbDataSet.Tables(0).Rows.Count - 1
                        'DeleteObject(dbDataSet.Tables(0).Rows(iRow).Item(0), sObjects(iObjects))
                        'Next

                        dbDataSet.Dispose()
                        dbDataSet = Nothing

                    ElseIf bDeletePermanently = "NotLast" Then

                        Dim dbDataSet As Data.DataSet
                        Dim sSqlQuery2 As String
                        Dim iRow As Integer

                        sSqlQuery2 = "SELECT PKEY FROM T_CAT_NODES WHERE PKEY='" & sObjects(iObjects) & "' AND PGLOBALSTATUS<>4"

                        'Open command
                        dbDataSet = New Data.DataSet
                        oConn.Open_Dataset(sSqlQuery2, dbDataSet)
                        If dbDataSet.Tables(0).Rows.Count > 1 Then
                            DeleteObject(sObjects(iObjects), parentKey)
                        Else
                            Response.Write("<script language=""javascript"">alert('you cannot delete last occurence');</script>")
                        End If
                        dbDataSet.Dispose()
                        dbDataSet = Nothing

                    Else
                        ''''''''''if the object exist only once - delete. otherwise - remove relation.
                        '''''''''sSqlQuery = "SELECT PKEY FROM T_CAT_NODES WHERE PKEY='" & sObjects(iObjects) & "' AND PGLOBALSTATUS<>4" ' pglobalstatus

                        ''''''''''Open command
                        '''''''''dbCountDeleteDataSet = New DataSet
                        '''''''''oConn.Open_Dataset(sSqlQuery, dbCountDeleteDataSet)
                        ''''''''''if the object exist only once 
                        '''''''''If dbCountDeleteDataSet.Tables(0).Rows.Count = 1 Then
                        '''''''''    DeleteObject(sObjects(iObjects))
                        '''''''''    'DeleteObjectsRec(sObjects(iObjects))
                        '''''''''Else
                        '''''''''    DeleteObject(sObjects(iObjects), parentKey)
                        '''''''''    'DeleteObjectsRec(sObjects(iObjects))
                        '''''''''End If
                        '''''''''dbCountDeleteDataSet.Dispose()
                        '''''''''dbCountDeleteDataSet = Nothing

                        'default: like childs
                        DeleteObject(sObjects(iObjects), parentKey)
                        Dim dbDataSet As Data.DataSet
                        Dim sSqlQuery2 As String
                        Dim iRow As Integer

                        sSqlQuery2 = "SELECT PKEY FROM T_CAT_NODES WHERE PKEY='" & sObjects(iObjects) & "' AND PGLOBALSTATUS<>4"

                        'Open command
                        dbDataSet = New Data.DataSet
                        oConn.Open_Dataset(sSqlQuery2, dbDataSet)
                        If dbDataSet.Tables(0).Rows.Count = 0 Then
                            DeleteObjectsRec(sObjects(iObjects))
                        End If

                        'For iRow = 0 To dbDataSet.Tables(0).Rows.Count - 1
                        'DeleteObject(dbDataSet.Tables(0).Rows(iRow).Item(0), sObjects(iObjects))
                        'Next

                        dbDataSet.Dispose()
                        dbDataSet = Nothing

                    End If
                Else
                    Response.Write(oAclPermissions.ShowMessage("You dont have Permission", 1))
                End If
                oAclPermissions.Dispose()
                oAclPermissions = Nothing
            End If
        Next

        oConn.Close_Connection()
        oConn = Nothing

    End Sub

    Sub DeleteObjectsRec(ByVal pkey As String)
        Dim dbDataSet As Data.DataSet
        Dim sSqlQuery As String
        Dim iRow As Integer

        sSqlQuery = "SELECT PKEY FROM T_CAT_NODES WHERE PPARENTKEY='" & pkey & "'"

        'Open command
        dbDataSet = New Data.DataSet
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        For iRow = 0 To dbDataSet.Tables(0).Rows.Count - 1
            DeleteObject(dbDataSet.Tables(0).Rows(iRow).Item(0), pkey)
            DeleteObjectsRec(dbDataSet.Tables(0).Rows(iRow).Item(0))
        Next

        dbDataSet.Dispose()
        dbDataSet = Nothing

    End Sub

    Sub DeleteObjectsRec(ByVal pkey As String, ByVal pparentkey As String)
        Dim dbDataSet As Data.DataSet
        Dim sSqlQuery As String
        Dim iRow As Integer

        sSqlQuery = "SELECT PKEY FROM T_CAT_NODES WHERE PPARENTKEY='" & pkey & "'"

        'Open command
        dbDataSet = New Data.DataSet
        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        For iRow = 0 To dbDataSet.Tables(0).Rows.Count - 1
            DeleteObject(dbDataSet.Tables(0).Rows(iRow).Item(0), pkey)
            DeleteObjectsRec(dbDataSet.Tables(0).Rows(iRow).Item(0))
        Next

        dbDataSet.Dispose()
        dbDataSet = Nothing

    End Sub

    Sub DeleteObject(ByVal pkey As String, Optional ByVal pParentkey As String = "")
        Dim dbDeleteDataSet As Data.DataSet
        Dim sSqlQuery As String
        Dim oHandlers As CHandlers

        If pParentkey <> "" Then
            sSqlQuery = "UPDATE T_CAT_NODES SET PGLOBALSTATUS='4' WHERE PKEY='" & pkey & "' AND PPARENTKEY='" & pParentkey & "'"
        Else
            sSqlQuery = "UPDATE T_CAT_NODES SET PGLOBALSTATUS='4' WHERE PKEY='" & pkey & "'"
        End If

        'Open command
        dbDeleteDataSet = New Data.DataSet
        oConn.Open_Dataset(sSqlQuery, dbDeleteDataSet)
        dbDeleteDataSet.Dispose()
        dbDeleteDataSet = Nothing

        'Handler check
        oHandlers = New CHandlers(Application("ODBCNAME"))

        oHandlers.ObjectEvents("DELETEOBJECT", pkey, pParentkey, catalogKey)

        oHandlers = Nothing
        'End Handlers

    End Sub

End Class
