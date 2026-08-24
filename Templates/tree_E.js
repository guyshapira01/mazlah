function addToCookieDiv()
{
	//if(ExpandAllBool=='2')
	//{
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
		//alert("Add To Cookie");
		//alert(ExpandDiv.length);
		//alert(ExpandDiv[0]);
			   //var fso, s;
			    //fso = new ActiveXObject("Scripting.FileSystemObject");
			    //s = fso.OpenTextFile("C:\\test.txt" , 2, 1, -2);
		var j=0;
		for(var i=0;i<ExpandDiv.length;i++) 
			{
				//alert(ExpandDiv[i]);
				//alert('ExpandDiv' + i);
				if (ExpandDiv[i] != "noneID") 	{
					setCookie (catalogNumber+'ExpandDiv' + j, ExpandDiv[i], expdate);
					//s.writeline("name:" + catalogNumber+'ExpandDiv' + j + " value:" + ExpandDiv[i]);
					j++;
				}

			}
			    //s.Close();
		setCookie(catalogNumber+'scrollTopPos', window.document.body.scrollTop, expdate);
		//alert(window.document.body.scrollTop)
	//}
}
function addToCookie(value,name)
{
		var expdate = new Date();
		expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
		setCookie(name, value, expdate);
}
function getFromCookie(name)
	{
		var i='';
		i=getCookie(name) 
	return i
	}
function getFromCookieDiv()
	{
			var i=0;
			var PictureName;
			//alert(getCookie(catalogNumber+'ExpandDiv'+i));
			if (! isCookieLoaded){
				//alert('bring from cookie');
				//alert(getCookie(catalogNumber+'ExpandDiv'+i));
				while ( getCookie(catalogNumber+'ExpandDiv'+i) != null ) {
					//alert(getCookie(catalogNumber+'ExpandDiv'+i) );
					PictureName = getCookie(catalogNumber+'ExpandDiv'+i);
					PictureName = PictureName.substr(PictureName.indexOf("_")+1);
					//alert(i);
					Switch(getCookie(catalogNumber+'ExpandDiv'+i), PictureName);
					delCookie(catalogNumber+'ExpandDiv'+i);
					//alert(getCookie(catalogNumber+'ExpandDiv'+i));
					i++;


					window.document.body.scrollTop = getCookie(catalogNumber+'scrollTopPos');
					//alert(window.document.body.scrollTop);
					delCookie(catalogNumber+'scrollTopPos');
				}
				isCookieLoaded=true;
			}else {
				//alert('bring from array');
				var PicrureName='';
				for (var i=0;i<ExpandDiv.length;i++)
				{
								//alert (ExpandDiv[i] );
					PictureName = ExpandDiv[i];
					PictureName = PictureName.substr(PictureName.indexOf("_")+1);
					OpenExpand(ExpandDiv[i],PictureName);
					//Switch(ExpandDiv[i] , PictureName);
					//delCookie(catalogNumber+'ExpandDiv'+i);
								
				}
				
			}
			
		
	}
	function deleteAllValuesFromCookies()
	{
		for (var i=0;i<ExpandDiv.length;i++)
		{
			delCookie(catalogNumber+ExpandDiv[i] );	
			//removeFromArrayDiv(ExpandDiv[i] );
		}
		 ExpandDiv =  new Array();
	}
