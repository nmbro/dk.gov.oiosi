<?xml version="1.0" encoding="UTF-16" standalone="yes"?>
<xsl:stylesheet version="1.0" doc:dummy-for-xmlns="" cac:dummy-for-xmlns="" cbc:dummy-for-xmlns="" ccts:dummy-for-xmlns="" sdt:dummy-for-xmlns="" udt:dummy-for-xmlns="" xs:dummy-for-xmlns="" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:sch="http://www.ascc.net/xml/schematron" xmlns:doc="urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2" xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2" xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2" xmlns:ccts="urn:oasis:names:specification:ubl:schema:xsd:CoreComponentParameters-2" xmlns:sdt="urn:oasis:names:specification:ubl:schema:xsd:SpecializedDatatypes-2" xmlns:udt="urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2" xmlns:xs="http://www.w3.org/2001/XMLSchema">
<xsl:output method="xml" encoding="utf-8" />
<xsl:template match="*|@*" mode="schematron-get-full-path">
<xsl:apply-templates select="parent::*" mode="schematron-get-full-path" />
<xsl:text>/</xsl:text>
<xsl:if test="count(. | ../@*) = count(../@*)">@</xsl:if>
<xsl:value-of select="name()" />
<xsl:text>[</xsl:text>
<xsl:value-of select="1+count(preceding-sibling::*[name()=name(current())])" />
<xsl:text>]</xsl:text>
</xsl:template>
<xsl:template match="/" mode="generate-id-from-path" />
<xsl:template match="text()" mode="generate-id-from-path">
<xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
<xsl:value-of select="concat('.text-', 1+count(preceding-sibling::text()), '-')" />
</xsl:template>
<xsl:template match="comment()" mode="generate-id-from-path">
<xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
<xsl:value-of select="concat('.comment-', 1+count(preceding-sibling::comment()), '-')" />
</xsl:template>
<xsl:template match="processing-instruction()" mode="generate-id-from-path">
<xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
<xsl:value-of select="concat('.processing-instruction-', 1+count(preceding-sibling::processing-instruction()), '-')" />
</xsl:template>
<xsl:template match="@*" mode="generate-id-from-path">
<xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
<xsl:value-of select="concat('.@', name())" />
</xsl:template>
<xsl:template match="*" mode="generate-id-from-path" priority="-0.5">
<xsl:apply-templates select="parent::*" mode="generate-id-from-path" />
<xsl:text>.</xsl:text>
<xsl:choose>
<xsl:when test="count(. | ../namespace::*) = count(../namespace::*)">
<xsl:value-of select="concat('.namespace::-',1+count(namespace::*),'-')" />
</xsl:when>
<xsl:otherwise>
<xsl:value-of select="concat('.',name(),'-',1+count(preceding-sibling::*[name()=name(current())]),'-')" />
</xsl:otherwise>
</xsl:choose>
</xsl:template>
<xsl:template match="/">
<Schematron>
<Information>Checking OIOUBL-2.01 CreditNote, FC 2007-08-16, Version 0.3</Information>
<xsl:apply-templates select="/" mode="M11" /><xsl:apply-templates select="/" mode="M12" /><xsl:apply-templates select="/" mode="M13" /><xsl:apply-templates select="/" mode="M14" /><xsl:apply-templates select="/" mode="M15" /><xsl:apply-templates select="/" mode="M16" /><xsl:apply-templates select="/" mode="M17" /><xsl:apply-templates select="/" mode="M18" /><xsl:apply-templates select="/" mode="M19" /><xsl:apply-templates select="/" mode="M20" /><xsl:apply-templates select="/" mode="M21" /><xsl:apply-templates select="/" mode="M22" /><xsl:apply-templates select="/" mode="M23" /><xsl:apply-templates select="/" mode="M24" /><xsl:apply-templates select="/" mode="M26" /><xsl:apply-templates select="/" mode="M27" /><xsl:apply-templates select="/" mode="M28" /><xsl:apply-templates select="/" mode="M29" /><xsl:apply-templates select="/" mode="M30" /><xsl:apply-templates select="/" mode="M31" /><xsl:apply-templates select="/" mode="M32" />
</Schematron>
</xsl:template>
<xsl:template match="/" priority="3999" mode="M11">
<xsl:choose>
<xsl:when test="local-name(*) = 'CreditNote'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(*) = 'CreditNote'</Pattern>
<Description>[F-CRN001] Root element must be CreditNote</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M11" />
</xsl:template>
<xsl:template match="doc:CreditNote" priority="3998" mode="M11">
<xsl:choose>
<xsl:when test="cbc:UBLVersionID = '2.0'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UBLVersionID = '2.0'</Pattern>
<Description>[F-LIB001] Invalid UBLVersionID. Must be '2.0'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CustomizationID = 'OIOUBL-2.01'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CustomizationID = 'OIOUBL-2.01'</Pattern>
<Description>[F-LIB002] Invalid CustomizationID. Must be 'OIOUBL-2.01'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ProfileID/@schemeID = 'urn:oioubl:id:profileid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ProfileID/@schemeID = 'urn:oioubl:id:profileid-1.1'</Pattern>
<Description>[W-LIB003] Invalid schemeID. Must be 'urn:oioubl:id:profileid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ProfileID/@schemeAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ProfileID/@schemeAgencyID = '320'</Pattern>
<Description>[W-LIB203] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="contains(cbc:ProfileID, 'Procurement') or contains(cbc:ProfileID, 'Catalogue') or contains(cbc:ProfileID, 'nesubl.eu')" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>contains(cbc:ProfileID, 'Procurement') or contains(cbc:ProfileID, 'Catalogue') or contains(cbc:ProfileID, 'nesubl.eu')</Pattern>
<Description>[F-LIB004] Invalid ProfileID. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ProfileID = 'Procurement-OrdSimR-BilSim-1.0' or cbc:ProfileID = 'Procurement-OrdAdv-BilSim-1.0' or cbc:ProfileID = 'urn:www.nesubl.eu:profiles:profile7:ver1.0') and not(cac:OrderReference)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ProfileID = 'Procurement-OrdSimR-BilSim-1.0' or cbc:ProfileID = 'Procurement-OrdAdv-BilSim-1.0' or cbc:ProfileID = 'urn:www.nesubl.eu:profiles:profile7:ver1.0') and not(cac:OrderReference)</Pattern>
<Description>[F-CRN002] There must be an OrderReference class for this profileID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M11" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M11" />
<xsl:template match="doc:CreditNote" priority="3999" mode="M12">
<xsl:choose>
<xsl:when test="count(cac:TaxRepresentativeParty) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxRepresentativeParty) = 0</Pattern>
<Description>[F-CRN165] TaxRepresentativeParty class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:DocumentCurrencyCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentCurrencyCode != ''</Pattern>
<Description>[F-CRN004] Invalid DocumentCurrencyCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:TaxTotal) != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxTotal) != 0</Pattern>
<Description>[F-CRN005] One or more TaxTotal class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN006] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AccountingCost and cbc:AccountingCostCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AccountingCost and cbc:AccountingCostCode</Pattern>
<Description>[F-LIB021] Use either AccountingCost or AccountingCostCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:InvoicePeriod) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:InvoicePeriod) &gt; 1</Pattern>
<Description>[F-CRN159] No more than one InvoicePeriod class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:CreditNote/cbc:UUID" priority="3998" mode="M12">
<xsl:choose>
<xsl:when test="string-length(string(.)) = 36" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(string(.)) = 36</Pattern>
<Description>[F-LIB006] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:CreditNote/cbc:Note" priority="3997" mode="M12">
<xsl:if test="count(../cbc:Note) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Note) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB011] The attribute languageID should be used when more than one<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB012] Multilanguage error. Replicated<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:CreditNote/cbc:DocumentCurrencyCode" priority="3996" mode="M12">
<xsl:if test="/*/cac:CreditNoteLine/cbc:LineExtensionAmount[@currencyID][@currencyID!=string(current())]">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>/*/cac:CreditNoteLine/cbc:LineExtensionAmount[@currencyID][@currencyID!=string(current())]</Pattern>
<Description>[F-CRN007] There is a LineExtensionAmount for one or more CreditNoteLines where the currencyID does not equal the DocumentCurrencyCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="/*/cac:LegalMonetaryTotal/cbc:LineExtensionAmount[@currencyID][@currencyID!=string(current())]">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>/*/cac:LegalMonetaryTotal/cbc:LineExtensionAmount[@currencyID][@currencyID!=string(current())]</Pattern>
<Description>[F-CRN008] There is a LineExtensionAmount where the currencyID does not equal the DocumentCurrencyCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="/*/cac:LegalMonetaryTotal/cbc:PayableAmount[@currencyID][@currencyID!=string(current())]">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>/*/cac:LegalMonetaryTotal/cbc:PayableAmount[@currencyID][@currencyID!=string(current())]</Pattern>
<Description>[F-CRN009] There is a PayableAmount where the currencyID does not equal the DocumentCurrencyCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:CreditNote/cbc:TaxCurrencyCode" priority="3995" mode="M12">
<xsl:if test="//cbc:TaxAmount[@currencyID][@currencyID!=string(current())]">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>//cbc:TaxAmount[@currencyID][@currencyID!=string(current())]</Pattern>
<Description>[F-CRN010] There is a TaxAmount where the currencyID does not equal the TaxCurrencyCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test=".='DKK' or . ='EUR'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>.='DKK' or . ='EUR'</Pattern>
<Description>[F-CRN011] TaxCurrencyCode must be either DKK or EUR</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(/*/cac:TaxExchangeRate) != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(/*/cac:TaxExchangeRate) != 0</Pattern>
<Description>[F-CRN012] One TaxExchangeRate class must be present when TaxCurrencyCode element is used</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(/*/cac:TaxTotal/cac:TaxSubtotal/cbc:TransactionCurrencyTaxAmount) != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(/*/cac:TaxTotal/cac:TaxSubtotal/cbc:TransactionCurrencyTaxAmount) != 0</Pattern>
<Description>[F-CRN013] One TransactionCurrencyTaxAmount element must be present when TaxCurrencyCode element is used</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:CreditNote/cbc:PricingCurrencyCode" priority="3994" mode="M12">
<xsl:if test="/*/cac:CreditNoteLine/cac:Price/cbc:PriceAmount[@currencyID][@currencyID!=string(current())]">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>/*/cac:CreditNoteLine/cac:Price/cbc:PriceAmount[@currencyID][@currencyID!=string(current())]</Pattern>
<Description>[F-CRN014] There is a PriceAmount where the currencyID does not equal the PricingCurrencyCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="count(/*/cac:PricingExchangeRate) != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(/*/cac:PricingExchangeRate) != 0</Pattern>
<Description>[F-CRN015] One PricingExchangeRate class must be present when PricingCurrencyCode element is used</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:CreditNote/cbc:PaymentCurrencyCode" priority="3993" mode="M12">
<xsl:choose>
<xsl:when test="count(/*/cac:PaymentExchangeRate) != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(/*/cac:PaymentExchangeRate) != 0</Pattern>
<Description>[F-CRN016] One PaymentExchangeRate class must be present when PaymentCurrencyCode element is used</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:CreditNote/cbc:PaymentAlternativeCurrencyCode" priority="3992" mode="M12">
<xsl:choose>
<xsl:when test="count(/*/cac:PaymentAlternativeExchangeRate) != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(/*/cac:PaymentAlternativeExchangeRate) != 0</Pattern>
<Description>[F-CRN017] One PaymentAlternativeExchangeRate class must be present when PaymentAlternativeCurrencyCode element is used</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M12" />
<xsl:template match="doc:CreditNote/cac:InvoicePeriod" priority="3999" mode="M13">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:InvoicePeriod/cbc:Description" priority="3998" mode="M13">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M13" />
<xsl:template match="doc:CreditNote/cac:DiscrepancyResponse" priority="3999" mode="M14">
<xsl:choose>
<xsl:when test="cbc:ReferenceID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ReferenceID != ''</Pattern>
<Description>[F-CRN161] Invalid ReferenceID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:ResponseCode and cbc:Description">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ResponseCode and cbc:Description</Pattern>
<Description>[F-CRN161] Use either ResponseCode or Description</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:DiscrepancyResponse/cbc:ResponseCode" priority="3998" mode="M14">
<xsl:choose>
<xsl:when test="./@listID = 'urn:oioubl:codelist:responsecode-1.0'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@listID = 'urn:oioubl:codelist:responsecode-1.0'</Pattern>
<Description>[F-CRN018] Invalid listID. Must be 'urn:oioubl:codelist:responsecode-1.0'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="./@listAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@listAgencyID = '320'</Pattern>
<Description>[F-CRN160] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:DiscrepancyResponse/cbc:Description" priority="3997" mode="M14">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-CRN019] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-CRN020] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M14" />
<xsl:template match="doc:CreditNote/cac:OrderReference" priority="3999" mode="M15">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN022] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:OrderReference/cbc:UUID" priority="3998" mode="M15">
<xsl:choose>
<xsl:when test="string-length(string(.)) = 36" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(string(.)) = 36</Pattern>
<Description>[F-LIB006] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:OrderReference/cac:DocumentReference" priority="3997" mode="M15">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M15" />
<xsl:template match="doc:CreditNote/cac:BillingReference" priority="3999" mode="M16">
<xsl:choose>
<xsl:when test="count(cac:DebitNoteDocumentReference) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:DebitNoteDocumentReference) = 0</Pattern>
<Description>[F-CRN023] DebitNoteDocumentReference class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:AdditionalDocumentReference) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AdditionalDocumentReference) = 0</Pattern>
<Description>[F-CRN024] AdditionalDocumentReference class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:BillingReferenceLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:BillingReferenceLine) = 0</Pattern>
<Description>[F-CRN162] BillingReferenceLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:BillingReference/cac:InvoiceDocumentReference" priority="3998" mode="M16">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:BillingReference/cac:SelfBilledInvoiceDocumentReference" priority="3997" mode="M16">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:BillingReference/cac:CreditNoteDocumentReference" priority="3996" mode="M16">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:BillingReference/cac:SelfBilledCreditNoteDocumentReference" priority="3995" mode="M16">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:BillingReference/cac:ReminderDocumentReference" priority="3994" mode="M16">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M16" />
<xsl:template match="doc:CreditNote/cac:DespatchDocumentReference" priority="3999" mode="M17">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M17" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M17" />
<xsl:template match="doc:CreditNote/cac:ReceiptDocumentReference" priority="3999" mode="M18">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M18" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M18" />
<xsl:template match="doc:CreditNote/cac:ContractDocumentReference" priority="3999" mode="M19">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M19" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M19" />
<xsl:template match="doc:CreditNote/cac:AdditionalDocumentReference" priority="3999" mode="M20">
<xsl:choose>
<xsl:when test="cbc:DocumentType or cbc:DocumentTypeCode" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentType or cbc:DocumentTypeCode</Pattern>
<Description>[F-LIB092] Use either DocumentType or DocumentTypeCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB093] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:DocumentType and cbc:DocumentTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentType and cbc:DocumentTypeCode</Pattern>
<Description>[F-LIB094] Use either DocumentType or DocumentTypeCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB095] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB097] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB098] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB213] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M20" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M20" />
<xsl:template match="doc:CreditNote/cac:Signature" priority="3999" mode="M21">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN025] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty" priority="3998" mode="M21">
<xsl:choose>
<xsl:when test="count(cbc:MarkCareIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkCareIndicator) = 0</Pattern>
<Description>[F-LIB166] MarkCareIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:MarkAttentionIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkAttentionIndicator) = 0</Pattern>
<Description>[F-LIB167] MarkAttentionIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:AgentParty) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AgentParty) = 0</Pattern>
<Description>[F-LIB168] AgentParty class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')</Pattern>
<Description>[F-LIB022] PartyName/Name is mandatory if PartyIdentification/ID is not found</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')</Pattern>
<Description>[F-LIB179] Invalid schemeID. Must be a valid scheme for EndpointID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB180] schemeID = DK:CVR, EndpointID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB215] schemeID = DK:CPR, EndpointID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB181] schemeID = GLN, EndpointID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB216] schemeID = EAN, EndpointID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:PartyLegalEntity) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PartyLegalEntity) &gt; 1</Pattern>
<Description>[F-CRN163] No more than one PartyLegalEntity class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PartyIdentification" priority="3997" mode="M21">
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN'</Pattern>
<Description>[F-LIB183] Invalid schemeID. Must be a valid scheme for PartyIdentification/ID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB184] schemeID = DK:CVR, ID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB217] schemeID = DK:CPR, ID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB185] schemeID = GLN, ID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB218] schemeID = EAN, ID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PartyName" priority="3996" mode="M21">
<xsl:if test="count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)</Pattern>
<Description>[W-LIB219] The attribute Name@languageID should be used when more than one PartyName class is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID</Pattern>
<Description>[W-LIB220] Multilanguage error. Replicated PartyName classes with same Name@languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PostalAddress" priority="3995" mode="M21">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation" priority="3994" mode="M21">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3993" mode="M21">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3992" mode="M21">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation/cac:Address" priority="3991" mode="M21">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PartyTaxScheme" priority="3990" mode="M21">
<xsl:choose>
<xsl:when test="count(cbc:TaxLevelCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TaxLevelCode) = 0</Pattern>
<Description>[F-LIB192] TaxLevelCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB193] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB195] Invalid schemeID. Must be a valid scheme for PartyTaxScheme/CompanyID (DK:SE)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB196] schemeID = DK:SE, CompanyID must be a valid SE number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3989" mode="M21">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:PartyLegalEntity" priority="3988" mode="M21">
<xsl:choose>
<xsl:when test="count(cac:CorporateRegistrationScheme) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:CorporateRegistrationScheme) = 0</Pattern>
<Description>[F-LIB186] CorporateRegistrationScheme class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB187] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB189] Invalid schemeID. Must be a valid scheme for PartyLegalEntity/CompanyID (DK:CVR or DK:CPR)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB190] schemeID = DK:CVR, CompanyID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB191] schemeID = DK:CPR, CompanyID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:Contact" priority="3987" mode="M21">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:Signature/cac:SignatoryParty/cac:Person" priority="3986" mode="M21">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M21" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M21" />
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty" priority="3999" mode="M22">
<xsl:choose>
<xsl:when test="count(cbc:DataSendingCapability) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DataSendingCapability) = 0</Pattern>
<Description>[F-CRN026] DataSendingCapability element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:Party) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:Party) = 1</Pattern>
<Description>[[F-CRN027]] Party class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party" priority="3998" mode="M22">
<xsl:choose>
<xsl:when test="count(cbc:MarkCareIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkCareIndicator) = 0</Pattern>
<Description>[F-LIB166] MarkCareIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:MarkAttentionIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkAttentionIndicator) = 0</Pattern>
<Description>[F-LIB167] MarkAttentionIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:AgentParty) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AgentParty) = 0</Pattern>
<Description>[F-LIB168] AgentParty class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:EndpointID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EndpointID != ''</Pattern>
<Description>[F-CRN028] Invalid EndpointID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:PartyLegalEntity) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PartyLegalEntity) = 1</Pattern>
<Description>[F-CRN031] One PartyLegalEntity class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')</Pattern>
<Description>[F-LIB022] PartyName/Name is mandatory if PartyIdentification/ID is not found</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')</Pattern>
<Description>[F-LIB179] Invalid schemeID. Must be a valid scheme for EndpointID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB180] schemeID = DK:CVR, EndpointID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB215] schemeID = DK:CPR, EndpointID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB181] schemeID = GLN, EndpointID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB216] schemeID = EAN, EndpointID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyIdentification" priority="3997" mode="M22">
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN'</Pattern>
<Description>[F-LIB183] Invalid schemeID. Must be a valid scheme for PartyIdentification/ID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB184] schemeID = DK:CVR, ID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB217] schemeID = DK:CPR, ID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB185] schemeID = GLN, ID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB218] schemeID = EAN, ID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyName" priority="3996" mode="M22">
<xsl:if test="count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)</Pattern>
<Description>[W-LIB219] The attribute Name@languageID should be used when more than one PartyName class is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID</Pattern>
<Description>[W-LIB220] Multilanguage error. Replicated PartyName classes with same Name@languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PostalAddress" priority="3995" mode="M22">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PhysicalLocation" priority="3994" mode="M22">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PhysicalLocation/cac:ValidityPeriod" priority="3993" mode="M22">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3992" mode="M22">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PhysicalLocation/cac:Address" priority="3991" mode="M22">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme" priority="3990" mode="M22">
<xsl:choose>
<xsl:when test="count(cbc:TaxLevelCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TaxLevelCode) = 0</Pattern>
<Description>[F-LIB192] TaxLevelCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB193] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB195] Invalid schemeID. Must be a valid scheme for PartyTaxScheme/CompanyID (DK:SE)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB196] schemeID = DK:SE, CompanyID must be a valid SE number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme" priority="3989" mode="M22">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity" priority="3988" mode="M22">
<xsl:choose>
<xsl:when test="count(cac:CorporateRegistrationScheme) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:CorporateRegistrationScheme) = 0</Pattern>
<Description>[F-LIB186] CorporateRegistrationScheme class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB187] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB189] Invalid schemeID. Must be a valid scheme for PartyLegalEntity/CompanyID (DK:CVR or DK:CPR)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB190] schemeID = DK:CVR, CompanyID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB191] schemeID = DK:CPR, CompanyID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:Contact" priority="3987" mode="M22">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:Party/cac:Person" priority="3986" mode="M22">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:DespatchContact" priority="3985" mode="M22">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:AccountingContact" priority="3984" mode="M22">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingSupplierParty/cac:SellerContact" priority="3983" mode="M22">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M22" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M22" />
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty" priority="3999" mode="M23">
<xsl:choose>
<xsl:when test="count(cac:Party) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:Party) = 1</Pattern>
<Description>[F-CRN039] One Party class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party" priority="3998" mode="M23">
<xsl:choose>
<xsl:when test="count(cbc:MarkCareIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkCareIndicator) = 0</Pattern>
<Description>[F-LIB166] MarkCareIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:MarkAttentionIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkAttentionIndicator) = 0</Pattern>
<Description>[F-LIB167] MarkAttentionIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:AgentParty) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AgentParty) = 0</Pattern>
<Description>[F-LIB168] AgentParty class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:EndpointID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EndpointID != ''</Pattern>
<Description>[F-CRN040] Invalid EndpointID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:PartyLegalEntity) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PartyLegalEntity) = 1</Pattern>
<Description>[F-CRN164] One PartyLegalEntity class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:Contact) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:Contact) = 1</Pattern>
<Description>[F-CRN042] One Contact class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')</Pattern>
<Description>[F-LIB022] PartyName/Name is mandatory if PartyIdentification/ID is not found</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')</Pattern>
<Description>[F-LIB179] Invalid schemeID. Must be a valid scheme for EndpointID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB180] schemeID = DK:CVR, EndpointID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB215] schemeID = DK:CPR, EndpointID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB181] schemeID = GLN, EndpointID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB216] schemeID = EAN, EndpointID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyIdentification" priority="3997" mode="M23">
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN'</Pattern>
<Description>[F-LIB183] Invalid schemeID. Must be a valid scheme for PartyIdentification/ID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB184] schemeID = DK:CVR, ID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB217] schemeID = DK:CPR, ID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB185] schemeID = GLN, ID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB218] schemeID = EAN, ID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyName" priority="3996" mode="M23">
<xsl:if test="count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)</Pattern>
<Description>[W-LIB219] The attribute Name@languageID should be used when more than one PartyName class is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID</Pattern>
<Description>[W-LIB220] Multilanguage error. Replicated PartyName classes with same Name@languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PostalAddress" priority="3995" mode="M23">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PhysicalLocation" priority="3994" mode="M23">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PhysicalLocation/cac:ValidityPeriod" priority="3993" mode="M23">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3992" mode="M23">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PhysicalLocation/cac:Address" priority="3991" mode="M23">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme" priority="3990" mode="M23">
<xsl:choose>
<xsl:when test="count(cbc:TaxLevelCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TaxLevelCode) = 0</Pattern>
<Description>[F-LIB192] TaxLevelCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB193] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB195] Invalid schemeID. Must be a valid scheme for PartyTaxScheme/CompanyID (DK:SE)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB196] schemeID = DK:SE, CompanyID must be a valid SE number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyTaxScheme/cac:TaxScheme" priority="3989" mode="M23">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity" priority="3988" mode="M23">
<xsl:choose>
<xsl:when test="count(cac:CorporateRegistrationScheme) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:CorporateRegistrationScheme) = 0</Pattern>
<Description>[F-LIB186] CorporateRegistrationScheme class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB187] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB189] Invalid schemeID. Must be a valid scheme for PartyLegalEntity/CompanyID (DK:CVR or DK:CPR)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB190] schemeID = DK:CVR, CompanyID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB191] schemeID = DK:CPR, CompanyID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:Contact" priority="3987" mode="M23">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN047] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:Party/cac:Person" priority="3986" mode="M23">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:DeliveryContact" priority="3985" mode="M23">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:AccountingContact" priority="3984" mode="M23">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AccountingCustomerParty/cac:BuyerContact" priority="3983" mode="M23">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M23" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M23" />
<xsl:template match="doc:CreditNote/cac:PayeeParty" priority="3999" mode="M24">
<xsl:choose>
<xsl:when test="count(cbc:MarkCareIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkCareIndicator) = 0</Pattern>
<Description>[F-LIB166] MarkCareIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:MarkAttentionIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkAttentionIndicator) = 0</Pattern>
<Description>[F-LIB167] MarkAttentionIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:AgentParty) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AgentParty) = 0</Pattern>
<Description>[F-LIB168] AgentParty class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')</Pattern>
<Description>[F-LIB022] PartyName/Name is mandatory if PartyIdentification/ID is not found</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')</Pattern>
<Description>[F-LIB179] Invalid schemeID. Must be a valid scheme for EndpointID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB180] schemeID = DK:CVR, EndpointID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB215] schemeID = DK:CPR, EndpointID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB181] schemeID = GLN, EndpointID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB216] schemeID = EAN, EndpointID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:PartyLegalEntity) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PartyLegalEntity) &gt; 1</Pattern>
<Description>[F-CRN166] No more than one PartyLegalEntity class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PartyIdentification" priority="3998" mode="M24">
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN'</Pattern>
<Description>[F-LIB183] Invalid schemeID. Must be a valid scheme for PartyIdentification/ID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB184] schemeID = DK:CVR, ID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB217] schemeID = DK:CPR, ID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB185] schemeID = GLN, ID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB218] schemeID = EAN, ID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PartyName" priority="3997" mode="M24">
<xsl:if test="count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)</Pattern>
<Description>[W-LIB219] The attribute Name@languageID should be used when more than one PartyName class is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID</Pattern>
<Description>[W-LIB220] Multilanguage error. Replicated PartyName classes with same Name@languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PostalAddress" priority="3996" mode="M24">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PhysicalLocation" priority="3995" mode="M24">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3994" mode="M24">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3993" mode="M24">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PhysicalLocation/cac:Address" priority="3992" mode="M24">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PartyTaxScheme" priority="3991" mode="M24">
<xsl:choose>
<xsl:when test="count(cbc:TaxLevelCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TaxLevelCode) = 0</Pattern>
<Description>[F-LIB192] TaxLevelCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB193] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB195] Invalid schemeID. Must be a valid scheme for PartyTaxScheme/CompanyID (DK:SE)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB196] schemeID = DK:SE, CompanyID must be a valid SE number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3990" mode="M24">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:PartyLegalEntity" priority="3989" mode="M24">
<xsl:choose>
<xsl:when test="count(cac:CorporateRegistrationScheme) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:CorporateRegistrationScheme) = 0</Pattern>
<Description>[F-LIB186] CorporateRegistrationScheme class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB187] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB189] Invalid schemeID. Must be a valid scheme for PartyLegalEntity/CompanyID (DK:CVR or DK:CPR)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB190] schemeID = DK:CVR, CompanyID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB191] schemeID = DK:CPR, CompanyID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:Contact" priority="3988" mode="M24">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PayeeParty/cac:Person" priority="3987" mode="M24">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M24" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M24" />
<xsl:template match="doc:CreditNote/cac:PricingExchangeRate" priority="3999" mode="M26">
<xsl:if test="cac:ForeignExchangeContract and not(cac:ForeignExchangeContract/cbc:ID != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:ForeignExchangeContract and not(cac:ForeignExchangeContract/cbc:ID != '')</Pattern>
<Description>[F-LIB238] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:SourceCurrencyCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyCode != ''</Pattern>
<Description>[F-LIB083] Invalid SourceCurrencyCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:TargetCurrencyCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyCode != ''</Pattern>
<Description>[F-LIB084] Invalid TargetCurrencyCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:SourceCurrencyBaseRate and (starts-with(cbc:SourceCurrencyBaseRate,'-') or cbc:SourceCurrencyBaseRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyBaseRate and (starts-with(cbc:SourceCurrencyBaseRate,'-') or cbc:SourceCurrencyBaseRate = 0)</Pattern>
<Description>[F-LIB085] Invalid SourceCurrencyBaseRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:SourceCurrencyBaseRate and string-length(substring-after(cbc:SourceCurrencyBaseRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyBaseRate and string-length(substring-after(cbc:SourceCurrencyBaseRate, '.')) != 4</Pattern>
<Description>[F-LIB086] Invalid SourceCurrencyBaseRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TargetCurrencyBaseRate and (starts-with(cbc:TargetCurrencyBaseRate,'-') or cbc:TargetCurrencyBaseRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyBaseRate and (starts-with(cbc:TargetCurrencyBaseRate,'-') or cbc:TargetCurrencyBaseRate = 0)</Pattern>
<Description>[F-LIB087] Invalid TargetCurrencyBaseRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TargetCurrencyBaseRate and string-length(substring-after(cbc:TargetCurrencyBaseRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyBaseRate and string-length(substring-after(cbc:TargetCurrencyBaseRate, '.')) != 4</Pattern>
<Description>[F-LIB088] Invalid TargetCurrencyBaseRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationRate and (starts-with(cbc:CalculationRate,'-') or cbc:CalculationRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationRate and (starts-with(cbc:CalculationRate,'-') or cbc:CalculationRate = 0)</Pattern>
<Description>[F-LIB089] Invalid CalculationRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationRate and string-length(substring-after(cbc:CalculationRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationRate and string-length(substring-after(cbc:CalculationRate, '.')) != 4</Pattern>
<Description>[F-LIB090] Invalid CalculationRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:ForeignExchangeContract/cbc:ContractTypeCode and cac:ForeignExchangeContract/cbc:ContractType">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:ForeignExchangeContract/cbc:ContractTypeCode and cac:ForeignExchangeContract/cbc:ContractType</Pattern>
<Description>[F-LIB239] Use either ContractTypeCode or ContractType</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:ForeignExchangeContract/cac:ContractDocumentReference) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:ForeignExchangeContract/cac:ContractDocumentReference) &gt; 1</Pattern>
<Description>[F-LIB240] No more than one ContractDocumentReference class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M26" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PricingExchangeRate/cac:ForeignExchangeContract/cac:ValidityPeriod" priority="3998" mode="M26">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M26" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PricingExchangeRate/cac:ForeignExchangeContract/cac:ValidityPeriod/cbc:Description" priority="3997" mode="M26">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M26" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PricingExchangeRate/cac:ForeignExchangeContract/cac:ContractDocumentReference" priority="3996" mode="M26">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M26" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M26" />
<xsl:template match="doc:CreditNote/cac:PaymentExchangeRate" priority="3999" mode="M27">
<xsl:if test="cac:ForeignExchangeContract and not(cac:ForeignExchangeContract/cbc:ID != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:ForeignExchangeContract and not(cac:ForeignExchangeContract/cbc:ID != '')</Pattern>
<Description>[F-LIB238] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:SourceCurrencyCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyCode != ''</Pattern>
<Description>[F-LIB083] Invalid SourceCurrencyCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:TargetCurrencyCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyCode != ''</Pattern>
<Description>[F-LIB084] Invalid TargetCurrencyCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:SourceCurrencyBaseRate and (starts-with(cbc:SourceCurrencyBaseRate,'-') or cbc:SourceCurrencyBaseRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyBaseRate and (starts-with(cbc:SourceCurrencyBaseRate,'-') or cbc:SourceCurrencyBaseRate = 0)</Pattern>
<Description>[F-LIB085] Invalid SourceCurrencyBaseRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:SourceCurrencyBaseRate and string-length(substring-after(cbc:SourceCurrencyBaseRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyBaseRate and string-length(substring-after(cbc:SourceCurrencyBaseRate, '.')) != 4</Pattern>
<Description>[F-LIB086] Invalid SourceCurrencyBaseRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TargetCurrencyBaseRate and (starts-with(cbc:TargetCurrencyBaseRate,'-') or cbc:TargetCurrencyBaseRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyBaseRate and (starts-with(cbc:TargetCurrencyBaseRate,'-') or cbc:TargetCurrencyBaseRate = 0)</Pattern>
<Description>[F-LIB087] Invalid TargetCurrencyBaseRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TargetCurrencyBaseRate and string-length(substring-after(cbc:TargetCurrencyBaseRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyBaseRate and string-length(substring-after(cbc:TargetCurrencyBaseRate, '.')) != 4</Pattern>
<Description>[F-LIB088] Invalid TargetCurrencyBaseRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationRate and (starts-with(cbc:CalculationRate,'-') or cbc:CalculationRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationRate and (starts-with(cbc:CalculationRate,'-') or cbc:CalculationRate = 0)</Pattern>
<Description>[F-LIB089] Invalid CalculationRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationRate and string-length(substring-after(cbc:CalculationRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationRate and string-length(substring-after(cbc:CalculationRate, '.')) != 4</Pattern>
<Description>[F-LIB090] Invalid CalculationRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:ForeignExchangeContract/cbc:ContractTypeCode and cac:ForeignExchangeContract/cbc:ContractType">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:ForeignExchangeContract/cbc:ContractTypeCode and cac:ForeignExchangeContract/cbc:ContractType</Pattern>
<Description>[F-LIB239] Use either ContractTypeCode or ContractType</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:ForeignExchangeContract/cac:ContractDocumentReference) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:ForeignExchangeContract/cac:ContractDocumentReference) &gt; 1</Pattern>
<Description>[F-LIB240] No more than one ContractDocumentReference class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M27" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PaymentExchangeRate/cac:ForeignExchangeContract/cac:ValidityPeriod" priority="3998" mode="M27">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M27" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PaymentExchangeRate/cac:ForeignExchangeContract/cac:ValidityPeriod/cbc:Description" priority="3997" mode="M27">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M27" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PaymentExchangeRate/cac:ForeignExchangeContract/cac:ContractDocumentReference" priority="3996" mode="M27">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M27" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M27" />
<xsl:template match="doc:CreditNote/cac:PaymentAlternativeExchangeRate" priority="3999" mode="M28">
<xsl:if test="cac:ForeignExchangeContract and not(cac:ForeignExchangeContract/cbc:ID != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:ForeignExchangeContract and not(cac:ForeignExchangeContract/cbc:ID != '')</Pattern>
<Description>[F-LIB238] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:SourceCurrencyCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyCode != ''</Pattern>
<Description>[F-LIB083] Invalid SourceCurrencyCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:TargetCurrencyCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyCode != ''</Pattern>
<Description>[F-LIB084] Invalid TargetCurrencyCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:SourceCurrencyBaseRate and (starts-with(cbc:SourceCurrencyBaseRate,'-') or cbc:SourceCurrencyBaseRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyBaseRate and (starts-with(cbc:SourceCurrencyBaseRate,'-') or cbc:SourceCurrencyBaseRate = 0)</Pattern>
<Description>[F-LIB085] Invalid SourceCurrencyBaseRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:SourceCurrencyBaseRate and string-length(substring-after(cbc:SourceCurrencyBaseRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:SourceCurrencyBaseRate and string-length(substring-after(cbc:SourceCurrencyBaseRate, '.')) != 4</Pattern>
<Description>[F-LIB086] Invalid SourceCurrencyBaseRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TargetCurrencyBaseRate and (starts-with(cbc:TargetCurrencyBaseRate,'-') or cbc:TargetCurrencyBaseRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyBaseRate and (starts-with(cbc:TargetCurrencyBaseRate,'-') or cbc:TargetCurrencyBaseRate = 0)</Pattern>
<Description>[F-LIB087] Invalid TargetCurrencyBaseRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TargetCurrencyBaseRate and string-length(substring-after(cbc:TargetCurrencyBaseRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TargetCurrencyBaseRate and string-length(substring-after(cbc:TargetCurrencyBaseRate, '.')) != 4</Pattern>
<Description>[F-LIB088] Invalid TargetCurrencyBaseRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationRate and (starts-with(cbc:CalculationRate,'-') or cbc:CalculationRate = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationRate and (starts-with(cbc:CalculationRate,'-') or cbc:CalculationRate = 0)</Pattern>
<Description>[F-LIB089] Invalid CalculationRate. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationRate and string-length(substring-after(cbc:CalculationRate, '.')) != 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationRate and string-length(substring-after(cbc:CalculationRate, '.')) != 4</Pattern>
<Description>[F-LIB090] Invalid CalculationRate. Must have 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:ForeignExchangeContract/cbc:ContractTypeCode and cac:ForeignExchangeContract/cbc:ContractType">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:ForeignExchangeContract/cbc:ContractTypeCode and cac:ForeignExchangeContract/cbc:ContractType</Pattern>
<Description>[F-LIB239] Use either ContractTypeCode or ContractType</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:ForeignExchangeContract/cac:ContractDocumentReference) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:ForeignExchangeContract/cac:ContractDocumentReference) &gt; 1</Pattern>
<Description>[F-LIB240] No more than one ContractDocumentReference class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M28" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PaymentAlternativeExchangeRate/cac:ForeignExchangeContract/cac:ValidityPeriod" priority="3998" mode="M28">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M28" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PaymentAlternativeExchangeRate/cac:ForeignExchangeContract/cac:ValidityPeriod/cbc:Description" priority="3997" mode="M28">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M28" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:PaymentAlternativeExchangeRate/cac:ForeignExchangeContract/cac:ContractDocumentReference" priority="3996" mode="M28">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M28" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M28" />
<xsl:template match="doc:CreditNote/cac:AllowanceCharge" priority="3999" mode="M29">
<xsl:choose>
<xsl:when test="count(cac:TaxTotal) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxTotal) = 0</Pattern>
<Description>[F-LIB224] TaxTotal class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:PaymentMeans) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PaymentMeans) = 0</Pattern>
<Description>[F-LIB225] PaymentMeans class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:TaxCategory) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxCategory) = 1</Pattern>
<Description>[F-LIB226] One TaxCategory class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:MultiplierFactorNumeric and not(cbc:BaseAmount != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:MultiplierFactorNumeric and not(cbc:BaseAmount != '')</Pattern>
<Description>[F-LIB248] When MultiplierFactorNumeric is used, BaseAmount is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="starts-with(cbc:MultiplierFactorNumeric,'-')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>starts-with(cbc:MultiplierFactorNumeric,'-')</Pattern>
<Description>[F-LIB227] MultiplierFactorNumeric must be a positive number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:MultiplierFactorNumeric and not(cbc:Amount = (cbc:BaseAmount * cbc:MultiplierFactorNumeric))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:MultiplierFactorNumeric and not(cbc:Amount = (cbc:BaseAmount * cbc:MultiplierFactorNumeric))</Pattern>
<Description>[F-LIB228] Amount must equal BaseAmount * MultiplierFactorNumeric</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AccountingCost and cbc:AccountingCostCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AccountingCost and cbc:AccountingCostCode</Pattern>
<Description>[F-LIB021] Use either AccountingCost or AccountingCostCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M29" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AllowanceCharge/cbc:SequenceNumeric" priority="3998" mode="M29">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-'))</Pattern>
<Description>[F-LIB020] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M29" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AllowanceCharge/cbc:Amount" priority="3997" mode="M29">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M29" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AllowanceCharge/cbc:BaseAmount" priority="3996" mode="M29">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M29" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AllowanceCharge/cac:TaxCategory" priority="3995" mode="M29">
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB072] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB073] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB074] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'</Pattern>
<Description>[F-LIB075] Invalid schemeID. Must be 'urn:oioubl:id:taxcategoryid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeAgencyID = '320'</Pattern>
<Description>[W-LIB229] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))</Pattern>
<Description>[W-LIB230] Name should only be used within NES profiles</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and cbc:Percent">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and cbc:Percent</Pattern>
<Description>[F-LIB231] Use either PerUnitAmount or Percent</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')</Pattern>
<Description>[F-LIB232] When PerUnitAmount is used, BaseUnitMeasure is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M29" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:AllowanceCharge/cac:TaxCategory/cac:TaxScheme" priority="3994" mode="M29">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M29" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M29" />
<xsl:template match="doc:CreditNote/cac:TaxTotal" priority="3999" mode="M30">
<xsl:choose>
<xsl:when test="not(starts-with(cbc:TaxAmount,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(cbc:TaxAmount,'-'))</Pattern>
<Description>[F-LIB249] Invalid TaxAmount. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(cbc:TaxAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(cbc:TaxAmount, '.')) != 2</Pattern>
<Description>[F-LIB250] Invalid TaxAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:RoundingAmount and (starts-with(cbc:RoundingAmount,'-') or cbc:RoundingAmount = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:RoundingAmount and (starts-with(cbc:RoundingAmount,'-') or cbc:RoundingAmount = 0)</Pattern>
<Description>[F-LIB251] Invalid RoundingAmount. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:RoundingAmount and string-length(substring-after(cbc:RoundingAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:RoundingAmount and string-length(substring-after(cbc:RoundingAmount, '.')) != 2</Pattern>
<Description>[F-LIB252] Invalid RoundingAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TaxEvidenceIndicator = 'false' and /doc:Invoice/cbc:InvoiceTypeCode != '325'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TaxEvidenceIndicator = 'false' and /doc:Invoice/cbc:InvoiceTypeCode != '325'</Pattern>
<Description>[F-LIB253] Can only be false if proforma invoice (InvoiceTypeCode = '325')</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M30" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:TaxTotal/cac:TaxSubtotal" priority="3998" mode="M30">
<xsl:choose>
<xsl:when test="count(cbc:Percent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:Percent) = 0</Pattern>
<Description>[F-LIB254] Percent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:BaseUnitMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BaseUnitMeasure) = 0</Pattern>
<Description>[F-LIB255] BaseUnitMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:PerUnitAmount) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:PerUnitAmount) = 0</Pattern>
<Description>[F-LIB256] PerUnitAmount element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB257] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB258] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:TaxableAmount != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TaxableAmount != ''</Pattern>
<Description>[F-LIB259] Invalid TaxableAmount. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="not(starts-with(cbc:TaxableAmount,'-') or cbc:TaxableAmount = 0)" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(cbc:TaxableAmount,'-') or cbc:TaxableAmount = 0)</Pattern>
<Description>[F-LIB260] Invalid TaxableAmount. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(cbc:TaxableAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(cbc:TaxableAmount, '.')) != 2</Pattern>
<Description>[F-LIB261] Invalid TaxableAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="not(starts-with(cbc:TaxAmount,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(cbc:TaxAmount,'-'))</Pattern>
<Description>[F-LIB262] Invalid TaxAmount. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(cbc:TaxAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(cbc:TaxAmount, '.')) != 2</Pattern>
<Description>[F-LIB263] Invalid TaxAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationSequenceNumeric and (starts-with(cbc:CalculationSequenceNumeric,'-') or cbc:CalculationSequenceNumeric = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationSequenceNumeric and (starts-with(cbc:CalculationSequenceNumeric,'-') or cbc:CalculationSequenceNumeric = 0)</Pattern>
<Description>[F-LIB264] Invalid CalculationSequenceNumeric. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="/doc:Invoice/cac:TaxExchangeRate and count(cbc:TransactionCurrencyTaxAmount) = 0">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>/doc:Invoice/cac:TaxExchangeRate and count(cbc:TransactionCurrencyTaxAmount) = 0</Pattern>
<Description>[F-LIB265] if Tax Currency is different from Document Currency, TransactionCurrencyTaxAmount is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TransactionCurrencyTaxAmount and (starts-with(cbc:TransactionCurrencyTaxAmount,'-'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TransactionCurrencyTaxAmount and (starts-with(cbc:TransactionCurrencyTaxAmount,'-'))</Pattern>
<Description>[F-LIB266] Invalid TransactionCurrencyTaxAmount. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TransactionCurrencyTaxAmount and string-length(substring-after(cbc:TransactionCurrencyTaxAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TransactionCurrencyTaxAmount and string-length(substring-after(cbc:TransactionCurrencyTaxAmount, '.')) != 2</Pattern>
<Description>[F-LIB267] Invalid TransactionCurrencyTaxAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M30" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory" priority="3997" mode="M30">
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB072] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB073] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB074] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'</Pattern>
<Description>[F-LIB075] Invalid schemeID. Must be 'urn:oioubl:id:taxcategoryid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeAgencyID = '320'</Pattern>
<Description>[W-LIB229] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))</Pattern>
<Description>[W-LIB230] Name should only be used within NES profiles</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and cbc:Percent">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and cbc:Percent</Pattern>
<Description>[F-LIB231] Use either PerUnitAmount or Percent</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')</Pattern>
<Description>[F-LIB232] When PerUnitAmount is used, BaseUnitMeasure is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M30" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cac:TaxScheme" priority="3996" mode="M30">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M30" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M30" />
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal" priority="3999" mode="M31">
<xsl:choose>
<xsl:when test="cbc:LineExtensionAmount != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:LineExtensionAmount != ''</Pattern>
<Description>[F-CRN066] Invalid LineExtensionAmount. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:TaxExclusiveAmount != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TaxExclusiveAmount != ''</Pattern>
<Description>[F-CRN067] TaxExclusiveAmount is mandatory when TaxTotal classes are present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="count(../cac:AllowanceCharge[cbc:ChargeIndicator='false']) and not(cbc:AllowanceTotalAmount)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:AllowanceCharge[cbc:ChargeIndicator='false']) and not(cbc:AllowanceTotalAmount)</Pattern>
<Description>[F-CRN068] AllowanceTotalAmount is mandatory when AllowanceCharge classes (with ChargeIndicator='false') are present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(../cac:AllowanceCharge[cbc:ChargeIndicator='true']) and not(cbc:ChargeTotalAmount)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:AllowanceCharge[cbc:ChargeIndicator='true']) and not(cbc:ChargeTotalAmount)</Pattern>
<Description>[F-CRN069] ChargeTotalAmount is mandatory when AllowanceCharge classes (with ChargeIndicator='true') are present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(../cac:PrepaidPayment/cbc:PaidAmount) and not(cbc:PrepaidAmount)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:PrepaidPayment/cbc:PaidAmount) and not(cbc:PrepaidAmount)</Pattern>
<Description>[F-CRN070] PrepaidAmount is mandatory when PrepaidPayment/PaidAmount elements are present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(../cac:TaxTotal/cbc:RoundingAmount) and not(cbc:PayableRoundingAmount)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:TaxTotal/cbc:RoundingAmount) and not(cbc:PayableRoundingAmount)</Pattern>
<Description>[F-CRN071] PayableRoundingAmount is mandatory when TaxTotal/RoundingAmount elements are present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:LineExtensionAmount = sum(../cac:CreditNoteLine/cbc:LineExtensionAmount)" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:LineExtensionAmount = sum(../cac:CreditNoteLine/cbc:LineExtensionAmount)</Pattern>
<Description>[F-CRN072] The sum of CreditNoteLine/LineExtensionAmount elements must equal LineExtensionAmount</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:TaxExclusiveAmount and not(cbc:TaxExclusiveAmount = sum(../cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TaxExclusiveAmount and not(cbc:TaxExclusiveAmount = sum(../cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount))</Pattern>
<Description>[F-CRN073] The sum of TaxTotal/TaxSubtotal/TaxAmount elements must equal TaxExclusiveAmount</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TaxInclusiveAmount and not(cbc:TaxInclusiveAmount = sum(cbc:LineExtensionAmount) + sum(../cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount) + sum(cbc:ChargeTotalAmount) - sum(cbc:AllowanceTotalAmount) + sum(cbc:PayableRoundingAmount))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TaxInclusiveAmount and not(cbc:TaxInclusiveAmount = sum(cbc:LineExtensionAmount) + sum(../cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount) + sum(cbc:ChargeTotalAmount) - sum(cbc:AllowanceTotalAmount) + sum(cbc:PayableRoundingAmount))</Pattern>
<Description>[F-CRN074] TaxInclusiveAmount is calculated incorrectly</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AllowanceTotalAmount and not(cbc:AllowanceTotalAmount = sum(../cac:AllowanceCharge[cbc:ChargeIndicator='false']/cbc:Amount))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AllowanceTotalAmount and not(cbc:AllowanceTotalAmount = sum(../cac:AllowanceCharge[cbc:ChargeIndicator='false']/cbc:Amount))</Pattern>
<Description>[F-CRN075] The sum of AllowanceCharge/Amount elements (with ChargeIndicator='false') must equal AllowanceTotalAmount</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ChargeTotalAmount and not(cbc:ChargeTotalAmount = sum(../cac:AllowanceCharge[cbc:ChargeIndicator='true']/cbc:Amount))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ChargeTotalAmount and not(cbc:ChargeTotalAmount = sum(../cac:AllowanceCharge[cbc:ChargeIndicator='true']/cbc:Amount))</Pattern>
<Description>[F-CRN076] The sum of AllowanceCharge/Amount elements (with ChargeIndicator='true') must equal cbc:ChargeTotalAmount</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PrepaidAmount and not(cbc:PrepaidAmount = sum(../cac:PrepaidPayment/cbc:PaidAmount))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PrepaidAmount and not(cbc:PrepaidAmount = sum(../cac:PrepaidPayment/cbc:PaidAmount))</Pattern>
<Description>[F-CRN077] The sum of PrepaidPayment/PaidAmount elements must equal PrepaidAmount</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PayableRoundingAmount and not(cbc:PayableRoundingAmount = sum(../cac:TaxTotal/cbc:RoundingAmount))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PayableRoundingAmount and not(cbc:PayableRoundingAmount = sum(../cac:TaxTotal/cbc:RoundingAmount))</Pattern>
<Description>[F-CRN078] The sum of TaxTotal/RoundingAmount elements must equal PayableRoundingAmount</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:PayableAmount = sum(cbc:LineExtensionAmount) + sum(../cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount) + sum(cbc:ChargeTotalAmount) - sum(cbc:AllowanceTotalAmount) - sum(cbc:PrepaidAmount) + sum(cbc:PayableRoundingAmount)" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PayableAmount = sum(cbc:LineExtensionAmount) + sum(../cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount) + sum(cbc:ChargeTotalAmount) - sum(cbc:AllowanceTotalAmount) - sum(cbc:PrepaidAmount) + sum(cbc:PayableRoundingAmount)</Pattern>
<Description>[F-CRN079] PayableAmount is calculated incorrectly</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="../cbc:TaxCurrencyCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>../cbc:TaxCurrencyCode</Pattern>
<Description>[I-LIB999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:LineExtensionAmount" priority="3998" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB013] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB014] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:TaxExclusiveAmount" priority="3997" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-'))</Pattern>
<Description>[F-LIB016] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB017] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:TaxInclusiveAmount" priority="3996" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB013] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB014] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:AllowanceTotalAmount" priority="3995" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB013] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB014] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:ChargeTotalAmount" priority="3994" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB013] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB014] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:PrepaidAmount" priority="3993" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB013] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB014] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:PayableRoundingAmount" priority="3992" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB013] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB014] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:LegalMonetaryTotal/cbc:PayableAmount" priority="3991" mode="M31">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB013] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) != 2</Pattern>
<Description>[F-LIB014] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M31" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M31" />
<xsl:template match="doc:CreditNote/cac:CreditNoteLine" priority="3999" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:TaxTotal) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxTotal) = 1</Pattern>
<Description>[F-CRN081] One TaxTotal class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="not(count(cac:Item)) and not(count(cac:BillingReference))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(count(cac:Item)) and not(count(cac:BillingReference))</Pattern>
<Description>[F-CRN082] One Item class must be present when CreditNoteLine/BillingReference is not specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="not(count(cac:Price)) and not(count(cac:BillingReference))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(count(cac:Price)) and not(count(cac:BillingReference))</Pattern>
<Description>[F-CRN083] One Price class must be present when CreditNoteLine/BillingReference is not specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN084] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AccountingCost and cbc:AccountingCostCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AccountingCost and cbc:AccountingCostCode</Pattern>
<Description>[F-LIB021] Use either AccountingCost or AccountingCostCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="../cbc:PricingCurrencyCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>../cbc:PricingCurrencyCode</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:LineExtensionAmount &lt; (cbc:CreditedQuantity * (cac:Price/cbc:PriceAmount div cac:Price/cbc:BaseQuantity) - '01.00')) or (cbc:LineExtensionAmount &gt; (cbc:CreditedQuantity * (cac:Price/cbc:PriceAmount div cac:Price/cbc:BaseQuantity) + '01.00'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:LineExtensionAmount &lt; (cbc:CreditedQuantity * (cac:Price/cbc:PriceAmount div cac:Price/cbc:BaseQuantity) - '01.00')) or (cbc:LineExtensionAmount &gt; (cbc:CreditedQuantity * (cac:Price/cbc:PriceAmount div cac:Price/cbc:BaseQuantity) + '01.00'))</Pattern>
<Description>[F-CRN085] LineExtensionAmount must equal CreditedQuantity * (Price.PriceAmount/Price.BaseQuantity)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cbc:CreditedQuantity" priority="3998" mode="M32">
<xsl:if test="not(./@unitCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(./@unitCode)</Pattern>
<Description>[F-LIB007] Attribute unitCode must be used for<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>
</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="string-length(./@unitCode)&gt;1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(./@unitCode)&gt;1</Pattern>
<Description>[W-LIB008] The value of unitCode attribute should be a valid UOM measure</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(., '.')) &gt; 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) &gt; 4</Pattern>
<Description>[F-CRN087] Invalid CreditedQuantity. No more than 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test=". != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>. != 0</Pattern>
<Description>[F-CRN088] Invalid CreditedQuantity. Must not be zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cbc:LineExtensionAmount" priority="3997" mode="M32">
<xsl:if test="string-length(substring-after(., '.')) &lt; 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) &lt; 2</Pattern>
<Description>[F-CRN089] Invalid LineExtensionAmount. Must have at least 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="string-length(substring-after(., '.')) &gt; 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) &gt; 4</Pattern>
<Description>[F-CRN090] Invalid LineExtensionAmount. No more than 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:DiscrepancyResponse" priority="3996" mode="M32">
<xsl:choose>
<xsl:when test="cbc:ReferenceID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ReferenceID != ''</Pattern>
<Description>[F-CRN168] Invalid ReferenceID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:ResponseCode and cbc:Description">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ResponseCode and cbc:Description</Pattern>
<Description>[F-CRN169] Use either ResponseCode or Description</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:DiscrepancyResponse/cbc:ResponseCode" priority="3995" mode="M32">
<xsl:choose>
<xsl:when test="./@listID = 'urn:oioubl:codelist:lineresponsecode-1.0'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@listID = 'urn:oioubl:codelist:lineresponsecode-1.0'</Pattern>
<Description>[F-CRN091] Invalid listID. Must be 'urn:oioubl:codelist:lineresponsecode-1.0'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="./@listAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@listAgencyID = '320'</Pattern>
<Description>[F-CRN167] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:DiscrepancyResponse/cbc:Description" priority="3994" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-CRN092] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-CRN093] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:DespatchLineReference" priority="3993" mode="M32">
<xsl:choose>
<xsl:when test="cbc:LineID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:LineID != ''</Pattern>
<Description>[F-CRN170] Invalid LineID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:DespatchLineReference/cac:DocumentReference" priority="3992" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:ReceiptLineReference" priority="3991" mode="M32">
<xsl:choose>
<xsl:when test="cbc:LineID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:LineID != ''</Pattern>
<Description>[F-CRN171] Invalid LineID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:ReceiptLineReference/cac:DocumentReference" priority="3990" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference" priority="3989" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:DebitNoteDocumentReference) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:DebitNoteDocumentReference) = 0</Pattern>
<Description>[F-CRN172] DebitNoteDocumentReference class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:AdditionalDocumentReference) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AdditionalDocumentReference) = 0</Pattern>
<Description>[F-CRN173] AdditionalDocumentReference class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="count(cac:BillingReferenceLine) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:BillingReferenceLine) &gt; 1</Pattern>
<Description>[F-CRN174] No more than one BillingReferenceLine class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:InvoiceDocumentReference" priority="3988" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:SelfBilledInvoiceDocumentReference" priority="3987" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:CreditNoteDocumentReference" priority="3986" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:SelfBilledCreditNoteDocumentReference" priority="3985" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:ReminderDocumentReference" priority="3984" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:BillingReferenceLine" priority="3983" mode="M32">
<xsl:if test="count(cac:AllowanceCharge) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AllowanceCharge) &gt; 1</Pattern>
<Description>[F-CRN175] No more than one AllowanceCharge class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:BillingReferenceLine/cac:AllowanceCharge" priority="3982" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:TaxTotal) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxTotal) = 0</Pattern>
<Description>[F-LIB224] TaxTotal class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:PaymentMeans) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PaymentMeans) = 0</Pattern>
<Description>[F-LIB225] PaymentMeans class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:TaxCategory) = 1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxCategory) = 1</Pattern>
<Description>[F-LIB226] One TaxCategory class must be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:MultiplierFactorNumeric and not(cbc:BaseAmount != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:MultiplierFactorNumeric and not(cbc:BaseAmount != '')</Pattern>
<Description>[F-LIB248] When MultiplierFactorNumeric is used, BaseAmount is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="starts-with(cbc:MultiplierFactorNumeric,'-')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>starts-with(cbc:MultiplierFactorNumeric,'-')</Pattern>
<Description>[F-LIB227] MultiplierFactorNumeric must be a positive number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:MultiplierFactorNumeric and not(cbc:Amount = (cbc:BaseAmount * cbc:MultiplierFactorNumeric))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:MultiplierFactorNumeric and not(cbc:Amount = (cbc:BaseAmount * cbc:MultiplierFactorNumeric))</Pattern>
<Description>[F-LIB228] Amount must equal BaseAmount * MultiplierFactorNumeric</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AccountingCost and cbc:AccountingCostCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AccountingCost and cbc:AccountingCostCode</Pattern>
<Description>[F-LIB021] Use either AccountingCost or AccountingCostCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:BillingReferenceLine/cac:AllowanceCharge/cbc:SequenceNumeric" priority="3981" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-'))</Pattern>
<Description>[F-LIB020] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:BillingReferenceLine/cac:AllowanceCharge/cbc:Amount" priority="3980" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:BillingReferenceLine/cac:AllowanceCharge/cbc:BaseAmount" priority="3979" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:BillingReferenceLine/cac:AllowanceCharge/cac:TaxCategory" priority="3978" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB072] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB073] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB074] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'</Pattern>
<Description>[F-LIB075] Invalid schemeID. Must be 'urn:oioubl:id:taxcategoryid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeAgencyID = '320'</Pattern>
<Description>[W-LIB229] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))</Pattern>
<Description>[W-LIB230] Name should only be used within NES profiles</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and cbc:Percent">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and cbc:Percent</Pattern>
<Description>[F-LIB231] Use either PerUnitAmount or Percent</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')</Pattern>
<Description>[F-LIB232] When PerUnitAmount is used, BaseUnitMeasure is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:BillingReference/cac:BillingReferenceLine/cac:AllowanceCharge/cac:TaxCategory/cac:TaxScheme" priority="3977" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:DocumentReference" priority="3976" mode="M32">
<xsl:choose>
<xsl:when test="cbc:DocumentType or cbc:DocumentTypeCode" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentType or cbc:DocumentTypeCode</Pattern>
<Description>[F-LIB092] Use either DocumentType or DocumentTypeCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB093] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:DocumentType and cbc:DocumentTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentType and cbc:DocumentTypeCode</Pattern>
<Description>[F-LIB094] Use either DocumentType or DocumentTypeCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB095] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB097] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB098] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB213] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:PricingReference" priority="3975" mode="M32">
<xsl:if test="'1' = '1'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>'1' = '1'</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery" priority="3974" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:LatestDeliveryDate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:LatestDeliveryDate) = 0</Pattern>
<Description>[F-CRN098] LatestDeliveryDate element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:LatestDeliveryTime) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:LatestDeliveryTime) = 0</Pattern>
<Description>[F-CRN099] LatestDeliveryTime element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:DeliveryAddress) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:DeliveryAddress) = 0</Pattern>
<Description>[F-CRN157] DeliveryAddress class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:PromisedDeliveryPeriod) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PromisedDeliveryPeriod) = 0</Pattern>
<Description>[F-CRN100] PromisedDeliveryPeriod class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:EstimatedDeliveryPeriod) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:EstimatedDeliveryPeriod) = 0</Pattern>
<Description>[F-CRN101] EstimatedDeliveryPeriod class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Despath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Despath</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryLocation" priority="3973" mode="M32">
<xsl:if test="not(cbc:ID) and not(cac:Address)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(cbc:ID) and not(cac:Address)</Pattern>
<Description>[F-CRN158] Address is mandatory when ID is not specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryLocation/cac:ValidityPeriod" priority="3972" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryLocation/cac:ValidityPeriod/cbc:Description" priority="3971" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryLocation/cac:Address" priority="3970" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:RequestedDeliveryPeriod" priority="3969" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:RequestedDeliveryPeriod/cbc:Description" priority="3968" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty" priority="3967" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:MarkCareIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkCareIndicator) = 0</Pattern>
<Description>[F-LIB166] MarkCareIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:MarkAttentionIndicator) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:MarkAttentionIndicator) = 0</Pattern>
<Description>[F-LIB167] MarkAttentionIndicator element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:AgentParty) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AgentParty) = 0</Pattern>
<Description>[F-LIB168] AgentParty class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cac:PartyIdentification) or cac:PartyIdentification/cbc:ID = '') and (not(cac:PartyName) or cac:PartyName/cbc:Name = '')</Pattern>
<Description>[F-LIB022] PartyName/Name is mandatory if PartyIdentification/ID is not found</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EndpointID and not(cbc:EndpointID/@schemeID = 'DK:CVR' or cbc:EndpointID/@schemeID = 'DK:CPR' or cbc:EndpointID/@schemeID = 'EAN' or cbc:EndpointID/@schemeID = 'GLN')</Pattern>
<Description>[F-LIB179] Invalid schemeID. Must be a valid scheme for EndpointID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CVR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB180] schemeID = DK:CVR, EndpointID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'DK:CPR') and not(string-length(cbc:EndpointID) = 10)</Pattern>
<Description>[F-LIB215] schemeID = DK:CPR, EndpointID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'GLN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB181] schemeID = GLN, EndpointID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndpointID/@schemeID = 'EAN') and not(string-length(cbc:EndpointID) = 13)</Pattern>
<Description>[F-LIB216] schemeID = EAN, EndpointID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:PartyLegalEntity) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PartyLegalEntity) &gt; 1</Pattern>
<Description>[F-CRN176] No more than one PartyLegalEntity class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PartyIdentification" priority="3966" mode="M32">
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'DK:CVR' or cbc:ID/@schemeID = 'DK:CPR' or cbc:ID/@schemeID = 'EAN' or cbc:ID/@schemeID = 'GLN'</Pattern>
<Description>[F-LIB183] Invalid schemeID. Must be a valid scheme for PartyIdentification/ID (DK:CVR, GLN, etc.)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CVR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB184] schemeID = DK:CVR, ID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'DK:CPR') and not(string-length(cbc:ID) = 10)</Pattern>
<Description>[F-LIB217] schemeID = DK:CPR, ID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'GLN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB185] schemeID = GLN, ID must be a valid GLN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID/@schemeID = 'EAN') and not(string-length(cbc:ID) = 13)</Pattern>
<Description>[F-LIB218] schemeID = EAN, ID must be a valid EAN number (1234567890123)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PartyName" priority="3965" mode="M32">
<xsl:if test="count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cac:PartyName) &gt; 1 and not(./cbc:Name/@languageID)</Pattern>
<Description>[W-LIB219] The attribute Name@languageID should be used when more than one PartyName class is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/cbc:Name/@languageID = self::*/cbc:Name/@languageID</Pattern>
<Description>[W-LIB220] Multilanguage error. Replicated PartyName classes with same Name@languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PostalAddress" priority="3964" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PhysicalLocation" priority="3963" mode="M32">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3962" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3961" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PhysicalLocation/cac:Address" priority="3960" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PartyTaxScheme" priority="3959" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:TaxLevelCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TaxLevelCode) = 0</Pattern>
<Description>[F-LIB192] TaxLevelCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB193] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ' " />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:SE' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB195] Invalid schemeID. Must be a valid scheme for PartyTaxScheme/CompanyID (DK:SE)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:SE') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB196] schemeID = DK:SE, CompanyID must be a valid SE number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3958" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:PartyLegalEntity" priority="3957" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:CorporateRegistrationScheme) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:CorporateRegistrationScheme) = 0</Pattern>
<Description>[F-LIB186] CorporateRegistrationScheme class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID != ''</Pattern>
<Description>[F-LIB187] Invalid CompanyID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CompanyID/@schemeID = 'DK:CVR' or cbc:CompanyID/@schemeID = 'DK:CPR' or cbc:CompanyID/@schemeID = 'ZZZ'</Pattern>
<Description>[F-LIB189] Invalid schemeID. Must be a valid scheme for PartyLegalEntity/CompanyID (DK:CVR or DK:CPR)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CVR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB190] schemeID = DK:CVR, CompanyID must be a valid CVR number (DK12345678)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:CompanyID/@schemeID = 'DK:CPR') and not(string-length(cbc:CompanyID) = 10)</Pattern>
<Description>[F-LIB191] schemeID = DK:CPR, CompanyID must be a valid CPR number (1234560000)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:Contact" priority="3956" mode="M32">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (not(cbc:Name) or cbc:Name = '') and (not(cbc:Telephone) or cbc:Telephone = '') and (not(cbc:Telefax) or cbc:Telefax = '') and (not(cbc:ElectronicMail) or cbc:ElectronicMail = '') and (not(cbc:Note) or cbc:Note = '') and not(cac:OtherCommunication)</Pattern>
<Description>[F-LIB235] At least one field should be specified</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication/cbc:ChannelCode and cac:OtherCommunication/cbc:Channel</Pattern>
<Description>[F-LIB236] Use either ChannelCode or Channel</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:OtherCommunication and not(cac:OtherCommunication/cbc:Value != '')</Pattern>
<Description>[F-LIB237] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Delivery/cac:DeliveryParty/cac:Person" priority="3955" mode="M32">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:TaxTotal" priority="3954" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(cbc:TaxAmount,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(cbc:TaxAmount,'-'))</Pattern>
<Description>[F-LIB249] Invalid TaxAmount. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(cbc:TaxAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(cbc:TaxAmount, '.')) != 2</Pattern>
<Description>[F-LIB250] Invalid TaxAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:RoundingAmount and (starts-with(cbc:RoundingAmount,'-') or cbc:RoundingAmount = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:RoundingAmount and (starts-with(cbc:RoundingAmount,'-') or cbc:RoundingAmount = 0)</Pattern>
<Description>[F-LIB251] Invalid RoundingAmount. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:RoundingAmount and string-length(substring-after(cbc:RoundingAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:RoundingAmount and string-length(substring-after(cbc:RoundingAmount, '.')) != 2</Pattern>
<Description>[F-LIB252] Invalid RoundingAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TaxEvidenceIndicator = 'false' and /doc:Invoice/cbc:InvoiceTypeCode != '325'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TaxEvidenceIndicator = 'false' and /doc:Invoice/cbc:InvoiceTypeCode != '325'</Pattern>
<Description>[F-LIB253] Can only be false if proforma invoice (InvoiceTypeCode = '325')</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:TaxTotal/cac:TaxSubtotal" priority="3953" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:Percent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:Percent) = 0</Pattern>
<Description>[F-LIB254] Percent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:BaseUnitMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BaseUnitMeasure) = 0</Pattern>
<Description>[F-LIB255] BaseUnitMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:PerUnitAmount) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:PerUnitAmount) = 0</Pattern>
<Description>[F-LIB256] PerUnitAmount element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB257] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB258] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:TaxableAmount != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TaxableAmount != ''</Pattern>
<Description>[F-LIB259] Invalid TaxableAmount. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="not(starts-with(cbc:TaxableAmount,'-') or cbc:TaxableAmount = 0)" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(cbc:TaxableAmount,'-') or cbc:TaxableAmount = 0)</Pattern>
<Description>[F-LIB260] Invalid TaxableAmount. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(cbc:TaxableAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(cbc:TaxableAmount, '.')) != 2</Pattern>
<Description>[F-LIB261] Invalid TaxableAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="not(starts-with(cbc:TaxAmount,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(cbc:TaxAmount,'-'))</Pattern>
<Description>[F-LIB262] Invalid TaxAmount. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(substring-after(cbc:TaxAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(cbc:TaxAmount, '.')) != 2</Pattern>
<Description>[F-LIB263] Invalid TaxAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:CalculationSequenceNumeric and (starts-with(cbc:CalculationSequenceNumeric,'-') or cbc:CalculationSequenceNumeric = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:CalculationSequenceNumeric and (starts-with(cbc:CalculationSequenceNumeric,'-') or cbc:CalculationSequenceNumeric = 0)</Pattern>
<Description>[F-LIB264] Invalid CalculationSequenceNumeric. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="/doc:Invoice/cac:TaxExchangeRate and count(cbc:TransactionCurrencyTaxAmount) = 0">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>/doc:Invoice/cac:TaxExchangeRate and count(cbc:TransactionCurrencyTaxAmount) = 0</Pattern>
<Description>[F-LIB265] if Tax Currency is different from Document Currency, TransactionCurrencyTaxAmount is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TransactionCurrencyTaxAmount and (starts-with(cbc:TransactionCurrencyTaxAmount,'-'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TransactionCurrencyTaxAmount and (starts-with(cbc:TransactionCurrencyTaxAmount,'-'))</Pattern>
<Description>[F-LIB266] Invalid TransactionCurrencyTaxAmount. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:TransactionCurrencyTaxAmount and string-length(substring-after(cbc:TransactionCurrencyTaxAmount, '.')) != 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:TransactionCurrencyTaxAmount and string-length(substring-after(cbc:TransactionCurrencyTaxAmount, '.')) != 2</Pattern>
<Description>[F-LIB267] Invalid TransactionCurrencyTaxAmount. Must have 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory" priority="3952" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB072] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB073] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB074] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'</Pattern>
<Description>[F-LIB075] Invalid schemeID. Must be 'urn:oioubl:id:taxcategoryid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeAgencyID = '320'</Pattern>
<Description>[W-LIB229] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))</Pattern>
<Description>[W-LIB230] Name should only be used within NES profiles</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and cbc:Percent">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and cbc:Percent</Pattern>
<Description>[F-LIB231] Use either PerUnitAmount or Percent</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')</Pattern>
<Description>[F-LIB232] When PerUnitAmount is used, BaseUnitMeasure is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:TaxTotal/cac:TaxSubtotal/cac:TaxCategory/cac:TaxScheme" priority="3951" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item" priority="3950" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:OriginCountry) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:OriginCountry) = 0</Pattern>
<Description>[F-CRN109] OriginCountry class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-CRN110] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="string-length(cbc:Name) &gt; 40">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(cbc:Name) &gt; 40</Pattern>
<Description>[W-CRN153] Invalid Name. Should not exceed 40 characters</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cbc:Keyword) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:Keyword) &gt; 1</Pattern>
<Description>[F-CRN177] No more than one Keyword element may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cbc:BrandName) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BrandName) &gt; 1</Pattern>
<Description>[F-CRN178] No more than one BrandName element may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cbc:ModelName) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:ModelName) &gt; 1</Pattern>
<Description>[F-CRN179] No more than one ModelName element may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:ManufacturersItemIdentification) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:ManufacturersItemIdentification) &gt; 1</Pattern>
<Description>[F-CRN180] No more than one ManufacturersItemIdentification class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:AdditionalItemIdentification) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:AdditionalItemIdentification) &gt; 1</Pattern>
<Description>[F-CRN181] No more than one AdditionalItemIdentification class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="count(cac:ItemSpecificationDocumentReference) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:ItemSpecificationDocumentReference) &gt; 1</Pattern>
<Description>[F-CRN182] No more than one ItemSpecificationDocumentReference class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cbc:Description" priority="3949" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-CRN154] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-CRN155] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:BuyersItemIdentification" priority="3948" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:PhysicalAttribute) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PhysicalAttribute) = 0</Pattern>
<Description>[F-LIB175] PhysicalAttribute class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:MeasurementDimension) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:MeasurementDimension) = 0</Pattern>
<Description>[F-LIB176] MeasurementDimension class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB177] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:IssuerParty">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:IssuerParty</Pattern>
<Description>[I-LIB999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:SellersItemIdentification" priority="3947" mode="M32">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN115] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:IssuerParty">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:IssuerParty</Pattern>
<Description>[I-INV999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:SellersItemIdentification/cac:PhysicalAttribute" priority="3946" mode="M32">
<xsl:choose>
<xsl:when test="cbc:Description != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Description != ''</Pattern>
<Description>[F-CRN119] Invalid Description. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AttributeID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AttributeID != ''</Pattern>
<Description>[F-CRN118] Invalid AttributeID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:SellersItemIdentification/cac:MeasurementDimension" priority="3945" mode="M32">
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:SellersItemIdentification/cac:MeasurementDimension/cbc:Description" priority="3944" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-CRN183] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-CRN184] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ManufacturersItemIdentification" priority="3943" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:PhysicalAttribute) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PhysicalAttribute) = 0</Pattern>
<Description>[F-LIB175] PhysicalAttribute class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:MeasurementDimension) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:MeasurementDimension) = 0</Pattern>
<Description>[F-LIB176] MeasurementDimension class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB177] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:IssuerParty">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:IssuerParty</Pattern>
<Description>[I-LIB999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:StandardItemIdentification" priority="3942" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:PhysicalAttribute) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PhysicalAttribute) = 0</Pattern>
<Description>[F-LIB175] PhysicalAttribute class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:MeasurementDimension) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:MeasurementDimension) = 0</Pattern>
<Description>[F-LIB176] MeasurementDimension class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB177] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:IssuerParty">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:IssuerParty</Pattern>
<Description>[I-LIB999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:CatalogueItemIdentification" priority="3941" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:PhysicalAttribute) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PhysicalAttribute) = 0</Pattern>
<Description>[F-LIB175] PhysicalAttribute class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:MeasurementDimension) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:MeasurementDimension) = 0</Pattern>
<Description>[F-LIB176] MeasurementDimension class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB177] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:IssuerParty">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:IssuerParty</Pattern>
<Description>[I-LIB999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:AdditionalItemIdentification" priority="3940" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:PhysicalAttribute) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PhysicalAttribute) = 0</Pattern>
<Description>[F-LIB175] PhysicalAttribute class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:MeasurementDimension) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:MeasurementDimension) = 0</Pattern>
<Description>[F-LIB176] MeasurementDimension class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB177] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:IssuerParty">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:IssuerParty</Pattern>
<Description>[I-LIB999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:CatalogueDocumentReference" priority="3939" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-LIB170] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DocumentTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentTypeCode) = 0</Pattern>
<Description>[F-LIB172] DocumentTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB169] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB171] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB173] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB174] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB096] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ItemSpecificationDocumentReference" priority="3938" mode="M32">
<xsl:choose>
<xsl:when test="cbc:DocumentType or cbc:DocumentTypeCode" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentType or cbc:DocumentTypeCode</Pattern>
<Description>[F-LIB092] Use either DocumentType or DocumentTypeCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cac:Attachment and cbc:XPath">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment and cbc:XPath</Pattern>
<Description>[F-LIB093] Use either Attachment or XPath</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:DocumentType and cbc:DocumentTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentType and cbc:DocumentTypeCode</Pattern>
<Description>[F-LIB094] Use either DocumentType or DocumentTypeCode</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and cac:Attachment/cac:ExternalReference</Pattern>
<Description>[F-LIB095] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-LIB097] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cbc:EmbeddedDocumentBinaryObject and not(cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/tiff' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/png' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/jpeg' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='image/gif' or cac:Attachment/cbc:EmbeddedDocumentBinaryObject/@mimeCode='application/pdf')</Pattern>
<Description>[F-LIB098] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Attachment/cac:ExternalReference and not(cac:Attachment/cac:ExternalReference/cbc:URI != '')</Pattern>
<Description>[F-LIB213] When using ExternalReference, URI is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:CommodityClassification" priority="3937" mode="M32">
<xsl:if test="cbc:ItemClassificationCode and not(cbc:ItemClassificationCode/@listID='UNSPSC')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ItemClassificationCode and not(cbc:ItemClassificationCode/@listID='UNSPSC')</Pattern>
<Description>[W-CRN132] Use a Valid UNSPSC 7.0401 code</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:TransactionConditions" priority="3936" mode="M32">
<xsl:if test="'1' = '1'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>'1' = '1'</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:HazardousItem" priority="3935" mode="M32">
<xsl:if test="'1' = '1'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>'1' = '1'</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ClassifiedTaxCategory" priority="3934" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB072] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB073] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB074] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'</Pattern>
<Description>[F-LIB075] Invalid schemeID. Must be 'urn:oioubl:id:taxcategoryid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeAgencyID = '320'</Pattern>
<Description>[W-LIB229] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))</Pattern>
<Description>[W-LIB230] Name should only be used within NES profiles</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and cbc:Percent">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and cbc:Percent</Pattern>
<Description>[F-LIB231] Use either PerUnitAmount or Percent</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')</Pattern>
<Description>[F-LIB232] When PerUnitAmount is used, BaseUnitMeasure is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ClassifiedTaxCategory/cac:TaxScheme" priority="3933" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:AdditionalItemProperty" priority="3932" mode="M32">
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-CRN133] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Value != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Value != ''</Pattern>
<Description>[F-CRN151] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:AdditionalItemProperty/cac:ItemPropertyGroup" priority="3931" mode="M32">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN152] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ManufacturerParty" priority="3930" mode="M32">
<xsl:if test="'1' = '1'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>'1' = '1'</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:InformationContentProviderParty" priority="3929" mode="M32">
<xsl:if test="'1' = '1'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>'1' = '1'</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:OriginAddress" priority="3928" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:BlockName) = 0</Pattern>
<Description>[F-LIB210] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB211] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB212] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode != ''</Pattern>
<Description>[F-LIB025] Invalid AddressFormatCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listID = 'urn:oioubl:codelist:addresstypecode-1.1')</Pattern>
<Description>[F-LIB204] Invalid listID. Must be 'urn:oioubl:codelist:addresstypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB205] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressTypeCode and not(cbc:AddressTypeCode = 'Home' or cbc:AddressTypeCode = 'Business' )</Pattern>
<Description>[F-LIB206] Invalid AddressTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' or cbc:AddressFormatCode/@listID = 'UN/ECE 3477'</Pattern>
<Description>[F-LIB026] Invalid listID. Must be either 'urn:oioubl:codelist:addressformatcode-1.1' or 'UN/ECE 3477'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode/@listAgencyID = '320')</Pattern>
<Description>[F-LIB207] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'urn:oioubl:codelist:addressformatcode-1.1' and not(cbc:AddressFormatCode = 'StructuredDK' or cbc:AddressFormatCode = 'StructuredLax' or cbc:AddressFormatCode = 'StructuredID' or cbc:AddressFormatCode = 'StructuredRegion' or cbc:AddressFormatCode = 'Unstructured')</Pattern>
<Description>[F-LIB027] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode/@listAgencyID = '6')</Pattern>
<Description>[F-LIB208] Invalid listAgencyID. Must be '6'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:AddressFormatCode/@listID = 'UN/ECE 3477' and not(cbc:AddressFormatCode = '1' or cbc:AddressFormatCode = '2' or cbc:AddressFormatCode = '3' or cbc:AddressFormatCode = '4' or cbc:AddressFormatCode = '5' or cbc:AddressFormatCode = '6' or cbc:AddressFormatCode = '7' or cbc:AddressFormatCode = '8' or cbc:AddressFormatCode = '9')</Pattern>
<Description>[F-LIB209] Invalid AddressFormatCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cac:Country and not(cac:Country/cbc:IdentificationCode != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cac:Country and not(cac:Country/cbc:IdentificationCode != '')</Pattern>
<Description>[F-LIB213] When Country is used the element Country/IdentificationCode must be filled out</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'Unstructured') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB031] An Unstructured address is only allowed to have AddressLine elements</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and cac:AddressLine</Pattern>
<Description>[F-LIB032] AddressLine elements not allowed for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and (not(cbc:PostalZone) or cbc:PostalZone = '')</Pattern>
<Description>[F-LIB033] PostalZone is mandatory for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:StreetName) or cbc:StreetName = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB034] There should be either a StreetName or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredDK') and ((not(cbc:BuildingNumber) or cbc:BuildingNumber = '') and (not(cbc:Postbox) or cbc:Postbox = ''))</Pattern>
<Description>[F-LIB035] There should be either a BuildingNumber or a Postbox for a StructuredDK address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredLax') and cac:AddressLine</Pattern>
<Description>[F-LIB036] AddressLine elements not allowed for a StructuredLax address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (not(cbc:ID) or cbc:ID = '')</Pattern>
<Description>[F-LIB037] ID is required for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredID') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0' or count(cac:Country) != '0')</Pattern>
<Description>[F-LIB038] Only the ID is used for a StructuredID address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and ((not(cac:Country/cbc:IdentificationCode) or cac:Country/cbc:IdentificationCode = '') and (not(cbc:Region) or cbc:Region = '') and (not(cbc:District) or cbc:District = ''))</Pattern>
<Description>[F-LIB039] Region or District or Country/IdentificationCode is required for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:AddressFormatCode = 'StructuredRegion') and (count(cbc:StreetName) != '0' or count(cbc:BuildingNumber) != '0' or count(cbc:CityName) != '0' or count(cbc:PostalZone) != '0')</Pattern>
<Description>[F-LIB040] Only Region, District, and/or Country/IdentificationCode can be used for a StructuredRegion address type</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(string-length(cbc:ID/@schemeID)&gt;0)</Pattern>
<Description>[F-LIB028] When ID is used under Address the attribute schemeID is used to give an addressregister</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:ID and not(cbc:ID/@schemeID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID and not(cbc:ID/@schemeID)</Pattern>
<Description>[F-LIB029] schemeID attribute must be present on an address ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Postbox and not(number(cbc:Postbox)=((cbc:Postbox + 1)-1))</Pattern>
<Description>[F-LIB030] The value of Postbox must always be a number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ItemInstance" priority="3927" mode="M32">
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ItemInstance/cac:AdditionalItemProperty" priority="3926" mode="M32">
<xsl:if test="'1' = '1'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>'1' = '1'</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ItemInstance/cac:LotIdentification/cac:AdditionalItemProperty" priority="3925" mode="M32">
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-CRN185] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Value != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Value != ''</Pattern>
<Description>[F-CRN186] Invalid Value. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ItemInstance/cac:LotIdentification/cac:AdditionalItemProperty/cac:UsabilityPeriod" priority="3924" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ItemInstance/cac:LotIdentification/cac:AdditionalItemProperty/cac:UsabilityPeriod/cbc:Description" priority="3923" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Item/cac:ItemInstance/cac:LotIdentification/cac:AdditionalItemProperty/cac:ItemPropertyGroup" priority="3922" mode="M32">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-CRN187] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price" priority="3921" mode="M32">
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cbc:PriceAmount" priority="3920" mode="M32">
<xsl:if test="string-length(substring-after(., '.')) &lt; 2">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) &lt; 2</Pattern>
<Description>[F-CRN136] Invalid PriceAmount. Must have at least 2 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="string-length(substring-after(., '.')) &gt; 4">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(substring-after(., '.')) &gt; 4</Pattern>
<Description>[F-CRN137] Invalid PriceAmount. No more than 4 decimals</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="not(starts-with(.,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-'))</Pattern>
<Description>[F-CRN138] Invalid PriceAmount. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cbc:BaseQuantity" priority="3919" mode="M32">
<xsl:if test="not(./@unitCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(./@unitCode)</Pattern>
<Description>[F-LIB007] Attribute unitCode must be used for<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>
</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="string-length(./@unitCode)&gt;1" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>string-length(./@unitCode)&gt;1</Pattern>
<Description>[W-LIB008] The value of unitCode attribute should be a valid UOM measure</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cbc:PriceChangeReason" priority="3918" mode="M32">
<xsl:if test="count(../cbc:PriceChangeReason) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:PriceChangeReason) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-CRN139] The attribute languageID should be used when more than one PriceChangeReason element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-CRN140] Multilanguage error. Replicated PriceChangeReason elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cbc:PriceTypeCode" priority="3917" mode="M32">
<xsl:choose>
<xsl:when test="./@listID = 'UN/ECE 5387'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@listID = 'UN/ECE 5387'</Pattern>
<Description>[F-CRN141] Invalid listID. Must be 'UN/ECE 5387'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cbc:OrderableUnitFactorRate" priority="3916" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:ValidityPeriod" priority="3915" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:DurationMeasure) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DurationMeasure) = 0</Pattern>
<Description>[F-LIB076] DurationMeasure element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:DescriptionCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DescriptionCode) = 0</Pattern>
<Description>[F-LIB077] DescriptionCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime) and (not(cbc:StartDate) or cbc:StartDate = '')</Pattern>
<Description>[F-LIB078] There must be a StartDate if you have a StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:EndTime) and (not(cbc:EndDate) or cbc:EndDate = '')</Pattern>
<Description>[F-LIB079] There must be a EndDate if you have a EndTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartDate and cbc:EndDate) and not(number(translate(cbc:EndDate,'-','')) &gt; number(translate(cbc:StartDate,'-','')) or number(translate(cbc:EndDate,'-','')) = number(translate(cbc:StartDate,'-','')))</Pattern>
<Description>[F-LIB080] The EndDate must be greater or equal to the startdate</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:StartTime and cbc:EndTime) and not(number(translate(cbc:EndTime,':','')) &gt; number(translate(cbc:StartTime,':','')) or number(translate(cbc:EndTime,':','')) = number(translate(cbc:StartTime,':','')))</Pattern>
<Description>[F-LIB081] EndTime must be greater or equal to StartTime</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:ValidityPeriod/cbc:Description" priority="3914" mode="M32">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-LIB222] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-LIB223] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:PriceList" priority="3913" mode="M32">
<xsl:if test="'1' = '1'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>'1' = '1'</Pattern>
<Description>[I-CRN999] Validation not yet implemented!</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:AllowanceCharge" priority="3912" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:TaxTotal) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:TaxTotal) = 0</Pattern>
<Description>[F-LIB268] TaxTotal class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:PaymentMeans) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:PaymentMeans) = 0</Pattern>
<Description>[F-LIB269] PaymentMeans class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:AccountingCostCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:AccountingCostCode) = 0</Pattern>
<Description>[F-LIB273] AccountingCostCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:AccountingCost) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:AccountingCost) = 0</Pattern>
<Description>[F-LIB274] AccountingCost element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:MultiplierFactorNumeric and not(cbc:BaseAmount != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:MultiplierFactorNumeric and not(cbc:BaseAmount != '')</Pattern>
<Description>[F-LIB270] When MultiplierFactorNumeric is used, BaseAmount is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="starts-with(cbc:MultiplierFactorNumeric,'-')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>starts-with(cbc:MultiplierFactorNumeric,'-')</Pattern>
<Description>[F-LIB271] MultiplierFactorNumeric must be a positive number</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:MultiplierFactorNumeric and not(cbc:Amount = (cbc:BaseAmount * cbc:MultiplierFactorNumeric))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:MultiplierFactorNumeric and not(cbc:Amount = (cbc:BaseAmount * cbc:MultiplierFactorNumeric))</Pattern>
<Description>[F-LIB272] Amount must equal BaseAmount * MultiplierFactorNumeric</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:AllowanceCharge/cbc:SequenceNumeric" priority="3911" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-'))" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-'))</Pattern>
<Description>[F-LIB020] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:AllowanceCharge/cbc:Amount" priority="3910" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:AllowanceCharge/cbc:BaseAmount" priority="3909" mode="M32">
<xsl:choose>
<xsl:when test="not(starts-with(.,'-')) and . != 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>not(starts-with(.,'-')) and . != 0</Pattern>
<Description>[F-LIB019] Invalid<xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>. Must not be negative or zero</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:AllowanceCharge/cac:TaxCategory" priority="3908" mode="M32">
<xsl:choose>
<xsl:when test="count(cbc:TierRange) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRange) = 0</Pattern>
<Description>[F-LIB072] TierRange element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cbc:TierRatePercent) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:TierRatePercent) = 0</Pattern>
<Description>[F-LIB073] TierRatePercent element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB074] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxcategoryid-1.1'</Pattern>
<Description>[F-LIB075] Invalid schemeID. Must be 'urn:oioubl:id:taxcategoryid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeAgencyID = '320'</Pattern>
<Description>[W-LIB229] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:Name != '') and not(contains(/doc:Invoice/cbc:ProfileID, 'nesubl.eu'))</Pattern>
<Description>[W-LIB230] Name should only be used within NES profiles</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and cbc:Percent">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and cbc:Percent</Pattern>
<Description>[F-LIB231] Use either PerUnitAmount or Percent</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:PerUnitAmount and not(cbc:BaseUnitMeasure != '')</Pattern>
<Description>[F-LIB232] When PerUnitAmount is used, BaseUnitMeasure is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="doc:CreditNote/cac:CreditNoteLine/cac:Price/cac:AllowanceCharge/cac:TaxCategory/cac:TaxScheme" priority="3907" mode="M32">
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:ID) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:ID) = 0</Pattern>
<Description>[F-LIB041] ID element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AddressTypeCode) = 0</Pattern>
<Description>[F-LIB042] AddressTypeCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Postbox) = 0</Pattern>
<Description>[F-LIB043] Postbox element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Floor) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Floor) = 0</Pattern>
<Description>[F-LIB044] Floor element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Room) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Room) = 0</Pattern>
<Description>[F-LIB045] Room element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:StreetName) = 0</Pattern>
<Description>[F-LIB046] StreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:AdditionalStreetName) = 0</Pattern>
<Description>[F-LIB047] AdditionalStreetName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BlockName) = 0</Pattern>
<Description>[F-LIB048] BlockName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingName) = 0</Pattern>
<Description>[F-LIB049] BuildingName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:BuildingNumber) = 0</Pattern>
<Description>[F-LIB050] BuildingNumber element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:InhouseMail) = 0</Pattern>
<Description>[F-LIB051] InhouseMail element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:Department) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:Department) = 0</Pattern>
<Description>[F-LIB052] Department element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkAttention) = 0</Pattern>
<Description>[F-LIB053] MarkAttention element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:MarkCare) = 0</Pattern>
<Description>[F-LIB054] MarkCare element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PlotIdentification) = 0</Pattern>
<Description>[F-LIB055] PlotIdentification element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CitySubdivisionName) = 0</Pattern>
<Description>[F-LIB056] CitySubdivisionName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CityName) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CityName) = 0</Pattern>
<Description>[F-LIB057] CityName element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:PostalZone) = 0</Pattern>
<Description>[F-LIB058] PostalZone element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentity) = 0</Pattern>
<Description>[F-LIB059] CountrySubentity element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:CountrySubentityCode) = 0</Pattern>
<Description>[F-LIB060] CountrySubentityCode element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cbc:TimezoneOffset) = 0</Pattern>
<Description>[F-LIB063] TimezoneOffset element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:AddressLine) = 0</Pattern>
<Description>[F-LIB234] AddressLine class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:JurisdictionRegionAddress/cac:LocationCoordinate) = 0</Pattern>
<Description>[F-LIB064] LocationCoordinate class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID = '63') and cbc:TaxTypeCode">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:TaxTypeCode</Pattern>
<Description>[F-LIB067] TaxTypeCode is not allowed when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-LIB065] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:Name != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:Name != ''</Pattern>
<Description>[F-LIB066] Invalid Name. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ID != '63') and not(cbc:TaxTypeCode)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and not(cbc:TaxTypeCode)</Pattern>
<Description>[F-LIB197] TaxTypeCode is mandatory when TaxScheme/ID is different from '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID/@schemeID = 'urn:oioubl:id:taxschemeid-1.1'</Pattern>
<Description>[F-LIB070] Invalid schemeID. Must be 'urn:oioubl:id:taxschemeid-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:TaxTypeCode) and not(cbc:TaxTypeCode/@listID = 'urn:oioubl:codelist:taxtypecode-1.1')</Pattern>
<Description>[F-LIB071] Invalid listID. Must be 'urn:oioubl:codelist:taxtypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID = '63') and cbc:Name != 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID = '63') and cbc:Name != 'Moms'</Pattern>
<Description>[F-LIB198] Name must equal 'Moms' when TaxScheme/ID equals '63' (Moms)</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cbc:ID != '63') and cbc:Name = 'Moms'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ID != '63') and cbc:Name = 'Moms'</Pattern>
<Description>[F-LIB199] Name must correspond to the value of TaxScheme/ID</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cac:JurisdictionRegionAddress) and cac:JurisdictionRegionAddress/cbc:AddressFormatCode != 'StructuredRegion'</Pattern>
<Description>[F-LIB233] The AddressFormatCode under JurisdictionRegionAddress must always equal 'StructuredRegion'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M32" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M32" />
<xsl:template match="text()" priority="-1" />
</xsl:stylesheet>
