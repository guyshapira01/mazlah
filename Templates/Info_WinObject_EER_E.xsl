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
<SCRIPT LANGUAGE="JavaScript" SRC="webeditor.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
<script language="JavaScript" src="Calendar.js"/>
<SCRIPT LANGUAGE="VbScript" SRC="webEditor.vbs"></SCRIPT>
<SCRIPT LANGUAGE="javascript" SRC="ToolBar.js"></SCRIPT>
<SCRIPT LANGUAGE="javascript" SRC="../publish.js"></SCRIPT>


</head>
<body dir="ltr" topmargin="0" leftmargin="0" style="background-color:WhiteSmoke;">
	<div width="100%" height="100%" style="background-color:WhiteSmoke;">
	<form name="NewObject" method="post">
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
		<xsl:attribute name="name">PPUBLISH</xsl:attribute>
		<xsl:attribute name="value">1</xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">FileExists</xsl:attribute>
		<xsl:attribute name="value"></xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">PKEY</xsl:attribute>
		<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PKEY" /></xsl:attribute>
	</input>

	<input type="Hidden">
		<xsl:attribute name="name">PDOCPATH</xsl:attribute>
		<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH" /></xsl:attribute>
	</input>

	<input type="Hidden">
		<xsl:attribute name="name">destcopy</xsl:attribute>
		<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME" /></xsl:attribute>
	</input>

<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%">

 <TR>
	<TD align="right" colspan="2">Curren User:&#160;&#160;
	<font style="color:red"><xsl:value-of select="//CURRENTUSER"/></font>
	</TD>

</TR>




	<TR>
		<TD width="150" nowrap="yes">&#160;&#160;Document Name:</TD>
		<TD>
			<input disabled="true" type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
				<xsl:attribute name="name">PHEBDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
		</TD>
	</TR>
	<TR>
		<TD width="150" nowrap="yes">&#160;&#160;Document Description:</TD>
		<TD>
			<input disabled="true" type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PENGDESC"/></xsl:attribute>
				<xsl:attribute name="name">PENGDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
			</input>
		</TD>
	</TR>
