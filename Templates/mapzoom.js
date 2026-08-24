var pszOrigCoords, origImgWidth, svMapName, svImage, svZoom, my_timer, bWaiting=true, bFirstZoom=true;
var zoomRatio=parseFloat('0' + getCookie('_zoom_ratio'));
function getPartsMapWindow()
{
    try { return top.frames['Parts Map']; } catch(e) { return null; }
}

function getPartsMapImage()
{
    var w = getPartsMapWindow();
    if (!w || !w.document) return null;
    return w.document.getElementById('myimg');
}

function getZoomTarget()
{
    var w = getPartsMapWindow();
    if (!w || !w.document) return null;

    // cvi_map replaces/wraps the image with a canvas in Chrome.
    // Zoom the common containing block so image/canvas/hotspots stay together.
    return w.document.getElementById('ImageMapDIV') ||
           w.document.getElementById('mybody') ||
           w.document.body;
}

function updateZoomPercent()
{
    try {
        var h = top.frames['Header'];
        var el = h.document.getElementById('percent');
        if (el) el.textContent = Math.round(zoomRatio * 100) + "%";
    } catch(e) {}
}

function applyChromeZoom()
{
    var target = getZoomTarget();
    if (!target) return false;

    if (zoomRatio < 0.1) zoomRatio = 0.1;
    if (zoomRatio > 5) zoomRatio = 5;

    // CSS zoom is supported by current Chrome and, unlike the old IE code,
    // works through normal DOM access.
    target.style.zoom = String(zoomRatio);

    try {
        var w = getPartsMapWindow();
        w.zoomRatio = zoomRatio;
        if (w.document && w.document.getElementById('tipwindow')) {
            w.document.getElementById('tipwindow').style.fontSize = '30px';
        }
    } catch(e) {}

    updateZoomPercent();

    // Repaint a persistent selected hotspot after the canvas is resized/zoomed.
    try {
        var w = getPartsMapWindow();
        if (typeof w.restoreSelectedHotspot === 'function') {
            setTimeout(function(){ w.restoreSelectedHotspot(); }, 0);
        }
    } catch(e) {}

    return false;
}

function fitSz()
{
    var w = getPartsMapWindow();
    var img = getPartsMapImage();
    if (!w || !img) return 1;

    var originalWidth = img.naturalWidth || img.width || 1;
    var viewportWidth = w.document.documentElement.clientWidth ||
                        w.document.body.clientWidth || originalWidth;

    var ratio = (viewportWidth - 20) / originalWidth;
    if (!isFinite(ratio) || ratio <= 0) ratio = 1;
    if (ratio > 1) ratio = 1;

    return ratio;
}

