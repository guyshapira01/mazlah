function getParamFromUrl(paramName)	{
	var paramValue="";
	var location = top.location.href;
	var iStart = location.indexOf(paramName + "=");
	if (iStart<1)	return "";
	location = location.substring(iStart+paramName.length+1);
	var iEnd = location.indexOf("&");
	if (iEnd<1)	iEnd = location.length;
	paramValue = location.substring(0,iEnd);
	return (paramValue);
}


var mode;

function Publish()	{
	var selectedCatalogs;
	
	
	selectedCatalogs = getParamFromUrl("Catalogs");
	mode = getParamFromUrl("Mode");
	
	//progress = setInterval("sessionPrecent()", 5000);
	
	//xmlhttp
	var xmlHttpPub = new ActiveXObject("Microsoft.XMLHTTP");
			//alert("PublishPackage.aspx?Mode=" + mode + "&Catalogs="+selectedCatalogs + "&CDName=" + PCDNAME.value + "&CDNumber=" + PCDNUMBER.value + "&CDCOMMENT=" + PCOMMENT.value)
			if (mode=='Gen')	{
				//generate only - CD Name is the catalog that we generate.
				var mydate=new Date()
				var year=mydate.getYear()
				if (year < 1000)
				year+=1900
				var day=mydate.getDay()
				var month=mydate.getMonth()+1
				if (month<10)
				month="0"+month
				var daym=mydate.getDate()
				if (daym<10)
				daym="0"+daym
				FullDate = daym + "/" + month + "/" + year
				xmlHttpPub.open("GET", "PublishPackage.aspx?Mode=" + mode + "&Catalogs="+selectedCatalogs + "&CDName=" + getParamFromUrl("CatalogName").replace(/%20/g," ") + "&CDNumber=1&CDCOMMENT=&CDDATE=" + FullDate + "&PIMPORTSTATUS=1", true);
			}
			else	{
				xmlHttpPub.open("GET", "PublishPackage.aspx?Mode=" + mode + "&Catalogs="+selectedCatalogs + "&CDName=" + PCDNAME.value + "&CDNumber=" + PCDNUMBER.value + "&CDCOMMENT=" + PCOMMENT.value + "&CDDATE=" + PDATE.value + "&PIMPORTSTATUS=" + PImportStatus.value, true);
			}
			xmlHttpPub.setRequestHeader("Pragma","no-cache");
			xmlHttpPub.setRequestHeader("Cache-control","no-cache");
			xmlHttpPub.onreadystatechange = function () {
				if (xmlHttpPub.readyState == 4)
					{
					//alert("Finish!"); // responseXML : XmlDocument
					var strLink = xmlHttpPub.responseText;
					xmlHttpPub = null;
					document.location.href = strLink;
					}
			}
			
			xmlHttpPub.send();
			
			
			//xmlHttpPub = null;
	
	//progressbar

}


function sessionPrecent()
{
	var xmlHttpRem = new ActiveXObject("Microsoft.XMLHTTP");
	xmlHttpRem.open("GET", "tomer.txt", false);
	xmlHttpRem.setRequestHeader("Pragma","no-cache");
	xmlHttpRem.setRequestHeader("Cache-control","no-cache");
	xmlHttpRem.send();
			
			if (mode == "GenPub")
			{
				if (xmlHttpRem.responseText >= "200")
				{
					clearInterval(progress);
					xmlHttpRem = null;
					return;
				}
				
				var res = xmlHttpRem.responseText;
				var iRes = res * 1;
				setCount(iRes/2);
			}
			else
			{
				if (xmlHttpRem.responseText >= "100")
				{
					clearInterval(progress);
					xmlHttpRem = null;
					return;
				}
				alert(xmlHttpRem.responseText);
				//document.write("p: " + xmlHttpRem.responseText)
				var res = xmlHttpRem.responseText;
				var iRes = res * 1;
				//setCount(iRes);
			}
		
			xmlHttpRem = null;
		
}