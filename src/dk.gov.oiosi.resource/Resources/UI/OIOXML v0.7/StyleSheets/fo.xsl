<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
  xmlns="http://www.w3.org/1999/XSL/Format" 
  xmlns:in="http://rep.oio.dk/ubl/xml/schemas/0p71/maindoc/" 
  xmlns:cat="http://rep.oio.dk/ubl/xml/schemas/0p71/common/"
  xmlns:pie="http://rep.oio.dk/ubl/xml/schemas/0p71/pie/"
  xmlns:tpcm="http://rep.oio.dk/ubl/xml/schemas/0p71/testpcm/"
  xmlns:tpcp="http://rep.oio.dk/ubl/xml/schemas/0p71/testpcp/"
  xmlns:tpie="http://rep.oio.dk/ubl/xml/schemas/0p71/testpie/"
  xmlns:tpip="http://rep.oio.dk/ubl/xml/schemas/0p71/testpip/"
  xmlns:pip="http://rep.oio.dk/ubl/xml/schemas/0p71/pip/"
  xmlns:pcm="http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/"
  xmlns:pcp="http://rep.oio.dk/ubl/xml/schemas/0p71/testpcp/"
>



<xsl:import href="fonamedtemplates.xsl"/>
<xsl:param name="pagewidth" select="'8in'"/>
<xsl:param name="pageheight" select="'11in'"/>
<xsl:variable name="issueDate" select="/*/cat:IssueDate"/>



<xsl:template match="/">
   <root font-family="Times" font-size="9pt" line-height="1.2">
	<xsl:call-template name="layoutmaster"/>
      	<xsl:apply-templates/>
   </root>
</xsl:template>


<xsl:template match="in:Invoice | pip:Invoice | pie:Invoice | pcm:Invoice |tpcm:Invoice |tpcp:Invoice|tpie:Invoice|tpip:Invoice">
   <page-sequence master-reference="layout">
      <title><xsl:value-of select="concat(cat:ID,'-',cat:IssueDate,' - Efaktura')"/></title>
      <xsl:call-template name="start"/>
      <xsl:call-template name="before"/>
      <xsl:call-template name="after"/>
      <flow flow-name="body">
     
         <table table-layout="fixed" width="50%">
            <table-column column-width="29%"/>
            <table-column column-width="21%"/>
            <table-body>
               <table-row height="6em">
                      <table-cell height="6em">
                     <block-container height="6em" text-align="start" display-align="before">
                        <xsl:apply-templates select="cat:BuyerParty[cat:Address/cat:ID='Faktura']"/>
                     </block-container>
                  </table-cell>
                  <table-cell height="6em">
                    <block-container height="6em" display-align="before">
                                         <xsl:apply-templates
                               select="cat:ReferencedOrder/cat:BuyersOrderID"/>
                          </block-container>
                  </table-cell>
               </table-row>
            </table-body>
         </table>
	<xsl:apply-templates select="cat:DestinationParty"/>
         <block space-before="5em">  
         <xsl:apply-templates select="(cat:InvoiceLine/cat:DeliveryRequirement/cat:DeliverySchedule/cat:RequestedDeliveryDate)[1]"/>
</block>
         <!--do all the line items in the table-->
         <xsl:call-template name="do-line-items"/>

        <block-container height="10em">
                  <block space-before="40em"> <xsl:apply-templates select="cat:SellerParty"/></block>
                     </block-container>

      </flow>
   </page-sequence>
</xsl:template>


<xsl:template match="cat:DestinationParty">
 <block-container height="2em" display-align="after">
                        <block>
                           <xsl:text>Kontact:</xsl:text>
                        </block>
			<block>
			 <xsl:apply-templates/>
			</block>
                     </block-container>
</xsl:template>





<xsl:template match="cat:InvoicedQuantity" >
<xsl:apply-templates select="@unitCode"><xsl:with-param name="value" select="."/></xsl:apply-templates> 
</xsl:template>
<xsl:template match="@unitCode">
<xsl:param name="value"/>
 <xsl:value-of select="$value"/> <xsl:value-of select="."/>
