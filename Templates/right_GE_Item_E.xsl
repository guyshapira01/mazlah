<?xml version="1.0" encoding="ISO-8859-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">

<html>
<base target="_parent"/>
<LINK href="Style_blue.css" rel="stylesheet" type="text/css" />
<head>
<title>Table of Parts</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="catalogicNumber.js"></SCRIPT>
</head>

<body dir="ltr" bgcolor="white">
<style>
BODY 
{
	scrollbar-face-color: #666698;
	scrollbar-shadow-color: #DFDFFC;
	scrollbar-highlight-color: #666698;
	scrollbar-3dlight-color: #666698;
	scrollbar-darkshadow-color: #DFDFFC;
	scrollbar-track-color: #DFDFFC;
	scrollbar-arrow-color: black;
	background-color:white;
}
</style>
<table border="0"><tr><td style="background-color:#FFFFCD"><font style="font-size:12px;font-color:black;font-family:Arial"><b>Part List</b></font></td></tr></table>
<br/><br/>
<script language="JavaScript">
	i=1;
</script>
<TABLE border="1" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr" width="100%">
    <TR style="background-color:#666698;font-weight:bold;">
	<TD style="BORDER-BOTTOM: #333333 1px solid;BORDER-LEFT: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid" align="center"><FONT style="font-size:10px;color:white;font-family:Arial"><b>#</b><!--Sequence Number--></FONT></TD>
	<TD style="BORDER-BOTTOM: #333333 1px solid;BORDER-LEFT: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid" align="center"><FONT style="font-size:10px;color:white;font-family:Arial"><b>Part Number</b></FONT></TD>
	<TD style="BORDER-BOTTOM: #333333 1px solid;BORDER-LEFT: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid" align="center"><FONT style="font-size:10px;color:white;font-family:Arial"><b>Part Description</b></FONT></TD>
		
	
	</TR>
	<xsl:for-each select="Items/Item">
			
		<TR style="background-color:white;">
			<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>
			<td><font style="color:red;font-size:10px;font-family:Arial;"><script language="JavaScript">document.write(i);i++;</script></font>
			</td>
			<TD align="center">
				<font style="color:black;font-size:10px;font-family:Arial;">&#160;<xsl:value-of select="PARTNUMBER"/>&#160;</font>
			</TD>
			<TD align="left">
				<font size="2">&#160;<a style="color:black;font-size:10px;font-family:Arial;"><xsl:attribute name="href">
				<xsl:choose>
					<xsl:when match=".[//IsWebEditor='True']">JavaScript:top.frames('Index').LinkSwitch('<xsl:value-of select="ID"/>');</xsl:when>
					<xsl:otherwise><xsl:value-of select="ID" />.html</xsl:otherwise>
				</xsl:choose></xsl:attribute>
				<xsl:value-of select="PartName"/></a>&#160;</font>
			</TD>
		</TR>
	
	</xsl:for-each>
</TABLE>
<!--<hr  size="5" clolor = "RED"/>
<BR/>
<DIV align="CENTER">
Update Date:
<script language="javascript">
document.write(document.lastModified);
</script>
</DIV>-->
</body>
</html>
</xsl:template>
</xsl:stylesheet>