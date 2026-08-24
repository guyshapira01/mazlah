
function ValidateForm(subcheck,valcheck,formname)
{
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "ValidateForm.aspx?subCheck=" + subcheck +"&valCheck=" + valcheck, false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseText;
	
	if (resp == "True")
	{
		//alert("ok");
		document.getElementById(formname).submit();
		 //and submit
	}
	else
	{
		if (resp == "False")
		{
			alert("DOCNUMBER Already Exists");
		}
		else
		{
			alert("Error on Request");
		}
	}

	xmlHttpObj = null;
}

function ValidateObject(subcheck,valcheck,propcheck)
{
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=" + subcheck +"&valCheck=" + valcheck, false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseText;
	xmlHttpObj = null;
	
	return resp;
}

function UniqueCheck(table, range, propcheck, valcheck,iOption)
{
	//iOption
	//0 - Regular Mode
	//1 - Case Insensitive check
	//2 - Alphanumeric check - N/A.
	
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	switch(iOption)
	{
		case 0:sUrl = "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=UniqueCheck&valCheck=" + valcheck + "&table=" + table + "&range=" + range;break;
		case 1:sUrl = "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=UniqueCheckInsensitive&valCheck=" + valcheck + "&table=" + table + "&range=" + range;break;
		case 2:sUrl = "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=UniqueCheck&valCheck=" + valcheck + "&table=" + table + "&range=" + range;break;
		default:sUrl = "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=UniqueCheck&valCheck=" + valcheck + "&table=" + table + "&range=" + range;break;
	}	
	xmlHttpObj.open("GET", sUrl, false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseText;
	xmlHttpObj = null;
	
	return resp=='true';
}

function GenerateUnique(table, range, propcheck, txtPrefix, typeNumerator)
{
	//typeNumerator: numeric / Alpha
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=GenerateUnique&valCheck=" + txtPrefix + "&table=" + table + "&range=" + range + "&typeNumerator=" + typeNumerator, false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseText;
	xmlHttpObj = null;
	
	return resp;
}

function GetUniqueIndex(table, range, propcheck)
{
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=GetUniqueIndex&table=" + table + "&range=" + range, false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseText;
	xmlHttpObj = null;
	
	return resp;
}

//propcheck - pkey of LOV.
function GetLOV(propcheck)
{
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=GetLOV", false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseXML;
	xmlHttpObj = null;
	return resp;
}

//propcheck - name of LOV.
function GetLOVByName(propcheck)
{
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "ValidateForm.aspx?propCheck=" + propcheck + "&subCheck=GetLOVByName", false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseXML;
	xmlHttpObj = null;
	return resp;
}

function GetMultiLOV(selectprops,whereclause)
{
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "ValidateForm.aspx?propCheck=" + whereclause + "&valcheck=" + selectprops + "&subCheck=GenerateMultiLOV", false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseXML;
	xmlHttpObj = null;
	return resp;
}

function GetMultiLOVFromReport(selectprops,whereclause)
{
	var xmlHttpObj = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpObj.open("GET", "Templates/ValidateForm.aspx?propCheck=" + whereclause + "&valcheck=" + selectprops + "&subCheck=GenerateMultiLOV", false);
	xmlHttpObj.setRequestHeader("Pragma","no-cache");
	xmlHttpObj.setRequestHeader("Cache-control","no-cache");
	xmlHttpObj.send();
	var resp = xmlHttpObj.responseXML;
	xmlHttpObj = null;
	return resp;
}

//example usage:GetArrayOfLov(GetLOV('pkey of lov object'))
function GetArrayOfLov(lovXML)
{
	var properties = new Array();

	for(i=0;i<lovXML.getElementsByTagName('PROPERTY').length;i++)
		properties[properties.length] = lovXML.getElementsByTagName('PROPERTY')(i).text;
	
	return properties;		
}

function FillSelectBasedOnLOV(selectObj, LOVArray, defaultValue)
{
	for(i=0;i<LOVArray.length;i++)
	{
		var oOption = document.createElement("OPTION");
		oOption.text=LOVArray[i];
		oOption.value=LOVArray[i];
		selectObj.add(oOption);
		if (LOVArray[i]==defaultValue)
			selectObj[selectObj.length-1].selected = true;
	}
}

function FillSelectBasedOnMultiLOV(selectObj, LOVArray, defaultValue)
{
	for(i=0;i<LOVArray.length;i++)
	{
		var oOption = document.createElement("OPTION");
		var sTextVal = LOVArray[i].substring(0,LOVArray[i].indexOf('|'));
		var sPropVals = LOVArray[i].substring(LOVArray[i].indexOf('|')+1);
		oOption.text=sTextVal;
		oOption.value=sPropVals;
		selectObj.add(oOption);
		if (sPropVals==defaultValue)
			selectObj[selectObj.length-1].selected = true;
	}
}

function DelObj(bDeletePermanently)	{
	//Parameters:
	// bDeletePermanently = 'True' Deletes all instances of the object.
	// bDeletePermanently = 'NotLast' Delete only when is not last object. 
	// bDeletePermanently = 'Childs' Deletes all childs of the object (also the default action). 

	if (typeof(bDeletePermanently)=="undefined") bDeletePermanently = "";

	var j=0;
	var deletedObjects = "";
	var temp = "";
	var curkey = "";
	var self = "";
	var parentFather ="";
	var catalog;
	
	if (top.document.location.href.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			catalog = document.getElementById("catkey").value;
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = top.document.location.href.substring(top.document.location.href.indexOf("PkeyCatalog=",0)+12,top.document.location.href.length);	
	}
	
		temp = document.location.href;
		curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
		parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	if (DelObj.arguments.length>1)	{
		//updateObject = UpdateObj.arguments[0];
		deletedObjects = document.location.href;
		deletedObjects = deletedObjects.substring(deletedObjects.indexOf("Pkey=",0)+5,deletedObjects.indexOf("&",deletedObjects.indexOf("Pkey=")))
		self = "true";
		
	}
	else	{
		self = "false";
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				deletedObjects = deletedObjects + document.all.tags('INPUT').item(j).name + "|";
			}
		}
	}
	if (deletedObjects!="")	{
		if (confirm('Are you sure you want to delete the selected objects?'))	{
			w=window.open('deleteObjects.aspx?PkeyCatalog=' + catalog + '&Pkey='+ curkey +'&Parent='+parentFather+'&Self='+ self +'&Objects=' + deletedObjects + "|" + "&DeletePermanently=" + bDeletePermanently,'DeleteObj','width=50,height=50,toolbar=no,scrollbars=yes,top=5000,left=5000,screenX=5000,screenY=5000');
		//	 w.focus();
		}
	}
	else	{
		alert('Please choose an object');
	}
}


//   *******   The same function as the prev with diffrent location of updateObject.aspx  *****


