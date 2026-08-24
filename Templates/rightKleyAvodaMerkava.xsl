<?xml version="1.0" encoding="ISO-8859-8"?>
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

<TABLE border="1" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="LTR">
    <TR>
	<td class="GridHeader" align="center" colspan="7">מיועד לשימוש ב*
	</td>
	<td colspan="6">&#160;
	</td>
   </TR>
  <TR>
<TD class="GridHeader" align="center"><FONT class="GridHeader">מחקלה</FONT></TD>
<TD class="GridHeader" align="center"><FONT class="GridHeader">מחלקה בגש"ח</FONT></TD>
<TD class="GridHeader" align="center"><FONT class="GridHeader">כתה בגש"ח</FONT></TD>
<TD class="GridHeader" align="center"><FONT class="GridHeader">מח"ג</FONT></TD>
<TD class="GridHeader" align="center"><FONT class="GridHeader">חט"פ</FONT></TD>
<TD class="GridHeader" align="center"><FONT class="GridHeader">זיווד</FONT></TD>
<TD class="GridHeader" align="center"><FONT class="GridHeader">מרכז טיפולים</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">ייעוד הכלי</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">שם  הכלי</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">קוד יצרן</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מספר יצרן</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מק"ט</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מס' חלק</FONT></TD>
	</TR>
	<xsl:for-each select="Items/Item">
	
		<TR class="GridMain">
			<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>





<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="Mahlaka"/>&#160;</font></TD>
<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="MahlakaGashach"/>&#160;</font></TD>
<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="KitaGashach"/>&#160;</font></TD>
<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="mahag"/>&#160;</font></TD>
<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="hatap"/>&#160;</font></TD>
<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="zivud"/>&#160;</font></TD>
<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="merkazTipul"/>&#160;</font></TD>

			<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="yeud"/>&#160;</font></TD>
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
<DIV align="CENTER">
דף זה מעודכן נכון ל:
<script language="javascript">
document.write(document.lastModified);
</script>
</DIV>
</body>
</html>
</xsl:template>
</xsl:stylesheet>