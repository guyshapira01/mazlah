<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">

<html>
<LINK href="Style.css" rel="stylesheet" type="text/css" />
<head>
<title>Table of Parts</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
<LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />
<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
<!-- <LINK REL="stylesheet" HREF="WebEditor.css" TYPE="text/css" />
-->
  <LINK REL="stylesheet" HREF="Styles/WebEditor-SteelBlue.css" TYPE="text/css" />

<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="WebEditor.js"></SCRIPT>
<script language="VBSCRIPT" src="alerts.vbs"></script>
<SCRIPT LANGUAGE="javascript" SRC="../Reports/ReportFunctions.js"></SCRIPT>

	
</head>

<input type="hidden" name="pic_name" value=""></input><!--<# PICTURE_NAME ORDINAL=1 ENCAPS=' #>-->
<input type="hidden" name="map_name" value=""></input><!--<# IMAGEMAP_NAME ORDINAL=1 ENCAPS=' #>-->
<body class="BodyCatalog" dir="ltr">


	<table border="0" cellspacing="0" cellpadding="0" width="100%" >

		<TR>
			<TD  ALIGN="left" colspan="6"><a style="FONT-SIZE:12px; COLOR:black" href="VbScript:showClosedStatuses()" target="_self"><span id="txtShow" name="txtShow">+Show Closed</span>&#160;Tasks</a></TD>		
		</TR>


		<tr valign="bottom" style="background-color:#d2ccb9;font-weight:bold;">
			<xsl:choose>
				<xsl:when match=".[//IsWebEditor='True']">
					<td bgcolor="#c0c0c0" align="left" colspan="2" width="3%" nowrap="yes" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px"></FONT>
<!--							<a target="_self">
								<xsl:attribute name="href">../generatereport.aspx?Catalog=89824a1&amp;Table=T_CAT_PART&amp;XSL=TaskReport.xsl&amp;PKEYTYPE=33&amp;t_cat_part_father.pkey=<xsl:value-of select="Items/PAR_ID"/></xsl:attribute>
								<IMG border="0" src="CurrentTask.jpg" alt = "Current Folder Tasks" />
							</a>
-->
							<a>
								<xsl:attribute name="href">JavaScript:buildUrlWithBackInSameWindow('../generatereport.aspx?Catalog=89824a1&amp;Table=T_CAT_PART&amp;XSL=TaskReport.xsl&amp;PKEYTYPE=33&amp;t_cat_part_father.pkey=<xsl:value-of select="Items/PAR_ID"/>');void(0);</xsl:attribute>
								<IMG border="0" src="CurrentTask.jpg" alt = "Current Folder Tasks" />
							</a>	
							
							

					
					</td> 
				</xsl:when>
				<xsl:otherwise>				
					<td align="center" width="1" style="BORDER-BOTTOM: #333333 1px solid;BORDER-LEFT: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black">&#160;</FONT>
					</td>

				</xsl:otherwise>
			</xsl:choose>
			<td nowrap="yes" align="left" width="40%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Name&#160;</FONT></td>
			<td nowrap="yes" align="left" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Owner&#160;</FONT></td>
			<td nowrap="yes" align="left" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Due Date&#160;</FONT></td>
			<td nowrap="yes" align="left" width="10%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Status&#160;</FONT></td>
			<td nowrap="yes" align="left" width="5%" style="BORDER-BOTTOM: #333333 1px solid;BORDER-RIGHT: #333333 1px solid;BORDER-TOP: #333333 1px solid"><FONT color="black" style="FONT-SIZE:14px">&#160;Alert&#160;</FONT></td>



		</tr>
		<xsl:choose>
		    <xsl:when match=".[Items $eq$ '' and Items/Documents $eq$ '']">
		    	<tr valign="bottom" style="background-color:PaleGoldenrod;">
		    		<td colspan="7">No Objects Found</td>
		    	</tr>
		    </xsl:when>
		    <xsl:otherwise>
<!--				<script language="JavaScript">
					var i=0;
				</script>
