

function CheckSearchURL() {
	//set strSrch to the location of search engine
	var strSrch="javascript:parent.OpenSearch();"
    document.write('<a target="_top"  href="' + strSrch + '">');
}

function CheckSearchURLOnline() {
	//set strSrch to the location of search engine
	strSrch='http://lw1/lw-tc2go/';
    return strSrch;
}

function CheckHomeURL() {
	//set strSrch to the location of home page
	//OpenFamilyTV
	var strSrch="try{ window.opener.OpenFamilyTV(); opener.focus();}catch(err){var w = window.open('../Default.aspx?Tab=1');}"
	//document.write( strSrch );
	document.write('<a target="_top" href="javascript:' + strSrch + '">');
}

function CheckHomeURLOnline() {
	//set strSrch to the location of home page
	var strSrch='../cataloglist2go.aspx';
	return strSrch;
}


function CloseURL() {
	document.write('</a>');
}

//OpenSearch('search')
function GetCatalogR()
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
function OpenSearch() {
//debugger;

    try{
        
		
		//var w = window.open('../Default.aspx?Tab=2&PkeyCatalog='+ GetCatalog());
        //opener.form1.TabContainer1.tabIndex = 2;
        //opener.location.href='../aaa.aspx';
        //window.opener.OpenSearchFunc();
        //opener.focus();
		
		var w = window.open('http://localhost/CatmamEngineWeb/Default.aspx?Tab=2&PkeyCatalog='+ GetCatalogR());
        //return false;

       // opener.openLatestCatalogs();
    }
    catch(err){
        //var w = window.open('../Default.aspx?Tab=2');
	var w = window.open('../Default.aspx?Tab=2&PkeyCatalog='+ GetCatalogR());
        //w.opener.GetFunc();
    }

}

     




function CheckHelpURL(){
	var strLocation;
	var WebServerAddress;
	WebServerAddress = "gems-web";	
	strLocation = document.location.href;
	if (strLocation.indexOf(WebServerAddress) == -1 )
	    {
		  //alert("Local");
		  strSrch='../help/help.htm';
		  w=window.open(strSrch);
		  w.focus();
            
            }  
	else	
            {
		 
		  strSrch='http://www.Linkware.com/LinkwareWeb/help/help.htm';
		   w=window.open(strSrch);
		  w.focus();
            }
}

function CheckHelpURL_E(){
	var strLocation;
	var WebServerAddress;
	WebServerAddress = "gems-web";	
	strLocation = document.location.href;
	if (strLocation.indexOf(WebServerAddress) == -1 )
	    {
		  //alert("Local");
		  strSrch='/catalogs/help/help_E.htm';
		  w=window.open(strSrch);
		  w.focus();
            
            }  
	else	
            {
		 
		  strSrch='../help/help_E.htm';
		   w=window.open(strSrch);
		  w.focus();
            }
}
