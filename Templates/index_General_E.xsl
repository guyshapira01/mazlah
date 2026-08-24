<?xml version="1.0" encoding="ISO-8859-8"?>
 
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

<xsl:param name="WebEditorParam" select="//IsWebEditor" />
<xsl:param name="RootIndexParam" select="//IsRootIndex" />

<xsl:template match="/">

  <SCRIPT LANGUAGE="JavaScript" SRC="tree_E.js"></SCRIPT>
  <SCRIPT LANGUAGE="JavaScript" SRC="Expand_Orbotech.js"></SCRIPT>
  <SCRIPT LANGUAGE="JavaScript" SRC="../SearchPath.js"></SCRIPT>
  <SCRIPT LANGUAGE="JavaScript1.2" SRC="cookutil.js"></SCRIPT>
  <SCRIPT LANGUAGE="javascript" SRC="specialfx_blue_General_E.js"></SCRIPT>
  <LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
  <SCRIPT LANGUAGE="javascript" SRC="ToolBar.js"></SCRIPT>
  <LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css" />
  <input type="hidden" name="temp" value="0"/>
   <input type="hidden" name="catkey" >
   <xsl:attribute name="value"><xsl:value-of select="TreeIndex/CatalogKey" />
   </xsl:attribute>
   </input>
  
<script language="JavaScript">
	 var ExpandDiv =  new Array();
 	 var ExpandDivStyle =  new Array();
 //alert(ExpndDiv.length);
</script>

<table id="tblIndex" width="100%" height="48" align="right" border="0">
<tr>

  <SCRIPT LANGUAGE="JavaScript">
  //Add the correct href acording to the url - local or internet "
//  CheckHomeURL();
//check if we are at Main Index Page
if ((window.name==" ")||(window.name=="")){
	document.write('<td></td><td>');
	CheckHomeURL();	
	document.write('<IMG src="home.jpg" border="0" />')	
	document.write('</td><td></td>')
CloseURL();
 }

  </SCRIPT>


  <SCRIPT LANGUAGE="JavaScript">
 if ((window.name==" ")||(window.name=="")){
 document.write('<td nowrap="yes"><div  class="mainCatalog" align="center"><xsl:value-of select="TreeIndex/CatalogName" />&#160;&#160;&#160;&#160;<font size="3">Revision: <xsl:value-of select="TreeIndex/CatalogRevision" /></font></div></td>');
	
 }
  </SCRIPT>


<!-- <td width="50"><a href="JavaScript:void(0);"><img border="0" src="namaicon.gif" onClick="Javascript:window.open ('Credits.html', 'Credits', 'width=700,height=500,top=40,left=40,resizeable=0,menubar=no,alwaysRaised,dependent');" /></a></td> -->
</tr></table>

 <SCRIPT LANGUAGE="JavaScript">
 if ((window.name==" ")||(window.name=="")){
	document.write('<div class="underLine">');
	document.write('<br/><br/><br/>');
	document.write('</div>');
 }
 
 </SCRIPT>
	<script language ="javascript">insertTable()</script>
	<script language ="javascript">insertTR ()</script>
	<script language ="javascript">insertTD()</script>
 
<script language="JavaScript">
//alert(parent.document.location.href);
	var siteLocation;
	var catalogNumber;
	var LinesPerCol;
	
	LinesPerCol = 100;
	siteLocation = document.location.href;
	siteLocation = siteLocation.toUpperCase()
	siteLocation = siteLocation.split("CATALOGS/");    //Put here the Directory site of the catalog.
	//For Web Editor (temporary) siteLocation = siteLocation[1].split("/");
	//For Web Editor (temporary) catalogNumber = siteLocation[0];
	//alert(catalogNumber);
	var preloadFlag = false;
	
</script>


<xsl:choose>
	<xsl:when test="$RootIndexParam='True'">
