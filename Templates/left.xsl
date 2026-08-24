<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">

	<html>

	<base target="_parent" />
	<head>
	<title>Catalog Page</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1255" />
	</head>

	<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
	<LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css" />
	<SCRIPT LANGUAGE="JavaScript" SRC="tooltip.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript" SRC="mapzoom.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript1.2" SRC="ontop.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript1.2" SRC="cookutil.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript" SRC="panning.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript" SRC="events.js"></SCRIPT>
	<SCRIPT LANGUAGE="JavaScript1.2" SRC="addSearchFocus.js"></SCRIPT>
	
	<input type="hidden" name="pic_name"><xsl:attribute name="value"><xsl:value-of select="map/PicName"/></xsl:attribute></input>
	
	<input type="hidden" name="map_name"><xsl:attribute name="value"><xsl:value-of select="map/MapName"/></xsl:attribute></input>

	<body class="BodyCatalog">
	<SPAN id="globalAreaId" style="visibilty:hidden;"></SPAN>

	<div align="center">
	<table align="center">
		<tr>
			<td><xsl:value-of select="map/Text1" /></td>
			<td><xsl:value-of select="map/Text2" /></td>
			<td><xsl:value-of select="map/Text3" /></td>
		</tr>
		<tr>
			<td><xsl:value-of select="map/Text4" /></td>
			<td align="center"><xsl:value-of select="map/Text5" /><IMG  name="SearchFocus" src="blankSearchFocus.gif" STYLE="position:absolute; top:0px ; left:0px; z-index : 100 visibilty : :hidden" onclick="javascript:hideSearchImage()" />
		
		
			<xsl:apply-templates select="map/mapOrd" />
		
		<!-- <# MAP ORDINAL=1 LABLE1="שם" LABLE2="מק"ט" LABLE3="הערה" #> -->
		
		
			</td>
		
		
			<td><xsl:value-of select="map/Text6" /></td>
		</tr>
		<tr>
			<td><xsl:value-of select="map/Text7" /></td>
			<td><xsl:value-of select="map/Text8" /></td>
			<td><xsl:value-of select="map/Text9" /></td>
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
				window.onresize = changeSize ;
			}

	</script>

	</body>
	</html>
	
</xsl:template>

<xsl:template match="mapOrd">

	<MAP><xsl:attribute name="Name"><xsl:value-of select="//MapName" /></xsl:attribute>
	<xsl:apply-templates select="Area" />
	<AREA></AREA>
	</MAP>
	<IMG style="position:relative; visibility:hidden;"><xsl:attribute name="SRC"><xsl:value-of select="imgSrc" /></xsl:attribute><xsl:attribute name="USEMAP">#<xsl:value-of select="//MapName" /></xsl:attribute><xsl:attribute name="name"><xsl:value-of select="//PicName" /></xsl:attribute></IMG>
	
</xsl:template>

<xsl:template match="Area">

	<AREA><xsl:attribute name="Shape"><xsl:value-of select="Shape" /></xsl:attribute>
		<xsl:attribute name="Id"><xsl:value-of select="ID" /></xsl:attribute>
		<xsl:attribute name="onmouseover">javascript:ToolTipMultiLine('<xsl:value-of select="ToolTipMultiLine" />'); highlight('<xsl:value-of select="Highlight" />');Focus('<xsl:value-of select="Focus" />');</xsl:attribute>
		<xsl:attribute name="onmouseout">javascript:HideToolTip(); unlight('<xsl:value-of select="Highlight" />');</xsl:attribute>
		<xsl:attribute name="Coords"><xsl:value-of select="Coords" /></xsl:attribute>
		<xsl:attribute name="href"><xsl:value-of select="Href" /></xsl:attribute>
		<xsl:if match=".[Href $ne$ 'javascript:;']">
			<xsl:attribute name="target"><xsl:value-of select="Target" /></xsl:attribute>
		</xsl:if>
	
	</AREA>	
	
</xsl:template>

</xsl:stylesheet>