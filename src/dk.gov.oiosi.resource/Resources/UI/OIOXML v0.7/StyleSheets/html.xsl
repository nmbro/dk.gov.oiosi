<?xml version="1.0" encoding="UTF-8"?>

<!--  ...................................................  -->
<!-- fc07.xml                                              -->
<!-- 18.01.05 (FC) Version 0.7                             -->
<!-- (c)2005 EDImatic A/S                                  -->
<!--                                                       -->
<!-- Html konvertering af OIO faktura                      -->
<!--                 (PIE,PCM,PIP,PCP samt testvarianter)  -->
<!--                                                       -->
<!-- Noter:                                                -->
<!--   1. Visning af TIFF                                  -->
<!--   2. Baggrundsfarver?                                 -->
<!--   3. De rigtige betalingsoplysninger skal fiskes      -->
<!--   5. Liniespecificeret                                -->
<!--   6. Linieskift ved fritekst skal bevares             -->
<!--  ...................................................  -->
<!-- Bryan Rasmussen changed structure of stylesheet, seperated out into templates, use css for styling, added meta tags to output -->

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

<xsl:output method="xml" indent="yes"/>
  <xsl:variable name="fakturatype">
          <xsl:choose>
            <xsl:when test="contains(/pie:Invoice/com:TypeCode, 'PIE')">FAKTURA</xsl:when>
            <xsl:when test="contains(/pip:Invoice/com:TypeCode, 'PIP')">FAKTURA</xsl:when>
            <xsl:when test="contains(/pcm:Invoice/com:TypeCode, 'PCM')">KREDITNOTA</xsl:when>
            <xsl:when test="contains(/pcp:Invoice/com:TypeCode, 'PCP')">KREDITNOTA</xsl:when>
            <xsl:otherwise>Ukendt dokumenttype</xsl:otherwise>
          </xsl:choose>
        </xsl:variable>
<xsl:template match="/">
        <xsl:apply-templates select="/*[local-name()='Invoice']"/>
</xsl:template>

<xsl:template match="udk:Invoice| pip:Invoice | pie:Invoice | pcm:Invoice |tpcm:Invoice |tpcp:Invoice|tpie:Invoice|tpip:Invoice | pcp:Invoice ">

       


	<!-- opretter HTML med max 4 tabeller -->
	<html>
		<head>
			<title>OIOXML fakturaudskrivning version 0.7 (<xsl:value-of select="com:TypeCode"/>)</title>					<meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
<link rel="Stylesheet" type="text/css" href="../ubl.css">  </link><link rel="Stylesheet" type="text/css" href="../finance.css">  </link>	

		</head>
		<body>
			<!-- Start på fakturahovedet -->
<xsl:call-template name="fakturatop"/>
			
				

			
<!--put div around invoicetable betalingsoplysninger, get away from using hr tag-->
<div class="invoicemain">
			
<xsl:call-template name="invoicetable"/>
<xsl:call-template name="betalingsoplysninger"/>
</div>

			<br/>
			<!-- Slut på betalingsoplysninger -->

			<!-- Start på fritekst -->
                	<xsl:if test="com:Note[.!='']">
			<table border="0" width="100%" cellspacing="0" cellpadding="2">
				<tr>
					<td valign="top"></td>
					<td valign="top"></td>
					<td valign="top"></td>
					<td valign="top"></td>
				</tr>
				<tr>
					<td valign="top" colspan="4" >
						<!-- her indsættes fritekst -->
                                		<b>Yderligere oplysninger:</b><br/>
                                                <xsl:value-of select="com:Note"/><br/>
					</td>
				</tr>
			</table>
	                </xsl:if>
			<!-- Slut på fritekst -->

		</body>
	</html>
</xsl:template>


