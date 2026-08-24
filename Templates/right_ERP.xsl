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

<body class="BodyCatalog" dir="rtl" color="red">

<script language="JAvaScript">
	
	function submForm()
		{
			document.erp12.submit();
		}

</script>
<TABLE border="1" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr" bgcolor="red">
    <TR>
	<TD align="center"><FONT class="GridHeader">SECTION-COMPONENT</FONT></TD>
	<TD  align="center"><FONT class="GridHeader">משקל</FONT></TD>
	<TD  align="center"><FONT class="GridHeader">חומר</FONT></TD>
	<TD  align="center"><FONT class="GridHeader">תאור</FONT></TD>
	<TD  align="center"><FONT class="GridHeader">כמות</FONT></TD>
	<TD  align="center"><FONT class="GridHeader">שם הפריט</FONT></TD>
	<!-- <TD  align="center"><FONT class="GridHeader">קוד יצרן</FONT></TD> -->
	<TD  align="center"><FONT class="GridHeader">מספר קטלוגי</FONT></TD>
	<TD align="center"><FONT class="GridHeader">מס' חלק</FONT></TD>
	<TD  align="center"><FONT class="GridHeader"> ERP הזמנה</FONT></TD>


	</TR>
	<xsl:for-each select="Items/Item">
	
		<TR class="GridMain">
			<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>
			<TD align="center"><font size="2">&#160;<xsl:value-of select="SECTION-COMPONENT"/>&#160;</font></TD>
			<TD align="center"><font size="2">&#160;<xsl:value-of select="Weight"/>&#160;</font></TD>
			<TD align="center"><font size="2">&#160;<xsl:value-of select="Material"/>&#160;</font></TD>
			<TD align="center"><font size="2">&#160;<xsl:value-of select="Desc"/>&#160;</font></TD>

			<TD align="center"><font size="2">&#160;<xsl:value-of select="Amount"/>&#160;</font></TD>
			<TD align="right"><font size="2"><a><xsl:attribute name="href"><xsl:value-of select="ID" />.html</xsl:attribute>&#160;<xsl:value-of select="PartName"/>&#160;</a></font></TD>
			<!--<TD align="left">
				<xsl:for-each select="ManufacturerCode">
					<nobr><font size="2">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>-->
			<TD align="left">
				<xsl:for-each select="ManufacturerNum">
					<nobr><font size="2">&#160;<xsl:value-of />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="center">
				<xsl:for-each select="PartNum">
					<font size="2">&#160;<xsl:value-of />&#160;</font><br/>
				</xsl:for-each>
			</TD>

			<TD align="center"><nobr>
				<form target="_blank" method="get" action="http://10.200.200.75/catmamweb/erp.asp" name="erp12">
				<input type="text" name="count" id="count"/>
				<input type="hidden" name="itemId"><xsl:attribute name="value"><xsl:value-of select="ManufacturerNum" /></xsl:attribute></input>
				<input type="hidden" name="itemName"><xsl:attribute name="value"><xsl:value-of select="PartName" /></xsl:attribute></input>
				<input type="image" src="buy.gif" id="submit1" name="submit1"/>
				</form>
				</nobr>
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