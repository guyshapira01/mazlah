<%@ Page Language="vb" AutoEventWireup="false" CodeFile="EditText.aspx.vb" Inherits="EditText"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EditText</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<asp:ListBox id="seltextBox" style="Z-INDEX: 100; LEFT: 56px; POSITION: absolute; TOP: 56px"
				runat="server" Width="376px" Height="208px" Font-Names="Tahoma"></asp:ListBox>
			<asp:Button id="btnClose" style="Z-INDEX: 105; LEFT: 240px; POSITION: absolute; TOP: 256px"
				runat="server" Font-Names="Tahoma" Height="32px" Width="192px" Text="Close" BackColor="#7F97B9"
				ForeColor="White" CssClass="Toolbar"></asp:Button>
			<asp:Button CssClass="Toolbar" id="btnDel" style="Z-INDEX: 104; LEFT: 56px; POSITION: absolute; TOP: 256px"
				runat="server" Font-Names="Tahoma" Height="32px" Width="184px" ForeColor="White" BackColor="#7F97B9"
				Text="Delete"></asp:Button>
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 56px; POSITION: absolute; TOP: 16px" runat="server"
				Width="168px" Height="24px" Font-Names="Tahoma" Font-Size="Medium" ForeColor="DarkBlue">Edit Text Object</asp:Label>
			<asp:Button CssClass="Toolbar" id="btnEdit" style="Z-INDEX: 102; LEFT: 56px; POSITION: absolute; TOP: 256px"
				runat="server" Width="184px" Height="32px" Font-Names="Tahoma" Text="Edit" ForeColor="White"
				BackColor="#7F97B9"></asp:Button></form>
	</body>
</HTML>
