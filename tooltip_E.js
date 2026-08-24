<!-- ToolTip div manipulation funcs -->
var timeout;

function ToolTip(ToolTipText) {
    theItem = document.all.item("tipwindow",0);
    theItem.innerText = ToolTipText;
    theItem.innerHTML = '<CENTER>' + theItem.innerHTML + '</CENTER>';
    theItem.style.width = (ToolTipText.length * 8);
    theItem.style.left = window.event.clientX + 10;
    theItem.style.top = window.event.clientY + 10;
    timeout = setTimeout( "ShowToolTip()", 1000, "JAVASCRIPT" );
}

function makatWithoutHyphen(makat)	{
	arrayOfStrings = makat.split("-");
	newMakat="";
	for(i=0;i<arrayOfStrings.length;i++)
		newMakat = newMakat + arrayOfStrings[i];
	return(newMakat);
}

function ToolTipMultiLine(LineStrings) {
    if (LineStrings == "") return;
    // split the input line
    SplitStrings = LineStrings.split("|");
    // find the 'ToolTip' DIV object
    theItem = document.all.item("tipwindow",0);
	theItem.align='left';
    var tbl = theItem.innerHTML = '<table dir=rtl>';
	var tRow, tCell, tBody = theItem.all(2);
	tBody.align='left';
	maxLen = 0;
	//alert("SplitStrings.length"  + SplitStrings.length );
    for (i=0; i<SplitStrings.length; i++)
	maxLen = maxLen > SplitStrings[i].length ? maxLen : SplitStrings[i].length;

    w = maxLen*8;
	theItem.style.width = w;
    for (i=0; i<SplitStrings.length; i++) {
		InnerSplit = SplitStrings[i].split("^");
		tRow = tBody.insertRow();
		//tCell.noWrap = true;
		for (j=0; j<2; j++) {
			tCell = tRow.insertCell();
			//check for makat (i=1;j=0)
			if	( (i==1) && (j==0) )	{
				tCell.innerHTML = "<font size=2px>"+makatWithoutHyphen(InnerSplit[j])+"</font>";
			}
			else	{
				tCell.innerHTML = "<font size=2px>"+InnerSplit[j]+"</font>";
			}
			if (j==1) tCell.style.fontWeight='bold';
		}
	}

	bInFrames = false;
	for(i=0;i<top.frames.length;i++)	{
		if (top.frames[i].name=="Parts Map")	{
			bInFrames = true;
		}
	}
	if (bInFrames)	{
		if ((event.x-document.body.scrollLeft) > document.body.clientWidth/2)
			theItem.style.left = top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].offsetLeft + event.x - w ;
		else
			theItem.style.left = top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].offsetLeft + event.x -20;

		if ((event.y-document.body.scrollTop) > document.body.clientHeight/2)	
			theItem.style.top = top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].offsetTop + event.y - (60 + SplitStrings.length *10);
		else
			theItem.style.top = top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].offsetTop + event.y + (60 + SplitStrings.length *10);
	}
	else {
		if ((event.x-document.body.scrollLeft) > document.body.clientWidth/2)
			theItem.style.left = document.images[window.pic_name.value].offsetLeft + event.x - w ;
		else
			theItem.style.left = document.images[window.pic_name.value].offsetLeft + event.x -20;

		if ((event.y-document.body.scrollTop) > document.body.clientHeight/2)	
			theItem.style.top = document.images[window.pic_name.value].offsetTop + event.y - (60 + SplitStrings.length *10);
		else
			theItem.style.top = document.images[window.pic_name.value].offsetTop + event.y + (60 + SplitStrings.length *10);
	}
	// This is the time that one must pause on a hot spot until it will show
	timeout = setTimeout( "ShowToolTip()", 10, "JAVASCRIPT" );
}

function ShowToolTip() {
    theItem.style.display="";

    //This is the time that the tooltip will hide after not moving with the cursor.
    timeout = setTimeout( "HideToolTip()", 50000, "JAVASCRIPT" );
}

function HideToolTip() {
    clearTimeout(timeout);
    document.all.item("tipwindow",0).style.display="none";
}

function ToolTipObjDesc(ToolTipText) {

    theItem = document.all.item("tipwindow",0);

	//Show tooltip according to tooltiptext

	switch(ToolTipText) {
		case 'Specification':
			 theItem.innerText = "Performance Specification";
			 break;
	    case 'Solid':
		 	 theItem.innerText = "Output of the CAD System, providing visualization of the designed component. A top-level diagram will identify the location of each described element in the Tank";
		  	 break;
		case 'Study':
		     theItem.innerText = "A report, analysis or technical document explaining trade-offs, evaluation considerations or theoretical background for the modification or customization performed on the specific item";
			 break;
        case 'Mockup':
			 theItem.innerText = "A physical mockup (or actual subsystem), available for visual inspection";
			 break;
        case 'Dwg':
			 theItem.innerText = "Engineering mechanical drawing, showing layout and dimensions ";
			 break;
        case 'Test  Data':
 			 theItem.innerText = "Results of relevant tests performed on the specific item";
			 break;
        case 'HMI':
 			 theItem.innerText = "Human-Machine interface considerations";
			 break;
        case 'Mechanical ICD':
 			 theItem.innerText = "Mechanical drawing, showing the physical interfaces between the item and its next-higher assembly";
			 break;
        case 'Electrical ICD':
 			 theItem.innerText = "Electrical diagram, showing the electrical interconnections";
			 break;
        case 'Producibility':
  			 theItem.innerText = "Document describing the producibility considerations, relevant to System / Subsystems to be produced in Turkey";
			 break;
        case 'Weight':
			 theItem.innerText = "Preliminary weight data, used in the Weight Control Program";
			 break;
        case 'Safety':
			 theItem.innerText = "Applicable safety considerations or instructions";
			 break;

	}

    //theItem.innerHTML = '<LEFT>' + theItem.innerHTML + '</LEFT>';
	theItem.align='left';

	theItem.style.width = (ToolTipText.length * 20);
	theItem.style.left = window.event.clientX +70 ;
        theItem.style.top = window.event.clientY + 400;

    timeout = setTimeout( "ShowToolTip()", 1000, "JAVASCRIPT" );
}
