<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

<xsl:template match="/">

<html>

<base target="_parent" />
<head>
<title>Catalog Page</title>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
</head>

<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="WebEditor.css" TYPE="text/css" />

<SCRIPT LANGUAGE="JavaScript" SRC="tooltip_E.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="mapzoom.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx_E.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript1.2" SRC="ontop.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript1.2" SRC="cookutil.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="panning.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="events.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript1.2" SRC="addSearchFocus.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="WebEditor.js"></SCRIPT>

<a name = "Picture"></a>
<input type="hidden" name="pic_name"><xsl:attribute name="value"><xsl:value-of select="Items/Item/map/PicName"/></xsl:attribute></input><!--<# PICTURE_NAME ORDINAL=1 ENCAPS=' #>-->
<input type="hidden" name="map_name"><xsl:attribute name="value"><xsl:value-of select="Items/Item/map/MapName"/></xsl:attribute></input><!--<# IMAGEMAP_NAME ORDINAL=1 ENCAPS=' #>-->

<body class="BodyCatalog" dir="ltr">
		<SCRIPT LANGUAGE="JavaScript">
			addModeTypeColor("<xsl:value-of select="Prop3"/>");
		</SCRIPT>

<SPAN id="globalAreaId" style="visibilty:hidden;"></SPAN>

<div align="center">
<table align="center">
	<tr>
		<td><xsl:value-of select="Text1"/></td>
		<td><xsl:value-of select="Text2"/></td>
		<td><xsl:value-of select="Text3"/></td>
	</tr>
	<tr>
		<td><xsl:value-of select="Text4"/></td>
		<td align="center"><xsl:value-of select="Text5"/>

		<IMG  name="SearchFocus" src="blankSearchFocus.gif" STYLE="position:absolute; top:0px ; left:0px; z-index : 100 visibilty : :hidden" onclick="javascript:hideSearchImage()" />
		<xsl:if test="Items/Item/map/mapOrd/MapHTML != ''">
			<xsl:value-of select="Items/Item/map/mapOrd/MapHTML" disable-output-escaping="yes"/>
		</xsl:if>
		</td>
		<td><xsl:value-of select="Text6"/></td>
	</tr>
	<tr>
		<td><xsl:value-of select="Text7"/></td>
		<td><xsl:value-of select="Text8"/></td>
		<td><xsl:value-of select="Text9"/></td>
	</tr>
</table>
</div>
<a name = "ItemProperties"></a> <!-- do not delete this href it is for up/down arrows -->
<!-- Item Documents -->
<div align="center">
<!-- Properties -->

<table  width="100%">
<tr align="center">
<td>
<br/>
<font class="TitleText_E">Item Properties</font>
<hr/>

<TABLE border="0" cellPadding="0" cellSpacing="0" height="100%" width="100%">
  <TR>
    <TD align="left" vAlign="top" width="100%">
      <TABLE  border="0" cellPadding="2" cellSpacing="0" id="idTabs"  width="98%">
        <TBODY>
        <TR height="25" vAlign="center">
 <!--<TD class="clsTab_Blank" width="1%">&#160;&#160;&#160;&#160;&#160;</TD>-->
 <TD class="clsTab" id="tabs" onclick="TabClick('0');" width="50%"><table><tr><td class="clsTab_Part_Small"><a>A-</a></td><td><A href="javscript:void();"
   onclick="return TabClick('0');">General</A></td></tr></table></TD>
 <TD class="clsTab" id="tabs" onclick="TabClick('1');" width="50%"><table><tr><td class="clsTab_Part_Small"><a>B-</a></td><td><A href="javscript:void();"
   onclick="return TabClick('1');">Properties</A></td></tr></table></TD>
<TD class="clsTab_Blank" id="tabs" width="100%">&#160;</TD>
</TR></TBODY></TABLE> 

     <SCRIPT>tabs[2].width="100%"; idTabs.style.display="block";</SCRIPT>


<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="98%"><TBODY>
<TR><TD width="5"></TD><TD align="center" width="100%" class="clstabselected">
<br></br>
<!--Start Content of tab 1 -->
<TABLE class="regular"	border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
	<xsl:for-each select="Items/Item">
	
		<TR>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP1LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP1" />
			&#160;</TD>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP2LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP2" />
			&#160;</TD>
		</TR>

		<TR>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP3LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP3" />
			&#160;</TD>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP4LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP4" />
			&#160;</TD>
		</TR>

		<TR>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP5LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP5" />
			&#160;</TD>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP6LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP6" />
			&#160;</TD>
		</TR>

		<TR>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP7LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP7" />
			&#160;</TD>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP8LABEL" /></TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP8" />
			&#160;</TD>
		</TR>

		<TR>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP9LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP9" />
			&#160;</TD>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP10LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP10" />
			&#160;</TD>
		</TR>
	</xsl:for-each>
