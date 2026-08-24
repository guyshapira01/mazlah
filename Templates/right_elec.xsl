<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">



<xsl:template match="/">

<html>
<base target="_parent"/>
<LINK href="Style.css" rel="stylesheet" type="text/css" />
<head>
<title>Table of Parts</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>





</head>

<body class="BodyCatalog" dir="rtl">

<TABLE border="1" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr">
	<tr>
		<td colspan="6" align="right" dir="rtl" style="background-color:lightblue"><b>איתור רכיבים</b>&#160;&#160;&#160;
		
		
  
  <!--add by guy feb-2014===============================================-->
<select id="ddParts" name="ddParts" onchange="highlight_table(this.value)">
  <xsl:for-each select="Items/Item">
	<script type="text/javascript">
			
				var obj=document.getElementById("ddParts");  
				
				
				var scheme  = [];
				var arr1 = [/\R\d*\-\R\d*/,'R'];
				var arr2 = [/\C\d*\-\C\d*/,'C'];
				var arr3 = [/\J\d*\-\J\d*/,'J'];
				var arr4 = [/\RN\d*\-\RN\d*/,'RN'];
				scheme.push(arr1);
				scheme.push(arr2);
				scheme.push(arr3);
				scheme.push(arr4);
				
				
				var curValue='<xsl:value-of select="PartName"/>';
				var curID='<xsl:value-of select="ID"/>';
				
				for(var k=0;k&lt;scheme.length;k++)
				{
					//alert(scheme[k][0]);
					var sIndex=curValue.search(scheme[k][0])*1;
					alert(sIndex);
					if (sIndex!=-1) 
					{
						var pre=curValue.substring(0,sIndex-1);
						
						var fNum  =curValue.substring(sIndex);
						lNum=fNum.substring(fNum.indexOf("-")+2);
						fNum = fNum.substring(1,fNum.indexOf("-"))
						lNum=parseInt(lNum);
						fNum=parseInt(fNum);
						for(var i=fNum;i&lt;lNum;i++)
						{
							//alert(i);
							opt = document.createElement("option");
							opt.value = curID;
							opt.appendChild(document.createTextNode(pre + scheme[k][1]+i));
							opt.text=pre + scheme[k][1]+i;
							obj.appendChild(opt);
						}
						opt = document.createElement("option");
						opt.value = curID;
						opt.appendChild(document.createTextNode(pre +scheme[k][1]+i));
						opt.text=pre +scheme[k][1]+i;
						obj.appendChild(opt);
						
						
					}
				}
		
			
	</script>
	
  </xsl:for-each>
</select>
		
		</td>
	</tr>
    <TR>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">כמות</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">שם הפריט</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">קוד יצרן</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מספר יצרן</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מק"ט</FONT></TD>
	<TD class="GridHeader" align="center"><FONT class="GridHeader">מס' חלק</FONT></TD>
	</TR>
	
	<xsl:for-each select="Items/Item">
			<TR class="GridMain" style="cursor:hand;" onclick="javascript:parent.frames('Parts Map').highlightAreaByName(this.id);highlightClick(this.id);">
			<xsl:attribute name="ID"><xsl:value-of select="ID" /></xsl:attribute>
			<TD align="center"><font class="GridMain">&#160;<xsl:value-of select="Amount"/>&#160;</font></TD>
			<TD align="right" DIR="RTL"><font class="GridMainBold">&#160;<xsl:value-of select="PartName"/>&#160;</font></TD>
			<TD align="left">
				<xsl:for-each select="ManufacturerCode">
					<nobr><font class="GridMain">&#160;<xsl:value-of select="."/>&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="left">
				<xsl:for-each select="ManufacturerNum">
					<nobr><font class="GridMain">&#160;<xsl:value-of select="."/>&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="left">
				<xsl:for-each select="CatalogNum">
					<nobr><font class="GridMainBold">&#160;<xsl:value-of select="."/>&#160;</font></nobr><br/>
				</xsl:for-each>
			</TD>
			<TD align="center">
				<xsl:for-each select="PartNum">
					<font class="GridMain">&#160;<xsl:value-of select="."/>&#160;</font><br/>
				</xsl:for-each>
			</TD>
		</TR>
	
	</xsl:for-each>
</TABLE>
<hr  size="5" color = "RED"/>
<BR/>

</body>
<script language="javascript">
document.write(document.lastModified);
var lastLight='';
function highlight_table(ctrl_id)
{
	highlight(ctrl_id);
	if(lastLight!=ctrl_id)
		unlight(lastLight);
	lastLight=ctrl_id;
}
 </script>
</html>
</xsl:template>
</xsl:stylesheet>