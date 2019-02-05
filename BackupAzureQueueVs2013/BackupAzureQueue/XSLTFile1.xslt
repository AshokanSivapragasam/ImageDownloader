<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:bgtlib="http://burning-glass.com/BGT.JobFeed/libraries" >
  <xsl:output method="xml" indent="yes" encoding="iso-8859-1" />
  
  <msxsl:script language="C#" implements-prefix="bgtlib">
		<msxsl:using namespace="System.Globalization"/>
		<msxsl:using namespace="System"/>
		<msxsl:using namespace="System.Xml"/>
		<msxsl:using namespace="System.Text.RegularExpressions"/>
		<![CDATA[  
				public string ReplaceAll(string data, string find, string replace)
				{
					try
					{
						return Regex.Replace(data, find, replace, RegexOptions.IgnoreCase);
					}
					catch
					{
						return data;
					}
				}
		   ]]></msxsl:script>
  
  <xsl:template match="/">
    <xsl:element name="row">
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>
  
  	<xsl:template match="*">
			
		<xsl:for-each select="@*|node()">
			<xsl:choose>
				<xsl:when test="local-name(.) = 'PostingHtml'">
					<xsl:apply-templates select="." />
				</xsl:when>
				<xsl:otherwise>
					<xsl:copy-of select="."/>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>
		<xsl:element name="JobFeedSource">
		  <xsl:text>kapow</xsl:text>
		</xsl:element>
		<xsl:element name="JobInsertDate">
		  <xsl:text/>
		</xsl:element>
  </xsl:template>

  <xsl:template match="PostingHtml">
	  <xsl:element name="PostingHtml">
		  <xsl:variable name="PostingHtmlInnerText">
				<xsl:value-of select="/row/PostingHtml"/>
		  </xsl:variable>
		  <xsl:value-of select="bgtlib:ReplaceAll($PostingHtmlInnerText,'&lt;meta name=&quot;description&quot;', '&lt;META NAME=&quot;Description&quot;')"/>
	  </xsl:element>
  </xsl:template>

</xsl:stylesheet>
