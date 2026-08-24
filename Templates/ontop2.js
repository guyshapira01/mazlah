<!-------- floating window DIVs ---------->
// Initial floating window position
var w=5; //document.body.clientWidth-120
var h=5;

//// Do not edit pass this line
w+=top.frames('Header').document.body.scrollLeft
h+=top.frames('Header').document.body.scrollTop

var leftpos=w
var toppos=h
top.frames('Header').scontentmain.style.left=w
top.frames('Header').scontentmain.style.top=h

function openCloseDisplay(){
if (top.frames('Header').scontentsub.style.display=='') {
	top.frames('Header').scontentsub.style.display='none';
	top.frames('Header').document.images("OpenClose").src= 'open.gif';
	}
else {
	top.frames('Header').scontentsub.style.display='';
	top.frames('Header').document.images("OpenClose").src= 'close.gif';
}

}

function staticize(){
w2=top.frames('Header').document.body.scrollLeft+leftpos
h2=top.frames('Header').document.body.scrollTop+toppos
top.frames('Header').scontentmain.style.left=w2
top.frames('Header').scontentmain.style.top=h2
}
window.onscroll=staticize