function fnZoom(pszMapName,pszImage, lZoom, bAbsolute)
{
//debugger;
//alert(" before zoom:" + top.frames('Parts Map').VmlDiv.children(1).Points.value);

if (top.length!=0){

if (top.frames('Parts Map').document.all[pszMapName]+"" != "undefined" )
		
	{

	if (bAbsolute==true) top.frames('Parts Map').window.zoomRatio=1.0;
	if ((!top.frames.bFinishedLoading)&&(top.frames('Parts Map').window.bWaiting)) {
//		top.frames('Parts Map').window.bWaiting=true;
		fnZoomDelayed(pszMapName,pszImage,lZoom);
		return;
	}

	top.frames('Parts Map').window.bWaiting=false; 
//	if ((lZoom==null)||(lZoom=='FIT_TO_PG')) lZoom = fitSz();

        if (lZoom=='FIT_TO_PG')
        {
        	//alert(fitSz());
        	lZoom = fitSz(); 
        	if (fitSz()>1) lZoom=1;
        }
        if (lZoom==null) lZoom = 1; 

	var i, j, pszItemCoord, arCoord, pszCoordOld, pszCoordNew;
	var newPercentage;
	arCoord=new Array(0);
	
	if (top.frames('Parts Map').window.bFirstZoom) {
		top.frames('Parts Map').window.bFirstZoom=false;
		top.frames('Parts Map').window.pszOrigCoords = new Array(0,0);
		for (i=0; i<top.frames('Parts Map').document.all[pszMapName].areas.length; i++) {
			top.frames('Parts Map').window.pszOrigCoords[i] = 			top.frames('Parts Map').document.all[pszMapName].areas(i).coords;
			
		}
		top.frames('Parts Map').window.origImgWidth = top.frames('Parts Map').document.images(pszImage).width;
	}
	
	top.frames('Parts Map').window.zoomRatio*=lZoom;
	
	tCell = top.frames('Header').document.all("percent");
	newPercentage = 10*Math.round(10*top.frames('Parts Map').window.zoomRatio);
	if (tCell ) tCell.innerText =  newPercentage + "%";
	
  //************************************************************************
  //Scale picture
  // Yoav changed : added visible after Zoom. Iivisble is in the HTML page style of the image.
  //top.frames[2].top.frames('Parts Map').document.images(pszImage).style.visibility = 'hidden';
	if (lZoom == 1) {
	top.frames('Parts Map').window.zoomRatio*=(1/top.frames('Parts Map').window.zoomRatio);
	if (tCell ) tCell.innerText = "100%";
	}
  top.frames('Parts Map').document.images(pszImage).width = top.frames('Parts Map').window.origImgWidth * top.frames('Parts Map').window.zoomRatio;
 // t=  top.frames[2].top.frames('Parts Map').document.images(pszImage).width ;
//	alert( "The width from zoom : " + t + " , " +  top.frames('Parts Map').window.origImgWidth +  " , " + top.frames('Parts Map').window.zoomRatio);
  top.frames('Parts Map').document.images(pszImage).style.visibility = 'visible';

  //*************************************************************************
  //scale map coords

// Do not scal if there are no AREA elemtns..

	if(top.frames('Parts Map').document.all[top.frames('Parts Map').window.map_name.value].areas.length > 0 ) {
	numberOfAreaElemts = top.frames('Parts Map').window.pszOrigCoords.length - 1 ;
	  for (i=0; i< numberOfAreaElemts ; i++)
	   {
		//alert("top.frames('Parts Map').window.pszOrigCoords[i]; = " + top.frames('Parts Map').window.pszOrigCoords[i]);
	    pszCoordOld = top.frames('Parts Map').window.pszOrigCoords[i];
		//alert("pszCoordOld : "  + pszCoordOld );
		//Old coord
		try{
		
		    arCoord=pszCoordOld.split(",");
		}
		catch(err){}
		
		pszCoordNew="";
		 for (j=0;j<arCoord.length;j++)
		  {
	 		 pszCoordNew=pszCoordNew + arCoord[j]*top.frames('Parts Map').window.zoomRatio + ",";
		  }
		//New coord  
	    pszCoordNew=pszCoordNew.slice(0,-1);
	    top.frames('Parts Map').document.all[pszMapName].areas(i).coords=pszCoordNew;
	    //alert ("Elemt number "+  i + " Coords are : " + pszCoordNew);
	  }
	} // End if 
	areaId= top.frames('Parts Map').document.all("globalAreaId").innerHTML;
	top.frames('Parts Map').document.all("globalAreaId").style.visibility = 'hidden';
	if (areaId != ""){
		fnAddSearchFocus(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value ,areaId ,true);
	}
   }
}
//alert(" after zoom:" + top.frames('Parts Map').VmlDiv.children(1).Points.value);
top.frames('Parts Map').ChangeHotspotGraphic();
}

oldZoomratio = 1;


function fnZoomNew(addratio)
{
    var n = parseFloat(addratio);
    if (!isFinite(n) || n <= 0) n = 1;
    zoomRatio = n;
    return applyChromeZoom();
}

function fnAddZoom(addratio)
{
    var delta = parseFloat(addratio);
    if (!isFinite(delta)) delta = 0;

    if (!isFinite(zoomRatio) || zoomRatio <= 0) zoomRatio = 1;
    zoomRatio += delta;

    return applyChromeZoom();
}

