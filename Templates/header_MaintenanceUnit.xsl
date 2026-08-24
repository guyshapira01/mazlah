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
	<SCRIPT LANGUAGE="javascript1.2" SRC="freeze.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="../SearchPath.js"></SCRIPT>

	<SCRIPT LANGUAGE="javascript">
		document.linkColor="#c7c7ba";
		document.alinkColor="#c7c7ba";
		document.vlinkColor="#c7c7ba";

function Picture(){
		    var frameName;
		    var i;
		    var firstPos;
		    var resultArray;
	  	    resultArray = new Array();
	  	    frameName = new String (top.frames('Parts Map').document.location);
		    
                  firstPos = frameName.indexOf("#");
		    
		    // # not found
		    if (firstPos == -1) {
			frameName = top.frames('Parts Map').document.location + "#Picture";
		    }
		   else {
			   resultArray=frameName.split("#");	
			   
			   if (resultArray[1] = "ItemProperties") {
				frameName= resultArray[0] + "#Picture";
		  	   }
		   	  
			   else if (resultArray[1] = "Picture") {
				top.frames('Parts Map').document.location.href=frameName;			        
		          }
                    }

		    top.frames('Parts Map').document.location.href=frameName;
		}
	
function Prop(){
		    var frameName;
		    var i;
		    var firstPos;
		    var resultArray;
	  	    resultArray = new Array();
	  	    frameName = new String (top.frames('Parts Map').document.location);
		    
                   firstPos = frameName.indexOf("#");
		    
		    // # not found
		    if (firstPos == -1) {
			frameName = top.frames('Parts Map').document.location + "#ItemProperties";
		    }
		   else {
			   resultArray=frameName.split("#");	
			   
			   if (resultArray[1] = "Picture") {
				frameName= resultArray[0] + "#ItemProperties";
		  	   }
		   	  
			   else if (resultArray[1] = "ItemProperties") {
				top.frames('Parts Map').document.location.href=frameName;			        
		          }
                    }

		    top.frames('Parts Map').document.location.href=frameName;
		}
		
</SCRIPT>

	<div align="center">
	<table cellpadding="0" cellspacing="0" border="1" bordercolor="Blue">
		<tr>
  			<td><table><tr><td dir="rtl"><b style="color:red; font-size:80%"><xsl:value-of select="Header/CatalogName" /></b></td><td></td></tr></table></td>
			<td><table><tr><td dir="rtl"><b style="color:green; font-size:80%"><xsl:value-of select="Header/ChapterName" /></b></td><td></td></tr></table></td>
			<td><table><tr><td dir="rtl"><b style="color:black;font-size:80%"><xsl:value-of select="Header/PageHebDesc" /></b></td><td></td></tr></table></td>
			<td><table><tr><td dir="rtl"><b style="color:blue; font-size:80%"><xsl:value-of select="Header/PageNum" /></b></td><td></td></tr></table></td>
		</tr>
	</table>
	</div>

	<div align="center" style="border:2px ;" >
	<table class="TableMainBar" border="0" >
			<tr align="center" >
				<td>
					<div id="scontentmain">
					<div id="scontentbar" >
					<table border="0" id="moveTable">
						<tr>
							<td id="moveTable"><img id="moveTable" style="cursor:hand;" src="ZoomOut.jpg" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)" /><img id="moveTable" style="cursor:hand;" src="ZoomIn.jpg" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.17)" /></td>
							<td id="moveTable"><SPAN id="percent" align="center"></SPAN></td>
							<td align="left" id="moveTable"><div align="center" id="scontentsub"><img src="width.jpg" alt="כל הרוחב" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);" /><img src="100.jpg" alt="100%" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" /><img src="freezeImage.jpg" alt="הקפאה" align="baseline" onclick="javascript:SetZoomSize('');" /></div><SPAN id="freezeImageOk" align="center"></SPAN></td>
						</tr>
					</table>
					</div>
					</div>
				</td>
				<td>
					<nobr>
					<a style="cursor:hand; position:relative ; top : -2.5;" target="_blank" id="mapat_pritim_top"><xsl:attribute name="onclick">javascript:openWin( <xsl:value-of select="Header/WindowSourceFile" />,'MyWin2', 'width=300,height=200,toolbar=0,location=0,directories=0,status=0,menuBar=0,scrollBars=2,resizable=1' )</xsl:attribute><b><IMG src="flowting_table.jpg" alt = " טבלה צפה" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0%,*');"><b><IMG src="table.jpg" alt = "טבלה" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('100%,*');"><b><IMG src="image.jpg" alt ="איור" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('50%,50%');"><b><IMG src="image_table.jpg" alt = "איור וטבלה" /></b></a>
					<a style="cursor:hand; position:relative ; top : -2.5;" onclick="javascript:SaveColRatio();" target="_blank" id="pritim_link_top"><b><IMG src="freeze.jpg" alt = "הקפאה" /></b></a>
					</nobr>
				</td>
				<td>
					<SPAN id="freezeFrameOk" align="center"></SPAN>
				</td>
				<td>
					<!-- <a href="javascript:;" onclick="Prop();"><img src="down.jpg" alt="תכונות פריט" /></a>
					<a href="javascript:;" onclick="Picture();"><img src="up.jpg" alt="תמונת קירושים" /></a> -->
					<nobr>
					<!--
					<a href="javascript:;"><IMG src="back.jpg" alt = "אחורה"	onclick="javascript:parent.history.back();"></a>
					<a href="javascript:;"><IMG src="forward.jpg" align="absbottom" alt ="קדימה" onclick="javascript:parent.history.forward();"></a>
					-->
					<a><xsl:attribute name="href"><xsl:value-of select="Header/Next" /></xsl:attribute><IMG src="next.jpg" alt = "דף הבא" /></a>
					<a><xsl:attribute name="href"><xsl:value-of select="Header/Prev" /></xsl:attribute><IMG src="prev.jpg" alt = "דף קודם" /></a>
					</nobr>
					<a><xsl:attribute name="href"><xsl:value-of select="Header/Index" /></xsl:attribute><IMG src="index.jpg" alt = "תוכן" /></a>
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						 CheckHomeURL();
					</SCRIPT>

					<IMG src="home.jpg" alt="חזרה לעמוד ראשי" />
				</td>

				<td>
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						CheckSearchURL();
					</SCRIPT>
					<IMG src="search.jpg" alt = "חיפוש" />
					<a href="javascript:;" onclick="CheckHelpURL();"><img src="help.jpg" alt="עזרה" /></a>

				</td>


			</tr>
		</table>
		</div>
	
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