</TABLE>

<!--End Content of tab 1 -->

<br></br></TD></TR></TBODY></TABLE>
<SCRIPT>newsContent.style.display="none";</SCRIPT>

<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="98%"><TBODY>
<TR><TD width="5"></TD><TD width="100%" class="clstabselected"><br></br>

<!--Start Content of tab 2 -->
<TABLE class="regular"	border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
	<xsl:for-each select="Items/Item">
		<TR>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP11LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP11" />
			&#160;</TD>
			<TD class="normal" width="20%"><xsl:value-of select="PPROP12LABEL" />:</TD>
			<TD align="left" class="notestabs" width="30%">
				<xsl:value-of select="PPROP12" />
			&#160;</TD>
		</TR>
		<TR>
			<TD class="normal"><xsl:value-of select="PPROP13LABEL" />:</TD>
			<TD align="left" class="notestabs">
				<xsl:value-of select="PPROP13" />
			&#160;</TD>
			<TD class="normal"><xsl:value-of select="PPROP14LABEL" />:</TD>
			<TD align="left" class="notestabs">
				<xsl:value-of select="PPROP14" />
			&#160;</TD>
		</TR>
		<TR>
			<TD class="normal"><xsl:value-of select="PPROP15LABEL" />:</TD>
			<TD align="left" class="notestabs">
				<xsl:value-of select="PPROP15" />
			&#160;</TD>
			<TD class="normal"><xsl:value-of select="PPROP16LABEL" />:</TD>
			<TD align="left" class="notestabs">
				<xsl:value-of select="PPROP16" />
			&#160;</TD>
		</TR>
		<TR>
			<TD class="normal"><xsl:value-of select="PPROP17LABEL" />:</TD>
			<TD align="left" class="notestabs">
				<xsl:value-of select="PPROP17" />
			&#160;</TD>
			<TD class="normal" width="40%"><xsl:value-of select="PPROP18LABEL" />:</TD>
			<TD align="left" colspan="3" class="notestabs">
				<xsl:value-of select="PPROP18" />
			&#160;</TD>
		</TR>		
		<TR>
			<TD class="normal"><xsl:value-of select="PPROP19LABEL" />:</TD>
			<TD align="left" class="notestabs">
				<xsl:value-of select="PPROP19" />
			&#160;</TD>
			<TD class="normal" width="40%"><xsl:value-of select="PPROP20LABEL" />:</TD>
			<TD align="left" colspan="3" class="notestabs">
				<xsl:value-of select="PPROP20" />
			&#160;</TD>
		</TR>		
	</xsl:for-each>
</TABLE>

<!--End Content of tab 2 -->


<br></br></TD></TR></TBODY></TABLE>
<SCRIPT>newsContent[1].style.display="none";</SCRIPT>

<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="100%"><TBODY>
<TR><TD width="5"></TD><TD width="100%" class="clstabselected"><br></br>

<!--Start Content of tab 7 -->


<!--End Content of tab 7-->


<br></br></TD></TR></TBODY></TABLE>
<SCRIPT>newsContent[2].style.display="none";</SCRIPT>

</TD>
</TR>
</TABLE><SCRIPT>newsContent[0].style.display = "block";tabs[0].className = "clsTabSelected_Top";</SCRIPT>
<xsl:choose>
	<xsl:when test="//IsWebEditor='True'">
