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


<xsl:template name="layoutmaster">
      <layout-master-set>
         
<simple-page-master master-name="invoicepage" 
              page-width="{$pagewidth}" 
              page-height="{$pageheight}" 
              margin-top="1mm"
              margin-bottom="1mm" 
              margin-right="1mm"
              margin-left="1mm">
            <region-body region-name="body" 
                 margin-left="5mm" margin-top="2.5cm" 
                 margin-bottom=".5cm"/>
            <region-before region-name="before" extent="2.5cm"/>
            <region-after region-name="after" extent=".5cm"/>
            <region-start region-name="start" extent="10mm"/>
         </simple-page-master>
<simple-page-master master-name="invoicebackpage" 
              page-width="{$pagewidth}" 
              page-height="{$pageheight}" 
              margin-top="1mm"
              margin-bottom="1mm" 
              margin-right="1mm"
              margin-left="1mm">
            <region-body region-name="body" 
                 margin-left="5mm" margin-top="2.5cm" 
                 margin-bottom=".5cm"/>
            <region-before region-name="before" extent="2.5cm"/>
            <region-after region-name="after" extent=".5cm"/>
            <region-start region-name="start" extent="10mm"/>
         </simple-page-master>
         <page-sequence-master master-name="layout">
            <repeatable-page-master-reference master-reference="invoicepage"/>
	    <repeatable-page-master-reference master-reference="invoicebackpage"/>
         </page-sequence-master>
      </layout-master-set>
</xsl:template>




<xsl:template name="do-line-items">
   <table table-layout="fixed">
   <table-column column-width="30mm"/>
     <table-column column-width="30mm"/>
   <table-column column-width="34mm"/>
   <table-column column-width="20mm"/>
   <table-column column-width="40mm"/>
   <xsl:call-template name="addcellsColumns"></xsl:call-template>
   <table-column column-width="35mm"/>
      <table-header text-align="center" start-indent="2pt" end-indent="2pt">
         <table-row font-size="75%">
	     <table-cell>
               <block>
                  <xsl:text> </xsl:text>
               </block>
            </table-cell>
	  <table-cell border="solid 1pt"  
                        display-align="center">
               <block font-style="italic">
                  <xsl:text>Dato</xsl:text>
               </block>
            </table-cell>
             <table-cell border="solid 1pt"  
                        display-align="center">
               <block font-style="italic">
                  <xsl:text>Tekst</xsl:text>
               </block>
            </table-cell>
            <table-cell border="solid 1pt" display-align="center">
               <block font-style="italic">
                  <xsl:text>Antal</xsl:text>
               </block>
            </table-cell>
          
     
            
            <table-cell border="solid 1pt" display-align="center">
              <block font-style="italic">
                <xsl:text>Pris</xsl:text>
              </block>
            </table-cell>
            <xsl:call-template name="addcellsHeaders"></xsl:call-template>
            <table-cell border="solid 1pt" display-align="center">
              <block>Final Pris</block>
            </table-cell>
         </table-row>


       </table-header>

   
      <table-body text-align="center" start-indent="2pt" end-indent="2pt">
 

        <xsl:apply-templates select="cat:InvoiceLine"/>

        <table-row font-size="75%">
	     <table-cell>
               <block>
                  <xsl:text> </xsl:text>
               </block>
            </table-cell>
	<xsl:call-template name="styledemptycell"><xsl:with-param name="cellcount" select="4"/></xsl:call-template>
             
            <xsl:call-template name="addcellFooters"></xsl:call-template>
            <table-cell border="solid 1pt" display-align="center">
              
  <block><xsl:value-of select="/*[local-name()='Invoice']/cat:LegalTotals/cat:ToBePaidTotalAmount"/></block>
            </table-cell>
         </table-row>
         
      </table-body>
   </table>
</xsl:template>

<xsl:template name="styledemptycell">
<xsl:param name="cellcount" select="1"/>
<xsl:choose><xsl:when test="$cellcount &gt; 1">
  <table-cell border="solid 1pt"  
                        display-align="center">
               <block font-style="italic">
                  <xsl:text> </xsl:text>
               </block>
            </table-cell><xsl:call-template name="styledemptycell"><xsl:with-param name="cellcount" select="$cellcount - 1"/></xsl:call-template></xsl:when>
<xsl:when test="$cellcount = 1">  <table-cell border="solid 1pt"  
                        display-align="center">
               <block font-style="italic">
                  <xsl:text> </xsl:text>
               </block>
            </table-cell></xsl:when>
<xsl:otherwise></xsl:otherwise></xsl:choose>
</xsl:template>

<xsl:template name="emptyPartycell">
<xsl:param name="cellcount" select="1"/>
<xsl:choose><xsl:when test="$cellcount &gt; 1">
   <table-cell height="9em">
 <block-container height="9em"  display-align="center">
<block><xsl:text> </xsl:text></block> 
</block-container>
      </table-cell><xsl:call-template name="emptyPartycell"><xsl:with-param name="cellcount" select="$cellcount - 1"/></xsl:call-template></xsl:when>
<xsl:when test="$cellcount = 1"> <table-cell height="9em">
 <block-container height="9em"  display-align="center">
<block><xsl:text> </xsl:text></block> 
</block-container>
      </table-cell></xsl:when>
<xsl:otherwise></xsl:otherwise></xsl:choose>
</xsl:template>



<xsl:template name="emptycell">
<xsl:param name="cellcount" select="1"/>
<xsl:choose><xsl:when test="$cellcount &gt; 1">
  <table-cell>
               <block>
                  <xsl:text> </xsl:text>
               </block>
            </table-cell><xsl:call-template name="emptycell"><xsl:with-param name="cellcount" select="$cellcount - 1"/></xsl:call-template></xsl:when>
<xsl:when test="$cellcount = 1">  <table-cell>
               <block>
                  <xsl:text> </xsl:text>
               </block>
            </table-cell></xsl:when>
<xsl:otherwise></xsl:otherwise></xsl:choose>
</xsl:template>


<xsl:template name="before">
   <static-content flow-name="before">
      <block font-weight="bold" text-align-last="justify">
         <xsl:text>Faktura</xsl:text>
            <leader/>
           <xsl:text>Side </xsl:text>
           <page-number/>
                  </block>
      <table table-layout="fixed" width="100%"
             space-before.conditionality="retain" space-before="1em">
        <table-column column-width="72%"/>
        <table-column column-width="26%"/>
        <table-body>
          <table-row>
            <table-cell>
              <block/>
            </table-cell>
            <table-cell>
              <xsl:apply-templates select="cat:ID" mode="header-info"/>
              <xsl:apply-templates select="cat:IssueDate" mode="header-info"/>
            </table-cell>
          </table-row>
        </table-body>
      </table>
   </static-content>
</xsl:template>

<xsl:template name="after">
 
</xsl:template>
<xsl:template name="start">
 
</xsl:template>


<xsl:template name="addcellsContent"/>
<xsl:template name="addcellsColumns"/>
<xsl:template name="addcellsHeaders"/>
<xsl:template name="addcellFooters"/>
</xsl:stylesheet>

