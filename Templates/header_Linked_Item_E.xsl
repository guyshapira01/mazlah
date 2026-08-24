<?xml version="1.0" encoding="ISO-8859-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">

	<html>
	<!--<base target="_parent" />-->
	<head>
	<title>Header</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1255" />

	<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
	<!-- <LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css"> -->
	</head>

	<body class="BodyCatalog" topMargin="0" leftMargin="0">

	<SCRIPT LANGUAGE="javascript" SRC="tooltip.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="mapzoom_orbotech_e.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="specialfx.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="ontop.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="cookutil.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="panning.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="events.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="addSearchFocus.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="srcpath.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="freeze_E_orbotech.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="../SearchPath.js"></SCRIPT>
	<SCRIPT LANGUAGE="vbscript" SRC="notes_orbotech.js"></SCRIPT>

	<SCRIPT LANGUAGE="javascript">
		document.linkColor="#c7c7ba";
		document.alinkColor="#c7c7ba";
		document.vlinkColor="#c7c7ba";

		//preload images
		image1 = new Image();
		image1.src = "orbotech/showtree.jpg";
		image2 = new Image();
		image2.src = "orbotech/hidetree.jpg";
		
		
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
		
		function refreshFrames(sPkey)	{
			if (top.frames.length==0)	{
				link = sPkey + '.html'
				top.document.location.href=link;
			} 
			else {
				link = sPkey;
				top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);
				top.frames('Index').currentHlighlighPkey=link;
				top.frames('Index').highlightIndex(link);
				link = sPkey.substring(0,sPkey.length-1)  + '_f3.xml';
				//alert(link);
				top.frames('Parts Table').document.location.href=link;
				//alert(0);
				link = sPkey.substring(0,sPkey.length-1) + '_f2.html';
				//alert(link);
				top.frames('Parts Map').document.location.href=link;
				link = sPkey.substring(0,sPkey.length-1)  + '_f1.xml';
				//alert(link);
				top.frames('Header').document.location.href=link;
				//alert(1);
				//alert(2);
				//alert(3);
			}
		}
		
