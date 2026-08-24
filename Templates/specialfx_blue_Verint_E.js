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
//alert('obj_id='+obj_id);
	
	//alert("objid:" + obj_id);
	if (top.frames.length>0)	{
		//alert("1:" + top.frames('Index').currentHlighlighPkey);
		//alert("2:" + top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1]);
		if ((top.frames('Index').currentHlighlighPkey)!=(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1]+"") )	{
			if (obj_id==top.frames('Index').currentHlighlighPkey)	{
				//alert("+ 1");
				//alert("currentHlighlighPkey:" + top.frames('Index').currentHlighlighPkey);
				//alert(top.frames('Index').iCurrentPos);
				top.frames('Index').sPosition[top.frames('Index').iCurrentPos] = obj_id;
				top.frames('Index').iCurrentPos = top.frames('Index').iCurrentPos+1;
				//top.frames('Index').iCurrentPos = top.frames('Index').iCurrentPos+1;
				//alert(top.frames('Index').iCurrentPos);
			}
		}
	}
	// look for all elements with obj_id in all frames
	for (i=1; i<=top.frames.length; i++)
	{
	if (top.frames[i-1].name!='Index')
	{
	   for (j=0; j<top.frames[i-1].document.all.tags('TR').length; j++) 
	   {
   	      if (theObj = top.frames[i-1].document.all.item(obj_id,j)) 
		  {
		  //  alert('obj_id=' + obj_id + 'j='+j);
		    //alert(top.frames[i-1].name);
		  //  alert(theObj.type);
		     theObj.style.backgroundColor='yellow';
		     theObj.style.color='red';
		     theObj.scrollIntoView(false);
		     //alert(top.frames[2].window.document.body.scrollLeft);
		     //alert(top.frames[2].window.document.body.clientWidth);
		     //top.frames[2].window.document.body.scrollRight = top.frames[2].window.document.body.clientWidth;
		  }
	   }
	}
	  // alert('1=' + obj_id);
	}
	
   // look for all elements in current document
   for (i=0; i<document.all.tags('TR').length; i++)
        if (the_obj = document.all.item(obj_id,i)) {
	      the_obj.style.background='yellow';
	      the_obj.style.color='red';
	      //the_obj.scrollIntoView(true);
        } 
        // alert('2=' + obj_id);
}

isBackAvailable = false;
isTrueAvailable = false;

function highlightIndex(obj_id)
{
//alert('obj_id='+obj_id);
	
	//alert("objid:" + obj_id);
	if (top.frames.length>0)	{
		//alert("1:" + top.frames('Index').currentHlighlighPkey);
		//alert("2:" + top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1]);
		if ((top.frames('Index').currentHlighlighPkey)!=(top.frames('Index').sPosition[top.frames('Index').iCurrentPos-1]+"") )	{
			//alert("3:" + obj_id + " high= " + top.frames('Index').currentHlighlighPkey);
			if (obj_id==top.frames('Index').currentHlighlighPkey)	{
				//alert("4:");
				//alert("currentHlighlighPkey:" + top.frames('Index').currentHlighlighPkey);
				//alert(top.frames('Index').iCurrentPos);
				top.frames('Index').sPosition[top.frames('Index').iCurrentPos] = obj_id;
				top.frames('Index').iCurrentPos = top.frames('Index').iCurrentPos+1;
				//top.frames('Index').iCurrentPos = top.frames('Index').iCurrentPos+1;
				//alert(top.frames('Index').iCurrentPos);
			}
		}
	}
	// look for all elements with obj_id in all frames
	for (i=1; i<=top.frames.length; i++)
	{
	if (top.frames[i-1].name!='Index')
	{
	   for (j=0; j<top.frames[i-1].document.all.tags('TR').length; j++) 
	   {
   	      if (theObj = top.frames[i-1].document.all.item(obj_id,j)) 
		  {
		  //  alert('obj_id=' + obj_id + 'j='+j);
		    //alert(top.frames[i-1].name);
		  //  alert(theObj.type);
		     theObj.style.backgroundColor='yellow';
		     theObj.style.color='red';
		     theObj.scrollIntoView(true);
		     //alert(top.frames[2].window.document.body.scrollLeft);
		     //alert(top.frames[2].window.document.body.clientWidth);
		     //top.frames[2].window.document.body.scrollRight = top.frames[2].window.document.body.clientWidth;
		  }
	   }
	}
	  // alert('1=' + obj_id);
	}
	
   // look for all elements in current document
   for (i=0; i<document.all.tags('TR').length; i++)
        if (the_obj = document.all.item(obj_id,i)) {
	      the_obj.style.background='yellow';
	      the_obj.style.color='red';
	      //the_obj.scrollIntoView(true);
        } 
        // alert('2=' + obj_id);

}