<xsl:template match="com:InvoiceLine">
	<!-- indsætter 3 koloner for hver fakturalinie,en med materiale og pris oplysninger, en med supl. oplysninger og en med luft -->
	<tr>
		<td width="17%" valign="top">
			
				<!-- indsætter EAN varenr -->
				<xsl:value-of select="com:Item/com:ID"/>
			
		</td>
		<td width="50%" valign="top">
			
			<!-- indsætter varebeskrivelse -->
			<xsl:value-of select="com:Item/com:Description"/>
			
		</td>
		<td width="10%" valign="top">
			
			<!-- indsætter antal -->
			<xsl:variable name="antal" select="com:InvoicedQuantity"/>
			<xsl:value-of select="format-number($antal, '##0.00')"/>
                      
		</td>
		<td width="10%" valign="top">
                       
			<!-- indsætter enhed -->
			<xsl:value-of select="com:InvoicedQuantity/@unitCode"/>
                      
		</td>
		<td width="5%" valign="top">
                       
			<!-- indsætter enhedspris -->
			<xsl:variable name="enhedspris" select="com:BasePrice/com:PriceAmount"/>
			<xsl:value-of select="format-number($enhedspris, '##0.00')"/>
                       
		</td>
		<td width="3%" valign="top">
                        
			<!-- indsætter tom felt -->
                     
		</td>
		<td width="13%"  align="right">
                        
			<!-- indsætter linietotal -->
			<xsl:variable name="linietotal" select="com:LineExtensionAmount"/>
			<xsl:value-of select="format-number($linietotal, '##0.00')"/>
                     
		</td>
	</tr>
	<tr>
		<td ><font face="Arial" size="2"></font></td>
		<td colspan="1" ><span class="varenr">
			<!-- indsætter flere ordreoplysniger -->
                	<xsl:if test="com:Item/com:SellersItemIdentification/ID[.!='']">
		              <b>Leverandørens varenr.: </b> <xsl:value-of select="com:Item/com:SellersItemIdentification/ID"/>
	                </xsl:if>
                	<xsl:if test="com:Item/com:CommodityClassification/com:CommodityCode[.!='']">
		              <b>UNSPSC code.: </b> <xsl:value-of select="com:Item/com:CommodityClassification/com:CommodityCode"/>
	                </xsl:if>
                	<xsl:if test="com:Note[.!='']">
		              <b>Note.: </b> <xsl:value-of select="com:Note"/>
	                </xsl:if></span>
                </td>
		<td > </td>
		<td > </td>
		<td > </td>
	</tr>
	<tr>
		<td colspan="7"  height="8"> 
		</td>
	</tr>
</xsl:template>

<xsl:template match="com:AllowanceCharge[parent::*[local-name()='Invoice']]">
				<tr>
					<td  colspan="6"> <span class="important" id="Invoice_AllowanceCharge_ID"><b><xsl:value-of select="com:ID"/></b></span></td>
					<td  align="right">
                                                <span class="important" id="Invoice_AllowanceCharge_AllowanceChargeAmount">
                         	
			                  <xsl:value-of select="format-number(com:AllowanceChargeAmount, '##0.00')"/>
                                                </span>
					</td>
				</tr>
                                
</xsl:template>


<xsl:template match="com:SellerParty">
<div class="sellerparty">
<b class="partyinfo">Leverandør</b>
  <xsl:call-template name="partycontent"/>
   CVR.: <xsl:value-of select="com:PartyTaxScheme/com:CompanyTaxID"/>
	<br/>
                             <b>Kontaktoplysninger</b>
                                                <xsl:value-of select="com:OrderContact/com:Name"/><br/>
                                                Tlf.: <xsl:value-of select="com:OrderContact/com:Phone"/><br/>
                                                Email.: <xsl:value-of select="com:OrderContact/com:E-Mail"/><br/>

</div>
</xsl:template>
<xsl:template match="com:BuyerParty">

                                              
                                                <xsl:choose>
                                                <xsl:when test = "com:Address/com:ID = 'Juridisk'">
<div class="buyerparty" id="juridiskbuyerparty" >
<b class="partyinfo">Juridisk</b><br/>
<xsl:call-template name="partycontent"/>


</div>
                                                </xsl:when>
<xsl:otherwise><div class="buyerparty" id="faktureringbuyerparty">
<b>Fakturering</b><br/>
<xsl:call-template name="partycontent"/></div>
</xsl:otherwise>
</xsl:choose>
                                               
</xsl:template>

