<?xml version="1.0" encoding="UTF-16" standalone="yes"?>
<xsl:stylesheet version="1.0" com:dummy-for-xmlns="" pie:dummy-for-xmlns="" pcm:dummy-for-xmlns="" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:sch="http://www.ascc.net/xml/schematron" xmlns:com="http://rep.oio.dk/ubl/xml/schemas/0p71/common/" xmlns:pie="http://rep.oio.dk/ubl/xml/schemas/0p71/pie/" xmlns:pcm="http://rep.oio.dk/ubl/xml/schemas/0p71/pcm/">
<xsl:output method="xml" />
<xsl:template match="*|@*" mode="schematron-get-full-path">
<xsl:apply-templates select="parent::*" mode="schematron-get-full-path" />
<xsl:text>/</xsl:text>
<xsl:if test="count(. | ../@*) = count(../@*)">@</xsl:if>
<xsl:value-of select="name()" />
<xsl:text>[</xsl:text>
<xsl:value-of select="1+count(preceding-sibling::*[name()=name(current())])" />
<xsl:text>]</xsl:text>
</xsl:template>
<xsl:template match="/">
<schematron>
<xsl:apply-templates select="/" mode="M4" /><xsl:apply-templates select="/" mode="M5" /><xsl:apply-templates select="/" mode="M6" /><xsl:apply-templates select="/" mode="M7" /><xsl:apply-templates select="/" mode="M8" /><xsl:apply-templates select="/" mode="M9" /><xsl:apply-templates select="/" mode="M10" /><xsl:apply-templates select="/" mode="M11" /></schematron>
</xsl:template>
<xsl:template match="*[@schemeID]" priority="4000" mode="M4">
<xsl:if test="@schemeID='EAN' and string-length(.) != 13">
<error>In pattern @schemeID='EAN' and string-length(.) != 13:
   WARNING: EAN numbers are 13 digits in length
</error></xsl:if>
<xsl:if test="@schemeID='EAN' and . != (. + 1) - 1">
<error>In pattern @schemeID='EAN' and . != (. + 1) - 1:
   WARNING: EAN numbers are 13 digits in length
</error></xsl:if>
<xsl:if test="@schemeID='EAN' and substring(.,13,1)!=0 and ((((10 - substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1)) + ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) - ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) != substring(.,13,1) )">
<error>In pattern @schemeID='EAN' and substring(.,13,1)!=0 and ((((10 - substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1)) + ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) - ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) != substring(.,13,1) ):
   there is an improperly formatted EAN number.
</error></xsl:if>
<xsl:if test="@schemeID='EAN' and substring(.,13,1) =0 and substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1) != 0">
<error>In pattern @schemeID='EAN' and substring(.,13,1) =0 and substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1) != 0:
   there is an improperly formatted EAN number.
</error></xsl:if>
<xsl:if test="@schemeID='CVR' and string-length(.) != 8">
<error>In pattern @schemeID='CVR' and string-length(.) != 8:
   WARNING: CVR numbers are 8 digits in length
</error></xsl:if>
<xsl:if test="@schemeID='CVR' and . != (. + 1) - 1">
<error>In pattern @schemeID='CVR' and . != (. + 1) - 1:
   WARNING: CVR numbers are 8 digits in length
</error></xsl:if>
<xsl:if test="@schemeID='SE' and string-length(.) != 8">
<error>In pattern @schemeID='SE' and string-length(.) != 8:
   WARNING: SE numbers are 8 digits in length
</error></xsl:if>
<xsl:if test="@schemeID='SE' and . != (. + 1) - 1">
<error>In pattern @schemeID='SE' and . != (. + 1) - 1:
   WARNING: SE numbers are 8 digits in length
</error></xsl:if>
<xsl:apply-templates mode="M4" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M4" />
<!--
  <xsl:template match="/*[local-name()='Invoice']/com:BuyersReferenceID[substring(.,13,1)!=0]" priority="4000" mode="M5">
