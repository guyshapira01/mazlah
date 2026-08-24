<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">
<xsl:template match="/">

<html>
	<base target="_parent"/>
	<LINK href="Style.css" rel="stylesheet" type="text/css" />
	<head>
		<title>Failure</title>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1255"/>
		<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
		<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />
		<LINK REL="stylesheet" HREF="WebEditor.css" TYPE="text/css" />
			<!-- <LINK REL="stylesheet" HREF="WebEditor.css" TYPE="text/css" />
			-->
			  <LINK REL="stylesheet" HREF="Styles/WebEditor-SteelBlue.css" TYPE="text/css" />

		<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
		<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
		<SCRIPT LANGUAGE="JavaScript" SRC="WebEditor.js"></SCRIPT>

	</head>
	<body class="BodyCatalog" dir="ltr" topmargin="20" leftmargin="20">
		<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%">
			<TR>
				<TD>
					<TABLE class="regularThin" border="0" cellPadding="0" cellSpacing="4" height="100%" width="98%">
			  			<TR>
							<TD width="10%" >
								<font style="color:black;font-size:16px;">Package &#160;</font>
							</TD>
							<TD nowrap="yes" class="NotesTabs_gray" width="18%" >
								<font style="color:red;font-weight:bold;font-size:14px">&#160;<xsl:value-of select="Items/Item/PPROP3"/></font>
							</TD>
							<TD nowrap="yes" class="NotesTabs_gray" width="60%"  colspan="2">
								<font style="color:red;font-weight:bold;font-size:14px">&#160;<xsl:value-of select="Items/Item/PHEBDESC"/></font>
							</TD>
			  			</TR>
						<TR>
							<TD ><font style="font-size:12px">Supplier</font></TD>
							<TD   align="left" class="NotesTabs_gray">&#160;
								<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP5"/> </font>
							</TD>
							<TD width="10%"><font style="font-size:12px">Price</font></TD>
							<TD  width="25%" align="left" class="NotesTabs_gray">&#160;
								<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP9"/> </font>
							</TD>
							<TD />
						</TR>
						<TR>
							<TD ><font style="font-size:12px">Stage</font></TD>
							<TD   align="left" class="NotesTabs_gray">&#160;
								<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP20"/> </font>
							</TD>
							<TD ><font style="font-size:12px">Date</font></TD>
							<TD  align="left" class="NotesTabs_gray">&#160;
								<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP26"/> </font>
							</TD>
							<TD />
						</TR>
			  			
					</TABLE>
				</TD>
			</TR>
			<TR>
				 <TD align="left" vAlign="top" width="100%">&#160;
				</TD>
			</TR>			
	 		<TR>
				<TD align="left" vAlign="top" width="100%">
					<TABLE  border="0" cellPadding="2" cellSpacing="0" id="idTabs"  width="98%">
			  			<TBODY>
			  				<TR height="25" vAlign="center">
	 							<!--<TD class="clsTab_Blank" width="1%">&#160;&#160;&#160;&#160;&#160;</TD>-->
								<TD class="clsTab" id="tabs" onclick="TabClick('0');" width="20%">
									<table>
										<tr>
											<td class="clsTab_Part">
												<a></a>
											</td>
											<td>
												<A href="javscript:void();" onclick="return TabClick('0');">
													<font style="font-size:16px">General</font>
												</A>
											</td>
										</tr>
									</table>
								</TD>
								<TD class="clsTab" id="tabs" onclick="TabClick('1');" width="20%">
									<table>
										<tr>
											<td class="clsTab_Part">
												<a></a></td><td>
												<A href="javscript:void();" onclick="return TabClick('1');">
													<font style="font-size:16px">Shipment Data</font>
												</A>
											</td>
										</tr>
									</table>
								</TD>
								<TD class="clsTab_Blank" id="tabs" width="100%">&#160;</TD>
			<!--				<TD class="clsTab_Blank" id="tabs" width="100%">&#160;</TD>
								<TD class="clsTab_Blank" id="tabs" width="100%">&#160;</TD>
			-->
							</TR>
						</TBODY>
					</TABLE> 
		  		<SCRIPT>tabs[2].width="100%"; idTabs.style.display="block";</SCRIPT>
				<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="98%">
					<TBODY>
						<TR>
							<TD width="5"></TD>
							<TD align="center" width="100%" class="clsTabSelected">
								<br></br>
								<!--Start Content of tab 1 -->
									<xsl:for-each select="Items/Item">
										<TABLE  border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
											<TR>
												<TD>
							<!--						<div align="center"><font style="font-size:18px;color:darkred">Corrective Action</font></div>
							-->

													<TABLE class="BG_Gradient" border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="100%">
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">
																	<TR>
																		<TD width="15%">
																			<INPUT type="checkbox" name="RIR" id="RIR">
																				<xsl:attribute name="disabled">true</xsl:attribute>
																				<xsl:choose>
																					<xsl:when match=".[//Items/Item/PPROP13='Yes']">
																						<xsl:attribute name="Checked">true</xsl:attribute>
																					</xsl:when>
																					</xsl:choose>
																			</INPUT>
																			<font style="font-size:12px">RIR</font>
																		</TD>
																		<TD width="7%"><font style="font-size:12px">Date</font></TD>
																		<TD width="25%" align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP25"/> </font>
																		</TD>
																		<TD />
																		
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">Remark</font></TD>
																		<TD align="left" class="NotesTabs_gray" colspan="3">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP19"/> </font>
																		</TD>
																	</TR>

																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>	
											<TR>
												<TD>
													<div align="center"><font style="font-size:18px;color:darkred">Non Conforming data</font></div>
							

													<TABLE class="BG_Gradient" border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="100%">
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">
																	<TR>
																		<TD width="20%">
																			<INPUT type="checkbox" name="Non-Conforming" id="Non-Conforming">
																				<xsl:attribute name="disabled">true</xsl:attribute>
																				<xsl:choose>
																					<xsl:when match=".[Items/Item/PPROP27!='']">
																						<xsl:attribute name="Checked">true</xsl:attribute>
																					</xsl:when>
																				</xsl:choose>
																				
																			</INPUT>
																			<font style="font-size:12px">Non-Conforming</font>
																		</TD>
																		<TD width="25%" align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP27"/> </font>
																		</TD>
																		<TD width="10%"><font style="font-size:12px">NC Stage</font></TD>
																		<TD width="25%" align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP24"/> </font>
																		</TD>
																		<TD />
																		
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">Activity</font></TD>
																		<TD align="left" class="NotesTabs_gray" colspan="1">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP28"/> </font>
																		</TD>
																			<TD align="left" class="NotesTabs_gray" colspan="3">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP29"/> </font>
																		</TD>
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">MRB / MR #</font></TD>
																		<TD align="left" class="NotesTabs_gray" colspan="1">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP30"/> </font>
																		</TD>
																		<TD><font style="font-size:12px">S/N</font></TD>
																			<TD align="left" class="NotesTabs_gray" colspan="1">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP21"/> </font>
																		</TD>
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">Remark</font></TD>
																		<TD align="left" class="NotesTabs_gray" colspan="10">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP23"/> </font>
																		</TD>
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">NC Status</font></TD>
																		<TD align="left" class="NotesTabs_gray" >&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP31"/> </font>
																		</TD>
																	</TR>

																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>					




										</TABLE>
									</xsl:for-each>
								<!--End Content of tab 1 -->
								<br></br>
							</TD>
						</TR>
					</TBODY>
				</TABLE>
				<SCRIPT>newsContent.style.display="none";</SCRIPT>
				<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="98%">
					<TBODY>
						<TR>
							<TD width="5"></TD>
							<TD width="100%" class="clsTabSelected">
								<br></br>
								<!--Start Content of tab 2 -->
									<xsl:for-each select="Items/Item">
										<TABLE  border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="90%">
											<TR>
												<TD>
													<div align="center"><font style="font-size:18px;color:darkred"></font></div>

													<TABLE class="BG_Gradient" border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="100%">
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">
																	<TR>
																		<TD width="15%"><font style="font-size:12px">Pallet ID</font></TD>
																		<TD width="20%" align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP22"/> </font>
																		</TD>
																		<TD width="15%"><font style="font-size:12px">Packing List</font></TD>
																		<TD width="20%" align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP6"/> </font>
																		</TD>
																			<TD width="15%"><font style="font-size:12px">Date</font></TD>
																		<TD width="20%" align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP8"/></font>
																		</TD>
																	</TR>
																	<TR>
																		<TD ><font style="font-size:12px">Shipment ID</font></TD>
																		<TD  align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP7"/> </font>
																		</TD>
																		<TD ><font style="font-size:12px">Contaner ID</font></TD>
																		<TD  align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP2"/> </font>
																		</TD>
																			<TD ><font style="font-size:12px">INDEX</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP4"/></font>
																		</TD>
																	</TR>
																	<TR>
																		<TD ><font style="font-size:12px">Status</font></TD>
																		<TD  align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP12"/> </font>
																		</TD>
																		<TD ><font style="font-size:12px">Remark</font></TD>
																		<TD  align="left" class="NotesTabs_gray" colspan="3">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP14"/> </font>
																		</TD>
																	</TR>
																	<TR>
																		<TD ><font style="font-size:12px">Date</font></TD>
																		<TD  align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP10"/> </font>
																		</TD>
																		<TD ><font style="font-size:12px"> Time</font></TD>
																		<TD  align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:value-of select="PPROP11"/> </font>
																		</TD>
																	</TR>
																	
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>


										</TABLE>
									</xsl:for-each>
								<!--End Content of tab 2 -->
								<br></br>
							</TD>
						</TR>
					</TBODY>
				</TABLE>
				<SCRIPT>newsContent[1].style.display="none";</SCRIPT>
				<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="100%">
					<TBODY>
						<TR>
							<TD width="5"></TD>
							<TD width="100%" class="clsTabSelected">
								<br></br>
									<!--Start Content of tab 3 -->
									<!--End Content of tab 3-->
								<br></br>
							</TD>
						</TR>
					</TBODY>
				</TABLE>
				<SCRIPT>newsContent[2].style.display="none";</SCRIPT>
			</TD>
		</TR>
	</TABLE>
	<SCRIPT>newsContent[0].style.display = "block";tabs[0].className = "clsTabSelected_Top";</SCRIPT>
	<xsl:choose>
		<xsl:when match=".[//IsWebEditor='True']">
			<!-- Start Part Toolbar -->
			<br/>
			<TABLE cellpadding="0" cellspacing="4" border="0" align="center" width="100%">
				<TR>
					<td  bgcolor="#d2ccb9" width="50"> 
						<table border="2" cellspacing="2" cellpadding="2">
							<tr>
								<td nowrap="yes">
									<a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Cut">
										<img src="WEImages/tbcut.gif" alt="Cut" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Copy">
										<img src="WEImages/tbcopy.gif" alt="Copy" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Paste">
										<img src="WEImages/tbpaste.gif" alt="Paste" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="JavaScript:UpdateObj('T_CAT_PART','Update_Material_Man_TAAS_E.xsl','true');void(0);" target="_self" title="Edit">
										<img src="WEImages/editgrid.gif" alt="Edit" border="0" width="16" height="16"/>
										Edit
									</a>
								</td>
							</tr>
						</table>
					</td>
		<!--
					<td  bgcolor="#d2ccb9" width="50"> 
						<table border="2" cellspacing="2" cellpadding="2">
							<tr>
								<td nowrap="yes">
									<a class="LW-toolbar" href="JavaScript:DelObj();void(0);" target="_self" title="Delete">
										<img src="WEImages/delete.gif" alt="Delete" border="0" width="16" height="16"/>
										Delete
									</a>
								</td>
							</tr>
						</table>
					</td>
		-->
					<td  bgcolor="#d2ccb9" width="50"> 
						<table border="2" cellspacing="2" cellpadding="2">
							<tr>
								<td nowrap="yes">
									<a class="LW-toolbar" target="_self" href="JavaScript:PermissionsWin('true');void(0);" title="Permissions">
										<img src="WEImages/permissions.gif" alt="Permissions" border="0" width="16" height="16"/></a></td><td class="LW-toolbar" nowrap="yes"><a class="LW-font" target="_self" href="JavaScript:PermissionsWin('true');void(0);" title="Permissions">
										Permissions 
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
		<!-- End Part Toolbar -->
	<!--
		<script language="JavaScript">
			putMailLink('<xsl:value-of select="//Items/Item/tagNamaID"/>','<xsl:value-of select="//Items/Item/PageHebDesc"/>');
		</script>
	-->
		<hr  size="5" clolor = "RED"/>
		<DIV align="right">
			<TABLE border="0" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr">
				<tr>
					<td align="center" colspan="6">
						<font class4="TitleText_E">Related Documents </font>
					</td>
				</tr>
				<tr>
					<xsl:choose>
						<xsl:when match=".[Items/Item/Documents/Document!='']">
							<TABLE class="regular"	border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="90%">
								<xsl:for-each select="Items/Item/Documents/Document">
									<xsl:if match=".[Doc_Group = 'CP']">
										<TR>
											<xsl:choose>
												<xsl:when match=".[//IsWebEditor='True']">
													<td width="1">
														<INPUT type="checkbox">
															<xsl:attribute name="name"><xsl:value-of select="DOCUMENTSYSTTEMID" /></xsl:attribute>
														</INPUT>
													</td>
												</xsl:when>
												<xsl:otherwise><td></td></xsl:otherwise>
											</xsl:choose>
											<TD width="10%" align="left" class="NotesTabs_White">&#160;
												<xsl:value-of select="Doc_Group"/>
											</TD>
											<TD width="19%" align="left" class="NotesTabs_White">&#160;
												<xsl:value-of select="DOCUMENTID"/>
											</TD>
											<TD width="39%" align="left" class="NotesTabs_White">
												<TABLE width="100%" cellPadding="0" cellSpacing="0" border="0">
													<TR>
														<TD width="10%">
															<img><xsl:attribute name="src"><xsl:value-of select="DocumentIcon"/></xsl:attribute></img>
														</TD>
														<TD width="90%">
															<a target="_blank">
																<xsl:attribute name="href"><xsl:value-of select="DOCUMENTPATH"/></xsl:attribute>
																<xsl:value-of select="DOCUMENTNAME" />
															</a>
															&#160;
														</TD>
													</TR>
												</TABLE>
											</TD>
											<TD width="25%" align="left" class="NotesTabs_White">&#160;
												<xsl:value-of select="DOCUMENTDESCRIPTION"/>
											</TD>
											<xsl:choose>
												<xsl:when match=".[//IsWebEditor='True']">
													<TD width="7%" align="left" class="NotesTabs_White">&#160;
														<a>
															<xsl:attribute name="href">JavaScript:w=window.open('ShowTemplate.aspx?Template=Winobject_Properties_E.xml&amp;Type=18&amp;Pkey=<xsl:value-of select="DOCUMENTSYSTTEMID" />&amp;ParentKey=<xsl:value-of select="DOC_ParentKey" />&amp;Table=T_CAT_WINOBJECT&amp;PkeyCatalog=<xsl:value-of select="//Items/PKEYCATALOG" />','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');w.focus();void(0);</xsl:attribute>
													<!--		
															<xsl:attribute name="href">JavaScript:w=window.open('updateObject.aspx?Object=<xsl:value-of select="DOCUMENTSYSTTEMID" />&amp;Table=T_CAT_WINOBJECT&amp;Template=WinObject_e.xsl','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');w.focus();void(0);</xsl:attribute>
													-->
															<xsl:attribute name="onMouseOver">window.status='More Information';return true;</xsl:attribute>
															<xsl:attribute name="onMouseOut">window.status='';</xsl:attribute>
															<img border="0" src="info.gif"/>
														</a>
													</TD>
												</xsl:when>
												<xsl:otherwise><td></td></xsl:otherwise>
											</xsl:choose>
										</TR>
									</xsl:if>
								</xsl:for-each>
								<xsl:for-each select="Items/Item/Documents/Document">
									<xsl:if match=".[Doc_Group != 'CP' and Doc_Group != 'CO']">
										<TR>
											<xsl:choose>
												<xsl:when match=".[//IsWebEditor='True']">
													<td width="1">
														<INPUT type="checkbox">
															<xsl:attribute name="name"><xsl:value-of select="DOCUMENTSYSTTEMID" /></xsl:attribute>
														</INPUT>
													</td> 
												</xsl:when>
												<xsl:otherwise><td></td></xsl:otherwise>
											</xsl:choose>
											<TD width="10%" align="left" class="NotesTabs_White">&#160;
												<xsl:value-of select="Doc_Group"/>
											</TD>
											<TD width="19%" align="left" class="NotesTabs_White">&#160;
												<xsl:value-of select="DOCUMENTID"/>
											</TD>
											<TD width="39%" align="left" class="NotesTabs_White">
												<TABLE width="100%" cellPadding="0" cellSpacing="0"  border="0">
													<TR valign="center">
														<TD width="10%" >
															<img><xsl:attribute name="src"><xsl:value-of select="DocumentIcon"/></xsl:attribute></img>
														</TD>
															<TD width="90%">
																<a target="_blank" >
																	<xsl:attribute name="href"><xsl:value-of select="DOCUMENTPATH"/></xsl:attribute> 
																	<xsl:value-of select="DOCUMENTNAME" />
																</a>
																&#160;
															</TD>
														</TR>
												</TABLE>
											</TD>
											<TD width="25%" align="left" class="NotesTabs_White">&#160;
												<xsl:value-of select="DOCUMENTDESCRIPTION"/>
											</TD>
											<xsl:choose>
												<xsl:when match=".[//IsWebEditor='True']">
													<TD width="7%" align="left" class="NotesTabs_White">&#160;
														<a>
															<xsl:attribute name="href">JavaScript:w=window.open('ShowTemplate.aspx?Template=Winobject_Properties_E.xml&amp;Type=18&amp;Pkey=<xsl:value-of select="DOCUMENTSYSTTEMID" />&amp;ParentKey=<xsl:value-of select="DOC_ParentKey" />&amp;Table=T_CAT_WINOBJECT&amp;PkeyCatalog=<xsl:value-of select="//Items/PKEYCATALOG" />','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');w.focus();void(0);</xsl:attribute>
									<!--					<xsl:attribute name="href">JavaScript:w=window.open('updateObject.aspx?Object=<xsl:value-of select="DOCUMENTSYSTTEMID" />&amp;Table=T_CAT_WINOBJECT&amp;Template=WinObject_e.xsl','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');w.focus();void(0);</xsl:attribute>
									-->
															<xsl:attribute name="onMouseOver">window.status='More Information';return true;</xsl:attribute>
															<xsl:attribute name="onMouseOut">window.status='';</xsl:attribute>
															<img border="0" src="info.gif"/>
														</a>
													</TD>
												</xsl:when>
												<xsl:otherwise><td></td></xsl:otherwise>
											</xsl:choose>
										</TR>
								</xsl:if>
							</xsl:for-each>
							<xsl:for-each select="Items/Item/Documents/Document">
								<xsl:if match=".[Doc_Group = 'CO']">
									<TR>
										<xsl:choose>
											<xsl:when match=".[//IsWebEditor='True']">
												<td width="1">
													<INPUT type="checkbox"><xsl:attribute name="name">
														<xsl:value-of select="DOCUMENTSYSTTEMID" /></xsl:attribute>
													</INPUT>
												</td> 
											</xsl:when>
											<xsl:otherwise><td></td></xsl:otherwise>
										</xsl:choose>
										<TD width="10%" align="left" class="NotesTabs_White">&#160;
											<xsl:value-of select="Doc_Group"/>
										</TD>
										<TD width="19%" align="left" class="NotesTabs_White">&#160;
											<xsl:value-of select="DOCUMENTID"/>
										</TD>
										<TD width="39%" align="left" class="NotesTabs_White">
											<TABLE width="100%" cellPadding="0" cellSpacing="0"  border="0">
												<TR valign="center">
													<TD width="10%" >
														<img><xsl:attribute name="src"><xsl:value-of select="DocumentIcon"/></xsl:attribute></img>
													</TD>
													<TD width="90%">
														<a target="_blank" >
															<xsl:attribute name="href">
																	<xsl:value-of select="DOCUMENTPATH"/>		
															</xsl:attribute> 
															<xsl:value-of select="DOCUMENTNAME" />
														</a>
														&#160;
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD width="25%" align="left" class="NotesTabs_White">&#160;
												<xsl:value-of select="DOCUMENTDESCRIPTION"/>
										</TD>
											<xsl:choose>
												<xsl:when match=".[//IsWebEditor='True']">
													<TD width="7%" align="left" class="NotesTabs_White">&#160;
														<a>
															<xsl:attribute name="href">JavaScript:w=window.open('ShowTemplate.aspx?Template=Winobject_Properties_E.xml&amp;Type=18&amp;Pkey=<xsl:value-of select="DOCUMENTSYSTTEMID" />&amp;ParentKey=<xsl:value-of select="DOC_ParentKey" />&amp;Table=T_CAT_WINOBJECT&amp;PkeyCatalog=<xsl:value-of select="//Items/PKEYCATALOG" />','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');w.focus();void(0);</xsl:attribute>
										<!--					<xsl:attribute name="href">JavaScript:w=window.open('updateObject.aspx?Object=<xsl:value-of select="DOCUMENTSYSTTEMID" />&amp;Table=T_CAT_WINOBJECT&amp;Template=WinObject_e.xsl','PopUp','scrollbars=no, toolbar, resizable, width=700, height=650');w.focus();void(0);</xsl:attribute>
										-->
															<xsl:attribute name="onMouseOver">window.status='More Information';return true;</xsl:attribute>
															<xsl:attribute name="onMouseOut">window.status='';</xsl:attribute>
															<img border="0" src="info.gif"/>
														</a>
													</TD>
												</xsl:when>
												<xsl:otherwise><td></td></xsl:otherwise>
											</xsl:choose>
									</TR>
								</xsl:if>
							</xsl:for-each>
						</TABLE>
					 </xsl:when>
					 <xsl:otherwise>
						<TABLE width="100%" border="0" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr" id="ItemDocuments">
							<tr valign="bottom" style="background-color:#9999ff;">
								<td align="center"><font color="Black" size="4">There are no Related Documents to this Change Request</font>&#160;</td>
							</tr>
						</TABLE>
					</xsl:otherwise>
				</xsl:choose>
			</tr>
		</TABLE>
		<!-- Start Document Toolbar -->
		<xsl:choose>
			<xsl:when match=".[//IsWebEditor='True']">
			<br/>
				<TABLE cellpadding="0" cellspacing="4" border="0" align="center" width="100%">
					<TR>
						<td  bgcolor="#d2ccb9" width="50"> 
							<table border="2" cellspacing="2" cellpadding="2">
								<tr>
									<td nowrap="yes">
										<a class="LW-toolbar" href="JavaScript:AddObj('New_WinObject_FileonServer_E.xsl');void(0);" target="_self" title="New">
											<img src="WEImages/newdoc.gif" alt="New" border="0" width="16" height="16"/>
											New
										</a>
									</td>
								</tr>
							</table>
						</td>
						<td  bgcolor="#d2ccb9" width="50"> 
						<table border="2" cellspacing="2" cellpadding="2">
							<tr>
								<td nowrap="yes">
									<a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Cut">
										<img src="WEImages/tbcut.gif" alt="Cut" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Copy">
										<img src="WEImages/tbcopy.gif" alt="Copy" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" title="Paste">
										<img src="WEImages/tbpaste.gif" alt="Paste" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="JavaScript:UpdateObj('T_CAT_WINOBJECT','Update_WinObject_E.xsl');void(0);" target="_self" title="Edit">
										<img src="WEImages/editgrid.gif" alt="Edit" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="JavaScript:DelObj();void(0);" target="_self" title="Delete">
										<img src="WEImages/delete.gif" alt="Delete" border="0" width="16" height="16"/>
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
									<a class="LW-toolbar" href="JavaScript:alert('Permission Denied');void(0);" target="_self" title="Delete">
										<img src="WEImages/delete.gif" alt="Delete" border="0" width="16" height="16"/>
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
			<!-- End Document Toolbar -->
		</DIV>
	</body>
</html>
</xsl:template>
</xsl:stylesheet>