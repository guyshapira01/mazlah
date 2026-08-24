<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="UTF-8" omit-xml-declaration="yes" />

<xsl:template match="/">

	<html >
	<base target="_parent" />
	<head>
	<title>Header</title>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

	<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
	<!-- <LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css"> -->
	
<style type="text/css">
html, body {
    margin: 0;
    padding: 0;
    background: #f4f7fb;
    font-family: "Segoe UI", Arial, sans-serif;
    color: #13213c;
}
.BodyCatalog {
    margin: 0 !important;
    padding: 0 !important;
    background: #f4f7fb;
    font-family: "Segoe UI", Arial, sans-serif;
}
.header-shell {
    margin: 5px 8px 4px 8px;
    border: 1px solid #a9c3e2;
    border-radius: 9px;
    overflow: hidden;
    background: #fff;
    box-shadow: 0 2px 8px rgba(28,67,112,.12);
}
.header-info {
    min-height: 32px;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 18px;
    padding: 5px 12px;
    direction: rtl;
    background: linear-gradient(to bottom, #1459a2, #0c4384);
    color: #fff;
    font-size: 12px;
    font-weight: 600;
}
.info-main, .info-date {
    display: flex;
    align-items: center;
    gap: 18px;
    min-width: 0;
}
.info-item {
    display: inline-flex;
    align-items: center;
    gap: 6px;
    white-space: nowrap;
}
.info-label { color: #cfe3fb; font-weight: 500; }
.info-value { color: #fff; font-weight: 700; }
.info-icon {
    width: 16px;
    height: 16px;
    object-fit: contain;
}
.header-actions {
    min-height: 52px;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    justify-content: space-between;
    gap: 14px;
    padding: 7px 12px;
    background: linear-gradient(to bottom, #ffffff, #f7faff);
    direction: ltr;
}
.toolbar-group {
    display: flex;
    align-items: center;
    gap: 5px;
}
.toolbar-separator {
    width: 1px;
    height: 30px;
    background: #d7e2ef;
    margin: 0 7px;
}
.tool-btn {
    width: 36px;
    height: 36px;
    box-sizing: border-box;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    border: 1px solid #c9d8e9;
    border-radius: 7px;
    background: #fff;
    cursor: pointer;
    text-decoration: none;
    box-shadow: 0 1px 2px rgba(20,55,95,.06);
    transition: background-color .12s ease, border-color .12s ease, transform .12s ease;
}
.tool-btn:hover {
    background: #edf5ff;
    border-color: #91b6df;
    transform: translateY(-1px);
}
.tool-btn.active {
    background: #1459a2;
    border-color: #0e4a8c;
}
.tool-btn img {
    width: 21px;
    height: 21px;
    object-fit: contain;
    border: 0;
}
.zoom-value {
    min-width: 50px;
    height: 34px;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    color: #124d91;
    font-size: 14px;
    font-weight: 700;
    background: #eef5fd;
    border-radius: 7px;
    padding: 0 7px;
    box-sizing: border-box;
}
.action-label {
    font-size: 10px;
    color: #536b88;
    text-align: center;
    line-height: 1;
    margin-top: 2px;
}
.tool-stack {
    display: inline-flex;
    flex-direction: column;
    align-items: center;
    gap: 1px;
}
#freezeImageOk, #freezeFrameOk {
    color: #218545;
    font-size: 12px;
    font-weight: 700;
}
@media (max-width: 1100px) {
    .header-info { gap: 8px; font-size: 11px; }
    .info-main { gap: 9px; }
    .header-actions { gap: 7px; padding-left: 7px; padding-right: 7px; }
    .tool-btn { width: 33px; height: 33px; }
    .toolbar-separator { margin: 0 3px; }
}
</style>

</head>

	<body class="BodyCatalog" topmargin="0" rightmargin="0" leftmargin="0">

	<SCRIPT LANGUAGE="javascript" SRC="tooltip.js"></SCRIPT>
	
	<SCRIPT LANGUAGE="javascript" SRC="specialfx.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="ontop.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="cookutil.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="mapzoom.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="panning.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="events.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="addSearchFocus.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="srcpath.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript1.2" SRC="freeze.js"></SCRIPT>
	<SCRIPT LANGUAGE="javascript" SRC="../SearchPath.js"></SCRIPT>

	<SCRIPT LANGUAGE="javascript">
		document.linkColor="#c7c7ba";
		document.alinkColor="#c7c7ba";
		document.vlinkColor="#c7c7ba";
	</SCRIPT>
	<SCRIPT LANGUAGE="javascript">
		// Chrome-safe bridge: zoom logic must execute inside the "Parts Map" frame,
		// because that is where myimg, the image-map and the canvas live.
		function getPartsMapFrameForZoom() {
			try {
			
				return top.frames['Parts Map'] || parent.frames['Parts Map'] || null;
			} catch (e) {
				return null;
			}
		}

		function updateHeaderZoomPercent(ratio) {
			var el = document.getElementById('percent');
			if (!el) return;

			var n = parseFloat(ratio);
			if (!isNaN(n) &amp;&amp; n &gt; 0) {
				el.textContent = Math.round(n * 100) + '%';
			}
		}

		function HeaderZoomNew(ratio) {
			var mapFrame = getPartsMapFrameForZoom();
			if (!mapFrame) return false;

			var n = parseFloat(ratio);
			if (isNaN(n) || n &lt;= 0) n = 1;

			if (typeof mapFrame.fnZoomNew === 'function') {
				mapFrame.fnZoomNew.call(mapFrame, n);
				updateHeaderZoomPercent(n);
				return false;
			}

			return false;
		}

		function HeaderAddZoom(delta) {
			var mapFrame = getPartsMapFrameForZoom();
			if (!mapFrame) return false;

			// Prefer the original function, but execute it in the Parts Map frame.
			if (typeof mapFrame.fnAddZoom === 'function') {
				mapFrame.fnAddZoom.call(mapFrame, delta);

				// Read the ratio cookie if the legacy function updated it.
				try {
					var ratio = parseFloat('0' + mapFrame.getCookie('_zoom_ratio'));
					if (!isNaN(ratio) &amp;&amp; ratio &gt; 0) {
						updateHeaderZoomPercent(ratio);
					}
				} catch (e) {}
				return false;
			}

			// Fallback if fnAddZoom itself is IE-specific.
			var ratio = 1;
			try {
				ratio = parseFloat('0' + mapFrame.getCookie('_zoom_ratio'));
			} catch (e) {}

			if (isNaN(ratio) || ratio &lt;= 0) ratio = 1;
			ratio += parseFloat(delta);
			if (ratio &lt; 0.1) ratio = 0.1;

			return HeaderZoomNew(ratio);
		}

		function HeaderFitSize() {
			var mapFrame = getPartsMapFrameForZoom();
			if (!mapFrame) return false;

			var ratio = null;

			if (typeof mapFrame.fitSz === 'function') {
				try {
					ratio = mapFrame.fitSz.call(mapFrame);
				} catch (e) {}
			}

			// Modern fallback: fit the original image width into the Parts Map viewport.
			if (!ratio || isNaN(parseFloat(ratio)) || parseFloat(ratio) &lt;= 0) {
				try {
					var img = mapFrame.document.getElementById('myimg');
					if (img) {
						var originalWidth = img.naturalWidth || img.width;
						var viewportWidth = mapFrame.document.documentElement.clientWidth ||
											mapFrame.document.body.clientWidth;
						if (originalWidth &gt; 0 &amp;&amp; viewportWidth &gt; 0) {
							ratio = (viewportWidth - 20) / originalWidth;
						}
					}
				} catch (e) {}
			}

			return HeaderZoomNew(ratio || 1);
		}

		function HeaderSaveZoom() {
		alert();
			var mapFrame = getPartsMapFrameForZoom();
			if (!mapFrame) return false;

			if (typeof mapFrame.SetZoomSize === 'function') {
				mapFrame.SetZoomSize.call(mapFrame, '');
			}

			return false;
		}

		// Show current zoom value when Header loads.
		window.addEventListener('load', function() {

			var mapFrame = getPartsMapFrameForZoom();
			if (!mapFrame)
				return;

			try {

				var cookieValue = getCookie('_zoom_ratio');

				alert("cookie = " + cookieValue);

				var ratio = parseFloat(cookieValue);

				if (!isNaN(ratio) &amp;&amp; ratio &gt; 0) {
					updateHeaderZoomPercent(ratio);
				}
				else {
					updateHeaderZoomPercent(1);
				}
			}
			catch (e) {
				alert("ERROR: " + e.message);
				updateHeaderZoomPercent(1);
			}
		});
	</SCRIPT>

    <!-- Modern Header - Option 1 -->
    <div class="header-shell">

      <div class="header-info">
        <div class="info-main">
          <span class="info-item">
            <img class="info-icon" src="images/header/modern/catalog.png" alt="" />
            <span class="info-label">קטלוג:</span>
            <span class="info-value"><xsl:value-of select="Header/CatalogName" /></span>
          </span>

          <span class="info-item">
            <img class="info-icon" src="images/header/modern/folder.png" alt="" />
            <span class="info-label">נושא:</span>
            <span class="info-value"><xsl:value-of select="Header/ChapterName" /></span>
          </span>

          <span class="info-item">
            <img class="info-icon" src="images/header/modern/document.png" alt="" />
            <span class="info-label">דף:</span>
            <span class="info-value"><xsl:value-of select="Header/PageHebDesc" /></span>
          </span>

          <span class="info-item">
            <img class="info-icon" src="images/header/modern/image.png" alt="" />
            <span class="info-label">תמונה מספר:</span>
            <span class="info-value"><xsl:value-of select="Header/PageNum" /></span>
          </span>
        </div>

        <div class="info-date">
          <span class="info-item">
            <img class="info-icon" src="images/header/modern/calendar.png" alt="" />
            <span class="info-label">מעודכן לתאריך:</span>
            <span class="info-value"><xsl:value-of select="Header/UpdateDate" /></span>
          </span>
        </div>
      </div>

      <div class="header-actions">

        <!-- Zoom -->
        <div class="toolbar-group">
          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="return HeaderAddZoom(-0.1);" title="הקטנה">
              <img src="images/header/modern/zoom-out.png" alt="הקטנה" />
            </a>
          </div>
          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="return HeaderAddZoom(0.1);" title="הגדלה">
              <img src="images/header/modern/zoom-in.png" alt="הגדלה" />
            </a>
          </div>
          <span id="percent" class="zoom-value">100%</span>
          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="return HeaderFitSize();" title="התאם לרוחב">
              <img src="images/header/modern/fit.png" alt="התאם לרוחב" />
            </a>
          </div>
          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="return HeaderZoomNew(1);" title="100%">
              <img src="images/header/modern/actual.png" alt="100%" />
            </a>
          </div>
          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="return HeaderSaveZoom();" title="שמור גודל">
              <img src="images/header/modern/pin.png" alt="שמור גודל" />
            </a>
          </div>
          <span id="freezeImageOk"></span>
        </div>

        <div class="toolbar-separator"></div>

        <!-- Layout -->
        <div class="toolbar-group">
          <div class="tool-stack">
            <a class="tool-btn" target="_blank" id="mapat_pritim_top" title="טבלה צפה">
              <xsl:attribute name="onclick">
                javascript:openWin( <xsl:value-of select="Header/WindowSourceFile" />,'MyWin2', 'width=300,height=200,toolbar=0,location=0,directories=0,status=0,menuBar=0,scrollBars=2,resizable=1' )
              </xsl:attribute>
              <img src="images/header/modern/float.png" alt="טבלה צפה" />
            </a>
            <span class="action-label">צפה</span>
          </div>

          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="javascript:SetColRatio('0%,*,0%');" title="טבלה">
              <img src="images/header/modern/table.png" alt="טבלה" />
            </a>
            <span class="action-label">טבלה</span>
          </div>

          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="javascript:SetColRatio('100%,*,*');" title="איור">
              <img src="images/header/modern/image-view.png" alt="איור" />
            </a>
            <span class="action-label">איור</span>
          </div>

          <div class="tool-stack">
            <a class="tool-btn " href="javascript:;" onclick="javascript:SetColRatio('65%,35%,*');" title="איור וטבלה">
              <img src="images/header/modern/split.png" alt="איור וטבלה" />
            </a>
            <span class="action-label">משולב</span>
          </div>

          <div class="tool-stack">
            <a class="tool-btn" href="javascript:;" onclick="javascript:SaveColRatio();" id="pritim_link_top" title="שמור תצוגה">
              <img src="images/header/modern/save.png" alt="שמור תצוגה" />
            </a>
            <span class="action-label">שמור</span>
          </div>
          <span id="freezeFrameOk"></span>
        </div>

        <div class="toolbar-separator"></div>

        <!-- Navigation -->
        <div class="toolbar-group">
          <xsl:if test="normalize-space(Header/Prev) != ''">
            <a class="tool-btn" title="דף קודם">
              <xsl:attribute name="href"><xsl:value-of select="Header/Prev" /></xsl:attribute>
              <img src="images/header/modern/prev.png" alt="דף קודם" />
            </a>
          </xsl:if>

          <xsl:if test="normalize-space(Header/Next) != ''">
            <a class="tool-btn" title="דף הבא">
              <xsl:attribute name="href"><xsl:value-of select="Header/Next" /></xsl:attribute>
              <img src="images/header/modern/next.png" alt="דף הבא" />
            </a>
          </xsl:if>

          <a class="tool-btn" title="תוכן">
            <xsl:attribute name="href"><xsl:value-of select="Header/Index" /></xsl:attribute>
            <img src="images/header/modern/index.png" alt="תוכן" />
          </a>
        </div>

        <div class="toolbar-separator"></div>

        <!-- Search / Help -->
        <div class="toolbar-group">
          <a class="tool-btn"
		   href="javascript:;"
		   onclick="parent.OpenSearch(); return false;"
		   title="חיפוש">
			<img src="images/header/modern/search.png" alt="חיפוש" />
		</a>
          <a class="tool-btn" href="javascript:;" onclick="CheckHelpURL();" title="עזרה">
            <img src="images/header/modern/help.png" alt="עזרה" />
          </a>
        </div>

      </div>
    </div>

   
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