<xsl:if test="((((10 - substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1)) + ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) - ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) != substring(.,13,1) )">
<error>In pattern ((((10 - substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1)) + ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) - ((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3))) != substring(.,13,1) ):
   BuyersReferenceID is not formatted as a proper EAN number.
</error></xsl:if>
<xsl:apply-templates mode="M5" />
</xsl:template>
<xsl:template match="/*/com:BuyersReferenceID[substring(.,13,1)=0]" priority="3999" mode="M5">
<xsl:if test="substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1) != 0">
<error>In pattern substring((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3),string-length((substring(.,1,1) * 1 + substring(.,2,1) * 3) + (substring(.,3,1) * 1 + substring(.,4,1) * 3) + (substring(.,5,1) * 1 + substring(.,6,1) * 3) + (substring(.,7,1) * 1 + substring(.,8,1) * 3) + (substring(.,9,1) * 1 + substring(.,10,1) * 3) + (substring(.,11,1) * 1 + substring(.,12,1) * 3)),1) != 0:
   BuyersReferenceID is not formatted as a proper EAN number.
</error></xsl:if>
<xsl:apply-templates mode="M5" />
</xsl:template>
-->
<xsl:template match="text()" priority="-1" mode="M5" />
<xsl:template match="/pie:Invoice/com:PaymentMeans | /pcm:Invoice/com:PaymentMeans" priority="4000" mode="M6">
<xsl:if test="com:TypeCodeID=01 and com:PaymentID &gt; 0">
<error>In pattern com:TypeCodeID=01 and com:PaymentID &gt; 0:
   IF com:TypeCodeID under PaymentMeans = 01 then com:PaymentID under PaymentMeans should not be found or should equal 0
</error></xsl:if>
<xsl:if test="com:TypeCodeID='null' and com:PaymentChannelCode='INDBETALINGSKORT'">
<error>In pattern com:TypeCodeID='null' and com:PaymentChannelCode='INDBETALINGSKORT':
   If PaymentChannelCode is "KONTOOVERFØRSEL" or "DIRECT DEBET" then com:TypeCodeID = "null"
</error></xsl:if>
<xsl:if test="com:TypeCodeID=15 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=15 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 15 then com:PaymentID under PaymentMeans should be a number of 16 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=04 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=04 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 04 then com:PaymentID under PaymentMeans should be a number of 16 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=75 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=75 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 75 then com:PaymentID under PaymentMeans should be a number of 16 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=71 and string-length(com:PaymentID) != 15 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=71 and string-length(com:PaymentID) != 15 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 71 then com:PaymentID under PaymentMeans should be a number of 15 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=73 and com:PaymentID &gt; 0">
<error>In pattern com:TypeCodeID=73 and com:PaymentID &gt; 0:
   IF com:TypeCodeID under PaymentMeans = 73 then com:PaymentID under PaymentMeans should not be found or should equal 0
</error></xsl:if>
<xsl:if test="com:TypeCodeID=71 and com:PayeeFinancialAccount">
<error>In pattern com:TypeCodeID=71 and com:PayeeFinancialAccount:
   When TypeCodeID under PaymentMeans = 71 then there should not be a com:PayeeFinancialAccount under PaymentMeans
</error></xsl:if>
<xsl:if test="com:TypeCodeID=73 and com:PayeeFinancialAccount">
<error>In pattern com:TypeCodeID=73 and com:PayeeFinancialAccount:
   When TypeCodeID under PaymentMeans = 73 then there should not be a com:PayeeFinancialAccount under PaymentMeans
</error></xsl:if>
<xsl:if test="com:TypeCodeID=75 and com:PayeeFinancialAccount">
<error>In pattern com:TypeCodeID=75 and com:PayeeFinancialAccount:
   When TypeCodeID under PaymentMeans = 75 then there should not be a com:PayeeFinancialAccount under PaymentMeans
</error></xsl:if>
<xsl:if test="com:TypeCodeID=04 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PayeeFinancialAccount))">
<error>In pattern com:TypeCodeID=04 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PayeeFinancialAccount)):
   WARNING: When com:TypeCodeID under PaymentMeans = 04 then all classes and fields under PaymentMeans other than com:JointPaymentID and com:PaymentAdvice are considered to be required
