<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <body>
        <H1>
          <xsl:value-of select="error/header"/>
        </H1>
        <p>
          <xsl:value-of select="error/description"/>
        </p>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>