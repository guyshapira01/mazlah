mouseover=true
xcoord=0;ycoord=0;
lastx=0;lasty=0;
deltax=0;deltay=0;

function coordinates() {
	bInFrames = false;
	for(i=0;i<top.frames.length;i++)	{
		if (top.frames[i].name=="Parts Map")	{
			bInFrames = true;
		}
	}
	if (bInFrames)	{
		if (event.srcElement.name == top.frames['Parts Map'].window.pic_name.value) {
			event.srcElement.style.cursor='move';
			top.frames['Parts Map'].window.mouseover=true;
		}
	}
}

function scrollImage() {
	bInFrames = false;
	for(i=0;i<top.frames.length;i++)	{
		if (top.frames[i].name=="Parts Map")	{
			bInFrames = true;
		}
	}
	if (bInFrames)	{
		top.frames['Parts Map'].window.lastx=top.frames['Parts Map'].window.xcoord;
		top.frames['Parts Map'].window.lasty=top.frames['Parts Map'].window.ycoord;
		top.frames['Parts Map'].window.xcoord=event.clientX
		top.frames['Parts Map'].window.ycoord=event.clientY
		top.frames['Parts Map'].window.deltax=top.frames['Parts Map'].window.lastx-top.frames['Parts Map'].window.xcoord;
		top.frames['Parts Map'].window.deltay=top.frames['Parts Map'].window.lasty-top.frames['Parts Map'].window.ycoord;
		//status= ' deltas: d_X=' + top.frames['Parts Map'].window.deltax + ' d_Y=' + top.frames['Parts Map'].window.deltay;
		if (top.frames['Parts Map'].window.mouseover&&event.button==1)
			window.scrollBy(top.frames['Parts Map'].window.deltax,top.frames['Parts Map'].window.deltay);				
	}
}

function mouseup() {
	event.srcElement.style.cursor='';
	bInFrames = false;
	for(i=0;i<top.frames.length;i++)	{
		if (top.frames[i].name=="Parts Map")	{
			bInFrames = true;
		}
	}
	if (bInFrames)	{
		top.frames['Parts Map'].window.mouseover=false;
	}
	bFirst=true;
}