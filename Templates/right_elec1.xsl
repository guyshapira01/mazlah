<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">

<html>
<base target="_parent"/>
<LINK href="Style.css" rel="stylesheet" type="text/css" />
<head>
<title>Table of Parts</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript1.2" SRC="addSearchFocus.js"></SCRIPT>




</head>

<body class="BodyCatalog" dir="rtl" onload="populateSearchBox();">

<TABLE border="1" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr">
    <tr >
		<td colspan="6" bgcolor="yellow">
			
			<select id="sbox" name="sbox" onChange="javascript:parent.frames('Parts Map').searchBoxClicked(this)">
				<option value="1">חפש רכיב בתמונה</option>
			</select> חפש  רכיב בתמונה 
		</td>
	</tr>
	<TR>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">כמות</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">שם הפריט</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">קוד יצרן</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מספר יצרן</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מק"ט</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מס' חלק</FONT></TD>
	</TR>
	<xsl:for-each select="Items/Item">
			<TR class="GridMain" style="cursor:hand;" onclick="javascript:parent.frames('Parts Map').highlightAreaByName(this.id);highlightClick(this.id);">
			<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>
			<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="Amount"/>&#160;</font></TD>
			<TD align="right" DIR="RTL"><font class="GridMainBold">&#160;<xsl:value-of select="PartName"/>&#160;</font></TD>
			<TD align="left">
				<xsl:for-each select="ManufacturerCode">
					<nobr><font class="GridMain">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="left">
				<xsl:for-each select="ManufacturerNum">
					<nobr><font class="GridMain">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="left">
				<xsl:for-each select="CatalogNum">
					<nobr><font class="GridMainBold">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="center">
				<xsl:for-each select="PartNum">
					<font class="GridMain">&#160;<xsl:value-of />&#160;</font><br/>
				</xsl:for-each>
			</TD>
		</TR>
	
	</xsl:for-each>
</TABLE>
<hr  size="5" clolor = "RED"/>
<BR/>

</body>
</html>
</xsl:template>
</xsl:stylesheet>