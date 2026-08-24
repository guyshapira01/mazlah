<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
   <xsl:template match="/">
   <body dir="rtl">
      <table dir="rtl">
	      <tr>
		      	<td>שם הקובץ המקורי: </td>
		      	<td><xsl:value-of select="NewDataSet/Catalogs/PORGFILENAME"/></td>
		</tr>
		<tr>
		      	<td>שם אנגלי: </td>
		      	<td><xsl:value-of select="NewDataSet/Catalogs/PENGDESC"/></td>
		</tr>
		<tr>
		      	<td>שם עיברי: </td>
		      	<td><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></td>
		</tr>
	</table>
	</body>
   </xsl:template>
</xsl:stylesheet>