</error></xsl:if>
<xsl:if test="com:TypeCodeID=15 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PayeeFinancialAccount))">
<error>In pattern com:TypeCodeID=15 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PayeeFinancialAccount)):
   WARNING: When com:TypeCodeID under PaymentMeans = 15 then all classes and fields under PaymentMeans other than com:JointPaymentID and com:PaymentAdvice are considered to be required
</error></xsl:if>
<xsl:if test="com:TypeCodeID=75 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PaymentID) or not(com:JointPaymentID) or not(com:PaymentAdvice))">
<error>In pattern com:TypeCodeID=75 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PaymentID) or not(com:JointPaymentID) or not(com:PaymentAdvice)):
   WARNING: When com:TypeCodeID under PaymentMeans = 75 then all classes and fields under PaymentMeans except com:PayeeFinancialAccount are considered to be required
</error></xsl:if>
<xsl:if test="com:TypeCodeID=73 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:JointPaymentID) or not(com:PaymentAdvice))">
<error>In pattern com:TypeCodeID=73 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:JointPaymentID) or not(com:PaymentAdvice)):
   WARNING: When com:TypeCodeID under PaymentMeans = 73 then all classes and fields under PaymentMeans except com:PayeeFinancialAccount and com:PaymentID are considered to be required
</error></xsl:if>
<xsl:if test="com:TypeCodeID=71 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PaymentID) or not(com:JointPaymentID))">
<error>In pattern com:TypeCodeID=71 and (not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PaymentID) or not(com:JointPaymentID)):
   WARNING: When com:TypeCodeID under PaymentMeans = 71 then all fields under PaymentMeans but none of the classes are considered to be required
</error></xsl:if>
<xsl:apply-templates mode="M6" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M6" />
<xsl:template match="/*[local-name()='Invoice']/com:PaymentMeans" priority="4000" mode="M7">
<xsl:if test="com:TypeCodeID='null' and com:PaymentChannelCode='INDBETALINGSKORT'">
<error>In pattern com:TypeCodeID='null' and com:PaymentChannelCode='INDBETALINGSKORT':
   If PaymentChannelCode is "KONTOOVERFØRSEL" or "DIRECT DEBET" then com:TypeCodeID = "null"
</error></xsl:if>
<xsl:if test="com:TypeCodeID=15 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=15 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 15 then com:PaymentID under PaymentMeans should be a number of 16 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=04 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=04 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 04 then com:PaymentID under PaymentMeans should be a number of 16 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=75 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=75 and string-length(com:PaymentID) != 16 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 75 then com:PaymentID under PaymentMeans should be a number of 16 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=71 and string-length(com:PaymentID) != 15 and com:PaymentID != (com:PaymentID + 1) - 1">
<error>In pattern com:TypeCodeID=71 and string-length(com:PaymentID) != 15 and com:PaymentID != (com:PaymentID + 1) - 1:
   IF com:TypeCodeID under PaymentMeans = 71 then com:PaymentID under PaymentMeans should be a number of 15 digits in length
</error></xsl:if>
<xsl:if test="com:TypeCodeID=01 and ( not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PayeeFinancialAccount) or not(com:PaymentAdvice))">
<error>In pattern com:TypeCodeID=01 and ( not(com:PaymentDueDate) or not(com:PaymentChannelCode) or not(com:PayeeFinancialAccount) or not(com:PaymentAdvice)):
   WARNING: When com:TypeCodeID under PaymentMeans = 01 then all classes and fields under PaymentMeans other than com:JointPaymentID and com:PaymentID are considered to be required