</SCRIPT>

	<div align="center" style="border:2px ;" >
	<table border="0" bgcolor="#6395CA" width="100%" cellspacing="0" cellpadding="0">
			<tr width="100%" height="3" style="background-image:url(orbotech/frmTop.jpg);">
				<td width="1" rowspan="2" style="background-image:url(orbotech/frmLft.jpg);">
				</td>
				<td colspan="18"></td>
				<td width="1" rowspan="2" style="background-image:url(orbotech/frmRgt.jpg);">
				</td>
			</tr>
			<tr align="center">
				<td width="1" style="PADDING-LEFT:8px">
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:freezeIndex();"><b><IMG align="middle" border="0" src="orbotech/hidetree.jpg" id="showhideIndex" alt = "Hide Product Tree"  style="margin:0"/></b></a>
					<script language="JavaScript">
						function refreshImgIndex()	{
							if (window.parent.document.all('inr_frmst').getAttribute('cols').substr(0,2)!="0,")	{
									showhideIndex.src = 'orbotech/hidetree.jpg';
									showhideIndex.alt = 'Hide Product Tree';
							}
							else	{
									showhideIndex.src = 'orbotech/showtree.jpg';
									showhideIndex.alt = 'Show Product Tree';
							}
						}
						function freezeIndex()	{
							//get the current coords
							arrayOfCoords = window.parent.document.all('inr_frmst').getAttribute('cols').split(",");
							 i=0;
							 while (true)	{
							  	//alert(arrayOfCoords[i]);
							  	i=i+1;
							  	if (i==arrayOfCoords.length)	{
							  		break;
							  	}
							  }
							
							if (arrayOfCoords[0]!="0")	{
								arrayOfCoords[0] = "0"
							}
							else	{
								arrayOfCoords[0] = "20%"
							}
							newCoords = arrayOfCoords[0] + "," + arrayOfCoords[1] + "," + arrayOfCoords[2] + ",";
							SetColRatio(newCoords);

							refreshImgIndex();
						}
						refreshImgIndex();
					</script>
				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				<td width="1">
					<SCRIPT LANGUAGE="javascript">
						//Add the correct href acording to the url - local or internet
						CheckSearchURL();
					</SCRIPT>
					<IMG align="middle" src="orbotech/search.jpg" alt = "Search" border="0" style="margin:0"/>
				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				<td width="60" align="right">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				<td  width="1">
					<nobr>
					<a>
						<xsl:choose>
							<xsl:when match=".[Header/HomeUrl $eq$ '']"><xsl:attribute name="target">_top</xsl:attribute></xsl:when>
						</xsl:choose>
						<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when match=".[Header/HomeUrl $eq$ '']"><xsl:value-of select="Header/Index"/></xsl:when>
								<xsl:otherwise>JavaScript:top.frames('Parts Map').refreshFramesWithUrl('<xsl:value-of select="Header/HomeUrl" />');void(0);</xsl:otherwise>
							</xsl:choose>
						</xsl:attribute>
						<IMG align="middle" src="orbotech/home.jpg" alt="Back To Home" border="0" style="margin:0"/>
					</a>
					<a>
					<xsl:attribute name="href">
						<xsl:choose>
							<xsl:when match=".[Header/IsWebEditor='True']">JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-2])=='undefined') {alert('No Back Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos-1; top.frames('Index').LinkSwitch(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:when>
							<xsl:otherwise>JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-2])=='undefined') {alert('No Back Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos-1; top.frames('Parts Map').refreshFramesWithUrl(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:otherwise>
						</xsl:choose>
					</xsl:attribute><img align="middle" alt="Back" border="0" src="orbotech/back.jpg"/></a>
					<a>
					<xsl:attribute name="href">
						<xsl:choose>
							<xsl:when match=".[Header/IsWebEditor='True']">JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos])=='undefined') {alert('No Forward Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos+1; top.frames('Index').LinkSwitch(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:when>
							<xsl:otherwise>JavaScript:if (typeof(top.frames('Index').sPosition[top.frames('Index').iCurrentPos])=='undefined') {alert('No Forward Available.');} else {top.frames('Index').iCurrentPos=top.frames('Index').iCurrentPos+1; top.frames('Parts Map').refreshFramesWithUrl(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].substring(0,top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1].length-1));} ;void(0);</xsl:otherwise>
						</xsl:choose>
					</xsl:attribute><img align="middle" alt="Forward" border="0" src="orbotech/forward.jpg"/>
					</a>
					<a style="cursor:hand;" onclick="javascript:SaveColRatio_3frames();" target="_blank" id="pritim_link_top"><img align="middle" src="orbotech/freeze.jpg" alt="Set default frames" /></a>
					<!--
					<xsl:choose>
					    <xsl:when match=".[Header/Prev $eq$ '']">					    
			     		    </xsl:when>
					    <xsl:otherwise>
						<a><xsl:attribute name="href"><xsl:value-of select="Header/Prev" /></xsl:attribute><img align="middle" src="orbotech/up.jpg" alt="Prev" border="0" style="margin:0"/></a>
					    </xsl:otherwise>
					</xsl:choose>
					
					
					<xsl:choose>
					    <xsl:when match=".[Header/Next $eq$ '']">					    
			     		    </xsl:when>
					    <xsl:otherwise>
					       <a><xsl:attribute name="href"><xsl:value-of select="Header/Next" /></xsl:attribute><img align="middle" src="orbotech/down.jpg" alt="Next" border="0" style="margin:0"/></a>
					    </xsl:otherwise>
					</xsl:choose>
					-->
					<!--<img align="middle" src="orbotech/down.jpg" alt="Item Properties" onclick="Prop();" border="0" style="margin:0"/>
					<img align="middle" src="orbotech/up.jpg" alt="Image Map" onclick="Picture();" border="0" style="margin:0"/>-->
					</nobr>
				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" style="margin:0"/>
				</td>
				<td  width="1" valign="middle">
					<nobr>
					<img align="middle" id="moveTable" style="cursor:hand;margin:0;" src="orbotech/ZoomIn.jpg" alt="Zoom In" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.17)"  border="0" />				
					<img align="middle" id="moveTable" style="cursor:hand;margin:0;" src="orbotech/ZoomOut.jpg" alt="Zoom Out" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)"  border="0" />
					<SPAN style="background-color:#9DD7FF;width:50;height:20;BORDER-RIGHT: 1px solid;BORDER-TOP: 1px solid;BORDER-LEFT: 1px solid;BORDER-BOTTOM: 1px solid;margin-top:4px"  id="percent" align="center" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1,false);fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1,false);"></SPAN>
					</nobr>
				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				<td width="1">
					<nobr>
					<img align="middle" src="orbotech/invtrDis.jpg" border="0" style="margin:0"/>
					<img align="middle" src="orbotech/purchDis.jpg" border="0" style="margin:0"/>
					</nobr>
				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				<td  width="1">
					<nobr>
					<a href="JavaScript:top.frames('Parts Map').window.focus();top.frames('Parts Map').window.print();"><img align="middle" src="orbotech/print.jpg" alt="Print main page" border="0" style="margin:0"/></a>
					<a>
						<xsl:attribute name="href">mailto:?Subject=Item details&amp;Body=%0D%0AItem Number: <xsl:value-of select="//Header/PPROP1"/>%0D%0ADescription: <xsl:value-of select="//Header/PageHebDesc"/></xsl:attribute>
						<img align="middle" src="orbotech/mail.jpg" alt="Send mail" border="0" style="margin:0"/></a>
					</nobr>
				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				<td width="1">
					<a>
					<xsl:attribute name="href">JavaScript:window.open('notes_orbo.htm?FilePath=<xsl:value-of select="Header/pkey" />&amp;desc=<xsl:value-of select="Header/PageHebDesc" />','notes','scrollbars=yes, toolbar, resizable, width=550, height=280');void(0);</xsl:attribute>
					<img align="middle" id="imgnote" src="orbotech/note.jpg" border="0" style="margin:0"/>
					</a>
					<SCRIPT LANGUAGE="vbscript">
						checkpkey("<xsl:value-of select="Header/pkey" />")
					</SCRIPT>

				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				<td>
					<a href="javascript:;" onclick="CheckHelpURL_E();"><img align="middle" border="0" src="orbotech/help.jpg" alt="Help"  style="margin:0"/></a>
				</td>
				<td width="30">
					<img align="middle" src="orbotech/divider.jpg" border="0" style="margin:0"/>
				</td>
				
					<!--
					<a href="javascript:;"><IMG align="middle" src="back.jpg" alt = "Previous Page" onclick="javascript:parent.history.back();"></a>
					<a href="javascript:;"><IMG align="middle" src="forward.jpg" align="absbottom" alt ="Next Page" onclick="javascript:parent.history.forward();"></a>
					
					<xsl:choose>
					    <xsl:when match=".[Header/Prev $eq$ '']">					    
			     		    </xsl:when>
					    <xsl:otherwise>
						<a><xsl:attribute name="href"><xsl:value-of select="Header/Prev" /></xsl:attribute><IMG align="middle" src="next.jpg" alt = "Previous Page" /></a>
					    </xsl:otherwise>
					</xsl:choose>
					
					
					<xsl:choose>
					    <xsl:when match=".[Header/Next $eq$ '']">					    
			     		    </xsl:when>
					    <xsl:otherwise>
					       <a><xsl:attribute name="href"><xsl:value-of select="Header/Next" /></xsl:attribute><IMG align="middle" src="prev.jpg" alt = "Next Page" /></a>
					    </xsl:otherwise>
					</xsl:choose>
					
					</nobr>
					<a target="_blank"><xsl:attribute name="href"><xsl:value-of select="Header/Index" /></xsl:attribute><IMG align="middle" src="index.jpg" alt = "Index" /></a>
				</td>
				<td>
					<nobr>
					<a style="cursor:hand; position:relative ; top : -2.5;" target="_blank" id="mapat_pritim_top"><xsl:attribute name="onclick">javascript:openWin( <xsl:value-of select="Header/WindowSourceFile" />,'MyWin2', 'width=300,height=200,toolbar=0,location=0,directories=0,status=0,menuBar=0,scrollBars=2,resizable=1' )</xsl:attribute><b><IMG align="middle" src="flowting_table.jpg" alt = "Flowting Table" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0,0,100%');"><b><IMG align="middle" src="table.jpg" alt = "Table" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0,100%,0');"><b><IMG align="middle" src="image.jpg" alt ="Image" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('0,60%,40%,');"><b><IMG align="middle" src="image_table.jpg" alt = "Image And Table" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('40%,60%,0');"><b><IMG align="middle" src="image_index.jpg" alt = "Image And Index" /></b></a>
					<a href="javascript:;" style="cursor:hand;" onclick="javascript:SetColRatio('20%,50%,30%');"><b><IMG align="middle" src="image_table_index.jpg" alt ="Image, Table And Index" /></b></a>
					<a style="cursor:hand; position:relative ; top : -2.5;" onclick="javascript:SaveColRatio();" target="_blank" id="pritim_link_top"><b><IMG align="middle" src="freeze.jpg" alt = "Freeze Frames" /></b></a>
					</nobr>
				</td>
				<td>
					<table border="0" id="moveTable">
						<tr>
							<td id="moveTable">&#160;<img align="middle" src="down.jpg" alt="Item Properties" onclick="Prop();"/>&#160;<img align="middle" src="up.jpg" alt="Image Map" onclick="Picture();"/>
							&#160;&#160;</td>
							<td id="moveTable"></td>
							<td align="left" id="moveTable"><div align="center" id="scontentsub">&#160;<img align="middle" src="width.jpg" alt="Fit To Page" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);SetZoomSize('FIT_TO_PG');" />&#160;<img align="middle" src="100.jpg" alt="100%" align="baseline" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" />&#160;<img align="middle" src="freezeImage.jpg" alt="Freeze Image" align="baseline" onclick="javascript:SetZoomSize('');" /></div><SPAN id="freezeImageOk" align="center"></SPAN></td>
						</tr>
					</table>
				</td>-->
				<td>
					<img align="middle" src="orbotech/logo.jpg" style="margin:0"/>
					<SPAN id="freezeFrameOk" align="center"></SPAN>
					<div id="scontentmain">
					<div id="scontentbar" >
					</div>
					</div>
				</td>
			</tr>
			<tr width="100%" height="3" style="background-image:url(orbotech/frmBotm.jpg);">
				<td colspan="20"></td>
			</tr>
		</table>
		</div>

	<div align="center">
	<table border="0" width="100%" cellspacing="0" cellpadding="0">
		<tr bgcolor="#070E97">
			<td width="2%">
				<!--<img align="middle" src="LinkWareIcon.GIF"/>-->
				<!--Logo Should Come Here-->
			</td>
			<td width="96%" align="center">
				<table border="0" cellspacing="0" cellpadding="0" width="100%">
					<tr>
						<td width="33%" align="left">
							<font color="#FFCB74" style="font-family: Arial;font-size:12px"><b><xsl:value-of select="Header/PageHebDesc" /></b></font>
						</td>
						<td width="33%" align="center">
							<font color="#FFCB74" style="font-family: Arial;font-size:12px"><b><xsl:value-of select="Header/CatalogName" /></b></font>
						</td>
						<td width="33%" align="right">
							<font color="#FFCB74" style="font-family: Arial;font-size:12px"><b>Published On: <xsl:value-of select="Header/PublishDate" /></b></font>
						</td>
					</tr>
				</table>
			</td>
			<td width="2%">
				<!--Logo Should Come Here-->
			</td>
		</tr>
	</table>
	</div>
	
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
