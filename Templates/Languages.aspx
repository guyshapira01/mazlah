<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Languages.aspx.vb" Inherits="Languages"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Languages</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK REL="stylesheet" HREF="Styles/Styles-SteelBlue.css" TYPE="text/css">
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<asp:ImageButton id="imgEnglish" style="Z-INDEX: 100; LEFT: 168px; POSITION: absolute; TOP: 72px"
				runat="server" Width="96px" Height="72px" ImageUrl="IconTypes/englishbig.gif" ForeColor="White"></asp:ImageButton>
			<asp:ImageButton id="imgGlobal" style="Z-INDEX: 107; LEFT: 40px; POSITION: absolute; TOP: 168px"
				runat="server" ImageUrl="IconTypes/global1.gif" Height="72px" Width="96px"></asp:ImageButton>
			<asp:ImageButton id="imgItalian" style="Z-INDEX: 105; LEFT: 296px; POSITION: absolute; TOP: 168px"
				runat="server" Width="96px" Height="72px" ImageUrl="IconTypes/italybig.gif"></asp:ImageButton>
			<asp:ImageButton id="imgGerman" style="Z-INDEX: 104; LEFT: 296px; POSITION: absolute; TOP: 72px"
				runat="server" Width="96px" Height="72px" ImageUrl="IconTypes/germanbig.gif"></asp:ImageButton>
			<asp:ImageButton id="imgHebrew" style="Z-INDEX: 103; LEFT: 40px; POSITION: absolute; TOP: 72px" runat="server"
				Width="96px" Height="72px" ImageUrl="IconTypes/israelbig1.gif"></asp:ImageButton>
			<asp:ImageButton id="imgSpanish" style="Z-INDEX: 102; LEFT: 168px; POSITION: absolute; TOP: 168px"
				runat="server" Width="96px" Height="72px" ImageUrl="IconTypes/spainbig.gif"></asp:ImageButton>&nbsp;
			<asp:Label id="Label1" style="Z-INDEX: 106; LEFT: 40px; POSITION: absolute; TOP: 16px" runat="server"
				Width="352px" Height="40px" Font-Names="Tahoma" Font-Size="Large">Select Language:</asp:Label></form>
	</body>
</HTML>
