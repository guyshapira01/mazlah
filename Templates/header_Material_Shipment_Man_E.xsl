<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">

	<html>
	<head>
	<title>Header</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1255" />

	<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
	<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />
	</head>

	<body class="BodyCatalog">

	<SCRIPT LANGUAGE="javascript" SRC="tooltip.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="mapzoom.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="specialfx.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="ontop.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="cookutil.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="panning.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="events.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="addSearchFocus.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="srcpath.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="freeze_E.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="WebEditor.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="../SearchPath.js"></SCRIPT>
	<SCRIPT LANGUAGE="vbscript" SRC="notes.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="../Reports/ReportFunctions.js"></SCRIPT>




	<SCRIPT LANGUAGE="javascript">
		document.linkColor="#c7c7ba";
		document.alinkColor="#c7c7ba";
		document.vlinkColor="#c7c7ba";
	</SCRIPT>

	<SCRIPT LANGUAGE="JavaScript">
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
	</SCRIPT>	
	<TEXTAREA ID="holdtext" STYLE="display:none;"></TEXTAREA>
	
	<div align="center">
	<table cellpadding="0" cellspacing="0" border="0" bordercolor="Blue" dir="rtl" width="100%">
			<tr>
				<td class="header"><img src="IS-TR flag.jpg" align="right" height="35px"></img>
				</td>
	  			<td  class="header" align="left" dir="ltr"><b style="color:black;font-size:80%"><span id="ItemName" name="ItemName"><xsl:value-of select="Header/PageHebDesc" /></span></b></td>
	  			<td  class="header" align="left" dir="ltr"><b style="color:black; font-size:80%"><span id="CatalogName" name="CatalogName"><xsl:value-of select="Header/CatalogName" /></span></b></td>
				<td class="header"><img src="IMI logo.jpg" align="left" height="35px"></img>
				</td>
			</tr>
	</table>
	</div>

	<div align="center" style="border:2px ;" >
	<table class="TableMainBarDOC" border="0" >
			<tr align="center" >
				<td align="left">&#160;&#160;&#160;&#160;&#160;&#160;
			<!--
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						 CheckHomeURL();
					</SCRIPT>
					<IMG src="home.jpg" alt="Back To Home" />
			-->					
			<!--    *****   Help is not avaliable   ***************   -->
					<a href="../../LWWebSiteSearchEngine/default.aspx?Object=Item&amp;SearchPopulation=Book&amp;SearchPopulationSpecify=Shipment" target="_blank"><IMG border="0" src="search.jpg" alt = "Search" /></a>
<!--					<a href="javascript:;" onclick="CheckHelpURL_E();"><img border="0" src="help.jpg" alt="Help" /></a>

				
				<a href="javascript:;" style="cursor:hand;" onclick="javascript:CopiedURL=CopyUrl();setCookie('TabNo', '1',expdate);w=window.open('../generatereport.aspx?Catalog=107779a1&amp;Table=T_CAT_PART&amp;XSL=Portal-Change%20Preference.xsl&amp;PPROP7=currentuser&amp;PPUBLISH=1','PortalPreference','width=750,height=700,top=5,left=50,resizable=yes,toolbar=no,scrollbars=yes');w.focus();"><b><IMG src="fav.jpg" alt ="Add to Favorites" /></b></a>
	-->			
				<a href="javascript:;" style="cursor:hand;">
					<xsl:attribute name="onclick">javascript:CopiedURL=CopyUrl();setCookie('TabNo', '1',expdate);w=window.open('../generatereport.aspx?Catalog=107779a1&amp;Table=T_CAT_PART&amp;XSL=Portal-Change%20Preference.xsl&amp;PPROP7=currentuser&amp;PPUBLISH=1','PortalPreference','width=750,height=700,top=5,left=50,resizable=yes,toolbar=no,scrollbars=yes');w.focus();</xsl:attribute>
					<b><IMG src="fav.jpg" alt ="Add to Favorites" /></b>
				</a>
				<a href="javascript:;" style="cursor:hand;">
					<xsl:attribute name="onclick">javascript:MailLink_Tree(CopyUrl(),'LW - ' + '<xsl:value-of select="Header/CatalogName" />' + ' - ' + '<xsl:value-of select="Header/PageHebDesc" />','');</xsl:attribute>
					<b><IMG src="MailURL.jpg" alt ="Send URL by Mail" /></b>
				</a>
				<a href="javascript:;" style="cursor:hand;">
					<xsl:attribute name="onclick">javascript:CopyUrl();</xsl:attribute>
					<b><IMG src="CopyURL.jpg" alt ="Copy URL" /></b>
				</a>

<!--
					<a href="JavaScript:;">
					<xsl:attribute name="onclick">javascript:buildUrlWithBackInNewWindow('../generatereport.aspx?Catalog=102885a1&amp;Table=T_CAT_PART&amp;XSL=UpdatePackage_Material_Man.xsl&amp;PKEYTYPE=41&amp;PPROP7=<xsl:value-of select="Header/PPROP7" />','PackageReport','width=990,height=660,toolbar=no,scrollbars=yes');void(0); </xsl:attribute>
							<IMG border="0" src="Activity Summary Report.jpg" alt = "Update Package Status"	/>			
					</a>
					<a href="JavaScript:;">
					<xsl:attribute name="onclick">javascript:buildUrlWithBackInNewWindow('../generatereport.aspx?Catalog=102885a1&amp;Mode=FullSQL&amp;XSL=PalletList.xsl&amp;SQL=select%20PPROP2,PPROP5,PPROP22,PPROP8,PPROP7%20from%20t_cat_part%20where%20PPROP7=\'<xsl:value-of select="Header/PPROP7"/>\'%20and%20PKEYTYPE=\'41\'%20and%20PKEY%20in%20(select%20Pkey%20from%20t_cat_nodes%20where%20pglobalstatus=0)%20group%20by%20PPROP5,PPROP22,PPROP8,PPROP7,PPROP2','PackageReport','width=990,height=660,toolbar=no,scrollbars=yes');void(0); </xsl:attribute>
							<IMG border="0" src="Activity Summary Report.jpg" alt = "Pallet Per Contaner"	/>			
					</a>
