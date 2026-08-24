<?xml version="1.0" encoding="ISO-8859-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/TR/WD-xsl">

<xsl:template match="/">

	<html>
	<base target="_parent" />
	<head>
	<title>Header</title>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1255" />

	<LINK REL="stylesheet" HREF="LinkWare1.css" TYPE="text/css" />
	<!-- <LINK REL="stylesheet" HREF="ontop.css" TYPE="text/css"> -->
	</head>

	<body class="BodyCatalog" topmargin="0" rightmargin="0" leftmargin="0">

    <SCRIPT LANGUAGE="javascript" SRC="tooltip.js"></SCRIPT>
    <SCRIPT LANGUAGE="javascript" SRC="mapzoom.js"></SCRIPT>
    <SCRIPT LANGUAGE="javascript" SRC="specialfx.js"></SCRIPT>
    <SCRIPT LANGUAGE="javascript1.2" SRC="ontop.js"></SCRIPT>
    <SCRIPT LANGUAGE="javascript1.2" SRC="cookutil.js"></SCRIPT>
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

    <!--========================== Infromation Toolbar =======================-->
    <!-- %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% -->
    <div align="right"  width="96%">
      <table class="boldText"  cellpadding="2" cellspacing="0" border="0" bordercolor="Blue" dir="rtl" width="100%">
        <tr>
          <td >
            <nobr>
              <img src="Images/icon_catalog.gif" align="absmiddle"></img>
              קטלוג:
              <xsl:value-of select="Header/CatalogName" />
            </nobr>
          </td>
          <td   dir="rtl">
            <nobr>
              &#160;&#160;&#160;
              <img src="Images/icon_folder.gif" align="absmiddle"></img>
              נושא:
              <xsl:value-of select="Header/ChapterName" />
            </nobr>
          </td>
          <td nobr="" dir="rtl">
            <nobr>
              &#160;&#160;&#160;
              <img src="Images/icon_document.gif" align="absmiddle"></img>
              דף:
              <xsl:value-of select="Header/PageHebDesc" />
            </nobr>
          </td>

          <td nobr="" dir="rtl">
            <nobr>
              &#160;&#160;&#160;
              <img src="Images/page.gif" align="absmiddle"></img>
              תמונה מספר:
              <xsl:value-of select="Header/PageNum" />
            </nobr>
          </td>


        </tr>
      </table>
    </div>
    <!-- ==================================================================== -->
    <!--========================== Action Toolbar ============================-->
    <!-- %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% -->
    <div align="center" style="border:2px ;" >
      <table class="TableMainBar" border="0" width="100%">
        <tr align="center" >
          <!--========================== Picture zoom preferences ==============-->
          <!-- %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% -->
          <td>
            <div id="scontentmain">
              <div id="scontentbar" >
                <table border="0" id="moveTable">
                  <tr>
                    <td id="moveTable" style="cursor:hand;">
                      <img align="absmiddle" id="moveTable" style="cursor:hand;" src="images/header/zoom_out.gif" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,0.85)" />
                      <img align="absmiddle" id="moveTable" style="cursor:hand;" src="images/header/zoom_in.gif" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value,1.2)" />
                    </td>

                    <td align="left" id="moveTable">
                      <div align="center" id="scontentsub" style="cursor:hand;">
                        <SPAN id="percent" style="COLOR:darkblue;FONT-WEIGHT:800" align="center"></SPAN>
                        <img style="cursor:hand;" src="images/header/lw_fit.gif" alt="כל הרוחב" align="absmiddle" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, fitSz(), false);" />
                        <img style="cursor:hand;" src="images/header/actual_size.gif" alt="100%" align="absmiddle" onclick="fnZoom(top.frames('Parts Map').window.map_name.value, top.frames('Parts Map').window.pic_name.value, 1, false);" />

                        <img style="cursor:hand;" src="images/header/zoom_saved.gif" alt="הקפאה" align="absmiddle" onclick="javascript:SetZoomSize('');" />
                      </div>
                      <SPAN id="freezeImageOk" align="center"></SPAN>
                    </td>
                  </tr>
                </table>
              </div>
            </div>
          </td>
          <!--========================== Page Navigation(Back,Next,Index)=======-->
          <!-- %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% -->
          <td>
            <SPAN id="freezeFrameOk" align="center"></SPAN>
          </td>
          <td>
            <nobr>
              <!--
					<a href="javascript:;"><IMG src="back.jpg" alt = "אחורה"	onclick="javascript:parent.history.back();"></a>
					<a href="javascript:;"><IMG src="forward.jpg" align="absbottom" alt ="קדימה" onclick="javascript:parent.history.forward();"></a>
					-->

              <xsl:choose>
                <xsl:when match=".[Header/Prev $eq$ '']">
                </xsl:when>
                <xsl:otherwise>
                  <a>
                    <xsl:attribute name="href">
                      <xsl:value-of select="Header/Prev" />
                    </xsl:attribute>
                    <IMG src="images/header/prev.gif" alt = "דף קודם" />
                  </a>
                </xsl:otherwise>
              </xsl:choose>


              <xsl:choose>
                <xsl:when match=".[Header/Next $eq$ '']">
                </xsl:when>
                <xsl:otherwise>
                  <a>
                    <xsl:attribute name="href">
                      <xsl:value-of select="Header/Next" />
                    </xsl:attribute>
                    <IMG src="images/header/next.gif" alt = "דף הבא" />
                  </a>
                </xsl:otherwise>
              </xsl:choose>

            </nobr>
            <a>
              <xsl:attribute name="href">
                <xsl:value-of select="Header/Index" />
              </xsl:attribute>
              <IMG src="images/header/lw_index.gif" alt = "תוכן" />
            </a>
            <SCRIPT LANGUAGE="javascript">
              //Add the correct href acording to the url - local or internet
              CheckHomeURL();
            </SCRIPT>

            <IMG src="images/header/lw_home.gif" alt="חזרה לעמוד ראשי" />
          </td>
          <!--========================== Search And Help =======================-->
          <!-- %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% -->
          <td>
            <SCRIPT LANGUAGE="javascript">
              //Add the correct href acording to the url - local or internet

              CheckSearchURL();
            </SCRIPT>
            <IMG src="images/header/lw_search.gif" alt = "חיפוש" />
            <a href="javascript:;" onclick="CheckHelpURL();">
              <img src="images/header/lw_help.gif" alt="עזרה" />
            </a>
          </td>
          
        </tr>
      </table>
    </div>
    <!--======================================================================-->
	
	</body>
	</html>

</xsl:template>

</xsl:stylesheet>
