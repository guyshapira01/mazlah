function makatWithoutHyphen(makat)	{
	arrayOfStrings = makat.split("-");
	newMakat="";
	for(i=0;i<arrayOfStrings.length;i++)
		newMakat = newMakat + arrayOfStrings[i];
	return(newMakat);
}

function makatDisplay(makat,format)	{
	if (makat=="") return("");
	Makat = makatWithoutHyphen(makat);
	newMakat = "";
	j=0;
	for(i=0;i<format.length;i++)	{
		if (format.substr(i,1)=='x')	{
			newMakat = newMakat + Makat.substr(j,1);
			j++;
		}
		else	{
			newMakat = newMakat + format.substr(i,1);
		}
	}
	for(i=j;i<makat.length;i++)
		newMakat = newMakat + Makat.substr(i,1);
		
	return(newMakat);
}