<HTML>
<HEAD>
  
   <TITLE><xsl:value-of select="TreeIndex/CatalogName" /></TITLE>
  <SCRIPT LANGUAGE="JavaScript" SRC="WebEditor.js"></SCRIPT>
   
</HEAD>
<BODY leftmargin="0" topmargin="0" onunload="">
<xsl:attribute name="onLoad">JavaScript:preloadImages();proloadIcons();SessionState('FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="TreeIndex/CatalogKey" />','<xsl:value-of select="TreeIndex/OpenLevel" />');</xsl:attribute>
<div>&#160;</div>
<table   border="0"  cellspacing="0"  class="ToolBar" width="100%">
	<tr  valign="top" >  
		<td align="left" width="0%"><input type="hidden" id="Expand" name="IndexStatus" value="0" ></input></td>
		<td align="left" width="40%">
			<table cellspacing="2" cellpadding="2"  height="28" ID="btnTable" class="ToolBarGlow" width="100%" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<tr>
					<td class="ToolBarFont" >
						<img align="absmiddle" src="WEImages/collapse.gif" alt="Collapse All" id="CollapseLable">
							<xsl:attribute name="onclick"> CollapseLable.src='WEImages/collapse.gif';ExpandLable.alt='Expand All';ExpandLable.src='WEImages/Expand.jpg';ExpandAll('0');addToCookie('2',catalogNumber+'ExpandAll');deleteAllValuesFromCookies();ExpandAll('2');CollapseLable.src='WEImages/collapse.gif';
							</xsl:attribute>
							<xsl:attribute name="onMouseOver">this.style.cursor='hand';
							</xsl:attribute>
							<xsl:attribute name="onMouseOut">this.style.cursor='normal';
							</xsl:attribute>
						</img>&#160;Collapse All
					</td>
				</tr>
			</table>	
		</td>
		
		<td align="left" width="40%">
		<table cellspacing="2" cellpadding="2" height="28" ID="btnTable" class="ToolBarGlow" width="100%" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<tr>
					<td class="ToolBarFont" >
						<img src="WEImages/Expand.jpg" align="absmiddle" alt="Expand All" id="ExpandLable">
							<xsl:attribute name="onclick">javascript:expandALL(0); if (ExpandStatus==2)	{	ExpandLable.src='WEImages/Expand.jpg'; ExpandLable.alt='Last State';ExpandAll('1');} else	{ExpandLable.alt='Expand All';ExpandLable.src='WEImages/collapse.gif';ExpandAll('2');}</xsl:attribute>
							<xsl:attribute name="onMouseOver">this.style.cursor='hand';
							</xsl:attribute>
							<xsl:attribute name="onMouseOut">this.style.cursor='normal';
							</xsl:attribute>
						</img><a style="color:White">&#160;Expand All</a>
					</td>
				</tr>
			</table>
		</td> 
		
		<td align="left" width="20%">
		<table cellspacing="2" cellpadding="2"  ID="btnTable" class="ToolBarGlow" width="100%" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
				<tr>
					<td nowrap="yes" class="ToolBarFont" align="absmiddle" onClick="JavaScript:AddObj('New_Item_General_E.xsl');void(0);">
								<img src="WEImages/newdoc.gif" alt="New" border="0" width="16" height="16"/>
								<a align="absmiddle" style="color:White" target="_self" title="New">
									
									New
						</a>
					</td>
				</tr>
			</table>
		</td> 

	 </tr>
	 </table>

<!--<script language="vbscript" >
alert QueryString("url")
</script>-->
<script LANGUAGE="JavaScript">

function newImage(arg) {
	if (document.images) {
		rslt = new Image();
		rslt.src = arg;
		return rslt;
	}
}

