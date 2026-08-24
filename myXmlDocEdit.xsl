<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
   <xsl:template match="/">
   <link rel="stylesheet" type="text/css" href="Styles.css"></link>
   <body  bgcolor="lightyellow" >
   <form id="frmEdit" method="post">
      <table dir="ltr" width="50%">
	      
		<tr class="csTableTDOne">
		      	<td>Name </td>
		      	<td>
		      		<input name="PHEBDESC" size="50%" type="text"  ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PHEBDESC"/></xsl:attribute>
		      		</input>
		      		<input name="UpdateClick" value="0" type="hidden"></input>
		      		<input name="DocumentAttributes" value="PHEBDESC,PENGDESC,PORGFILENAME,PKEYUSER,PREVISION,PKEYTYPE,PDOCID,PSTATUS,PFILEMODIFIEDDATE,PCREATEDATE,PPROP1,PPROP2,PPROP3,PPROP4,PPROP5,PPROP6" type="hidden"></input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>Description </td>
		      	<td>
		      	<input type="text" size="50%" name="PENGDESC" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PENGDESC"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDOne">
		      	<td>Path: </td>
		      	<td>
		      		<input type="text" size="50%" name="PORGFILENAME" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PORGFILENAME"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>Created Date </td>
		      	<td>
		      	<input type="text" size="50%" name="PCREATEDATE" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PCREATEDATE"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDOne">
		      	<td>XXX</td>
		      	<td>
		      	<input type="text" size="50%" name="PKEYUSER" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PKEYUSER"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>Revision</td>
		      	<td>
		      	<input type="text" size="50%" name="PREVISION" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PREVISION"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDOne">
		      	<td>PKEYTYPE</td>
		      	<td>
		      	<input type="text" size="50%" name="PKEYTYPE" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PKEYTYPE"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>Doc ID</td>
		      	<td>
		      	<input type="text" size="50%" name="PDOCID" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PDOCID"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDOne">
		      	<td>PFILEMODIFIEDDATE</td>
		      	<td>
		      	<input type="text" size="50%" name="PFILEMODIFIEDDATE" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PFILEMODIFIEDDATE"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>PSTATUS</td>
		      	<td>
		      	<input type="text" size="50%" name="PSTATUS" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PSTATUS"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDOne">
		      	<td>PPROP1</td>
		      	<td>
		      	<input type="text" size="50%" name="PPROP1" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPROP1"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>PPROP2</td>
		      	<td>
		      	<input type="text" size="50%" name="PPROP2" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPROP2"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDOne">
		      	<td>PPROP3</td>
		      	<td>
		      	<input type="text" size="50%" name="PPROP3" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPROP3"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>PPROP4</td>
		      	<td>
		      	<input type="text" size="50%" name="PPROP4" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPROP4"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDOne">
		      	<td>PPROP5</td>
		      	<td>
		      	<input type="text" size="50%" name="PPROP5" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPROP5"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr class="csTableTDTwo">
		      	<td>PPROP6</td>
		      	<td>
		      	<input type="text" size="50%" name="PPROP6" ><xsl:attribute name="value"><xsl:value-of select="NewDataSet/Catalogs/PPROP6"/></xsl:attribute>
		      		</input>
		      	</td>
		</tr>
		<tr><td>Status Name </td>
			<td>
				<select name="selStatus" size="1" class="TXTBox" >
					<xsl:for-each select="NewDataSet/Status">
						<option>
						<xsl:choose>
							<xsl:when test="PKEY=//NewDataSet/Catalogs/PSTATUS">
								<xsl:attribute name="selected">selected</xsl:attribute>
							</xsl:when>
						</xsl:choose>
							<xsl:attribute name="value"><xsl:value-of select="PKEY"/></xsl:attribute>
							<xsl:value-of select="PSTATUS"/>
						</option>
					</xsl:for-each>
				</select>
						
				
			</td>
		</tr>

	</table>
	
	
	<input type="submit" id="btnSave" value="Save"><xsl:attribute name="onclick">UpdateClick.value='1';</xsl:attribute></input>
	</form>
	</body>
   </xsl:template>
</xsl:stylesheet>