<!-- Start Part Toolbar -->
	<br/>
	<TABLE class="LW-toolbar" cellpadding="2" cellspacing="0" border="0" align="center">
	<TR>
			<td class="LW-toolbar"> 
					<table border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td class="LW-toolbar" nowrap="yes">
								<a class="LW-toolbar" href="JavaScript:AddObj('New_Item_E.xsl');void(0);" target="_self" title="New">
									<img src="WEImages/newdoc.gif" alt="New" border="0" width="16" height="16"/>
								</a>
							</td>
							<td class="LW-toolbar" nowrap="yes">
								<a class="LW-font" href="JavaScript:AddObj('New_Item_E.xsl');void(0);" target="_self" title="New"> New </a>
							</td>
						</tr>
					</table>
			</td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar"> <table border="0" cellspacing="0" cellpadding="0"><tr><td class="LW-toolbar" nowrap="yes"><a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Cut"><img src="WEImages/tbcut.gif" alt="Cut" border="0" width="16" height="16"/></a></td><td class="LW-toolbar" nowrap="yes"><a class="LW-font" href="javascript:alert('Functionality Not Available Yet');void(0);" title="New"> Cut </a></td></tr></table></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar"> <table border="0" cellspacing="0" cellpadding="0"><tr><td class="LW-toolbar" nowrap="yes"><a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Copy"><img src="WEImages/tbcopy.gif" alt="Copy" border="0" width="16" height="16"/></a></td><td class="LW-toolbar" nowrap="yes"><a class="LW-font" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Copy"> Copy </a></td></tr></table></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar"> <table border="0" cellspacing="0" cellpadding="0"><tr><td class="LW-toolbar" nowrap="yes"><a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Paste"><img src="WEImages/tbpaste.gif" alt="Paste" border="0" width="16" height="16"/></a></td><td class="LW-toolbar" nowrap="yes"><a class="LW-font" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Paste"> Paste </a></td></tr></table></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar"> 
					<table border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td class="LW-toolbar" nowrap="yes">
								<a class="LW-toolbar" href="JavaScript:UpdateObj('T_CAT_PART','Update_Item_E.xsl','true');void(0);" target="_self" title="Edit">
									<img src="WEImages/editgrid.gif" alt="Edit" border="0" width="16" height="16"/>
								</a>
							</td>
							<td class="LW-toolbar" nowrap="yes">
								<a class="LW-font" href="JavaScript:UpdateObj('T_CAT_PART','Update_Item_E.xsl','true');void(0);" target="_self" title="Edit"> Edit </a>
							</td>
						</tr>
					</table>
			</td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar">
					<table border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td class="LW-toolbar" nowrap="yes">
								<a class="LW-toolbar" href="JavaScript:DelObj();void(0);" target="_self" title="Delete">
									<img src="WEImages/delete.gif" alt="Delete" border="0" width="16" height="16"/>
								</a>
							</td>
							<td class="LW-toolbar" nowrap="yes">
								<a class="LW-font" href="JavaScript:DelObj();void(0);" target="_self" title="Delete"> Delete </a>
							</td>
						</tr>
					</table>
			</td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar"> <table border="0" cellspacing="0" cellpadding="0"><tr><td class="LW-toolbar" nowrap="yes"><a class="LW-toolbar" target="_self" href="JavaScript:PermissionsWin('true');void(0);" title="Permissions"><img src="WEImages/permissions.gif" alt="Permissions" border="0" width="16" height="16"/></a></td><td class="LW-toolbar" nowrap="yes"><a class="LW-font" target="_self" href="JavaScript:PermissionsWin('true');void(0);" title="Permissions"> Permissions </a></td></tr></table></td>
			<!--<TD width="99%" class="LW-toolbar" align="right" nowrap="yes" id="align01">&#160;</TD>-->
		</TR>
	</TABLE>
	<br/><br/>
	</xsl:when>
</xsl:choose>
<!-- End Part Toolbar -->

<TABLE width="100%" border="0" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr" id="ItemDocuments">
<tr>
	<td align="center" colspan="3">

	<font class="TitleText_E">Item Documents</font>
	<HR/>
	</td>
