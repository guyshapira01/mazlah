// Returns the source path of the request, up to and incl. the "Catalogs/" substr.
// On error will return ""
function getSrcPath() {
debugger;
	var srcPath, iFound;
	// retrieves the URI of the form 'file:///C:/../../Catalogs/SomeCatalog/SomePage.html"
	if (top.frames.length)
		srcPath = new String(top.frames('Header').document.location);
	else
		srcPath = new String(window.document.location);
	//alert(srcPath);
	iFound = srcPath.indexOf('Catalogs',0);
	// check that "Catalogs" substr was found
	if (iFound==(-1)) return "";
	srcPath = srcPath.substring(0,iFound) + "Catalogs";
	return srcPath;
}