function changeImages() {
 if (document.images)
 	{
 		if (preloadFlag == true)
 			{
   				// Must Be Open And Close Images.
   				// The Files will be xxxxOpen.jpg and xxxxClose.jpg      xxxxx = Object Type
   				
   				if ('noneID'==changeImages.arguments[0])	{
   					return 0;
   				}
   				else	{
					NewPictureName = document[changeImages.arguments[0]].src;
					if (NewPictureName.indexOf("Close") == -1)
						{
							NewPictureName = NewPictureName.replace("Open","Close");
							//alert(NewPictureName);
						}
					else
						{
							NewPictureName = NewPictureName.replace("Close","Open");
							//alert(NewPictureName);
						}
					document[changeImages.arguments[0]].src = NewPictureName;
				}
   			}
 	}
}


var preloadFlag = false;
function proloadIcons()	{
		//alert("run");
		chapterOpen = newImage("FolderOpen_E.gif");
		chapterClose = newImage("FolderClose_E.gif");
		//chapter_Without_Sun = newImage("Folder.jpg"); - not available.

		Page_Collapse = newImage("page_pic_plus_E.gif");
		Page_Expand = newImage("page_pic_minus_E.gif");
		Page_Without_Sun = newImage("page_pic.gif");

		itemOpen = newImage("IconTypes\Open.gif");
	        itemClose = newImage("IconTypes\Close.gif");
	        Item_Without_Sun = newImage("IconTypes\item.gif");

		preloadFlag = true;

}
function preloadImages() {
	if (document.images) {
		proloadIcons();
	}
	getFromCookieDiv();

}	
</script>
	</BODY>
	</HTML>
		
	</xsl:when>
	<xsl:otherwise></xsl:otherwise>
</xsl:choose>

  <div align="left">
 
  <xsl:choose>
	<xsl:when test="$RootIndexParam='True'">

<table border="0"  cellspacing="0" >
<tr  valign="top" > 
<!-- 
	<td ><input type="hidden" id="Expand" name="IndexStatus" value="0" ></input></td>
		<td>
			<img src="Orbotech/colpsEnb.jpg" alt="Collapse All" id="CollapseLable">
				<xsl:attribute name="onclick"> CollapseLable.src='Collaps.jpg';ExpandLable.alt='Expand All';ExpandLable.src='Orbotech/ExpndEnb.jpg';ExpandAll('0');addToCookie('2',catalogNumber+'ExpandAll');deleteAllValuesFromCookies();ExpandAll('2');CollapseLable.src='Orbotech/colpsEnb.jpg';
				</xsl:attribute>
				<xsl:attribute name="onMouseOver">this.style.cursor='hand';
				</xsl:attribute>
				<xsl:attribute name="onMouseOut">this.style.cursor='normal';
				</xsl:attribute>
			</img></td>
	<td>&#160;&#160;</td>
	-->
		<td><!--<input type="radio" id="Expand" name="IndexStatus" value="1" >--><!--</input>--></td>
		<!--
		<td>
			<img src="Orbotech/ExpndEnb.jpg" alt="Expand All" id="ExpandLable">
				<xsl:attribute name="onclick"> if (ExpandStatus==2)	{	ExpandLable.src='Orbotech/LastEnb.jpg'; ExpandLable.alt='Last State';ExpandAll('1');} else	{ExpandLable.alt='Expand All';ExpandLable.src='Orbotech/ExpndEnb.jpg';ExpandAll('2');}</xsl:attribute>
				<xsl:attribute name="onMouseOver">this.style.cursor='hand';
				</xsl:attribute>
				<xsl:attribute name="onMouseOut">this.style.cursor='normal';
				</xsl:attribute>
			</img>
		</td>
	<td>&#160;&#160;</td>
	-->
		<td><!--<input type="radio" id="Remember" name="IndexStatus" value="2" >--><!--</input>--></td>
	<!--	<td >
			<img src="Load.jpg" alt="remember State" id="rememberLable">
				<xsl:attribute name="onclick">rememberLable.src='Remember.jpg';ExpandLable.src='Expand_Unpressed.jpg'; ExpandAll('2');rememberLable.alt='Remember State';</xsl:attribute>				
				<xsl:attribute name="onMouseOver">this.style.cursor='hand';
				</xsl:attribute>
				<xsl:attribute name="onMouseOut">this.style.cursor='normal';
				</xsl:attribute>
			</img>
			</td>  -->
	<td></td>
 </tr> 
 </table> 
 </xsl:when>
	<xsl:otherwise></xsl:otherwise>