<xsl:template name="partycontent">
<xsl:apply-templates select="com:PartyName"/>
<xsl:apply-templates select="com:Address/com:Street | com:Address/com:HouseNumber | com:Address/com:CityName | com:Address/com:PostalZone"/>



</xsl:template>

<xsl:template match="com:Address/com:HouseNumber|  com:Address/com:CityName | com:PartyName"><xsl:value-of select="."/><br/></xsl:template>

<xsl:template match="com:Address/com:Street | com:Address/com:PostalZone">
<xsl:value-of select="."/> <xsl:text> </xsl:text>
</xsl:template>

<xsl:template name="fakturatop">
<div class="fakturahovedet">
<span class="logo"><img src="../logo.gif" ALIGN="RIGHT"></img></span><span><h5><xsl:value-of select="$fakturatype"/></h5></span>
<div class="invoiceinfo">
<b>Køber</b><br/>
                                                EAN: <xsl:value-of select="com:BuyersReferenceID"/><br/>
                                                Ordrekontakt.: <xsl:value-of select="com:BuyerParty/com:BuyerContact/com:Name"/><br/>
<span> <b>Fakturanr: </b> <xsl:value-of select="com:ID"/></span>
<span><b>Købers ordrenr: </b><xsl:value-of select="com:ReferencedOrder/com:BuyersOrderID"/></span>
<span><b>Sælgers ordrenr: </b><xsl:value-of select="com:ReferencedOrder/com:SellersOrderID"/></span>
<span><b>Dato: </b><xsl:value-of select="com:IssueDate"/></span>
<span><b>DimensionsKonto:</b> <xsl:value-of select="com:BuyerParty/com:AccountCode"/></span>

</div>
<xsl:apply-templates select="com:BuyerParty"/>
<xsl:apply-templates select="com:SellerParty"/>
</div>
</xsl:template>
<xsl:template name="invoicetable">

	<table border="0" width="100%" cellspacing="0" cellpadding="2">
				<tr>
					<td >Varenr</td>
					<td >Beskrivelse</td>
					<td >Antal</td>
					<td >Enhed</td>
					<td >Enhedspris</td>
					<td > &#160;</td>
					<td  align="right">
						Pris<br/>
					</td>
				</tr>
				
				<!-- indsætter Ordreliner -->
				<xsl:apply-templates select="com:InvoiceLine"/>
				
			<tr>
					<td width="100%"  colspan="7"  height="1"><hr class="seperator" id="ilinesep"/></td>
				</tr>

				<tr>
					<td  colspan="6">Pris i alt excl moms</td>
					<td   align="right">
            
              <xsl:value-of select="format-number(com:LegalTotals/com:LineExtensionTotalAmount, '##0.00')"/>
                                                
					</td>
				</tr>
                      <xsl:apply-templates select="com:AllowanceCharge"/>
				<tr>
                         	      
					<td   colspan="6"><b>Total momsbeløb (<xsl:value-of select="format-number(com:TaxTotal/com:CategoryTotal/com:RatePercentNumeric, '##0.00')"/>%)</b></td>
					<td   align="right">
                                               
						
			                        <xsl:value-of select="format-number(com:TaxTotal/com:TaxAmounts/com:TaxAmount, '##0.00')"/>
                                               
					</td>
				</tr>
				<tr>
					<td   colspan="6">Total incl moms</td>
					<td   align="right">
                                            
						
                              <xsl:value-of select="format-number(com:LegalTotals/com:ToBePaidTotalAmount, '##0.00')"/>
                                               
					</td>
				</tr>
				<tr>	
                                        <td width="100%"  colspan="7"  height="2"><hr class="seperator"/></td>
				</tr>

			</table>

</xsl:template>

<xsl:template name="betalingsoplysninger">

