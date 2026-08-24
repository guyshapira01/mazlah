<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Cut.aspx.vb" Inherits="Cut"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Cut</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body >
<script language="javascript" src="xp_progress.js"/>
<script language=javascript>
<!--
	var bIsNotFinish = true;
//-->
</script>
    <form id="Form1" method="post" runat="server">
<table border="0" width="300">
	<tr>
		<td>
			<font style="FONT-SIZE:13px;COLOR:blue;FONT-FAMILY:Arial;"><b>Please Wait......</b></Font>
		</td>
	</tr>
	<tr>
		<td>
			<script>
				var bar1= createBar(300,15,'white',1,'black','blue',85,7,3,"");
			</script>
		</td>
	</tr>
</table>
    </form>

  </body>
</html>
