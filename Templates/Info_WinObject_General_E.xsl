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
<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css" />
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
		<xsl:attribute name="value">T_CAT_WINOBJECT</xsl:attribute>
		<xsl:attribute name="name">TABLE</xsl:attribute>
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
		<xsl:attribute name="name">FileExists</xsl:attribute>
		<xsl:attribute name="value"></xsl:attribute>
	</input>
	<input type="Hidden">
		<xsl:attribute name="name">PUploadMode</xsl:attribute>
		<xsl:attribute name="value"></xsl:attribute>
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
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
				<xsl:attribute name="name">PHEBDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
				<xsl:attribute name="readonly">yes</xsl:attribute>
			</input>
		</TD>
	</TR>
	<TR>
		<TD width="150" nowrap="yes">&#160;&#160;Document Description:</TD>
		<TD>
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PENGDESC"/></xsl:attribute>
				<xsl:attribute name="name">PENGDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:350px</xsl:attribute>
				<xsl:attribute name="readonly">yes</xsl:attribute>
			</input>
		</TD>
	</TR>
</TABLE>
<br/>
<TABLE border="0" cellPadding="0" cellSpacing="10" width="100%">
  <TR>
    <TD align="left" vAlign="top" width="100%">
      <TABLE  border="0" cellPadding="0" cellSpacing="0" id="idTabs"  width="98%">
        <TBODY>
        <TR height="25" vAlign="center">
 <!--<TD class="clsTab_Blank" width="1%">&#160;&#160;&#160;&#160;&#160;</TD>-->
 <TD class="clsTab" id="tabs" onclick="TabClick('0');" width="20%"><table><tr><td class="clsTab_Part"><a></a></td><td><A href="javscript:void();"
   onclick="return TabClick('0');">General</A></td></tr></table></TD>
 <TD class="clsTab" id="tabs" onclick="TabClick('1');" width="20%"><table><tr><td class="clsTab_Part"><a></a></td><td><A href="javscript:void();"
   onclick="return TabClick('1');">Properties</A></td></tr></table></TD>
<TD class="clsTab_Blank" id="tabs" width="100%">&#160;</TD>
</TR></TBODY></TABLE> 

     <SCRIPT>tabs[2].width="100%"; idTabs.style.display="block";</SCRIPT>


<TABLE border="1" cellPadding="0" cellSpacing="0" id="newsContent" width="98%" style="BORDER-LEFT: #000000 1px solid;"><TBODY>
<TR><TD width="5"></TD><TD align="center" width="100%" class="clsTabSelected">

<!--Start Content of tab 1 -->
<TABLE class="regular"	border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
	<xsl:for-each select="NewDataSet/Catalogs">
	
		<TR>
			<TD class="normal" width="25%">Create Date:</TD>
			<TD width="55%" align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PCREATEDATE']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PCREATEDATE','<xsl:value-of select="//NewDataSet/Catalogs/PCREATEDATE" />'));
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
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PUPDATEDATE','<xsl:value-of select="//NewDataSet/Catalogs/PUPDATEDATE" />'));
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
			<TD class="normal" >File Created:</TD>
			<TD align="left" colspan="1" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PFILECREATEDDATE']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PFILECREATEDDATE','<xsl:value-of select="//NewDataSet/Catalogs/PFILECREATEDDATE" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
							<xsl:element name="input">
								<xsl:attribute name="type">text</xsl:attribute>
								<xsl:attribute name="style">width:135px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
								<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PFILECREATEDDATE" /></xsl:attribute>
								<xsl:attribute name="name">PFILECREATEDDATE</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
							</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
				<IMG id="PFILECREATEDDATECal" onclick="javascript:OpenCalendar('calendar_windowC','calendar.aspx?formname=NewObject.PFILECREATEDDATE&amp;cur=' + NewObject.PFILECREATEDDATE.value);"	height="16" src="calendar.gif" width="16" name="PFILECREATEDDATECal" onMouseOver="this.style.cursor='hand';" onMouseOut="JavaScript:this.style.cursor='auto';"/>
			</TD>
			<TD class="normal">File Modified:</TD>
			<TD align="left" colspan="1" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PFILEMODIFIEDDATE']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PFILEMODIFIEDDATE','<xsl:value-of select="//NewDataSet/Catalogs/PFILEMODIFIEDDATE" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:150px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PFILEMODIFIEDDATE" /></xsl:attribute>
							<xsl:attribute name="name">PFILEMODIFIEDDATE</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
				<IMG id="PFILEMODIFIEDDATECal" onclick="javascript:OpenCalendar('calendar_windowD','calendar.aspx?formname=NewObject.PFILEMODIFIEDDATE&amp;cur=' + NewObject.PFILEMODIFIEDDATE.value);"	height="16" src="calendar.gif" width="16" name="PFILEMODIFIEDDATECal" onMouseOver="this.style.cursor='hand';" onMouseOut="JavaScript:this.style.cursor='auto';"/>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Status:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<SELECT id="PSTATUS" name="PSTATUS">
				<xsl:attribute name="style">width:155px;</xsl:attribute>
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
					<OPTION>
						<xsl:choose>
							<xsl:when test="PKEY=//NewDataSet/Catalogs/PKEYTYPE">
								<xsl:attribute name="selected">True</xsl:attribute>
							</xsl:when>
						</xsl:choose>
						<xsl:attribute name="value"><xsl:value-of select="PKEY"/></xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						<xsl:value-of select="PTYPENAME"/>
					</OPTION>
				</xsl:for-each>
				</SELECT>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Revision:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PREVISION']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PREVISION','<xsl:value-of select="//NewDataSet/Catalogs/PREVISION" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
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
<!--			<TD class="normal">Print:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PDOCTEMPLATE']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PDOCTEMPLATE',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="PDOCTEMPLATE" /></xsl:attribute>
							<xsl:attribute name="name">PDOCTEMPLATE</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>-->
			<TD class="normal">Publish:</TD>
			<TD align="left" class="NotesTabs">&#160;
