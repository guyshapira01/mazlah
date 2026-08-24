function doMouseMoveStuff() {
	scrollImage();
	movescontentmain();
	return false
}

function doMouseDownStuff() {
	coordinates();
	dragscontentmain();
	return false
}

function doMouseUpStuff() {
	mouseup();
	dragApproved();
	return false
}

if (top.length!=0){
	document.onmousedown=doMouseDownStuff
	document.onmouseup=doMouseUpStuff
	document.onmousemove=doMouseMoveStuff
}