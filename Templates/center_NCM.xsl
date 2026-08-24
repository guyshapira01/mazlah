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
			<LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css" />
			<LINK REL="stylesheet" HREF="tabs.css" TYPE="text/css" />
			<!-- <LINK REL="stylesheet" HREF="WebEditor.css" TYPE="text/css" />
			-->
			  <LINK REL="stylesheet" HREF="Styles/WebEditor-SteelBlue.css" TYPE="text/css" />

			<SCRIPT LANGUAGE="JavaScript" SRC="specialfx.js"></SCRIPT>
			<SCRIPT LANGUAGE="JavaScript" SRC="tabs.js"></SCRIPT>
			<SCRIPT LANGUAGE="JavaScript" SRC="WebEditor.js"></SCRIPT>
			<script language="VBSCRIPT" src="alerts.vbs"></script>
			<SCRIPT LANGUAGE="javascript" SRC="../Reports/ReportFunctions.js"></SCRIPT>
		</head>
		<input type="hidden" name="pic_name" value=""></input><!--<# PICTURE_NAME ORDINAL=1 ENCAPS=' #>-->
		<input type="hidden" name="map_name" value=""></input><!--<# IMAGEMAP_NAME ORDINAL=1 ENCAPS=' #>-->
		<body class="BodyCatalog" dir="ltr" topmargin="20" leftmargin="20">
			<TABLE border="0" cellPadding="0" cellSpacing="0" height="100%" width="100%">
				<TR>
					<TD align="left" vAlign="top" width="100%">
						<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="2" height="100%" width="100%">
							<TR>
								<TD nowrap="yes" width="10%">
									<font style="color:black;font-size:14px">Failure Name:</font>
								</TD>
								<TD nowrap="yes"  colspan="5" align="left" vAlign="top" bgcolor="d2ccb9">
										 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/Failure_Description"/></font>
								</TD>
								<TD nowrap="yes">
									<font style="color:black;font-size:14px">&#160;&#160;Follow up by:</font>
								</TD>
								<TD nowrap="yes"  colspan="1" align="left" vAlign="top" bgcolor="d2ccb9">
										 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/PPROP97"/></font>
								</TD>
							</TR>
							<TR>
								<TD nowrap="yes">
									<font style="color:black;font-size:14px">Owner:&#160;&#160;&#160;</font>
								</TD>
								<TD width="15%" nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									&#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/OwningUserName"/></font>
								</TD>
								
								<TD nowrap="yes" colspan="2">
									<font style="color:black;font-size:14px">&#160;&#160;MR No.</font>
								</TD>
								<TD  colspan="2" nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									<font style="color:blue;font-size:16px">&#160;<xsl:value-of select="Items/Item/PPROP95"/></font>
								</TD>
								<TD width="10%" nowrap="yes">
									<font style="color:black;font-size:14px">&#160;&#160;MRB No.</font>
								</TD>
								<TD width="20%" nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/PPROP96"/></font>
								</TD> 
								
	<!--
								<TD nowrap="yes" colspan="2">
									<font style="color:black;font-size:14px">&#160;&#160;Permitted:</font>
								</TD>
								<TD  colspan="2" nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									<font style="color:blue;font-size:16px">&#160;<xsl:value-of select="Items/Item/Permitted"/></font>
								</TD>
								<TD width="10%" nowrap="yes">
									<font style="color:black;font-size:14px">&#160;&#160;Group:</font>
								</TD>
								<TD width="20%" nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/ResponsibleGroup"/></font>
								</TD> 
