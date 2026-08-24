<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:template match="/">

<html>
<base target="_parent"/>
<!--<LINK href="Style.css" rel="stylesheet" type="text/css" />-->
<SCRIPT LANGUAGE="javascript" SRC="ToolBar.js"></SCRIPT>
<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlueNoGradiante.css" TYPE="text/css" />
<head>
<title>Item</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<!--<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />-->
<!--<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />-->

<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
<SCRIPT LANGUAGE="VbScript" SRC="webEditor.vbs"></SCRIPT>

<script LANGUAGE="JavaScript">
	//change the window size and position
	window.moveTo(20,20)
	window.resizeTo(550,635) 
</script>

</head>

<body style="background-color:WhiteSmoke;" dir="ltr" topmargin="0" leftmargin="0">
<div width="100%" height="100%" style="background-color:WhiteSmoke;">
<!-- Start Validation -->
<script language="JavaScript">
	
	function validateForm()	{
		return true;
	}
</script>
<!-- End Validation -->
	<form name="NewObject" method="post" onSubmit="JavaScript:return validateForm()">
	<input type="hidden">
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PKEY"/></xsl:attribute>
		<xsl:attribute name="name">Pkey</xsl:attribute>
	</input>
	<!--<input type="hidden">
		<xsl:attribute name="value">Failure Report</xsl:attribute>
		<xsl:attribute name="name">PENGDESC</xsl:attribute>
	</input>-->
	<input type="hidden">
		<xsl:attribute name="value">T_CAT_PART</xsl:attribute>
		<xsl:attribute name="name">TABLE</xsl:attribute>
	</input>
	<input type="hidden">
		<xsl:attribute name="value">10</xsl:attribute>
		<xsl:attribute name="name">POBJECTTYPE</xsl:attribute>
	</input>
	
	<input type="hidden">
		<xsl:attribute name="value">1</xsl:attribute>
		<xsl:attribute name="name">PPUBLISH</xsl:attribute>
	</input>
		<input type="hidden">
		<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP50"/></xsl:attribute>
		<xsl:attribute name="name">PPROP50</xsl:attribute>
	</input>
	
	
<div align="center">
<table>
<TR>
	<TD align="right"><xsl:value-of select="//CURRENTUSER"/></TD>
</TR>


<tr align="center">
<td>
<img border="0" align="absmiddle" src="images/NewDocument.gif" width="35"></img>&#160;&#160;<font color="9b0000" style="FONT-SIZE:24px">Edit Site Item</font>
<hr/>
<TABLE border="1" cellPadding="2" cellSpacing="0" align="center" bordercolor="black" dir="ltr">
 <TR>
		<TD bgcolor="#eef7de" class="TableHeader" ><b>Name:</b></TD>
		<TD class="TableText" align="center">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
				<xsl:attribute name="name">PHEBDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
		</TD>
   </TR>
<TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Comment:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP20"/></xsl:attribute>
				<xsl:attribute name="name">PPROP20</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
	</TD>
</TR>
<!--
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Template:</b></TD>
	<TD class="tabletext" align="left">
			<SELECT id="_exclude_PPARTTEMPLATE" name="_exclude_PPARTTEMPLATE" onchange="JavaScript:PPARTTEMPLATE.value=this.options[this.selectedIndex].value;">
				<OPTION value="SiteLayout.html">SiteLayout</OPTION>
				<OPTION value="SiteLayout1.html">SiteLayout1</OPTION>
			</SELECT>
	</TD>
</TR>
-->
<TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Template:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPARTTEMPLATE"/></xsl:attribute>
				<xsl:attribute name="name">PPARTTEMPLATE</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
	</TD>
</TR>
<TR>
	<script>
		var selectedL = '';
	</script>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Layout:</b></TD>
	<TD class="tabletext" align="left">
	<table style="border:0"><TR>
		<TD id="layout0" class="tabletext" align="left" onmouseover="JavaScript:if(this.style.border != 'yellow 1px solid')this.style.border='1px black solid';" onmouseout="JavaScript:if(this.style.border == 'black 1px solid')this.style.border='1px WhiteSmoke solid';" onclick="JavaScript:PPROP50.value='layout0';if (selectedL != '') selectedL.style.border='1px WhiteSmoke solid';selectedL=this;selectedL.style.border='1px yellow solid'">
		<img src="Images/sitelayout.jpg"></img>
		</TD>
		<TD id="layout1" class="tabletext" align="left" onmouseover="JavaScript:if(this.style.border != 'yellow 1px solid')this.style.border='1px black solid';" onmouseout="JavaScript:if(this.style.border == 'black 1px solid')this.style.border='1px WhiteSmoke solid';" onclick="JavaScript:PPROP50.value='layout1';if (selectedL != '') selectedL.style.border='1px WhiteSmoke solid';selectedL=this;selectedL.style.border='1px yellow solid'">
		<img src="Images/sitelayout1.jpg"></img>
		</TD>
		<TD class="tabletext" align="left">
		</TD>
	</TR></table>
	</TD>
		<script>
		var lName;
		
		if (NewObject.PPROP50.value != '')
		{
			lName = NewObject.PPROP50.value;
			
			selectedL = document.getElementById(lName);
			if (selectedL != null)
				selectedL.style.border='1px yellow solid'
			else
				selectedL = '';
		}
	</script>
</TR>
<TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Restricted:</b></TD>
	<TD class="tabletext" align="left">
			<SELECT id="PPROP25" name="PPROP25">
				<xsl:attribute name="style">width:350px;</xsl:attribute>
				<OPTION value="false">
					<xsl:choose>
						<xsl:when test="'false'=//NewDataSet/Catalogs/PPROP25">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
				No</OPTION>
				<OPTION value="true">
					<xsl:choose>
						<xsl:when test="'true'=//NewDataSet/Catalogs/PPROP25">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
					Yes</OPTION>
			</SELECT>
	</TD>