</error></xsl:if>
<xsl:apply-templates mode="M7" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M7" />
<xsl:template match="com:PayeeFinancialAccount" priority="4000" mode="M8">
<xsl:choose>
<xsl:when test="com:TypeCode = 'null' or com:TypeCode = 'BANK' or com:TypeCode = 'GIRO' or com:TypeCode = 'KREDITORNR' or com:TypeCode = 'FIK' or com:TypeCode = 'BANKGIROT' or com:TypeCode = 'POSTGIROT' or com:TypeCode = 'IBAN'" />
<xsl:otherwise>
<error>In pattern com:TypeCode = 'null' or com:TypeCode = 'BANK' or com:TypeCode = 'GIRO' or com:TypeCode = 'KREDITORNR' or com:TypeCode = 'FIK' or com:TypeCode = 'BANKGIROT' or com:TypeCode = 'POSTGIROT' or com:TypeCode = 'IBAN':
   <xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>should equal 'null' ,'BANK','GIRO','KREDITORNR','FIK','BANKGIROT','POSTGIROT', or 'IBAN'
</error></xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M8" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M8" />
<xsl:template match="com:AllowanceCharge" priority="4000" mode="M9">
<xsl:choose>
<xsl:when test="com:ID='Rabat' or com:ID='Gebyr' or com:ID='Fragt' or com:ID='Afgift' or com:ID='Told'" />
<xsl:otherwise>
<error>In pattern com:ID='Rabat' or com:ID='Gebyr' or com:ID='Fragt' or com:ID='Afgift' or com:ID='Told':
   <xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>should equal 'Rabat','Gebyr','Fragt','Afgift', or 'Told'.
</error></xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M9" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M9" />
<xsl:template match="com:InvoiceLine/com:Item/com:CommodityClassification/com:CommodityCode[@listID='UNSPSC']" priority="4000" mode="M10">
<xsl:if test="string-length(.) != 10 and string-length(.) != 8">
<error>In pattern string-length(.) != 10 and string-length(.) != 8:
   When the listID is UNSPSC the content of CommodityClassification should be following the UNSPSC recommendation with a 10 or 8 digit number
</error></xsl:if>
<xsl:if test=". != (. + 1) - 1">
<error>In pattern . != (. + 1) - 1:
   When the listID is UNSPSC the content of CommodityClassification should be following the UNSPSC recommendation with a 10 or 8 digit number
</error></xsl:if>
<xsl:apply-templates mode="M10" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M10" />
<xsl:template match="/*[local-name()='Invoice']/com:InvoiceLine/com:Item/com:Tax/com:TaxScheme/com:ID" priority="4000" mode="M11">
<xsl:choose>
<xsl:when test="string-length(substring-before(.,'-'))=4" />
<xsl:otherwise>
<error>In pattern string-length(substring-before(.,'-'))=4:
   com:ID under com:Item/com:Tax/com:TaxScheme/com:ID is a 'MomsAngivelsesParagraf' in the form of '2004-3-P.1.2.1'
</error></xsl:otherwise>
</xsl:choose>
<xsl:choose>
<xsl:when test="substring-before(.,'-') = (substring-before(.,'-') + 1) - 1 " />
<xsl:otherwise>
<error>In pattern substring-before(.,'-') = (substring-before(.,'-') + 1) - 1:
   com:ID under com:Item/com:Tax/com:TaxScheme/com:ID is a 'MomsAngivelsesParagraf' in the form of '2004-3-P.1.2.1'
</error></xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M11" />
</xsl:template>
<xsl:template match="/*[local-name()='Invoice']/com:InvoiceLine/com:Item/com:Tax/com:TypeCode" priority="3999" mode="M11">
<xsl:choose>
<xsl:when test=".='VAT' or .='ZERO-RATED'" />
<xsl:otherwise>
<error>In pattern .='VAT' or .='ZERO-RATED':
   <xsl:text xml:space="preserve"> </xsl:text>
<xsl:value-of select="name(.)" /><xsl:text xml:space="preserve"> </xsl:text>should equal VAT or ZERO-RATED.
</error></xsl:otherwise>
</xsl:choose>
<xsl:apply-templates mode="M11" />
</xsl:template>
<xsl:template match="text()" priority="-1" mode="M11" />
<xsl:template match="text()" priority="-1" />
</xsl:stylesheet>
