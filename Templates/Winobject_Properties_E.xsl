<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:template match="/">

<html>
<!--<base target="_parent"/>-->
<LINK href="Style.css" rel="stylesheet" type="text/css" />
<head>
<title>Document</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />

<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
<SCRIPT LANGUAGE="VbScript" SRC="webEditor.vbs"></SCRIPT>


</head>

<body class="BodyCatalog" dir="ltr" topmargin="20" leftmargin="20">
<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%">
	<TR>
		<TD width="150" nowrap="yes">Document Name:</TD>
		<TD>
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/DOCUMENTNAME"/></xsl:attribute>
				<xsl:attribute name="name">DOCUMENTNAME</xsl:attribute>
				<xsl:attribute name="style">WIDTH:500px</xsl:attribute>
				<xsl:attribute name="readonly">Yes</xsl:attribute>
				<xsl:attribute name="disabled">True</xsl:attribute>
			</input>
		</TD>
	</TR>
	<TR>
		<TD width="150" nowrap="yes">Document Description:</TD>
		<TD>
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/DOCUMENTDESCRIPTION"/></xsl:attribute>
				<xsl:attribute name="name">DOCUMENTDESCRIPTION</xsl:attribute>
				<xsl:attribute name="style">WIDTH:500px</xsl:attribute>
				<xsl:attribute name="readonly">Yes</xsl:attribute>
				<xsl:attribute name="disabled">True</xsl:attribute>
			</input>
		</TD>
	</TR>
</TABLE>
<br/>
<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%">
  <TR>
    <TD align="left" vAlign="top" width="100%">
      <TABLE  border="0" cellPadding="2" cellSpacing="0" id="idTabs"  width="98%">
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


