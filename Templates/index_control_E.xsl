<!--     ******  Not in use ???????   -->
<?xml version="1.0" encoding="ISO-8859-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

<xsl:param name="WebEditorParam" select="//IsWebEditor" />

<xsl:template match="/">


<HTML>
<HEAD>
  <SCRIPT LANGUAGE="JavaScript" SRC="tree_E.js"></SCRIPT>
   <SCRIPT LANGUAGE="JavaScript" SRC="Expand.js"></SCRIPT>
  <SCRIPT LANGUAGE="JavaScript" SRC="../SearchPath.js"></SCRIPT>
  <SCRIPT LANGUAGE="JavaScript1.2" SRC="cookutil.js"></SCRIPT>
  <SCRIPT LANGUAGE="javascript" SRC="specialfx_e.js"></SCRIPT>
  <LINK REL="stylesheet" HREF="LinkWare_control.css" TYPE="text/css" />
  <TITLE><xsl:value-of select="TreeIndex/CatalogName" /></TITLE>
</HEAD>
<!--     <BODY class="BodyIndex" onload="JavaScript:ExpandPreload();" onunload="JavaScript:addToCookieDiv()">
-->
<BODY class="BodyIndex" onunload="">
<xsl:attribute name="onLoad">JavaScript:preloadImages();proloadIcons();SessionState('FolderClose_E.gif','FolderOpen_E.gif','<xsl:value-of select="TreeIndex/CatalogKey" />','<xsl:value-of select="TreeIndex/OpenLevel" />');</xsl:attribute>

<input type="hidden" name="temp" value="0"/>
<script language="JavaScript">
	 var ExpandDiv =  new Array();
 	 var ExpandDivStyle =  new Array();
 //alert(ExpndDiv.length);
</script>

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
</script>

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
	
	getFromCookieDiv()

}	
</script>

  <div align="right">
<table id="tblIndex" width="100%" height="48"   align="right" border="0">
<tr>

  <SCRIPT LANGUAGE="JavaScript">
  //Add the correct href acording to the url - local or internet "
  CheckHomeURL();
