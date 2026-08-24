<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Paste.aspx.vb" Inherits="Paste"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Paste Options</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
	<body >
		<!--
		Automatic Paste:
		pass a querystring parameter named "PasteOption" and give the folllwing values:
		0 - Paste
		1 - Paste as Duplicate
		2 - Deep Paste as Duplicate (not available yet)
		3 - Paste as WebLink (General Link)
		4 - Paste as WebLink (TreeLink)
	-->
		<form id="Form1" method="post" runat="server">
			<table border="1" style="WIDTH: 288px; HEIGHT: 441px">
				<tr>
					<td align="center">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr bgColor="royalblue">
								<td><asp:image id="Image1" runat="server" Width="24px" ImageUrl="../Images/LinkWare.jpg"></asp:image></td>
								<td align="center"><asp:label id="Label2" runat="server" BackColor="RoyalBlue" Font-Names="Tahoma" Font-Bold="True"
										Font-Size="Medium" Height="30px" Width="321px">Paste Options</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center"><asp:label id="Label1" style="Z-INDEX: 104" runat="server" BackColor="Gainsboro" Font-Names="Tahoma"
							Font-Bold="True" Font-Size="X-Small" Height="20px" Width="348px">On this page, you can paste coppied objects</asp:label></td>
				</tr>
				<tr>
					<td><asp:radiobuttonlist id="rdbPaste" style="Z-INDEX: 100" runat="server" Font-Names="Tahoma" Width="328px">
							<asp:ListItem Value="0">Paste as Link</asp:ListItem>
							<asp:ListItem Value="1">Paste as Duplicate</asp:ListItem>
							<asp:ListItem Value="2">Deep Paste as Duplicate</asp:ListItem>
							<asp:ListItem Value="3">Paste as WebLink (General Link)</asp:ListItem>
							<asp:ListItem Value="4">Paste as WebLink (TreeLink)</asp:ListItem>
						</asp:radiobuttonlist><br>
						<asp:CheckBox id="cBoxKeepOccur" runat="server" Width="320px" Font-Names="Tahoma" Text="Keep Occurence Properties"
							Checked="True"></asp:CheckBox></td>
				</tr>
				<tr>
					<td align="center"><asp:button id="btnPaste" style="Z-INDEX: 101" runat="server" Height="24px" Width="72px" Text="Paste"></asp:button><asp:button id="btnClose" runat="server" Height="24px" Width="72px" Text="Close"></asp:button></td>
				</tr>
				<tr>
					<td>
						&nbsp;
					</td>
				</tr>
				<tr>
					<td><asp:label id="lblError" style="Z-INDEX: 102" runat="server" Font-Names="Tahoma" Font-Bold="True"
							Font-Size="Smaller" Height="24px" Width="344px" Visible="False" ForeColor="#C00000">Paste is not allowed because it will cause a circular reference where an object is a parent/child of itself or the object already exists in the target location .
						</asp:label></td>
				</tr>
				<tr>
					<td align="center">
						<asp:Table id="tblClipboardObjects" runat="server" Width="344px" Visible="False">
							<asp:TableRow BackColor="RoyalBlue">
								<asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
									<asp:Label runat="server" Width="344px" ID="Label3" BackColor="RoyalBlue" Height="20px" Font-Size="x-small" Font-Names="Tahoma" Font-Bold="True">You are about to paste the following objects:</asp:Label>
								</asp:TableCell>
							</asp:TableRow>
						</asp:Table>
					</td>
				</tr>
				<tr>
				</tr>
			</table>
			<asp:Label id="lblScript" style="Z-INDEX: 101; LEFT: 72px; POSITION: absolute; TOP: 472px"
				runat="server" Width="203px"></asp:Label></form>
	</body>
</HTML>
