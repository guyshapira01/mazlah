<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">

	<html>
	<base target="_parent" />
	<head>
	<title>Header</title>
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />

	<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css" />
	</head>

	<body  topmargin="0" leftmargin="0" STYLE="filter:progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr=#7F97B9, EndColorStr=#deeeff);">
	
	<SCRIPT LANGUAGE="javascript" SRC="tooltip.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="ToolBar.js"></SCRIPT>
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
	<script type="text/javascript" src="pngfix.js"></script>
	<script language="javascript" src="../Publish.js"></script>
	<SCRIPT LANGUAGE="javascript">
		document.linkColor="#c7c7ba";
		document.alinkColor="#c7c7ba";
		document.vlinkColor="#c7c7ba";
		var state=1; //0-Index AND Image//1-Index AND Image AND Table//2-Index AND Table//3-Image AND Table
		function Picture(){
		    var frameName;
		    var i;
		    var firstPos;
		    var resultArray;
	  	    resultArray = new Array();
	  	    frameName = new String (top.frames('Parts Map').document.location);
		    firstPos = frameName.indexOf("#");
		    // # not found
			if (firstPos == -1) {
				frameName = top.frames('Parts Map').document.location + "#Picture";
			}
			else {
				resultArray=frameName.split("#");	
				if (resultArray[1] = "ItemProperties") {
					frameName= resultArray[0] + "#Picture";
				}
		   		else if (resultArray[1] = "Picture") {
					top.frames('Parts Map').document.location.href=frameName;			        
				}
			}
		    top.frames('Parts Map').document.location.href=frameName;
		}
		function Prop(){
		    var frameName;
		    var i;
		    var firstPos;
		    var resultArray;
	  	    resultArray = new Array();
	  	    frameName = new String (top.frames('Parts Map').document.location);
		    firstPos = frameName.indexOf("#");
		    // # not found
		    if (firstPos == -1) {
				frameName = top.frames('Parts Map').document.location + "#ItemProperties";
		    }
			else {
				resultArray=frameName.split("#");	
				if (resultArray[1] = "Picture") {
					frameName= resultArray[0] + "#ItemProperties";
				}
				else if (resultArray[1] = "ItemProperties") {
					top.frames('Parts Map').document.location.href=frameName;			        
				}
			}
		    top.frames('Parts Map').document.location.href=frameName;
		}	
		function ViewState()
		{
			switch(state)
			{
				case 0:
					SetColRatio('30%,70%,0%');
					state=1
					break;
				case 1:
					SetColRatio('0%,100%,0%');
					state=0
					break;
			}
		}

		function ResetState()
		{
			SetColRatio('30%,70%,0%');
			state=1
		}
		
	</SCRIPT>
	<SCRIPT LANGUAGE="JavaScript">
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
		
		
		function SubmitSearch(searchkey){
			var spkeycatalog = getParamFromUrl("PkeyCatalog");
			alert (searchkey);
			window.open('../simplesearch.aspx?SearchKey=' + searchkey + '&amp;pkeycatalog=' + spkeycatalog);
		}
	</SCRIPT>	
	<TEXTAREA ID="holdtext" STYLE="display:none;"></TEXTAREA>
	<div align="center" style="border:0px ;" >
	<TABLE width="100%" cellpadding="1" cellspacing="0" border="0" ID="PartToolbar" >
		<TR >
			<form onsubmit="javascript:window.open('','simplesearchwindow','height=600,width=700,status=no,toolbar=no,menubar=no,location=no,scrollbars=yes');" action="../simplesearch.aspx" target="simplesearchwindow"><td width="45%" align="left"><input type="text" name="searchkey" style="width:100px" class="ToolBarGlow"></input>&#160;&#160;&#160;<input type="image" src="Images/search.ico" value="Search"></input><input type="hidden" name="pkeycatalog"></input></td></form>
			<script>document.all.pkeycatalog.value=getParamFromUrl("PkeyCatalog");</script>

			<td align="left" dir="ltr">
				<table align="left" border="0"  style="font-family:tahoma;font-size:12px;font-weight:bold">
					<tr>
						<td>
  							<b style="font-family:tahoma;color:#000066"><span id="ItemName" name="ItemName"><xsl:value-of select="Header/PageHebDesc" /></span></b>
						</td>
  					</tr>
				</table>
			</td>
			
			
		</TR>
		</TABLE>
		<TABLE cellpadding="0" cellspacing="0" border="0" ID="PartToolbar2" class="ToolBar">
		<TR >
		<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Home XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
		<td class="ToolBarGlow" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<table cellspacing="2" cellpadding="2"  ID="btnTable0">
					<tr>
					<td nowrap="yes">
					<a  class="ToolBarFont">
						<xsl:attribute name="href">JavaScript:top.frames('Index').LinkSwitch('<xsl:value-of select="//Header/ReferenceURL"/>','37202a1');void(0);</xsl:attribute>
					<nobr><img  height="16" src="Images/Home.gif" align="absmiddle" border="0"/>&#160; Home </nobr>	</a>
				</td>
				</tr>
				</table>
				</td>		
		<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Search XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
				<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						//CheckSearchURL();
					</SCRIPT>
				<table cellspacing="2" cellpadding="2"  ID="btnTable">
				<tr>
					<td nowrap="yes">
					<a class="ToolBarFont" vertical-align="absmiddle">
						<xsl:attribute name="href">javascript:window.open('../advancedsearch.aspx','','height=600,width=700,status=no,toolbar=no,menubar=no,location=no,scrollbars=yes');void(0);</xsl:attribute> 
						<nobr><img src="Images/search.ico" border="0" align="absmiddle" width="16" height="16"></img>&#160; Search </nobr>
					</a>
					</td>
				</tr>
				</table>						
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Index        XXXXXXXXXXXXXXXXXXXXXXX-->
				
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Add to Favorites        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td valign="middle" class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<table cellspacing="2" cellpadding="2"  ID="btnTable2">
					<tr>
					<td nowrap="yes">
				<a  class="ToolBarFont" href="javascript:window.parent.external.AddFavorite(window.top.location.href,window.top.document.title);" >
					
					<!--<IMG src="fav.jpg" alt ="Add to Favorites" />-->
					<nobr><img width="16" height="16" src="Images/fav.ico" border="0" align="absmiddle"/>&#160; Favorites </nobr>
				</a>
				</td>
				</tr>
				</table>
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Send URL by Mail        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<table cellspacing="2" cellpadding="2"  ID="btnTable3">
					<tr>
					<td nowrap="yes">
				<a class="ToolBarFont">
					<!--<xsl:attribute name="onclick">javascript:MailLink_Tree(CopyUrl(),'LW - ' + '<xsl:value-of select="Header/CatalogName" />' + ' - ' + '<xsl:value-of select="Header/PageHebDesc" />','');</xsl:attribute>-->
					<!--<IMG src="MailURL.jpg" alt ="Send URL by Mail" />-->									
					<xsl:attribute name="href">mailto:?Subject=Item details&amp;Body=%0D%0AItem Number: <xsl:value-of select="//Header/PPROP1"/>%0D%0ADescription: <xsl:value-of select="//Header/PageHebDesc"/></xsl:attribute>
					<nobr><img src="Images/mail.ico" border="0" width="16" height="16" align="absmiddle"/>&#160; Mail </nobr>
				</a>
				</td>
				</tr>
				</table>
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           View States       XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<table cellspacing="2" cellpadding="2"  ID="btnTable4">
					<tr>
					<td nowrap="yes">
					<a  href="javascript:;" class="ToolBarFont" onclick="javascript:ViewState();" ondblclick="javascript:ResetState();">
					<nobr><img src="Images/views.ico" border="0" width="16" height="16" align="absmiddle"/> Views </nobr></a>
					<!--<b><IMG src="image.jpg" alt ="Image" /></b>-->
					</td>
				</tr>
				</table>
				</td>
				
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Zoom Out        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<table cellspacing="2" cellpadding="2"  ID="btnTable6">
					<tr>
					<td nowrap="yes">
								<!--<img id="moveTable" style="cursor:hand;" src="ZoomOut.jpg" alt="Zoom Out" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)" />-->
								<a class="ToolBarFont" href="javascript:fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)" target="_self">
					<nobr><img src="Images/ZoomOut.gif" border="0" width="16" height="16" align="absmiddle"/>&#160; Zoom Out </nobr></a>
				</td>
				</tr>
				</table>
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Zoom In        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
						      <table cellspacing="2" cellpadding="2"  ID="btnTable7">
					<tr>
					<td nowrap="yes">
						      <!-- <img id="moveTable" style="cursor:hand;" src="ZoomIn.jpg" alt="Zoom In" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.17)" />-->
						      <a class="ToolBarFont" href="javascript:fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.17)" target="_self">
						      <nobr><img src="Images/ZoomIN.gif" border="0" width="16" height="16" align="absmiddle"/>&#160; Zoom In </nobr></a>
				</td>
				</tr>
				</table>
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Precent        XXXXXXXXXXXXXXXXXXXXXXX-->
				<!--
				<td class="ToolBarGlow" WIDTH="1%" id="moveTable">
							<SPAN id="percent" align="center"></SPAN>
				</td>
				-->
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Fit To Page        XXXXXXXXXXXXXXXXXXXXXXX-->
						
						<!--<img src="width.jpg" alt="Fit To Page" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);SetZoomSize('FIT_TO_PG');" />-->
						<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
						<table cellspacing="2" cellpadding="2"  ID="btnTable8">
							<tr>
							<td nowrap="yes">
							<a class="ToolBarFont" href="javascript:fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);SetZoomSize('FIT_TO_PG');" target="_self" >
							<nobr><img src="Images/fit2.ico" border="0" width="16" height="16" align="absmiddle"/>&#160; Fit </nobr></a>
						</td>
						</tr>
						</table>
						</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Actual Size        XXXXXXXXXXXXXXXXXXXXXXX-->
						<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
							<!--<img src="100.jpg" alt="100%" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" />-->
							<table cellspacing="2" cellpadding="2"  ID="btnTable11">
							<tr>
							<td nowrap="yes">
							<a class="ToolBarFont" href="javascript:fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" target="_self" >
							<nobr><img src="Images/actual.ico" border="0" width="16" height="16" align="absmiddle"/>&#160; Actual </nobr></a>
						</td>
						</tr>
						</table>
						</td>
						
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Languages        XXXXXXXXXXXXXXXXXXXXXXX-->
						<td class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
							<!--<img src="100.jpg" alt="100%" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" />-->
							<table cellspacing="2" cellpadding="2"  ID="btnTable11">
							<tr>
							<td nowrap="yes">
							<a class="ToolBarFont" href="Javascript:window.open('Languages.aspx', 'Languages', 'screenX=' + ((screen.width-430)/2) + ',screenY=' + ((screen.height-300)/2) + ',scrollbars=no,toolbar=no,width=430,top=' + ((screen.height-300)/2) + ',left=' + ((screen.width-430)/2) + ',height=300,location=no,menubar=no');void(0);" >
							<nobr><img border="0" width="22" height="16" align="absmiddle"> 
								  <xsl:attribute name="src">IconTypes/<xsl:value-of select="//Header/PREFS/PTEXTLANG"/>.gif</xsl:attribute>
								  </img> &#160; Languages
							</nobr>
							</a>
						</td>
						</tr>
						</table>
						</td>
			
				<!--XXXXXXXXXXXXXXXXXXXXXXX           freezeFrameOk        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow" >
					<SPAN id="freezeFrameOk" align="center"></SPAN>
				</td>
				
		</TR>
		
	</TABLE>
		</div>
	
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
