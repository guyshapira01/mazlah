function setLang(lang,tEng,tGer)
{
	if (lang == 'English' || lang == 'Global') 
	{
		document.write(tEng);
	} 
	else 
	{
		document.write(tGer);
	}

}

function BuildReport()
{
	var xmlObj = document.XMLDocument.documentElement.getElementsByTagName('Object');
	var arr = new Array();
	var arrColName = new Array();
	
	for (var i=0;i<xmlObj.length;i++)
	{
		for (var m=0;m<xmlObj[i].childNodes.length;m++)
		{
			var bfound = false;
			var curColname = xmlObj[i].childNodes[m].tagName;
			if (curColname != 'ID' && curColname != 'KEY')
			{
				var curCol = xmlObj[i].childNodes[m].childNodes[0].nodeValue;
				for (var j=0;j< arr.length;j++)
				{
					if (curCol==arr[j])
					{
						bfound = true;
					}
				}
				if (!bfound)
				{
					arr[arr.length] = curCol;
					arrColName[arrColName.length] = curColname;
				}
			}
		}
	}
	
	var sOutput = "<table width=100% bgcolor=white border=1 bordercolor=black cellPadding=2 cellSpacing=0 align=center dir=ltr>";
	sOutput += "<tr class=ToolBarTableSubHeader>";
	sOutput += "<td>name</td>";
	for (var i=0;i<arr.length;i++)
	{
		val = arr[i].toString();
		val = "<td>" + val + "</td>";
		sOutput += val;
	}
	sOutput += "</tr>";
	
	for (var i=0;i<xmlObj.length;i++)
	{
		obj1 = xmlObj[i].getElementsByTagName("ID")[0].childNodes[0].nodeValue;
		sOutput += "<tr>";
		sOutput += "<td>" + obj1 + "</td>";
		for (var j=0;j<arrColName.length;j++)
		{
			obj = xmlObj[i].getElementsByTagName(arrColName[j]);
			val = "";
			if (obj.length>0)
			{
				//val = obj[0].childNodes[0].nodeValue + " " + obj1;
				val = "<input type=checkbox checked=true>";
			}
			else
			{
				val = "<input type=checkbox>";
			}
			
			sOutput += "<td>" + val + "</td>";
		}
		sOutput += "</tr>";
	}
	
	sOutput += "</table>";
	return sOutput;
}

function NewNodeTransfer()
{
	SaveObjToSession();
	//CopyObj('self');
	CopyObjAJAX('self');
	var temp = document.location.href;
	var sPasteIn = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	var template = temp.substring(temp.indexOf("Template=",0)+9,temp.indexOf("&",temp.indexOf("Template=")));
	
	var catalog = GetCatalog();
	var sClass = '';
	var sTypeName = '';
	var PasteOption = '1';
	var KeepOccur = '';
	var DisablePasteOptions = '';
	
	var parentFather = GetParent(sPasteIn,catalog);
	var sPasteUrl = 'Paste.aspx?Pkey='+ sPasteIn + '&PasteCatalog=' + catalog + '&Class=' + sClass + '&TypeName=' + sTypeName + '&PasteOption=' + PasteOption + '&KeepOccur=' + KeepOccur + '&ParentKey=' + parentFather + '&DisablePasteOptions=' + DisablePasteOptions;
	//var w = window.open('Paste.aspx?Pkey='+ sPasteIn + '&PasteCatalog=' + catalog + '&Class=' + sClass + '&TypeName=' + sTypeName + '&PasteOption=' + PasteOption + '&KeepOccur=' + KeepOccur + '&ParentKey=' + parentFather + '&DisablePasteOptions=' + DisablePasteOptions,'PasteObj','screenX=0,screenY=0,scrollbars=yes,toolbar=no,width=420,top=0,left=0,height=520,location=no,menubar=no');
	
	OpenURLAjax(sPasteUrl);
		
	var newkey = GetKeyFromSession('NewObjectPkey');
		
	var w = window.open('Showtemplate.aspx?Template='+template+'&Pkey='+newkey+'&ParentKey='+ sPasteIn +'&Type=10&PkeyCatalog=' + catalog+'&ShowIndex=False','NewFlight','');
	document.location.href = document.location.href;
	//top.frames['Index'].LinkSwitch(sPasteIn,parentFather);
}