</xsl:choose>


	<xsl:apply-templates select="TreeIndex/Object" />
<xsl:choose>
	<xsl:when test="$RootIndexParam='True'">	
	<br/><br/>
	<br/><br/>
	</xsl:when>
</xsl:choose>
	</div>
	<xsl:choose>
	<xsl:when test="$RootIndexParam='True'">
	<div id="NoneID"></div> <!-- This is id for uncollapse node. its for noneID in tree.js will not return error. -->
	</xsl:when>
	<xsl:otherwise></xsl:otherwise>
</xsl:choose>
 
	<xsl:choose>
		<xsl:when test="$WebEditorParam='True'">
			<script type="text/javascript">
			
			 	var currentHlighlighPkey;
				var iCurrentPos = 0;
				var sPosition = new Array();
				sPosition[0]="123";
				
				function highlightCurrentDiv()	{
					var HtmlUrl=parent.document.location.href;
					var url_array = HtmlUrl.split('Pkey=');
					//alert(s++);
					//alert('xx' + url_array[1]);
					//alert( url_array[url_array.length-1]);
					url_array = url_array[url_array.length-1].split('Type');
					url_array = url_array[0].substring(0,url_array[0].length-1);

					//alert( url_array[0]);
					//if ( url_array.length!=1){
					//alert(url_array);
					highlight(url_array+'a');
					currentHlighlighPkey=url_array+'a';
					//	//alert(url_array[1]);
					//
					//	}
				}
				//highlightCurrentDiv();
			
			</script>
		</xsl:when>
		<xsl:otherwise>
			<script type="text/javascript">
			 	var currentHlighlighPkey;
				var iCurrentPos = 0;
				var sPosition = new Array();
				sPosition[0]="123";
				
				function highlightCurrentDiv()	{
					var HtmlUrl=parent.document.location.href;
					var url_array = HtmlUrl.split('/');
					//alert(s++);
					//alert('xx' + url_array[1]);
					//alert( url_array[url_array.length-1]);
					url_array = url_array[url_array.length-1].split('.');


					//alert( url_array[0]);
					//if ( url_array.length!=1){
					highlight(url_array[0]+'a');
					currentHlighlighPkey=url_array[0]+'a';
					//	//alert(url_array[1]);
					//
					//	}
				}
				highlightCurrentDiv();
			</script>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
<xsl:template match="Object">
<xsl:param name="IDParam" select="ID" />
<xsl:param name="NameParam" select="Name" />
<xsl:param name="ObjectClassParam" select="ObjectClass" />

	<!-- Level 1 Only ==============================================================-->
	<xsl:choose>
		<!-- Catalog Name - For WebEditor -->
		<xsl:when test="Level = 0">
					<xsl:apply-templates select="Object" />
		</xsl:when>
		<!-- End Catalog Name - For WebEditor -->
		<xsl:when test="Level = 1">
			<script language="javascript">
				temp.value = (temp.value*1) + 1
				if ( ((temp.value*1)/LinesPerCol) == Math.round((temp.value*1)/LinesPerCol) ) {
				
					insertTD();				
				}
			</script>
			<xsl:choose>
				<xsl:when test="Object/Level &gt; 1">
