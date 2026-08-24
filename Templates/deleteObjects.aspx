<%@ Page Language="vb" AutoEventWireup="false" CodeFile="deleteObjects.aspx.vb" Inherits="deleteFiles" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>deleteFiles</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body  onload="JavaScript:ReloadParentPage()">
		<script language=javascript>
		<!--
			function ReloadParentPage()	{
				if (window.opener.top.frames.length != 0)
				{
					var a = document.location.href;
					var key = a.substring(a.indexOf("Pkey=",0)+5,a.indexOf("&",a.indexOf("Pkey=")))
					var selfdel = a.substring(a.indexOf("Self=",0)+5,a.indexOf("&",a.indexOf("Self=")))
					var parent = a.substring(a.indexOf("Parent=",0)+7,a.indexOf("&",a.indexOf("Parent=")))
					if (selfdel.toLowerCase() == "true")
					{
						window.opener.top.frames('Index').UpdateNode(parent,"na");
						window.opener.top.frames('Index').LinkSwitch(parent);
						//window.opener.top.frames('Index').RefreshFrames();
					}
					else
					{
						window.opener.top.frames('Index').UpdateNode(key,parent);
						window.opener.top.frames('Index').RefreshFrames();
					}
				}
				else
				{
					window.opener.top.window.location.reload();
				}
				window.close();
			}
		//-->
		</script>
		<form id="Form1" method="post" runat="server">
		</form>
	</body>
</HTML>
