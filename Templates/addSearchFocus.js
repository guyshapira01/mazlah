var pszImageName = 'SearchFocus';
var xPosition ;
var yPosition ;
var coordsString;
var arCoords; 



function fnSetSearchFocus(){

	if (top.length!=0){

		if (!top.frames.bFinishedLoading) {
			bWaiting=true;
			DelayeSearchFocus();
			return;
		}
		//check if exist (because when we doing update, its refresh only the 'right' frame.
		//alert(typeof(searchFocusTimer))
		if (typeof(searchFocusTimer)=='number')	{
			clearTimeout(searchFocusTimer);
		}

		
	// The fuction calls fnAddSearchFocus after parsing theurl for the area id..
		top.frames('Parts Map').top.frames('Parts Map').document.images("SearchFocus").src = "SearchFocus.gif";
		top.frames('Parts Map').top.frames('Parts Map').document.images("SearchFocus").style.visibility = 'hidden';
		var pszTheLocation= parent.location.href;
		

		pszPartId= pszTheLocation.split("part=area_");
		
		highlightSearch();
			
		

		//Check to see if there are any hotspots .. and that page is from search engine (include part= )
		if ( ((top.frames('Parts Map').top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas.length) > 0) && ( pszTheLocation.search('part=') != -1  ) ) 
		{
			
			
			fnAddSearchFocus(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value ,pszPartId[1] ,true);
			
		} //End if
	}
}

function doNothing()
{
	alert('nothing');
}

function fnAddSearchFocus(pszMapName, pszPicName, pszAreaId, bFirstLoad) {
	
	top.frames('Parts Map').top.frames('Parts Map').document.all("globalAreaId").innerHTML= pszAreaId;
	var blIdNotFound = true;	
	var idNumber  = 0;
	var AID=pszAreaId.replace('area_','');
	//Finding the area element with id pszAreaId
	
	while (blIdNotFound)  {
		if  (top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas.length == idNumber)
		{
			
			return;
			}
		else {
			//alert (top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas(idNumber).id + " =/ " +  pszAreaId);
		if (top.frames('Parts Map').top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas(idNumber).id == pszAreaId  ||  top.frames('Parts Map').top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas(idNumber).name == pszAreaId) {
				
				blIdNotFound = false;	
				break;
				
			}	
			idNumber = idNumber + 1 ;
		}; //end if
	} //End while
	

	top.frames('Parts Map').window.coordsString = top.frames('Parts Map').top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas(idNumber).coords;
	
	//alert ("top.frames('Parts Map').window.coordsString = "  + top.frames('Parts Map').window.coordsString);
//	alert("The final Id is " + idNumber );	
	top.frames('Parts Map').window.arCoords = top.frames('Parts Map').window.coordsString.split(",");

	pszScreenWidth= top.frames('Parts Map').top.frames('Parts Map').document.body.offsetWidth;
//alert( "Sreen = " + pszScreenWidth);

	pszImageWidth= top.frames('Parts Map').top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].width ;
	pszImageheight= top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].height  ;	


	// Value kept in temp var to avoid problems of window resize..
	if (bFirstLoad) {
		tmpImageWidth = pszImageWidth;
		tmpImageheight = pszImageheight;
		
	}
	
	//The size of the Saerch image 
	pszSearchImageWidth = top.frames('Parts Map').document.images[top.frames('Parts Map').window.pszImageName].width;
	pszSearchImageHeight = top.frames('Parts Map').document.images[top.frames('Parts Map').window.pszImageName].height;

	//alert ("image width = " +pszImageWidth + " , " + "image height " +pszImageheight );
	//alert("The image size is " + pszImageWidth );

	// Value kept in temp var to avoid problems of window resize..
	if (bFirstLoad ) {
		tmpXCoord = top.frames('Parts Map').window.arCoords[0]/1 -11 ; //Had to devide by one to change to integer !!
		tmpYCoord = top.frames('Parts Map').window.arCoords[1]/1 +37;//+14;
		//alert("x:"+tmpXCoord+"y:"+tmpYCoord)
	}
	//alert("x:"+tmpXCoord+"y:"+tmpYCoord)
	//alert("tmpXCoord"+tmpXCoord+"screen:"+pszScreenWidth+"image width:"+pszImageWidth);
	if (pszImageWidth < pszScreenWidth ) 
		top.frames('Parts Map').window.xPosition  = tmpXCoord+((pszScreenWidth-pszImageWidth)/2)- (pszSearchImageWidth)-12;// - pszSearchImageWidth/2 ;
	else{
		top.frames('Parts Map').window.xPosition  = tmpXCoord-(pszImageWidth-pszScreenWidth)- (pszSearchImageWidth)-16;
		}
		//alert("xpos:"+top.frames('Parts Map').window.xPosition );
//		}
//	else {
//		top.frames('Parts Map').window.xPosition  = tmpXCoord  +  (pszScreenWidth - tmpImageWidth )  / 2 - pszSearchImageWidth/1.5 ;
		//alert("top.frames('Parts Map').window.xPosition  =!! " + tmpXCoord   + " + ( " + pszScreenWidth + " - " + tmpImageWidth   + ")/2- "  + pszSearchImageWidth + "/1.5");
//		}
    
	top.frames('Parts Map').window.yPosition =  tmpYCoord - pszSearchImageHeight/2 +10;//+10 ;
