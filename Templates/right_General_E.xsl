<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">
<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css" />
<html>
<base target="_parent"/>
<!--<LINK href="Style.css" rel="stylesheet" type="text/css" />-->
<head>
<title>Table of Parts</title>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<!-- <LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />  -->
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx_E_General.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="ToolBar.js"></SCRIPT>
<!-- LINK REL="stylesheet" HREF="WebEditor.css" TYPE="text/css" /> -->
<SCRIPT LANGUAGE="JavaScript" SRC="WebEditor.js"></SCRIPT>
</head>

<body class="BodyCatalog"  leftmargin="0" topmargin="0"  dir="ltr">

<br></br>

<TABLE class="ToolBarTable"  cellPadding="2" cellSpacing="0" align="center"  dir="ltr" width="100%">
 <TR >
    <td style="font-weight:bolder;" width="100%" colspan="12"><nobr>Items Table</nobr></td>
  </TR>
    
    <TR class="TableHeader">
    <TD align="left" class="TableHeader" width="1">&#160;</TD>
    <TD align="left" class="TableHeader" width="1">&#160;</TD>
    
    <TD  class="TableHeader"><nobr>Item ID</nobr></TD>
	<TD  class="TableHeader"><nobr>Item Name</nobr></TD>
	<TD  class="TableHeader"><nobr>Description</nobr></TD>
	<TD class="TableHeader"><nobr>Quantity</nobr></TD>
	<TD align="left" class="TableHeader" width="1">Status</TD>
	<TD  class="TableHeader"><nobr>Owner</nobr></TD>
	<TD class="TableHeader"><nobr>Sequence No.</nobr></TD>
	<TD class="TableHeader"><nobr>Created Date</nobr></TD>
	<TD class="TableHeader"><nobr>Revision</nobr></TD>
	<TD class="TableHeader"><nobr>Group</nobr></TD>
	<TD class="TableHeader"><nobr>Price</nobr></TD>
	<TD class="TableHeader"><nobr>Weight</nobr></TD>
	<TD class="TableHeader"><nobr>Project</nobr></TD>
	<TD class="TableHeader"><nobr>Classification</nobr></TD>
    </TR>
    
    <script>
    var iItems = 0;
    </script>
   
	<xsl:for-each select="Items/Item">
	<script>
			iItems = iItems + 1;
    </script>
			<xsl:choose>
			    <xsl:when match=".[PUBLISH $eq$ '1']">
				<TR class="GridText" style="background-color:WhiteSmoke;">
					<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>
			<xsl:choose>
				<xsl:when match=".[//IsWebEditor='True']">
					<td width="1"  class="tabletextnew"><INPUT type="checkbox" id="PDItem"><xsl:attribute name="name"><xsl:value-of select="ID" /></xsl:attribute><!--<xsl:attribute name="onclick">javaScript:CheckOnlyOne(this,'PDItem');</xsl:attribute>--></INPUT></td>
				</xsl:when>
				<xsl:otherwise><td width="1"></td></xsl:otherwise>
			</xsl:choose>
				<input type="hidden">
						<xsl:attribute name="value"><xsl:value-of select="PIMPORTKEYVALUE"/></xsl:attribute>
						<xsl:attribute name="name"><xsl:value-of select="ID"/>_PDOCID</xsl:attribute>
					</input>

					<TD align="center" nowrap="true" class="tabletextnew">
						<IMG>
						<xsl:attribute name="src">IconTypes/<xsl:value-of select="ITEM_OBJECT_TYPE"/>.gif</xsl:attribute>
						</IMG>

					</TD>

					
					<TD align="center" nowrap="true" class="tabletextnew">
						<font size="2">&#160;<xsl:value-of select="PPROP1"/>&#160;</font>
					</TD>

					<TD align="left" nowrap="true" class="tabletextnew">
						<font size="2">&#160;<a><xsl:attribute name="href">
						<xsl:choose>
							<xsl:when match=".[//IsWebEditor='True']">JavaScript:top.frames('Index').LinkSwitch('<xsl:value-of select="ID" />');</xsl:when>
							<xsl:otherwise><xsl:value-of select="ID" />.html</xsl:otherwise>
						</xsl:choose></xsl:attribute>
						<xsl:value-of select="PartName"/></a>
				&#160;</font>
					</TD>

					<TD nowrap="yes" align="center" class="tabletextnew">
						<font size="2">&#160;<xsl:value-of select="PENGDESC"/>&#160;</font>
					</TD>		

					<TD nowrap="yes" align="center" class="tabletextnew">
						<font size="2">&#160;<xsl:value-of select="OCPROP1"/>&#160;</font>
					</TD>		

					<TD align="center" nowrap="true" class="tabletextnew">
						<!--<font size="2">&#160;<xsl:value-of select="PARTSTATUS"/>&#160;</font>-->
						<IMG>
						<xsl:attribute name="id">statimg_<xsl:value-of select="ID"/></xsl:attribute>
						<xsl:attribute name="alt"><xsl:value-of select="PARTSTATUS"/></xsl:attribute>
						<script>
						if ('<xsl:value-of select="PARTSTATUS"/>' != '')
							 document.getElementById('statimg_<xsl:value-of select="ID"/>').src='IconTypes/<xsl:value-of select="PARTSTATUS"/>.gif';
						 else
							 document.getElementById('statimg_<xsl:value-of select="ID"/>').src='IconTypes/na.gif';
						</script>
						</IMG>
					</TD>


					<TD nowrap="yes" align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PKEYUSER"/>&#160;</font>
					</TD>

					<TD nowrap="yes" align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PPROP3"/>&#160;</font>
					</TD>

					<TD nowrap="yes" align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PCREATEDATE"/>&#160;</font>
					</TD>
					<TD align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PREVISION"/>&#160;</font>
					</TD>
					<TD align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PPROP6"/>&#160;</font>
					</TD>


					<TD align="center" class="tabletextnew">
						<font size="2">&#160;<xsl:value-of select="PPROP7"/>&#160;</font>
					</TD>

					<TD align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PPROP8"/>&#160;</font>
					</TD>
					<TD align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PPROP9"/>&#160;</font>
					</TD>
					<TD align="center" class="tabletextnew">
									<font size="2">&#160;<xsl:value-of select="PPROP10"/>&#160;</font>
					</TD>
				</TR>
		    </xsl:when>
		</xsl:choose>				
	
	</xsl:for-each>
	<TR>
		<td colspan="18">
			<xsl:choose>
				<xsl:when match=".[//IsWebEditor='True']">
			<!-- Start Item Part Toolbar -->

					<xsl:if match=".[//ACLPERMISSIONSNAME = 'Administrator']">
				
				<TABLE class="ToolBar" cellpadding="0" cellspacing="0" width="100%">
				<TR>
				<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   New XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
						<td class="ToolBarGlow" onClick="JavaScript:AddObj('New_Item_General_E.xsl');void(0);" target="_self" title="New Item"> 
								<table width="100%" cellspacing="2" cellpadding="2" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
									<tr>
										<td nowrap="yes">
											<a >
												<img src="WEImages/newdoc.gif"  border="0" width="16" height="16"/>
											</a>
											<a class="ToolBarFont"> &#160;New</a>
										</td>
									</tr>
								</table>
						</td>
						<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Cut XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
						<td class="ToolBarGlow" onClick="javascript:CutObj();void(0);" target="_self" title="Cut">
							<table width="100%" cellspacing="2" cellpadding="2" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
							<tr><td nowrap="yes">
							<a >
							<img src="WEImages/tbcut.gif" border="0" width="16" height="16"/>
							</a><a class="ToolBarFont" > &#160;Cut </a>
							</td></tr>
						</table>
					</td>
					<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Copy XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
						<td class="ToolBarGlow" onClick="javascript:CopyObj();void(0);" target="_self" title="Copy"> 
							<table width="100%" cellspacing="2" cellpadding="2" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
							<tr><td nowrap="yes">
							<a >
							<img src="WEImages/tbcopy.gif" border="0" width="16" height="16"/>
							</a><a class="ToolBarFont" > &#160;Copy </a>
							</td></tr>
						</table>
					</td>
					<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Paste XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
						<td class="ToolBarGlow"  target="_self" title="Paste" onClick="javascript:PasteObj('','','','');void(0);"> 
							<table width="100%" cellspacing="2" cellpadding="2" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
							<tr><td nowrap="yes">
							<a >
							<img src="WEImages/tbpaste.gif" border="0" width="16" height="16"/>
							</a><a class="ToolBarFont">&#160; Paste </a>
							</td></tr>
						</table>
					</td>
					<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Edit XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
						<td class="ToolBarGlow" target="_self" title="Edit" onClick="JavaScript:UpdateObj('T_CAT_PART','Update_Item_General_E.xsl');void(0);" > 
								<table width="100%" cellspacing="2" cellpadding="2" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
									<tr>
										<td nowrap="yes">
											<a>
												<img src="WEImages/editgrid.gif" border="0" width="16" height="16"/>
											</a>
											<a class="ToolBarFont">&#160; Edit</a>
										</td>
									</tr>
								</table>
						</td>
						<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Delete XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
						<td class="ToolBarGlow" onClick="JavaScript:DelObj();void(0);"  target="_self" title="Delete">
								<table width="100%" cellspacing="2" cellpadding="2" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
									<tr>
										<td nowrap="yes">
											<a>
												<img src="WEImages/delete.gif" border="0" width="16" height="16"/>
											</a>
											<a class="ToolBarFont"> &#160;Delete</a>
										</td>
									</tr>
								</table>
						</td>

					<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Reorder XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
					<td  class="ToolBarGlow" onClick="JavaScript:ReOrderObj(0);void(0);" target="_self" title="ReOrder">
						<table width="100%" cellspacing="2" cellpadding="2" ID="Table17" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
							<tr class="ToolBarFont">
								<td nowrap="yes">
									<a >
										<img src="WEImages/reorder.gif" border="0" width="16" height="16"/>
									</a>
									<a class="ToolBarFont">&#160;ReOrder</a>
								</td>
							</tr>
						</table>
					</td>
					<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   Revise XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
					<td  class="ToolBarGlow" onClick="JavaScript:Revise('T_CAT_PART','Update_Item_General_E.xsl','Item');void(0);" target="_self" title="Revise">
						<table width="100%" cellspacing="2" cellpadding="2" ID="Table17" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
							<tr class="ToolBarFont">
								<td nowrap="yes">
									<a >
										<img src="WEImages/revise.gif" border="0" width="16" height="16"/>
									</a>
									<a class="ToolBarFont">&#160;Revise</a>
								</td>
							</tr>
						</table>
					</td>
					<!-- XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   History XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX   -->
					<td  class="ToolBarGlow" target="_self" title="History">
					<xsl:attribute name="onClick">JavaScript:HistoryItems('T_CAT_PART','<xsl:value-of select="//PIMPORTKEYPROP"/>');void(0);</xsl:attribute>
						<table width="100%" cellspacing="2" cellpadding="2" ID="Table17" onload="glow(false, this);" onmouseover="glow(true, this);"  onmouseout="glow(false, this);">
							<tr class="ToolBarFont">
								<td nowrap="yes">
								<a class="LW-font">
								<img src="WEImages/history.gif" align="absmiddle" border="0" width="16" height="16"/>
									History
								</a>
								</td>
							</tr>
						</table>
					</td>

						<TD class="ToolBarGlow">&#160;</TD>
						
					</TR>
				</TABLE>
					</xsl:if>				
				</xsl:when>
			</xsl:choose>
			<!-- End Part Toolbar -->
		</td>
	</TR>
</TABLE>
<BR/>
<BR/>
<DIV align="CENTER">
This Page Update At:
<script language="javascript">
document.write(document.lastModified);
</script>
</DIV>
</body>
</html>
</xsl:template>
</xsl:stylesheet>