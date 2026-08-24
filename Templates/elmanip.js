<script language="JavaScript">
<!--

<!-- Changes the 'src' velue of the element(obj_id).
<!-- Basically used to change the picture when mouse is over it -->
function change(obj_id, change_to)
{
   var i;

   for (i=0; i<top.frames.length; i++)
   {
 	target=top.frames[i].document.all.item(obj_id,0);
	if (target)
	     target.src = change_to;
   }
   
   if (!i)
	target=document.all.item(obj_id);
   if (target)
	target.src = change_to;
}

<!-- Changes the style color of the element to 'yellow' -->
function highlight(obj_id)
{
	// look for all elements with obj_id in all frames
	for (i=1; i<=top.frames.length; i++)
	{
	   for (j=0; j<top.frames[i-1].document.all.tags('TR').length; j++) 
	   {
   	      if (theObj = top.frames[i-1].document.all.item(obj_id,j)) 
		  {
		     theObj.style.background='yellow';
		     theObj.style.color='red'; 
		  }
	   }
	}
	
   // look for all elements in current document
   for (i=0; i<document.all.tags('TR').length; i++)
        if (the_obj = document.all.item(obj_id,i)) {
	      the_obj.style.background='yellow';
	      the_obj.style.color='red';
        }
}

<!-- Changes the style color of the element to 'black' -->
function unlight(obj_id)
{
	// look for all elements with obj_id in all frames
	for (i=1; i<top.frames.length; i++)
	{
	   for (j=0; j<top.frames[i-1].document.all.tags('TR').length; j++) 
	   {
   	      if (theObj = top.frames[i-1].document.all.item(obj_id,j)) 
		  {
		     theObj.style.background='';
		     theObj.style.color='black';
		  }
	   }
	}
	
    // look for all elements in current document
    for (i=0; i<document.all.tags('TR').length; i++)
      if (the_obj = document.all.item(obj_id,i)) {
        the_obj.style.background='';
	    the_obj.style.color='black';
      }
}
//-->
</script>