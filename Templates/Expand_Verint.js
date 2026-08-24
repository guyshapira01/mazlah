 function ExpandAll(ExpandStatus1) {
ExpandStatus=ExpandStatus1;
	if(ExpandAllBool!=ExpandStatus)
	{
		if (ExpandStatus=='0')
		  {
		 	 for (var i in document.all) {      
		 	 		//window.setTimeout("void(0)",10);
				    	if (i.substring(0,5).toLowerCase()=='inner') 
				    	{
				     		CloseExpand(i,i.substring(6,(i.length)));
				      }
			 }
			 
		}
		  if (ExpandStatus=='1')
		 {
				   for (var i in document.all) {      
				   	//window.setTimeout("void(0)",10);
				    	if (i.substring(0,5).toLowerCase()=='inner') 
				    	{
				     		OpenExpand(i,i.substring(6,(i.length)));
				      }
			          }
			 
		  }
		  if (ExpandStatus=='2')
		 {
		 ///alert(ExpandStatus);
		 				 for (var i in document.all) {      
		 				 					//window.setTimeout("void(0)",10);
										    	if (i.substring(0,5).toLowerCase()=='inner') 
										    	{
										     		CloseExpand(i,i.substring(6,(i.length)));
										      }
						 }
		 	preloadImages();
		 }
	}	
	addToCookie(ExpandStatus,catalogNumber+'ExpandAll')
	ExpandAllBool=ExpandStatus;
		   
  }
  var ExpandStatus=2;
  var ExpandAllBool;
  var isCookieLoaded=false;
  function ExpandPreload()
  {
		ExpandAllBool =getFromCookie(catalogNumber+'ExpandAll');
		ExpandStatus=ExpandAllBool;
		alert(ExpandAllBool)
		alert(ExpandStatus)
	  	if(ExpandAllBool==null)
	  	{
	  		addToCookie('2',catalogNumber+'ExpandAll')
	  	}
	  	else
	  	{
	  		if(ExpandAllBool=='1')
	  		{
	  			 proloadIcons();
	  			 for (var i in document.all) {      
	  				    	if (i.substring(0,5).toLowerCase()=='inner') 
	  				    	{
	  				     		OpenExpand(i,i.substring(6,(i.length)));
	  				      }
	  			  }	
	  			  //rememberLable.src='Load.jpg';
	  			  ExpandLable.src='Orbotech/LastEnb.jpg';
	  			  ExpandLable.alt='Last State';
	  			  //IndexStatus[1].checked=true;
	  		}
	  		else  if (ExpandAllBool=='2')
	  		 {	
	  		 	
	  		 	preloadImages();
	  			 //rememberLable.src='Remember.jpg';
	  			 ExpandLable.src='Orbotech/ExpndEnb.jpg';
	  			 ExpandLable.alt='Expand All';
	  			 //IndexStatus[2].checked=true;
	  		}			
	
		}
		 
}