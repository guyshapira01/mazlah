<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="UTF-8" omit-xml-declaration="yes" />



<xsl:template match="/">

<html>
<base target="_parent"/>
<LINK href="Style.css" rel="stylesheet" type="text/css" />
<head>
<title>Table of Parts</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="parts-table.css" TYPE="text/css" />
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>





</head>

<body class="BodyCatalog" dir="rtl">
<div class="parts-shell">
<TABLE class="parts-table" dir="ltr">
    <thead>
    <TR>
        <th>כמות</th>
        <th>שם הפריט</th>
        <th>קוד יצרן</th>
        <th>מספר יצרן</th>
        <th>מק"ט</th>
        <th>מס' חלק</th>
        <th>+</th>
    </TR>
    </thead>
    <tbody>
	
	<xsl:for-each select="Items/Item">
			<TR class="GridMain" style="cursor:pointer;"
        onclick="javascript:
            if(top.frames['Parts Map'] &amp;&amp; top.frames['Parts Map'].selectHotspot){{top.frames['Parts Map'].selectHotspot(this.id);}}">
			<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>
			<TD class="qty-cell"><span class="qty-badge"><xsl:value-of select="Amount"/></span></TD>
			<TD class="part-name"><xsl:value-of select="PartName"/></TD>
			<TD class="code-cell">
				<xsl:for-each select="ManufacturerCode">
					<nobr><font class="GridMain">&#160;<xsl:value-of select="." />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD class="code-cell">
				<xsl:for-each select="ManufacturerNum">
					<nobr><font class="GridMain">&#160;<xsl:value-of select="." />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD class="code-cell">
				<xsl:for-each select="CatalogNum">
					<nobr><font class="GridMainBold">&#160;<xsl:value-of select="." />&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD class="part-number">
				<xsl:for-each select="PartNum">
					<font class="GridMain">&#160;<xsl:value-of select="." />&#160;</font><br/>
				</xsl:for-each>
			</TD>
			<TD class="cart-cell">
                <a class="cart-btn">
                    <xsl:attribute name="href">javascript:alert('<xsl:value-of select="ID" />');void(0)</xsl:attribute>
                    <img src="../images/basket_modern.png" alt="הוסף" />
                </a>
            </TD>
			
		</TR>
	
	</xsl:for-each>
    </tbody>
</TABLE>
<div class="parts-footer">
    <div>סה"כ פריטים: <xsl:value-of select="count(Items/Item)" /></div>
    <div class="legend">
        <span class="legend-item"><span class="legend-dot selected"></span>פריט נבחר</span>
        <span class="legend-item"><span class="legend-dot hovered"></span>מעבר עכבר</span>
    </div>
</div>
</div>

</body>
</html>
</xsl:template>
</xsl:stylesheet>