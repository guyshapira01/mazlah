//Drag and drop engine for static content
var dragapproved=false
var zcor,xcor,ycor


function movescontentmain(){
	bInFrames = false;
	for(i=0;i<top.frames.length;i++)	{
		if (top.frames[i].name=="Parts Map")	{
			bInFrames = true;
		}
	}
	if (bInFrames)	{
		if (event.button==1&&top.frames['Parts Map'].window.dragapproved){
		top.frames['Parts Map'].window.zcor.style.pixelLeft=tempvar1+event.clientX-top.frames['Parts Map'].window.xcor
		top.frames['Parts Map'].window.zcor.style.pixelTop=tempvar2+event.clientY-top.frames['Parts Map'].window.ycor
		leftpos=top.frames['Header'].document.all.scontentmain.style.pixelLeft-top.frames['Header'].document.body.scrollLeft
		toppos=top.frames['Header'].document.all.scontentmain.style.pixelTop-top.frames['Header'].document.body.scrollTop
		//return false
	}
}

}
function dragscontentmain(){
	bInFrames = false;
	for(i=0;i<top.frames.length;i++)	{
		if (top.frames[i].name=="Parts Map")	{
			bInFrames = true;
		}
	}
	if (bInFrames)	{
		if (!top.frames['Header'].document.all)
		return
		if ((event.srcElement.id=="percent")|| (event.srcElement.id=="moveTable")){
		top.frames['Parts Map'].window.dragapproved=true
		top.frames['Parts Map'].window.zcor=scontentmain
		tempvar1=top.frames['Parts Map'].window.zcor.style.pixelLeft
		tempvar2=top.frames['Parts Map'].window.zcor.style.pixelTop
		top.frames['Parts Map'].window.xcor=event.clientX
		top.frames['Parts Map'].window.ycor=event.clientY
		//top.frames['Header'].document.onmousemove=movescontentmain
		}
	}
}

function dragApproved() {
	bInFrames = false;
	for(i=0;i<top.frames.length;i++)	{
		if (top.frames[i].name=="Parts Map")	{
			bInFrames = true;
		}
	}
	if (bInFrames)	{
		top.frames['Parts Map'].window.dragapproved=false;
	}
}

//top.frames['Header'].document.onmousedown=dragscontentmain
//top.frames['Header'].document.onmouseup=new Function("top.frames['Parts Map'].window.dragapproved=false")
