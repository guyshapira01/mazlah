<script language="JavaScript">
<!--

bIsMinimized = false;

<!-- Minimizes or Maximizes the window-style Div  -->
function minimize(win_id, tbl_id, btn_id)
{
	if (!bIsMinimized)
	{
		document.all.item(tbl_id).style.display='none';
		document.all.item(btn_id).innerText = '[]';
	}
	else
	{
		document.all.item(tbl_id).style.display='';
		document.all.item(btn_id).innerText = '_';	
	}

	bIsMinimized = bIsMinimized ? false : true;
}
//-->
</script>