-->				
				
				<a>
					<xsl:attribute name="href">JavaScript:buildUrlWithBackInNewWindow('../generatereport.aspx?Catalog=<xsl:value-of select="Header/PKEYCATALOG" />&amp;Mode=FullSQL&amp;XSL=ShipmentList.xsl&amp;SQL=select%20t_cat_part.*,t_cat_nodes.pparentkey%20from%20t_cat_part,t_cat_nodes%20where%20pkeycatalog=\'102885a1\'%20and%20t_cat_part.pkey=t_cat_nodes.pkey%20and%20PKEYTYPE=42%20and%20t_cat_part.pkey%20in%20(select%20Pkey%20from%20t_cat_nodes%20where%20pglobalstatus=0)%20order%20by%20TO_DATE(PPROP8,\'DD/MM/YYYY\')','DocumentReport','width=950,height=720,toolbar=no,scrollbars=yes');void(0);</xsl:attribute>
				<IMG border="0" src="Shipment.jpg" alt = "Shipment Report" />
				</a>
				</td>
				<td>


<!--  ********************     Failure Reports    -->

<!--
			<td>
		
				<a href="JavaScript:buildUrlWithBackInSameWindow('../generatereport.aspx?Catalog=TSGa6615&amp;Table=T_CAT_PART&amp;XSL=FailureReportChangeStatus.xsl&amp;PKEYTYPE=14&amp;PSTATUS=94829a1');"><IMG border="0" src="FR-finish.jpg" alt = "Finished Failures" /></a>
				<a href="JavaScript:buildUrlWithBackInSameWindow('../generatereport.aspx?Catalog=TSGa6615&amp;Table=T_CAT_PART&amp;XSL=FailureReport.xsl&amp;PKEYTYPE=14&amp;PKEYUSER=currentuser');"><IMG border="0" src="FR-my.jpg" alt = "My Failures" /></a>
				<a href="JavaScript:buildUrlWithBackInSameWindow('../generatereport.aspx?Catalog=TSGa6615&amp;Table=T_CAT_PART&amp;XSL=FailureReportChangeStatus.xsl&amp;PKEYTYPE=14&amp;PKEYUSER=currentuser&amp;PSTATUS=85597a2');"><IMG border="0" src="FR-New.jpg" alt = "My New Failures" /></a>
				<a href="JavaScript:buildUrlWithBackInSameWindow('../generatereport.aspx?Catalog=TSGa6615&amp;Table=T_CAT_PART&amp;XSL=FailureReport.xsl&amp;PKEYTYPE=14');"><IMG border="0" src="FR-all.jpg" alt = "All Failures" /></a>

				<a href="JavaScript:buildUrlWithBackInNewWindow('../generatereport.aspx?Catalog=TSGa6615&amp;Table=T_CAT_PART&amp;XSL=ManagerFailureReport.xsl&amp;PKEYTYPE=14','FailureReport','width=750,height=520,toolbar=no,scrollbars=yes');void(0);"><IMG border="0" src="Failure Summary Report.jpg" alt = "Failure Summary Report" /></a>				

			</td>

-->

<!--  ********************     End Failure Reports    -->


				</td>
			<td>
				<nobr>
				<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0,100%,*');"><b><IMG src="image.jpg" alt ="Image" /></b></a>
				<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('25%,75%,0.5%');"><b><IMG src="image_index.jpg" alt = "Image And Index" /></b></a>
				<a style="cursor:hand; position:relative ; top : -2.5;" onclick="javascript:SaveColRatio_3frames();" target="_blank" id="pritim_link_top"><b><IMG src="freeze.jpg" alt = "Freeze Frames" /></b></a>
				</nobr>
				
			</td>
				<td>
					<div id="scontentmain">
					<div id="scontentbar" >
					<table border="0" id="moveTable">
						<tr>
							<!-- <td id="moveTable"><img id="moveTable" style="cursor:hand;" src="ZoomOut.jpg" alt="Zoom Out" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)" /><img id="moveTable" style="cursor:hand;" alt="Zoom In" src="ZoomIn.jpg" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.17)" /></td>
							<td id="moveTable"><SPAN id="percent" align="center"></SPAN></td>
							<td align="left" id="moveTable"><div align="center" id="scontentsub"><img src="width.jpg" alt="Fit To Page" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);" /><img src="100.jpg" alt="100%" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" /><img src="freezeImage.jpg" alt="Freeze Image" align="baseline" onclick="javascript:SetZoomSize('');" /></div><SPAN id="freezeImageOk" align="center"></SPAN></td> -->
						</tr>
					</table>
					</div>
					</div>
				</td>
				<td>
					<SPAN id="freezeFrameOk" align="center"></SPAN>
				</td>
				
				<td>
					<xsl:value-of select="Header/UserLogon" />
				</td>
			</tr>
		</table>
		</div>
	
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