<!--				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPUBLISH']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPUBLISH',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="PPUBLISH" /></xsl:attribute>
							<xsl:attribute name="name">PPUBLISH</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
-->
				<SELECT id="PPUBLISH" name="PPUBLISH">
				<xsl:attribute name="readonly">yes</xsl:attribute>
				<xsl:attribute name="style">width:170px</xsl:attribute>
					<OPTION value="">By Type</OPTION>
					<OPTION>
						<xsl:choose>
							<xsl:when test="0=//NewDataSet/Catalogs/PPUBLISH">
								<xsl:attribute name="selected">True</xsl:attribute>
							</xsl:when>
						</xsl:choose>
						<xsl:attribute name="value">0</xsl:attribute>
						No
					</OPTION>
					<OPTION>
						<xsl:choose>
							<xsl:when test="1=//NewDataSet/Catalogs/PPUBLISH">
								<xsl:attribute name="selected">True</xsl:attribute>
							</xsl:when>
						</xsl:choose>
						<xsl:attribute name="value">1</xsl:attribute>
						<xsl:attribute name="selected">True</xsl:attribute>
						Yes
					</OPTION>
				</SELECT>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Doc Number:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PDOCNUMBER']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PDOCNUMBER','<xsl:value-of select="//NewDataSet/Catalogs/PDOCNUMBER" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PDOCNUMBER" /></xsl:attribute>
							<xsl:attribute name="name">PDOCNUMBER</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
			<TD class="normal">Suffix:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PSUFFIX']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PSUFFIX','<xsl:value-of select="//NewDataSet/Catalogs/PSUFFIX" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:170px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PSUFFIX" /></xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
							<xsl:attribute name="name">PSUFFIX</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