<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="98%"><TBODY>
<TR><TD width="5"></TD><TD align="center" width="100%" class="clsTabSelected">
<br></br>
<!--Start Content of tab 1 -->
<TABLE class="regular"	border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
	<xsl:for-each select="NewDataSet/Catalogs">
	
		<TR>
			<TD class="normal" width="20%">Create Date:</TD>
			<TD width="30%" align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTCREATEDATE" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTCREATEDATE</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
			<TD class="normal" width="20%">Update Date:</TD>
			<TD width="30%" align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTMODIFIEDATE" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTMODIFIEDATE</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>

		<TR>
			<TD class="normal">File Created:</TD>
			<TD align="left" colspan="1" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTFILECREATEDATE" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTFILECREATEDATE</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
			</xsl:element>
			</TD>
			<TD class="normal">File Modified:</TD>
			<TD align="left" colspan="1" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTFILEMODIFIEDDATE" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTFILEMODIFIEDDATE</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Status:</TD>
			<TD align="left" class="NotesTabs" nowrap="yes">&#160;
				<img width="16">
					<xsl:attribute name="src">IconTypes/<xsl:value-of select="STATUSNAME"/>.gif</xsl:attribute>
					<xsl:attribute name="alt"><xsl:value-of select="STATUSNAME"/></xsl:attribute>
				</img>
				&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/STATUSNAME" /></xsl:attribute>
					<xsl:attribute name="name">STATUSNAME</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>										
			</TD>
			<TD class="normal">Object Type:</TD>
			<TD align="left" class="NotesTabs" nowrap="yes">&#160;
				<img width="16">
					<xsl:attribute name="src">IconTypes/<xsl:value-of select="DOCUMENTOBJECTTYPE"/>.gif</xsl:attribute>
					<xsl:attribute name="alt"><xsl:value-of select="DOCUMENTOBJECTTYPE"/></xsl:attribute>
				</img>
				&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:130px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTOBJECTTYPE" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTOBJECTTYPE</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>										

			</TD>
		</TR>
		<TR>
			<TD class="normal">Reivion:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTREVISION" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTREVISION</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
			<TD class="normal">Print:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PDOCTEMPLATE" /></xsl:attribute>
					<xsl:attribute name="name">PDOCTEMPLATE</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Doc Number:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTNUMBER" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTNUMBER</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
			<TD class="normal">Suffix:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTSUFFIX" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTSUFFIX</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Publish:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<SELECT id="PPUBLISH" name="PPUBLISH" disabled="true">
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
					<OPTION value="">By Type</OPTION>
				</SELECT>
			</TD>
			<!--<TD class="normal">PublishRev:</TD>
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
							<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="PREVISION" /></xsl:attribute>
							<xsl:attribute name="name">PREVISION</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>-->
		</TR>
		<TR>
			<TD class="normal">Reference:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:445px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTCOMMENT" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTCOMMENT</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Original File Path:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:445px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PORGFILENAME" /></xsl:attribute>
					<xsl:attribute name="name">PORGFILENAME</xsl:attribute>
					<xsl:attribute name="readonly">yes</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
				</TD>
		</TR>
		<TR>
			<TD class="normal">Current File Path:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:445px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTPATH" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTPATH</xsl:attribute>
					<xsl:attribute name="readonly">yes</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
				</TD>
		</TR>
		<TR>
			<TD class="normal">System File Name:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH" /></xsl:attribute>
					<xsl:attribute name="name">PDOCPATH</xsl:attribute>
					<xsl:attribute name="readonly">yes</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
			<TD class="normal">Owning User:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTAUTHOR" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTAUTHOR</xsl:attribute>
					<xsl:attribute name="readonly">yes</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Network File:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:choose>
					<xsl:when test="//NewDataSet/Catalogs/PLINKTONETWORKFILE='1'"><img alt="File is managed in network." src="WEImages/network.gif" align="absmiddle"/>&#160;Network File</xsl:when>
					<xsl:otherwise><img src="WEImages/mycomputer.gif" alt="File is managed in Linkware volume" align="absmiddle"/>&#160;Local File.</xsl:otherwise>
				</xsl:choose>
			</TD>
			<TD class="normal">Check Out User:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTCHECKOUTUSER" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTCHECKOUTUSER</xsl:attribute>
					<xsl:attribute name="readonly">yes</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Document ID:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="readonly">Yes</xsl:attribute>
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTID" /></xsl:attribute>
					<xsl:attribute name="name">DOCUMENTID</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
			<TD class="normal">System ID:</TD>
			<TD align="left" class="NotesTabs">&#160;
				<xsl:element name="input">
					<xsl:attribute name="type">text</xsl:attribute>
					<xsl:attribute name="style">width:155px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
					<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PKEY" /></xsl:attribute>
					<xsl:attribute name="name">PKEY</xsl:attribute>
					<xsl:attribute name="readonly">yes</xsl:attribute>
					<xsl:attribute name="disabled">True</xsl:attribute>
				</xsl:element>
			</TD>
		</TR>

	</xsl:for-each>
</TABLE>

<!--End Content of tab 1 -->

<br></br></TD></TR></TBODY></TABLE>
<SCRIPT>newsContent.style.display="none";</SCRIPT>





<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="98%"><TBODY>
<TR><TD width="5"></TD><TD width="100%" class="clsTabSelected"><br></br>

<!--Start Content of tab 2 -->
<TABLE class="regular"	border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
	<xsl:for-each select="NewDataSet/Catalogs">
		<TR>
			<TD class="normal">Prop 1:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP1" /></xsl:attribute>
							<xsl:attribute name="name">PPROP1</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 2:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP2" /></xsl:attribute>
							<xsl:attribute name="name">PPROP2</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 3:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP3" /></xsl:attribute>
							<xsl:attribute name="name">PPROP3</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 4:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP4" /></xsl:attribute>
							<xsl:attribute name="name">PPROP4</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 5:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP5" /></xsl:attribute>
							<xsl:attribute name="name">PPROP5</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 6:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP6" /></xsl:attribute>
							<xsl:attribute name="name">PPROP6</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 7:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP7" /></xsl:attribute>
							<xsl:attribute name="name">PPROP7</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 8:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP8" /></xsl:attribute>
							<xsl:attribute name="name">PPROP8</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 9:</TD>
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
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP9" /></xsl:attribute>
							<xsl:attribute name="name">PPROP9</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<TR>
			<TD class="normal">Prop 10:</TD>
			<TD align="left" colspan="3" class="NotesTabs">&#160;
				<xsl:for-each select="//NewDataSet/DocumentLables[PKEY = 'PPROP10']">
					<xsl:choose>
						<xsl:when test="PLISTOFVALUES!=''">
							<script language="JavaScript">
								document.write(PutComboFromLOV('<xsl:value-of select="PLISTOFVALUES"/>','PPROP10','<xsl:value-of select="//NewDataSet/Catalogs/PPROP10" />'));
							</script>
						</xsl:when>
						<xsl:otherwise>
						<xsl:element name="input">
							<xsl:attribute name="readonly">Yes</xsl:attribute>
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:467px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP10" /></xsl:attribute>
							<xsl:attribute name="name">PPROP10</xsl:attribute>
							<xsl:attribute name="disabled">True</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
	
	</xsl:for-each>