//check if we are at Main Index Page
 if ((window.name==" ")||(window.name=="")){
	document.write('<td></td><td><IMG src="home.jpg" border="0" /></td><td></td>')
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
	<script language ="javascript">insertTR ()</script><script language ="javascript">insertTD()</script>

	<table  border="0"  cellspacing="0" >
	<tr  valign="top" >  
		<td ><input type="hidden" id="Expand" name="IndexStatus" value="0" ></input></td>
		<td>
			<img src="Collaps_Unpressed.jpg" alt="Collapse All" id="CollapseLable">
				<xsl:attribute name="onclick"> CollapseLable.src='Collaps.jpg';ExpandLable.alt='Expand All';ExpandLable.src='Expand.jpg';ExpandAll('0');addToCookie('2',catalogNumber+'ExpandAll');deleteAllValuesFromCookies();ExpandAll('2');CollapseLable.src='Collaps_Unpressed.jpg';
				</xsl:attribute>
				<xsl:attribute name="onMouseOver">this.style.cursor='hand';
				</xsl:attribute>
				<xsl:attribute name="onMouseOut">this.style.cursor='normal';
				</xsl:attribute>
			</img></td>
		<td>&#160;&#160;</td>

		<td><input type="radio" id="Expand" name="IndexStatus" value="1" ></input></td>
		<td>
			<img src="Expand.jpg" alt="Expand All" id="ExpandLable">
				<xsl:attribute name="onclick">JavaScript:{{document.body.style.cursor='wait';w=window.open('wait.html', 'wait','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');CollapseMost();document.body.style.cursor='auto';w.close();}}
				</xsl:attribute>
				<xsl:attribute name="onMouseOver">this.style.cursor='hand';
				</xsl:attribute>
				<xsl:attribute name="onMouseOut">this.style.cursor='normal';
				</xsl:attribute>
			</img>
		</td> 
		<td>&#160;&#160;</td>

		<td><!--<input type="radio" id="Remember" name="IndexStatus" value="2" >--><!--</input>--></td>
		<td >
			<img src="Load.jpg" alt="remember State" id="rememberLable">
				<xsl:attribute name="onclick">JavaScript:{if (confirm('This operation might take some time. Do you wish to continue?')) {document.body.style.cursor='wait';w=window.open('wait.html', 'wait','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');ExpandMost('MAX');document.body.style.cursor='auto';w.close();}}</xsl:attribute>
				<xsl:attribute name="onMouseOver">this.style.cursor='hand';
				</xsl:attribute>
				<xsl:attribute name="onMouseOut">this.style.cursor='normal';
				</xsl:attribute>
			</img>
			</td>
		<td></td>
	 </tr>
	 </table>
	<div class="underLine"></div>

	<xsl:apply-templates select="TreeIndex/Object" />
	</div>
	<div class="underLine"></div>
	<div id="NoneID"></div> <!-- This is id for uncollapse node. its for noneID in tree.js will not return error. -->


	<xsl:choose>
		<xsl:when test="$WebEditorParam='True'">
			<script type="text/javascript">
			<![CDATA[
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
				//	//alert(url_array[1]);
				//
				//	}
			]]>
			</script>
		</xsl:when>
		<xsl:otherwise>
			<script type="text/javascript">
				var HtmlUrl=parent.document.location.href;
				var url_array = HtmlUrl.split('/');
				//alert(s++);
				//alert('xx' + url_array[1]);
				//alert( url_array[url_array.length-1]);
				url_array = url_array[url_array.length-1].split('.');


				//alert( url_array[0]);
				//if ( url_array.length!=1){
				highlight(url_array[0]+'a');
				//	//alert(url_array[1]);
				//
				//	}
					
			</script>
		</xsl:otherwise>
	</xsl:choose>
	
	</BODY>
	</HTML>
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
				<xsl:when test="Object/Level > 1">
					<div  style="  right=20; cursor:hand;" >
					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();"><xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif');</xsl:attribute></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif" onmouseover="enter();" onmouseout="leave();"><xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />');</xsl:attribute></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
						<img src="IconTypes/Close.gif" align="absMiddle">
						<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>
						<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','IconTypes/Close.gif','IconTypes/Open.gif');</xsl:attribute>
						</img>
						<img width = "16" height = "24" align="absMiddle">
						<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
						</img>
						
						</xsl:when>
					</xsl:choose><!-- &nbsp; = &#160; -->

					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level1" ><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif');</xsl:attribute><xsl:value-of select="$NameParam" /></span>
			     			</xsl:when>
			     			<!-- For Items add additional property -->
			                        <xsl:when test="$ObjectClassParam = 'Item'">			                        			                        
							<span class="level1" ><a target="_top">
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
														<xsl:attribute name="href">
														<xsl:choose>
															<xsl:when test="$WebEditorParam='True'">ShowTemplate.aspx?Template=<xsl:value-of select="Template"/>&amp;Pkey=<xsl:value-of select="$IDParam" />&amp;Type=10</xsl:when>
															<xsl:otherwise><xsl:value-of select="$IDParam" />.html</xsl:otherwise>
														</xsl:choose>
							</xsl:attribute>
							
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         &#160;<xsl:value-of select="$NameParam" />&#160;
							     </xsl:when>	
							     <xsl:otherwise>
							     
								<xsl:choose>
 								    <!-- CS -->
								    <xsl:when test="ItemProp = 'CS'">
								         <font size = "1" color="Red" > &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- MI -->
								    <xsl:when test="ItemProp = 'MI'">
								         <font size = "1" color="#FF00FF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
 								    <!-- NMI -->
								    <xsl:when test="ItemProp = 'NMI'">
								         <font size = "1" color="#FFC300"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- NDI -->
								    <xsl:when test="ItemProp = 'NDI'">
								         <font size = "1" color="#00FF00"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
								    <!-- NR -->
								    <xsl:when test="ItemProp = 'NR'">
								         <font size = "1" color="#00FFFF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
									    
								    <xsl:otherwise>
							                <font size = "1">&#160;<xsl:value-of select="ItemProp" /></font>&#160;&#160;&#160;<xsl:value-of select="$NameParam" />
							            </xsl:otherwise>
							    
							     </xsl:choose>  							
							      
							  </xsl:otherwise>
							</xsl:choose> 	
							     	
							</a></span>
					     	</xsl:when>
					     	<xsl:otherwise>
							<span class="level1" ><a target="_top">
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
														<xsl:attribute name="href">
														<xsl:choose>
															<xsl:when test="$WebEditorParam='True'">ShowTemplate.aspx?Template=<xsl:value-of select="Template"/>&amp;Pkey=<xsl:value-of select="$IDParam" />&amp;Type=10</xsl:when>
															<xsl:otherwise><xsl:value-of select="$IDParam" />.html</xsl:otherwise>
														</xsl:choose>
							</xsl:attribute>
							
							<xsl:value-of select="$NameParam" /></a></span>
			     			</xsl:otherwise>
		
                        		</xsl:choose>
	     				</div>
					<DIV  style="display:none;"><xsl:attribute name="id">inner_<xsl:value-of select="$IDParam" /></xsl:attribute>
					<xsl:apply-templates select="Object" />
					</DIV>
				</xsl:when>
				<xsl:otherwise>
					<div  style="  right=20;">
					&#160;&#160;
					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();"></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif"  onmouseover="enter();" onmouseout="leave();"></img></xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
						<img width = "16" height = "24" align="absMiddle"><xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute></img></xsl:when>
					</xsl:choose>&#160;<!-- &nbsp; = &#160; -->

					<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level1" ><xsl:value-of select="$NameParam" /></span>
			     			</xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
							<span class="level1" ><font style="FONT-SIZE:5px">&#160;</font><a target="_top">
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
														<xsl:attribute name="href">
														<xsl:choose>
															<xsl:when test="$WebEditorParam='True'">ShowTemplate.aspx?Template=<xsl:value-of select="Template"/>&amp;Pkey=<xsl:value-of select="$IDParam" />&amp;Type=10</xsl:when>
															<xsl:otherwise><xsl:value-of select="$IDParam" />.html</xsl:otherwise>
														</xsl:choose>
							</xsl:attribute>
							
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         <xsl:value-of select="$NameParam" />
							     </xsl:when>	
							     <xsl:otherwise>
							   	  <!-- Add additional property to the name  --> 
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         &#160;<xsl:value-of select="$NameParam" />&#160;
							     </xsl:when>	
							     <xsl:otherwise>
							     
								<xsl:choose>
 								    <!-- CS -->
								    <xsl:when test="ItemProp = 'CS'">
								         <font size = "1" color="Red" > &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- MI -->
								    <xsl:when test="ItemProp = 'MI'">
								         <font size = "1" color="#FF00FF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
 								    <!-- NMI -->
								    <xsl:when test="ItemProp = 'NMI'">
								         <font size = "1" color="#FFC300"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- NDI -->
								    <xsl:when test="ItemProp = 'NDI'">
								         <font size = "1" color="#00FF00"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
								    <!-- NR -->
								    <xsl:when test="ItemProp = 'NR'">
								         <font size = "1" color="#00FFFF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
									    
								    <xsl:otherwise>
							                <font size = "1"><xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
							            </xsl:otherwise>
							    
							</xsl:choose>  							
							     
							     </xsl:otherwise>
							    </xsl:choose> 	
							     
							     </xsl:otherwise>
							    </xsl:choose> 	
							    
							</a></span>
			     			</xsl:when>
					     	<xsl:otherwise>
							<span class="level1" ><a target="_top">
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href"><xsl:value-of select="$IDParam" />.html</xsl:attribute>
							<xsl:value-of select="$NameParam" />
							</a></span>
						</xsl:otherwise>
                        		</xsl:choose>
	     				</div>
				</xsl:otherwise>
			</xsl:choose>
			
		</xsl:when>
		<xsl:otherwise>	<!-- Level With Childs============================================================================================== -->
			<xsl:choose>
				<xsl:when test="Object/Level > Level">
<div><xsl:attribute name="style">position:relative; cursor:hand;left:<xsl:value-of select="number(Level)*20"/></xsl:attribute>										

				<xsl:choose>
				<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();">
				<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>
				<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif');						</xsl:attribute></img></xsl:when>
				<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif" onmouseover="enter();" onmouseout="leave();">
				<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>		
				<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />');</xsl:attribute></img></xsl:when>
				<xsl:when test="$ObjectClassParam = 'Item'">
				<img src="IconTypes/Close.gif" align="absMiddle">
				<xsl:attribute name="name"><xsl:value-of select="$IDParam" /></xsl:attribute>
				<xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','IconTypes/Close.gif','IconTypes/Open.gif');</xsl:attribute>
				</img>
				<img width = "16" height = "24" align="absMiddle">
				<xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute>
				</img>

				</xsl:when>
				</xsl:choose>
<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level2" ><xsl:attribute name="onclick">Switch('inner_<xsl:value-of select="$IDParam" />','<xsl:value-of select="$IDParam" />','FolderClose_E.gif','FolderOpen_E.gif');</xsl:attribute><xsl:value-of select="$NameParam" /></span>
						</xsl:when>
						<xsl:when test="$ObjectClassParam = 'Item'">
							<span class="level2" ><a target="_top" ><xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
														<xsl:attribute name="href">
														<xsl:choose>
															<xsl:when test="$WebEditorParam='True'">ShowTemplate.aspx?Template=<xsl:value-of select="Template"/>&amp;Pkey=<xsl:value-of select="$IDParam" />&amp;Type=10</xsl:when>
															<xsl:otherwise><xsl:value-of select="$IDParam" />.html</xsl:otherwise>
														</xsl:choose>
							</xsl:attribute>
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         &#160;<xsl:value-of select="$NameParam" />
							     </xsl:when>	
							     <xsl:otherwise>
								<xsl:choose>
 								    <!-- CS -->
								    <xsl:when test="ItemProp = 'CS'">
								         <font size = "1" color="Red" > &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- MI -->
								    <xsl:when test="ItemProp = 'MI'">
								         <font size = "1" color="#FF00FF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
 								    <!-- NMI -->
								    <xsl:when test="ItemProp = 'NMI'">
								         <font size = "1" color="#FFC300"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- NDI -->
								    <xsl:when test="ItemProp = 'NDI'">
								         <font size = "1" color="#00FF00"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
								    <!-- NR -->
								    <xsl:when test="ItemProp = 'NR'">
								         <font size = "1" color="#00FFFF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
									    
								    <xsl:otherwise>
							                <font size = "1"><xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
							            </xsl:otherwise>
							    
							</xsl:choose>  							
							     </xsl:otherwise>
							    </xsl:choose> 								</a></span>
					     	</xsl:when>
					     	<xsl:otherwise>
					               <span class="level3" ><a target="_top">
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href"><xsl:value-of select="$IDParam" />.html
							</xsl:attribute><xsl:value-of select="$NameParam" />
							</a></span>
			     			</xsl:otherwise>
		                        </xsl:choose>
				
<!--//SPACES HERE-->
</div>						
						
					<DIV  style="display:none;"><xsl:attribute name="id">inner_<xsl:value-of select="$IDParam" /></xsl:attribute>
					<xsl:apply-templates select="Object" />
					</DIV>
	
				</xsl:when>
				
				<xsl:otherwise>
	<!--=============================================Level that the item have no child==========================================--> 

<div><xsl:attribute name="style">position:relative; cursor:hand;left:<xsl:value-of select="number(Level)*20"/></xsl:attribute>				
<!--//SPACES HERE-->
&#160;&#160;


						<xsl:choose>
							<xsl:when test="$ObjectClassParam = 'Chapter'"><img align="absMiddle" src="FolderClose_E.gif" onmouseover="enter();" onmouseout="leave();">
							</img></xsl:when>
							<xsl:when test="$ObjectClassParam = 'Page'"><img align="absMiddle" src="page_pic.gif"  onmouseover="enter();" onmouseout="leave();">
							</img></xsl:when>
							<xsl:when test="$ObjectClassParam = 'Item'">
							<img width = "16" height = "24" align="absMiddle"><xsl:attribute name="src">IconTypes/<xsl:value-of select="Type"/>.gif</xsl:attribute></img></xsl:when>
						</xsl:choose>					
						
						<xsl:choose>
						<xsl:when test="$ObjectClassParam = 'Chapter'">
							<span class="level3" ><xsl:value-of select="$NameParam" /></span>
			     			</xsl:when>
			     			<xsl:when test="$ObjectClassParam = 'Item'">
							<span class="level3" ><a target="_top">
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<!--<xsl:attribute name="onmouseover">highlight('<xsl:value-of select="$IDParam" />');</xsl:attribute>-->							<xsl:attribute name="href">
							<xsl:choose>
								<xsl:when test="$WebEditorParam='True'">ShowTemplate.aspx?Template=<xsl:value-of select="Template"/>&amp;Pkey=<xsl:value-of select="$IDParam" />&amp;Type=10</xsl:when>
								<xsl:otherwise><xsl:value-of select="$IDParam" />.html</xsl:otherwise>
							</xsl:choose>
							</xsl:attribute>
							   <xsl:choose>
							     <xsl:when test="ItemProp = ''">
							         &#160;<xsl:value-of select="$NameParam" />
							     </xsl:when>	
							     <xsl:otherwise>
								<xsl:choose>
 								    <!-- CS -->
								    <xsl:when test="ItemProp = 'CS'">
								         <font size = "1" color="Red" > &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- MI -->
								    <xsl:when test="ItemProp = 'MI'">
								         <font size = "1" color="#FF00FF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
 								    <!-- NMI -->
								    <xsl:when test="ItemProp = 'NMI'">
								         <font size = "1" color="#FFC300"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  							    
								    <!-- NDI -->
								    <xsl:when test="ItemProp = 'NDI'">
								         <font size = "1" color="#00FF00"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
								    <!-- NR -->
								    <xsl:when test="ItemProp = 'NR'">
								         <font size = "1" color="#00FFFF"> &#160;<xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
								    </xsl:when>  
									    
								    <xsl:otherwise>
							                <font size = "1"><xsl:value-of select="ItemProp" /></font>&#160;<xsl:value-of select="$NameParam" />
							            </xsl:otherwise>
							    
							</xsl:choose>  							

							     
							     </xsl:otherwise>
							    </xsl:choose> 								</a></span>
			     			</xsl:when>
					     	<xsl:otherwise>		
					               <span class="level3" ><a target="_top">
							<xsl:attribute name="name"><xsl:value-of select="$IDParam" />a</xsl:attribute>
							<xsl:attribute name="href"><xsl:value-of select="$IDParam" />.html
							</xsl:attribute><xsl:value-of select="$NameParam" />
							</a></span>
				     		</xsl:otherwise>
                        		</xsl:choose>
<!--//SPACES HERE-->
</div>
</xsl:otherwise>
			</xsl:choose>
		</xsl:otherwise>
		
	</xsl:choose>
	
</xsl:template>


</xsl:stylesheet>