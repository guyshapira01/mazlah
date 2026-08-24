<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:template match="/">

<html>
<head>
<title>Document</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css" />
<SCRIPT LANGUAGE="JavaScript" SRC="ToolBar.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<script language="JavaScript" src="Calendar.js"/>
<SCRIPT LANGUAGE="VbScript" SRC="webEditor.vbs"></SCRIPT>
</head>


<body  dir="ltr" topmargin="0" leftmargin="0" style="background-color:WhiteSmoke;">
<script LANGUAGE="JavaScript">
	//change the window size and position
	window.moveTo(30,30)
	window.resizeTo(480,445) 
</script>
<div width="100%" height="100%" style="background-color:WhiteSmoke;">
	<form name="NewObject" method="post">
	<input type="hidden">
		<xsl:attribute name="value">T_CAT_WINOBJECT</xsl:attribute>
		<xsl:attribute name="name">TABLE</xsl:attribute>
	</input>
	<input type="hidden">
		<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PKEY"/></xsl:attribute>
		<xsl:attribute name="name">PKEY</xsl:attribute>
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
<tr ><td colspan="4" align="center"  ><img border="0" align="absmiddle" src="images/info.jpg"></img>&#160;&#160;<font color="9b0000" style="FONT-SIZE:24px;font-weight:normal;">File Information</font></td></tr>
  <TR>
    <TD align="left" vAlign="top" width="100%">
  



<!--Start Content of tab 1 -->
<TABLE 	border="0" cellPadding="1" cellSpacing="0" align="center" dir="ltr" width="100%">
	<TR>
		<TD  class="TableHeader" nowrap="yes">Name:</TD>
		<TD  class="TableText">&#160;
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
				<xsl:attribute name="name">PHEBDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:290px</xsl:attribute>
				<xsl:attribute name="readonly">yes</xsl:attribute>
			</input>
		</TD>
	</TR>
	<TR>
		<TD width="20%" class="TableHeader"  nowrap="yes">Description:</TD>
		<TD class="TableText">&#160;
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PENGDESC"/></xsl:attribute>
				<xsl:attribute name="name">PENGDESC</xsl:attribute>
				<xsl:attribute name="style">WIDTH:290px</xsl:attribute>
				<xsl:attribute name="readonly">yes</xsl:attribute>
			</input>
		</TD>
	</TR>
	<xsl:for-each select="NewDataSet/Catalogs">
	
		<TR>
			<TD   class="TableHeader" width="30%">Create Date:</TD>
			<TD  align="left" class="TableText">&#160;
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
							<xsl:attribute name="style">width:290px;color:gray;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PCREATEDATE" /></xsl:attribute>
							<xsl:attribute name="name">PCREATEDATE</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
				</TD>
		
		</TR>

		<TR>
			<TD  class="TableHeader">Comment:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
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
							<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PCOMMENT" /></xsl:attribute>
							<xsl:attribute name="name">PCOMMENT</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>
			</TD>
		</TR>
		<!--%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%-->
		<TR>
		<TD width="20%" class="TableHeader"  nowrap="yes">Document Type:</TD>
		<TD class="TableText">&#160;
			<input type="text">
				<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP5"/></xsl:attribute>
				<xsl:attribute name="name">PPROP5</xsl:attribute>
				<xsl:attribute name="style">WIDTH:290px</xsl:attribute>
				<xsl:attribute name="readonly">yes</xsl:attribute>
			</input>
		</TD>
		</TR>
		
		<!--%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%-->
		<!--%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%-->
		<TR>
			<TD class="TableHeader">File Type:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
			
			<SELECT id="PKEYTYPE" name="PKEYTYPE" >
			<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
			<xsl:attribute name="disabled">true</xsl:attribute>
			<xsl:for-each select="//NewDataSet/ObjectType">
				<xsl:choose>
					<xsl:when test="CLASS='doc'">
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
		
		
		<!--%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%-->
		<TR>
			<TD class="TableHeader">Destination File Path:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"></xsl:attribute>
							<xsl:attribute name="name">destcopy</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="TableHeader">File Path:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
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
							<xsl:attribute name="style">color:gray;width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
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
			<TD class="TableHeader">Author:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PKEYUSER" /></xsl:attribute>
							<xsl:attribute name="name">PKEYUSER</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="TableHeader">Source:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP2" /></xsl:attribute>
							<xsl:attribute name="name">PPROP2</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="TableHeader">Language:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
					<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP10" /></xsl:attribute>
							<xsl:attribute name="name">PPROP10</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
						</xsl:element>
						
			</TD>
		</TR>
		<TR>
			<TD class="TableHeader">Year:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP3" /></xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
							<xsl:attribute name="name">PPROP3</xsl:attribute>
						</xsl:element>
			</TD>
		</TR>
		<TR>
			<TD class="TableHeader">Summary:</TD>
			<TD align="left" colspan="3" class="TableText">&#160;
						<xsl:element name="input">
							<xsl:attribute name="type">text</xsl:attribute>
							<xsl:attribute name="readonly">yes</xsl:attribute>
							<xsl:attribute name="style">width:290px;background-color:white;font-family:Arial;font-size:12;font-weight:normal</xsl:attribute>
							<xsl:attribute name="value"><xsl:value-of select="//NewDataSet/Catalogs/PPROP4" /></xsl:attribute>
							<xsl:attribute name="name">PPROP4</xsl:attribute>
						</xsl:element>
			</TD>
		</TR>
		

	</xsl:for-each>
</TABLE>

<!--End Content of tab 1 -->







<SCRIPT language="JavaScript">
	var keypl = getLoc(0);
	var parentpl = getLoc(1);
	var catalogpl = getLoc(2);
</SCRIPT>


	
</TD>
</TR>


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

</TABLE>
</form>



<script language="JavaScript">
	function uploadFile()	{
		window.open('../FileUpload.aspx','FileUpload','toolbar=no,width=300,height=30');
	}
</script>
</div>
</body>
</html>
</xsl:template>
</xsl:stylesheet>