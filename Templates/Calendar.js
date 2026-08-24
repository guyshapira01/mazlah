function OpenCalendar(windowName,url)	{
	var left;
	var top;
	
	if (event.screenX+8+216>screen.width)	{
		left=event.screenX-216-8;
	}
	else	{
		left=(event.screenX+8);
	}

	if (event.screenY+195+220>screen.width)	{
		top=event.screenY-220-195;
	}
	else	{
		top=(event.screenY-220);
	}

	windowName=window.open(url,windowName,'width=216,height=195,top=' + top + ',left=' + left + '');
	windowName.focus();
}