<!--		<TR>
			<TD class="normal">Publish:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPUBLISH']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPUBLISH',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="PPUBLISH" /></xsl:attribute>
							<xsl:attribute name="name">PPUBLISH</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
				<SELECT id="PPUBLISH" name="PPUBLISH">
					<OPTION value="">By Type</OPTION>
					<OPTION>
						<xsl:choose>
							<xsl:when test="0=//NewDataSet/Catalogs/PPUBLISH">
								<xsl:attribute name="selected">True</xsl:attribute>
							</xsl:when>
						</xsl:choose>
						<xsl:attribute name="value">0</xsl:attribute>
						No
					</OPTION>
					<OPTION>
						<xsl:choose>
							<xsl:when test="1=//NewDataSet/Catalogs/PPUBLISH">
								<xsl:attribute name="selected">True</xsl:attribute>
							</xsl:when>
						</xsl:choose>
						<xsl:attribute name="value">1</xsl:attribute>
						Yes
					</OPTION>
				</SELECT>
			</TD>
			<TD class="normal">PublishRev:</TD>
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
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="PREVISION" /></xsl:attribute>
							<xsl:attribute name="name">PREVISION</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>-->
		<TR>
			<TD class="normal">Comment:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PCOMMENT']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PCOMMENT','<xsl:value-of select="//NewDataSet/Catalogs/PCOMMENT" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:540px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PCOMMENT" /></xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
							<xsl:attribute name="name">PCOMMENT</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
			
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="type">hidden</xsl:attribute>
					<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PKEY" /></xsl:attribute>
					<xsl:attribute name="name">PKEY</xsl:attribute>
					<xsl:attribute name="readonly">yes</xsl:attribute>
					<xsl:attribute name="style">color:gray;</xsl:attribute>
				</xsl:element>
			</TD>
			
		</TR>
		<TR>
			<TD class="normal">File Path:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PORGFILENAME']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PORGFILENAME','<xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:505px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME" /></xsl:attribute>
							<xsl:attribute name="name">PORGFILENAME</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
				
				&#160;
				<xsl:choose>
						<xsl:when test="//NewDataSet/Catalogs/PLINKTONETWORKFILE!='0'">
							<xsl:element name="img">
							<xsl:attribute name="alt">File linked to network location</xsl:attribute>
							<xsl:attribute name="src">Images/uplink.gif</xsl:attribute>
							<xsl:attribute name="style">width:23px;height:23px;color:gray;</xsl:attribute>
							<xsl:attribute name="value"></xsl:attribute>
							<xsl:attribute name="name">linkedstatus</xsl:attribute>
						</xsl:element>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="img">
							<xsl:attribute name="alt">File uploaded to linkware volume</xsl:attribute>
							<xsl:attribute name="src">Images/vol.gif</xsl:attribute>
							<xsl:attribute name="style">width:23px;height:23px;color:gray;</xsl:attribute>
							<xsl:attribute name="value"></xsl:attribute>
							<xsl:attribute name="name">linkedstatus</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
					
			</TD>
		</TR>
		<TR>
			
			<TD width="25%" class="normal">Destination File Path:</TD>
			<TD width="25%" align="left" class="NotesTabs">&#160;
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"></xsl:attribute>
							<xsl:attribute name="name">destcopy</xsl:attribute>
							<xsl:attribute name="onkeyup">JavaScript:if (typeof(document.ift.document.getElementById('hidBox')) != 'undefined'){document.ift.document.getElementById('hidBox').value = this.value;}</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
			</TD>
			
			<TD width="25%">Language:</TD>
			<TD width="25%" align="left">&#160;
			
						<SELECT id="PPROP10" name="PPROP10">
						<xsl:attribute name="readonly">yes</xsl:attribute>
						<xsl:attribute name="style">width:170px</xsl:attribute>
		
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
							<OPTION value="Spanish">
								<xsl:choose>
									<xsl:when test="'Spanish'=//NewDataSet/Catalogs/PPROP10">
										<xsl:attribute name="selected">True</xsl:attribute>
									</xsl:when>
								</xsl:choose>
							Spanish</OPTION>
							<OPTION value="Italian">
								<xsl:choose>
									<xsl:when test="'Italian'=//NewDataSet/Catalogs/PPROP10">
										<xsl:attribute name="selected">True</xsl:attribute>
									</xsl:when>
								</xsl:choose>
							Italian</OPTION>
						</SELECT>
						
			</TD>
			
		</TR>
		<TR>
		<TD class="normal">System File Name:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PDOCPATH']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PDOCPATH','<xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH" /></xsl:attribute>
							<xsl:attribute name="name">PDOCPATH</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
							<xsl:attribute name="style">color:gray;</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
			<TD class="normal">Owning User:</TD>
			
			 <xsl:choose>


			 <!--    ******    Only Administrator can change Owner    -->
				<xsl:when test="//CURRENTUSER = 'gile' or //CURRENTUSER = 'yairf'">


					<TD align="left" class="NotesTabs" colspan="3">&#160;


						<SELECT id="PKEYUSER" name="PKEYUSER">
						<xsl:attribute name="style">width:170px</xsl:attribute>
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


					<TD class="NotesTabs" colspan="3" align="left">&#160;
							<input type="text">
								<xsl:attribute name="value"><xsl:value-of select="//CURRENTUSER"/></xsl:attribute>
								<xsl:attribute name="name">PKEYUSER</xsl:attribute>
								<xsl:attribute name="style">color:gray;background-color:f6f6f6;WIDTH:170px</xsl:attribute>
								<xsl:attribute name="readonly">yes</xsl:attribute>
							</input>
					</TD>

				</xsl:otherwise>

	
   </xsl:choose>
		</TR>
	<!--	<TR>
			<TD class="normal">Document ID:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PDOCID']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PDOCID',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="PDOCID" /></xsl:attribute>
							<xsl:attribute name="name">PDOCID</xsl:attribute>
							<xsl:attribute name="style">color:gray;</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>-->
<!--			<TD class="normal">System ID:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PKEY']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PKEY',''));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="PKEY" /></xsl:attribute>
							<xsl:attribute name="name">PKEY</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>-->
		<!--</TR>-->

	</xsl:for-each>
</TABLE>

<!--End Content of tab 1 -->