function ChangeHotspotGraphic()	{
//debugger;
	if (typeof(VmlDiv)!="undefined")	{
		for(i=0;i<VmlDiv.children.length;i++)	{
			if (VmlDiv.children(i).tagName=="oval")	{
				VmlDiv.children(i).style.width = VmlDiv.children(i).style.width.substring(0,VmlDiv.children(i).style.width.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				VmlDiv.children(i).style.height = VmlDiv.children(i).style.height.substring(0,VmlDiv.children(i).style.height.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				VmlDiv.children(i).style.top = VmlDiv.children(i).style.top.substring(0,VmlDiv.children(i).style.top.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				VmlDiv.children(i).style.left = VmlDiv.children(i).style.left.substring(0,VmlDiv.children(i).style.left.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
			}
			if (VmlDiv.children(i).tagName=="rect")	{
				VmlDiv.children(i).style.width = VmlDiv.children(i).style.width.substring(0,VmlDiv.children(i).style.width.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				VmlDiv.children(i).style.height = VmlDiv.children(i).style.height.substring(0,VmlDiv.children(i).style.height.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				VmlDiv.children(i).style.top = VmlDiv.children(i).style.top.substring(0,VmlDiv.children(i).style.top.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				VmlDiv.children(i).style.left = VmlDiv.children(i).style.left.substring(0,VmlDiv.children(i).style.left.indexOf("p"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
			}
			if (VmlDiv.children(i).tagName=="polyline")	{
			if(top.frames('Parts Map').window.zoomRatio != 1){
				var HtmlPoly = VmlDiv.children(i).outerHTML;
				HtmlPoly = HtmlPoly.substr(HtmlPoly.indexOf("<v:polyline"))
				var sPointsStart = HtmlPoly.indexOf("points =") + 10;
				var sPointsEnd = HtmlPoly.indexOf("\"",sPointsStart);
				var HtmlPoints = HtmlPoly.substr(sPointsStart,sPointsEnd-sPointsStart);
				var arrayOfCorrds = HtmlPoints.split("pt,")
				for (j=0; j < arrayOfCorrds.length - 1; j++) {

					//if mesure is "in" insted of "pt" so need to  * 72
					/*if (arrayOfCorrds[j].substr(arrayOfCorrds[j].length-2,2)=="in")
					{
					arrayOfCorrds[j]=arrayOfCorrds[j].substr(0,arrayOfCorrds[j].length-2)*72	
					}
					else
					{
					arrayOfCorrds[j]=arrayOfCorrds[j].substr(0,arrayOfCorrds[j].length-2)
					}*/

					//alert(arrayOfCorrds[j]);
				      arrayOfCorrds[j]=arrayOfCorrds[j]*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
					//alert(arrayOfCorrds[j]);
				}
				//if mesure is "in" insted of "pt" so need to  * 72
				if (arrayOfCorrds[arrayOfCorrds.length-1].substr(arrayOfCorrds[arrayOfCorrds.length-1].length-2,2)=="in")
				{
					arrayOfCorrds[arrayOfCorrds.length-1] = ((arrayOfCorrds[arrayOfCorrds.length-1].substring(0,arrayOfCorrds[arrayOfCorrds.length-1].indexOf("in"))*72))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				}
				else
				{
					arrayOfCorrds[arrayOfCorrds.length-1] = arrayOfCorrds[arrayOfCorrds.length-1].substring(0,arrayOfCorrds[arrayOfCorrds.length-1].indexOf("pt"))*top.frames('Parts Map').window.zoomRatio/oldZoomratio;
				}


				var newPoints="";
				for (j=0; j < arrayOfCorrds.length; j++) {
					newPoints = newPoints + arrayOfCorrds[j] + "pt,";
				}
				newPoints = newPoints.substring(0,newPoints.length-1);
				//alert(HtmlPoly.substr(0,sPointsStart) + newPoints + HtmlPoly.substr(sPointsEnd))
				VmlDiv.children(i).outerHTML = HtmlPoly.substr(0,sPointsStart) + newPoints + HtmlPoly.substr(sPointsEnd);
				//document.write(VmlDiv.children(i).Points.value);
				//alert(VmlDiv.children(i).Points.value);
				//alert("newpoints:" + newPoints);
				//VmlDiv.children(i).Points.value = newPoints;
				//alert(VmlDiv.children(i).Points.value);
				//document.write(VmlDiv.children(i).Points.value);
				}
			}
		}
		oldZoomratio = top.frames('Parts Map').window.zoomRatio;
		initVML();
	}
}

function fnZoomNow() 
{
	clearTimeout(top.frames('Parts Map').window.my_timer);
	fnZoom(top.frames('Parts Map').window.svMapName,top.frames('Parts Map').window.svImage,top.frames('Parts Map').window.svZoom);
	
}

function fnZoomDelayed(pszMapName,pszImage,lZoom)
{
 	top.frames('Parts Map').window.svMapName=pszMapName;
	top.frames('Parts Map').window.svImage=pszImage;
	top.frames('Parts Map').window.svZoom=lZoom;
 	top.frames('Parts Map').window.my_timer = setTimeout( "fnZoomNow()", 50, "JAVASCRIPT" );	// retry after this period
}

function SetZoomSize(sz)
{
    var i_sz = (sz != '') ? parseFloat(sz) : zoomRatio;
    if (!isFinite(i_sz) || i_sz <= 0) i_sz = 1;

    var expdate = new Date();
    expdate.setTime(expdate.getTime() + (24 * 60 * 60 * 1000 * 365));
    setCookie('_zoom_ratio', i_sz, expdate);

    try {
        var h = top.frames['Header'];
        var ok = h.document.getElementById('freezeImageOk');
        if (ok) {
            ok.textContent = "✓";
            setTimeout(function(){ ok.textContent = ""; }, 1500);
        }
    } catch(e) {}

    return false;
}

