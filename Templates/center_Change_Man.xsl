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
		<TABLE border="0" cellPadding="0" cellSpacing="0" height="100%" width="100%">
			<TR>
				<TD>
					<TABLE class="regularThin" border="0" cellPadding="0" cellSpacing="2" height="100%" width="98%">
			  			<TR>
							<TD vAlign="top" width="18%"  bgcolor="whitesmoke">
								<font style="color:black;font-size:16px;">CCB&#160;</font>
							</TD>
							<TD nowrap="yes" bgcolor="whitesmoke" width="18%" >
								<font style="color:red;font-weight:bold;font-size:14px">&#160;<xsl:value-of select="Items/CCB/ParCCBNumber"/></font>
								&#160;-&#160;
								<font style="color:red;font-weight:bold;font-size:14px">&#160;<xsl:value-of select="Items/CCB/ParCCBDate"/></font>
							</TD>
							<TD nowrap="yes" bgcolor="whitesmoke" width="18%" >
							<font style="color:black;font-weight:bold;font-size:14px">&#160;<xsl:value-of select="Items/CCB/ParCCBTitle"/></font>
							</TD>
							<TD colspan="3"  bgcolor="whitesmoke">
								<font style="color:black;font-weight:bold;font-size:14px">&#160;<xsl:value-of select="Items/CCB/ParCCBRemark"/></font>
							</TD>
			  			</TR>
					</TABLE>
				</TD>
			</TR>
			<TR>
				<TD>
					<TABLE class="BorderNoBackground" border="0" cellPadding="0" cellSpacing="2" height="100%" width="98%">
			  			<TR>
							<TD vAlign="top"  nowrap="yes">
								<font style="color:black;font-size:14px">Change Description&#160;&#160;</font>
							</TD>
							<TD colspan="5" vAlign="top" bgcolor="d2ccb9" style="direction:rtl">
							<font style="color:red;font-weight:bold;font-size:18px">&#160;<xsl:value-of select="Items/Item/Change_Title"/></font>
							</TD>
			  			</TR>

					  <TR>
							<TD width="18%" vAlign="top"  nowrap="yes">
								<font style="color:black;font-size:14px">ECP / ECO No.&#160;&#160;</font>
							</TD>
							<TD align="left" vAlign="top"  width="18%"  bgcolor="dcdc87" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
								 &#160;<font style="font-weight:bold"><xsl:value-of select="Items/Item/ECO_P_NO"/></font></TD>

							<TD  width="18%" vAlign="top" nowrap="yes">
								<font style="color:black;font-size:14px">&#160;&#160;Change Level&#160;&#160;</font>
							</TD>
							<TD align="left" vAlign="top" width="18%" style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
								<xsl:choose>
									<xsl:when match=".[Items/Item/Change_Level $eq$ 'Minor']">
										<xsl:attribute name="bgcolor">pink</xsl:attribute>
									</xsl:when>
									<xsl:when match=".[Items/Item/Change_Level $eq$ 'Major']">
										<xsl:attribute name="bgcolor">FF7809</xsl:attribute>
									</xsl:when>
									<xsl:when match=".[Items/Item/Change_Level $eq$ 'Formal']">
										<xsl:attribute name="bgcolor">white</xsl:attribute>
									</xsl:when>
									<xsl:otherwise>
										<xsl:attribute name="bgcolor">white</xsl:attribute>
									</xsl:otherwise>
								</xsl:choose>
								 &#160;<font style="font-weight:bold"><xsl:value-of select="Items/Item/Change_Level"/></font></TD>
							<TD  vAlign="top" nowrap="yes" width="10%">
								<font style="color:black;font-size:14px">&#160;&#160;Change Status&#160;&#160;</font>
							</TD>
							<TD align="left" vAlign="top" width="18%" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
								<xsl:choose>
									<xsl:when match=".[Items/Item/Change_Status $eq$ 'Evaluated']">
										<xsl:attribute name="bgcolor">white</xsl:attribute>
									</xsl:when>
									<xsl:when match=".[Items/Item/Change_Status $eq$ 'Pending']">
										<xsl:attribute name="bgcolor">white</xsl:attribute>
									</xsl:when>
									<xsl:when match=".[Items/Item/Change_Status $eq$ 'Approved']">
										<xsl:attribute name="bgcolor">6EFA68</xsl:attribute>
									</xsl:when>
									<xsl:when match=".[Items/Item/Change_Status $eq$ 'Rejected']">
										<xsl:attribute name="bgcolor">red</xsl:attribute>
									</xsl:when>
									<xsl:otherwise>
										<xsl:attribute name="bgcolor">white</xsl:attribute>
									</xsl:otherwise>
								</xsl:choose>
								&#160;<font style="font-weight:bold"><xsl:value-of select="Items/Item/Change_Status"/></font>
							</TD>
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
													<font style="font-size:16px">Change Details</font>
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
													<font style="font-size:16px">Activities</font>
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
								<TABLE 	border="0" cellPadding="2" cellSpacing="3" align="center" dir="ltr" width="95%">
									<xsl:for-each select="Items/Item">
										<TR>
											<TD vAlign="top"  width="8%" nowrap="yes">
												<font style="color:black;font-size:14px">Subsystem&#160;&#160;</font>
											</TD>
											<TD width="45%" nowrap="yes" align="left" vAlign="top">
												<table width="100%">
													<tr>
														<td width="70%" nowrap="yes" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
															 &#160;<xsl:value-of select="Subsystem"/>
												 		</td>
												 		<td>
												 			&#160;&#160;Rev&#160;&#160;
												 		</td>
														<td width="30%" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												 			<xsl:value-of select="KitRevision"/>
														 </td>
													</tr>
												</table>
											</TD>

											<TD align="left" vAlign="top"  width="10%">
												<font style="color:black;font-size:14px">LRU</font>
											</TD>
											<TD width="35%" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												 &#160;<xsl:for-each select="LRU"><xsl:value-of /></xsl:for-each>
											</TD>
											<TD width="1%">&#160;</TD>
										</TR>
										<TR>
											<TD vAlign="top"  nowrap="yes">
												<font style="color:black;font-size:14px">Part Name&#160;&#160;</font>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												 &#160;<xsl:for-each select="Part_Name"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD align="left"  vAlign="top">
												<font style="color:black;font-size:14px">P/N</font>
											</TD>
											<TD align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												 &#160;<xsl:for-each select="P_N"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD>&#160;</TD>
										</TR>
										<TR>
											<TD vAlign="top"  nowrap="yes">
												<font style="color:black;font-size:14px">Request By&#160;&#160;</font>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												 &#160;<xsl:for-each select="Request_By"> <xsl:value-of /></xsl:for-each>
											</TD>

											<TD align="left"  vAlign="top">
												<font style="color:black;font-size:14px">Date</font>
											</TD>
											<TD align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												 &#160;<xsl:for-each select="Date"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD>&#160;</TD>
										</TR>
										<TR>
											<TD vAlign="top"  nowrap="yes">
												<font style="color:black;font-size:14px">change Type&#160;&#160;</font>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												 &#160;<xsl:value-of select="ChangeType"/>
											</TD>
											<td colspan="2">
												<table width="100%">
													<tr>
														<TD align="left"  vAlign="top" nowrap="yes">
															<font style="color:black;font-size:14px">Recommended Level &#160;</font>
														</TD>
														<TD align="left"  width="40%" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
															<xsl:choose>
																<xsl:when match=".[Recommended_Change_Level $eq$ 'Minor']">
																	<xsl:attribute name="style">background:pink;width</xsl:attribute>
																</xsl:when>
																<xsl:when match=".[Recommended_Change_Level $eq$ 'Major']">
																	<xsl:attribute name="style">background:FF7809</xsl:attribute>
																</xsl:when>
																<xsl:when match=".[Recommended_Change_Level $eq$ 'Formal']">
																	<xsl:attribute name="style">background:ebebf0</xsl:attribute>
																</xsl:when>
																<xsl:otherwise>
																	<xsl:attribute name="style">background:ebebf0</xsl:attribute>
																</xsl:otherwise>
															</xsl:choose>
															<font style="font-weight:bold;font-size:18px">
																&#160;<xsl:for-each select="Recommended_Change_Level"> <xsl:value-of /></xsl:for-each>
															</font>
														</TD>
													</tr>
												</table>
											</td>
											<TD>&#160;</TD>
										</TR>
										<TR>
											<TD vAlign="top"  nowrap="yes">
												<font style="color:black;font-size:14px">Change Details&#160;&#160;</font>
											</TD>
											<TD colspan="3" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:40;overflow:auto;">
												 	&#160;<xsl:for-each select="Change_Details"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD>&#160;</TD>
										</TR>
										<TR>
											<TD vAlign="top"  nowrap="yes">
												<font style="color:black;font-size:14px">CCB Conclusion&#160;&#160;</font>
											</TD>
											<TD colspan="3" align="left"  style="direction:rtl" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:40;overflow:auto;">
													 &#160;<xsl:value-of select="CCB_Conclusion"/>
												</div>
											</TD>
											<TD>&#160;</TD>
										</TR>
										<TR>
											<TD vAlign="top"  nowrap="yes">
												<font style="color:black;font-size:14px">Remark&#160;&#160;</font>
											</TD>
											<TD colspan="3" align="left"  style="direction:rtl" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:40;overflow:auto;">
													 &#160;<xsl:for-each select="Remark"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD>&#160;</TD>
										</TR>
										<TR>
											<TD vAlign="top"  nowrap="yes">
												<font style="color:black;font-size:14px">For Customer&#160;&#160;</font>
											</TD>
											<TD colspan="3">
										<!--	<INPUT type="checkbox" disabled="true">
													<xsl:choose>
														<xsl:when match=".[SentToCustomerInd='Yes']">
															<xsl:attribute name="checked">true</xsl:attribute>
														</xsl:when>
														<xsl:otherwise>
														</xsl:otherwise>
													</xsl:choose>
															
												</INPUT>
											-->
												<table>
													<tr>
														<TD align="left" width="10%" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
															<xsl:value-of select="SentToCustomerInd"/>
														</TD>
														<TD>&#160;&#160;&#160;</TD>
														<TD align="left" width="90%" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
															<xsl:choose>
																<xsl:when match=".[ReferenceLink $ne$ '']">
																	<a target="_blank"><xsl:attribute name="href"><xsl:value-of select="ReferenceLink"/></xsl:attribute>
																	<xsl:value-of select="SendingReference"/></a>
																</xsl:when>
																<xsl:otherwise><xsl:value-of select="SendingReference"/></xsl:otherwise>
															</xsl:choose>
														</TD>
													</tr>
												</table>
											</TD>
											<TD>&#160;</TD>
										</TR>

									</xsl:for-each>
								</TABLE>
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
								<TABLE 	border="0" cellPadding="5" cellSpacing="5" align="center" dir="ltr" width="95%">
									<xsl:for-each select="Items/Item">
										<TR>
											<TD width="50%" align="center" class="docs" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<font style="color:black;font-size:14px">Activity</font>
											</TD>
											<TD  width="15%" align="center" vAlign="top" class="docs"  Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<font style="color:black;font-size:14px">Date</font>
											</TD>
											<TD  width="32%" align="center" vAlign="top" class="docs"  Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<font style="color:black;font-size:14px">Remark</font>
											</TD>
											<TD width="3%">&#160;</TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity1"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity1"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity1"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity2"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity2"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity2"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity3"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity3"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity3"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity4"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity4"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity4"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity5"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity5"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity5"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity6"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity6"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity6"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity7"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity7"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity7"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
										<TR>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Activity8"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<xsl:for-each select="Date_Activity8"> <xsl:value-of /></xsl:for-each>
											</TD>
											<TD nowrap="yes" align="left" vAlign="top" bgcolor="ebebf0" Style="border-left:black 1px solid; border-right:black 1px solid; border-top:black 1px solid; border-bottom:black 1px solid;">
												<div style="HEIGHT:25;overflow:auto;">
													<xsl:for-each select="Remark_Activity8"> <xsl:value-of /></xsl:for-each>
												</div>
											</TD>
											<TD></TD>
										</TR>
									</xsl:for-each>
								</TABLE>
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
	</TABLE><SCRIPT>newsContent[0].style.display = "block";tabs[0].className = "clsTabSelected_Top";</SCRIPT>
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
									<a class="LW-toolbar" href="JavaScript:UpdateObj('T_CAT_PART','Update_Change_Man_TAAS_E.xsl','true');void(0);" target="_self" title="Edit">
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