function DelObjFromReport()	{
	var j=0;
	var deletedObjects = "";
	if (DelObjFromReport.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		deletedObjects = document.location.href;
		deletedObjects = deletedObjects.substring(deletedObjects.indexOf("Pkey=",0)+5,deletedObjects.indexOf("&",deletedObjects.indexOf("Pkey=")))
		deletedObjects = deletedObjects.substr(11);
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				deletedObjects = deletedObjects + document.all.tags('INPUT').item(j).name.substr(11) + "|";
			}
		}
	}
	if (deletedObjects!="")	{
		if (confirm('Are you sure you want to delete the selected objects?'))	{
			w=window.open('Templates/deleteObjects.aspx?Objects=' + deletedObjects + "|");
		//	w.focus();
		}
	}
	else	{
		alert('Please choose an object');
	}
}

//   ******************************************************

function UpdateObj(sTableName,sTemplate)	{
	var j=0;
	var self = "false";
	var updateObject = "";
	
	var temp = document.location.href;
	var	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}

	
	if (UpdateObj.arguments.length>2)	{
		
		//updateObject = UpdateObj.arguments[0];
		updateObject = document.location.href;
		updateObject = updateObject.substring(updateObject.indexOf("Pkey=",0)+5,updateObject.indexOf("&",updateObject.indexOf("Pkey=")))
		self = "true";
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (updateObject=="")	{
					updateObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (updateObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
		
			if (sTableName == "T_CAT_CATALOG")
			{
				w=window.open('Templates/updateObject.aspx?PkeyCatalog=' + catalog + '&Pkey='+ catalog+ '&Self='+self+'&Object=' + catalog + '&Table=' + sTableName + '&Template=' + sTemplate ,'updateObject','width=950,height=700,toolbar=no,scrollbars=yes');
				w.focus();
			}
			else
			{
				w=window.open('updateObject.aspx?PkeyCatalog=' + catalog + '&Pkey='+curkey+'&ParentFather='+parentFather+'&Self='+self+'&Object=' + updateObject + '&Table=' + sTableName + '&Template=' + sTemplate ,'updateObject','width=950,height=700,toolbar=no,scrollbars=yes');
				w.focus();
			}
		//}
	}
	else	{
		alert('Please choose an object');
	}
}

//=================================================================
//Created By Guy 15-06-2005
//   ******************************************************

function InfoObj(sPkey,sTemplate)	{
	var j=0;
	var self = "false";
	var temp = document.location.href;
	var updateObject = "";
	var	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}

	
	
		
			w=window.open('updateObject.aspx?PkeyCatalog=' + catalog + '&Pkey=' + curkey + '&ParentFather='+parentFather+'&Self='+self+'&Object=' + sPkey + '&Table=' + 'T_CAT_WINOBJECT' + '&Template=' + sTemplate ,'updateObject','width=950,height=700,toolbar=no,scrollbars=yes');
			w.focus();
			
		
	
}

function InfoObjParts(sPkey,sTemplate)	{
	var j=0;
	var self = "false";
	var temp = document.location.href;
	var updateObject = "";
	var	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
			w=window.open('updateObject.aspx?PkeyCatalog=' + catalog + '&Pkey=' + curkey + '&ParentFather='+parentFather+'&Self='+self+'&Object=' + sPkey + '&Table=' + 'T_CAT_PART' + '&Template=' + sTemplate ,'updateObject','width=950,height=700,toolbar=no,scrollbars=yes');
			w.focus();
}

function EditText(mode,template)	{
	var j=0;
	var self = "True";
	var temp = document.location.href;
	var updateObject = "";
	var	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	
	if (typeof(template)=="undefined") template = "";
	
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
		
			w=window.open('EditText.aspx?Template='+template+'&PkeyCatalog=' + catalog + '&Pkey=' + curkey + '&ParentFather='+parentFather+'&Mode='+mode+'&Self='+self+'&Object=' + curkey ,'EditText','width=500,height=400,toolbar=no,scrollbars=yes');
			w.focus();
}


//   *******   The same function as the prev with diffrent location of updateObject.aspx  *****