</xsl:template>
<xsl:template match="@unitCode[.='promille']">
<xsl:param name="value"/>
 <xsl:value-of select="$value div 1000"/>
</xsl:template>
<xsl:template match="@unitCode[.='centimeters']">
<xsl:param name="value"/>
 <xsl:value-of select="$value"/>cm
</xsl:template>
<xsl:template match="@unitCode[.='millimeters']">
<xsl:param name="value"/>
 <xsl:value-of select="$value"/>mm
</xsl:template>
<xsl:template match="@unitCode[.='meters']">
<xsl:param name="value"/>
 <xsl:value-of select="$value"/>m
</xsl:template>





<xsl:template match="cat:ReferencedDespatchAdvice/cat:ID">
   <block space-before="2em">
      <xsl:text>Delivery Doc: </xsl:text>
      <xsl:apply-templates/>
   </block>
</xsl:template>

<xsl:template match="cat:BuyersOrderID">
   <block space-before="2em">
      <xsl:text>Order No: </xsl:text>

<block><xsl:apply-templates/><xsl:value-of select="//cat:BuyersOrderID"/></block>
   </block>
</xsl:template>

<xsl:template match="cat:ReferencedOrder/cat:IssueDate">
   <block>
      <xsl:text>Issue Dato: </xsl:text>
      <xsl:apply-templates/>
   </block>
</xsl:template>

<xsl:template match="cat:RequestedDeliveryDate">
   <block space-before="1em" space-after="1em">
      <xsl:text>
        Required Delivery on </xsl:text>
        <xsl:apply-templates/>
        Confirmed
   </block>
</xsl:template>

<xsl:template match="cat:AllowanceCharge[cat:MultiplierReasonCode='trade'][parent::*[local-name()='Invoice']]">
  <block space-before="1em">
   Trade Discount: 
    <xsl:value-of select="cat:MultiplierFactorQuantity * 100"/>
    <xsl:text>% discount will be applied to this order.</xsl:text>
  </block>
</xsl:template>

<xsl:template match="cat:InvoiceLine" mode="do-delivery">
  <block keep-with-previous="always" space-before="1em">
    <xsl:for-each select="cat:Item/cat:BasePrice/cat:PriceAmount">
      <xsl:text>A surcharge of </xsl:text>
      <xsl:value-of select="concat( @currencyID, '&#xa0;', . )"/>
      <xsl:text> will be made for delivery.</xsl:text>
    </xsl:for-each>
  </block>
</xsl:template>


<xsl:template match="cat:InvoiceLine">
 <table-row>   <xsl:call-template name="emptycell"/>
 <table-cell border="solid 1pt" display-align="center">
               <block>
                   <xsl:value-of select="$issueDate"/> 
               </block>
            </table-cell>
    <xsl:apply-templates select="cat:Item/cat:Description"/>
    <xsl:apply-templates select="cat:InvoicedQuantity"/>
    <xsl:apply-templates select="cat:LineExtensionAmount"/>
    <xsl:call-template name="addcellsContent"></xsl:call-template>
    <table-cell border="solid 1pt" display-align="center">
      
    </table-cell>
   </table-row>
   
</xsl:template>

<xsl:template match="cat:BasePrice[1]/cat:PriceAmount | cat:LineExtensionAmount">
  <block text-align="end" text-align-last="justify">
    <xsl:value-of select="@currencyID"/>
    <leader/>
    <xsl:value-of select="format-number( ., '0,00' )"/>
  </block>
</xsl:template>
<xsl:template match="cat:LineExtensionAmount"/>




<xsl:template match="cat:BuyerParty" name="name-address">
   <block border-start-color="#ff0000">
      <xsl:apply-templates select="cat:PartyName[1]/cat:Name"/>
      <xsl:apply-templates select="cat:Address[1]"/>
   </block>
