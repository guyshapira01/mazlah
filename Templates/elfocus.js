<script language="JavaScript">
<!--
<!-- puts the focus on an HTML element -->
function Focus(obj_id)
{
   // get own is and Focus on it.
   // no use looking for more elements, b/c only 1 element
   // can have the focus at a given time anyway.
   if (the_obj = document.all.item(obj_id,0))
     the_obj.focus();
}
//-->
</script>