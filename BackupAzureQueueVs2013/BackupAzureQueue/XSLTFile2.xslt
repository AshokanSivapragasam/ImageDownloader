<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:TriggeredRequestNs="http://schemas.datacontract.org/2004/07/Microsoft.IT.RelationshipManagement.Interchange.Email.Common.Core" xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns:mslib="http://microsoft.com/EIvNext/libraries" version="1.0">
  <xsl:output method="xml" indent="no" omit-xml-declaration="yes" encoding="iso-8859-1" />
  <msxsl:script language="C#" implements-prefix="mslib">
    <msxsl:using namespace="System.Globalization" />
    <msxsl:using namespace="System" />
    <msxsl:using namespace="System.Xml" />
    <msxsl:using namespace="System.Text.RegularExpressions" />

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
       </msxsl:script>
  <xsl:template match="/">
    <xsl:element name="TriggeredRequest">
      <xsl:apply-templates />
    </xsl:element>
  </xsl:template>
  <xsl:template match="TriggeredRequestNs:TriggeredRequestBase">
    <xsl:element name="Type">
      <xsl:text>Regsys</xsl:text>
    </xsl:element>
    <xsl:element name="TenantName">
      <xsl:text>#TenantName#</xsl:text>
    </xsl:element>
    <xsl:element name="EnterpriseAccountId">
      <xsl:text>#VarEnterpriseAccountId#</xsl:text>
    </xsl:element>
    <xsl:element name="SubsidiaryAccountId">
      <xsl:value-of select="TriggeredRequestNs:AccountId" />
    </xsl:element>
    <xsl:element name="ConversationId">
      <xsl:choose>
        <xsl:when test="TriggeredRequestNs:ConversationId != ''">
          <xsl:value-of select="TriggeredRequestNs:ConversationId" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:value-of select="TriggeredRequestNs:Subscribers/TriggeredRequestNs:SubscriberBase/TriggeredRequestNs:SubscriberKey" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:element>
    <xsl:element name="TriggeredSendDefinitionExternalKey">
      <xsl:choose>
        <xsl:when test="TriggeredRequestNs:CustomerKey != ''">
          <xsl:value-of select="TriggeredRequestNs:CustomerKey" />
        </xsl:when>
        <xsl:otherwise>
          <xsl:text>TBN_</xsl:text>
          <xsl:value-of select="TriggeredRequestNs:CommunicationId" />
        </xsl:otherwise>
      </xsl:choose>
    </xsl:element>
    <xsl:variable name="varCommunicationId" select="TriggeredRequestNs:CommunicationId" />
    <xsl:choose>
      <xsl:when test="count(TriggeredRequestNs:Subscribers/TriggeredRequestNs:SubscriberBase) &lt;= 1">
        <xsl:element name="Subscribers">#RemoveIt#</xsl:element>
      </xsl:when>
    </xsl:choose>
    <xsl:for-each select="TriggeredRequestNs:Subscribers/TriggeredRequestNs:SubscriberBase">
      <xsl:element name="Subscribers">
        <xsl:element name="EmailAddress">
          <xsl:value-of select="TriggeredRequestNs:EmailAddress" />
        </xsl:element>
        <xsl:element name="SubscriberKey">
          <xsl:value-of select="TriggeredRequestNs:SubscriberKey" />
        </xsl:element>
        <xsl:for-each select="TriggeredRequestNs:Attributes/TriggeredRequestNs:Attribute">
          <xsl:element name="Attributes">
            <xsl:element name="Key">
              <xsl:value-of select="TriggeredRequestNs:Name" />
            </xsl:element>
            <xsl:element name="Value">
              <xsl:choose>
                <xsl:when test="string(./TriggeredRequestNs:Value)">
                  <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:Value, '&quot;', '&quot;')" />
                </xsl:when>
                <xsl:otherwise>
                  <xsl:text>#EmptyValue#</xsl:text>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:element>
          </xsl:element>
        </xsl:for-each>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>CommID</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:value-of select="$varCommunicationId" />
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>FirstName</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:choose>
              <xsl:when test="string(./TriggeredRequestNs:FirstName)">
                <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:FirstName, '&quot;', '&quot;')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>#EmptyValue#</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>MiddleName</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:choose>
              <xsl:when test="string(./TriggeredRequestNs:MiddleName)">
                <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:MiddleName, '&quot;', '&quot;')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>#EmptyValue#</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>LastName1</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:choose>
              <xsl:when test="string(./TriggeredRequestNs:LastName1)">
                <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:LastName1, '&quot;', '&quot;')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>#EmptyValue#</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>LastName2</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:choose>
              <xsl:when test="string(./TriggeredRequestNs:LastName2)">
                <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:LastName2, '&quot;', '&quot;')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>#EmptyValue#</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>NamePrefix</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:choose>
              <xsl:when test="string(./TriggeredRequestNs:NamePrefix)">
                <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:NamePrefix, '&quot;', '&quot;')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>#EmptyValue#</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>NameSuffix</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:choose>
              <xsl:when test="string(./TriggeredRequestNs:NameSuffix)">
                <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:NameSuffix, '&quot;', '&quot;')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>#EmptyValue#</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>RegistrationDate</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:value-of select="TriggeredRequestNs:RegistrationDate" />
          </xsl:element>
        </xsl:element>
        <xsl:element name="Attributes">
          <xsl:element name="Key">
            <xsl:text>CustomerAnswerXML</xsl:text>
          </xsl:element>
          <xsl:element name="Value">
            <xsl:choose>
              <xsl:when test="string(./TriggeredRequestNs:WizardData)">
                <xsl:value-of select="mslib:ReplaceAll(TriggeredRequestNs:WizardData, '&quot;', '&quot;')" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>#EmptyValue#</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:element>
        </xsl:element>
      </xsl:element>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>