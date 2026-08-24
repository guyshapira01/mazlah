<!-- Drag n Move stuff -->
var curElement, begX=0, begY=0;
 
function doDragStart()
{
  // Don't do default drag operation.
  if ((("DIV"==event.srcElement.tagName) && ("window_div"==event.srcElement.id)) || (("DIV"==event.srcElement.tagName) && ("test"==event.srcElement.id)))
    event.returnValue=false;
}

function doMouseMove()
{
  if ((event.button==1) && (curElement!=null))
  {
      // position object
      var x=event.x;
      var y=event.y;
      curElement.style.pixelLeft=x-begX;
      curElement.style.pixelTop=y-begY;
      event.returnValue = false
      event.cancelBubble = true
  }
}

function doMouseDown()
{
  
    if (((event.button==1) && (event.srcElement.tagName=="DIV") && ("window_div"==event.srcElement.id))
	|| (("DIV"==event.srcElement.tagName) && ("test"==event.srcElement.id)))
    {
      curElement =document.all.item("window_div"); // event.srcElement
      var x=event.x;
      var y=event.y;
      begX=x-curElement.style.pixelLeft;
      begY=y-curElement.style.pixelTop;
    }
}

<!-- document.ondragstart = doDragStart; 
document.onmousedown = doMouseDown;
document.onmousemove = doMouseMove; 
document.onmouseup = new Function("curElement=null")