-->
				<xsl:for-each select="Items/Item">
					<xsl:choose>
						<xsl:when match=".[PARTTYPENAME='Task']">
						</xsl:when>
						<xsl:otherwise>

	<!--					<script language="JavaScript">
							if (i%2==0)	{
								document.write(String.fromCharCode(60) + 'tr height="30" valign="middle" style="background-color:PaleGoldenrod;"' + String.fromCharCode(62));
							}
							else	{
								document.write(String.fromCharCode(60) + 'tr height="30" valign="middle" style="background-color:LightGoldenrodYellow;"' + String.fromCharCode(62));
							}
							</script>
	-->
						<tr>
								<xsl:choose>
									<xsl:when match=".[//IsWebEditor='True']">
										<td width="1">
											<INPUT type="checkbox" id="TaskFolder">
												<xsl:attribute name="name"><xsl:value-of select="ID" /></xsl:attribute>
												<xsl:attribute name="onclick">javaScript:CheckOnlyOne(this,'TaskFolder');
												</xsl:attribute>
											</INPUT>		
										</td>
									
									
							<!--		<td><INPUT type="checkbox" id="TaskFolder"><xsl:attribute name="name"><xsl:value-of select="ID" /></xsl:attribute><xsl:attribute name="id"><xsl:value-of select="PARTTYPENAME"/></xsl:attribute></INPUT></td> 
							-->
									</xsl:when>
									<xsl:otherwise></xsl:otherwise>
								</xsl:choose>
								<td width="1"><img><xsl:attribute name="src">IconTypes/<xsl:value-of select="PARTTYPENAME" />.gif</xsl:attribute></img>&#160;</td>
								<td>
									<xsl:choose>
										<xsl:when match=".[Category='Internal Tasks']">
												<xsl:attribute name="style">direction:rtl</xsl:attribute>			
										</xsl:when>
									</xsl:choose>			
								
									<a target="_top" class="DocumentName">
									<xsl:attribute name="href">
										<xsl:choose>
											<xsl:when match=".[//IsWebEditor='True']">JavaScript:top.frames('Index').LinkSwitch('<xsl:value-of select="ID" />','<xsl:value-of select="ParentKey" />');</xsl:when>
											<xsl:otherwise><xsl:value-of select="ID" />.html</xsl:otherwise>
										</xsl:choose>
									</xsl:attribute>
										<xsl:value-of select="PartName"/>
									</a>
								</td>
								<td>&#160;<xsl:value-of select="Owner"/></td>
								<td>&#160;</td>
								<td>&#160;</td>
								<td>&#160;</td>

							</tr>

				<!--		<script language="JavaScript">
								document.write(String.fromCharCode(60) + '/tr' + String.fromCharCode(62));
								i++;
							</script>
				-->
						</xsl:otherwise>
					</xsl:choose>
				</xsl:for-each>

				<xsl:for-each select="Items/Item">
					<xsl:choose>
						<xsl:when match=".[PARTTYPENAME='Task']">

	<!--					<script language="JavaScript">
							if (i%2==0)	{
								document.write(String.fromCharCode(60) + 'tr height="30" valign="middle" style="background-color:PaleGoldenrod;"' + String.fromCharCode(62));
							}
							else	{
								document.write(String.fromCharCode(60) + 'tr height="30" valign="middle" style="background-color:LightGoldenrodYellow;"' + String.fromCharCode(62));
							}
						</script>

	-->
							<tr>
								<xsl:choose>
									<xsl:when match=".[StatusName='Closed' or StatusName='Canceled']">
										<xsl:attribute name="id">StatusClosed</xsl:attribute>
										<xsl:attribute name="name">StatusClosed</xsl:attribute>
										<xsl:attribute name="style">display:none;</xsl:attribute>
									</xsl:when>
									<xsl:otherwise></xsl:otherwise>
								</xsl:choose>



								<xsl:choose>
									<xsl:when match=".[//IsWebEditor='True']">
									<td width="1">
										<INPUT type="checkbox" id="TaskTask">
											<xsl:attribute name="name"><xsl:value-of select="ID" /></xsl:attribute>
											<xsl:attribute name="onclick">javaScript:CheckOnlyOne(this,'TaskTask');
											</xsl:attribute>
										</INPUT>		
									</td>
							<!--		<td><INPUT type="checkbox" id="TaskTask"><xsl:attribute name="name"><xsl:value-of select="ID" /></xsl:attribute><xsl:attribute name="id"><xsl:value-of select="PARTTYPENAME"/></xsl:attribute></INPUT></td> 
							-->
									</xsl:when>
									<xsl:otherwise></xsl:otherwise>
								</xsl:choose>
								<td width="1"><img><xsl:attribute name="src">IconTypes/<xsl:value-of select="PARTTYPENAME" />.gif</xsl:attribute></img>&#160;</td>
								<td>
									<xsl:choose>
										<xsl:when match=".[Category='Internal Tasks']">
												<xsl:attribute name="style">direction:rtl</xsl:attribute>			
										</xsl:when>
									</xsl:choose>			
									<a target="_top" class="DocumentName">
									<xsl:attribute name="href">
										<xsl:choose>
											<xsl:when match=".[//IsWebEditor='True']">JavaScript:top.frames('Index').LinkSwitch('<xsl:value-of select="ID" />','<xsl:value-of select="ParentKey" />');</xsl:when>
											<xsl:otherwise><xsl:value-of select="ID" />.html</xsl:otherwise>
										</xsl:choose>
									</xsl:attribute>
										<xsl:value-of select="PartName"/>
									</a>
								</td>
								<td>&#160;<xsl:value-of select="Owner"/></td>
								<td nowrap="yes">&#160;<xsl:value-of select="DUEDATE"/></td>
								<td>&#160;<xsl:value-of select="StatusName"/></td>
								<TD>
									<xsl:attribute name="id">alertActivity<xsl:value-of select="ID"/></xsl:attribute>
									<script language="VBSCRIPT">
										DueDate = "<xsl:value-of select="DUEDATE"/>"
										currentDate= "<xsl:value-of select="//CurrentDate"/>"
										AlertDays = "<xsl:value-of select="ALERTDAYS"/>"
										TDName="<xsl:value-of select="ID"/>"
										CurrentStatus="<xsl:value-of select="StatusName"/>"
										If Not "<xsl:value-of select="StatusName"/>"="Closed" And Not "<xsl:value-of select="StatusName"/>"="Canceled" Then
											ShowAlert currentDate,DueDate,AlertDays,TDName,CurrentStatus
											document.write(Abs(DateDiff("d","<xsl:value-of select="//CurrentDate"/>","<xsl:value-of select="DUEDATE"/>")))
										End If

									</script>


								</TD>


			<!--					<script language="JavaScript">
									document.write(String.fromCharCode(60) + '/tr' + String.fromCharCode(62));
									i++;
								</script>

			-->
							</tr>
						</xsl:when>
					</xsl:choose>
			</xsl:for-each>
			

		    </xsl:otherwise>
		</xsl:choose>
	</table>

<xsl:choose>
	<xsl:when match=".[//IsWebEditor='True']">
<!-- Start Documents Toolbar -->
<!--	<TABLE class="LW-toolbar" cellpadding="2" cellspacing="0" border="0" align="center" width="80%">
-->
		<TABLE cellpadding="0" cellspacing="4" border="0" align="center" width="100%">
			<TR>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" href="JavaScript:AddObj('New_Folder_Task_E.xsl');void(0);" target="_self" title="New">
									<img src="IconTypes/Task Folder.gif" alt="New" border="0" align="absmiddle" width="16" height="16"/>
									New Folder
								</a>
							</td>
						</tr>
					</table>
				</td>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" href="JavaScript:AddObj('New_Item_Task_Taas_E.xsl');void(0);" target="_self" title="New">
									<img src="IconTypes/Task.gif" alt="New" border="0" align="absmiddle" width="16" height="16"/>
									New Task
								</a>
							</td>
						</tr>
					</table>
				</td>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" target="_self" href="javascript:CutObj();void(0);" title="Cut">
									<img src="WEImages/tbcut.gif" alt="Cut" align="absmiddle" border="0" width="16" height="16"/>
									Cut
								</a>
							</td>
						</tr>
					</table>
				</td>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" target="_self" href="javascript:CopyObj();void(0);" title="Copy">
									<img src="WEImages/tbcopy.gif" alt="Copy" align="absmiddle" border="0" width="16" height="16"/>
									Copy
								</a>
							</td>
						</tr>
					</table>
				</td>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" target="_self" href="javascript:PasteObj('|10|','','','');void(0);" title="Paste">
									<img src="WEImages/tbpaste.gif" alt="Paste" align="absmiddle" border="0" width="16" height="16"/>
									Paste
								</a>
							</td>
						</tr>
					</table>
				</td>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" href="JavaScript:if (getIDofSelectedCbox()=='TaskFolder') { UpdateObj('T_CAT_PART','Update_Folder_Task_E.xsl');void(0); };if (getIDofSelectedCbox()=='TaskTask') { UpdateObj('T_CAT_PART','Update_Item_Task_Taas_E.xsl');void(0); }" target="_self" title="Edit">
									<img src="WEImages/editgrid.gif" alt="Edit" align="absmiddle" border="0" width="16" height="16"/>
									Edit
								</a>
							</td>
						</tr>
					</table>
				</td>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" href="JavaScript:DelObj();void(0);" target="_self" title="Delete">
									<img src="WEImages/delete.gif" alt="Delete" align="absmiddle" border="0" width="16" height="16"/>
									Delete
								</a>
							</td>
						</tr>
					</table>
				</td>
				<td  bgcolor="#d2ccb9" width="50"> 
					<table border="2" cellspacing="2" cellpadding="2">
						<tr>
							<td nowrap="yes">
								<a class="LW-font" href="JavaScript:ReOrderObj(0);void(0);" target="_self" title="ReOrder">
									<img src="WEImages/reorder.gif" alt="Delete" align="absmiddle" border="0" width="16" height="16"/>
									ReOrder
								</a>
							</td>
						</tr>
					</table>
				</td>	
				<TD bgcolor="#d2ccb9">&#160;</TD>
			</TR>
		</TABLE>
		<br/><br/>
	</xsl:when>
</xsl:choose>
<!-- End Documents Toolbar -->


<BR/><hr  size="1" clolor = "RED"/>
<BR/>
<!--
	<DIV align="CENTER">
	This Page was Updated on:
	<script language="javascript">
	document.write(document.lastModified);
	</script>
	</DIV>
-->
</body>
</html>
</xsl:template>
</xsl:stylesheet>