</TABLE>
<br/>
<TABLE border="0" cellPadding="0" cellSpacing="10" width="100%">
  <TR>
    <TD align="left" vAlign="top" width="100%">

		<TABLE border="1" cellPadding="0" cellSpacing="0" id="newsContent" width="98%" style="BORDER-LEFT: #000000 1px solid;"><TBODY>
		<TR><TD width="5"></TD><TD align="center" width="100%" class="clsTabSelected">

			<TABLE class="regular"	border="0" cellPadding="3" cellSpacing="0" align="center" dir="ltr" width="90%">
				<xsl:for-each select="NewDataSet/Catalogs">
				
					<TR>
						<TD class="normal" width="25%">Creation Date:</TD>
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
										<xsl:attribute name="disabled">True</xsl:attribute>
										<xsl:attribute name="type">text</xsl:attribute>
										<xsl:attribute name="style">width:155px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
										<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PCREATEDATE" /></xsl:attribute>
										<xsl:attribute name="name">PCREATEDATE</xsl:attribute>
										<xsl:attribute name="readonly">yes</xsl:attribute>
									</xsl:element>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
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
											<xsl:attribute name="disabled">True</xsl:attribute>
											<xsl:attribute name="type">text</xsl:attribute>
											<xsl:attribute name="style">width:170px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
											<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PUPDATEDATE" /></xsl:attribute>
											<xsl:attribute name="name">PUPDATEDATE</xsl:attribute>
											<xsl:attribute name="readonly">yes</xsl:attribute>
										</xsl:element>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
						</TD>
					</TR>

					<TR>
						<TD class="normal">File Creation Date:</TD>
						<TD align="left" colspan="1" class="NotesTabs">&#160;
							<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PFILECREATEDDATE']">
								<xsl:choose>
									<xsl:when test="PLISTOFVALUES!=''">
										<script language="JavaScript">
											document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PFILECREATEDDATE',''));
										</script>
									</xsl:when>
									<xsl:otherwise>
										<xsl:element name="input">
											<xsl:attribute name="disabled">True</xsl:attribute>
											<xsl:attribute name="type">text</xsl:attribute>
											<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
											<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PFILECREATEDDATE" /></xsl:attribute>
											<xsl:attribute name="name">PFILECREATEDDATE</xsl:attribute>
										</xsl:element>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
						</TD>
						<TD class="normal">File Update Date:</TD>
						<TD align="left" colspan="1" class="NotesTabs">&#160;
							<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PFILEMODIFIEDDATE']">
								<xsl:choose>
									<xsl:when test="PLISTOFVALUES!=''">
										<script language="JavaScript">
											document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PFILEMODIFIEDDATE',''));
										</script>
									</xsl:when>
									<xsl:otherwise>
									<xsl:element name="input">
										<xsl:attribute name="disabled">True</xsl:attribute>
										<xsl:attribute name="type">text</xsl:attribute>
										<xsl:attribute name="style">width:170px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
										<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PFILEMODIFIEDDATE" /></xsl:attribute>
										<xsl:attribute name="name">PFILEMODIFIEDDATE</xsl:attribute>
									</xsl:element>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
						</TD>
					</TR>
					<TR>
						<TD class="normal">Status:</TD>
						<TD align="left" class="NotesTabs">&#160;
							<SELECT disabled="true" id="PSTATUS" name="PSTATUS">
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
						<script language="JavaScript">
						function checkType()
						{
							var curType = document.getElementById('PKEYTYPE').value;
							if (curType=='')
								document.getElementById('PKEYTYPE').value = 2;
						}
						</script>
						<TD class="normal">Document Type:</TD>
						<TD align="left" class="NotesTabs">&#160;
							<SELECT disabled="true" id="PKEYTYPE" name="PKEYTYPE">
							<xsl:attribute name="onpropertychange">JavaScript:checkType();</xsl:attribute>
							<xsl:attribute name="style">width:170px</xsl:attribute>
								<OPTION value=""></OPTION>
							<xsl:for-each select="//NewDataSet/ObjectType">
							<xsl:choose>
								<xsl:when test="CLASS='18'">
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
						<script language="JavaScript">
						if (document.getElementById('PKEYTYPE').value == '')
							document.getElementById('PKEYTYPE').value=0;
						</script>
					</TR>
					<TR>
						<TD class="normal">Revision:</TD>
						<TD align="left" class="NotesTabs">&#160;
							<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PREVISION']">
								<xsl:choose>
									<xsl:when test="PLISTOFVALUES!=''">
										<script language="JavaScript">
											document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PREVISION',''));
										</script>
									</xsl:when>
									<xsl:otherwise>
									<xsl:element name="input">
										<xsl:attribute name="disabled">True</xsl:attribute>
										<xsl:attribute name="type">text</xsl:attribute>
										<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
										<xsl:attribute name="value">
											<xsl:choose>
												<xsl:when test="//NewDataSet/Revision/ISREVISE='yes'"><xsl:value-of select="//NewDataSet/Revision/NEWREVISION" /></xsl:when>
												<xsl:otherwise><xsl:value-of select="//NewDataSet/Catalogs/PREVISION" /></xsl:otherwise>
											</xsl:choose>										
										</xsl:attribute>
										<xsl:attribute name="name">PREVISION</xsl:attribute>
									</xsl:element>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
						</TD>
						<TD width="25%">Is Group Document:</TD>
						<TD width="25%" align="left">&#160;
						<SELECT disabled="true" id="PPROP4" name="PPROP4">
							<xsl:attribute name="style">width:170px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<OPTION value="False">
								<xsl:choose>
									<xsl:when test="'False'=PPROP4">
										<xsl:attribute name="selected">True</xsl:attribute>
									</xsl:when>
								</xsl:choose>
							False</OPTION>
							<OPTION value="True">
								<xsl:choose>
									<xsl:when test="'True'=PPROP4">
										<xsl:attribute name="selected">True</xsl:attribute>
									</xsl:when>
								</xsl:choose>
								True</OPTION>
						</SELECT>
									
						</TD>
					</TR>
					<TR>
						<TD class="normal">Document Number:</TD>
						<TD align="left" class="NotesTabs">&#160;
									<xsl:element name="input">
										<xsl:attribute name="disabled">True</xsl:attribute>
										<xsl:attribute name="type">text</xsl:attribute>
										<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
										<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PDOCNUMBER" /></xsl:attribute>
										<xsl:attribute name="name">PDOCNUMBER</xsl:attribute>
									</xsl:element>
						</TD>

						<TD class="normal">Origin:</TD>
						<TD align="left" class="NotesTabs">&#160;
							<SELECT disabled="true" id="PPROP7" name="PPROP7">
								<xsl:attribute name="style">width:170px</xsl:attribute>
								<OPTION>
									<xsl:choose>
										<xsl:when test="'EER'=//NewDataSet/Catalogs/PPROP7">
											<xsl:attribute name="selected">True</xsl:attribute>
										</xsl:when>
									</xsl:choose>
									<xsl:attribute name="value">EER</xsl:attribute>
									EER
								</OPTION>
								<OPTION>
									<xsl:choose>
										<xsl:when test="'ESH-DAR'=//NewDataSet/Catalogs/PPROP7">
											<xsl:attribute name="selected">True</xsl:attribute>
										</xsl:when>
									</xsl:choose>
									<xsl:attribute name="value">ESH-DAR</xsl:attribute>
									ESH-DAR
								</OPTION>
								<OPTION>
									<xsl:choose>
										<xsl:when test="'LUDAN'=//NewDataSet/Catalogs/PPROP7">
											<xsl:attribute name="selected">True</xsl:attribute>
										</xsl:when>
									</xsl:choose>
									<xsl:attribute name="value">LUDAN</xsl:attribute>
									LUDAN
								</OPTION>
								<OPTION>
									<xsl:choose>
										<xsl:when test="'RADON'=//NewDataSet/Catalogs/PPROP7">
											<xsl:attribute name="selected">True</xsl:attribute>
										</xsl:when>
									</xsl:choose>
									<xsl:attribute name="value">RADON</xsl:attribute>
									RADON
								</OPTION>
								<OPTION>
									<xsl:choose>
										<xsl:when test="'KURCHATOV Inst.'=//NewDataSet/Catalogs/PPROP7">
											<xsl:attribute name="selected">True</xsl:attribute>
										</xsl:when>
									</xsl:choose>
									<xsl:attribute name="value">KURCHATOV Inst.</xsl:attribute>
									KURCHATOV Inst.
								</OPTION>
								<OPTION>
									<xsl:choose>
										<xsl:when test="'External'=//NewDataSet/Catalogs/PPROP7">
											<xsl:attribute name="selected">True</xsl:attribute>
										</xsl:when>
									</xsl:choose>
									<xsl:attribute name="value">External</xsl:attribute>
									External
								</OPTION>
							</SELECT>
						</TD>


					</TR>
					<TR>
						<TD class="normal"><xsl:value-of select="//ParentObject/PPROP10"/>:</TD>
						<TD align="left" colspan="3" class="NotesTabs">&#160;
							<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP5']">
								<xsl:choose>
									<xsl:when test="PLISTOFVALUES!=''">
										<script language="JavaScript">
											document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP5',''));
										</script>
									</xsl:when>
									<xsl:otherwise>
										<SELECT id="PPROP9" name="PPROP9" disabled="true"><OPTION value=""></OPTION></SELECT>
										<script language="JavaScript">
											FillSelectBasedOnLOV(document.getElementById('PPROP9'), GetArrayOfLov(GetLOVByName('<xsl:value-of select="//ParentObject/PPROP10"/>')),'<xsl:value-of select="//NewDataSet/Catalogs/PPROP9" />');
										</script>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
						</TD>
					</TR>
					<TR>
						<TD class="normal">Key words:</TD>
						<TD align="left" colspan="3" class="NotesTabs">&#160;
							<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP5']">
								<xsl:choose>
									<xsl:when test="PLISTOFVALUES!=''">
										<script language="JavaScript">
											document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP5',''));
										</script>
									</xsl:when>
									<xsl:otherwise>
									<xsl:element name="input">
										<xsl:attribute name="disabled">True</xsl:attribute>
										<xsl:attribute name="type">text</xsl:attribute>
										<xsl:attribute name="style">width:530px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
										<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP5" /></xsl:attribute>
										<xsl:attribute name="name">PPROP5</xsl:attribute>
									</xsl:element>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:for-each>
						</TD>
					</TR>
					<TR>
						<TD class="normal">File Path:</TD>
						<TD align="left" colspan="3" class="NotesTabs">&#160;
							<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PORGFILENAME']">
								<xsl:choose>
									<xsl:when test="PLISTOFVALUES!=''">
										<script language="JavaScript">
											document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PORGFILENAME',''));
										</script>
									</xsl:when>
									<xsl:otherwise>
									<xsl:element name="input">
										<xsl:attribute name="disabled">True</xsl:attribute>
										<xsl:attribute name="type">text</xsl:attribute>
										<xsl:attribute name="style">width:530px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
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
						<!--<TD class="normal">Destination File Path:</TD>
						<TD align="left" class="NotesTabs">&#160;
									<xsl:element name="input">
										<xsl:attribute name="disabled">True</xsl:attribute>
										<xsl:attribute name="type">text</xsl:attribute>
										<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
										<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME" /></xsl:attribute>
										<xsl:attribute name="name">destcopy</xsl:attribute>
										<xsl:attribute name="onkeyup">JavaScript:if (typeof(document.ift.document.getElementById('hidBox')) != 'undefined'){document.ift.document.getElementById('hidBox').value = this.value;}</xsl:attribute>
									</xsl:element>
						</TD>-->

						<TD class="normal">Owning User:</TD>
						
						<xsl:choose>


						<!--    ******    Only Administrator can change Owner    -->
							<xsl:when test="//CURRENTUSER = 'gile' or //CURRENTUSER = 'yairf'">


								<TD align="left" class="NotesTabs" colspan="3">&#160;


									<SELECT disabled="true" id="PKEYUSER" name="PKEYUSER" style="width:170px">
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


							</xsl:when>

							<xsl:otherwise>


								<TD class="NotesTabs" align="left" colspan="3">&#160;
										<input disabled="true" type="text">
											<xsl:attribute name="value"><xsl:value-of select="//CURRENTUSER"/></xsl:attribute>
											<xsl:attribute name="name">PKEYUSER</xsl:attribute>
											<xsl:attribute name="style">color:gray;background-color:f6f6f6;WIDTH:170px</xsl:attribute>
											<xsl:attribute name="readonly">yes</xsl:attribute>
										</input>
								</TD>

							</xsl:otherwise>

				
						</xsl:choose>
						
					</TR>
				</xsl:for-each>
			</TABLE>

		</TD></TR></TBODY></TABLE>