<!-- Changes the style color of the element to 'black' -->
function unlight(obj_id)
{
	//if (obj_id==top.frames('Index').currentHlighlighPkey)	{
	//	top.frames('Index').iCurrentPos = top.frames('Index').iCurrentPos-1;
	//}
	// look for all elements with obj_id in all frames
	for (i=1; i<top.frames.length; i++)
	{
//top.frames[i-1] was chenged to top.frames[i] because changed map to left side-  Yoav

	   for (j=0; j<top.frames[i].document.all.tags('TR').length; j++) 
	   {
//top.frames[i-1] was chenged to top.frames[i] because changed map to left side-  Yoav

   	      if (theObj = top.frames[i].document.all.item(obj_id,j)) 
		  {
		     theObj.style.background='#FFFFFF';
		     theObj.style.color='black';
		  }
	   }
	}
	
    // look for all elements in current document
    for (i=0; i<document.all.tags('TR').length; i++)
      if (the_obj = document.all.item(obj_id,i)) {
        the_obj.style.background='#FFFFFF';
	    the_obj.style.color='black';
      }
}

<!-- puts the focus on an HTML element -->
function Focus(obj_id)
{
   // get own is and Focus on it.
   // no use looking for more elements, b/c only 1 element
   // can have the focus at a given time anyway.
   //if (the_obj = document.all.item(obj_id,0))
  //   the_obj.focus();
}

bIsMinimized = false;

<!-- Minimizes or Maximizes the window-style Div  -->
function minimize(win_id, tbl_id, btn_id)
{
	if (!top.frames('Parts Map').window.bIsMinimized)
	{
		document.all.item(tbl_id).style.display='none';
		document.all.item(btn_id).innerText = '[]';
	}
	else
	{
		document.all.item(tbl_id).style.display='';
		document.all.item(btn_id).innerText = '_';	
	}

	top.frames('Parts Map').window.bIsMinimized = top.frames('Parts Map').window.bIsMinimized ? false : true;
}

nLastX = 0;
nLastY = 0;
bIsDragged = false;

<!-- PopUp window function -->
function openWin( windowURL, windowName, windowFeatures ) 
{ 
	theWin = window.open( windowURL, windowName, windowFeatures ); 
	theWin.focus();
}

function addModeTypeColor(ModeType){
	//document.write("<HR size=20 width=100% color=");
	document.write("<TABLE width=100%><TR><TD width=47% bgcolor=");
	
	switch(ModeType){
		case "CS" : 
			document.write("Red ><td align=center><a href='JavaScript:window.open(\"modhelp.htm\",\"helpWin\",\"toolbar, resizable, width=400, height=300\");void(0);'>CS</a></td><TD width=47% align=center bgcolor=Red></TD>");
			break;
		case "MI" : 
			document.write("#FF00FF ><td align=center><a href='JavaScript:window.open(\"modhelp.htm\",\"helpWin\",\"toolbar, resizable, width=400, height=300\");void(0);'>MI</a></td><TD width=47% align=center bgcolor=#FF00FF></TD>");
			break;
		case "NMI" : 
			document.write("#FFC300 ><td align=center><a href='JavaScript:window.open(\"modhelp.htm\",\"helpWin\",\"toolbar, resizable, width=400, height=300\");void(0);'>NMI</a></td><TD width=47% align=center bgcolor=#FFC300></TD>");
			break;
		case "NDI" : 
			document.write("#00FF00 ><td align=center><a href='JavaScript:window.open(\"modhelp.htm\",\"helpWin\",\"toolbar, resizable, width=400, height=300\");void(0);'>NDI</a></td><TD width=47% align=center bgcolor=#00FF00></TD>");
			break;
		case "NR" : 
					document.write("#00FFFF ><td align=center><a href='JavaScript:window.open(\"modhelp.htm\",\"helpWin\",\"toolbar, resizable, width=400, height=300\");void(0);'>NR</a></td><TD width=47% align=center bgcolor=#00FFFF></TD>");
			break;
		default: 
			document.write(" ><td align=center></td><TD width=47% align=center bgcolor=></TD>");
			break;
	};
	document.write("<TR><TABLE>");
}
