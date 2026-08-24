
Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Public dTblCatalogs As DataTable
    Const ConstHideUpdateDate As String = "הסתר תאריכי עדכון"
    Const ConstShowUpdateDate As String = "הצג תאריכי עדכון"
    Enum enumSecurityType
        enNonClassified = 1
        enLimited = 2
        '''   enClassified = 3
        enReserved = 4
        enSecret = 5
        enMostSecret = 6

        enNonClassifiedE = 11
        enLimitedE = 12
        enReservedE = 14
        enSecretE = 15
        enMostSecretE = 16

    End Enum
#Region "Events"
    Protected Sub CatalogsDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CatalogsDataGrid.ItemDataBound
        'If e.Item.ItemType = ListItemType.Header Then
        Dim TC As TableCell
        If (Session("TRCounter") Is Nothing) Then Session("TRCounter") = 0
        If (Session("EndInd") Is Nothing) Then Session("EndInd") = 0
        If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
            If (Session("FamilyID") Is Nothing) Then Session("FamilyID") = e.Item.DataItem("FAMILY_PKEY").ToString.Trim
            'Dim oLinkBtnFileName As LinkButton = CType(e.Item.Cells(5).FindControl("LinkBtnFileName"), LinkButton)
            e.Item.Attributes.Add("ID", "TRCatalogsDataGrid_" & Session("TRCounter").ToString)
            e.Item.Attributes.Add("FAMILY_PKEY", e.Item.DataItem("FAMILY_PKEY").ToString)
            e.Item.Attributes.Add("FAMILY_NAME", e.Item.DataItem("PFAMILY").ToString)
			e.Item.Attributes.Add("CHILD_FAMILY", e.Item.DataItem("CHILD_FAMILY").ToString)
            e.Item.Attributes.Add("UPDATE_DATE", e.Item.DataItem("PLASTUPDATEDATEFORMAT").ToString)
            e.Item.Attributes.Add("CATDIR", e.Item.DataItem("PDIRECTION").ToString)
            e.Item.Attributes.Add("Style", "display:none")

            If (e.Item.DataItem("FAMILY_PKEY").ToString.Trim <> Session("FamilyID").ToString.Trim) Then
                e.Item.Attributes.Add("EndInd", Session("EndInd") + 1)
                Session("FamilyID") = e.Item.DataItem("FAMILY_PKEY").ToString.Trim
                Session("EndInd") = 0
            Else
                Session("EndInd") = Session("EndInd") + 1
            End If

            Session("TRCounter") = Session("TRCounter") + 1
            'If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
            '    ' e.Item.Attributes.Add("onmouseover", "this.style.font-Color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
            '    If e.Item.ItemType = ListItemType.AlternatingItem Then
            '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.ForeColor) & "'")
            '    Else
            '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.ForeColor) & "'")
            '    End If
            'End If
        End If
        If (lnbShowUpdateDate.Text = ConstShowUpdateDate) Then
            e.Item.Cells(3).Visible = False
        End If

    End Sub
    Protected Sub CatalogsDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CatalogsDataGrid.PageIndexChanged
        CatalogsDataGrid.CurrentPageIndex = e.NewPageIndex
        Dim dbDataSet As New DataSet
        Dim drArr() As DataRow
        dbDataSet = Session("dbCatalogDataSet")
        drArr = dbDataSet.Tables("Catalogs").Select("FAMILY_PKEY='" & FamilyTreeView.SelectedNode.Value() & "' ", Session("SortExpression").ToString)
        CatalogsDataGrid.DataSource = drArr
        CatalogsDataGrid.DataBind()
    End Sub
    Protected Sub ddlFamily_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFamily.SelectedIndexChanged
        'UpdateCatalogsCombo()
        'If ddlFamily.SelectedIndex > 0 Then
        '    rdbAllCatalogs.Checked = False
        'End If
    End Sub
    'Search Parts
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        
    End Sub
    Protected Sub PartSearchDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles PartSearchDataGrid.PageIndexChanged
        
    End Sub
    Protected Sub PartSearchDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles PartSearchDataGrid.SortCommand

        
    End Sub
    Protected Sub lnbShowUpdateDate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        
    End Sub
    Protected Sub CatalogsDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles CatalogsDataGrid.SortCommand
        


    End Sub
    '================Search Catalog==========================
    Protected Sub btnSearchCatalog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchCatalog.Click
        
    End Sub
    Protected Sub CatalogSearchDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles CatalogSearchDataGrid.PageIndexChanged
        

    End Sub
    Protected Sub CatalogSearchDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles CatalogSearchDataGrid.SortCommand
        
    End Sub
    Protected Sub rdbPicture_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txbProduce.Enabled = False
        txbMakat.Enabled = False
        txbProduce.Text = "לא זמין"
        txbMakat.Text = "לא זמין"
    End Sub
    Protected Sub rdbDetail_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txbProduce.Enabled = True
        txbMakat.Enabled = True
        txbProduce.Text = ""
        txbMakat.Text = ""
    End Sub
    Sub dgBoundItems(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        Dim cell As TableCell
        For Each cell In e.Item.Cells

            'Response.Write("R" + e.Item.DataSetIndex.ToString()); = row
            'Response.Write("C" + e.Item.Cells.GetCellIndex(cell)+" "); = column

            If (e.Item.Cells.GetCellIndex(cell) = 1 And e.Item.DataSetIndex >= 0) Then

                If e.Item.DataItem("PDIRECTION").ToString() <> "" Then
                    cell.Style.Add("direction", "ltr")
                Else
                    cell.Style.Add("direction", "rtl")
                End If
            End If
        Next
    End Sub
    Protected Sub boundPartItems(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles PartSearchDataGrid.ItemDataBound
        Dim cell As TableCell

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If (e.Item.DataItem("PMAKAT").ToString().Length > 4) Then
                CType(e.Item.FindControl("PartMakatLbl"), Label).Text = _
                    e.Item.DataItem("PMAKAT").ToString().Substring(0, 4) & "-" & _
                    e.Item.DataItem("PMAKAT").ToString().Substring(4, (e.Item.DataItem("PMAKAT").ToString().Length - 4))
            Else
                CType(e.Item.FindControl("PartMakatLbl"), Label).Text = _
                  e.Item.DataItem("PMAKAT").ToString()
            End If
            For Each cell In e.Item.Cells

                'Response.Write("R" + e.Item.DataSetIndex.ToString()); = row
                'Response.Write("C" + e.Item.Cells.GetCellIndex(cell)+" "); = column
                'e.Item.Cells.GetCellIndex(cell) = 1 And
                If (e.Item.DataSetIndex >= 0) Then

                    If e.Item.DataItem("PDIRECTION").ToString() <> "" Then
                        cell.Style.Add("direction", "ltr")
                    Else
                        cell.Style.Add("direction", "rtl")
                    End If
                End If
            Next
        End If
    End Sub
#End Region
#Region "Methods"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        

	
    End Sub
    Private Sub LoadClientScript()
        

    End Sub
    Private Sub WriteClientScript()
        Dim strScript As String
        strScript = "var areaVSsubject=new Array(); "
        Dim x As Integer
        For x = 0 To dTblCatalogs.Rows.Count - 1
            strScript += "areaVSsubject[" & x & "] = ['" & dTblCatalogs.Rows(x)("PHEBDESC").ToString().Replace("'", """") & "','" & dTblCatalogs.Rows(x)("PKEY").ToString().Replace("'", """") & "','" & dTblCatalogs.Rows(x)("FAMILY_PKEY").ToString().Replace("'", """") & "'];"
        Next

        Response.Write("<script>" & strScript & "</script>")

    End Sub
    Private Sub GetOpenMsg()

        

    End Sub
    Private Sub CheckAdmin()
        
    End Sub
    Private Sub Fill_FamilyDDL(ByRef DefaultDataSet As DataSet)
        ddlFamily.Items.Add(New ListItem("כל המשפחות", "-1"))
        ddlFamilySelect.Items.Add(New ListItem("כל המשפחות", "-1"))
        Dim Row As DataRow
        For Each Row In DefaultDataSet.Tables("Family").Rows
            ddlFamily.Items.Add(New ListItem(Row("PFAMILY"), Row("PKEY")))
            ddlFamilySelect.Items.Add(New ListItem(Row("PFAMILY"), Row("PKEY")))
        Next

    End Sub
    Private Sub Fill_TreeViewFamily(ByRef ParentItem As TreeNode, _
                                     ByRef FamilyDataSet As DataSet, ByRef orderNum As Integer) 'As Boolean
        
    End Sub
	Private Sub buildDataSet(ByRef dbDataSet As DataSet, ByRef oConn As CConnectionR) ', ByVal strFamilyKey As String
  

    End Sub
    Sub UpdateCatalogsCombo(Optional ByVal SelectedKey As String = "")
        

    End Sub
    Protected Function GetImage(ByVal oImageFileExtension As Object) As String
        Dim strImageFileExtension As String = ""
        If (Not IsDBNull(oImageFileExtension)) Then
            strImageFileExtension = oImageFileExtension.ToString
        End If

        GetImage = ""
        Select Case strImageFileExtension
            Case "PDF", "pdf"
                GetImage = "Templates/IconTypes/PDF.gif"
            Case "doc", "DOC"
                GetImage = "Templates/IconTypes/Word_Document.gif"
            Case "TXT", "txt"
                GetImage = "Templates/IconTypes/text.gif"
            Case "gif", "GIF", "jpg", "JPG", "jpeg", "JPEG", "tiff", "TIFF", "tif", "TIF"
                GetImage = "Templates/IconTypes/gif.gif"
            Case "xls", "XLS"
                GetImage = "Templates/IconTypes/Excel Document.gif"
            Case "html", "htm", "HTML", "HTM"
                GetImage = "Templates/IconTypes/Verint_HTML.gif"
            Case "pps", "ppt", "PPS", "PPT"
                GetImage = "Templates/IconTypes/PowerPoint Document.gif"
            Case Else
                GetImage = "Templates/IconTypes/document.gif"

        End Select
    End Function
    Protected Function GetClassification(ByVal oClass As Object) As String
        Dim nClass As Integer
        If (Not IsDBNull(oClass)) Then
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
    Private Function GetNews() As String
        
    End Function
    Private Function GetNewCatalogs() As String
        
    End Function
    Private Sub ShowPrintedCatalogs()
        
    End Sub
	Private Sub ShowAllCatalogs(ByVal orderby As String)
        
    End Sub
    Sub ShowHideDates(ByVal sender As Object, ByVal e As System.EventArgs)
        AllCatalogsDataGrid.Columns(4).Visible = Not AllCatalogsDataGrid.Columns(4).Visible
        If AllCatalogsDataGrid.Columns(4).Visible Then
            lblShowHide.Text = "הסתר תאריכי עדכון"
        Else
            lblShowHide.Text = "הצג תאריכי עדכון"
        End If
        TabContainer1.ActiveTabIndex = 4
    End Sub
#End Region
    'Protected Sub FamilyTreeView_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FamilyTreeView.SelectedNodeChanged
    '    lnbShowUpdateDate.Visible = True
    '    Dim strFamilyKey As String = FamilyTreeView.SelectedNode.Value()
    '    Dim dbDataSet As New DataSet
    '    Dim drArr() As DataRow
    '    Dim strError As String = ""
    '    If (Session("dbCatalogDataSet") Is Nothing) Then
    '        Dim oConn As New CConnectionR
    '        oConn.Open_Oracle_Connection(Application("OdbcName"), strError)
    '        If (strError.Trim <> "") Then
    '            lblError.Text = strError
    '            lblError.Visible = True
    '            Exit Sub
    '        End If
    '        buildDataSet(dbDataSet, oConn)
    '        Session("dbCatalogDataSet") = dbDataSet
    '        oConn.Close_Connection(strError)
    '        If (strError.Trim <> "") Then
    '            lblError.Text = strError
    '            lblError.Visible = True
    '            Exit Sub
    '        End If
    '        ' ViewState.Add("Family", FamilyTreeView.SelectedNode.Value())
    '    Else
    '        dbDataSet = Session("dbCatalogDataSet")
    '    End If



    '    drArr = dbDataSet.Tables("Catalogs").Select("FAMILY_PKEY='" & FamilyTreeView.SelectedNode.Value() & "' ")


    '    CatalogsDataGrid.CurrentPageIndex = 0
    '    CatalogsDataGrid.DataSource = drArr
    '    CatalogsDataGrid.DataBind()
    '    lblOpenMsg.Visible = False
    '    UpdatePanelFamilyCatalog.Update()
    'End Sub

    'Private Sub buildDataSet(ByRef dbDataSet As DataSet, ByRef oConn As CConnectionR)
    '    Dim expression As String = " AND T_CAT_FAMILY.PKEY='" & FamilyTreeView.SelectedNode.Value() & "' "
    '    Dim sSqlQuery As String = "SELECT T_CAT_CATALOG.PKEY AS PKEY, T_CAT_CATALOG.PINDEXTEMPLATE, TO_CHAR(T_CAT_CATALOG.PCATALOGCREATEDATE,'DD/MM/YYYY') AS PCATALOGCREATEDATEFORMAT, PHEBDESC, PCATALOGMAKAT, PREVISION, PTRANSFERTOZAHALDATE, PFAMILY ,T_CAT_FAMILY.PKEY AS FAMILY_PKEY   FROM T_CAT_CATALOG ,T_CAT_FAMILY where t_cat_catalog.PCATALOGFAMILY = t_cat_family.PKEY(+)" & expression & " ORDER BY " & System.Configuration.ConfigurationManager.AppSettings("SortExpression")
    '    oConn.Open_Dataset(sSqlQuery, dbDataSet, "Catalogs")
    'End Sub



    'Protected Sub PartSearchDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles PartSearchDataGrid.ItemDataBound
    '    If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
    '        'Dim oLinkBtnFileName As LinkButton = CType(e.Item.Cells(5).FindControl("LinkBtnFileName"), LinkButton)

    '        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            ' e.Item.Attributes.Add("onmouseover", "this.style.font-Color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            If e.Item.ItemType = ListItemType.AlternatingItem Then
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.ForeColor) & "'")
    '            Else
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.ForeColor) & "'")
    '            End If
    '        End If
    '    End If
    'End Sub


    'GetImage


    'Protected Sub CatalogSearchDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles CatalogSearchDataGrid.ItemDataBound
    '    If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
    '        'Dim oLinkBtnFileName As LinkButton = CType(e.Item.Cells(5).FindControl("LinkBtnFileName"), LinkButton)

    '        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            ' e.Item.Attributes.Add("onmouseover", "this.style.font-Color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            If e.Item.ItemType = ListItemType.AlternatingItem Then
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.ForeColor) & "'")
    '            Else
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.ForeColor) & "'")
    '            End If
    '        End If
    '    End If
    'End Sub

    'Protected Sub AllCatalogsDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles AllCatalogsDataGrid.ItemDataBound
    '    If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
    '        'Dim oLinkBtnFileName As LinkButton = CType(e.Item.Cells(5).FindControl("LinkBtnFileName"), LinkButton)

    '        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            ' e.Item.Attributes.Add("onmouseover", "this.style.font-Color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            If e.Item.ItemType = ListItemType.AlternatingItem Then
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.ForeColor) & "'")
    '            Else
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.ForeColor) & "'")
    '            End If
    '        End If
    '    End If
    'End Sub


    'Protected Sub AllCatalogsDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles AllCatalogsDataGrid.SortCommand
    '    If Session("SortAllCatalogExpression").ToString = e.SortExpression Then
    '        Session("SortAllCatalogExpression") = e.SortExpression & " DESC"
    '    Else
    '        Session("SortAllCatalogExpression") = e.SortExpression
    '    End If
    '    'Dim strQuerySelect As String = Session("SortAllCatalogExpression")

    '    Dim dbDataSet As New DataSet()

    '    dbDataSet = Session("dbAllCatalogDataSet")
    '    dbDataSet.Tables("AllCatalogs").DefaultView.Sort = Session("SortAllCatalogExpression").ToString


    '    AllCatalogsDataGrid.DataSource = dbDataSet.Tables("AllCatalogs").DefaultView
    '    AllCatalogsDataGrid.DataBind()
    'End Sub





    'Protected Sub ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If (TabContainer1.ActiveTabIndex = 3) Then
    '        ShowAllCatalogs()
    '    End If
    'End Sub

    'Protected Sub DoServerSideValidation(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If (TabContainer1.ActiveTabIndex = 3) Then
    '        ShowAllCatalogs()
    '    End If
    'End Sub



    'Protected Sub PrintCatalogDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles PrintCatalogDataGrid.ItemDataBound
    '    If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
    '        'Dim oLinkBtnFileName As LinkButton = CType(e.Item.Cells(5).FindControl("LinkBtnFileName"), LinkButton)

    '        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
    '            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            ' e.Item.Attributes.Add("onmouseover", "this.style.font-Color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.SelectedItemStyle.ForeColor) & "'")
    '            If e.Item.ItemType = ListItemType.AlternatingItem Then
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.AlternatingItemStyle.ForeColor) & "'")
    '            Else
    '                e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.BackColor) & "';this.style.color='" & System.Drawing.ColorTranslator.ToHtml(AllCatalogsDataGrid.ItemStyle.ForeColor) & "'")
    '            End If
    '        End If
    '    End If
    'End Sub
    Protected Sub AllCatalogsDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles AllCatalogsDataGrid.SortCommand
        
        'WriteClientScript()
    End Sub
    Protected Sub PrintCatalogDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles PrintCatalogDataGrid.SortCommand
        
    End Sub
    Protected Sub ddlCatalogList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCatalogList.SelectedIndexChanged
        'If ddlCatalogList.SelectedIndex > 0 Then
        '    rdbAllCatalogs.Checked = False
        'End If
    End Sub

    
    Protected Sub rdbAllCatalogs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbAllCatalogs.CheckedChanged
        'If rdbAllCatalogs.Checked Then

        '    ddlFamily.SelectedIndex = 0
        '    ddlCatalogList.SelectedIndex = 0
        'End If
    End Sub
	    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        
    End Sub

    Protected Sub btnSearchText_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        

    End Sub

  
    Protected Sub textSearchDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles textSearchDataGrid.PageIndexChanged
        


    End Sub

    Protected Sub textSearchDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles textSearchDataGrid.SortCommand
        

    End Sub
End Class