</TD></TR>
<SCRIPT language="JavaScript">
	var keypl = getLoc(0);
	var parentpl = getLoc(1);
	var catalogpl = getLoc(2);
</SCRIPT>

<TR>
	<TD align="left" id="FileUpload" width="100%"></TD>
</TR>

</TABLE>
<!--<table align="center">
<tr><td align="center">
<xsl:choose>
	<xsl:when test="//NewDataSet/Catalogs/PLINKTONETWORKFILE='1'">
		<a target="_blank"><xsl:attribute name="href"><xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME"/></xsl:attribute><img src="WEImages/openfile.gif" border="0" alt="Open File" align="absmiddle"/><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></a>
	</xsl:when>
	<xsl:otherwise>
		<a target="_blank"><xsl:attribute name="href">/Catalog/Catalog/<xsl:value-of select="//NewDataSet/Catalogs/PKEYCATALOG"/>/Object/<xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH"/></xsl:attribute><img align="absmiddle" src="WEImages/openfile.gif" border="0" alt="Open File"/><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></a>
	</xsl:otherwise>
</xsl:choose>
</td></tr>
</table>-->
</form>

<script language="JavaScript">
	function uploadFile()	{
		window.open('../FileUpload.aspx','FileUpload','toolbar=no,width=300,height=70');
	}

	window.resizeTo('720','500');
	window.moveTo('20','20');
	if (getParamFromUrl('GroupDocument')=='True')
		document.getElementById('PPROP4').options[1].selected=true;
		
</script>
</div>
</body>
</html>
</xsl:template>
</xsl:stylesheet>