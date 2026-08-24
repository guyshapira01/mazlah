
	//Add By Guy
	
	
	var _oLast;

	//function window.onload()
	//{
		//setActive(tdHome);
	//}

	function load(o, sDir, sFile)
	{
		setActive(o);

		var sUrl = "/" + sDir + "/left.aspx";

		if (_oLast.sub)
		{
			sUrl += "?DefaultArea=" + _oLast.sub.id;

			sFile = _oLast.path;

			// Special case for Settings page cause its in an odd directory
			if (sFile == "settings")
			{
				sFile = "tools";
				sDir = "tools";
			}
		}

		top.nav.location.replace(sUrl);
		top.stage.frmPlatform.location.replace("/" + sDir + "/home_" + sFile + ".aspx");		
	}

	function setActiveSub(o, s)
	{
		_oLast.sub = eval("top.nav.nav_" + s);
		_oLast.path = s;
	}

	function setActive(o)
	{
		if (_oLast && o != _oLast)
		{
			glow(false, _oLast, true);
		}

		_oLast = o;

		glow(true, o);
	}

	function glow(bOn, o, bOverride)
	{
		if (bOn)
		{
			setStyle(o, "002564", "A9BEE5", "#ffff00", 100, "progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=#5F789A, EndColorStr=#213D61)");
		}
		else if (o != _oLast || bOverride)
		{
			setStyle(o, "8DA2C8", "273A7D", "", 60, null);
		}
	}
	
	function setStyle(o, sHi, sLo, sText, iOpac, sGrad)
	{
		with (o.style)
		{
			borderTop = "1px solid #" + sHi;
			borderLeft = "1px solid #" + sHi;
			borderRight = "1px solid #" + sLo;
			borderBottom = "1px solid #" + sLo;
		}

		o.runtimeStyle.filter = sGrad;

	}