//alert("pszSearchImageWidth = " +pszSearchImageWidth);

	
//alert ("tmpXCoords is " + 	tmpXCoord);
//alert ("tmpYCoords is " + 	tmpYCoord);
//alert ("pszScreenWidth is " + pszScreenWidth );
//alert ("tmpImageWidth is " + tmpImageWidth );
//alert( "X position is " + top.frames('Parts Map').window.xPosition);
//alert ("top.frames('Parts Map').window.arCoords[0]: " + top.frames('Parts Map').window.arCoords[0] + " pszZoomRatio " + pszZoomRatio );
//alert ( " pszScreenWidth: " +pszScreenWidth + " tmpImageWidth : " +tmpImageWidth );
//alert("X  is: " + top.frames('Parts Map').window.xPosition + " Y is: " + top.frames('Parts Map').window.yPosition);
//alert("after y:" + top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].offsetLeft);
	top.frames('Parts Map').top.frames('Parts Map').document.images("SearchFocus").style.top = top.frames('Parts Map').window.yPosition + top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].offsetTop ;
	top.frames('Parts Map').top.frames('Parts Map').document.images("SearchFocus").style.left = top.frames('Parts Map').window.xPosition + top.frames('Parts Map').document.images[top.frames('Parts Map').window.pic_name.value].offsetLeft ;
	top.frames('Parts Map').top.frames('Parts Map').document.images("SearchFocus").style.visibility = 'visible';

highlight(pszAreaId);	



}

function hideSearchImage() {
top.frames('Parts Map').top.frames('Parts Map').document.images("SearchFocus").style.visibility = 'hidden';

}

function changeSize(){
	
	if (top.length!=0){
		// Needed to define seperate function since window.changesize can't pass parameter ..
		var pszTheLocation= parent.location.href;
		
		//Check to see if there are any hotspots .. and that page is from search engine (include part= )
		if ( ((top.frames('Parts Map').top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas.length) > 0) && ( pszTheLocation.search('part=') != -1  ) ) {
			
				fnAddSearchFocus(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value ,pszPartId[1] ,false);
		} //End if

	}
	//change vml hotspot if exist on resize.
	if (typeof(VmlDiv)!='undefined')	{
		initVML();
	}
}




function DelayeSearchFocus(){
	//alert("Delaying");
	
	searchFocusTimer = setTimeout( "fnSetSearchFocus()", 100, "JAVASCRIPT" );
	}
	
function populateSearchBox()
	{
		
		var myarray=new Array();
		var x=top.frames('Parts Map').document.getElementsByTagName('AREA');
		var sb=top.frames('Parts Table').document.getElementById('sbox');
		var oName="";
		
		for(var i=0; i < x.length ;i++)
		{
			if(x[i].name !='' && x[i].name !=undefined)
			{
				
				//oName=x[i].name.split('_')[1];
				myarray[myarray.length++]=x[i];
				//sb.add(new Option(oName,x[i].id));	
				
			}
			
		}
		//alert(myarray.length);
		myarray.sort(sortArray);
		//alert(myarray[1].id);
		
		for(var j=0;j<myarray.length;j++)
		{
			sb.add(new Option(myarray[j].name.split('_')[1],myarray[j].id));
		}
	}
	
function populateComboSearchBox(dm)
	{
		
		var myarray=new Array();
		var x=top.frames('Parts Map').document.getElementsByTagName('AREA');
		
		var oName="";
		
		for(var i=0; i < x.length ;i++)
		{
			if(x[i].name !='' && x[i].name !=undefined)
			{
				
				//oName=x[i].name.split('_')[1];
				myarray[myarray.length++]=x[i];
				//sb.add(new Option(oName,x[i].id));
				
				
			}
			
		}

		//alert(myarray.length);
		myarray.sort(sortArray);
		//alert(myarray[1].id);
		for(var j=0;j<myarray.length;j++)
		{
			dm.add(new ComboBoxItem(myarray[j].name.split('_')[1]+'Y',myarray[j].id));
		}
	}

function sortArray(a,b)
{
	var x = a.name.split('_')[1];
    var y = b.name.split('_')[1];
    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
}	

function searchBoxClicked(sb)
	{
		
		//alert(sb.value);
		if(sb.selectedIndex>0)
			highlightAreaById(sb.value);
		//sb represent the searchbox object
		//fnAddSearchFocus('globalAreaId', '', sb.value, true);
	}
function searchComboBoxClicked(v)
	{
		
		//alert(sb.value);
		//if(sb.selectedIndex>0)
			highlightAreaById(v);
		//sb represent the searchbox object
		//fnAddSearchFocus('globalAreaId', '', sb.value, true);
	}
function highlightAreaByName(obj)
{
    //alert(obj);
	
	var elem = document.getElementsByTagName('AREA');
	var areas='';
	//alert(elem.length);
	for(var i=0;i<elem.length;i++) {

		//alert(elem[i].name);
		if(("area_"+elem[i].name).indexOf(obj,0)>=0)
		{
			//alert(elem[i].name + ' id=' + obj);
			//highlightAreaById(elem[i].id);
			if(areas!='')
				areas=areas+"," +elem[i].id;
			else
				areas=elem[i].id;
			extAreaOver('myimg', elem[i].id);
		}
	}

	if(areas!='')
		extAreasOver('myimg', areas);
	/*for(var i=0;i<elem.length;i++)
	{
		if (typeof elem[i].onmouseover == "function") {
			elem[i].onmouseover.apply(elem[i]);
		}
	}*/
	
}	

function highlightAreaById(obj)
{
	
	
	var elem=document.getElementById(obj);
	
	extAreaOver('myimg', elem.id);
	
	
}	

function highlightSearch() {
   
	var pszTheLocation= parent.location.href;
	
	
	pszPartId= pszTheLocation.split("part=");
	
	if(pszPartId[1]!=null && pszPartId[1].length>5)
	{
		highlightAreaByName(pszPartId[1]);
	}
}	