<div>
<xsl:attribute name="style">position:relative; cursor:hand;left:<xsl:value-of select="number(Level)*20"/></xsl:attribute>
					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();"><xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');</xsl:attribute></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif" onmouseover="enter();" onmouseout="leave();"><xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');</xsl:attribute></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
						<!--First child always open-->
						<img src="IconTypes/Close.gif" align="absMiddle">
						<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>
						<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');</xsl:attribute></img>
						<img width = "16" height = "16" align="absMiddle">
							<xsl:choose>
								<xsl:when test="ItemProp = 'Y'">
									<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
									<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
									<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</img>
						</xsl:when>
					</xsl:choose><!-- &nbsp; = &#160; -->
					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level1"><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');</xsl:attribute><font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font></span>
			     			</xsl:when>
			     			<!-- For Items add additional property -->
			                        <xsl:when test="$ObjectClassParam = 'Item'">			                        			                        
							<span class="level1"><a >
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute>
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         <font style="font-family:Arial; font-size:10px;color:#000000">&#160;<xsl:value-of select="$NameParam" />&#160;</font>
							     </xsl:when>	
							     <xsl:otherwise>
							     
						                <font style="font-family:Arial; font-size:10px;color:#000000">&#160;&#160;&#160;&#160;<xsl:value-of select="$NameParam" /></font>
							      
							  </xsl:otherwise>
							</xsl:choose> 	
							</a></span>
					     	</xsl:when>
					     	<xsl:otherwise>
							<span class="level1"><a >
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute>
							
							<font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font></a></span>
			     			</xsl:otherwise>
                        		</xsl:choose>
	     				</div>
					<DIV style="display:none;"><xsl:attribute name="id">inner_<xsl:value-of select="$IDParam" /></xsl:attribute>
					<xsl:apply-templates select="Object" /></DIV>
					
				</xsl:when>
				<xsl:otherwise>
<div><xsl:attribute name="style">position:relative; cursor:hand;left:<xsl:value-of select="number(Level)*20"/></xsl:attribute>
					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();"></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif"  onmouseover="enter();" onmouseout="leave();"></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
						<img width = "16" height = "16" align="absMiddle">
							<xsl:choose>
								<xsl:when test="ItemProp = 'Y'">
									<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
									<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
									<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</img></xsl:when>
					</xsl:choose>&#160;<!-- &nbsp; = &#160; -->

					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level1"><font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font></span>
			     			</xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
							<span class="level1"><a >
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute>
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         <font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font>
							     </xsl:when>	
							     <xsl:otherwise>
							   	  <!-- Add additional property to the name  --> 
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         <font style="font-family:Arial; font-size:10px;color:#000000">&#160;<xsl:value-of select="$NameParam" />&#160;</font>
							     </xsl:when>	
							     <xsl:otherwise>
						                <font style="font-family:Arial; font-size:10px;color:#000000">&#160;<xsl:value-of select="$NameParam" /></font>
							     </xsl:otherwise>
							    </xsl:choose> 	
							     </xsl:otherwise>
							    </xsl:choose> 	
							</a></span>
			     			</xsl:when>
					     	<xsl:otherwise>
							<span class="level1"><a >
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute><font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font>
							</a></span>
						</xsl:otherwise>
                        		</xsl:choose>
	     				</div>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:when>
		<xsl:otherwise>	<!-- Level With Childs============================================================================================== -->
			<xsl:choose>
				<xsl:when test="Object/Level &gt; Level">
<div><xsl:attribute name="style">position:relative; cursor:hand;left:<xsl:value-of select="number(Level)*20"/></xsl:attribute>
				<xsl:choose>
				<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();">
				<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>
				<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');						</xsl:attribute></img></xsl:when>
				<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif" onmouseover="enter();" onmouseout="leave();">
				<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>		
				<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');</xsl:attribute></img></xsl:when>
				<xsl:when test="$ObjectClassParam = 'Item'">
				<img src="IconTypes/Close.gif" align="absMiddle">
				<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>
				<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');</xsl:attribute></img>
<img width = "16" height = "16" align="absMiddle">
<xsl:choose>
	<xsl:when test="ItemProp = 'Y'">
		<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
		<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
	</xsl:when>
	<xsl:otherwise>
		<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
		<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
	</xsl:otherwise>
</xsl:choose>
</img>
				</xsl:when>
				</xsl:choose>
