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
	window.resizeTo(550,430) 
</script>

</head>

<body style="background-color:WhiteSmoke;" dir="ltr" topmargin="0" leftmargin="0">
<div width="100%" height="100%" style="background-color:WhiteSmoke;">
<!-- Start Validation -->
<script language="JavaScript">
	
	function validateForm()	{
	//	if (NewObject.PPROP42.value=='')	{
	//		alert("please fill AlertDays field.");
	//		NewObject.PPROP42.focus();
	//		return false;
	//	}
		//if (NewObject.FolderKey.checked){
		//	NewObject.PPROP45.value='Master';
		//	NewObject.PPROP45.value=NewObject.PHEBDESC.value;
		//	NewObject.PPROP45.value=NewObject.PKEY.value;
		//}
		
		return true;
		
		
	}
</script>
<!-- End Validation -->
	<input type="hidden">
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PKEY"/></xsl:attribute>
		<xsl:attribute name="name">Pkey</xsl:attribute>
	</input>
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
		<xsl:attribute name="value">Normal_Documents_EER_E.html</xsl:attribute>
		<xsl:attribute name="name">PPARTTEMPLATE</xsl:attribute>
	</input>
	
<div align="center">
<table>
<TR>
	<TD align="right"><xsl:value-of select="//CURRENTUSER"/></TD>
</TR>


<tr align="center">
<td>
<img border="0" align="absmiddle" src="images/NewDocument.gif" width="35"></img>&#160;&#160;<font color="9b0000" style="FONT-SIZE:24px">Details</font>
<hr/>
<TABLE border="1" cellPadding="2" cellSpacing="0" align="center" bordercolor="black" dir="ltr">
 <TR>
		<TD bgcolor="#eef7de" class="TableHeader" ><b>Name:</b></TD>
		<TD class="TableText" align="center">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
				<xsl:attribute name="name">PHEBDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
				<xsl:attribute name="disabled">True</xsl:attribute>
			</input>
		</TD>
   </TR>
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Description:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PENGDESC"/></xsl:attribute>
				<xsl:attribute name="name">PENGDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
				<xsl:attribute name="disabled">True</xsl:attribute>
			</input>
	</TD>
</TR>


 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Status:</b></TD>
	<TD class="tabletext" align="left">
			<SELECT id="PSTATUS" name="PSTATUS">
				<xsl:attribute name="disabled">True</xsl:attribute>
				<OPTION value=""></OPTION>
				<xsl:for-each select="//NewDataSet/Status">
					<OPTION>
					<xsl:choose>
						<xsl:when test="PKEY=//NewDataSet/Catalogs/PSTATUS">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
					<xsl:attribute name="value"><xsl:value-of select="PKEY"/></xsl:attribute>
						<xsl:value-of select="PSTATUS"/>
					</OPTION>
				</xsl:for-each>
			</SELECT>
	</TD>
</TR>

 
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Owner:</b></TD>
	<TD class="tabletext" align="left">
				<SELECT>
						<xsl:attribute name="disabled">True</xsl:attribute>
						<xsl:attribute name="name">PKEYUSER</xsl:attribute>
						<xsl:attribute name="id">PKEYUSER</xsl:attribute>
					<OPTION value=""></OPTION>
				<xsl:for-each select="//NewDataSet/Users">
					<OPTION>
						<xsl:choose>
							<xsl:when test="PKEY=//NewDataSet/Catalogs/PKEYUSER">
								<xsl:attribute name="selected">True</xsl:attribute>
							</xsl:when>
						</xsl:choose>
						<xsl:attribute name="value"><xsl:value-of select="PKEY"/></xsl:attribute>
						<xsl:value-of select="PUSERNAME"/>
					</OPTION>
				</xsl:for-each>
				</SELECT>
	</TD>
</TR>
 
 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Type:</b></TD>
	<TD class="tabletext" align="left">
		<SELECT id="PKEYTYPE" name="PKEYTYPE">
			<xsl:attribute name="disabled">True</xsl:attribute>
			<xsl:attribute name="style">width:350px;</xsl:attribute>
		<xsl:for-each select="//NewDataSet/ObjectType">
			<xsl:choose>
				<xsl:when test="CLASS='18'"></xsl:when>
				<xsl:otherwise><!--empty or not 18-->
					<xsl:for-each select="//NewDataSet/ObjectType">
						<OPTION>
							<xsl:choose>
								<xsl:when test="PKEY=//NewDataSet/Catalogs/PKEYTYPE">
									<xsl:attribute name="selected">True</xsl:attribute>
								</xsl:when>
							</xsl:choose>
							<xsl:attribute name="value"><xsl:value-of select="PKEY"/></xsl:attribute>
							<xsl:value-of select="PTYPENAME"/>
						</OPTION>
					</xsl:for-each>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
		</SELECT>
	</TD>
</TR>


 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Created Date:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="disabled">True</xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PCREATEDATE"/></xsl:attribute>
				<xsl:attribute name="name">PCREATEDATE</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
	</TD>
</TR>

 <TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Last Update:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="disabled">True</xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PUPDATEDATE"/></xsl:attribute>
				<xsl:attribute name="name">PUPDATEDATE</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
	</TD>
</TR>



<TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Revision:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
							<xsl:attribute name="disabled">True</xsl:attribute>
							<xsl:attribute name="value">
								<xsl:choose>
									<xsl:when test="//NewDataSet/Revision/ISREVISE='yes'"><xsl:value-of select="//NewDataSet/Revision/NEWREVISION" /></xsl:when>
									<xsl:otherwise><xsl:value-of select="//NewDataSet/Catalogs/PREVISION" /></xsl:otherwise>
								</xsl:choose>
							</xsl:attribute>
							<xsl:attribute name="name">PREVISION</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
	</TD>
</TR>

 <TR id="AttachedProperty">
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Attached Property Name:</b></TD>
	<TD class="tabletext" align="left">
			<input type="text">
				<xsl:attribute name="disabled">True</xsl:attribute>
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP10" /></xsl:attribute>
				<xsl:attribute name="name">PPROP10</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px;</xsl:attribute>
			</input>
	</TD>
	</TR>
	
<!--
</TR>
<TR>
	<TD bgcolor="#eef7de" class="TableHeader" ><b>Show Latest Revision:</b></TD>
	<TD class="TableText" align="left">
				
				<SELECT id="OCPROP20" name="OCPROP20">
				<xsl:attribute name="style">width:350px;</xsl:attribute>
				<OPTION value="True">
					<xsl:choose>
						<xsl:when test="True=//NewDataSet/Nodes/PNPROP20">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
					True</OPTION>
				<OPTION value="False">
					<xsl:choose>
						<xsl:when test="False=//NewDataSet/Nodes/PNPROP20">
							<xsl:attribute name="selected">True</xsl:attribute>
						</xsl:when>
					</xsl:choose>
				False</OPTION>
			</SELECT>
			
			</TD>
</TR>

-->

</TABLE>
</td>
</tr>
</table>
</div>
</div>
</body>
</html>
</xsl:template>
</xsl:stylesheet>