<br></br></TD></TR></TBODY></TABLE>
<SCRIPT>newsContent.style.display="none";</SCRIPT>





<TABLE border="1" cellPadding="0" cellSpacing="0" id="newsContent" width="98%" style="BORDER-LEFT: #000000 1px solid;"><TBODY>
<TR><TD width="5"></TD><TD width="100%" class="clsTabSelected"><br></br>

<!--Start Content of tab 2 -->
<TABLE border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
	<xsl:for-each select="NewDataSet/Catalogs">
		<TR>
			<TD class="normal" nowrap="yes">Prop 1:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP1']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP1','<xsl:value-of select="//NewDataSet/Catalogs/PPROP1" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP1" /></xsl:attribute>
							<xsl:attribute name="name">PPROP1</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 2:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP2']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP2','<xsl:value-of select="//NewDataSet/Catalogs/PPROP2" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP2" /></xsl:attribute>
							<xsl:attribute name="name">PPROP2</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 3:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP3']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP3','<xsl:value-of select="//NewDataSet/Catalogs/PPROP3" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP3" /></xsl:attribute>
							<xsl:attribute name="name">PPROP3</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 4:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP4']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP4','<xsl:value-of select="//NewDataSet/Catalogs/PPROP4" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP4" /></xsl:attribute>
							<xsl:attribute name="name">PPROP4</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 5:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP5']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP5','<xsl:value-of select="//NewDataSet/Catalogs/PPROP5" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP5" /></xsl:attribute>
							<xsl:attribute name="name">PPROP5</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 6:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP6']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP6','<xsl:value-of select="//NewDataSet/Catalogs/PPROP6" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP6" /></xsl:attribute>
							<xsl:attribute name="name">PPROP6</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 7:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP7']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP7','<xsl:value-of select="//NewDataSet/Catalogs/PPROP7" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP7" /></xsl:attribute>
							<xsl:attribute name="name">PPROP7</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 8:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP8']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP8','<xsl:value-of select="//NewDataSet/Catalogs/PPROP8" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP8" /></xsl:attribute>
							<xsl:attribute name="name">PPROP8</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal" nowrap="yes">Prop 9:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP9']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP9','<xsl:value-of select="//NewDataSet/Catalogs/PPROP9" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:500px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP9" /></xsl:attribute>
							<xsl:attribute name="name">PPROP9</xsl:attribute>
						<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
		</TR>
	
	</xsl:for-each>
</TABLE>

<!--End Content of tab 2 -->


</TD></TR></TBODY></TABLE>
<SCRIPT>newsContent[1].style.display="none";</SCRIPT>

<SCRIPT language="JavaScript">
	var keypl = getLoc(0);
	var parentpl = getLoc(1);
	var catalogpl = getLoc(2);
</SCRIPT>

<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="100%"><TBODY>
<TR><TD width="5"></TD><TD width="100%" class="clsTabSelected">

<!--Start Content of tab 5 -->


<!--End Content of tab 5-->


</TD></TR></TBODY></TABLE>
<SCRIPT>newsContent[2].style.display="none";</SCRIPT>

</TD>
</TR>
<TR>
	<TD align="left" id="FileUpload" width="100%"></TD>
</TR>
<TR>
	<TD align="left">
	</TD>
</TR>

</TABLE>
<table align="center">
<tr><td align="center">
<xsl:choose>
	<xsl:when test="//NewDataSet/Catalogs/PLINKTONETWORKFILE='1'">
		<a target="_blank"><xsl:attribute name="href"><xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME"/></xsl:attribute><img src="WEImages/openfile.gif" border="0" alt="Open File" align="absmiddle"/><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></a>
	</xsl:when>
	<xsl:otherwise>
		<!--Central Volume Mode --><a target="_blank"><xsl:attribute name="href">/Catalog/Catalog/<xsl:value-of select="//NewDataSet/Catalogs/PKEYCATALOG"/>/Object/<xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH"/></xsl:attribute><img align="absmiddle" src="WEImages/openfile.gif" border="0" alt="Open File"/><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></a>
	</xsl:otherwise>
</xsl:choose>
</td></tr>
</table><SCRIPT>newsContent[0].style.display = "block";tabs[0].className = "clsTabSelected_Top";</SCRIPT>
</form>

<!--<hr  size="5" clolor = "RED"/>-->

<!--<DIV align="CENTER">
This Page Update At:
<script language="javascript">
document.write(document.lastModified);
</script>
</DIV>-->
<script language="JavaScript">
	function uploadFile()	{
		window.open('../FileUpload.aspx','FileUpload','toolbar=no,width=300,height=70');
	}
	window.resizeTo('740','545');
</script>
</div>
</body>
</html>
</xsl:template>
</xsl:stylesheet>