<xsl:variable name="typecodeid" select="com:PaymentMeans/com:TypeCodeID"/>
			<table border="0" width="100%" cellspacing="0" cellpadding="2">
				<tr>
					<td > </td>
					<td > </td>
					<td > </td>
					<td > </td>
				</tr>
				<tr>
					<td  colspan="4" >
						<!-- her indsættes betalingsoplysninger -->
                                		<b>Betalingsoplysninger</b><br/>
                                                <xsl:choose>
                                                
                                                <xsl:when test = "com:PaymentMeans/com:PaymentChannelCode = 'KONTOOVERFØRSEL'">

                                                Forfaldsdato: <xsl:value-of select="com:PaymentMeans/com:PaymentDueDate"/><br/>
                                                Valutakode: <xsl:value-of select="udk:InvoiceCurrencyCode"/><br/>
                                                Betalingstype: <xsl:value-of select="com:PaymentMeans/com:PaymentChannelCode"/><br/>
                                                Kontotype.: <xsl:value-of select="com:PaymentMeans/com:PayeeFinancialAccount/com:TypeCode"/><br/>
                                                Regnr: <xsl:value-of select="com:PaymentMeans/com:PayeeFinancialAccount/com:FiBranch/com:ID"/><br/>
                                                Kontonr.: <xsl:value-of select="com:PaymentMeans/com:PayeeFinancialAccount/com:ID"/><br/>
                                                Pengeinstitut: <xsl:value-of select="com:PaymentMeans/com:PayeeFinancialAccount/com:FiBranch/com:FinancialInstitution/com:Name"/><br/>

                                                </xsl:when>

                                                                                             <xsl:when test = "com:PaymentMeans/com:PaymentChannelCode = 'INDBETALINGSKORT'">

<xsl:variable name="kreditornr">
<xsl:choose><xsl:when test="$typecodeid='01'"><xsl:value-of select="com:PaymentMeans/com:PayeeFinancialAccount/com:ID"/></xsl:when><xsl:otherwise><xsl:value-of select="com:PaymentMeans/com:JointPaymentID"/></xsl:otherwise></xsl:choose>
</xsl:variable>
                                                Forfaldsdato: <xsl:value-of select="com:PaymentMeans/com:PaymentDueDate"/><br/>
                                                Valutakode: <xsl:value-of select="udk:InvoiceCurrencyCode"/><br/>
                                                Betalingstype: <xsl:value-of select="com:PaymentMeans/com:PaymentChannelCode"/><br/>
                                                Kortart: <xsl:value-of select="com:PaymentMeans/com:TypeCodeID"/><br/>
                                                Betalingsid: <xsl:value-of select="com:PaymentMeans/com:PaymentID"/><br/>
                                                Kreditornr: <xsl:value-of select="$kreditornr"/><br/>

                                                </xsl:when>

                                                <xsl:when test = "com:PaymentMeans/com:PaymentChannelCode = 'DIRECT DEBET'">
                                                Forfaldsdato: <xsl:value-of select="com:PaymentMeans/com:PaymentDueDate"/><br/>
                                                Valutakode: <xsl:value-of select="udk:InvoiceCurrencyCode"/><br/>
                                                Betalingstype: <xsl:value-of select="com:PaymentMeans/com:PaymentChannelCode"/><br/>

                                                </xsl:when>

                                                
                                                <xsl:when test = "com:PaymentMeans/com:PaymentChannelCode = 'NATIONAL CLEARING'">
                                                Forfaldsdato: <xsl:value-of select="com:PaymentMeans/com:PaymentDueDate"/><br/>
                                                Valutakode: <xsl:value-of select="udk:InvoiceCurrencyCode"/><br/>
                                                Betalingstype: <xsl:value-of select="com:PaymentMeans/com:PaymentChannelCode"/><br/>

                                                </xsl:when>

                                                <!-- Ukendt betalingstype -->
                                                <xsl:otherwise>
                                                Forfaldsdato: <xsl:value-of select="com:PaymentMeans/com:PaymentDueDate"/><br/>
                                                Valutakode: <xsl:value-of select="udk:InvoiceCurrencyCode"/><br/>
                                                Betalingstype: <xsl:value-of select="com:PaymentMeans/com:PaymentChannelCode"/><br/>
                                                </xsl:otherwise>
                                                </xsl:choose>

                                                
                                                
					</td>
				</tr>
			</table>
</xsl:template>
</xsl:stylesheet>
