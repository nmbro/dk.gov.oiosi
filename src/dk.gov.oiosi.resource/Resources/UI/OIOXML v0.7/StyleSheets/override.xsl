<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl= "http://www.w3.org/1999/XSL/Transform"
    xmlns:udk= "http://rep.oio.dk/ubl/xml/schemas/0p71/maindoc/"
    xmlns:com= "http://rep.oio.dk/ubl/xml/schemas/0p71/common/"
    xmlns:pie= "http://rep.oio.dk/ubl/xml/schemas/0p71/pie/"
    xmlns:tpcm="http://rep.oio.dk/ubl/xml/schemas/0p71/testpcm/"
    xmlns:tpcp="http://rep.oio.dk/ubl/xml/schemas/0p71/testpcp/"
    xmlns:tpie="http://rep.oio.dk/ubl/xml/schemas/0p71/testpie/"
    xmlns:tpip="http://rep.oio.dk/ubl/xml/schemas/0p71/testpip/"
    xmlns:pip= "http://rep.oio.dk/ubl/xml/schemas/0p71/pip/"
    xmlns:pcm= "http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/"
    xmlns:pcp= "http://rep.oio.dk/ubl/xml/schemas/0p71/pcp/">

<xsl:import href="html.xsl"/>
<xsl:output method="xml" indent="yes"/>

