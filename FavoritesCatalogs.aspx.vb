

Imports System.Data.OleDb

Public Class FavoritesCatalogs
    Inherits System.Web.UI.Page

    Private oConn As CConnection

    Private sSqlQuery As String

    Private oAuthentication As cAuthentication
    Enum enumSecurityType
        enNonClassified = 1
        enLimited = 2
        enReserved = 4
        enSecret = 5
        enMostSecret = 6

        enNonClassifiedE = 11
        enLimitedE = 12
        enReservedE = 14
        enSecretE = 15
        enMostSecretE = 16

    End Enum
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        'put version
        'LWVersion.Text = LWVersion.Text & System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
        If Not Page.IsPostBack Then
            initPage()
        End If

        ''End If
    End Sub

    Private Sub initPage()
        dbCatalogList.PageSize = Application("RecordsPerPage")
        oAuthentication = Nothing
        ''If Not Page.IsPostBack Then
        If ViewState("SortExpression") Is Nothing Then
            ViewState("SortExpression") = "First(T_CAT_FAMILY.PFAMILY), PHEBDESC"
        End If
        Dim dbDataSet As New Data.DataSet
        dbDataSet = buildDataSet()
        dbCatalogList.DataSource = dbDataSet
        dbCatalogList.DataBind()
        lblCDName.Text = getCDName()

    End Sub

    Private Function getCDName() As String
        oConn = New CConnection
        If Application("gemAppMode") = "Access" Then
            oConn.Open_Access_Connection(Application("ODBCNAME"))

        End If

        If Application("gemAppMode") = "Access" Then

            sSqlQuery = "SELECT * from T_CAT_MAINOPTION where PCNAME='CDNAME'"
        End If
        'response.write(sSqlQuery)
        'Open command
        Dim dbDataSet As New Data.DataSet
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        getCDName = ""
        If dbDataSet.Tables.Count > 0 AndAlso dbDataSet.Tables(0).Rows.Count > 0 Then
            getCDName = dbDataSet.Tables(0).Rows(0)("PCVALUE").ToString()
        End If
    End Function



    Private Sub AddCatalogToFavorites(catalogId As String)
        ''Dim connStr As String = ConfigurationManager.ConnectionStrings("MyAccessConn").ConnectionString
        Dim pszFileName = Application("ODBCNAME")
        oConn = New CConnection
        oConn.Open_Access_Connection(Application("ODBCNAME"))
        sSqlQuery = "SELECT COUNT(*) FROM Favorites WHERE PKEYCATALOG = '" & catalogId & "'"
        Dim dbDataSet As New Data.DataSet

        oConn.Open_Dataset(sSqlQuery, dbDataSet)
        If dbDataSet.Tables.Count > 0 AndAlso dbDataSet.Tables(0).Rows.Count > 0 Then
            If dbDataSet.Tables(0).Rows(0)(0) > 0 Then
                oConn.SqlConnection.Close() ' <-- Close connection
                Exit Sub
            End If
        End If



        Dim query As String = "INSERT INTO Favorites (PKEYCATALOG, AddedDate) VALUES (?, ?)"
        Using cmd As New OleDbCommand(query, oConn.SqlConnection)
            cmd.Parameters.AddWithValue("?", catalogId.ToString())
            ''cmd.Parameters.AddWithValue("?", DateTime.Now)
            cmd.Parameters.AddWithValue("?", DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"), "MM/dd/yyyy HH:mm:ss", Nothing))

            cmd.ExecuteNonQuery()
        End Using
        oConn.SqlConnection.Close() ' <-- Close connection

        '' initPage()
    End Sub
    Private Sub RemoveCatalogFromFavorites(catalogId As String)
        ''Dim connStr As String = ConfigurationManager.ConnectionStrings("MyAccessConn").ConnectionString
        Dim pszFileName = Application("ODBCNAME")
        oConn = New CConnection
        oConn.Open_Access_Connection(Application("ODBCNAME"))



        Dim query As String = "DELETE FROM Favorites WHERE PKEYCATALOG=?"
        Using cmd As New OleDbCommand(query, oConn.SqlConnection)
            cmd.Parameters.AddWithValue("?", catalogId.ToString())
            cmd.ExecuteNonQuery()
        End Using
        oConn.SqlConnection.Close() ' <-- Close connection

        '' initPage()
    End Sub


    Private Function buildDataSet() As Data.DataSet
        'Open oracle connection
        oConn = New CConnection
        If Application("gemAppMode") = "Access" Then
            oConn.Open_Access_Connection(Application("ODBCNAME"))
        Else
            oConn.Open_Oracle_Connection(Application("ODBCNAME"))
        End If

        If Application("gemAppMode") = "Access" Then
            'sSqlQuery = "SELECT T_CAT_CATALOG.PKEY, T_CAT_CATALOG.PINDEXTEMPLATE, T_CAT_CATALOG.PCATALOGCREATEDATE AS PCATALOGCREATEDATEFORMAT, PHEBDESC, PCATALOGMAKAT, PREVISION, PTRANSFERTOZAHALDATE, PFAMILY FROM T_CAT_CATALOG LEFT JOIN T_CAT_FAMILY ON t_cat_catalog.PCATALOGFAMILY = t_cat_family.PKEY ORDER BY " & ViewState.Item("SortExpression")
            Dim sb As New System.Text.StringBuilder()

            sb.AppendLine("SELECT ")
            sb.AppendLine("    T_CAT_CATALOG.PKEY, ")
            sb.AppendLine("    T_CAT_CATALOG.PINDEXTEMPLATE, ")
            sb.AppendLine("    T_CAT_CATALOG.PCATALOGCREATEDATE AS PCATALOGCREATEDATEFORMAT, ")
            sb.AppendLine("    T_CAT_CATALOG.PHEBDESC, ")
            sb.AppendLine("    T_CAT_CATALOG.PCATALOGMAKAT, ")
            sb.AppendLine("    T_CAT_CATALOG.PREVISION, ")
            sb.AppendLine("    T_CAT_CATALOG.PTRANSFERTOZAHALDATE, ")
            sb.AppendLine("    First(T_CAT_FAMILY.PFAMILY) AS PFAMILY, ")
            sb.AppendLine("    T_CAT_CATALOG.PCATALOGSIVUG, ")
            sb.AppendLine("    T_CAT_CATALOG.PIMPORTSTATUS, ")
            sb.AppendLine("    Favorites.PKEYCATALOG ")
            sb.AppendLine("FROM (T_CAT_CATALOG ")
            sb.AppendLine("    LEFT JOIN T_CAT_FAMILY ON T_CAT_CATALOG.PCATALOGFAMILY = T_CAT_FAMILY.PKEY) ")
            sb.AppendLine("    INNER JOIN Favorites ON Favorites.PKEYCATALOG = T_CAT_CATALOG.PKEY ")
            sb.AppendLine("GROUP BY ")
            sb.AppendLine("    T_CAT_CATALOG.PKEY, ")
            sb.AppendLine("    T_CAT_CATALOG.PINDEXTEMPLATE, ")
            sb.AppendLine("    T_CAT_CATALOG.PCATALOGCREATEDATE, ")
            sb.AppendLine("    T_CAT_CATALOG.PHEBDESC, ")
            sb.AppendLine("    T_CAT_CATALOG.PCATALOGMAKAT, ")
            sb.AppendLine("    T_CAT_CATALOG.PREVISION, ")
            sb.AppendLine("    T_CAT_CATALOG.PTRANSFERTOZAHALDATE, ")
            sb.AppendLine("    T_CAT_CATALOG.PCATALOGSIVUG, ")
            sb.AppendLine("    T_CAT_CATALOG.PIMPORTSTATUS, ")
            sb.AppendLine("    Favorites.PKEYCATALOG ")
            sb.AppendLine("ORDER BY  " & ViewState.Item("SortExpression"))
            sSqlQuery = sb.ToString()
            'Else
            'sSqlQuery = "Select T_CAT_CATALOG.PKEY, T_CAT_CATALOG.PINDEXTEMPLATE, TO_CHAR(T_CAT_CATALOG.PCATALOGCREATEDATE,'DD/MM/YYYY') AS PCATALOGCREATEDATEFORMAT, PHEBDESC, PCATALOGMAKAT, PREVISION, PTRANSFERTOZAHALDATE, PFAMILY FROM T_CAT_CATALOG ,T_CAT_FAMILY where t_cat_catalog.PCATALOGFAMILY = t_cat_family.PKEY(+) ORDER BY " & ViewState.Item("SortExpression")
        End If
        'response.write(sSqlQuery)
        'Open command
        Dim dbDataSet As New Data.DataSet
        oConn.Open_Dataset(sSqlQuery, dbDataSet)

        'filter the dataset by filter from ACL
        'Dim iRows As Integer
        'Dim oAclPermissions As cACLPermissions
        'Dim sCatalogPkey As String
        'Dim oLWObjectTypes As New LWObjectTypes

        'oAclPermissions = New cACLPermissions(Request.ServerVariables("LOGON_USER"), Application("ODBCNAME"), Application("gemAppMode"), oLWObjectTypes.emCatalog, oConn)
        '      if dbDataSet.Tables.Count > 0 then
        '	For iRows = 0 To dbDataSet.Tables(0).Rows.Count - 1
        '		sCatalogPkey = dbDataSet.Tables(0).Rows(iRows).Item("PKEY")
        '              If Not CBool(oAclPermissions.HasPermission(sCatalogPkey, "P_Show")) Then

        '                  dbDataSet.Tables(0).Rows(iRows).Delete()
        '              End If
        '          Next
        'end if
        'oAclPermissions.Dispose()
        'oLWObjectTypes = Nothing
        'oAclPermissions = Nothing
        Return dbDataSet
    End Function
    Protected Function GetClassification(ByVal oClass As Object) As String
        Dim nClass As Integer
        If Not IsDBNull(oClass) Then
            nClass = CInt(oClass)
        End If

        GetClassification = ""
        Select Case nClass
            Case Is = enumSecurityType.enNonClassified
                GetClassification = "בלמ''ס"
            Case Is = enumSecurityType.enReserved
                GetClassification = "שמור"
            Case Is = enumSecurityType.enSecret
                GetClassification = "סודי"
            Case Is = enumSecurityType.enMostSecret
                GetClassification = "סודי ביותר"
            Case Is = enumSecurityType.enLimited
                GetClassification = "מוגבל"
            Case Is = enumSecurityType.enNonClassifiedE
                GetClassification = "Unclassified"
            Case Is = enumSecurityType.enReservedE
                GetClassification = "Confidential"
            Case Is = enumSecurityType.enSecretE
                GetClassification = "Secret"
            Case Is = enumSecurityType.enMostSecretE
                GetClassification = "Top Secret"
            Case Is = enumSecurityType.enLimitedE
                GetClassification = "Limited"
        End Select
    End Function


    Private Sub dbCatalogList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dbCatalogList.SortCommand
        If ViewState.Item("SortExpression").ToString().IndexOf(e.SortExpression) >= 0 And ViewState.Item("SortExpression").ToString().IndexOf(" DESC") < 1 Then
            ViewState.Item("SortExpression") = e.SortExpression & " DESC"
        Else
            ViewState.Item("SortExpression") = e.SortExpression
        End If
        If ViewState.Item("SortExpression").ToString().IndexOf("PFAMILY") > 0 Then
            ViewState.Item("SortExpression") = ViewState.Item("SortExpression") + ",PHEBDESC"
        End If
        'response.write(ViewState.Item("SortExpression"))
        Dim dbDataSet As New Data.DataSet
        dbDataSet = buildDataSet()
        dbCatalogList.CurrentPageIndex = 0
        dbCatalogList.DataSource = dbDataSet
        dbCatalogList.DataBind()

    End Sub

    Private Sub dbCatalogList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dbCatalogList.PageIndexChanged
        Dim dbDataSet As New Data.DataSet
        dbDataSet = buildDataSet()
        dbCatalogList.CurrentPageIndex = e.NewPageIndex
        dbCatalogList.DataSource = dbDataSet
        dbCatalogList.DataBind()

    End Sub
    Private Sub dbCatalogList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dbCatalogList.ItemCommand
        If e.CommandName = "AddFavorite" Then
            Dim catalogId As String = e.CommandArgument.ToString()
            AddCatalogToFavorites(catalogId)
        End If
        If e.CommandName = "RemoveFavorite" Then
            Dim catalogId As String = e.CommandArgument.ToString()
            RemoveCatalogFromFavorites(catalogId)
        End If

        ' Refresh grid
        initPage()
    End Sub

    Private Sub CatalogList_InitComplete(sender As Object, e As EventArgs) Handles Me.InitComplete

    End Sub
End Class