</tr>
<tr>
	<td colspan="3">
	<table border="0" cellspacing="0" cellpadding="0" width="100%" >
		<tr valign="top" style="background-color:Tan;font-weight:bold;">
			<xsl:choose>
				<xsl:when test="//IsWebEditor='True'"><td align="center" width="1" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;&#160;</FONT></td> </xsl:when>
				<xsl:otherwise></xsl:otherwise>
			</xsl:choose>
			<td align="center" width="1" style="BORDER-BOTTOM: #333333 1px solid;BORDER-LEFT: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black">&#160;</FONT></td>
			<td nowrap="true" align="left" width="20%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Name&#160;</FONT></td>
			<td align="left" width="40%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Description&#160;</FONT></td>
	 		<td align="center" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Rev&#160;</FONT></td> 
	 		<td align="center" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;ID&#160;</FONT></td> 
	 		<td align="center" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Check Out User&#160;</FONT></td> 
	 		<td align="center" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Info&#160;</FONT></td> 
	 		<td align="center" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Status&#160;</FONT></td> 
		</tr>
		<xsl:choose>
		    <xsl:when test="Items/Documents/Document = ''">
		    	<tr valign="bottom" style="background-color:PaleGoldenrod;">
		    		<td colspan="7">No Objects Found</td>
		    	</tr>
		    </xsl:when>
		    <xsl:otherwise>
			<script language="JavaScript">
				var i=0;
			</script>
	<xsl:for-each select="Items/Item/Documents/Document">
				<script language="JavaScript">
				if (i%2==0)	{
					document.write(String.fromCharCode(60) + 'tr height="30" valign="middle" style="background-color:PaleGoldenrod;"' + String.fromCharCode(62));
				}
				else	{
					document.write(String.fromCharCode(60) + 'tr height="30" valign="middle" style="background-color:LightGoldenrodYellow;"' + String.fromCharCode(62));
				}
				</script>
					<xsl:choose>
						<xsl:when test="//IsWebEditor='True'"><td><INPUT type="checkbox"><xsl:attribute name="name"><xsl:value-of select="DOCUMENTSYSTTEMID" /></xsl:attribute></INPUT></td> </xsl:when>
						<xsl:otherwise></xsl:otherwise>
					</xsl:choose>
					<td width="1"><img><xsl:attribute name="src">IconTypes/<xsl:value-of select="DOCUMENTOBJECTTYPE"/></xsl:attribute></img></td>
					<td nowrap="true">
						<a class="DocumentName" target="_blank">
							<xsl:attribute name="href"><xsl:value-of select="DOCUMENTPATH" /></xsl:attribute>
							<xsl:value-of select="DOCUMENTNAME"/>
						</a>
					</td>
					<td align="left" class="DocumentDescription"><xsl:value-of select="DOCUMENTDESCRIPTION"/></td>
					<td align="center" class="DocumentRevision"><xsl:value-of select="DOCUMENTREVISION"/></td>
					<td align="center" class="DocumentID"><xsl:value-of select="DOCUMENTID"/></td>
					<td align="center" class="DocumentID" nowrap="yes">
						<xsl:choose>
							<xsl:when test="DOCUMENTCHECKEDOUT = ''"></xsl:when>
							<xsl:otherwise>
								<img src="WEImages/checkout_other.gif" align="absmiddle">
								<xsl:attribute name="alt"><xsl:value-of select="DOCUMENTCHECKEDOUT"/></xsl:attribute>
								</img>
							</xsl:otherwise>
						</xsl:choose><xsl:value-of select="DOCUMENTCHECKEDOUT"/>
					</td>
					<td>
						<xsl:choose>
							<xsl:when test="StatusName = ''">
							</xsl:when>
							<xsl:otherwise>
								<img>
									<xsl:attribute name="src">IconTypes/<xsl:value-of select="StatusName"/>.gif</xsl:attribute>
									<xsl:attribute name="alt"><xsl:value-of select="StatusName"/></xsl:attribute>
								</img>
							</xsl:otherwise>
						</xsl:choose>
					</td>
					<td align="center">
						<a>
							<xsl:choose>
								<xsl:when test="//IsWebEditor='True'"><xsl:attribute name="href">JavaScript:w=window.open('updateObject.aspx?Object=<xsl:value-of select="DOCUMENTSYSTTEMID" />&amp;Table=T_CAT_WINOBJECT&amp;Template=WinObject_e.xsl','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');w.focus();void(0);</xsl:attribute></xsl:when>
								<xsl:otherwise><xsl:attribute name="href">JavaScript:window.open('DocumentPopup.htm?FilePath=<xsl:value-of select="DOCUMENTPATH" />&amp;FileName=<xsl:value-of select="DOCUMENTNAME"/>&amp;FileDescription=<xsl:value-of select="DOCUMENTDESCRIPTION"/>&amp;FileID=<xsl:value-of select="DOCUMENTID"/>&amp;FileRevision=<xsl:value-of select="DOCUMENTREVISION"/>&amp;CreateDate=<xsl:value-of select="DOCUMENTFILECREATEDATE"/>&amp;ModifiedDate=<xsl:value-of select="DOCUMENTFILEMODIFIEDDATE"/>&amp;ModifiedName=<xsl:value-of select="DOCUMENTMODIFIERNAME"/>&amp;Status=<xsl:value-of select="DOCUMENTSTATUS"/>&amp;SystemID=<xsl:value-of select="DOCUMENTSYSTTEMID"/>&amp;objType=<xsl:value-of select="DOCUMENTOBJECTTYPENAME"/>&amp;','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');void(0);</xsl:attribute></xsl:otherwise>
							</xsl:choose>
							<xsl:attribute name="onMouseOver">window.status='More Information';return true;</xsl:attribute>
							<xsl:attribute name="onMouseOut">window.status='';</xsl:attribute>
							<img border="0" src="info.gif"/>
						</a>
					</td>
				<script language="JavaScript">
					document.write(String.fromCharCode(60) + '/tr' + String.fromCharCode(62));
					i++;
				</script>
	</xsl:for-each>
		</xsl:otherwise>