<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level2"><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="number(Level)"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','','<xsl:value-of select="ParentKey"/>');</xsl:attribute><font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font></span>
						</xsl:when>
						<xsl:when test="$ObjectClassParam= 'Item'">
							<span class="level2"><a  ><xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute>
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         <font style="font-family:Arial; font-size:10px;color:#000000">&#160;<xsl:value-of select="$NameParam" /></font>
							     </xsl:when>	
							     <xsl:otherwise>
						                <font style="font-family:Arial; font-size:10px;color:#000000">&#160;<xsl:value-of select="$NameParam" /></font>
							     </xsl:otherwise>
							    </xsl:choose>
							    </a></span>
					     	</xsl:when>
					     	<xsl:otherwise>
					               <span class="level3"><a >
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute><font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font>
							</a></span>
			     			</xsl:otherwise>
		                        </xsl:choose>
				<!--//SPACES HERE-->
</div><DIV  style="display:none;"><xsl:attribute name="id">inner_<xsl:value-of select="$IDParam" /></xsl:attribute>
					<xsl:apply-templates select="Object" /></DIV>
				</xsl:when>
				<xsl:otherwise>
	<!--=============================================Level that the item have no child==========================================--> 
<div><xsl:attribute name="style">position:relative; cursor:hand;left:<xsl:value-of select="number(Level)*20"/></xsl:attribute>
<!--//SPACES HERE-->
<img src="spacer.gif" width="12" height="11"/>					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();">
						</img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif"  onmouseover="enter();" onmouseout="leave();">
						</img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
						<img width = "16" height = "16" align="absMiddle">
							<xsl:choose>
								<xsl:when test="ItemProp = 'Y'">
									<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
									<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
								</xsl:when>
								<xsl:otherwise>
									<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
									<xsl:attribute name="alt"><xsl:value-of select="Type"/></xsl:attribute>
								</xsl:otherwise>
							</xsl:choose>
						</img></xsl:when>
					</xsl:choose>					
					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level3"><font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font></span>
			     			</xsl:when>
			     			<xsl:when test="$ObjectClassParam = 'Item'">
							<span class="level3"><a >
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<!--<xsl:attribute name="onmouseover">highlight('<xsl:value-of select="$IDParam" />');</xsl:attribute>-->
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute>
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         <font style="font-family:Arial; font-size:10px;color:#000000">&#160;<xsl:value-of select="$NameParam" /></font>
							     </xsl:when>	
							     <xsl:otherwise>
						                <font style="font-family:Arial; font-size:10px;color:#000000">&#160;<xsl:value-of select="$NameParam" /></font>
							     </xsl:otherwise>
							    </xsl:choose> 								</a></span>
			     			</xsl:when>
					     	<xsl:otherwise>		
					               <span class="level3"><a >
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">JavaScript:GetHREF('<xsl:value-of select="$IDParam" />','<xsl:value-of select="Template"/>','<xsl:value-of select="//TreeIndex/CatalogKey"/>','<xsl:value-of select="ParentKey"/>');</xsl:when>
								<xsl:otherwise>JavaScript:if (top.frames.length==0)	{top.document.location.href='<xsl:value-of select="$IDParam" />.html'} else {top.frames('Parts Map').document.location.href='<xsl:value-of select="$IDParam" />_f2.html';top.frames('Header').document.location.href='<xsl:value-of select="$IDParam" />_f1.xml';top.frames('Parts Table').document.location.href='<xsl:value-of select="$IDParam" />_f3.xml';top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey);top.frames('Index').currentHlighlighPkey='<xsl:value-of select="$IDParam" />'+'a';top.frames('Index').highlight('<xsl:value-of select="$IDParam" />'+'a');}void(0);</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute><font style="font-family:Arial; font-size:10px;color:#000000"><xsl:value-of select="$NameParam" /></font>
							</a></span>
				     		</xsl:otherwise>
                        		</xsl:choose></div>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
	</xsl:choose>
</xsl:template>
</xsl:stylesheet>