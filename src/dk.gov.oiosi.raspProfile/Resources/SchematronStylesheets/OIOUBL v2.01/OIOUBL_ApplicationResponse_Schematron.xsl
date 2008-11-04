<?xml version="1.0" encoding="UTF-16" standalone="yes"?>
<xsl:stylesheet version="1.0" doc:dummy-for-xmlns="" cac:dummy-for-xmlns="" cbc:dummy-for-xmlns="" ccts:dummy-for-xmlns="" sdt:dummy-for-xmlns="" udt:dummy-for-xmlns="" xs:dummy-for-xmlns="" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:sch="http://www.ascc.net/xml/schematron" xmlns:doc="urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2" xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2" xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2" xmlns:ccts="urn:oasis:names:specification:ubl:schema:xsd:CoreComponentParameters-2" xmlns:sdt="urn:oasis:names:specification:ubl:schema:xsd:SpecializedDatatypes-2" xmlns:udt="urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2" xmlns:xs="http://www.w3.org/2001/XMLSchema">
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
<Information>Checking OIOUBL-2.01 ApplicationResponse, FC 2007-08-16, Version 0.3</Information>
<xsl:apply-templates select="/" mode="M11" /><xsl:apply-templates select="/" mode="M12" /><xsl:apply-templates select="/" mode="M13" /><xsl:apply-templates select="/" mode="M14" /><xsl:apply-templates select="/" mode="M15" /><xsl:apply-templates select="/" mode="M16" />
</Schematron>
</xsl:template>
<xsl:template match="/" priority="3999" mode="M11">
<xsl:choose>
<xsl:when test="local-name(*) = 'ApplicationResponse'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(*) = 'ApplicationResponse'</Pattern>
<Description>[F-APR001] Root element must be ApplicationResponse</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M11" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse" priority="3998" mode="M11">
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
<Description>[W-APR002] Invalid schemeID. Must be 'urn:oioubl:id:profileid-1.1'</Description>
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
<Description>[W-APR038] Invalid schemeAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="(cbc:ProfileID = 'NONE') or contains(cbc:ProfileID, 'Procurement') or contains(cbc:ProfileID, 'Catalogue') or contains(cbc:ProfileID, 'nesubl.eu')" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ProfileID = 'NONE') or contains(cbc:ProfileID, 'Procurement') or contains(cbc:ProfileID, 'Catalogue') or contains(cbc:ProfileID, 'nesubl.eu')</Pattern>
<Description>[F-APR003] Invalid ProfileID. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="(cbc:ProfileID = 'NONE') and not(cac:DocumentResponse/cac:Response/cbc:ResponseCode = 'TechnicalReject' or cac:DocumentResponse/cac:Response/cbc:ResponseCode = 'ProfileReject')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(cbc:ProfileID = 'NONE') and not(cac:DocumentResponse/cac:Response/cbc:ResponseCode = 'TechnicalReject' or cac:DocumentResponse/cac:Response/cbc:ResponseCode = 'ProfileReject')</Pattern>
<Description>[F-APR004] ProfileID with value 'NONE' is only allowed when ...ResponseCode equals 'TechnicalReject' or 'ProfileReject'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M11" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M11" />
<xsl:template match="doc:ApplicationResponse" priority="3999" mode="M12">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-APR005] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M12" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cbc:UUID" priority="3998" mode="M12">
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
<xsl:template match="doc:Invoice/cbc:Note" priority="3997" mode="M12">
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
<xsl:template match="text()" priority="-1" mode="M12" />
<xsl:template match="doc:ApplicationResponse/cac:Signature" priority="3999" mode="M13">
<xsl:choose>
<xsl:when test="cbc:ID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ID != ''</Pattern>
<Description>[F-APR006] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty" priority="3998" mode="M13">
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
<Description>[F-APR039] No more than one PartyLegalEntity class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PartyIdentification" priority="3997" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PartyName" priority="3996" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PostalAddress" priority="3995" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation" priority="3994" mode="M13">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3993" mode="M13">
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
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3992" mode="M13">
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
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PhysicalLocation/cac:Address" priority="3991" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PartyTaxScheme" priority="3990" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3989" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:PartyLegalEntity" priority="3988" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:Contact" priority="3987" mode="M13">
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
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:Signature/cac:SignatoryParty/cac:Person" priority="3986" mode="M13">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M13" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M13" />
<xsl:template match="doc:ApplicationResponse/cac:SenderParty" priority="3999" mode="M14">
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
<Description>[F-APR008] Invalid EndpointID. Must contain a value</Description>
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
<Description>[F-APR040] One PartyLegalEntity class must be present</Description>
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PartyIdentification" priority="3998" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PartyName" priority="3997" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PostalAddress" priority="3996" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PhysicalLocation" priority="3995" mode="M14">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3994" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3993" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PhysicalLocation/cac:Address" priority="3992" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PartyTaxScheme" priority="3991" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3990" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:PartyLegalEntity" priority="3989" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:Contact" priority="3988" mode="M14">
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
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:SenderParty/cac:Person" priority="3987" mode="M14">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M14" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M14" />
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty" priority="3999" mode="M15">
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
<Description>[F-APR012] Invalid EndpointID. Must contain a value</Description>
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
<Description>[F-APR041] One PartyLegalEntity class must be present</Description>
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PartyIdentification" priority="3998" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PartyName" priority="3997" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PostalAddress" priority="3996" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PhysicalLocation" priority="3995" mode="M15">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3994" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3993" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PhysicalLocation/cac:Address" priority="3992" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PartyTaxScheme" priority="3991" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3990" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:PartyLegalEntity" priority="3989" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:Contact" priority="3988" mode="M15">
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
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:ReceiverParty/cac:Person" priority="3987" mode="M15">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M15" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M15" />
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse" priority="3999" mode="M16">
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:Response" priority="3998" mode="M16">
<xsl:choose>
<xsl:when test="cbc:ResponseCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ResponseCode != ''</Pattern>
<Description>[F-APR015] Invalid ResponseCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ReferenceID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ReferenceID != ''</Pattern>
<Description>[F-APR016] Invalid ReferenceID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:Response/cbc:ResponseCode" priority="3997" mode="M16">
<xsl:choose>
<xsl:when test="./@listID = 'urn:oioubl:codelist:responsecode-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@listID = 'urn:oioubl:codelist:responsecode-1.1'</Pattern>
<Description>[F-APR017] Invalid listID. Must be 'urn:oioubl:codelist:responsecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test=". = 'TechnicalReject' or . = 'ProfileReject' or . = 'BusinessReject' or . = 'BusinessAccept'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>. = 'TechnicalReject' or . = 'ProfileReject' or . = 'BusinessReject' or . = 'BusinessAccept'</Pattern>
<Description>[F-APR018] Invalid ResponseCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:Response/cbc:Description" priority="3996" mode="M16">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-APR019] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-APR020] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:DocumentReference" priority="3995" mode="M16">
<xsl:choose>
<xsl:when test="count(cbc:DocumentType) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cbc:DocumentType) = 0</Pattern>
<Description>[F-APR021] DocumentType element must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:DocumentTypeCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentTypeCode != ''</Pattern>
<Description>[F-APR024] Invalid DocumentTypeCode. Must contain a value</Description>
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
<Description>[F-APR025] Invalid ID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-APR026] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:choose>
<xsl:when test="cbc:DocumentTypeCode/@listID = 'urn:oioubl:codelist:responsedocumenttypecode-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentTypeCode/@listID = 'urn:oioubl:codelist:responsedocumenttypecode-1.1'</Pattern>
<Description>[W-APR027] Invalid listID. Must be 'urn:oioubl:codelist:responsedocumenttypecode-1.1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:DocumentTypeCode/@listAgencyID = '320'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentTypeCode/@listAgencyID = '320'</Pattern>
<Description>[W-APR043] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:DocumentTypeCode = 'ApplicationResponse' or cbc:DocumentTypeCode = 'Cataloque' or cbc:DocumentTypeCode = 'CataloqueDeletion' or cbc:DocumentTypeCode = 'CataloqueItemSpecificationUpdate' or cbc:DocumentTypeCode = 'CataloquePricingUpdate' or cbc:DocumentTypeCode = 'CataloqueRequest' or cbc:DocumentTypeCode = 'CreditNote' or cbc:DocumentTypeCode = 'Invoice' or cbc:DocumentTypeCode = 'Order' or cbc:DocumentTypeCode = 'OrderCancellation' or cbc:DocumentTypeCode = 'OrderChange' or cbc:DocumentTypeCode = 'OrderResponse' or cbc:DocumentTypeCode = 'OrderResponseSimple' or cbc:DocumentTypeCode = 'Reminder' or cbc:DocumentTypeCode = 'Statement'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:DocumentTypeCode = 'ApplicationResponse' or cbc:DocumentTypeCode = 'Cataloque' or cbc:DocumentTypeCode = 'CataloqueDeletion' or cbc:DocumentTypeCode = 'CataloqueItemSpecificationUpdate' or cbc:DocumentTypeCode = 'CataloquePricingUpdate' or cbc:DocumentTypeCode = 'CataloqueRequest' or cbc:DocumentTypeCode = 'CreditNote' or cbc:DocumentTypeCode = 'Invoice' or cbc:DocumentTypeCode = 'Order' or cbc:DocumentTypeCode = 'OrderCancellation' or cbc:DocumentTypeCode = 'OrderChange' or cbc:DocumentTypeCode = 'OrderResponse' or cbc:DocumentTypeCode = 'OrderResponseSimple' or cbc:DocumentTypeCode = 'Reminder' or cbc:DocumentTypeCode = 'Statement'</Pattern>
<Description>[F-APR028] Invalid DocumentTypeCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:DocumentReference/cac:Attachment" priority="3994" mode="M16">
<xsl:if test="cbc:EmbeddedDocumentBinaryObject and cac:ExternalReference">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:EmbeddedDocumentBinaryObject and cac:ExternalReference</Pattern>
<Description>[F-APR045] Use either EmbeddedDocumentBinaryObject or ExternalReference</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:DocumentReference/cac:Attachment/cbc:EmbeddedDocumentBinaryObject" priority="3993" mode="M16">
<xsl:choose>
<xsl:when test="./@mimeCode='image/tiff' or ./@mimeCode='image/png' or ./@mimeCode='image/jpeg' or ./@mimeCode='image/gif' or ./@mimeCode='application/pdf'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@mimeCode='image/tiff' or ./@mimeCode='image/png' or ./@mimeCode='image/jpeg' or ./@mimeCode='image/gif' or ./@mimeCode='application/pdf'</Pattern>
<Description>[F-APR044] Attribute mimeCode must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:DocumentReference/cac:Attachment/cac:ExternalReference" priority="3992" mode="M16">
<xsl:choose>
<xsl:when test="cbc:URI != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:URI != ''</Pattern>
<Description>[F-APR046] Invalid URI. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty" priority="3991" mode="M16">
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
<Description>[F-APR047] Invalid EndpointID. Must contain a value</Description>
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
<Description>[F-APR048] One PartyLegalEntity class must be present</Description>
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PartyIdentification" priority="3990" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PartyName" priority="3989" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PostalAddress" priority="3988" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PhysicalLocation" priority="3987" mode="M16">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3986" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3985" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PhysicalLocation/cac:Address" priority="3984" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PartyTaxScheme" priority="3983" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3982" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:PartyLegalEntity" priority="3981" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:Contact" priority="3980" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:IssuerParty/cac:Person" priority="3979" mode="M16">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty" priority="3978" mode="M16">
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
<Description>[F-APR049] Invalid EndpointID. Must contain a value</Description>
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
<Description>[F-APR050] One PartyLegalEntity class must be present</Description>
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PartyIdentification" priority="3977" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PartyName" priority="3976" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PostalAddress" priority="3975" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PhysicalLocation" priority="3974" mode="M16">
<xsl:if test="(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:ID) or cbc:ID = '') and (count(cac:Address) = 0)</Pattern>
<Description>[F-LIB221] If ID not specified, Address is mandatory</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PhysicalLocation/cac:ValidityPeriod" priority="3973" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PhysicalLocation/cac:ValidityPeriod/cbc:Description" priority="3972" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PhysicalLocation/cac:Address" priority="3971" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PartyTaxScheme" priority="3970" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PartyTaxScheme/cac:TaxScheme" priority="3969" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:PartyLegalEntity" priority="3968" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:Contact" priority="3967" mode="M16">
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
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:RecipientParty/cac:Person" priority="3966" mode="M16">
<xsl:if test="(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>(not(cbc:FamilyName) or cbc:FamilyName = '') and (not(cbc:FirstName) or cbc:FirstName = '')</Pattern>
<Description>[F-LIB024] There must be a FirstName if the FamilyName is not present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:LineResponse" priority="3965" mode="M16">
<xsl:if test="count(cac:Response) &gt; 1">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:Response) &gt; 1</Pattern>
<Description>[F-APR051] No more than one Response class may be present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:LineResponse/cac:LineReference" priority="3964" mode="M16">
<xsl:choose>
<xsl:when test="count(cac:DocumentReference) = 0" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(cac:DocumentReference) = 0</Pattern>
<Description>[F-APR029] DocumentReference class must be excluded</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:LineID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:LineID != ''</Pattern>
<Description>[F-APR030] Invalid LineID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:if test="cbc:UUID and not(string-length(string(cbc:UUID)) = 36)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:UUID and not(string-length(string(cbc:UUID)) = 36)</Pattern>
<Description>[F-APR031] Invalid UUID. Must be of this form '6E09886B-DC6E-439F-82D1-7CCAC7F4E3B1'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:LineResponse/cac:Response" priority="3963" mode="M16">
<xsl:choose>
<xsl:when test="cbc:ResponseCode != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ResponseCode != ''</Pattern>
<Description>[F-APR032] Invalid ResponseCode. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="cbc:ReferenceID != ''" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>cbc:ReferenceID != ''</Pattern>
<Description>[F-APR033] Invalid ReferenceID. Must contain a value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:LineResponse/cac:Response/cbc:ResponseCode" priority="3962" mode="M16">
<xsl:choose>
<xsl:when test="./@listID = 'urn:oioubl:codelist:lineresponsecode-1.1'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>./@listID = 'urn:oioubl:codelist:lineresponsecode-1.1'</Pattern>
<Description>[W-APR034] Invalid listID. Must be 'urn:oioubl:codelist:lineresponsecode-1.1'</Description>
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
<Description>[W-APR052] Invalid listAgencyID. Must be '320'</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test=". = 'BusinessReject' or . = 'BusinessAccept'" />
<xsl:otherwise>
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>. = 'BusinessReject' or . = 'BusinessAccept'</Pattern>
<Description>[F-APR035] Invalid ResponseCode. Must be a value from the codelist</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="doc:ApplicationResponse/cac:DocumentResponse/cac:LineResponse/cac:Response/cbc:Description" priority="3961" mode="M16">
<xsl:if test="count(../cbc:Description) &gt; 1 and not(./@languageID)">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>count(../cbc:Description) &gt; 1 and not(./@languageID)</Pattern>
<Description>[W-APR036] The attribute languageID should be used when more than one Description element is present</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:if test="local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID">
<Error>
<xsl:attribute name="context"><xsl:value-of select="concat(name(parent::*),'/',name())" /></xsl:attribute>
<Pattern>local-name(following-sibling::*) = local-name(current()) and following-sibling::*/@languageID = self::*/@languageID</Pattern>
<Description>[W-APR037] Multilanguage error. Replicated Description elements with same languageID attribute value</Description>
<Xpath><xsl:for-each select="ancestor::*">/<xsl:value-of select="name()" />[<xsl:value-of select="position()" />]</xsl:for-each>
</Xpath>
</Error>
</xsl:if>
<xsl:apply-templates mode="M16" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M16" />
<xsl:template match="text()" priority="-1" />
</xsl:stylesheet>
