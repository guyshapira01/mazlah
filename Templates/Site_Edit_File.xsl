<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:template match="/">

<html>
<!--<base target="_parent"/>-->
<!--<LINK href="Style.css" rel="stylesheet" type="text/css" />-->
<head>
<title>Document</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<!--<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />-->
<LINK REL="stylesheet" HREF="tabs_SteelBlue.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlueNoGradiante.css" TYPE="text/css" />
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
<script language="JavaScript" src="Calendar.js"/>
<SCRIPT LANGUAGE="VbScript" SRC="webEditor.vbs"></SCRIPT>
<SCRIPT LANGUAGE="javascript" SRC="ToolBar.js"></SCRIPT>

</head>

<body  dir="ltr" topmargin="0" leftmargin="0" style="background-color:WhiteSmoke;">
	<div width="100%" height="100%" style="background-color:WhiteSmoke;">
	<form name="NewObject" method="post">
	<input type="hidden">
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PKEY"/></xsl:attribute>
		<xsl:attribute name="name">Pkey</xsl:attribute>
	</input>
	<input type="hidden">
		<xsl:attribute name="value">T_CAT_WINOBJECT</xsl:attribute>
		<xsl:attribute name="name">TABLE</xsl:attribute>
	</input>
	<input type="hidden">
		<xsl:attribute name="value">18</xsl:attribute>
		<xsl:attribute name="name">POBJECTTYPE</xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">PFILEEXTENSION</xsl:attribute>
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PFILEEXTENSION"/></xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">PLINKTONETWORKFILE</xsl:attribute>
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PLINKTONETWORKFILE"/></xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">PUploadFileName</xsl:attribute>
		<xsl:attribute name="value"></xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">PUploadMode</xsl:attribute>
		<xsl:attribute name="value"></xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">FileExists</xsl:attribute>
		<xsl:attribute name="value"></xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">destcopy</xsl:attribute>
		<xsl:attribute name="value"></xsl:attribute>
	</input>


<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%">

 <TR>
	<TD align="right" colspan="2">Curren User:&#160;&#160;
	<font style="color:red"><xsl:value-of select="//CURRENTUSER"/></font>
	</TD>

</TR>

<tr>
<td align="center" colspan="4">
<img border="0" align="absmiddle" src="images/NewDocument.gif" width="35"></img>&#160;&#160;<font color="9b0000" style="FONT-SIZE:24px">Edit Site File</font>
<hr/>
</td>
</tr>

	<TR>
		<TD width="150" nowrap="yes">&#160;&#160;Dcoument Name:</TD>
		<TD>
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
				<xsl:attribute name="name">PHEBDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
		</TD>
	</TR>
	<TR>
		<TD width="150" nowrap="yes">&#160;&#160;Comment:</TD>
		<TD>
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PCOMMENT"/></xsl:attribute>
				<xsl:attribute name="name">PCOMMENT</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
		</TD>
	</TR>
</TABLE>
<br/>
<TABLE border="0" cellPadding="0" cellSpacing="10" width="100%">
  <TR>
    <TD align="left" vAlign="top" width="100%"> 

