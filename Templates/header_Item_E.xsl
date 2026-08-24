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
	<SCRIPT LANGUAGE="javascript" SRC="WebEditor.js"></SCRIPT>
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

<SCRIPT LANGUAGE="JavaScript">
	var expdate = new Date();
	expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
</SCRIPT>	
<TEXTAREA ID="holdtext" STYLE="display:none;"></TEXTAREA>
	
	<div align="center">
	<table border="0" width="100%">
		<tr>
			<td width="100">
				<img src="LinkWareIcon.GIF"/>
				<!--Logo Should Come Here-->
			</td>
			<td width="100%" align="center">
	<table cellpadding="0" cellspacing="0" border="1" bordercolor="Blue" dir="rtl">
		<tr>
			<td><table><tr><td dir="ltr"><b style="color:black;font-size:80%"><span id="ItemName" name="ItemName"><xsl:value-of select="Header/PageHebDesc" /></span></b></td><td></td></tr></table></td>
  			<td><table><tr><td dir="ltr"><b style="color:red; font-size:80%"><span id="CatalogName" name="CatalogName"><xsl:value-of select="Header/CatalogName" /></span></b></td><td></td></tr></table></td>
		</tr>
	</table>
			</td>
			<td width="100">
				<!--Logo Should Come Here-->
			</td>
		</tr>
	</table>
	</div>

	<div align="center" style="border:2px ;" >
	<table class="TableMainBar" border="0" >
			<tr align="center" >

				<td align="left">&#160;&#160;&#160;&#160;&#160;&#160;
		<!--
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						 CheckHomeURL();
					</SCRIPT>
					<IMG src="home.jpg" alt="Back To Home" />
		-->					

					<a target="_blank">
						<xsl:attribute name="href">
							../../LWWebSiteSearchEngine/default.aspx?TypeProperty=&amp;Object=Item&amp;SearchPopulation=Book&amp;SearchPopulationSpecify=
							<xsl:value-of select="Header/CatalogName" />
						</xsl:attribute> 
						<IMG border="0" src="search.jpg" alt = "Search" />
					</a>					
					

			<!--    *****   Help is not avaliable   ***************   -->
	<!--		<a href="javascript:;" onclick="CheckHelpURL_E();"><img border="0" src="help.jpg" alt="Help" /></a>
	
				<a href="javascript:;" style="cursor:hand;" onclick="javascript:CopiedURL=CopyUrl();setCookie('TabNo', '1',expdate);w=window.open('../generatereport.aspx?Catalog=107779a1&amp;Table=T_CAT_PART&amp;XSL=Portal-Change%20Preference.xsl&amp;PPROP7=currentuser&amp;PPUBLISH=1','PortalPreference','width=750,height=700,top=5,left=50,resizable=yes,toolbar=no,scrollbars=yes');w.focus();"><b><IMG src="fav.jpg" alt ="Add to Favorites" /></b></a>
	-->
				<a href="javascript:;" style="cursor:hand;">
					<xsl:attribute name="onclick">javascript:CopiedURL=CopyUrl();setCookie('TabNo', '1',expdate);w=window.open('../generatereport.aspx?Catalog=107779a1&amp;Table=T_CAT_PART&amp;XSL=Portal-Change%20Preference.xsl&amp;PPROP7=currentuser&amp;PPUBLISH=1','PortalPreference','width=750,height=700,top=5,left=50,resizable=yes,toolbar=no,scrollbars=yes');w.focus();</xsl:attribute>
					<b><IMG src="fav.jpg" alt ="Add to Favorites" /></b>
				</a>
				<a href="javascript:;" style="cursor:hand;">
					<xsl:attribute name="onclick">javascript:MailLink_Tree(CopyUrl(),'LW - ' + '<xsl:value-of select="Header/CatalogName" />' + ' - ' + '<xsl:value-of select="Header/PageHebDesc" />','');</xsl:attribute>
					<b><IMG src="MailURL.jpg" alt ="Send URL by Mail" /></b>
				</a>
				<a href="javascript:;" style="cursor:hand;">
					<xsl:attribute name="onclick">javascript:CopyUrl();</xsl:attribute>
					<b><IMG src="CopyURL.jpg" alt ="Copy URL" /></b>
				</a>
	
				</td>

				<td align="left">
					<nobr>
<!--				<a style="cursor:hand; position:relative ; top : -2.5;" target="_blank" id="mapat_pritim_top"><xsl:attribute name="onclick">javascript:openWin( <xsl:value-of select="Header/WindowSourceFile" />,'MyWin2', 'width=300,height=200,toolbar=0,location=0,directories=0,status=0,menuBar=0,scrollBars=2,resizable=1' )</xsl:attribute><b><IMG src="flowting_table.jpg" alt = "Flowting Table" /></b></a>
-->
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0,*,100%');"><b><IMG src="table.jpg" alt = "Table" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0,100%,*');"><b><IMG src="image.jpg" alt ="Image" /></b></a>
<!--					
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0,35%,65%');"><b><IMG src="image_table.jpg" alt = "Image And Table" /></b></a>
-->
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('30%,70%,*');"><b><IMG src="image_index.jpg" alt = "Image And Index" /></b></a>

<!--				<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('20%,40%,45%');"><b><IMG src="image_table_index.jpg" alt ="Image, Table And Index" /></b></a>
-->
					<a style="cursor:hand; position:relative ; top : -2.5;" onclick="javascript:SaveColRatio_IndexPicture();" target="_blank" id="pritim_link_top"><b><IMG src="freeze.jpg" alt = "Freeze Frames" /></b></a>

					</nobr>
				</td>
<!--			<td>
					<div id="scontentmain">
					<div id="scontentbar" >
					<table border="0" id="moveTable">
						<tr>
							<td id="moveTable">&#160;<img src="down.jpg" alt="Item Properties" onclick="Prop();"/>&#160;<img src="up.jpg" alt="Image Map" onclick="Picture();"/>
							&#160;<img id="moveTable" style="cursor:hand;" src="ZoomOut.jpg" alt="Zoom Out" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)" />&#160;<img id="moveTable" style="cursor:hand;" src="ZoomIn.jpg" alt="Zoom In" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.17)" /></td>
							<td id="moveTable"><SPAN id="percent" align="center"></SPAN></td>
							<td align="left" id="moveTable"><div align="center" id="scontentsub">&#160;<img src="width.jpg" alt="Fit To Page" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);SetZoomSize('FIT_TO_PG');" />&#160;<img src="100.jpg" alt="100%" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" />&#160;<img src="freezeImage.jpg" alt="Freeze Image" align="baseline" onclick="javascript:SetZoomSize('');" /></div><SPAN id="freezeImageOk" align="center"></SPAN></td>
						</tr>
					</table>
					</div>
					</div>
				</td>
-->
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
