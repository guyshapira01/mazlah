<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">

<html>
<base target="_parent"/>
<LINK href="Style.css" rel="stylesheet" type="text/css" />
<head>
<title>Table of Parts</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
</head>

<body class="BodyCatalog" dir="rtl">

<TABLE border="1" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr">
    <TR>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">Manufacturer Code</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">Manufacturer Number</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">Quantity</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">Item ID</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">Item Name</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">Sequence Number</FONT></TD>
	</TR>
	<xsl:for-each select="Items/Item">
	
		<TR class="GridMain">
			<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>
			<TD align="left">
				<xsl:for-each select="ManufacturerCode">
					<nobr><font size="2">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="left">
				<xsl:for-each select="ManufacturerNum">
					<nobr><font size="2">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="center"><font size="2">&#160;<xsl:value-of select="Amount"/>&#160;</font></TD>
			<TD align="left">
				<xsl:for-each select="CatalogNum">
					<nobr><font size="2">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="right"><font size="2">&#160;<xsl:value-of select="PartName"/>&#160;</font></TD>
			<TD align="center">
				<xsl:for-each select="PartNum">
					<font size="2">&#160;<xsl:value-of />&#160;</font><br/>
				</xsl:for-each>
			</TD>
		</TR>
	
	</xsl:for-each>
</TABLE>
<hr  size="5" clolor = "RED"/>
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