<xsl:template name="fakturatop">
			<table border="0" width="100%" cellspacing="0" cellpadding="2">
				<tr>
					<td valign="top"><font face="Arial"><xsl:value-of select="$fakturatype"/></font></td>
					<td valign="top"></td>
					<td valign="top"></td>
					<td valign="top"><img src="logo.gif" ALIGN="RIGHT" /> </td>
				</tr>
				<tr>
					<td width="100%" valign="top" colspan="4" bgcolor="#FFFFFF" height="2"><hr color="#A1B4C4" size="5" NOSHADE="true"/></td>
				</tr>

				<tr>
					<td valign="top" bgcolor="#FFFFFF"><font face="Arial" size="1">
						<!-- indsætter køberadressen -->
                                		<b>Køber</b><br/>
                                                EAN: <xsl:value-of select="com:BuyersReferenceID"/><br/>
                                                Ordrekontakt.: <xsl:value-of select="com:BuyerParty/com:BuyerContact/com:Name"/><br/>
                                                DimensionsKonto: <xsl:value-of select="com:BuyerParty/com:AccountCode"/><br/></font>
					</td>
					<td  valign="top" bgcolor="#FFFFFF"><font face="Arial" size="1">
						<!-- indsætter leveringsadressen -->
                                		<b>Leveringsadresse</b><br/>
                                                <xsl:for-each select="com:DestinationParty">
                                                <xsl:if test = "com:Address/com:ID = 'Levering'">
                                                <xsl:value-of select="com:PartyName"/><br/>
                                                <xsl:value-of select="com:Address/com:Street"/>
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:Address/com:HouseNumber"/>
						<xsl:text>, </xsl:text>
						<xsl:value-of select="com:Address/com:InhouseMail"/><br/>
                                                <xsl:value-of select="com:Address/com:AdditionalStreet" /><br/>
                                                <xsl:value-of select="com:Address/com:PostalZone"/> 
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:Address/com:CityName"/><br/>
                                                </xsl:if>
                                                </xsl:for-each>
                                                </font>
	    			        </td>
					<td  valign="top" bgcolor="#FFFFFF"><font face="Arial" size="1">
						<!-- indsætter juridisk adresse -->
                                		<b>Juridisk adresse</b><br/>
                                                <xsl:for-each select="com:BuyerParty">
                                                <xsl:if test="com:Address/com:ID = 'Fakturering'">
                                                <xsl:value-of select="com:PartyName"/><br/>
                                                <xsl:value-of select="com:Address/com:Street"/>
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:Address/com:HouseNumber"/>
						<xsl:text>, </xsl:text>
						<xsl:value-of select="com:Address/com:InhouseMail"/><br/>
						<xsl:value-of select="com:Address/com:AdditionalStreet" /><br/>
                                                <xsl:value-of select="com:Address/com:PostalZone"/> 
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:Address/com:CityName"/><br/>
                                                </xsl:if>
                                                </xsl:for-each>   
                                                </font>
	    			        </td>
					<td  valign="top" bgcolor="#FFFFFF"><font face="Arial" size="1">
						<!-- indsætter faktureringsadressen -->
                                		<b>Faktureringsadresse</b><br/>
                                                <xsl:for-each select="com:BuyerParty">
                                                <xsl:if test = "com:Address/com:ID = 'Fakturering'">
                                                <xsl:value-of select="com:PartyName"/><br/>
                                                <xsl:value-of select="com:Address/com:Street"/>
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:Address/com:HouseNumber"/>
						<xsl:text>, </xsl:text>
						<xsl:value-of select="com:Address/com:InhouseMail"/><br/>
						<xsl:value-of select="com:Address/com:AdditionalStreet" /><br/>
                                                <xsl:value-of select="com:Address/com:PostalZone"/> 
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:Address/com:CityName"/><br/>
                                                </xsl:if>
                                                </xsl:for-each>
                                                </font>
					</td>
				</tr>


				<tr>
					<td width="100%" valign="top" colspan="4" bgcolor="#FFFFFF" height="1"><hr color="#A1B4C4" size="1" NOSHADE="true"/></td>
				</tr>


				<tr>
					<td valign="top" bgcolor="#FFFFFF"><font  face="Arial" size="1">
						<!-- indsætter leverandøradressen -->
                                		<b>Leverandør</b><br/>

                                                <xsl:value-of select="com:SellerParty/com:PartyName"/><br/>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:Street"/>
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:HouseNumber"/><br/>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:PostalZone"/> 
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:CityName"/><br/>
                                                CVR.: <xsl:value-of select="com:SellerParty/com:PartyTaxScheme/com:CompanyTaxID"/><br/></font>
					</td>
					<td valign="top" bgcolor="#FFFFFF"><font  face="Arial" size="1">
						<!-- indsætter Vareafsendelse -->
                                		<b>Vareafsendelse</b><br/>
                                                <xsl:value-of select="com:SellerParty/com:PartyName"/><br/>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:Street"/>
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:HouseNumber"/><br/>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:PostalZone"/> 
                                                <xsl:text> </xsl:text>
                                                <xsl:value-of select="com:SellerParty/com:Address/com:CityName"/><br/>
                                                CVR.: <xsl:value-of select="com:SellerParty/com:PartyTaxScheme/com:CompanyTaxID"/><br/></font>
					</td>
					<td  valign="top" colspan="2" bgcolor="#FFFFFF"><font face="Arial" size="1">
						<!-- indsætter kontaktoplysninger -->
                                		<b>Kontaktoplysninger</b><br/>
                                                <xsl:value-of select="com:SellerParty/com:OrderContact/com:Name"/><br/>
                                                Tlf.: <xsl:value-of select="com:SellerParty/com:OrderContact/com:Phone"/><br/>
						Fax.: <xsl:value-of select="com:SellerParty/com:OrderContact/com:Fax"/><br/>
                                                Email.: <xsl:value-of select="com:SellerParty/com:OrderContact/com:E-Mail"/><br/></font>
	    			        </td>
				</tr>

				<tr>
					<td width="100%" valign="top" colspan="4" bgcolor="#FFFFFF" height="1"><hr color="#A1B4C4" size="1" NOSHADE="true"/></td>
				</tr>
				<tr>
					<td width="26%" valign="top" bgcolor="#FFFFFF">
						<font face="Arial" size="1"><b>Fakturanr: </b>
						<!-- indsætter Fakturanummer -->
						<xsl:value-of select="com:ID"/></font>
					</td>
					<td width="26%" valign="top" bgcolor="#FFFFFF">
						<font face="Arial" size="1"><b>Købers ordrenr: </b>
						<!-- indsætter Ordrenr  -->
						<xsl:value-of select="com:ReferencedOrder/com:BuyersOrderID"/></font>
                                        </td>
					<td width="23%" valign="top" bgcolor="#FFFFFF">
						<font face="Arial" size="1"><b>Sælgers ordrenr: </b>
						<!-- indsætter Ordrenr  -->
						<xsl:value-of select="com:ReferencedOrder/com:SellersOrderID"/></font>
                                        </td>
					<td width="27%" valign="top" bgcolor="#FFFFFF">
                                                <font face="Arial" size="1"><b>Dato: </b>
						<!-- indsætter faktura dato -->
						<xsl:value-of select="com:IssueDate"/></font>
					</td>
				</tr>
				<tr>
					<td width="100%" valign="top" colspan="4" bgcolor="#FFFFFF" height="1"><hr color="#A1B4C4" size="1" NOSHADE="true"/></td>
				</tr>
			</table>
			<br/>
</xsl:template>
			



</xsl:stylesheet>