-->								
								
							</TR>
							<TR>
								<TD nowrap="yes">
									<font style="color:black;font-size:14px">Failure Status:</font>
								</TD>
								<TD nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									&#160;<font style="color:red;font-size:16px"><xsl:value-of select="Items/Item/Failure_Status_Name"/></font>
								</TD>
								<TD width="5%" nowrap="yes">
									<font style="color:black;font-size:14px">&#160;&#160;by</font>
								</TD>
								<TD width="12%" nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									<font style="color:black;font-size:14px"><xsl:value-of select="Items/Item/ChangerStatusName"/></font>
								</TD>
								<TD width="5%" nowrap="yes">
									<font style="color:black;font-size:14px">&#160;&#160;on</font>
								</TD>
								<TD width="13%" nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">			
									&#160;<font style="font-size:12px"><xsl:value-of select="Items/Item/StatusDate"/></font>
								</TD>

								<TD nowrap="yes">
									<font style="color:black;font-size:14px">&#160;&#160;Platform:</font>
								</TD>
								<TD nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9">
									 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/PPROP51"/></font>
								</TD>     

							</TR>
							<xsl:choose>
								 <xsl:when match=".[Items/Item/PPROP77 $eq$ 'Yes']">
									<TR>
						<!--			<TD nowrap="yes" Style="border-top:black 3px solid;">
											<font style="color:black;font-size:14px">FAR:</font>
										</TD>
						-->
										<TD nowrap="yes" Style="border-top:black 3px solid;">
											<font style="color:black;font-size:14px">Preleminary FAR:</font>
										</TD>	
										<TD nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9" colspan="1" Style="border-top:black 3px solid;">
											 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/PPROP80"/></font>
										</TD> 
										<TD nowrap="yes" Style="border-top:black 3px solid;">
											<xsl:choose>
												 <xsl:when match=".[Items/Item/PPROP81 $ne$ '']">
													<input type="Button">
														<xsl:attribute name="name">URLPrelemFAR</xsl:attribute>
														<xsl:attribute name="value">open</xsl:attribute>
														<xsl:attribute name="style">width:40;font-weight:bold;color:blue;</xsl:attribute>
														<xsl:attribute name="onClick">JavaScript:w=window.open('<xsl:value-of select="Items/Item/PPROP81"/>');w.focus();</xsl:attribute>
													</input>	
												 </xsl:when>
											</xsl:choose>					
										</TD> 

										<TD nowrap="yes" Style="border-top:black 3px solid;">
											<font style="color:black;font-size:14px">Final FAR:</font>
										</TD>	
										<TD nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9" colspan="2" Style="border-top:black 3px solid;">
											 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/PPROP82"/></font>
						<!--			</TD> 
										<TD nowrap="yes" Style="border-top:black 3px solid;">
						-->
											<xsl:choose>
												 <xsl:when match=".[Items/Item/PPROP83 $ne$ '']">
													<input type="Button">
														<xsl:attribute name="name">URLPrelemFAR</xsl:attribute>
														<xsl:attribute name="value">open</xsl:attribute>
														<xsl:attribute name="style">width:40;font-weight:bold;color:blue;</xsl:attribute>
														<xsl:attribute name="onClick">JavaScript:w=window.open('<xsl:value-of select="Items/Item/PPROP83"/>');w.focus();</xsl:attribute>
													</input>	
												 </xsl:when>
											</xsl:choose>					
										</TD>     
										<TD nowrap="yes" Style="border-top:black 3px solid;">
											<font style="color:black;font-size:14px">&#160;&#160;Status</font>
										</TD>
										<TD nowrap="yes" align="left" vAlign="top" bgcolor="d2ccb9" colspan="2" Style="border-top:black 3px solid;">
											 &#160;<font style="color:blue;font-size:16px"><xsl:value-of select="Items/Item/PPROP84"/></font>
										</TD> 
									</TR>
								 </xsl:when>
							</xsl:choose>					
						</TABLE>
	   			</TD>
				</TR>
			<TR>
	    		<TD align="left" vAlign="top" width="100%">&#160;
	   		</TD>
			</TR>
	 		<TR>
	    		<TD align="left" vAlign="top" width="100%">
					<TABLE  border="0" cellPadding="2" cellSpacing="0" id="idTabs"  width="100%">
						<TBODY>
							<TR height="25" vAlign="center">
								<TD class="clsTab" id="tabs" onclick="TabClick('0');" width="20%">
								 	<table>
								 		<tr>
								 			<td class="clsTab_Part">
												<a>
													<xsl:if match=".[Items/Item/Failure_Status_Name $eq$ 'New']">
														<xsl:attribute name="style">COLOR:darkred</xsl:attribute>
													</xsl:if>
													<font style="font-size:14px">A-</font>
												</a>
											</td>
											<td>
												<A href="javscript:void();" onclick="return TabClick('0');">
													<font style="font-size:14px">Identification</font>
												</A>
											</td>
										</tr>
									</table>
								</TD>
								 <TD class="clsTab" id="tabs" onclick="TabClick('1');" width="20%">
								 	<table>
								 		<tr>
								 			<td class="clsTab_Part">
												<a>
													<xsl:if match=".[Items/Item/Failure_Status_Name $eq$ 'Analyzing']">
														<xsl:attribute name="style">COLOR:RED</xsl:attribute>
													</xsl:if>
													<font style="font-size:14px">B-</font>
												</a>
											</td>
										<td>
											<A href="javscript:void();" onclick="return TabClick('1');">
												<font style="font-size:14px">Analysis</font>
											</A>
										</td>
									</tr>
								</table>
								</TD>
								<TD class="clsTab_Blank" id="tabs" width="100%" align="center">&#160;
									<xsl:choose>
										 <xsl:when match=".[Items/Item/PPROP77 $eq$ 'Yes']">
											<font style="FONT-SIZE:20px;FONT-WEIGHT:BOLD;COLOR:darkred">FAR</font>
										 </xsl:when>
									</xsl:choose>					
								</TD>
							</TR>
						</TBODY>
					</TABLE> 
					<SCRIPT>tabs[2].width="100%"; idTabs.style.display="block";</SCRIPT>
					<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="100%">
						<TBODY>
							<TR>
								<TD width="5"></TD>
								<TD align="center" width="100%" class="clsTabSelected">
								<!--Start Content of tab 1 -->
									<div align="center"><font style="font-size:18px;color:darkred">Failure Identification</font></div>

									<TABLE class="BG_Gradient"	border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="97%">
									  <xsl:for-each select="Items/Item">
										<TR>
											<TD>
												<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">

													<TR>
														<TD width="30%">Failure Identification:</TD>
														<TD width="25%" align="left" class="NotesTabs_gray">&#160;

															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP43"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD width="15%">Failure Date:</TD>
														<TD width="30%" align="left" class="NotesTabs_gray">

															<xsl:attribute name="id">alertFailure<xsl:for-each select="ID"> <xsl:value-of /></xsl:for-each></xsl:attribute>
															<script language="VBSCRIPT">
																FailureDate="<xsl:for-each select="PPROP45"><xsl:value-of/></xsl:for-each>"
																currentDate="<xsl:value-of select="//CurrentDate"/>"
																AlertDays = 0    <!-- not used -->
																TDName="<xsl:for-each select="ID"><xsl:value-of/></xsl:for-each>"
																If Not "<xsl:value-of select="Failure_Status_Name"/>"="Closed" And Not "<xsl:value-of select="Failure_Status_Name"/>"="Canceled" And Not "<xsl:value-of select="Items/Item/Failure_Status_Name"/>"="Finished" Then						
																	ShowAlertFailure currentDate,FailureDate,AlertDays,TDName
																End If
															</script>

															<font style="font-size:17px"><xsl:for-each select="PPROP45"> <xsl:value-of /></xsl:for-each></font>
														</TD>
													</TR>

													<TR>
														<TD>Identification of the item:</TD>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP47"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD>Subsystem:</TD>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP40"> <xsl:value-of /></xsl:for-each></font>
														</TD>

													</TR>
													<TR>
														<TD>P/N:</TD>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP48"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD>S/N:</TD>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP42"> <xsl:value-of /></xsl:for-each></font>
														</TD>
													</TR>
													<TR>
														<TD colspan="1">In which WS the failure occurred:</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP44"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD colspan="1">LRU/ASSY:</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP50"> <xsl:value-of /></xsl:for-each></font>
														</TD>

													</TR>

													<TR>
														<TD colspan="1">Activity and when then failure occurred:</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP46"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD colspan="1">Tank No.</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP88"> <xsl:value-of /></xsl:for-each></font>
														</TD>

													</TR>
													<TR>
														<TD colspan="1">Material Status:</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP91"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD colspan="1">Hull No.</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP89"> <xsl:value-of /></xsl:for-each></font>
														</TD>

													</TR>
													<TR>
														<TD colspan="1">Report Level:</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP92"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD colspan="1">Turret No:</TD>
														<TD align="left" colspan="1" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP90"> <xsl:value-of /></xsl:for-each></font>
														</TD>

													</TR>
													<TR>
														<TD colspan="1">Stage</TD>
														<TD align="left" colspan="3" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP93"> <xsl:value-of /></xsl:for-each></font>
														</TD>
													</TR>
													<TR>
														<TD colspan="1">Description of the failure:</TD>
														<TD align="left" colspan="3" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP3"> <xsl:value-of /></xsl:for-each></font>
														</TD>
													</TR>

										<!--		<TR>
														<TD colspan="4" class="normal"></TD>
													</TR>
										-->			
												</TABLE>
											</TD>
										</TR>

										<TR>
											<TD>
												<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="100%">

													<TR>
														<TD colspan="4">&#160;&#160;The person who report the failure:</TD>
													</TR>
													<TR>
														<TD colspan="4" class="normal"></TD>
													</TR>

													<TR>
														<TD align="left" width="30%">
															&#160;&#160;Name
														</TD>
														<TD align="left" width="25%">
															Position
														</TD>
														<TD align="left" width="18%">
															Phone
														</TD>
														<TD align="left" width="27%">
															Other persons present
														</TD>
													</TR>
													<TR>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:12px"><xsl:for-each select="PPROP4"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:12px"><xsl:for-each select="PPROP5"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:12px"><xsl:for-each select="PPROP6"> <xsl:value-of /></xsl:for-each></font>
														</TD>
														<TD align="left" class="NotesTabs_gray">&#160;
															<font style="color:darkblue;font-size:12px"><xsl:for-each select="PPROP8"> <xsl:value-of /></xsl:for-each></font>
														</TD>
													</TR>

												</TABLE>
											</TD>
										</TR>


									  </xsl:for-each>
									</TABLE>
							<!--End Content of tab 1 -->
								</TD>
							</TR>
						</TBODY>
					</TABLE>
					<SCRIPT>newsContent.style.display="none";</SCRIPT>
					<TABLE border="0" cellPadding="0" cellSpacing="0" id="newsContent" width="100%">
						<TBODY>
							<TR>
								<TD width="5"></TD>
								<TD width="100%" class="clsTabSelected">
							<!--Start Content of tab 2 -->
									<xsl:for-each select="Items/Item">
										<TABLE  border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="97%">
											<TR>
												<TD>
													<div align="center"><font style="font-size:18px;color:darkred">Failure Analysis and Repair</font></div>

													<TABLE class="BG_Gradient" border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="100%">
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">
																	<TR>
																		<TD width="30%"><font style="font-size:12px">Root Cause of This Failure:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP9"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">Repair of Demaged Items:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP11"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">


																	<TR>
																		<TD width="30%"><font style="font-size:12px">Repaired - Reported by:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP15"> <xsl:value-of /></xsl:for-each></font>
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
													<div align="center"><font style="font-size:18px;color:darkred">Decision and Corrective Action</font></div>

													<TABLE class="BG_Gradient" border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="100%">
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">
																	<TR>
																		<TD width="30%"><font style="font-size:12px">Decision:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP94"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
																	</TR>


																	<TR>
																		<TD width="30%"><font style="font-size:12px">Corrective Action to Prevent Re-occurrence:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP12"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">Verify Effectiveness  of Corrective Action:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP34"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
																	</TR>
																	<TR>
																		<TD><font style="font-size:12px">Repeated Tests:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP78"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
																	</TR>

																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">

																	<TR>
																		<TD width="30%"><font style="font-size:12px">Correcton Action - Reported by:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP27"> <xsl:value-of /></xsl:for-each></font>
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
													<div align="center"><font style="font-size:18px;color:darkred">Failure Closing</font></div>

													<TABLE class="BG_Gradient" border="0" cellPadding="0" cellSpacing="0" align="center" dir="ltr" width="100%">
														<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">
																	<TR>
																		<TD width="30%"><font style="font-size:12px">Closing Remark:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="ClosingRemark"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
															<!--		<TD width="5%"><font style="font-size:12px">FAR:</font></TD>
																		<TD  width="5%" align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="PPROP77"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
															-->			
																	</TR>

																</TABLE>
															</TD>
														</TR>
													<TR>
															<TD>
																<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="5" align="center" dir="ltr" width="100%">

																	<TR>
																		<TD width="30%"><font style="font-size:12px">Finished by Owner:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="ClosingbyOwnerName"> <xsl:value-of /></xsl:for-each></font>
																		</TD>
																	</TR>
																	<TR>
																		<TD colspan="2" class="normal"></TD>
																	</TR>
																	<TR>
																		<TD width="30%"><font style="font-size:12px">Closed by SE / QA:</font></TD>
																		<TD align="left" class="NotesTabs_gray">&#160;
																			<font style="color:darkblue;font-size:17px"><xsl:for-each select="ClosingbySEName"> <xsl:value-of /></xsl:for-each></font>
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
									<!-- Start Content of tab 3 -->

									<!--End Content of tab 3 -->
								</TD>
							</TR>
						</TBODY>
					</TABLE>
					<SCRIPT>newsContent[2].style.display="none";</SCRIPT>
				</TD>
			</TR>
			<SCRIPT>newsContent[0].style.display = "block";tabs[0].className = "clsTabSelected_Top";</SCRIPT>
			<TR>
				<TD align="left" style="BORDER-LEFT: black 1px solid;BORDER-BOTTOM: black 1px solid;">
				<xsl:choose>
					 <xsl:when match=".[Items/Item/Remark1 $ne$ '' or Items/Item/Remark2 $ne$ ''  or Items/Item/Remark3 $ne$ ''  or Items/Item/Remark4 $ne$ ''  or Items/Item/Remark5 $ne$ ''  or Items/Item/Remark6 $ne$ '' ]">
					<TABLE width="100%" cellPadding="4" cellSpacing="4" bgcolor="#7288AC">

						<TR height="10">
							<TD Align="Center" width="10%"><font style="FONT-SIZE:14px;FONT-WEIGHT:BOLD"><u>Date</u></font></TD>
							<TD Align="left" width="90%"><font style="FONT-SIZE:14px;FONT-WEIGHT:BOLD"><u>Follow up Remarks</u></font></TD>
						</TR>

						<xsl:choose>
							 <xsl:when match=".[Items/Item/Remark1 $ne$ '']">
							<TR>
								<TD  class="NotesTabs_gray" align="center"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/RemarkDate1"/></font></TD>
								<TD class="NotesTabs_gray" style="direction:rtl" align="left"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/Remark1"/></font></TD>
							</TR>
							 </xsl:when>
						</xsl:choose>

						<xsl:choose>
							 <xsl:when match=".[Items/Item/Remark2 $ne$ '']">
							<TR>
								<TD  class="NotesTabs_gray" align="center"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/RemarkDate2"/></font></TD>
								<TD class="NotesTabs_gray" style="direction:rtl" align="left"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/Remark2"/></font></TD>
							</TR>
							 </xsl:when>
						</xsl:choose>
						<xsl:choose>
							 <xsl:when match=".[Items/Item/Remark3 $ne$ '']">
							<TR>
								<TD class="NotesTabs_gray" align="center"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/RemarkDate3"/></font></TD>
								<TD class="NotesTabs_gray" style="direction:rtl" align="left"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/Remark3"/></font></TD>
							</TR>
							 </xsl:when>
						</xsl:choose>
						<xsl:choose>
							 <xsl:when match=".[Items/Item/Remark4 $ne$ '']">
							<TR>
								<TD class="NotesTabs_gray" align="center"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/RemarkDate4"/></font></TD>
								<TD class="NotesTabs_gray" style="direction:rtl" align="left"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/Remark4"/></font></TD>
							</TR>
							 </xsl:when>
						</xsl:choose>
						<xsl:choose>
							 <xsl:when match=".[Items/Item/Remark5 $ne$ '']">
							<TR>
								<TD class="NotesTabs_gray" align="center"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/RemarkDate5"/></font></TD>
								<TD class="NotesTabs_gray" style="direction:rtl" align="left"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/Remark5"/></font></TD>
							</TR>
							 </xsl:when>
						</xsl:choose>
						<xsl:choose>
							 <xsl:when match=".[Items/Item/Remark6 $ne$ '']">
							<TR>
								<TD class="NotesTabs_gray" align="center"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/RemarkDate6"/></font></TD>
								<TD class="NotesTabs_gray" style="direction:rtl" align="left"><font style="FONT-SIZE:14px;FONT-WEIGHT:NORMAL"><xsl:value-of select="Items/Item/Remark6"/></font></TD>
							</TR>
							 </xsl:when>
						</xsl:choose>
						<TR><TD></TD></TR>
						<TR><TD></TD></TR>

					</TABLE>
					 </xsl:when>
					 <xsl:otherwise>
					<TABLE width="100%" cellPadding="4" cellSpacing="4" bgcolor="#7288AC">
						<TR><TD></TD></TR>
						<TR><TD></TD></TR>

					</TABLE>


					 </xsl:otherwise>
				</xsl:choose>
				</TD>
			</TR>
		</TABLE>
		<!-- Start Failure Toolbar -->
		<xsl:choose>
			<xsl:when match=".[//IsWebEditor='True']">
				<br/>
				<TABLE cellpadding="0" cellspacing="4" border="0" align="center" width="100%">
					<TR>
				<!--	<td  bgcolor="#d2ccb9" width="50"> 
							<table border="2" cellspacing="2" cellpadding="2">
								<tr>
									<td nowrap="yes">
											<a class="LW-toolbar" href="javascript:alert('Functionality Not Available Yet');void(0);" target="_self" title="New">
												<img src="WEImages/newdoc.gif" alt="New" border="0" width="16" height="16"/>
												New
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
										<a class="LW-toolbar" href="javascript:CutObj('true');void(0);" title="Cut" target="_self">
											<img src="WEImages/tbcut.gif" alt="Cut" border="0" width="16" height="16"/>
											Cut
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
										<a class="LW-toolbar" href="javascript:CopyObj('true');void(0);" title="Copy" target="_self">
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
										<a class="LW-toolbar" href="javascript:PasteObj('|10|','','','');void(0);" title="Paste" target="_self">
											<img src="WEImages/tbpaste.gif" alt="Paste" border="0" width="16" height="16"/>
											Paste
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
											<a class="LW-toolbar" href="JavaScript:UpdateObj('T_CAT_PART','Update_NCM_TAAS_E.xsl','true');void(0);" target="_self" title="Edit">
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
										<a id="MailLink" class="LW-font" title="Mail">
											<img src="WEImages/mail.gif" alt="Mail" border="0" width="16" height="16"/>
											Mail
										</a>
									</td>
								</tr>
							</table>
						</td>
						<td  bgcolor="#d2ccb9" width="50"> 
							<table border="2" cellspacing="2" cellpadding="2">
								<tr>
									<td nowrap="yes">
										<a target="_self" class="LW-toolbar" href="JavaScript:printPage('Center_NCM_Print.xsl','TSGa6615');void(0);" title="Print">
											<img src="WEImages/print.gif" alt="Print" border="0" width="16" height="16"/>
											Print
										</a>
									</td>
								</tr>
							</table>
						</td>
						<td  bgcolor="#d2ccb9" width="50"> 
							<table border="2" cellspacing="2" cellpadding="2">
								<tr>
									<td nowrap="yes">
										<a class="LW-toolbar" title="FAR Data">
											<xsl:attribute name="href">
												JavaScript:w=window.open('../generatereport.aspx?Catalog=TSGa6615&amp;Table=T_CAT_PART&amp;XSL=Center_NCM_FAR.xsl&amp;PKEY=<xsl:value-of select="Items/Item/ID"/>','FailureReport','width=750,height=710,toolbar=no,scrollbars=yes');w.focus();void(0);
											</xsl:attribute>	
											<IMG border="0" src="WEImages/Far-Small.jpg" alt = "FAR Form" /> 
											FAR Data
										</a>				
									</td>
								</tr>
							</table>
						</td>	
						<TD bgcolor="#d2ccb9">&#160;</TD>
		<!--     ***** only for Gidehon     *****   -->