function addToArrayDiv(id)
	{
		//alert("Add To Array")
		ExpandDiv[ExpandDiv.length] = id;
		
		if (document.all(id).length>1)
			{
			for(i=0;i<document.all(id).length;i++)
				{
				ExpandDivStyle[ExpandDiv.length] = document.all(id)(i).style.display;
				}
			}
		else
			{
			ExpandDivStyle[ExpandDiv.length] = document.all(id).style.display;
			}
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
function Switch_OLD(divID, PictureName, ClosePicture, OpenPicture){
	//alert('close' + ClosePicture);
	//alert('open:' + OpenPicture);
	if ( document.all(divID)+"" != "null" )   {
		
		if (document.all(divID).length>1)
		{
			for(ij=0;ij<document.all(divID).length;ij++)
			{
				var sty = document.all(divID)(ij).style; 
				sty.display = sty.display==''?'none':'';
			}
		}
		else
		{
			var sty = document.all(divID).style; 
			sty.display = sty.display==''?'none':'';
		}
		
		if(ExpandStatus=='2'){
			if (sty.display != "none")
				{
					addToArrayDiv(divID);
				}
			else
				{
					removeFromArrayDiv(divID);
				}
		}
		if (sty.display == "none") 
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

function RefreshFrames()
{
	if (top.frames.length == 0)
	{
		top.document.location.href = top.document.location.href;
		return;
	}
	
	for (var j = 0; j< top.frames.length; j++)
	{
		var nFrame = top.frames[j].name;
		if (nFrame != 'Index')
		{
			top.frames[j].document.location.href = top.frames[j].document.location.href;
		}
	}

}

function GetHREF(divID,template,catalog,parent)
{
		
		var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
			xmlHttpRem.open("GET", "../SaveSession.aspx?ID=" + divID + "&Parent=" + parent + "&Op=click&Catalog="+catalog, false);
			xmlHttpRem.setRequestHeader("Pragma","no-cache");
			xmlHttpRem.setRequestHeader("Cache-control","no-cache");
			xmlHttpRem.send();
			xmlHttpRem = null;
		//alert(parent);
		//alert(template +"--"+top.frames.length);		
		if (template != "normal_WebLink_E.html" && template != "normal_file_TAAS_E.html")
		{
		if (top.frames.length==0)
		{
			top.document.location.href="ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
			
		} 
	else 
		{
		var xmlHttpTP = new ActiveXObject("Microsoft.XMLHTTP");
		xmlHttpTP.open("GET", "GetFrames.aspx?Template="+template, false);
		xmlHttpTP.setRequestHeader("Pragma","no-cache");
		xmlHttpTP.setRequestHeader("Cache-control","no-cache");
		xmlHttpTP.send();
		
		/*
		for (var ij =0; ij < top.frames.length;ij++)
		{
			alert(top.frames[ij].name);
			}
			*/
			
		if (xmlHttpTP.responseText != "empty")
			{
				var strArr = xmlHttpTP.responseText.split('|');
				var fcont = "";
				var fname ="";
				var counter = 0;
				for (var j = 0; j< top.frames.length; j++)
				{
					var nFrame = top.frames[j].name;
					var bx = false;
					if (nFrame != 'Index')
					{
						for (var i = 1; i < strArr.length ;i++)
						{
							var pictext = strArr[i].substring(3,strArr[i].length);
							fname = strArr[i].substring(strArr[i].indexOf("(")+1,strArr[i].indexOf(")"));
							fcont = strArr[i].substring(strArr[i].indexOf(")")+1);
							if (fname == nFrame)
								bx = true;
						}
						if (bx)
						{
							counter++;
							bx = false;
						}
						else
						{
							top.document.location.href="ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
							return;
						}
					}					
				}
				
				if (counter != top.frames.length -1)
				{
					alert('counter=' + counter);
					top.document.location.href="ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
					return;	
				}
				
				for (var i = 1; i < strArr.length ;i++)
				{
					var pictext = strArr[i].substring(3,strArr[i].length);
					fname = strArr[i].substring(strArr[i].indexOf("(")+1,strArr[i].indexOf(")"));
					fcont = strArr[i].substring(strArr[i].indexOf(")")+1);
					try
					{
						if (typeof(top.frames[fname]) != 'undefined')
						{
							top.frames(fname).document.location.href="ShowTemplate.aspx?Template="+fcont+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
						}
						else
						{
							top.document.location.href="ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
							return;
						}
							
					}
					catch(err)
					{
						alert('Template Exception Occurred, Click OK to Reload Page');
						top.document.location.href="ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
						return;
					}
				}
		top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey); 
		top.frames('Index').currentHlighlighPkey=divID+'a';
		top.frames('Index').highlightIndex(divID+'a'); 
		xmlHttpTP = null;
		}
		}
	}
		else
		{
			window.open("ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog);
		}			
}

function GetHREFreport(divID,template,catalog,parent)
{
		
		var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
			xmlHttpRem.open("GET", "../SaveSession.aspx?ID=" + divID + "&Parent=" + parent + "&Op=click&Catalog="+catalog, false);
			xmlHttpRem.setRequestHeader("Pragma","no-cache");
			xmlHttpRem.setRequestHeader("Cache-control","no-cache");
			xmlHttpRem.send();
			xmlHttpRem = null;
		//alert(parent);
		//alert(template +"--"+top.frames.length);		
		if (template != "normal_WebLink_E.html" && template != "normal_file_TAAS_E.html")
		{
		if (top.frames.length==0)
		{
			top.document.location.href="Templates/ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
			
		} 
	else 
		{
		var xmlHttpTP = new ActiveXObject("Microsoft.XMLHTTP");
		xmlHttpTP.open("GET", "GetFrames.aspx?Template="+template, false);
		xmlHttpTP.setRequestHeader("Pragma","no-cache");
		xmlHttpTP.setRequestHeader("Cache-control","no-cache");
		xmlHttpTP.send();
		
		/*
		for (var ij =0; ij < top.frames.length;ij++)
		{
			alert(top.frames[ij].name);
			}
			*/
			
		if (xmlHttpTP.responseText != "empty")
			{
				var strArr = xmlHttpTP.responseText.split('|');
				var fcont = "";
				var fname ="";
				var counter = 0;
				for (var j = 0; j< top.frames.length; j++)
				{
					var nFrame = top.frames[j].name;
					var bx = false;
					if (nFrame != 'Index')
					{
						for (var i = 1; i < strArr.length ;i++)
						{
							var pictext = strArr[i].substring(3,strArr[i].length);
							fname = strArr[i].substring(strArr[i].indexOf("(")+1,strArr[i].indexOf(")"));
							fcont = strArr[i].substring(strArr[i].indexOf(")")+1);
							if (fname == nFrame)
								bx = true;
						}
						if (bx)
						{
							counter++;
							bx = false;
						}
						else
						{
							top.document.location.href="Templates/ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
							return;
						}
					}					
				}
				
				if (counter != top.frames.length -1)
				{
					alert('counter=' + counter);
					top.document.location.href="Templates/ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
					return;	
				}
				
				for (var i = 1; i < strArr.length ;i++)
				{
					var pictext = strArr[i].substring(3,strArr[i].length);
					fname = strArr[i].substring(strArr[i].indexOf("(")+1,strArr[i].indexOf(")"));
					fcont = strArr[i].substring(strArr[i].indexOf(")")+1);
					try
					{
						if (typeof(top.frames[fname]) != 'undefined')
						{
							top.frames(fname).document.location.href="Templates/ShowTemplate.aspx?Template="+fcont+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
						}
						else
						{
							top.document.location.href="Templates/ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
							return;
						}
							
					}
					catch(err)
					{
						alert('Template Exception Occurred, Click OK to Reload Page');
						top.document.location.href="Templates/ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog;
						return;
					}
				}
		top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey); 
		top.frames('Index').currentHlighlighPkey=divID+'a';
		top.frames('Index').highlightIndex(divID+'a'); 
		xmlHttpTP = null;
		}
		}
	}
		else
		{
			window.open("Templates/ShowTemplate.aspx?Template="+template+"&Pkey="+divID+"&ParentKey="+parent+"&Type=10&PkeyCatalog="+catalog);
		}			
}


function LinkSwitchParent(divID,mode)
{
	if (typeof(mode) == "undefined")
			mode = "";
			
	var cat = document.getElementById("catkey").value;
	var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpRem.open("GET", "../OnlineSearchRedirect.aspx?pkey=" + divID+"&Mode=LinkSwitch&Pkeycatalog="+cat, false);
				xmlHttpRem.setRequestHeader("Pragma","no-cache");
				xmlHttpRem.setRequestHeader("Cache-control","no-cache");
				xmlHttpRem.send();
	var template = xmlHttpRem.responseText;

	var strArr = template.split("|");
	template = strArr[0];
	var parent = strArr[1];
	//alert(template+ " " + parent);
	//alert(divID + "--" + template);
	xmlHttpRem.open("GET", "../OnlineSearchRedirect.aspx?pkey=" + parent+"&Mode=LinkSwitch&Pkeycatalog="+cat, false);
	xmlHttpRem.setRequestHeader("Pragma","no-cache");
	xmlHttpRem.setRequestHeader("Cache-control","no-cache");
	xmlHttpRem.send();
	var template2 = xmlHttpRem.responseText;
	
	strArr = template2.split("|");
	template2 = strArr[0];
	var parentnew = strArr[1];
	
	if (top.frames.length != 0)
		SwitchEnter(parent,parent,'FolderClose_E.gif','FolderOpen_E.gif',cat,parentnew);
	
	if (mode != "del")
	{
		GetHREF(divID,template,cat,parent);
	}
	else
	{
		GetHREF(parent,template2,cat,parentnew);
	}
	xmlHttpRem = null;
	return parent;
}


function LinkSwitch(divID,parent)
{
	var getbool = "false";
	var sParent;
	
	if (typeof(parent) == "undefined")
	{
		getbool = "true";
	}
	else
	{
		if (parent == "")
			getbool="true"
	}
			
	var cat = document.getElementById("catkey").value;
	var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpRem.open("GET", "../OnlineSearchRedirect.aspx?pkey=" + divID+"&Mode=LinkSwitch&Pkeycatalog="+cat, false);
				xmlHttpRem.setRequestHeader("Pragma","no-cache");
				xmlHttpRem.setRequestHeader("Cache-control","no-cache");
				xmlHttpRem.send();
	var template = xmlHttpRem.responseText;
				xmlHttpRem = null;

	var strArr = template.split("|");
	template = strArr[0];
	var parent1 = strArr[1];
	
	if (getbool == "true")
	{
		sParent = parent1;
	}
	else
	{
		sParent = parent;
	}	
	//alert(parent1+ "-" + parent);
	//alert(divID + "--" + template);
	
	if (top.frames.length != 0)
		SwitchEnter(divID,divID,'FolderClose_E.gif','FolderOpen_E.gif',cat,sParent);
	
	GetHREF(divID,template,cat,sParent);
	//alert(divID + "--" + sParent + "--" + cat);

}

function LinkSwitchFromParent(divID,parent)
{
	var getbool = "false";
	var sParent;
	
	if (typeof(parent) == "undefined")
	{
		getbool = "true";
	}
	else
	{
		if (parent == "")
			getbool="true"
	}
			
	var cat = document.getElementById("catkey").value;
	var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpRem.open("GET", "../OnlineSearchRedirect.aspx?pkey=" + divID+"&Mode=LinkSwitch&Pkeycatalog="+cat, false);
				xmlHttpRem.setRequestHeader("Pragma","no-cache");
				xmlHttpRem.setRequestHeader("Cache-control","no-cache");
				xmlHttpRem.send();
	var template = xmlHttpRem.responseText;
				xmlHttpRem = null;

	var strArr = template.split("|");
	template = strArr[0];
	var parent1 = strArr[1];
	
	if (getbool == "true")
	{
		sParent = parent1;
	}
	else
	{
		sParent = parent;
	}	
	//alert(parent1+ "-" + parent);
	//alert(divID + "--" + template);
	
	if (top.frames.length != 0)
		SwitchEnter(divID,divID,'FolderClose_E.gif','FolderOpen_E.gif',cat,sParent);
	
	GetHREFreport(divID,template,cat,sParent);
	//alert(divID + "--" + sParent + "--" + cat);

}

function Switch(divID, PictureName, ClosePicture, OpenPicture, LevelNum, CatalogKey,dOpen,parent){

	if (typeof(dOpen) == "undefined")
	{
		dOpen = "false";
	}
	else
	{
		if (dOpen == "")
			dOpen = "false";
	}
	
		
	var allopen = true;
	
	if ( document.all(divID)+"" != "null" )
	{
		if (document.all(divID).length > 0)
		{
			for (var t=0;t < document.all(divID).length;t++)
			{
				if (document.all(divID)(t).style.display != '')
				{
					var sty = document.all(divID)(t).style; 
					var key = divID.substring(6,divID.length);

					//alert(divID+" "+LevelNum+" "+CatalogKey);
					var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");//("Microsoft.XMLDOM");Msxml2.DomDocument.3.0
					//xmlDoc.setProperty("NewParser", true);
					xmlDoc.validateOnParse = true;
					xmlDoc.async = false;
					var hr = xmlDoc.load("../index.aspx?Pkey=" + key + "&Type=10&Level=" + LevelNum + "&PkeyCatalog=" + CatalogKey);
			
			/*
			if (!hr)
				alert("errorload");
				*/
		
					var xslDoc = new ActiveXObject("Microsoft.XMLDOM");
					//xslDoc.setProperty("NewParser", true);
					xslDoc.validateOnParse = true;
					xslDoc.async = false;
			
					var href;
					var lastkey;
					if (top.frames.length == 0)
					{
						href = top.document.location.href;
					}
					else
					{
						href = top.frames('Index').location.href;
				
					}
			
			 
					var ind = href.lastIndexOf("Template=");
					var temp = href.substring(ind,href.length);
					ind = temp.indexOf("&");
					var template = temp.substring(9,ind);
			
					var xmlHttpTP = new ActiveXObject("Microsoft.XMLHTTP");
					xmlHttpTP.open("GET", "GetFrames.aspx?Template="+template+"&XSL=True", false);
					xmlHttpTP.setRequestHeader("Pragma","no-cache");
					xmlHttpTP.setRequestHeader("Cache-control","no-cache");
					xmlHttpTP.send();
			
					var xslTemplate = xmlHttpTP.responseText;
					xmlHttpTP = null;
			
					hr = xslDoc.load(xslTemplate);
			
			/*
			if (!hr)
				alert("errorloadxsl");
				*/
				
					if (xmlDoc.text == "")
					{
					//document.getElementById(divID).innerHTML = "";
					changeImages(PictureName, OpenPicture); 
					if (dOpen == "true")
					{
						if (sty.display=='')
							sty.display = 'none';
						else
							sty.display = '';	
					}
					xmlDoc = null;
					xslDoc = null;
					return;
				}
				else
				{
			
				var x = xmlDoc.transformNode(xslDoc);
			/*
			if (xmlDoc.parseError.errorCode != 0)
				alert("parseerror: "+ xmlDoc.parseError.reason);
				*/
					document.all(divID)(t).innerHTML = "";
					document.all(divID)(t).innerHTML = x;
				}
			
			if (sty.display=='')
			{
				sty.display = 'none';
				
				var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpRem.open("GET", "../SaveSession.aspx?ID=" + key + "&Parent=" + parent + "&Level=" + LevelNum + "&Op=rem&Catalog="+CatalogKey, false);
				xmlHttpRem.setRequestHeader("Pragma","no-cache");
				xmlHttpRem.setRequestHeader("Cache-control","no-cache");
				xmlHttpRem.send();
				xmlHttpRem = null;
				
			}
			else
			{
				sty.display = '';
				
				var xmlHttpAdd = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpAdd.open("GET", "../SaveSession.aspx?ID=" + key + "&Parent=" + parent + "&Level=" + LevelNum + "&Op=add&Catalog="+CatalogKey, false);
				xmlHttpAdd.setRequestHeader("Pragma","no-cache");
				xmlHttpAdd.setRequestHeader("Cache-control","no-cache");
				xmlHttpAdd.send();
				xmlHttpAdd = null;
				if (top.frames.length !=0)
				{
				var lastkey = top.frames('Index').currentHlighlighPkey;
				top.frames('Index').highlight(lastkey); 
				}
			}
			
			
			xmlDoc = null;
			xslDoc = null;
			allopen = false;
			}
			
			}
			if (allopen == true)
			{
				for (var s=0;s<document.all(divID).length;s++)
					{
					document.all(divID)(s).style.display = 'none';
					var NewPictureName = document.all(PictureName)(s).src;
					NewPictureName = NewPictureName.replace("Open","Close");
					document.all(PictureName)(s).src = NewPictureName;
					}
			}
			else
			{
				for (var s=0;s<document.all(divID).length;s++)
					{
					//document.all(divID)(s).style.display = 'none';
					var NewPictureName = document.all(PictureName)(s).src;
					NewPictureName = NewPictureName.replace("Close","Open");
					document.all(PictureName)(s).src = NewPictureName;
					}
			}	
		}
		else
		{
			var sty = document.getElementById(divID).style; 
			var key = divID.substring(6,divID.length);

			//alert(divID+" "+LevelNum+" "+CatalogKey);
			var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");//("Microsoft.XMLDOM");Msxml2.DomDocument.3.0
			//xmlDoc.setProperty("NewParser", true);
			xmlDoc.validateOnParse = true;
			xmlDoc.async = false;
			var hr = xmlDoc.load("../index.aspx?Pkey=" + key + "&Type=10&Level=" + LevelNum + "&PkeyCatalog=" + CatalogKey);
			
			/*
			if (!hr)
				alert("errorload");
				*/
		
			var xslDoc = new ActiveXObject("Microsoft.XMLDOM");
			//xslDoc.setProperty("NewParser", true);
			xslDoc.validateOnParse = true;
			xslDoc.async = false;
			
			var href;
			var lastkey;
			if (top.frames.length == 0)
			{
				href = top.document.location.href;
			}
			else
			{
				href = top.frames('Index').location.href;
				
			}
			
			 
			var ind = href.lastIndexOf("Template=");
			var temp = href.substring(ind,href.length);
			ind = temp.indexOf("&");
			var template = temp.substring(9,ind);
			
			var xmlHttpTP = new ActiveXObject("Microsoft.XMLHTTP");
			xmlHttpTP.open("GET", "GetFrames.aspx?Template="+template+"&XSL=True", false);
			xmlHttpTP.setRequestHeader("Pragma","no-cache");
			xmlHttpTP.setRequestHeader("Cache-control","no-cache");
			xmlHttpTP.send();
			
			var xslTemplate = xmlHttpTP.responseText;
			xmlHttpTP = null;
			
			hr = xslDoc.load(xslTemplate);
			
			/*
			if (!hr)
				alert("errorloadxsl");
				*/
				
			if (xmlDoc.text == "")
			{
				//document.getElementById(divID).innerHTML = "";
				changeImages(PictureName, OpenPicture); 
				if (dOpen == "true")
				{
					if (sty.display=='')
						sty.display = 'none';
					else
						sty.display = '';	
				}
				xmlDoc = null;
				xslDoc = null;
				return;
			}
			else
			{
			
			var x = xmlDoc.transformNode(xslDoc);
			/*
			if (xmlDoc.parseError.errorCode != 0)
				alert("parseerror: "+ xmlDoc.parseError.reason);
				*/
				document.getElementById(divID).innerHTML = "";
				document.getElementById(divID).innerHTML = x;
			}
			
			if (sty.display=='')
			{
				sty.display = 'none';
				
				var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpRem.open("GET", "../SaveSession.aspx?ID=" + key + "&Parent=" + parent + "&Level=" + LevelNum + "&Op=rem&Catalog="+CatalogKey, false);
				xmlHttpRem.setRequestHeader("Pragma","no-cache");
				xmlHttpRem.setRequestHeader("Cache-control","no-cache");
				xmlHttpRem.send();
				xmlHttpRem = null;
				
			}
			else
			{
				sty.display = '';
				
				var xmlHttpAdd = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpAdd.open("GET", "../SaveSession.aspx?ID=" + key + "&Parent=" + parent + "&Level=" + LevelNum + "&Op=add&Catalog="+CatalogKey, false);
				xmlHttpAdd.setRequestHeader("Pragma","no-cache");
				xmlHttpAdd.setRequestHeader("Cache-control","no-cache");
				xmlHttpAdd.send();
				xmlHttpAdd = null;
				if (top.frames.length !=0)
				{
				var lastkey = top.frames('Index').currentHlighlighPkey;
				top.frames('Index').highlight(lastkey); 
				}
			}
			if (sty.display == "none") 
			{ 
				changeImages(PictureName, ClosePicture); 
			} 
			else 
			{ 
				changeImages(PictureName, OpenPicture); 
			}
			
			xmlDoc = null;
			xslDoc = null;
		}
			
	}
}
function ClearSession(catalog)
{
	var xmlHttpClr = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpClr.open("GET", "../SaveSession.aspx?Op=clear&Catalog="+catalog, false);
				xmlHttpClr.setRequestHeader("Pragma","no-cache");
				xmlHttpClr.setRequestHeader("Cache-control","no-cache");
				xmlHttpClr.send();
				xmlHttpClr = null;

}

function FindInSession(pkey,parent,CatalogKey)
{
	var xmlHttpClr = new ActiveXObject("Microsoft.XMLHTTP");
				xmlHttpClr.open("GET", "../SaveSession.aspx?Op=find&ID=" + pkey+"&Parent="+parent+"&Catalog="+CatalogKey, false);
				xmlHttpClr.setRequestHeader("Pragma","no-cache");
				xmlHttpClr.setRequestHeader("Cache-control","no-cache");
				xmlHttpClr.send();
				var res = xmlHttpClr.responseText;
				var level = res.substring(res.indexOf("(")+1,res.indexOf(")"));
				
				xmlHttpClr = null;
		return level;
}

function UpdateNode(pkey,parent,CatalogKey,ClosePicture,OpenPicture)
{
	if (typeof(CatalogKey) == "undefined")
			CatalogKey = document.getElementById("catkey").value;
			
		if (typeof(ClosePicture) == "undefined")
			{
				ClosePicture = 'FolderClose_E.gif';
				OpenPicture = 'FolderOpen_E.gif';
			}
			
		if (parent == "na")
		{
			
			var cat = document.getElementById("catkey").value;
			var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
			xmlHttpRem.open("GET", "../OnlineSearchRedirect.aspx?pkey=" + pkey+"&Mode=LinkSwitch&Pkeycatalog="+cat, false);
			xmlHttpRem.setRequestHeader("Pragma","no-cache");
			xmlHttpRem.setRequestHeader("Cache-control","no-cache");
			xmlHttpRem.send();
			var template = xmlHttpRem.responseText;
				xmlHttpRem = null;

			var strArr = template.split("|");
			template = strArr[0];
			var parent1 = strArr[1];
			
			if (parent1 != "")
			{
				parent = parent1;
			}
			else
			{
				document.location.href = document.location.href;
				return;
			}
		}
		
		var level = "1";
		level = FindInSession(pkey,parent,CatalogKey);
		
		if (level == "")
			level = "1";
		
		try
		{
			if (document.getElementById("inner_"+pkey) == null)
			{
				document.location.href = document.location.href;
				return;
			}
			else
			{
				if (document.getElementById("inner_"+pkey).innerHTML == "")
				{
					document.location.href = document.location.href;
					return;
				}
			}
		}
		catch(err)
		{
			alert("Index Refresh Error:"+ err.description+"\nClick OK to Reload");
			document.location.href = document.location.href;
			return;
		}
		
		if (level != "")
		{
			Switch("inner_"+pkey,pkey,ClosePicture,OpenPicture,level,CatalogKey,"",parent);
			Switch("inner_"+pkey,pkey,ClosePicture,OpenPicture,level,CatalogKey,"",parent);
		}
}

function SessionState(ClosePicture, OpenPicture ,CatalogKey, Level)
{
		if (typeof(CatalogKey) == "undefined")
			CatalogKey = document.getElementById("catkey").value;
			
		if (typeof(ClosePicture) == "undefined")
			{
				ClosePicture = 'FolderClose_E.gif';
				OpenPicture = 'FolderOpen_E.gif';
			}
	//alert(CatalogKey + "--" + ClosePicture);	
	var xmlHttpRet = new ActiveXObject("Microsoft.XMLHTTP");
			xmlHttpRet.open("GET", "../SaveSession.aspx?Op=ret&Catalog="+CatalogKey,false);
			xmlHttpRet.setRequestHeader("Pragma","no-cache");
			xmlHttpRet.setRequestHeader("Cache-control","no-cache");
			xmlHttpRet.send();
			if (xmlHttpRet.responseText != "empty")
			{
				var strArr = xmlHttpRet.responseText.split('|');
				var level;
				var temptext = "";
				for (var i = 1; i < strArr.length ;i++)
				{
					var pictext = "";
					var partext = "";
					if (strArr[i].indexOf("-") == -1)
					{
						pictext = strArr[i].substring(3,strArr[i].length);
					}
					else
					{
						pictext = strArr[i].substring(3,strArr[i].indexOf("-"));
						partext = strArr[i].substring(strArr[i].indexOf("-")+1,strArr[i].length);
					}
					////////////////////////
					//alert(pictext + "-" + partext);
					///////////////////////
					temptext = "inner_" + pictext;
					level = strArr[i].substring(strArr[i].indexOf("(")+1,strArr[i].indexOf(")"));
					if (level == "C")
					{
					if (top.frames.length != 0)
					{
						//alert("frames");
						var fObject = "";
						try
						{
						fObject = top.frames('Header').location.href;
						fObject = fObject.substring(fObject.indexOf("Pkey=",0)+5,fObject.indexOf("&",fObject.indexOf("Pkey=")));
						}
						catch(err)
						{
						fObject = "";
						}
						//alert(pictext + "  -- " + fObject);
						 
						if (fObject != pictext)
						{
							var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
							xmlHttpRem.open("GET", "../OnlineSearchRedirect.aspx?pkey=" + pictext+"&Mode=TempOnly", false);
							xmlHttpRem.setRequestHeader("Pragma","no-cache");
							xmlHttpRem.setRequestHeader("Cache-control","no-cache");
							xmlHttpRem.send();
							var template = xmlHttpRem.responseText;
							//alert(template);
							var cat = document.getElementById("catkey").value;
							xmlHttpRem = null;
							GetHREF(pictext,template,cat,partext);
							top.frames('Index').unlight(fObject);
						}
						else
						{
						top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey); 
						top.frames('Index').highlightIndex(pictext+'a'); 
						top.frames('Index').currentHlighlighPkey=pictext+'a';
						}
					}
					else
					{
					//alert("no frames?");
					}
					}
					else
					{
						Switch(temptext,pictext,ClosePicture,OpenPicture,level,CatalogKey,"",partext);
					}
				}
			}
			else
			{
				if (top.frames.length==0)
				{
					//function expand
					ExpandMost(Level);
				}
				else
				{
					var s = top.location.href;
					var ampos = s.lastIndexOf("&Type");
					var divkey = s.substring(s.indexOf("Pkey=")+5,ampos);
					var catalog;
					//alert(s);
					if (s.toLowerCase().indexOf("pkeycatalog=") != -1)
					{
					  catalog = s.substring(s.toLowerCase().indexOf("pkeycatalog=")+12,s.length);
					  }
					 else
					 {
					  catalog = "";
					  }
					  
					var parent;
					ampos = s.toLowerCase().lastIndexOf("&pkeycatalog=");
					
					if (s.indexOf("pparentkey=") != -1)
					{
						if (ampos != -1)
						parent = s.substring(s.toLowerCase().indexOf("pparentkey=")+11,ampos);
						else
						parent = s.substring(s.toLowerCase().indexOf("pparentkey=")+11,s.length);
					}
					else
					{
						parent = "";
					}
					//alert(catalog + " p " + parent);
				
					SwitchEnter("inner_"+divkey,divkey,ClosePicture,OpenPicture,catalog,parent);
				}
			}
			xmlHttpRet = null;
			
			//alert('end');
}
function CollapseMost(prelevel)
{
		if (typeof(CatalogKey) == "undefined")
			CatalogKey = document.getElementById("catkey").value;
			
		ClosePicture = 'FolderClose_E.gif';
		OpenPicture = 'FolderOpen_E.gif';
			
		var eaf = document.getElementsByTagName('DIV');
		var len = eaf.length;
		var inArr = new Array();
		//return;
		
		eaf = document.getElementsByTagName('DIV');
		len = eaf.length;
				
			for (var b=0;b<eaf.length;b++)
				{
					if (eaf[b].id.length > 5)
					{
					//alert(eaf[b].id);
					if (eaf[b].id.substring(0,6) == "inner_")
						{
						var pic = eaf[b].id.substring(6,eaf[b].id.length);
						//var ea = eaf[b].document.getElementById("href");
						//alert(ea);
						//Switch("inner_"+pic,pic,ClosePicture,OpenPicture,iLev,CatalogKey);
						inArr[inArr.length]=pic;
						}
					}
				}
					
				//timedLoop();
				for (var c=0;c<inArr.length;c++)
				{
				if (document.getElementById("inner_"+inArr[c]) != null)
				{
				if (document.getElementById("inner_"+inArr[c]).style.display == '')
					{
						//alert(inArr[c]);
						//setTimeout("Switch('inner_"+inArr[c]+"','"+inArr[c]+"','"+ClosePicture+"','"+OpenPicture+"','"+iLev+"','"+CatalogKey+"');",1000);
						Switch("inner_"+inArr[c],inArr[c],ClosePicture,OpenPicture,0,CatalogKey,"Expand");
	
					}
				}
				}
			ClearSession(CatalogKey);
			
}

function ExpandMost(Level)
{
		if (typeof(CatalogKey) == "undefined")
			CatalogKey = document.getElementById("catkey").value;
			
		ClosePicture = 'FolderClose_E.gif';
		OpenPicture = 'FolderOpen_E.gif';
		
		if (typeof(Level) == "undefined")
			return;
		
		if(Level == "" || Level == "undefined" || Level == null)
			return;
			
		if (Level == 'MAX')
			Level = 10;
			
		var eaf = document.getElementsByTagName('DIV');
		var len = eaf.length;
		var inArr = new Array();
		
		for (var b=0;b<eaf.length;b++)
				{
					if (eaf[b].id.length > 5)
					{
					//alert(eaf[b].id);
					if (eaf[b].id.substring(0,6) == "inner_")
						{
						var pic = eaf[b].id.substring(6,eaf[b].id.length);
						//var ea = eaf[b].document.getElementById("href");
						//alert(ea);
						//Switch("inner_"+pic,pic,ClosePicture,OpenPicture,iLev,CatalogKey);
						var doc = eaf[b].style;
						if (doc.display == '')
						{
							doc.display = 'none';
							var NewPictureName = document.all(pic).src;
							NewPictureName = NewPictureName.replace("Open","Close");
							document.all(pic).src = NewPictureName;
							eaf[b].innerHTML = "";	
						}
						}
					}
				}
		//return;
		
		eaf = document.getElementsByTagName('DIV');
		len = eaf.length;
				
		for (var iLev=1;iLev <Level;iLev++)
			{
			for (var b=0;b<eaf.length;b++)
				{
					if (eaf[b].id.length > 5)
					{
					//alert(eaf[b].id);
					if (eaf[b].id.substring(0,6) == "inner_")
						{
						var pic = eaf[b].id.substring(6,eaf[b].id.length);
						//var ea = eaf[b].document.getElementById("href");
						//alert(ea);
						//Switch("inner_"+pic,pic,ClosePicture,OpenPicture,iLev,CatalogKey);
						inArr[inArr.length]=pic;
						}
					}
				}
					
				//timedLoop();
				for (var c=0;c<inArr.length;c++)
				{
				if (document.getElementById("inner_"+inArr[c]).style.display == 'none')
					{
						//alert(inArr[c]);
						//setTimeout("Switch('inner_"+inArr[c]+"','"+inArr[c]+"','"+ClosePicture+"','"+OpenPicture+"','"+iLev+"','"+CatalogKey+"');",1000);
						Switch("inner_"+inArr[c],inArr[c],ClosePicture,OpenPicture,iLev,CatalogKey,"Expand");
					}
				}
					
					
		}
		this.highlightIndex(this.currentHlighlighPkey); 

}

function timedLoop()
{
	if (document.getElementById("inner_"+arr[c]).style.display == 'none')
			{
				//alert(inArr[c]);
				//setTimeout("Switch('inner_"+inArr[c]+"','"+inArr[c]+"','"+ClosePicture+"','"+OpenPicture+"','"+iLev+"','"+CatalogKey+"');",1000);
				Switch("inner_"+arr[c],arr[c],closep,openp,leveltime,catkeytime);
				c++;
				if (c < arr.length)
					setTimeout('timedLoop();',10);
				else
					return;
			}

}

function SwitchEnter(divID, PictureName, ClosePicture, OpenPicture,CatalogKey, ParentKey){

			var xmlHttpEnt = new ActiveXObject("Microsoft.XMLHTTP");
			xmlHttpEnt.open("GET", "../DynamicGetNodes.aspx?Pkey="+PictureName+"&Pparentkey="+ParentKey+"&PkeyCatalog="+CatalogKey,false);
			xmlHttpEnt.setRequestHeader("Pragma","no-cache");
			xmlHttpEnt.setRequestHeader("Cache-control","no-cache");
			xmlHttpEnt.send();
			//alert(xmlHttpEnt.responseText);
			if (xmlHttpEnt.responseText != "empty")
			{
				var strArr = xmlHttpEnt.responseText.split('|');
				var level;
				var temptext = "";
			
				for (var i = 1; i < strArr.length ;i++)
				{
					var pictext = "";
					var partext = "";
					
					if (strArr[i].indexOf("-") == -1)
					{
						pictext = strArr[i].substring(3,strArr[i].length);
					}
					else
					{
						pictext = strArr[i].substring(3,strArr[i].indexOf("-"));
						partext = strArr[i].substring(strArr[i].indexOf("-")+1);
					}
					
					
					temptext = "inner_" + pictext;
					level = strArr[i].substring(strArr[i].indexOf("(")+1,strArr[i].indexOf(")"));
					//add - if divid is visible already don't switch
					//setInterval("Switch('"+temptext+"','"+pictext+"','"+ClosePicture+"','"+OpenPicture+"','"+level+"','"+CatalogKey+"')",100);
					if (level != 0)
					{
					var hr = document.getElementById(temptext);
					if (hr != null)
						{
						var sty = document.getElementById(temptext).style; 
						if (sty.display != '')
						Switch(temptext,pictext,ClosePicture,OpenPicture,level,CatalogKey,'',partext);
						}
					}


				}
				//alert(document.body.innerHTML.length);
				top.frames('Index').unlight(top.frames('Index').currentHlighlighPkey); 
				top.frames('Index').highlightIndex(PictureName+'a'); 
				top.frames('Index').currentHlighlighPkey=PictureName+'a';
			}
			xmlHttpEnt = null;
}

function OpenExpand(divID, PictureName){
	if ( document.all(divID)+"" != "null" )   {
		if (document.all(divID).length>1)
		{
			for(i111=0;i111<document.all(divID).length;i111++)
			{
				var sty = document.all(divID)(i111).style; 
				sty.display = '';
				document[PictureName](i111).src = document[PictureName](i111).src.replace("Close","Open");
			}
			return true;
		}
		else
		{
			var sty = document.all(divID).style; 
			sty.display = '';
			document[PictureName].src = document[PictureName].src.replace("Close","Open");
			return true;
		}
		
	}
}

function OpenExpand(divID, PictureName){
	if ( document.all(divID)+"" != "null" )   {
		if (document.all(divID).length>1)
		{
			for(i111=0;i111<document.all(divID).length;i111++)
			{
				var sty = document.all(divID)(i111).style; 
				sty.display = '';
				document[PictureName](i111).src = document[PictureName](i111).src.replace("Close","Open");
			}
			return true;
		}
		else
		{
			var sty = document.all(divID).style; 
			sty.display = '';
			document[PictureName].src = document[PictureName].src.replace("Close","Open");
			return true;
		}
		
	}
}
function CloseExpand(divID, PictureName){
//alert('Coll');
	if ( document.all(divID)+"" != "null" )   {
		if (document.all(divID).length>1)
		{
			for(i111=0;i111<document.all(divID).length;i111++)
			{
	
				var sty = document.all(divID)(i111).style; 
				sty.display = 'none';
				document[PictureName](i111).src = document[PictureName](i111).src.replace("Open","Close");

			}
			return true;
		}
		else
		{
			var sty = document.all(divID).style; 
			sty.display = 'none';
			document[PictureName].src = document[PictureName].src.replace("Open","Close");
			return true;
		}
	
	}
	
}
function enter(){
	event.srcElement.style.fontWeight='bold';
	event.srcElement.style.color='red';
	event.srcElement.style.backgroundcolor='green';
}
function leave(){
	event.srcElement.style.fontWeight='';
	event.srcElement.style.color='black';
}

function insertTable(){
	document.write('<TABLE CLASS="mainTable" width="100%" style="overflow:scroll;">')
}

function insertTR() {
	document.write('<TR CLASS="mainTR">')
}

function insertTD(){
	document.write('<TD CLASS="mainTD" align="left"  valign=top noWrap>')
}

function changeImages() {
 if (document.images)
 	{
 		if (preloadFlag == true)
 			{
   				// Must Be Open And Close Images.
   				// The Files will be xxxxOpen.jpg and xxxxClose.jpg      xxxxx = Object Type
   				
   				if ('noneID'==changeImages.arguments[0])	{
   					return 0;
   				}
   				else	{
				if (document.all(changeImages.arguments[0]).length>1)
				{
					for(i=0;i<document.all(changeImages.arguments[0]).length;i++)
					{

					NewPictureName = document.all(changeImages.arguments[0])(i).src;
					if (NewPictureName.indexOf("Close") == -1)
						{
							NewPictureName = NewPictureName.replace("Open","Close");
						}
					else
						{
							NewPictureName = NewPictureName.replace("Close","Open");
						}
					document.all(changeImages.arguments[0])(i).src = NewPictureName;
					
					}
				}
				else
				{
					NewPictureName = document[changeImages.arguments[0]].src;
					if (NewPictureName.indexOf("Close") == -1)
						{
							NewPictureName = NewPictureName.replace("Open","Close");
						}
					else
						{
							NewPictureName = NewPictureName.replace("Close","Open");
						}
					document[changeImages.arguments[0]].src = NewPictureName;
				
				}
				
				}
   			}
 	}
}

var wWait; //for please wait window...

function waitForWinLoaded(mode)	{
	if ((typeof(wWait.bIsNotFinish)!='undefined') && (!wWait.bIsNotFinish))	{
	    if (mode=="Expand")	{
			expandALL(1);
		}
		else	{
			collapseAll(1);
		}
	}
	else	{
		funcCall  = 'waitForWinLoaded(\'' + mode + '\')';
		setTimeout(funcCall,20)
	}	
}

function expandALL(i)	{
	if (i==0)	{
	document.body.style.cursor='wait';
	wWait=window.open('wait.html', 'wait','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	waitForWinLoaded('Expand');
	}
	else	{
		ExpandMost('MAX');
		document.body.style.cursor='auto';
		wWait.close();
	}
}

function collapseAll(i)	{
	if (i==0)	{
	document.body.style.cursor='wait';
	wWait=window.open('wait.html', 'wait','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	waitForWinLoaded('Collapse');
	}
	else	{
		CollapseMost();
		document.body.style.cursor='auto';
		wWait.close();
	}
}