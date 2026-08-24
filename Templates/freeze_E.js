function SaveColRatio() {
	
	var tbl_w, tot_w, map_w, tbl_h, map_h;
	var expdate = new Date();
	expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
	tbl_w = top.frames('Parts Map').window.document.body.clientWidth;
	map_w = top.frames('Parts Table').window.document.body.clientWidth;
	tbl_h = top.frames('Parts Table').window.document.body.offsetHeight ;
	head_h = top.frames('Header').window.document.body.offsetHeight ;
	
	r_w = map_w /(tbl_w+map_w) * 100;
	r_w += "%,*";
	setCookie('_col_ratio', r_w, expdate);
	r_h = head_h/(head_h+tbl_h ) * 100;
	r_h += "%,*";
	
	setCookie('_row_ratio', r_h, expdate);
	
	alert("Freeze Status Saved Successfully");
	
}

function SaveColRatio_IndexPicture() {
	
	var tbl_w, tot_w, map_w, tbl_h, map_h;
	var expdate = new Date();
	expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
	tbl_w = top.frames('Parts Map').window.document.body.clientWidth;
	map_w = top.frames('Index').window.document.body.clientWidth;
	tbl_h = top.frames('Parts Map').window.document.body.offsetHeight ;
	head_h = top.frames('Header').window.document.body.offsetHeight ;
	r_w = map_w/(tbl_w+map_w) * 100;
	r_w += "%,*";
	setCookie('_col_ratio', r_w, expdate);
	r_h = head_h/(head_h+tbl_h ) * 100;
	r_h += "%,*";
	
	setCookie('_row_ratio', r_h, expdate);
	
	alert("Freeze Status Saved Successfully");
	
}

function SaveColRatio_IndexText() {
	
	var tbl_w, tot_w, map_w, tbl_h, map_h;
	var expdate = new Date();
	expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
	tbl_w = top.frames('Parts Map').window.document.body.clientWidth;
	map_w = top.frames('Index').window.document.body.clientWidth;
	tbl_h = top.frames('Parts Map').window.document.body.offsetHeight ;
	head_h = top.frames('Header').window.document.body.offsetHeight ;
	
	r_w = map_w/(tbl_w+map_w) * 100;
	r_w += "%,*";
	setCookie('_col_ratio', r_w, expdate);
	r_h = head_h/(head_h+tbl_h ) * 100;
	r_h += "%,*";
	
	setCookie('_row_ratio', r_h, expdate);
	
	alert("Freeze Status Saved Successfully");
	
}

function SaveColRatio_3frames() {
	
	var index_w, tbl_w, tot_w, map_w, tbl_h, map_h;
	var expdate = new Date();
	expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
	tbl_w = top.frames('Parts Table').window.document.body.clientWidth;
	map_w = top.frames('Parts Map').window.document.body.clientWidth;
	index_w = top.frames('Index').window.document.body.clientWidth;
	tbl_h = top.frames('Parts Table').window.document.body.offsetHeight ;
	head_h = top.frames('Header').window.document.body.offsetHeight ;
	
        r_w_map=map_w/(tbl_w+map_w+index_w) * 100;
        r_w = tbl_w/(tbl_w+map_w+index_w) * 100;
        r_w_index=index_w/(tbl_w+map_w+index_w) * 100;
        

	r_w = r_w_index + "%," + r_w_map + "%," + r_w  + "%";
	alert(r_w);
	setCookie('_col_ratio3', r_w, expdate);
	r_h = head_h/(top.document.body.offsetHeight ) * 100;
	r_h += "%,*";

	setCookie('_row_ratio', r_h, expdate);
	
	//alert("Freeze Status Saved Successfully");
	
}

function SetColRatio(newRatio) {
	window.parent.document.all('inr_frmst').setAttribute('cols', newRatio);
	//window.parent.document.inr_frmst.cols='newRatio';
	var expdate2 = new Date();
	expdate2.setTime(expdate2.getTime() +  (24 * 60 * 60 * 1000 * 365));
	//Set The Cockie the last view
	setCookie('_col_ratio3', newRatio, expdate2);
}

function closeFrame(frameName)
	{
			
		var colRatio;
		var partsMapWidth, partsTableWidth, indexWidth
			
		partsMapWidth = top.frames('Parts Map').window.document.body.clientWidth*1
		partsTableWidth = top.frames('Parts Table').window.document.body.clientWidth*1+10
		indexWidth = top.frames('Index').window.document.body.clientWidth*1+10
			
		partsMapWidth = partsMapWidth*1+6
		partsTableWidth = partsTableWidth*1+6
		indexWidth = indexWidth*1+6

		switch ( frameName ) {
		   case "Parts Map" : 
				partsMapWidth = 0;
				break;
			case "Parts Table" : 
				partsTableWidth = 0;
				break;
			case "Index" : 
				indexWidth = 0;
				break;
		}

		colRatio = partsMapWidth + "," + partsTableWidth + "," + indexWidth
		window.parent.document.all('inr_frmst').setAttribute('cols', colRatio);

	}

function addCloseLine(frameName, windowHeader)
	{
	
		//document.write('<INPUT type="button" value="Button" id=button1 name=button1 onclick="JavaScript:closeFrame(\'' + frameName + '\')">');
		document.write('<p align="right" style="BACKGROUND-COLOR: royalblue">');
		document.write('<table dir=ltr width="100%">');
		document.write('<tr>');
		document.write('<td align="left">' + windowHeader + '</td>');
		document.write('<td align="right"><A style="BACKGROUND-COLOR: Silver" href="JavaScript:void(0)" onclick="JavaScript:closeFrame(\'' + frameName + '\')"><font color="black"><b>X</b></font></a></td>');
		document.write('</tr>');
		document.write('</table>');
		document.write('</p>');
		
	}

function SaveColRatio_DocumentsIndex() {
	
	var tbl_w, tot_w, map_w, tbl_h, map_h;
	var expdate = new Date();
	expdate.setTime(expdate.getTime() +  (24 * 60 * 60 * 1000 * 365));
	tbl_w = top.frames('Parts Table').window.document.body.clientWidth;
	map_w = top.frames('Index').window.document.body.clientWidth;
	tbl_h = top.frames('Parts Table').window.document.body.offsetHeight ;
	head_h = top.frames('Header').window.document.body.offsetHeight ;
	
	r_w = map_w/(tbl_w+map_w) * 100;
	r_w += "%,*";
	setCookie('_col_ratioD2', r_w, expdate);
	r_h = head_h/(head_h+tbl_h ) * 100;
	r_h += "%,*";
	
	setCookie('_row_ratioD', r_h, expdate);
	
	alert("Freeze Status Saved Successfully");
	
}
