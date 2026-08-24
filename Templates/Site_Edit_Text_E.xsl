<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:template match="/">

<html>
<base target="_parent"/>
<LINK href="Style.css" rel="stylesheet" type="text/css" />
<head>
<title>Text</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<!--<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />-->
<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css" />

<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
<SCRIPT LANGUAGE="VbScript" SRC="webEditor.vbs"></SCRIPT>


</head>

<body style="background-color:WhiteSmoke;" dir="ltr" topmargin="20" leftmargin="20">
	<form name="UpdateObject" method="post">
	<input type="hidden">
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PKEY"/></xsl:attribute>
		<xsl:attribute name="name">Pkey</xsl:attribute>
	</input>

<!--	<input type="hidden">
		<xsl:attribute name="value">3</xsl:attribute>
		<xsl:attribute name="name">PkeyType</xsl:attribute>
	</input>

-->

	<!--<input type="hidden">
		<xsl:attribute name="value"></xsl:attribute>
		<xsl:attribute name="name">PENGDESC</xsl:attribute>
	</input>-->


   <!--	<input type="hidden">
		<xsl:attribute name="value">normal_Documents_TAAS_E.html</xsl:attribute>
		<xsl:attribute name="name">PPARTTEMPLATE</xsl:attribute>
	</input>

  -->

	<input type="hidden">
		<xsl:attribute name="value">T_CAT_TEXT</xsl:attribute>
		<xsl:attribute name="name">TABLE</xsl:attribute>
	</input>
	<input type="hidden">
		<xsl:attribute name="value">6</xsl:attribute>
		<xsl:attribute name="name">POBJECTTYPE</xsl:attribute>
	</input>


	<input type="hidden">
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PCONTENT"/></xsl:attribute>
		<xsl:attribute name="name">PCONTENT</xsl:attribute>
	</input>


<div align="center">
<table  >
<tr align="center">
<td>
<font class="TitleText_E">Text Properties</font>
<hr/>
<TABLE border="1" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr" width="728" height="565">
 <TR>
		<TD class="bigtext_E" align="left"><b>Name:</b></TD>
		<TD class="tabletext" align="left" width="100%">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
				<xsl:attribute name="name">PHEBDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:250px</xsl:attribute>
				<xsl:attribute name="onkeyup">JavaScript: if ('<xsl:value-of select="//PATHMODE"/>'=='PARENTANDCURRENT'){if ('<xsl:value-of select="//UPLOADMODE"/>'=='CopyFileToNetwork'){document.getElementById('destcopy').value = v + document.getElementById('PHEBDESC').value;}}</xsl:attribute>
			</input>
		</TD>
   </TR>
 <TR>
	<TD class="bigtext_E" align="left"><b>Language:</b></TD>
	<TD class="tabletext" align="left" width="100%">
			<SELECT id="PENGDESC" name="PENGDESC">
			<xsl:attribute name="style">width:350px;</xsl:attribute>
			<OPTION value="Hebrew">
				<xsl:choose>
					<xsl:when test="'Hebrew'=//NewDataSet/Catalogs/PENGDESC">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
			Hebrew
			</OPTION>
			<OPTION value="English">
			<xsl:choose>
					<xsl:when test="'English'=//NewDataSet/Catalogs/PENGDESC">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
			English
			</OPTION>
			</SELECT>
		<!--
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PENGDESC"/></xsl:attribute>
				<xsl:attribute name="name">PENGDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:250px</xsl:attribute>
			</input>-->
	</TD>
</TR>

<TR>

	<TD class="bigtext_E" align="left"><b>Position:</b></TD>
	<TD class="tabletext" align="left" width="100%">
		<SELECT id="PPOSITION" name="PPOSITION">
			<xsl:attribute name="style">WIDTH:250px</xsl:attribute>
			<OPTION>
				<xsl:choose>
					<xsl:when test="3=//NewDataSet/Catalogs/PPOSITION">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value">3</xsl:attribute>
				FreeText
			</OPTION>
			<OPTION>
				<xsl:choose>
					<xsl:when test="4=//NewDataSet/Catalogs/PPOSITION">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value">4</xsl:attribute>
				ImageGallery
			</OPTION>
			<OPTION>
				<xsl:choose>
					<xsl:when test="6=//NewDataSet/Catalogs/PPOSITION">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value">6</xsl:attribute>
				UserTips
			</OPTION>
			<OPTION>
				<xsl:choose>
					<xsl:when test="7=//NewDataSet/Catalogs/PPOSITION">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value">7</xsl:attribute>
				SoftwareUpdates
			</OPTION>
			<OPTION>
				<xsl:choose>
					<xsl:when test="8=//NewDataSet/Catalogs/PPOSITION">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value">8</xsl:attribute>
				SystemReqs
			</OPTION>
			<OPTION>
				<xsl:choose>
					<xsl:when test="9=//NewDataSet/Catalogs/PPOSITION">
						<xsl:attribute name="selected">True</xsl:attribute>
					</xsl:when>
				</xsl:choose>
				<xsl:attribute name="value">9</xsl:attribute>
				General
			</OPTION>
		</SELECT>
	</TD>
</TR>

<TR height="100%">
	<TD colspan="2" align="center"><iframe name="ttt" src="../Scripts/RTE/default.asp" width="100%" height="100%" frameborder="0" border="0"></iframe></TD>
</TR>
	

<!--<TR>
	<TD colspan="2" align="center"><input type="Submit" value="Update"/></TD>
</TR>-->
</TABLE>
</td>
</tr>
</table>
</div>
</form>

<!--<hr  size="5" clolor = "RED"/>-->

<!--<DIV align="CENTER">
This Page Update At:
<script language="javascript">
document.write(document.lastModified);
</script>
</DIV>-->
<script language="javascript">
	window.resizeTo(800,680);
</script>
</body>
</html>
</xsl:template>
</xsl:stylesheet>