<!--Start Content of tab 1 -->
<TABLE class="regular"	border="0" cellPadding="3" cellSpacing="0" align="center" dir="ltr" width="90%">
	<xsl:for-each select="NewDataSet/Catalogs">
	
		<TR>
			<TD class="normal" width="25%">Create Date:</TD>
			<TD width="55%" align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PCREATEDATE']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PCREATEDATE',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:155px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PCREATEDATE" /></xsl:attribute>
							<xsl:attribute name="name">PCREATEDATE</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
				<!--<IMG id="PCREATEDATECal" onclick="javascript:OpenCalendar('calendar_windowA','calendar.aspx?formname=NewObject.PCREATEDATE&amp;cur=' + NewObject.PCREATEDATE.value);"	height="16" src="calendar.gif" width="16" name="PCREATEDATECal" onMouseOver="this.style.cursor='hand';" onMouseOut="JavaScript:this.style.cursor='auto';"/>-->
			</TD>
			<TD class="normal" width="25%">Update Date:</TD>
			<TD width="40%" align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PUPDATEDATE']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PUPDATEDATE',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
							<xsl:element name="input">
								<xsl:attribute name="type">text</xsl:attribute>
								<xsl:attribute name="style">width:170px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
								<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PUPDATEDATE" /></xsl:attribute>
								<xsl:attribute name="name">PUPDATEDATE</xsl:attribute>
								<xsl:attribute name="readonly">yes</xsl:attribute>
							</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
				<!--<IMG id="PUPDATEDATECal" onclick="javascript:OpenCalendar('calendar_windowB','calendar.aspx?formname=NewObject.PUPDATEDATE&amp;cur=' + NewObject.PUPDATEDATE.value);"	height="16" src="calendar.gif" width="16" name="PUPDATEDATECal" onMouseOver="this.style.cursor='hand';" onMouseOut="JavaScript:this.style.cursor='auto';"/>-->
			</TD>
		</TR>
		<TR>
			<TD class="normal">Status:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<SELECT id="PSTATUS" name="PSTATUS">
				<xsl:attribute name="style">width:155px</xsl:attribute>
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
			<TD class="normal">Object Type:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<SELECT id="PKEYTYPE" name="PKEYTYPE">
				<xsl:attribute name="style">width:170px</xsl:attribute>
					<OPTION value=""></OPTION>
				<xsl:for-each select="//NewDataSet/ObjectType">
				<xsl:choose>
					<xsl:when test="'18'=CLASS">
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
			<TD width="25%">Language:</TD>
			<TD width="25%" align="left">&#160;
						<SELECT id="PPROP10" name="PPROP10">
							<xsl:attribute name="style">width:156px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
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
			<TD class="normal">Owning User:</TD>
			<TD align="left" class="NotesTabs" colspan="3">&#160;


						<SELECT id="PKEYUSER" name="PKEYUSER">
							<OPTION value=""></OPTION>
						<xsl:for-each select="//NewDataSet/Users">
							<OPTION>
								<xsl:choose>
									<xsl:when test="PKEY=//CURRENTUSER">
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
			<TD class="normal">File Path:</TD>
			<TD align="left" colspan="4" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PORGFILENAME']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PORGFILENAME',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:400px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME" /></xsl:attribute>
							<xsl:attribute name="name">PORGFILENAME</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
		<TD class="normal">Size:</TD>
		<TD align="left" colspan="4" class="NotesTabs">&#160;
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPROP4"/></xsl:attribute>
				<xsl:attribute name="name">PPROP4</xsl:attribute>
				<xsl:attribute name="style">WIDTH:70px</xsl:attribute>
			</input>
		</TD>
	</TR>
		</xsl:for-each>

</TABLE>

<SCRIPT language="JavaScript">
	var keypl = getLoc(0);
	var parentpl = getLoc(1);
	var catalogpl = getLoc(2);
</SCRIPT>


</TD>
</TR>
<TR>
	<TD align="left" id="FileUpload" width="100%"></TD>
</TR>
<TR>
	<TD align="left">
	 	<xsl:choose>
			<xsl:when test="//ACLPERMISSIONSNAME = 'Administrator'">
				<iframe border="0" frameborder="0" scrolling="no" width="98%" height="120" name="ift"></iframe>
				<script>
				//alert(parentpl);
				document.ift.location.href = '../FileUpload.aspx?Parent='+ parentpl +'&amp;Pkey='+ keypl +'&amp;PkeyCatalog='+ catalogpl;
			</script>
			</xsl:when>
			<xsl:otherwise>
				<iframe src="../FileUpload.aspx?Network=False&amp;CeckboxAvialable=No" border="0" frameborder="0" scrolling="no" width="350" height="100"></iframe>
			</xsl:otherwise>
	 	</xsl:choose>
	</TD>
</TR>

</TABLE>
<table class="toolbar">
	<TR >
		
		<td class="ToolBarGlow" onClick="javascript:NewObject.submit();" >
					<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable1" style="cursor:hand" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
						<tr>
							<td align="center" nowrap="yes">
								<a class="ToolBarFont">
								<img  height="16" src="Images/save.gif" align="absmiddle" border="0"/>&#160;Save</a>
							</td>
						</tr>
					</table>
			</td>
			<td class="ToolBarGlow" onClick="javaScript:window.close();" >
					<table width="100%" cellspacing="2" cellpadding="2"  ID="btnTable0" style="cursor:hand" onmouseover="glow(true, this);" onmouseout="glow(false, this);">
						<tr>
							<td align="center" nowrap="yes">
								<a class="ToolBarFont">
								<img  height="16" src="WEImages/delete.gif" align="absmiddle" border="0"/>&#160;Cancel</a>
								
							</td>
						</tr>
					</table>
			</td>			
		
	</TR>
</table>
</form>

<script language="JavaScript">
	function uploadFile()	{
		window.open('../FileUpload.aspx','FileUpload','toolbar=no,width=300,height=70');
	}
	window.resizeTo('650','540');
	window.moveTo('20','20');
</script>
</div>
</body>
</html>
</xsl:template>
</xsl:stylesheet>