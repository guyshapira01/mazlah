<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">

	<html>
	<base target="_parent" />
	<head>
	<title>Header</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1255" />

	<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
	<!-- <LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css"> -->
	</head>

	<body class="BodyCatalog">

	<SCRIPT LANGUAGE="javascript" SRC="tooltip.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="mapzoom.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="specialfx.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="ontop.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="cookutil.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="panning.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="events.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="addSearchFocus.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="srcpath.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="freeze_E.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="../SearchPath.js"></SCRIPT>
	<SCRIPT LANGUAGE="vbscript" SRC="notes.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript">
		document.linkColor="#c7c7ba";
		document.alinkColor="#c7c7ba";
		document.vlinkColor="#c7c7ba";
	</SCRIPT>

	<div align="center">
	<table cellpadding="0" cellspacing="0" border="1" bordercolor="Blue" dir="rtl">
		<tr>
  			<td><table><tr><td dir="ltr"><b style="color:red; font-size:80%"><xsl:value-of select="Header/CatalogName" /></b></td><td></td></tr></table></td>
			<td><table><tr><td dir="ltr"><b style="color:green; font-size:80%"><xsl:value-of select="Header/ChapterName" /></b></td><td></td></tr></table></td>
			<td><table><tr><td dir="ltr"><b style="color:black;font-size:80%"><xsl:value-of select="Header/PageHebDesc" /></b></td><td></td></tr></table></td>
			<td><table><tr><td dir="ltr"><b style="color:blue; font-size:80%"><xsl:value-of select="Header/PageNum" /></b></td><td></td></tr></table></td>
		</tr>
	</table>
	</div>

	<div align="center" style="border:2px ;" >
	<table class="TableMainBarDOC" border="0" >
			<tr align="center" >
				<td>
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						CheckSearchURL();
					</SCRIPT>
					<IMG src="search.jpg" alt = "Search" />
					<a href="javascript:;" onclick="CheckHelpURL_E();"><img src="help.jpg" alt="Help" /></a>
				</td>
				<td>
					<nobr>
					<!--
					<a href="javascript:;"><IMG src="back.jpg" alt = "Back" onclick="javascript:parent.history.back();"></a>
					<a href="javascript:;"><IMG src="forward.jpg" align="absbottom" alt ="÷????" onclick="javascript:parent.history.forward();"></a>
					-->
					<xsl:choose>
					    <xsl:when match=".[Header/Prev $eq$ '']">					    
			     		    </xsl:when>
					    <xsl:otherwise>
						<a><xsl:attribute name="href"><xsl:value-of select="Header/Prev" /></xsl:attribute><IMG src="next.jpg" alt = "Previous Page" /></a>
					    </xsl:otherwise>
					</xsl:choose>
					
					
					<xsl:choose>
					    <xsl:when match=".[Header/Next $eq$ '']">					    
			     		    </xsl:when>
					    <xsl:otherwise>
					       <a><xsl:attribute name="href"><xsl:value-of select="Header/Next" /></xsl:attribute><IMG src="prev.jpg" alt = "Next Page" /></a>
					    </xsl:otherwise>
					</xsl:choose>

					</nobr>
					<a><xsl:attribute name="href"><xsl:value-of select="Header/Index" /></xsl:attribute><IMG src="index.jpg" alt = "Index" /></a>
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						 CheckHomeURL();
					</SCRIPT>

					<IMG src="home.jpg" alt="Back To HomePage" />
			<xsl:choose>
				<xsl:when match=".[//IsWebEditor='False']">
					<a>
					<xsl:attribute name="href">JavaScript:window.open('notes.htm?FilePath=<xsl:value-of select="Header/pkey" />&amp;desc=<xsl:value-of select="Header/PageHebDesc" />','notes','scrollbars=yes, toolbar, resizable, width=550, height=280');void(0);</xsl:attribute>
					<img id="imgnote" src="info.gif"/>
					</a>
					<SCRIPT LANGUAGE="vbscript">
						checkpkey("<xsl:value-of select="Header/pkey" />")
					</SCRIPT>
				</xsl:when>
			</xsl:choose>
					
				</td>
				<td>
					<nobr>
					<a style="cursor:hand; position:relative ; top : -2.5;" target="_blank" id="mapat_pritim_top"><xsl:attribute name="onclick">javascript:openWin( <xsl:value-of select="Header/WindowSourceFile" />,'MyWin2', 'width=300,height=200,toolbar=0,location=0,directories=0,status=0,menuBar=0,scrollBars=2,resizable=1' )</xsl:attribute><b><IMG src="flowting_table.jpg" alt = "Flowting Table" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('100%,*,*');"><b><IMG src="table.jpg" alt = "Table" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('*,100%,*');"><b><IMG src="image.jpg" alt ="Image" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('35%,65%,*');"><b><IMG src="image_table.jpg" alt = "Image And Table" /></b></a>
					<a style="cursor:hand; position:relative ; top : -2.5;" onclick="javascript:SaveColRatio();" target="_blank" id="pritim_link_top"><b><IMG src="freeze.jpg" alt = "Freeze Frames" /></b></a>
					</nobr>
				</td>
				<td>
					<div id="scontentmain">
					<div id="scontentbar" >
					<table border="0" id="moveTable">
						<tr>
							<td id="moveTable"><img id="moveTable" style="cursor:hand;" src="ZoomOut.jpg" alt="Zoom Out" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)" /><img id="moveTable" style="cursor:hand;" src="ZoomIn.jpg" alt="Zoom In" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.17)" /></td>
							<td id="moveTable"><SPAN id="percent" align="center"></SPAN></td>
							<td align="left" id="moveTable"><div align="center" id="scontentsub"><img src="width.jpg" alt="Fit To Page" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);" /><img src="100.jpg" alt="100%" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" /><img src="freezeImage.jpg" alt="Freeze Image" align="baseline" onclick="javascript:SetZoomSize('');" /></div><SPAN id="freezeImageOk" align="center"></SPAN></td>
						</tr>
					</table>
					</div>
					</div>
				</td>
				<td>
					<SPAN id="freezeFrameOk" align="center"></SPAN>
				</td>
				<td>
					<xsl:value-of select="Header/UserLogon" />
				</td>

			</tr>
		</table>
		</div>
	
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