</xsl:choose>
</table>
</td>
</tr>
</TABLE>

<xsl:choose>
	<xsl:when test="//IsWebEditor='True'">
<!-- Start Documents Toolbar -->
	<TABLE class="LW-toolbar" cellpadding="2" cellspacing="0" border="0" align="center" width="100%">
	<TR>
			<!--<td class="LW-toolbar" nowrap="yes"> 
					<a class="LW-font" href="JavaScript:AddObj('New_Item_Folder_E.xsl');void(0);" target="_self" title="New">
						<img src="WEImages/newfolder.gif" alt="New" border="0" align="absmiddle" width="16" height="16"/> New Folder
					</a>
			</td>
			<TD class="LW-separator">|</TD>-->
			<td class="LW-toolbar" nowrap="yes"> 
					<a class="LW-font" href="JavaScript:AddObj('New_WinObject_E.xsl');void(0);" target="_self" title="New">
						<img src="WEImages/newdoc.gif" alt="New" border="0" align="absmiddle" width="16" height="16"/> New Document
					</a>
			</td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Cut"><img src="WEImages/tbcut.gif" alt="Cut" align="absmiddle" border="0" width="16" height="16"/> Cut</a></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Copy"><img src="WEImages/tbcopy.gif" alt="Copy" align="absmiddle" border="0" width="16" height="16"/> Copy</a></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Paste"><img src="WEImages/tbpaste.gif" alt="Paste" align="absmiddle" border="0" width="16" height="16"/> Paste</a></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> 
					<a class="LW-font" href="JavaScript:UpdateObj('T_CAT_WINOBJECT','Update_WinObject_E.xsl');void(0);" target="_self" title="Edit">
						<img src="WEImages/editgrid.gif" alt="Edit" align="absmiddle" border="0" width="16" height="16"/> Edit Document
					</a>
			</td>
			<!--<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> 
					<a class="LW-font" href="JavaScript:UpdateObj('T_CAT_PART','Update_Item_Folder_E.xsl');void(0);" target="_self" title="Edit">
						<img src="WEImages/editgrid.gif" alt="Edit" align="absmiddle" border="0" width="16" height="16"/> Edit Folder
					</a>
			</td>-->
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes">
					<a class="LW-font" href="JavaScript:DelObj();void(0);" target="_self" title="Delete">
						<img src="WEImages/delete.gif" alt="Delete" align="absmiddle" border="0" width="16" height="16"/> Delete
					</a>
			</td>
			</TR><TR>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" target="_self" href="JavaScript:PermissionsWin();void(0);" title="Permissions"><img src="WEImages/permissions.gif" alt="Permissions" align="absmiddle" border="0" width="16" height="16"/> Permissions</a></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" target="_self" href="JavaScript:CheckIn();void(0);" title="Check In"><img src="WEImages/checkin.gif" alt="Check in" border="0" align="absmiddle" width="25" height="25"/> Check in</a></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" target="_self" href="JavaScript:CheckOut();void(0);" title="Check Out"><img src="WEImages/checkout.gif" alt="Check out" align="absmiddle" border="0" width="25" height="25"/> Check out</a></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" target="_self" href="JavaScript:Revise('Update_WinObject_E.xsl');void(0);" title="Revise"><img src="WEImages/revise.gif" alt="Revise" align="absmiddle" border="0" width="16" height="16"/> Revise</a></td>
			<TD class="LW-separator">|</TD>
			<td class="LW-toolbar" nowrap="yes"> <a class="LW-font" target="_self" href="JavaScript:History();void(0);" title="History"><img src="WEImages/history.gif" alt="History" align="absmiddle" border="0" width="25" height="25"/> History</a></td>
			<!--<TD width="99%" class="LW-toolbar" align="right" nowrap="yes" id="align01">&#160;</TD>-->
		</TR>
	</TABLE>
	<br/><br/>
	</xsl:when>
</xsl:choose>
<!-- End Documents Toolbar -->

<hr  size="5" clolor = "RED"/>

</td>
</tr>
</table>
</div>

<DIV ID="tipwindow" CLASS="TipHelp" STYLE="Top:100; Left:200; display=none; Z-INDEX:200;">
ToolTip text goes in here. Zac (-:</DIV>

<script language="JavaScript">
if (top.frames('Parts Map').document.all[window.map_name.value]+"" != "undefined" )
   {
    fnZoom(window.map_name.value, window.pic_name.value, getCookie('_zoom_ratio'));
    fnSetSearchFocus();
    window.onresize = changeSize;
   }
</script>

</body>
</html>

</xsl:template>

</xsl:stylesheet>