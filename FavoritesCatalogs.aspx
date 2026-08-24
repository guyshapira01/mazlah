<%@ Page Language="vb" AutoEventWireup="false" CodeFile="FavoritesCatalogs.aspx.vb" Inherits="FavoritesCatalogs"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CatalogList</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1255">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	
<style type="text/css">
html, body {
    margin: 0;
    padding: 0;
    background: #f4f7fb;
    color: #13213c;
    direction: rtl;
    font-family: "Segoe UI", Arial, sans-serif;
}
* { box-sizing: border-box; }

.catalog-page {
    min-height: 100vh;
    padding: 22px;
}
.catalog-shell {
    width: 100%;
    max-width: 1460px;
    margin: 0 auto;
}

/* Header */
.catalog-topbar {
    min-height: 78px;
    padding: 14px 20px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 20px;
    border: 1px solid #0c4384;
    border-radius: 12px;
    background: linear-gradient(to bottom, #1c61ad, #104985);
    box-shadow: 0 4px 15px rgba(17,56,101,.18);
    color: #fff;
}
.catalog-brand {
    display: flex;
    align-items: center;
    gap: 13px;
    min-width: 0;
}
.catalog-brand-icon {
    width: 46px;
    height: 46px;
    padding: 6px;
    border-radius: 10px;
    background: rgba(255,255,255,.12);
    border: 1px solid rgba(255,255,255,.18);
}
.catalog-brand-small {
    margin-bottom: 2px;
    color: #cfe3fb;
    font-size: 12px;
    font-weight: 600;
}
.catalog-brand-title {
    color: #fff;
    font-size: 23px;
    line-height: 1.15;
    font-weight: 700;
}
.catalog-top-hint {
    color: #d8e8fa;
    font-size: 13px;
    font-weight: 600;
}

/* Actions */
.catalog-toolbar {
    margin: 14px 0 12px;
    padding: 10px 12px;
    min-height: 58px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 14px;
    border: 1px solid #d3dfec;
    border-radius: 10px;
    background: #fff;
    box-shadow: 0 2px 9px rgba(20,48,85,.07);
}
.catalog-toolbar-actions {
    display: flex;
    align-items: center;
    gap: 8px;
}
.catalog-btn {
    min-height: 38px;
    padding: 0 13px;
    display: inline-flex;
    align-items: center;
    gap: 8px;
    border: 1px solid #c8d9eb;
    border-radius: 8px;
    background: #f8fbff;
    color: #174f8f;
    text-decoration: none;
    font-size: 13px;
    font-weight: 700;
    transition: all .13s ease;
}
.catalog-btn:hover {
    background: #edf5ff;
    border-color: #91b6df;
    color: #0d4385;
    transform: translateY(-1px);
}
.catalog-btn.favorite {
    background: #fffaf0;
    color: #805900;
    border-color: #ead49d;
}
.catalog-btn.favorite:hover {
    background: #fff3d6;
    border-color: #dcb95d;
}
.catalog-btn img {
    width: 20px;
    height: 20px;
    object-fit: contain;
}
.catalog-toolbar-note {
    color: #7a8da4;
    font-size: 12px;
}

/* Grid card */
.catalog-grid-card {
    overflow: hidden;
    border: 1px solid #d3dfec;
    border-radius: 11px;
    background: #fff;
    box-shadow: 0 3px 13px rgba(20,48,85,.09);
}
.catalog-grid {
    width: 100% !important;
    margin: 0 !important;
    border: 0 !important;
    border-collapse: separate !important;
    border-spacing: 0 !important;
    background: #fff !important;
    direction: rtl !important;
    font-family: "Segoe UI", Arial, sans-serif !important;
    font-size: 13px !important;
}
.catalog-grid th {
    height: 42px;
    padding: 10px 12px !important;
    border: 0 !important;
    border-left: 1px solid rgba(255,255,255,.25) !important;
    background: linear-gradient(to bottom, #185aa8, #0d4385) !important;
    color: #fff !important;
    text-align: center !important;
    font-size: 13px !important;
    font-weight: 700 !important;
    white-space: nowrap;
}
.catalog-grid th a {
    color: #fff !important;
    text-decoration: none !important;
}
.catalog-grid th a:hover { text-decoration: underline !important; }

.catalog-grid td {
    height: 48px;
    padding: 9px 12px !important;
    border: 0 !important;
    border-left: 1px solid #dce5ef !important;
    border-bottom: 1px solid #dce5ef !important;
    background: #fff !important;
    color: #233b59;
    vertical-align: middle !important;
    text-align: center;
}
.catalog-grid tr:nth-child(even) td {
    background: #f8fafc !important;
}
.catalog-grid tr:hover td {
    background: #eef5ff !important;
}

/* Columns: family, catalog, makat, classification, favorite */
.catalog-grid td:nth-child(1) {
    width: 34%;
    text-align: right;
}
.catalog-grid td:nth-child(2) {
    width: 30%;
    text-align: right;
}
.catalog-grid td:nth-child(2) a {
    color: #15579e;
    text-decoration: none;
    font-weight: 700;
}
.catalog-grid td:nth-child(2) a:hover {
    color: #0d4385;
    text-decoration: underline;
}
.catalog-grid td:nth-child(3) {
    width: 16%;
    direction: ltr;
    font-weight: 600;
    color: #173d6b;
}
.catalog-grid td:nth-child(4) {
    width: 12%;
    font-weight: 600;
}
.catalog-grid td:nth-child(5) {
    width: 8%;
}
.catalog-grid td:nth-child(5) img {
    width: 24px !important;
    height: 24px !important;
    transition: transform .12s ease;
}
.catalog-grid td:nth-child(5) a:hover img {
    transform: scale(1.12);
}

/* Pager */
.catalog-pager {
    background: #edf3fa !important;
    color: #174f8f !important;
    text-align: center !important;
    font-weight: 700 !important;
}
.catalog-pager td {
    height: 42px !important;
    background: #edf3fa !important;
    border: 0 !important;
    text-align: center !important;
}
.catalog-pager a {
    min-width: 30px;
    height: 28px;
    padding: 0 8px;
    margin: 0 2px;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    border: 1px solid #c6d7e9;
    border-radius: 6px;
    background: #fff;
    color: #1459a2 !important;
    text-decoration: none;
}
.catalog-pager a:hover { background: #e7f1fd; }

.catalog-footer {
    padding: 10px 0 0;
    color: #8a9aaf;
    text-align: center;
    font-size: 11px;
}

@media (max-width: 900px) {
    .catalog-page { padding: 10px; }
    .catalog-topbar, .catalog-toolbar {
        align-items: flex-start;
        flex-direction: column;
    }
    .catalog-toolbar-actions { flex-wrap: wrap; }
    .catalog-grid th, .catalog-grid td {
        padding: 7px 6px !important;
        font-size: 12px !important;
    }
}
</style>

</HEAD>
	<body>
        
		<form id="Form1" method="post" runat="server">
<div class="catalog-page">
  <div class="catalog-shell">

    <div class="catalog-topbar">
      <div class="catalog-brand">
        <img class="catalog-brand-icon" src="images/catalog-ui/catalog.png" alt="" />
        <div>
          <div class="catalog-brand-small">מערכת קטלוגים</div>
          <div class="catalog-brand-title" style="color:gold">קטלוגים מועדפים
              <asp:Label ID="lblCDName" runat="server"></asp:Label>
          </div>
        </div>
      </div>
      <div class="catalog-top-hint">בחר קטלוג מועדף לצפייה</div>
    </div>

    <div class="catalog-toolbar">
      <div class="catalog-toolbar-actions">
        <a class="catalog-btn" href="CatalogList.aspx" target="_self">
          <img src="images/catalog-ui/open.png" alt="" />
          כל הקטלוגים
        </a>

        <a class="catalog-btn" href="http://localhost/catmamengineweb/default.aspx" target="_blank">
          <img src="images/catalog-ui/search.png" alt="" />
          חיפוש בקטלוגים
        </a>
      </div>

      <div class="catalog-toolbar-note">לחץ על שם קטלוג לפתיחה בחלון חדש</div>
    </div>

    <div class="catalog-grid-card">
<asp:datagrid id="dbCatalogList" runat="server"
        CssClass="catalog-grid"
        GridLines="None"
        AllowSorting="True"
        AllowPaging="True"
        Width="100%"
        AutoGenerateColumns="False"
        PageSize="100"
        Font-Names="Segoe UI, Arial, sans-serif"
        RightToLeft="True">
					<Columns>
						<asp:BoundColumn DataField="PFAMILY" SortExpression="First(T_CAT_FAMILY.PFAMILY)" HeaderText="משפחה" ></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="קטלוג" SortExpression="PHEBDESC" >
							<ItemTemplate>
								<a target="_blank" href="Templates/ShowTemplate.aspx?Template=<%# Container.DataItem("PINDEXTEMPLATE")%>&amp;Pkey=<%# Container.DataItem("PKEY") %>&amp;ParentKey=r&amp;Type=1&amp;PkeyCatalog=<%# Container.DataItem("PKEY") %>&amp;CatalogIndex=<%# Container.DataItem("PIMPORTSTATUS") %>"><%# Container.DataItem("PHEBDESC") %></a>
							</ItemTemplate>
						</asp:TemplateColumn>												<asp:BoundColumn DataField="PCATALOGMAKAT" SortExpression="PCATALOGMAKAT" HeaderText="מק''ט"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="סיווג" SortExpression="PCATALOGSIVUG">
							<ItemTemplate>
							<%#GetClassification(Eval("PCATALOGSIVUG"))%>
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn HeaderText="מועדפים">
							<ItemTemplate>
								<asp:LinkButton ID="btnAddFavorite" runat="server" 
												CommandName='<%# If(Container.DataItem("PKEYCATALOG") IsNot Nothing AndAlso Container.DataItem("PKEYCATALOG").ToString() <> "", "RemoveFavorite", "AddFavorite") %>' 
												CommandArgument='<%# Container.DataItem("PKEY") %>' 
												ToolTip='<%# If(Container.DataItem("PKEYCATALOG") IsNot Nothing AndAlso Container.DataItem("PKEYCATALOG").ToString() <> "", "הסרה מהמועדפים", "הוסף למועדפים") %>' >
									<img src='<%# If(Container.DataItem("PKEYCATALOG") IsNot Nothing AndAlso Container.DataItem("PKEYCATALOG").ToString() <> "", "images/favorite-Selected.png", "images/favorite-icon.png") %>' alt="הוספה למועדפים" width="24" />

								</asp:LinkButton>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle CssClass="catalog-pager" HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
    </div>

    <div class="catalog-footer">Linkware · Catalog Browser</div>

  </div>
</div>
</form>

		
	</body>
</HTML>