</TR>
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Status:</b></TD>
	<TD class="tabletext" align="left">
			<SELECT id="PSTATUS" name="PSTATUS">
				<OPTION value=""></OPTION>
				<xsl:for-each select="//NewDataSet/Status">
					<OPTION><xsl:attribute name="value"><xsl:value-of select="PKEY"/></xsl:attribute>
						<xsl:value-of select="PSTATUS"/>
					</OPTION>
				</xsl:for-each>
			</SELECT>
	</TD>
</TR>
 
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Type:</b></TD>
	<TD class="tabletext" align="left">
		<SELECT id="PKEYTYPE" name="PKEYTYPE">
			<xsl:attribute name="style">width:350px;</xsl:attribute>
				<xsl:for-each select="//NewDataSet/ObjectType">
				<xsl:choose>
					<xsl:when test="'10'=CLASS">
						<OPTION>
							<xsl:choose>
								<xsl:when test="PKEY=//NewDataSet/Catalogs/PKEYTYPE">
									<xsl:attribute name="selected">True</xsl:attribute>
								</xsl:when>
							</xsl:choose>
							<xsl:attribute name="value"><xsl:value-of select="PKEY"/></xsl:attribute>
							<xsl:value-of select="PTYPENAME"/>
						</OPTION>
					</xsl:when>
				</xsl:choose>
		</xsl:for-each>
		</SELECT>
	</TD>
</TR>
  <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Direction:</b></TD>
			<TD class="TableText" align="left">
				<SELECT id="PPROP3" name="PPROP3">
				<xsl:attribute name="style">width:350px;</xsl:attribute>
				<OPTION value="ltr">
					<xsl:choose>
						<xsl:when test="'ltr'=//NewDataSet/Catalogs/PPROP3">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
					Left</OPTION>
				<OPTION value="rtl">
					<xsl:choose>
						<xsl:when test="'rtl'=//NewDataSet/Catalogs/PPROP3">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
				Right</OPTION>
			</SELECT>
			
			</TD>
</TR>
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Created Date:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/TodayDate/CURRENTDATE"/></xsl:attribute>
				<xsl:attribute name="name">PCREATEDATE</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
	</TD>
</TR>
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Language:</b></TD>
			<TD class="TableText" align="left">
				<SELECT id="PPROP10" name="PPROP10">
				<xsl:attribute name="style">width:350px;</xsl:attribute>
				<OPTION value="Hebrew">
					<xsl:choose>
						<xsl:when test="'Hebrew'=//NewDataSet/Catalogs/PPROP10">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
					Hebrew</OPTION>
				<OPTION value="English">
					<xsl:choose>
						<xsl:when test="'English'=//NewDataSet/Catalogs/PPROP10">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
				English</OPTION>
			</SELECT>
			
			</TD>
</TR>
<TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Last Modified:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PUPDATEDATE"/></xsl:attribute>
				<xsl:attribute name="name">_exclude_PUPDATEDATE</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
				<xsl:attribute name="readonly">true</xsl:attribute>
			</input>
	</TD>
</TR>
<TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>User:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PKEYUSER"/></xsl:attribute>
				<xsl:attribute name="name">_exclude_USER</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
				<xsl:attribute name="readonly">true</xsl:attribute>
			</input>
	</TD>
</TR>
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Course start date:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP15"/></xsl:attribute>
				<xsl:attribute name="name">PPROP15</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
			&#160;<IMG id="PPROP15Cal" onclick="javascript:OpenCalendar('calendar_windowB','calendar.aspx?formname=NewObject.PPROP15&amp;cur=' + NewObject.PPROP15.value);" height="16" src="calendar.gif" width="16" name="PPROP15Cal" onMouseOver="this.style.cursor='hand';" onMouseOut="JavaScript:this.style.cursor='auto';"/>
	</TD>
</TR>
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Course end date:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP16"/></xsl:attribute>
				<xsl:attribute name="name">PPROP16</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
			&#160;<IMG id="PPROP16Cal" onclick="javascript:OpenCalendar('calendar_windowB','calendar.aspx?formname=NewObject.PPROP16&amp;cur=' + NewObject.PPROP16.value);" height="16" src="calendar.gif" width="16" name="PPROP16Cal" onMouseOver="this.style.cursor='hand';" onMouseOut="JavaScript:this.style.cursor='auto';"/>
	</TD>
</TR>
</TABLE>
<TABLE cellpadding="0" cellspacing="0" border="0" ID="PartToolbar2" class="ToolBar">

<TR >
	<TD height="30" class="ToolBarGlow" style="cursor:hand" onmouseover="glow(true, this);" onmouseout="glow(false, this);" onClick="javascript:NewObject.submit();"><a>&#160;&#160;<img class="ToolBarImage" src="Images/save.gif"/>&#160;&#160;Save</a></TD>
	<TD height="30" class="ToolBarGlow" style="cursor:hand" onmouseover="glow(true, this);" onmouseout="glow(false, this);" onClick="javascript:window.close();"><a>&#160;&#160;<img class="ToolBarImage" src="Images/close.ico"/>&#160;&#160;Close</a></TD>
</TR>
</TABLE>
</td>
</tr>
</table>
</div>
</form>
</div>
<!--<DIV align="CENTER">
This Page Update At:
<script language="javascript">
document.write(document.lastModified);
</script>
</DIV>-->
</body>
</html>
</xsl:template>
</xsl:stylesheet>