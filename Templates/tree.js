function addToCookieDiv()
	{
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
		//alert("Add To Cookie");
		//alert(ExpandDiv.length);
		//alert(ExpandDiv[0]);
		for(var i=0;i<ExpandDiv.length;i++) 
			{
				//alert(ExpandDiv[i]);
				//alert('ExpandDiv' + i);
				if (ExpandDiv[i] != "noneID") setCookie (catalogNumber+'ExpandDiv' + i, ExpandDiv[i], expdate);
			}
		setCookie(catalogNumber+'scrollTopPos', window.document.body.scrollTop, expdate);
		//alert(window.document.body.scrollTop)
	}
function getFromCookieDiv()
	{
		//alert("Load From Cookie");
		var i=0;
		var PicrureName;
		//alert(getCookie(catalogNumber+'ExpandDiv'+i));
		while ( getCookie(catalogNumber+'ExpandDiv'+i) != null ) {
			PictureName = getCookie(catalogNumber+'ExpandDiv'+i);
			PictureName = PictureName.substr(PictureName.indexOf("_")+1);
	   		Switch(getCookie(catalogNumber+'ExpandDiv'+i), PictureName);
			delCookie(catalogNumber+'ExpandDiv'+i);
			//alert(getCookie(catalogNumber+'ExpandDiv'+i));
	   		i++;
		}
		window.document.body.scrollTop = getCookie(catalogNumber+'scrollTopPos');
		//alert(window.document.body.scrollTop);
		delCookie(catalogNumber+'scrollTopPos');
	}
function addToArrayDiv(id)
	{
		ExpandDiv[ExpandDiv.length] = id;
		ExpandDivStyle[ExpandDiv.length] = document.all(id).style.display;
		//alert("Add To Array")
	}
function removeFromArrayDiv(id)
	{
		//alert("Remove From Array")
		for (var i=0;i<ExpandDiv.length;i++)
			{
				if (ExpandDiv[i] == id) 
					{
						ExpandDiv[i] = 'noneID';
						//alert("Delete");
						break
					}
			}
		for(var j=i;j+1<ExpandDiv.length;j++)
			{
				ExpandDiv[j] = ExpandDiv[j+1];
				//alert("One Prev");
			}
		ExpandDiv[j] = 'noneID'
	}
function Switch(divID, PictureName, ClosePicture, OpenPicture){
	if ( document.all(divID)+"" != "null" )   {
		var sty = document.all(divID).style; 
		sty.display = sty.display==''?'none':'';
		if (sty.display != "none")
			{
				addToArrayDiv(divID);
			}
		else
			{
				removeFromArrayDiv(divID);
			}

		//alert(divID);
		//alert(PictureName);
		//alert(ClosePicture);
		if (document.all(divID).style.display == "none") 
			{ 
				changeImages(PictureName, ClosePicture); 
				return true;
			} 
		else 
			{ 
				changeImages(PictureName, OpenPicture); 
				return true;
			}
	}
}
function enter(){
	event.srcElement.style.fontWeight='bold';
	event.srcElement.style.color='red';
}
function leave(){
	event.srcElement.style.fontWeight='';
	event.srcElement.style.color='black';
}

function insertTable(){
	document.write('<TABLE DIR="RTL" CLASS="mainTable" width="100%" style="overflow:scroll;">')
}

function insertTR() {
	document.write('<TR CLASS="mainTR">')
}

function insertTD(){
	document.write('<TD DIR="LTR" CLASS="mainTD" align="right"  valign=top noWrap>')
}

