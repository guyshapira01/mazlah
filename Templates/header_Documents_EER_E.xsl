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

	<body  topmargin="0" leftmargin="0" STYLE="filter:progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr=#7F97B9, EndColorStr=#deeeff);" onload="JavaScript:document.getElementById('PkeyCatalog').value=getParamFromUrl('PkeyCatalog');">
	
	<SCRIPT LANGUAGE="javascript" SRC="tooltip.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="ToolBar.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="mapzoom_orbotech_e.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="specialfx.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="ontop.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="cookutil.js"></SCRIPT>
	<!--<SCRIPT LANGUAGE="javascript" SRC="panning.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="events.js"></SCRIPT>-->
	<SCRIPT LANGUAGE="javascript1.2" SRC="addSearchFocus.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="srcpath.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="../publish.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="freeze_E.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="WebEditor.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="../SearchPath.js"></SCRIPT>
	<script type="text/javascript" src="pngfix.js"></script>
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
					state=1;
					break;
				case 1:
					SetColRatio('0,100%,0');
					state=2;
					break;
				case 2:
					SetColRatio('100%,0,0');
					state=0;
					break;
				
			}
		}
		
		function SaveState()
		{
			switch(state-1)
			{
				case 0:
					SetColRatio('30%,70%,0');
					state=1;
					break;
				case 1:
					SetColRatio('0,100%,0');
					state=2;
					break;
				case 2:
					SetColRatio('100%,0,0');
					state=0;
					break;
				
			}
		}

		function ResetState()
		{
			SetColRatio('30%,70%,0%');
			state=1;
		}
		
		
	</SCRIPT>
	<SCRIPT LANGUAGE="JavaScript">
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
	</SCRIPT>	
	<TEXTAREA ID="holdtext" STYLE="display:none;"></TEXTAREA>
	<div align="center" style="border:0px ;" >
	<TABLE cellpadding="1" cellspacing="0" border="0" ID="PartToolbar" width="100%" align="center">
		<TR >
			<td align="left" dir="ltr" >&#160;<img src="LinkwareIcon.gif" height="18" border="0"/>&#160;</td>
			<td align="left" valign="middle"><form name="QuickSearch" action="../SimpleSearch.aspx" target="_blank" style="display:inline;"><input type="hidden" name="pkeycatalog"/><span style="font-family:Verdana;font-size:12px">Search&#160;</span><input type="text" name="SearchKey"/><input type="image" src="Images/search.ico"/></form></td>
			<td align="center" dir="ltr" >
				<table border="0" style="font-family:tahoma;font-size:12px;font-weight:bold">
					<tr>
						<td>
  							<xsl:value-of select="Header/OBJECTTYPE"/> Name:&#160;&#160;&#160;<b style="font-family:tahoma;color:#000066"><span id="ItemName" name="ItemName"><xsl:value-of select="Header/PageHebDesc" /></span></b>
						</td>
  						<td align="center" dir="ltr">&#160;&#160;Tree: <b style="color:#000066;">&#160;&#160;&#160;<span id="CatalogName" name="CatalogName"><xsl:value-of select="Header/CatalogName" /></span></b>&#160;&#160;&#160;
  						</td>
  					</tr>
				</table>
			</td>
			<td width="10%" id="sCookie" align="right" style="color:Red">
				&#160;<img src="Images/eerlogosmall.gif" height="30"/>&#160;
			</td>
		</TR>
		</TABLE>
		<TABLE cellpadding="0" cellspacing="0" border="0" ID="PartToolbar2" class="ToolBar">
		<TR >
		<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Home XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
		<td class="ToolBarGlow" onClick="javascript:top.document.location.href='../CatalogListEER.aspx';">
					<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable0" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<tr>
					<td align="center" nowrap="yes">
					<a  class="ToolBarFont">
					<nobr><img  height="16" src="Images/Home.gif" align="absmiddle" border="0"/>&#160; Home </nobr>	</a>
				</td>
				</tr>
				</table>
				</td>		
		<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Search XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
				<td class="ToolBarGlow"  onClick="javascript:window.open('/LW-EER');">
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						//CheckSearchURL();
					</SCRIPT>
				<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<tr>
					<td nowrap="yes" align="center">
					<a class="ToolBarFont"  vertical-align="absmiddle">
						<nobr><img src="Images/search.ico" border="0" align="absmiddle" width="16" height="16"></img>&#160; Search </nobr>
					</a>
					</td>
				</tr>
				</table>						
				</td>
		<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Back XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
				<td class="ToolBarGlow"  >
				<xsl:attribute name="onClick">
						<xsl:choose>
							<xsl:when match=".[Header/IsWebEditor='True']">JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-2])=='undefined') {alert('No Back Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos-1; top.frames('Index').LinkSwitch(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:when>
							<xsl:otherwise>JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-2])=='undefined') {alert('No Back Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos-1; top.frames('Parts Map').refreshFramesWithUrl(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:otherwise>
						</xsl:choose>
					</xsl:attribute>
				<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable87" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<tr>
					<td align="center" nowrap="yes">
					<a class="ToolBarFont">
					<nobr><img src="Images/back.ico" border="0" align="absmiddle" width="16" height="16"></img>&#160; Back </nobr>
					</a>
					</td>
				</tr>
				</table>						
				</td>
		<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Forward XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
				<td class="ToolBarGlow"  >
				<xsl:attribute name="onClick">
						<xsl:choose>
							<xsl:when match=".[Header/IsWebEditor='True']">JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos])=='undefined') {alert('No Forward Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos+1; top.frames('Index').LinkSwitch(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:when>
							<xsl:otherwise>JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos])=='undefined') {alert('No Forward Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos+1; top.frames('Parts Map').refreshFramesWithUrl(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:otherwise>
						</xsl:choose>
					</xsl:attribute>
				<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable99" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<tr>
					<td align="center" nowrap="yes">
					<a class="ToolBarFont"> 
					<nobr><img src="Images/forward.ico" border="0" align="absmiddle" width="16" height="16"></img>&#160; Forward </nobr>
					</a>
					</td>
				</tr>
				</table>						
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Index        XXXXXXXXXXXXXXXXXXXXXXX-->
				<!--<td class="ToolBarGlow"  >
					<xsl:attribute name="onClick">javascript:top.document.location.href='<xsl:value-of select="Header/Index" />'</xsl:attribute>
					<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable1" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<tr>
					<td align="center" nowrap="yes">
					<a  class="ToolBarFont" >
					<nobr><img  height="16" src="Images/Index.ico" align="absmiddle" border="0"/>&#160; Index </nobr>	</a>
				</td>
				</tr>
				</table>
				</td>-->
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Add to Favorites        XXXXXXXXXXXXXXXXXXXXXXX-->
				<!--<td valign="middle" class="ToolBarGlow"  onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<table cellspacing="2" cellpadding="2"  ID="btnTable2">
					<tr>
					<td nowrap="yes">
				<a  class="ToolBarFont" href="javascript:window.parent.external.AddFavorite(window.top.location.href,window.top.document.title);" >
					
					<nobr><img width="16" height="16" src="Images/fav.ico" border="0" align="absmiddle"/>&#160; Favorites </nobr>
				</a>
				</td>
				</tr>
				</table>
				</td>-->
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Send URL by Mail        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow"  >
				<xsl:attribute name="onClick">javascript:top.document.location.href='mailto:?Subject=<xsl:value-of select="Header/OBJECTTYPE"/>  Name: <xsl:value-of select="//Header/PHEBDESC"/>&amp;Body=' + top.document.location.href.replace(getParamFromUrl('Pkey'),'<xsl:value-of select="//Header/PKEY"/>').replace(getParamFromUrl('ParentKey'),'<xsl:value-of select="//Header/PARENTKEY"/>').replace(/&amp;/gi,"%26")</xsl:attribute>
				<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable3" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<tr>
					<td align="center" nowrap="yes">
				<a class="ToolBarFont">
					<!--<xsl:attribute name="onclick">javascript:MailLink_Tree(CopyUrl(),'LW - ' + '<xsl:value-of select="Header/CatalogName" />' + ' - ' + '<xsl:value-of select="Header/PageHebDesc" />','');</xsl:attribute>-->
					<!--<IMG src="MailURL.jpg" alt ="Send URL by Mail" />-->									
					<nobr><img src="Images/mail.ico" border="0" width="16" height="16" align="absmiddle"/>&#160; Mail </nobr>
				</a>
				</td>
				</tr>
				</table>
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Add to favorites        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow"  >
				<xsl:attribute name="onClick">JavaScript:AddToFavoritesTree('<xsl:value-of select="//Header/UserLogonPkey" />');</xsl:attribute>
				<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable3" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<tr>
					<td align="center" nowrap="yes">
				<a class="ToolBarFont">
					<!--<xsl:attribute name="onclick">javascript:MailLink_Tree(CopyUrl(),'LW - ' + '<xsl:value-of select="Header/CatalogName" />' + ' - ' + '<xsl:value-of select="Header/PageHebDesc" />','');</xsl:attribute>-->
					<!--<IMG src="MailURL.jpg" alt ="Send URL by Mail" />-->									
					<nobr><img src="Images/fav.ico" border="0" width="16" height="16" align="absmiddle"/>&#160; Add to favorites </nobr>
				</a>
				</td>
				</tr>
				</table>
				</td>
				<!--XXXXXXXXXXXXXXXXXXXXXXX           View States       XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow"  onclick="javascript:ViewState();" ondblclick="javascript:ResetState();">
					<table width="100%" cellspacing="2" cellpadding="2" onmouseover="glow(true, this);" onmouseout="glow(false, this);" ID="btnTable4">
					<tr>
					<td align="center" nowrap="yes">
					<a  class="ToolBarFont" >
					<nobr><img src="Images/views.ico" border="0" width="16" height="16" align="absmiddle"/> Views </nobr></a>
					<!--<b><IMG src="image.jpg" alt ="Image" /></b>-->
					</td>
				</tr>
				</table>
				</td>
				
				<!--XXXXXXXXXXXXXXXXXXXXXXX           Freeze        XXXXXXXXXXXXXXXXXXXXXXX-->
				<td class="ToolBarGlow" onClick="javascript:SaveState();sCookie.innerText='Prefrence Saved';" target="_self">
					<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable5" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
					<tr>
					<td align="center" nowrap="yes">
					<a  class="ToolBarFont" id="pritim_link_top">
					<nobr><img  src="Images/save.gif" border="0" width="16" height="16" align="absmiddle"/>&#160; Save View </nobr></a>
				</td>
				</tr>
				</table>
				</td>
		</TR>
		
	</TABLE>
		</div>
	
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