<!--
				<xsl:choose>
					 <xsl:when match=".[Items/UserLogon $eq$ 'gideonh']">


						<td class="LW-toolbar"> 
								<table border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td class="LW-toolbar" nowrap="yes">
											<a class="LW-toolbar" href="JavaScript:UpdateObj('T_CAT_PART','Update_Failure_TAAS_E_For upgrade_form.xsl','true');void(0);" target="_self" title="Upgrade Data">
												<img src="WEImages/editgrid.gif" alt="Upgrade Data" border="0" width="16" height="16"/>
											</a>
										</td>
										<td class="LW-toolbar" nowrap="yes">
											<a class="LW-font" href="JavaScript:UpdateObj('T_CAT_PART','Update_Failure_TAAS_E_For upgrade_form.xsl','true');void(0);" target="_self" title="Upgrade Data"> Upgrade Data </a>
										</td>
									</tr>
								</table>
						</td>
					 </xsl:when>				
				</xsl:choose>
-->
					</TR>
				</TABLE>
				<br/><br/>
			</xsl:when>
		</xsl:choose>
		<!-- End Failure Toolbar -->
		<script language="JavaScript">
			putMailLink('Failure: ' + '<xsl:value-of select="//Items/Item/PPROP43"/>','<xsl:value-of select="//Items/Item/PageHebDesc"/>');
		</script>
		<hr  size="5" clolor = "RED"/>
		<TABLE border="0" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr" id="ItemDocuments" width="100%">
			<tr>
				<td width="35%">&#160;</td>
				<td align="center" widht="30%" nowrap="yes">

				<font class="TitleText_E">Failure Documents</font>
				<HR/>
				</td>
				<td width="35%">&#160;</td>

			</tr>
			<tr>
				<td colspan="3">
					<xsl:choose>
						 <xsl:when match=".[Items/Item/Documents $ne$ '' or Items/WebLinks/WebLink $ne$ '']">
							<TABLE class="BorderNoBackground"	border="0" cellPadding="5" cellSpacing="0" align="center" dir="ltr" width="100%">
								<TR>
									<TD align="left" class="DocsTask">&#160;</TD>

									<TD align="left" class="DocsTask">&#160;
										ID
									</TD>

									<TD align="left" class="DocsTask">&#160;
										Name
									</TD>
									<TD align="left" class="DocsTask">&#160;
										Description
									</TD>
									<TD class="DocsTask">

									</TD>
								</TR>
								<xsl:for-each select="Items/Item/Documents/Document">
									<TR>
										<xsl:choose>
											<xsl:when match=".[//IsWebEditor='True']"><td  class="DocsTask" width="1"><INPUT type="checkbox"><xsl:attribute name="name"><xsl:value-of select="DOCUMENTSYSTTEMID" /></xsl:attribute></INPUT></td> </xsl:when>
											<xsl:otherwise><td></td></xsl:otherwise>
										</xsl:choose>
										<TD align="left" class="NotesTabs_White">&#160;
											<xsl:value-of select="DocumentID"/>
										</TD>

										<TD align="left" class="NotesTabs_White">&#160;
													<img>
													<xsl:attribute name="src">
															<xsl:value-of select="DocumentIcon"/>
													</xsl:attribute> 
													</img>
													<a target="_blank" >
													<xsl:attribute name="href">
															<xsl:value-of select="DocumentLink"/>
													</xsl:attribute> 
													<xsl:value-of select="DocumentName" />
													</a>
										</TD>
										<TD align="left" class="NotesTabs_White">&#160;
											<xsl:value-of select="DocumentDescription"/>
										</TD>
										<xsl:choose>
											<xsl:when match=".[//IsWebEditor='True']">
												<TD align="left" class="NotesTabs_White">&#160;
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
								</xsl:for-each>
							</TABLE>
						</xsl:when>
						<xsl:otherwise>
							<TABLE border="0" cellPadding="5" cellSpacing="0" align="center" bordercolor="black" dir="ltr" id="ItemDocuments" Width="100%">

									<tr valign="bottom" style="background-color:#9999ff;;">
										<td align="center" nowrap="yes" class="notestabsNoBackground"><font color="Black" size="4">There are no Related Documents to this Failure</font></td>
									</tr>
							</TABLE>
						 </xsl:otherwise>
					</xsl:choose>
				</td>
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
										<a class="LW-toolbar" href="javascript:CutObj();void(0);" title="Cut" target="_self">
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
										<a class="LW-toolbar" href="javascript:CopyObj();void(0);" title="Copy" target="_self">
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
										<a class="LW-toolbar" href="javascript:PasteObj('|18|','','','|2|3|4|','');void(0);" title="Paste" target="_self">
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
		<!-- End Document Toolbar -->
	</body>
</html>
</xsl:template>
</xsl:stylesheet>