function GetCatalog()
{
	var fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.toUpperCase().indexOf("PKEYCATALOG=",0)==-1)	
	{
		if (top.frames.length==0)	
		{
			catalog = document.getElementById("catkey").value;
		}
		else	
		{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	
	{
		catalog = fatherObject2.substring(fatherObject2.toUpperCase().indexOf("PKEYCATALOG=",0)+12,fatherObject2.length);	
	}
	
	return catalog;
}

function OpenURLAjax(sUrl)
{
	var xmlHttpObj = new ActiveXObject('Microsoft.XMLHTTP');
		xmlHttpObj.open('GET',sUrl, false);
		xmlHttpObj.setRequestHeader('Pragma','no-cache');
		xmlHttpObj.setRequestHeader('Cache-control','no-cache');
		xmlHttpObj.send();
		var resp = xmlHttpObj.responseText;
		return resp;
}

function GetParent(divID,cat,mode)
{
	if (typeof(mode) == "undefined")
			mode = "";
			
	var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpRem.open("GET", "../OnlineSearchRedirect.aspx?pkey=" + divID+"&Mode=LinkSwitch&Pkeycatalog="+cat, false);
				xmlHttpRem.setRequestHeader("Pragma","no-cache");
				xmlHttpRem.setRequestHeader("Cache-control","no-cache");
				xmlHttpRem.send();
	var template = xmlHttpRem.responseText;

	var strArr = template.split("|");
	template = strArr[0];
	var parent = strArr[1];
	return parent;
}

function GetParentFromReport(divID,cat,mode)
{
	if (typeof(mode) == "undefined")
			mode = "";
			
	var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpRem.open("GET", "OnlineSearchRedirect.aspx?pkey=" + divID+"&Mode=LinkSwitch&Pkeycatalog="+cat, false);
				xmlHttpRem.setRequestHeader("Pragma","no-cache");
				xmlHttpRem.setRequestHeader("Cache-control","no-cache");
				xmlHttpRem.send();
	var template = xmlHttpRem.responseText;

	var strArr = template.split("|");
	template = strArr[0];
	var parent = strArr[1];
	return parent;
}

function QuestionReportNext()
	{
			//window.open('../GenerateReport.aspx?Catalog=' + pkeycatalog + '&Mode=FullSQL&XSL=questionsreport.xsl&SQL=select * from (select distinct t_cat_part.*,t_cat_nodes.pparentkey from t_cat_part,t_cat_nodes where pkeytype=92 and t_cat_nodes.pparentkey=\''+ parentkey +'\' and t_cat_nodes.pkey=t_cat_part.pkey and pglobalstatus<>4 ) tbl1, (select distinct t_cat_part.*,t_cat_nodes.pparentkey from t_cat_part,t_cat_nodes where pkeytype=89 and t_cat_nodes.pparentkey=\''+ parentkey +'\' and t_cat_nodes.pkey=t_cat_part.pkey and pglobalstatus<>4 ) tbl2 where tbl1.pprop11=tbl2.pprop9 and (tbl1.pprop12<>tbl2.pprop8 or tbl2.pprop8 is null)','Questionnaire','width=800,height=600,scrollbars=yes,statusbar=no');
			document.location.href = 'GenerateReport.aspx?Catalog=' + pkeycatalog + '&Mode=FullSQL&XSL=questionsreport2.xsl&SQL=select * from t_cat_part where pkey in (select distinct tbl1.pkey from (select distinct t_cat_part.*,t_cat_nodes.pparentkey from t_cat_part,t_cat_nodes where pkeytype=92 and t_cat_nodes.pparentkey=\''+ parentkey +'\' and t_cat_nodes.pkey=t_cat_part.pkey and pglobalstatus<>4 ) tbl1, (select distinct t_cat_part.*,t_cat_nodes.pparentkey from t_cat_part,t_cat_nodes where pkeytype=89 and t_cat_nodes.pparentkey=\''+ parentkey +'\' and t_cat_nodes.pkey=t_cat_part.pkey and pglobalstatus<>4 ) tbl2 where (tbl1.pprop11=tbl2.pprop9 or tbl1.pprop11 is null) and (tbl1.pprop12<>tbl2.pprop8 or tbl2.pprop8 is null or tbl1.pprop12 is null))';
	}
			
function LoadSelectOptions()
{
	
	var ea = document.getElementsByTagName('SELECT');
	for (var i = 0; i < ea.length; i++)
	 {
		var e = ea[i];
		if (e.name.indexOf('LWPKEY_') != -1)
		 {
			if (e.propval.indexOf('|') != -1)
			{
				var vals = e.propval.split('|');
				for (j=0;j<vals.length; j++)
					{
						if (vals[j] != '')
						{
							var oOption = document.createElement("OPTION");
							oOption.text=vals[j];
							oOption.value=vals[j];
							if (e.selectedval == vals[j])
								oOption.selected = true;
							e.add(oOption);
						}
					}
			}
		 }
	}	
}

function AddAnswer()
{
	var oOption = document.createElement("OPTION");
	oOption.text=NewObject._exclude_text.value;
	oOption.value=NewObject._exclude_text.value;
	NewObject._exclude_PPROP7.add(oOption);
	
	NewObject._exclude_text.value = "";
	
	WriteAnswersToField();
}

function RemoveAnswer()
{
	if (NewObject._exclude_PPROP7.selectedIndex != -1)
		NewObject._exclude_PPROP7.options.remove(NewObject._exclude_PPROP7.selectedIndex);
		
		WriteAnswersToField();
}

function WriteAnswersToField()
{
	NewObject.PPROP7.value = "";
	var leng = NewObject._exclude_PPROP7.options.length;
	for (i=0;i<leng; i++)
	{
		NewObject.PPROP7.value += NewObject._exclude_PPROP7.options[i].value + "|";
	}
}

function LoadSelectValues()
{
	var vals = NewObject.PPROP7.value.split('|');
	for (i=0;i<vals.length; i++)
	{
		if (vals[i] != '')
		{
			var oOption = document.createElement("OPTION");
			oOption.text=vals[i];
			oOption.value=vals[i];
			NewObject._exclude_PPROP7.add(oOption);
		}
	}
}


function CopyUrl()
{
	var href = document.location.href;
	var pkey = href.substring(href.indexOf("Pkey=",0)+5,href.indexOf("&",href.indexOf("Pkey=")));
	var parentkey = href.substring(href.indexOf("ParentKey=",0)+10,href.indexOf("&",href.indexOf("ParentKey=")));
	var cat;
	
	if (top.frames.length != 0)
		cat = top.frames('Index').document.getElementById('catkey').value;
		
	var link = "OnlineSearchRedirect.aspx?pkey=" + pkey + "&pparentkey="+ parentkey +"&pkeycatalog="+cat;
	link = "http:\/\/200.200.39.87\/LinkwareWebEditor\/"  + link
	holdtext.innerText = link;
	Copied = holdtext.createTextRange();
	Copied.execCommand('Copy');
	//window.external.AddFavorite(link, link);
	//window.open(link);
	
	return(link);

}

function getLoc(mode)
{
	var href2 = document.location.href;
	if (mode == 1)
	{
		var parentParse = href2.substring(href2.indexOf("Pkey=",0)+5,href2.indexOf("&",href2.indexOf("Pkey=")));
		return parentParse;
	}
	
	if (mode == 0)
	{
		var keyParse = href2.substring(href2.indexOf("Object=",0)+7,href2.indexOf("&",href2.indexOf("Object=")));
		return keyParse;
	}
	 
	if (mode == 2)
	{
		var href3 = window.opener.top.document.location.href;
		var pkeycatalogParse = href3.substring(href3.indexOf("PkeyCatalog=",0)+12,href3.length);
		return pkeycatalogParse;
	}
}

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
 

// ===== Chrome-compatible highlighting / persistent selection =====

var selectedHotspotId = null;
var hoveredHotspotId = null;

function getFrameByNameSafe(frameName) {
    try {
        return top.frames[frameName] || null;
    } catch (e) {
        return null;
    }
}

function findPartRow(obj_id) {
    var tableFrame = getFrameByNameSafe('Parts Table');

    if (tableFrame && tableFrame.document) {
        var row = tableFrame.document.getElementById(obj_id);
        if (row) return row;
    }

    var localRow = document.getElementById(obj_id);
    if (localRow && localRow.tagName &&
        localRow.tagName.toUpperCase() === 'TR') {
        return localRow;
    }

    return null;
}

function setRowHighlight(obj_id, selected, color) {
    var row = findPartRow(obj_id);
    if (!row) return;

    // Remove our runtime classes first.
    row.classList.remove('hotspot-hover');
    row.classList.remove('hotspot-selected');

    if (selected) {
		if (String(color).toUpperCase() === '#B6FF00') {
            row.classList.add('hotspot-selected');
        } else {
            row.classList.add('hotspot-hover');
        }

        try {
            row.scrollIntoView({ block: 'nearest', inline: 'nearest' });
        } catch (e) {
            row.scrollIntoView(false);
        }
    }
}

function findAreaByPartId(obj_id) {
    var areas = document.getElementsByTagName('area');

    for (var i = 0; i < areas.length; i++) {
        var area = areas[i];
        var areaName = area.getAttribute('name') || '';

        // Generated names look like:
        // 34434a578_ קישור חדש 34434a102
        if (areaName === obj_id ||
            areaName.indexOf(obj_id + '_') === 0 ||
            areaName.indexOf(obj_id + ' ') === 0) {
            return area;
        }
    }

    return null;
}

function getPartIdFromArea(area) {
    if (!area) return null;

    var areaName = area.getAttribute('name') || '';
    if (areaName) {
        var underscore = areaName.indexOf('_');
        if (underscore > 0) return areaName.substring(0, underscore).trim();

        var space = areaName.indexOf(' ');
        if (space > 0) return areaName.substring(0, space).trim();
    }

    return null;
}

function getMapImage() {
    return document.getElementById('myimg');
}


function withAreaColor(color, callback) {
    var img = getMapImage();
    if (!img || !img.options) {
        return callback();
    }

    var oldColor = img.options['areacolor'];
    img.options['areacolor'] = color;

    try {
        return callback();
    } finally {
        img.options['areacolor'] = oldColor;
    }
}

function showSelectedAreaHighlight(obj_id) {
    // Persistent/clicked selection = light green.
	return withAreaColor('#B6FF00', function() {
        return showAreaHighlight(obj_id);
    });
}

function showAreaHighlight(obj_id) {
    var area = findAreaByPartId(obj_id);
    var img = getMapImage();

    if (!area || !img) return false;

    try {
        // cvi_map library uses canvas in Chrome.
        if (typeof extAreaOver === 'function') {
            extAreaOver(img.id, area.id);
            return true;
        }
    } catch (e) {
        if (window.console) console.error('showAreaHighlight failed', e);
    }

    return false;
}

function clearAreaHighlight(obj_id) {
    var area = findAreaByPartId(obj_id);
    var img = getMapImage();

    if (!area || !img) return;

    try {
        if (typeof extAreaOut === 'function') {
            extAreaOut(img.id, area.id);
        }
    } catch (e) {
        if (window.console) console.error('clearAreaHighlight failed', e);
    }
}

function restoreSelectedHotspot() {
    if (!selectedHotspotId) return;

	setRowHighlight(selectedHotspotId, true, '#B6FF00');
    showSelectedAreaHighlight(selectedHotspotId);
}

// Hover from an AREA or from the right table.
function highlight(obj_id) {
    hoveredHotspotId = obj_id;

    // Temporary hover highlight on the row.
    // If this is already the selected hotspot, keep the persistent green color.
    if (selectedHotspotId === obj_id) {
		setRowHighlight(obj_id, true, '#B6FF00');
    } else {
        setRowHighlight(obj_id, true, 'yellow');
    }

    // Keep the clicked hotspot visible, if one exists.
    if (selectedHotspotId && selectedHotspotId !== obj_id) {
        restoreSelectedHotspot();
    }

    // Add the currently hovered hotspot temporarily in yellow.
    if (selectedHotspotId !== obj_id) {
        showAreaHighlight(obj_id);
    }
}

function highlightIndex(obj_id) {
    highlight(obj_id);
}

// Mouse-out: remove only temporary hover.
// A clicked/selected hotspot remains highlighted.
function unlight(obj_id) {
    if (hoveredHotspotId === obj_id) {
        hoveredHotspotId = null;
    }

    if (selectedHotspotId === obj_id) {
        restoreSelectedHotspot();
        return;
    }

    setRowHighlight(obj_id, false);
    clearAreaHighlight(obj_id);

    // clearAreaHighlight clears the shared canvas, so repaint the selected hotspot.
    restoreSelectedHotspot();
}

// Persistent selection.
// Selecting a new hotspot clears the previous selection.
function selectHotspot(obj_id) {
    if (!obj_id) return;

    if (selectedHotspotId && selectedHotspotId !== obj_id) {
        setRowHighlight(selectedHotspotId, false);
        clearAreaHighlight(selectedHotspotId);
    }

    selectedHotspotId = obj_id;

	setRowHighlight(obj_id, true, '#B6FF00');
    showSelectedAreaHighlight(obj_id);
}

// Backward-compatible name used by the table.
function highlightAreaByName(obj_id) {
    selectHotspot(obj_id);
}

// Backward-compatible right-table selection.
function highlightClick(obj_id) {
    var partsMapFrame = getFrameByNameSafe('Parts Map');

    if (partsMapFrame && typeof partsMapFrame.selectHotspot === 'function') {
        partsMapFrame.selectHotspot(obj_id);
    } else {
        selectHotspot(obj_id);
    }
}

function Focus(obj_id) {
    var row = findPartRow(obj_id);
    if (!row) return;

    try {
        row.scrollIntoView({ block: 'nearest', inline: 'nearest' });
    } catch (e) {
        row.scrollIntoView(false);
    }
}

// The generated <AREA> tags have no onclick.
// Bind click handlers after the Parts Map frame finishes loading.
function bindHotspotClicks() {
    var areas = document.getElementsByTagName('area');

    for (var i = 0; i < areas.length; i++) {
        (function(area) {
            if (area.getAttribute('data-chrome-click-bound') === '1') return;

            area.setAttribute('data-chrome-click-bound', '1');

            var chooseArea = function(e) {
                if (e) {
                    e.preventDefault();
                    e.stopPropagation();
                }

                var obj_id = getPartIdFromArea(area);
                if (obj_id) {
                    selectHotspot(obj_id);

                    // AREA mouseout / canvas handlers may repaint immediately after click.
                    // Repaint the persistent green selection after the current event finishes.
                    window.setTimeout(function() {
                        restoreSelectedHotspot();
                    }, 0);
                }

                return false;
            };

            // Capture phase makes this run before older image-map handlers.
            area.addEventListener('click', chooseArea, true);
        })(areas[i]);
    }
}


function hotspotClick(obj_id) {
    selectHotspot(obj_id);
    window.setTimeout(function() {
        restoreSelectedHotspot();
    }, 0);
    return false;
}


function bindPersistentHotspotMouseDown() {
    document.addEventListener('mousedown', function(e) {
        if (!hoveredHotspotId) return;

        // Only the left/frame image area uses hoveredHotspotId.
        // Persist the currently hovered hotspot immediately on mouse-down.
        selectHotspot(hoveredHotspotId);

        // Repaint after legacy handlers finish.
        window.setTimeout(function() {
            restoreSelectedHotspot();
        }, 0);
    }, true);
}

if (window.addEventListener) {
    window.addEventListener('load', bindHotspotClicks, false);
    window.addEventListener('load', bindPersistentHotspotMouseDown, false);
}

// ===== End Chrome-compatible highlighting / persistent selection =====

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

function ResizeTable()
{
	if (document.getElementById('ContentList').offsetHeight>(document.body.offsetHeight-90))
	{
		document.getElementById('ContentList').style.overflowY = 'scroll';
		document.getElementById('ContentList').style.height=(document.body.offsetHeight-90) + 'px';
	}
	else
	{
		document.getElementById('ContentList').style.overflowY = 'hidden';
	}
}

function ResizeTableHeight()
{
	iInterval = window.setInterval(function()
									{
										if (document.body.offsetHeight>90) 
											{
												ResizeTable();
												clearInterval(iInterval);
											}
									},20);
}