</xsl:template>

<xsl:template match="cat:SellerParty">
      <table table-layout="fixed">
            <table-column column-width="30mm"/>
	<table-column column-width="10mm"/>
            	<table-column column-width="30mm"/>
<table-column column-width="10mm"/>
<table-column column-width="30mm"/>
<table-column column-width="10mm"/>
<table-column column-width="30mm"/>
            <table-body>
               <table-row height="9em">
                               <table-cell height="9em">
 <block-container height="9em"  display-align="center">
<block ><xsl:value-of select="cat:PartyName"/></block> 
<block><xsl:value-of select="cat:ID"/></block>
</block-container>
      </table-cell>
<xsl:call-template name="emptyPartycell"/>
        <table-cell height="9em">
<block-container height="9em" display-align="center">

<xsl:apply-templates select="cat:Address"/> </block-container>
      </table-cell>
<xsl:call-template name="emptyPartycell"/>
 <table-cell height="9em">
 <block-container height="9em" display-align="center">
<block>tlf:<xsl:value-of select="cat:OrderContact/cat:Phone"/></block>
<block>fax:<xsl:value-of select="cat:OrderContact/cat:Fax"/></block>
 </block-container>

      </table-cell>
 <table-cell height="9em">
 <block-container height="9em" display-align="center">
<block>email:<xsl:value-of select="cat:OrderContact/cat:E-Mail"/></block>

 </block-container>
      </table-cell>
</table-row>
</table-body>
</table>
</xsl:template>

<xsl:template match="cat:DeliverToAddress">

      <xsl:apply-templates select="cat:Address"/>

</xsl:template>

<xsl:template match="cat:Address" name="address">
  <block>
    <xsl:apply-templates select="cat:Street"/>
    <xsl:apply-templates select="cat:AdditionalStreet"/><xsl:apply-templates select="cat:HouseNumber"/>
    <block>
      <xsl:apply-templates select="cat:CityName"/>
       </block>
    <xsl:apply-templates select="cat:Country/cat:Code"/>
    <xsl:apply-templates select="cat:PostalZone"/>
  </block>
</xsl:template>

<xsl:template match="cat:BuyerParty/cat:BuyerContact/cat:Name | cat:Country/cat:Code | cat:PartyName/cat:Name | cat:PostalZone | cat:Street | cat:HouseName">
   <block>
      <xsl:apply-templates/>
   </block>
</xsl:template>

<xsl:template match="cat:CityName | cat:AdditionalStreet |cat:HouseNumber">
   <inline>
      <xsl:apply-templates/>
   </inline>
</xsl:template>





<xsl:template match="cat:ID" mode="header-info">
   <block>
      <xsl:text>Invoice No: </xsl:text>
      <xsl:apply-templates/>
   </block>
</xsl:template>

<xsl:template match="cat:IssueDate" mode="header-info">
   <block>
     <xsl:text>Invoice Dato: </xsl:text>
      <xsl:apply-templates/>
   </block>
</xsl:template>


<xsl:template match="cat:ReferencedOrderLine/cat:BuyersID">
<xsl:variable name="rows"><xsl:choose><xsl:when test="string-length(.) &lt; 5">2</xsl:when><xsl:when test="string-length(.) &gt; 10">4</xsl:when> </xsl:choose></xsl:variable>
    <table-cell border="solid 1pt" number-rows-spanned="{$rows}">
        <block>
         <xsl:value-of select="."/>
        </block>
      </table-cell>
</xsl:template>
<xsl:template match="cat:Item/cat:Description | cat:LineExtensionAmount | cat:Item[1]/cat:Tax[1]/cat:RateCategoryCodeID[1] | cat:InvoicedQuantity"> 
<table-cell border="solid 1pt" number-rows-spanned="1"><block>
<xsl:value-of select="."/></block></table-cell>
</xsl:template>

</xsl:stylesheet>

