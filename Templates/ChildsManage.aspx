<%@ Page Language="vb" AutoEventWireup="false" CodeFile="ChildsManage.aspx.vb" Inherits="ChildsManage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Linkware Reorder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="LinkWare1.css" type="text/css" rel="stylesheet">
		<LINK href="WebEditor.css" type="text/css" rel="stylesheet">
		<!--To Define object colors-->
		<style>.Part { BACKGROUND-COLOR: white }
	.Document { BACKGROUND-COLOR: white }
	.Picture { BACKGROUND-COLOR: white }
		</style>
	</HEAD>
	<body bgColor="#ffffff">
		&nbsp;
		<table style="WIDTH: 500px; HEIGHT: 248px" width="500" border="0" borderColor="#003300">
			<tr>
				<td align="center" width="100%" bgColor="#ffffff">
					<FORM name="children" method="post"> <!-- banner -->
						<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<TBODY>
								<TR>
									<TD class="collabMainSection" align="center" bgColor="#ffffff"><!-- instructions -->
										<TABLE class="collabEditorSection" cellSpacing="3" cellPadding="0" width="100%" align="center"
											border="0">
											<TBODY>
												<TR>
													<TD style="HEIGHT: 42px" align="center" bgColor="#6666cc">
														<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
															<TBODY>
																<TR class="objectHeaderBg">
																	<TD class="objectHeader" style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
																		align="center" width="100%" height="24">&nbsp; <b dir="ltr">
																			<asp:label id="lblPartName" Runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="Medium"></asp:label></b></TD>
																</TR>
															</TBODY>
														</TABLE>
													</TD>
												</TR>
												<TR class="objectHeaderBg">
													<TD align="center" width="100%" bgColor="gainsboro">&nbsp;
														<asp:label id="Label1" runat="server" Width="340px" Height="40px" Font-Names="Tahoma" Font-Bold="True"
															Font-Size="Small">On this page, you can reorder objects by dragging them to the desired position</asp:label></TD>
												</TR>
												<TR>
													<TD><!-- tasks -->
														<TABLE cellSpacing="2" cellPadding="2" align="center" border="0">
															<TBODY>
																<TR>
																	<TD align="center" bgColor="white"><asp:label id="lblTableLoop" Runat="server" Width="472px"></asp:label></TD>
																</TR>
															</TBODY>
														</TABLE>
														<asp:label id="lbltaskIndices" Runat="server"></asp:label><INPUT type="hidden" value="0" name="taskID"> <!-- end section --></TD>
												</TR>
											</TBODY>
										</TABLE>
										<INPUT type="submit" value="OK"> <INPUT onclick="JavaScript:window.close();" type="button" value="Cancel">
					</FORM>
				</td>
			</tr>
		</table>
		<asp:label id="lblScript" Runat="server"></asp:label>
		<DIV></DIV>
		</TD> </TR> </TBODY> </TABLE>
		<FORM id="Form1" method="post" runat="server">
		</FORM>
	</body>
</HTML>