</TABLE>

<!--End Content of tab 2 -->


<br></br></TD></TR></TBODY></TABLE>
<SCRIPT>newsContent[1].style.display="none";</SCRIPT>



<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="100%"><TBODY>
<TR><TD width="5"></TD><TD width="100%" class="clsTabSelected"><br></br>

<!--Start Content of tab 5 -->


<!--End Content of tab 5-->


<br></br></TD></TR></TBODY></TABLE>
<SCRIPT>newsContent[2].style.display="none";</SCRIPT>



</TD>
</TR>
<TR>
<!--	<TD align="left" id="FileUpload" width="100%"><a href="JavaScript:uploadFile()">Upload File</a>
	</TD>-->
</TR>
<TR>
	<TD align="center"><!--<input type="Submit" value="OK" style="BACKGROUND-COLOR:tan"/>--></TD>
</TR>
</TABLE><SCRIPT>newsContent[0].style.display = "block";tabs[0].className = "clsTabSelected_Top";</SCRIPT>
<div align="center">
		<a target="_blank"><xsl:attribute name="href"><xsl:value-of select="//NewDataSet/Catalogs/DOCUMENTPATH"/></xsl:attribute><img src="WEImages/openfile.gif" border="0" alt="Open File" align="absmiddle"/><xsl:value-of select="NewDataSet/Catalogs/DOCUMENTNAME"/></a>
<!--
<xsl:choose>
	<xsl:when test="//NewDataSet/Catalogs/PLINKTONETWORKFILE='1'">
		<a target="_blank"><xsl:attribute name="href"><xsl:value-of select="//NewDataSet/Catalogs/DOCORGFILENAME"/></xsl:attribute><img src="WEImages/openfile.gif" border="0" alt="Open File" align="absmiddle"/><xsl:value-of select="NewDataSet/Catalogs/DOCUMENTNAME"/></a>
	</xsl:when>
	<xsl:otherwise>
-->
		<!--Central Volume Mode --><!--<a target="_blank"><xsl:attribute name="href">/Catalog/Catalog/<xsl:value-of select="//NewDataSet/Catalogs/PKEYCATALOG"/>/Object/<xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH"/></xsl:attribute><img align="absmiddle" src="WEImages/openfile.gif" border="0" alt="Open File"/><xsl:value-of select="NewDataSet/Catalogs/DOCUMENTNAME"/></a>-->
		<!-- Per Catalog Volume Mode --><!--<a target="_blank"><xsl:attribute name="href">/Catalog/<xsl:value-of select="//NewDataSet/Catalogs/PKEYCATALOG"/>/Object/<xsl:value-of select="//NewDataSet/Catalogs/PDOCPATH"/></xsl:attribute><img align="absmiddle" src="WEImages/openfile.gif" border="0" alt="Open File"/><xsl:value-of select="NewDataSet/Catalogs/DOCUMENTNAME"/></a>-->
<!--
	</xsl:otherwise>
</xsl:choose>
-->
</div>
<hr  size="5" clolor = "RED"/>

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
</script>
</body>
</html>
</xsl:template>
</xsl:stylesheet>