function UpdateObjFromReport(sTableName,sTemplate)	{
	var j=0;
	var updateObject = "";
	if (UpdateObjFromReport.arguments.length>2)	{
		//updateObject = UpdateObjFromReport.arguments[0];
		updateObject = document.location.href;
		updateObject = updateObject.substring(updateObject.indexOf("Pkey=",0)+5,updateObject.indexOf("&",updateObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (updateObject=="")	{
					updateObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (updateObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('Templates/updateObject.aspx?Self=false&Object=' + updateObject.substr(11) + '&Table=' + sTableName + '&Template=' + sTemplate ,'updateObject','width=950,height=700,toolbar=no,scrollbars=yes');
			w.focus();
		//}
	}
	else	{
		alert('Please choose an object');
	}
}




//   ******************************************************


function ReOrderObjFromReport(idd)	{
	var j=0;
	var reOrderObject = "";
	if (ReOrderObjFromReport.arguments.length>1)	{
		//updateObject = UpdateObj.arguments[0];
		reOrderObject = document.location.href;
		reOrderObject = reOrderObject.substring(reOrderObject.indexOf("Pkey=",0)+5,reOrderObject.indexOf("&",reOrderObject.indexOf("Pkey=")))
	}
	else	{
		//alert(document.all.tags('INPUT').length);
			//var OpenerUser = document.location.href;
			//OpenerUser = OpenerUser.substring(OpenerUser.indexOf("PPROP7=",0)+7,OpenerUser.length);
		
		for (j=0; j<document.all.tags('INPUT').length; j++)	
		{
			if (document.all.tags('INPUT').item(j).id == idd && document.all.tags('INPUT').item(j).type == "checkbox")
			{
				if (document.all.tags('INPUT').item(j).PPROP3 != '')
				{
					if (document.all.tags('INPUT').item(j).name.indexOf("cboxUpdate_") != -1)	{

						if (reOrderObject=="")	{
							reOrderObject = document.all.tags('INPUT').item(j).name.substring(11,document.all.tags('INPUT').item(j).name.length);
							}
					else
						{
							reOrderObject = reOrderObject + '|' + document.all.tags('INPUT').item(j).name.substring(11,document.all.tags('INPUT').item(j).name.length);
						}
					}
				}
			}
		}
	}
	if (reOrderObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			var uri = document.location.href;
			w=window.open('Templates/ChildsManage.aspx?Mode=Report&ID='+idd+'&Object=' + reOrderObject+'&RefUrl='+uri,'reOrderObject','width=600,height=700,toolbar=no,scrollbars=yes');
			w.focus();
		//}
	}
	else	{
		alert('Please choose an object');
	}
}

function ReOrderObj()	{
	var j=0;
	var reOrderObject = "";
	if (ReOrderObj.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		reOrderObject = document.location.href;
		reOrderObject = reOrderObject.substring(reOrderObject.indexOf("Pkey=",0)+5,reOrderObject.indexOf("&",reOrderObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (reOrderObject=="")	{
					reOrderObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (reOrderObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('ChildsManage.aspx?Object=' + reOrderObject,'reOrderObject','width=600,height=700,toolbar=no,scrollbars=yes');
			w.focus();
		//}
	}
	else	{
		alert('Please choose an object');
	}
}

function AddObj(sTemplate)	{
	var j=0;
	var fatherObject = "";
	var temp = document.location.href;
	var parentFather="";
	fatherObject = document.location.href;
	fatherObject2 = top.document.location.href;
	fatherObject = fatherObject.substring(fatherObject.indexOf("Pkey=",0)+5,fatherObject.indexOf("&",fatherObject.indexOf("Pkey=")));
	var catalog;
	
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			catalog = document.getElementById("catkey").value;
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	if (fatherObject2.indexOf("ParentKey") > -1)
	{
		parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	}
	else
	{
		parentFather = catalog;
	}
	
	if (fatherObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('addObject.aspx?PkeyCatalog='+ catalog +'&FatherPkey=' + fatherObject + '&ParentFather='+ parentFather +'&Template=' + sTemplate,'AddObj','width=950,height=700,toolbar=no,scrollbars=yes,resizable=yes');
			w.focus();
		//}
	}
}

function AddObjFromIndex(sTemplate)	{
	var j=0;
	var fatherObject = "";
	var parentFather="";
	fatherObject = document.location.href;
	fatherObject2 = top.document.location.href;
	fatherObject = fatherObject.substring(fatherObject.indexOf("Pkey=",0)+5,fatherObject.indexOf("&",fatherObject.indexOf("Pkey=")));
	var catalog;
	
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			catalog = document.getElementById("catkey").value;
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	parentFather = catalog;
	
	if (fatherObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('addObject.aspx?PkeyCatalog='+ catalog +'&FatherPkey=' + fatherObject + '&ParentFather='+ parentFather +'&Template=' + sTemplate,'AddObj','width=950,height=700,toolbar=no,scrollbars=yes');
			w.focus();
		//}
	}
}


//   *******   The same function as the prev with diffrent location of updateObject.aspx and parameter as the FatherPkey *****

function AddObjFromReport(sTemplate,fatherObject,AdditionalParam)	{
	var j=0;
//	var fatherObject = "";
//	fatherObject = document.location.href;
//	fatherObject = fatherObject.substring(fatherObject.indexOf("Pkey=",0)+5,fatherObject.indexOf("&",fatherObject.indexOf("Pkey=")))
	if (fatherObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('Templates/addObject.aspx?FatherPkey=' + fatherObject + '&Template=' + sTemplate + '?&addparam=' + AdditionalParam,'AddObj','width=950,height=700,toolbar=no,scrollbars=yes,resizable=yes');
			w.focus();
		//}
	}
}


//   ******************************************************


function putMailLink(FailureID, FailureName)	{
	re = /&/gi;
	MailLink.href='mailto:?body=Link to failure:%0d' + document.location.href.replace(re,"%26") + '&subject=' + FailureID + ' - ' + FailureName;
}

function putMailLink_Task(ActivityID, ActivityName)	{
	re = /&/gi;
	MailLink.href='mailto:?body=Link to Activity:%0d' + document.location.href.replace(re,"%26") + '&subject=' + ActivityID + ' - ' + ActivityName;
}

function MailLink_Tree(Url,MailSubject,MailBody)	{
	re = /&/gi;
	var x = 'mailto:?body=' + MailBody + '%0dLink:%0d' + Url.replace(re,"%26") + '&subject=' + MailSubject;
	window.open(x,'Maillink','width=220,height=200,toolbar=no,scrollbars=no');
	//document.location.href='mailto:?body=' + MailBody + '%0dLink:%0d' + Url.replace(re,"%26") + '&subject=' + MailSubject;
}

function MailLink_Task(ActivityPkey, ActivityID, ActivityName, ActivityTemplate, ObjectType)	{
	re = /&/gi;
	url = document.location.href;
	url = url.substring(0,url.lastIndexOf("/"))
	url = url + '/ShowTemplate.aspx?Template=' + ActivityTemplate + '&Pkey=' + ActivityPkey + '&Type=' + ObjectType;
	document.location.href='mailto:?body=Link to Activity:%0d' + url.replace(re,"%26") + '&subject=' + ActivityID + ' - ' + ActivityName;
}

function PermissionsWin()	{
	var j=0;
	var permissionObject = "";
	if (PermissionsWin.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		permissionObject = document.location.href;
		permissionObject = permissionObject.substring(permissionObject.indexOf("Pkey=",0)+5,permissionObject.indexOf("&",permissionObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (permissionObject=="")	{
					permissionObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (permissionObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('../Permissions.aspx?Pkey=' + permissionObject ,'PermissionsWin','width=220,height=200,toolbar=no,scrollbars=yes');
			w.focus();
		//}
	}
}

function CheckOut()	{

	var fatherObject = "";
	var parentFather="";
	fatherObject = document.location.href;
	fatherObject2 = top.document.location.href;
	fatherObject = fatherObject.substring(fatherObject.indexOf("Pkey=",0)+5,fatherObject.indexOf("&",fatherObject.indexOf("Pkey=")));
	var catalog;
	
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			catalog = document.getElementById("catkey").value;
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	if (fatherObject2.indexOf("ParentKey") > -1)
	{
		parentFather = fatherObject2.substring(fatherObject2.indexOf("ParentKey=",0)+10,fatherObject2.indexOf("&",fatherObject2.indexOf("ParentKey=")));
	}
	else
	{
		parentFather = catalog;
	}
	
	var j=0;
	var CheckOutObject = "";
	
	if (CheckOut.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		CheckOutObject = document.location.href;
		CheckOutObject = CheckOutObject.substring(CheckOutObject.indexOf("Pkey=",0)+5,CheckOutObject.indexOf("&",CheckOutObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (CheckOutObject=="")	{
					CheckOutObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (CheckOutObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('../CheckOut.aspx?Father='+fatherObject+'&PkeyCatalog='+catalog+'&ParentFather='+parentFather+'&Pkey=' + CheckOutObject ,'CheckOutWin','width=500,height=200,toolbar=no,scrollbars=yes,top=50,left=50,screenX=50,screenY=50');
			w.focus();
		//}
	}
}

function CheckOutAuto()	{

	var fatherObject = "";
	var parentFather="";
	fatherObject = document.location.href;
	fatherObject2 = top.document.location.href;
	fatherObject = fatherObject.substring(fatherObject.indexOf("Pkey=",0)+5,fatherObject.indexOf("&",fatherObject.indexOf("Pkey=")));
	var catalog;
	
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			catalog = document.getElementById("catkey").value;
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	if (fatherObject2.indexOf("ParentKey") > -1)
	{
		parentFather = fatherObject2.substring(fatherObject2.indexOf("ParentKey=",0)+10,fatherObject2.indexOf("&",fatherObject2.indexOf("ParentKey=")));
	}
	else
	{
		parentFather = catalog;
	}
	
	var j=0;
	var CheckOutObject = "";
	
	if (CheckOutAuto.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		CheckOutObject = document.location.href;
		CheckOutObject = CheckOutObject.substring(CheckOutObject.indexOf("Pkey=",0)+5,CheckOutObject.indexOf("&",CheckOutObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (CheckOutObject=="")	{
					CheckOutObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (CheckOutObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('../CheckOut.aspx?Father='+fatherObject+'&PkeyCatalog='+catalog+'&ParentFather='+parentFather+'&AutoCheckOut=true&Pkey=' + CheckOutObject ,'CheckOutWin','width=500,height=200,toolbar=no,scrollbars=yes,top=50,left=50,screenX=50,screenY=50');
			w.focus();
		//}
	}
}

function OpenAjax(sUrl)
{
	var xmlHttpObj = new ActiveXObject('Microsoft.XMLHTTP');
		xmlHttpObj.open('GET',sUrl, false);
		xmlHttpObj.setRequestHeader('Pragma','no-cache');
		xmlHttpObj.setRequestHeader('Cache-control','no-cache');
		xmlHttpObj.send();
		var resp = xmlHttpObj.responseText;
		return resp;
}

function CheckInRevise(sTableName,sTemplate,reviseobj)	
{
	var j=0;
	var CheckInObject = "";
	var self = "false";
	
	var temp = document.location.href;
	var	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	fatherObject2 = top.document.location.href;
	
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	
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
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	for (j=0; j<document.all.tags('INPUT').length; j++)		
	{
		if (document.all.tags('INPUT').item(j).checked)	
		{
			if (CheckInObject=="")	
			{
				CheckInObject = document.all.tags('INPUT').item(j).name;
			}
			else	
			{
				alert("Please choose one object");
				return(0);
			}
		}
	}
	//revise
	if (CheckInObject!="")	
	{
	
		var w;
		
		var retVal = makeMsgBox("Confirmation","Would you like to revise the file?",48,4,256,4096);
		
		//confirm('Would you like to revise the file?')
		if (retVal == 6)
		{
			w = window.open('updateObject.aspx?PkeyCatalog=' + catalog + '&Pkey='+curkey+'&ParentFather='+parentFather+'&Self='+self+'&Object=' + CheckInObject + '&revise='+ reviseobj +'&Table=' + sTableName + '&Template=' + sTemplate + '&Checkin=true','AutoRevise','width=50,height=50,toolbar=no,scrollbars=yes,top=5000,left=5000,screenX=5000,screenY=5000');
		}
		else
		{
			CheckIn();
		}
		//w=window.open('../CheckIn.aspx?Pkey=' + CheckInObject + '&PkeyCatalog=' + catalog,'CheckInWin','width=50,height=50,toolbar=no,scrollbars=yes,top=5000,left=5000,screenX=5000,screenY=5000');
		//w.focus();
	}
}


function CheckIn()	{
	var j=0;
	var CheckInObject = "";
	
	var catalog;
	var fatherObject = "";
	var parentFather="";
	fatherObject = document.location.href;
	var fatherObject2 = top.document.location.href;
	fatherObject = fatherObject.substring(fatherObject.indexOf("Pkey=",0)+5,fatherObject.indexOf("&",fatherObject.indexOf("Pkey=")));
	
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	
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
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	
	if (CheckIn.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		CheckInObject = document.location.href;
		CheckInObject = CheckInObject.substring(CheckInObject.indexOf("Pkey=",0)+5,CheckInObject.indexOf("&",CheckInObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (CheckInObject=="")	{
					CheckInObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (CheckInObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('../CheckIn.aspx?Pkey=' + CheckInObject + '&PkeyCatalog=' + catalog,'CheckInWin','width=50,height=50,toolbar=no,scrollbars=yes,top=5000,left=5000,screenX=5000,screenY=5000');
			w.focus();
		//}
	}
}

function Revise(sTableName,sTemplate,reviseobj)	{
	var j=0;
	var self = "false";
	var updateObject = "";
	
	var temp = document.location.href;
	var	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}

	
	if (Revise.arguments.length>3)	{
		
		//updateObject = UpdateObj.arguments[0];
		updateObject = document.location.href;
		updateObject = updateObject.substring(updateObject.indexOf("Pkey=",0)+5,updateObject.indexOf("&",updateObject.indexOf("Pkey=")))
		self = "true";
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (updateObject=="")	{
					updateObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (updateObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
		
			if (sTableName == "T_CAT_CATALOG")
			{
				w=window.open('Templates/updateObject.aspx?PkeyCatalog=' + catalog + '&Pkey='+ catalog+ '&Self='+self+'&Object=' + catalog + '&Table=' + sTableName + '&Template=' + sTemplate ,'updateObject','width=950,height=700,toolbar=no,scrollbars=yes');
				w.focus();
			}
			else
			{
				w=window.open('updateObject.aspx?PkeyCatalog=' + catalog + '&Pkey='+curkey+'&ParentFather='+parentFather+'&Self='+self+'&Object=' + updateObject + '&revise='+ reviseobj +'&Table=' + sTableName + '&Template=' + sTemplate ,'updateObject','width=950,height=700,toolbar=no,scrollbars=yes');
				w.focus();
			}
		//}
	}
	else	{
		alert('Please choose an object');
	}
}

function Revise_OLD(template)	{
	var j=0;
	var ReviseObject = "";
	var fatherObject = "";
	fatherObject = document.location.href;
	fatherObject = fatherObject.substring(fatherObject.indexOf("Pkey=",0)+5,fatherObject.indexOf("&",fatherObject.indexOf("Pkey=")))
	
	if (Revise.arguments.length>1)	{
		//updateObject = UpdateObj.arguments[0];
		ReviseObject = document.location.href;
		ReviseObject = ReviseObject.substring(ReviseObject.indexOf("Pkey=",0)+5,ReviseObject.indexOf("&",ReviseObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (ReviseObject=="")	{
					ReviseObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (ReviseObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('../Revise.aspx?Pkey=' + ReviseObject + '&template=' + template,'ReviseWin','width=950,height=700,toolbar=no,scrollbars=yes,top=50,left=50,screenX=50,screenY=50');
			w.focus();
		//}
	}
}

function HistoryEER(table)	{
	var j=0;
	var HistoryObject = "";
	var docidobj = "";
	
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	
	if (HistoryEER.arguments.length>1)	{
		//updateObject = UpdateObj.arguments[0];
		HistoryObject = document.location.href;
		HistoryObject = HistoryObject.substring(HistoryObject.indexOf("Pkey=",0)+5,HistoryObject.indexOf("&",HistoryObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (HistoryObject=="")	{
					HistoryObject = document.all.tags('INPUT').item(j).name + '_PDOCID';
					docidobj = document.all(HistoryObject).value;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (HistoryObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			//w=window.open('../DocumentRevisions.aspx?rObject=' + HistoryObject + '&PkeyCatalog='+catalog+'&Mode='+mode ,'HistoryWin','width=750,height=400,toolbar=no,scrollbars=yes,top=50,left=50,screenX=50,screenY=50');
			//w=window.open('../GenerateReport.aspx?Catalog='+catalog+'&amp;Table='+table+'&amp;XSL=DocumentRevisions.xsl&amp;PDOCID='+ docidobj,'HistoryR');
			w=window.open('../GenerateReport.aspx?TABLE=T_CAT_WINOBJECT&LATESTREV=true&Catalog='+catalog+'&amp;Mode=FullSQL&amp;XSL=DocumentRevisionsEER.xsl&amp;&SQL=select t_cat_winobject.*,t_cat_objecttypes.ptypename,t_cat_status.pstatus statusname from t_cat_winobject,t_cat_status,t_cat_objecttypes where t_cat_objecttypes.pkey = t_cat_winobject.pkeytype and t_cat_status.pkey(%2B) = t_cat_winobject.pstatus and t_cat_status.pkeycatalog(%2B)=\''+catalog+'\' and pdocid =\''+ docidobj + '\' order by prevision','HistoryR','width=800,height=400,toolbar=no,scrollbars=yes,top=20,left=20');
			w.focus();
		//}
	}
}

function History(table)	{
	var j=0;
	var HistoryObject = "";
	var docidobj = "";
	
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	
	if (History.arguments.length>1)	{
		//updateObject = UpdateObj.arguments[0];
		HistoryObject = document.location.href;
		HistoryObject = HistoryObject.substring(HistoryObject.indexOf("Pkey=",0)+5,HistoryObject.indexOf("&",HistoryObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (HistoryObject=="")	{
					HistoryObject = document.all.tags('INPUT').item(j).name + '_PDOCID';
					docidobj = document.all(HistoryObject).value;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (HistoryObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			//w=window.open('../DocumentRevisions.aspx?rObject=' + HistoryObject + '&PkeyCatalog='+catalog+'&Mode='+mode ,'HistoryWin','width=750,height=400,toolbar=no,scrollbars=yes,top=50,left=50,screenX=50,screenY=50');
			//w=window.open('../GenerateReport.aspx?Catalog='+catalog+'&amp;Table='+table+'&amp;XSL=DocumentRevisions.xsl&amp;PDOCID='+ docidobj,'HistoryR');
			w=window.open('../GenerateReport.aspx?TABLE=T_CAT_WINOBJECT&LATESTREV=true&Catalog='+catalog+'&amp;Mode=FullSQL&amp;XSL=DocumentRevisions.xsl&amp;&SQL=select t_cat_winobject.*,t_cat_objecttypes.ptypename,t_cat_status.pstatus statusname from t_cat_winobject,t_cat_status,t_cat_objecttypes where t_cat_objecttypes.pkey = t_cat_winobject.pkeytype and t_cat_status.pkey(%2B) = t_cat_winobject.pstatus and t_cat_status.pkeycatalog(%2B)=\''+catalog+'\' and pdocid =\''+ docidobj + '\' order by prevision','HistoryR','width=800,height=400,toolbar=no,scrollbars=yes,top=20,left=20');
			w.focus();
		//}
	}
}

function HistoryItems(table,keyprop)	{
	var j=0;
	var HistoryObject = "";
	var docidobj = "";
	
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	
	if (HistoryItems.arguments.length>2)	{
		//updateObject = UpdateObj.arguments[0];
		HistoryObject = document.location.href;
		HistoryObject = HistoryObject.substring(HistoryObject.indexOf("Pkey=",0)+5,HistoryObject.indexOf("&",HistoryObject.indexOf("Pkey=")))
		docidobj = HistoryItems.arguments[2];
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (HistoryObject=="")	{
					HistoryObject = document.all.tags('INPUT').item(j).name + '_PDOCID';
					docidobj = document.all(HistoryObject).value;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (HistoryObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			//w=window.open('../DocumentRevisions.aspx?rObject=' + HistoryObject + '&PkeyCatalog='+catalog+'&Mode='+mode ,'HistoryWin','width=750,height=400,toolbar=no,scrollbars=yes,top=50,left=50,screenX=50,screenY=50');
			//w=window.open('../GenerateReport.aspx?Catalog='+catalog+'&amp;Mode=FullSQL&amp;XSL=DocumentRevisions.xsl&amp;&SQL=select * from t_cat_part where '+keyprop+' =\''+ docidobj + '\' order by prevision','HistoryR');
			w=window.open('../GenerateReport.aspx?TABLE=T_CAT_PART&LATESTREV=true&Catalog='+catalog+'&amp;Mode=FullSQL&amp;XSL=DocumentRevisions.xsl&amp;&SQL=select t_cat_part.*,t_cat_objecttypes.ptypename,t_cat_status.pstatus statusname from t_cat_part,t_cat_status,t_cat_objecttypes where t_cat_objecttypes.pkey = t_cat_part.pkeytype and t_cat_status.pkey(%2B) = t_cat_part.pstatus and t_cat_status.pkeycatalog(%2B)=\''+catalog+'\' and '+keyprop+' =\''+ docidobj + '\' order by prevision','HistoryR','width=800,height=400,toolbar=no,scrollbars=yes,top=20,left=20');
			w.focus();
		//}
	}
}


function History_OLD(mode)	{
	var j=0;
	var HistoryObject = "";
	
	fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	
	if (History.arguments.length>1)	{
		//updateObject = UpdateObj.arguments[0];
		HistoryObject = document.location.href;
		HistoryObject = HistoryObject.substring(HistoryObject.indexOf("Pkey=",0)+5,HistoryObject.indexOf("&",HistoryObject.indexOf("Pkey=")))
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (HistoryObject=="")	{
					HistoryObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}
	if (HistoryObject!="")	{
		//if (confirm('Are you sure you want to update those object/s?'))	{
			w=window.open('../DocumentRevisions.aspx?rObject=' + HistoryObject + '&PkeyCatalog='+catalog+'&Mode='+mode ,'HistoryWin','width=750,height=400,toolbar=no,scrollbars=yes,top=50,left=50,screenX=50,screenY=50');
			w.focus();
		//}
	}
}

function CopyObj()	{
	var j=0;
	var copyObjects = "";
	var temp = "";
	var curkey = "";
	var self = "";
	
	temp = document.location.href;
	
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	var fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));

	if (CopyObj.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		copyObjects = document.location.href;
		copyObjects = copyObjects.substring(copyObjects.indexOf("Pkey=",0)+5,copyObjects.indexOf("&",copyObjects.indexOf("Pkey=")))
		self = "true";
		
	}
	else	{
		self = "false";
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (document.all.tags('INPUT').item(j).name != 'allbox')
					copyObjects = copyObjects + document.all.tags('INPUT').item(j).name + "|";
			}
		}
	}
	if (copyObjects!="")	{
		window.open('Copy.aspx?Pkey='+ curkey +'&Parent='+parentFather+'&PkeyCatalog='+catalog+'&Self='+ self +'&Objects=' + copyObjects + '|', 'CutWin','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	}
	else	{
		alert('Please choose an object');
	}
}

function GetKeyFromSession(okey)	{
	
		var xmlHttpObj = new ActiveXObject('Microsoft.XMLHTTP');
		xmlHttpObj.open('GET','Copy.aspx?RetVal='+okey, false);
		xmlHttpObj.setRequestHeader('Pragma','no-cache');
		xmlHttpObj.setRequestHeader('Cache-control','no-cache');
		xmlHttpObj.send();
		var resp = xmlHttpObj.responseText;
		return resp;
}


function CopyObjAJAX()	{
	var j=0;
	var copyObjects = "";
	var temp = "";
	var curkey = "";
	var self = "";
	
	temp = document.location.href;
	
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	var fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));

	if (CopyObjAJAX.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		copyObjects = document.location.href;
		copyObjects = copyObjects.substring(copyObjects.indexOf("Pkey=",0)+5,copyObjects.indexOf("&",copyObjects.indexOf("Pkey=")))
		self = "true";
		
	}
	else	{
		self = "false";
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (document.all.tags('INPUT').item(j).name != 'allbox')
					copyObjects = copyObjects + document.all.tags('INPUT').item(j).name + "|";
			}
		}
	}
	if (copyObjects!="")	{
		
		var xmlHttpObj = new ActiveXObject('Microsoft.XMLHTTP');
		xmlHttpObj.open('GET','Copy.aspx?Pkey='+ curkey +'&Parent='+parentFather+'&PkeyCatalog='+catalog+'&Self='+ self +'&Objects=' + copyObjects + '|', false);
		xmlHttpObj.setRequestHeader('Pragma','no-cache');
		xmlHttpObj.setRequestHeader('Cache-control','no-cache');
		xmlHttpObj.send();
		var resp = xmlHttpObj.responseText;
		
		//window.open('Copy.aspx?Pkey='+ curkey +'&Parent='+parentFather+'&PkeyCatalog='+catalog+'&Self='+ self +'&Objects=' + copyObjects + '|', 'CutWin','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	}
	else	{
		alert('Please choose an object');
	}
}


function CopyObjFromReport()	{
	var j=0;
	var copyObjects = "";
	var temp = "";
	var curkey = "";
	var self = "";
	
	temp = document.location.href;
	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));

	if (CopyObjFromReport.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		copyObjects = document.location.href;
		copyObjects = copyObjects.substring(copyObjects.indexOf("Pkey=",0)+5,copyObjects.indexOf("&",copyObjects.indexOf("Pkey=")))
		self = "true";
		
	}
	else	{
		self = "false";
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				copyObjects = copyObjects + document.all.tags('INPUT').item(j).name.replace("cboxUpdate_","") + "|";
			}
		}
	}
		
	if (copyObjects!="")	{
    		window.open('Templates/Copy.aspx?FromReport=true&Pkey='+ curkey +'&Self='+ self +'&Objects=' + copyObjects + '|', 'CutWin','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	}
	else	{
		alert('Please choose an object');
	}
}

function CopyObjFromReportAJAX()	{
	var j=0;
	var copyObjects = "";
	var temp = "";
	var curkey = "";
	var self = "";
	
	temp = document.location.href;
	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));

	if (CopyObjFromReportAJAX.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		copyObjects = document.location.href;
		copyObjects = copyObjects.substring(copyObjects.indexOf("Pkey=",0)+5,copyObjects.indexOf("&",copyObjects.indexOf("Pkey=")))
		self = "true";
		
	}
	else	{
		self = "false";
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if ( (document.all.tags('INPUT').item(j).checked) && (document.all.tags('INPUT').item(j).name.indexOf('cboxUpdate_')!=-1) )	{
				copyObjects = copyObjects + document.all.tags('INPUT').item(j).name.replace("cboxUpdate_","") + "|";
			}
		}
	}
	
	if (copyObjects!="")	{
		
		var xmlHttpObj = new ActiveXObject('Microsoft.XMLHTTP');
		xmlHttpObj.open('GET','Templates/Copy.aspx?FromReport=true&Pkey='+ curkey +'&Self='+ self +'&Objects=' + copyObjects + '|', false);
		xmlHttpObj.setRequestHeader('Pragma','no-cache');
		xmlHttpObj.setRequestHeader('Cache-control','no-cache');
		xmlHttpObj.send();
		var resp = xmlHttpObj.responseText;
		
	//if (copyObjects!="")	{
    //		window.open('Templates/Copy.aspx?FromReport=true&Pkey='+ curkey +'&Self='+ self +'&Objects=' + copyObjects + '|', 'CutWin','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	}
	else	{
		alert('Please choose an object');
	}
}

function SaveObjToSession()	{
	var j=0;
	var copyObjects = "";
	var temp = "";
	var curkey = "";
	var self = "";
	
	temp = document.location.href;
	
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	var fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));

	if (SaveObjToSession.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		copyObjects = document.location.href;
		copyObjects = copyObjects.substring(copyObjects.indexOf("Pkey=",0)+5,copyObjects.indexOf("&",copyObjects.indexOf("Pkey=")))
		self = "true";
		
	}
	else	{
		self = "false";
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (document.all.tags('INPUT').item(j).name != 'allbox')
					copyObjects = copyObjects + document.all.tags('INPUT').item(j).name + "|";
			}
		}
	}
	if (copyObjects!="")	{
		window.open('Copy.aspx?Pkey='+ curkey +'&Parent='+parentFather+'&PkeyCatalog='+catalog+'&Self='+ 'savesession' +'&Objects=' + copyObjects + '|', 'CutWin','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	}
	else	{
		alert('Please choose an object');
	}
}

function CutObj()	{
	var j=0;
	var cutObjects = "";
	var temp = "";
	var curkey = "";
	var self = "";
	
	temp = document.location.href;
	
	var parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	var fatherObject2 = top.document.location.href;
	var catalog;
	if (fatherObject2.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			
			catalog = document.getElementById("catkey").value;
			//alert(catalog);
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = fatherObject2.substring(fatherObject2.indexOf("PkeyCatalog=",0)+12,fatherObject2.length);	
	}
	
	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	
	
	if (CutObj.arguments.length>0)	{
		//updateObject = UpdateObj.arguments[0];
		cutObjects = document.location.href;
		cutObjects = cutObjects.substring(cutObjects.indexOf("Pkey=",0)+5,cutObjects.indexOf("&",cutObjects.indexOf("Pkey=")))
		self = "true";
		
	}
	else	{
		self = "false";
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				cutObjects = cutObjects + document.all.tags('INPUT').item(j).name + "|";
			}
		}
	}
	if (cutObjects!="")	{
		window.open('Cut.aspx?Pkey='+ curkey +'&Parent='+parentFather+'&PkeyCatalog='+catalog+'&Self='+ self +'&Objects=' + cutObjects + '|', 'CutWin','screenX=0,screenY=0,scrollbars=no,toolbar=no,width=320,top=0,left=0,height=15,location=no,menubar=no');
	}
	else	{
		alert('Please choose an object');
	}
}

function PasteObj(sClass, sTypeName, PasteOption, DisablePasteOptions, KeepOccur, bOnlyForHandlerUse)	{
	//Parameters:
	//	sClass = Class of authorized class - |10|=part, |18|=winobject. |10|18| is permit....
	//  sTypeName = name of objecttype authrized - example: |Task|, |Folder| .... |Task|Activity| is permit.
	//  PasteOption = automatic paste:
	//	0 - Paste
	//	1 - Paste as Duplicate
	//	2 - Deep Paste as Duplicate (not available yet)
	//	3 - Paste as WebLink (General Link)
	//	4 - Paste as WebLink (TreeLink)
	//  DisablePasteOptions = Id's of not Allowing Paste options:
	//  |0|-Paste, |1|-Paste as duplicate, |2|-Deep Paste as Duplicate, |3|-Paste as WebLink (General Link), |4|-Paste as WebLink (TreeLink). example: |0| - only paste is allowed. |1|2| - only paste or paste as duplicate are aloowed.
	//  KeepOccur = True keeps the occurence properties.
	// bOnlyForHandlerUse = True deletes the paste duplicate objects(but runs the handler for them before deletion) (only in paste as duplicate) -

	if (typeof(KeepOccur)=="undefined") KeepOccur = "";
	if (typeof(DisablePasteOptions)=="undefined") DisablePasteOptions = "";
	if (typeof(bOnlyForHandlerUse)=="undefined") bOnlyForHandlerUse="False"
	
	var temp = "";
	var curkey = "";
	var catalog;
	if (top.document.location.href.indexOf("PkeyCatalog=",0)==-1)	{
		if (top.frames.length==0)	{
			catalog = document.getElementById("catkey").value;
		}
		else	{
			catalog = top.frames['Index'].document.getElementById("catkey").value;
		}
	}
	else	{
		catalog = top.document.location.href.substring(top.document.location.href.indexOf("PkeyCatalog=",0)+12,top.document.location.href.length);	
	}
	
	temp = document.location.href;
	curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	var	parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));

	if (curkey!="")	{
		window.open('Paste.aspx?Pkey='+ curkey + '&PasteCatalog=' + catalog + '&Class=' + sClass + '&TypeName=' + sTypeName + '&PasteOption=' + PasteOption + '&KeepOccur=' + KeepOccur + '&ParentKey=' + parentFather + '&DisablePasteOptions=' + DisablePasteOptions + '&OnlyForHandlerUse=' + bOnlyForHandlerUse,'PasteObj','screenX=0,screenY=0,scrollbars=yes,toolbar=no,width=420,top=0,left=0,height=520,location=no,menubar=no');
	}
}


function PasteObjAJAX(sClass, sTypeName, PasteOption, DisablePasteOptions, KeepOccur, bOnlyForHandlerUse, sCatalog, sCurKey, sParentKey)	{
	//Parameters:
	//	sClass = Class of authorized class - |10|=part, |18|=winobject. |10|18| is permit....
	//  sTypeName = name of objecttype authrized - example: |Task|, |Folder| .... |Task|Activity| is permit.
	//  PasteOption = automatic paste:
	//	0 - Paste
	//	1 - Paste as Duplicate
	//	2 - Deep Paste as Duplicate (not available yet)
	//	3 - Paste as WebLink (General Link)
	//	4 - Paste as WebLink (TreeLink)
	//  DisablePasteOptions = Id's of Allowing Paste options:
	//  |0|-Paste, |1|-Paste as duplicate, |2|-Deep Paste as Duplicate, |3|-Paste as WebLink (General Link), |4|-Paste as WebLink (TreeLink). example: |0| - only paste is allowed. |1|2| - only paste or paste as duplicate are aloowed.
	//  KeepOccur = True keeps the occurence properties.
	// bOnlyForHandlerUse = True deletes the paste duplicate objects(but runs the handler for them before deletion) (only in paste as duplicate) -
	// sCatalog, sCurKey, sParentKey - optional parameters - send the catalog, parentkey, currentkey to paste the object.

	if (typeof(KeepOccur)=="undefined") KeepOccur = "";
	if (typeof(DisablePasteOptions)=="undefined") DisablePasteOptions = "";
	if (typeof(bOnlyForHandlerUse)=="undefined") bOnlyForHandlerUse="False"
	
	var temp = "";
	var curkey = "";
	var catalog;
	
	if (typeof(sCatalog)=="undefined")
	{
		if (top.document.location.href.indexOf("PkeyCatalog=",0)==-1)	{
			if (top.frames.length==0)	{
				catalog = document.getElementById("catkey").value;
			}
			else	{
				catalog = top.frames['Index'].document.getElementById("catkey").value;
			}
		}
		else	{
			catalog = top.document.location.href.substring(top.document.location.href.indexOf("PkeyCatalog=",0)+12,top.document.location.href.length);	
		}
	}
	else	{
		catalog = sCatalog;
	}	
	temp = document.location.href;
	
	if (typeof(sCurKey)=="undefined")
		curkey = temp.substring(temp.indexOf("Pkey=",0)+5,temp.indexOf("&",temp.indexOf("Pkey=")));
	else
		curkey = sCurKey;

	if (typeof(sParentKey)=="undefined")
		var	parentFather = temp.substring(temp.indexOf("ParentKey=",0)+10,temp.indexOf("&",temp.indexOf("ParentKey=")));
	else
		var	parentFather = sParentKey;
	if (curkey!="")	{
		var resp = OpenURLAjax('Paste.aspx?Pkey='+ curkey + '&PasteCatalog=' + catalog + '&Class=' + sClass + '&TypeName=' + sTypeName + '&PasteOption=' + PasteOption + '&KeepOccur=' + KeepOccur + '&ParentKey=' + parentFather + '&DisablePasteOptions=' + DisablePasteOptions + '&OnlyForHandlerUse=' + bOnlyForHandlerUse);
		//refresh the page only if paste to currentpkey.
		if (typeof(sCurKey)=="undefined")
			document.location.href = document.location.href;
	}
}

function getIDofSelectedCbox()
{
	for (j=0; j<document.all.tags('INPUT').length; j++)		{
		if (document.all.tags('INPUT').item(j).checked)	{
			return(document.all.tags('INPUT').item(j).id);
		}
	}
}

function printPage(PrintTemplate, CatalogPkey, Checkbox)	{
	var PrintObject="";
	var j=0;
	
	if (printPage.arguments.length<3)	{
		PrintObject = document.location.href;
		PrintObject = PrintObject.substring(PrintObject.indexOf("Pkey=",0)+5,PrintObject.indexOf("&",PrintObject.indexOf("Pkey=")))
//		if (PrintObject!="")	{
//			window.open('../GenerateReport.aspx?Catalog=' + CatalogPkey + '&amp;Table=T_CAT_PART&amp;XSL=' + PrintTemplate + '&amp;PKEY=' + PrintObject ,'PrintWin');
//		}
	}
	else	{
		for (j=0; j<document.all.tags('INPUT').length; j++)		{
			if (document.all.tags('INPUT').item(j).checked)	{
				if (PrintObject=="")	{
					PrintObject = document.all.tags('INPUT').item(j).name;
				}
				else	{
					alert("Please choose one object");
					return(0);
				}
			}
		}
	}		
		
	if (PrintObject!="")	{
		w=window.open('../GenerateReport.aspx?Catalog=' + CatalogPkey + '&amp;Table=T_CAT_PART&amp;XSL=' + PrintTemplate + '&amp;PKEY=' + PrintObject ,'PrintWin');
		w.focus();
	}
	else	{
		alert('Please choose an object');
	}		
		
		
}




function FileLocation(FullFileName)	{
 //	alert(FullFileName);
	var iend = FullFileName.lastIndexOf("\\");
	
	if (iend == -1)
		iend = FullFileName.lastIndexOf("\/");
	
	w=window.open(FullFileName.substr(0,iend),'Folder');
	//w.focus();

}




function FindCharOnInput(allInput,CheckChar)	{
	for(i=0; i<allInput.length; i++)	{
		if (allInput[i].value.indexOf(CheckChar) != -1)	{
		// return i+1 ; in case that the char is found at position 0 - should return true
			return i+1;
		}
	}
	return false;
}


function UnCheckedAllChecbox(allInput)	{
	for(i=0; i<allInput.length; i++)	{
		if (allInput[i].type == 'checkbox')	{
				allInput[i].checked=false;
		}
		}
}

function ChangeAllChecbox(allInput,TopCheckbox)	{
	for(i=0; i<allInput.length; i++)	{
		if (allInput[i].type == 'checkbox' && !(allInput[i].disabled))	{
				allInput[i].checked=TopCheckbox.checked;
		}
	}
}

function CheckOnlyOneVB(CheckedRowName,GroupPrefix)	{
	//Unchecked all checkbox expet the current (Name=CheckedRowName)
	//GroupPrefix input ID of checkboxes 
	for (j=0; j<document.all.tags('INPUT').length; j++)		{
		if ((document.all.tags('INPUT').item(j).type=='checkbox') && (document.all.tags('INPUT').item(j).id.indexOf(GroupPrefix)!= -1) && (document.all.tags('INPUT').item(j).checked) && (document.all.tags('INPUT').item(j).name != CheckedRowName))	{
				document.all.tags('INPUT').item(j).checked=false
		}
	}
}


function CheckOnlyOne(CheckedRow,GroupPrefix)	{
	//Unchecked all checkbox expet the current (CheckedRow)
	// CheckedRow - this
	//GroupPrefix input ID of checkboxes 
	for (j=0; j<document.all.tags('INPUT').length; j++)		{
		if ((document.all.tags('INPUT').item(j).type=='checkbox') && (document.all.tags('INPUT').item(j).id.indexOf(GroupPrefix)!= -1) && (document.all.tags('INPUT').item(j).checked) && (document.all.tags('INPUT').item(j) != CheckedRow))	{
				document.all.tags('INPUT').item(j).checked=false
		}
	}
}


function CheckOne(GroupPrefix)	{
	//find if more then one checkbox are checked (in group GroupPrefix)
	//return -1 if no checkbox or more then one are chosen
	//return the index of the chosen
	//GroupPrefix input ID of checkboxes 
	var ichecked = -1;
	for (j=0; j<document.all.tags('INPUT').length; j++)		{
		if ((document.all.tags('INPUT').item(j).type=='checkbox') && (document.all.tags('INPUT').item(j).id.indexOf(GroupPrefix)!= -1) && (document.all.tags('INPUT').item(j).checked))	{
				if(ichecked ==-1)	{
					ichecked = j;
				}
				else	{
					alert("Please choose one object");
					return(-1);
				}	
		}
	}
	if(ichecked ==-1)	
		// no object is checked 
		alert("Please choose an object");
	return(ichecked);
}

function NoChecked(GroupPrefix)	{
	//find if no checkbox is checked (in group GroupPrefix)
	//return false  if no checkbox
	//return true if at least on is checked
	//GroupPrefix input ID of checkboxes 
	
	var inputAraay=document.all.tags('INPUT');
	
	for (j=0; j<inputAraay.length; j++)		{
		if ((inputAraay.item(j).type=='checkbox') && (inputAraay.item(j).id.indexOf(GroupPrefix)!= -1) && (inputAraay.item(j).checked))	{
				return true
		}
	}

	return false;
}

function ShowHideFields(trID,GifName,Hide)	{
	if (document.all(trID).length>1)
	{
		if (!hide)	{	
			for(ij=0;ij<document.all(trID).length;ij++)
			{
				var sty = document.all(trID)(ij).style; 
				sty.display = 'none';
			}
			GifName.src="WEImages/Arrow_d.gif";
		}
		else	{
			for(ij=0;ij<document.all(trID).length;ij++)
			{
				var sty = document.all(trID)(ij).style; 
				sty.display = '';
			}
			GifName.src="WEImages/Arrow_u.gif";
		}
	}
}

function ShowHideColumn(tdID,hide)	{
	if (tdID.length>1)
	{
		if (!hide)	{	
			for(ij=0;ij<tdID.length;ij++)
			{
				var sty = tdID(ij).style; 
				sty.display = 'none';
			}
		}
		else	{
			for(ij=0;ij<tdID.length;ij++)
			{
				var sty = tdID(ij).style; 
				sty.display = '';
			}
		}
	}
}

function ReplaceNullPic(IMGName,PicPath)	{
//	alert(IMGName.comlete);
//	alert(IMGName.fileSize);
	if(IMGName.fileSize==-1)
		IMGName.src=PicPath + "Empty.gif";
}


