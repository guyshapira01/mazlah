<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Calendar.aspx.vb" Inherits="CalendarDate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Calendar</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body >
		<form id="Form1" method="post" runat="server">
			<asp:calendar id="Calendar1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				BackColor="White" Width="200px" ForeColor="Black" Height="180px" Font-Size="8pt" Font-Names="Verdana"
				BorderColor="#999999" CellPadding="4" FirstDayOfWeek="Sunday">
				<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
				<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
				<DayStyle BackColor="#FFFFCC"></DayStyle>
				<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
				<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
				<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
				<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
				<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
				<OtherMonthDayStyle ForeColor="Gray"></OtherMonthDayStyle>
			</asp:calendar>
			<asp:Literal id="Literal1" runat="server"></asp:Literal></form>
	</body>
</HTML>
