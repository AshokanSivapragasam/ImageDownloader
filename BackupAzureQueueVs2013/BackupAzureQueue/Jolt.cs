using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace BackupAzureQueue
{
    /// <summary>
    /// Json object language transformation using json stylesheet
    /// </summary>
    public class Jolt
    {
        /// <summary>
        /// Transform input json to desired output form using json stylesheet
        /// </summary>
        /// <param name="inputJsonText"></param>
        /// <param name="jsonTransformLangaugeText"></param>
        /// <returns></returns>
        public static string TransformJsonbyJstl(string inputJsonText, string jsonTransformLangaugeText)
        {
            // 0. Init
            var outputJsonText = string.Empty;
            var intermediaryXmlTransformLangaugeText = string.Empty;
            var intermediaryInputXmlText = string.Empty;
            var intermediaryOutputXmlText = string.Empty;
            var intermediaryXmlDocument = new XmlDocument();
            XsltSession xsltSession = null;
            try
            {
                // 1. Convert input json to xml
                intermediaryInputXmlText = JsonConvert.DeserializeXmlNode(inputJsonText).OuterXml;

                // 2. Send input xml against xsl
                xsltSession = XsltSessionManager.CreateOrGetSession(jsonTransformLangaugeText);
                intermediaryOutputXmlText = xsltSession.xslt.TransformerXmlByXsl(intermediaryInputXmlText);

                // 3. Convert output xml to json
                intermediaryXmlDocument.LoadXml(intermediaryOutputXmlText);
                outputJsonText = JsonConvert.SerializeXmlNode(intermediaryXmlDocument);
            }
            finally
            {
                #region RELEASING_RESOURCES_TO_PREVENT_ANY_.NET_MEMORY_LEAKAGE
                if (xsltSession != null)
                    xsltSession = null;

                if (intermediaryXmlDocument != null)
                {
                    intermediaryXmlDocument.RemoveAll();
                    intermediaryXmlDocument = null;
                }

                outputJsonText = null;
                intermediaryXmlTransformLangaugeText = null;
                intermediaryInputXmlText = null;
                intermediaryOutputXmlText = null;
                #endregion
            }

            // 6. Return it
            return outputJsonText;
        }
    }

    /// <summary>
    /// Xslt library with advanced, compiled xslt object. This helps to improve performance as xsl file is already compiled once at the startup.
    /// </summary>
    public class Xslt
    {
        /// <summary>
        /// An advanced, compiled xslt object
        /// </summary>
        XslCompiledTransform xslCompiledTransform;

        /// <summary>
        /// Ctor
        /// </summary>
        public Xslt()
        {
        }

        /// <summary>
        /// Ctor(with json stylesheet text)
        /// </summary>
        /// <param name="xmlTransformLangaugeText"></param>
        public Xslt(string xslFileContent)
        {
            var sxslWriter = new StringReader(xslFileContent);
            var xslReader = XmlReader.Create(sxslWriter);
            xslCompiledTransform = new XslCompiledTransform();
            xslCompiledTransform.Load(xslReader, XsltSettings.TrustedXslt, new XmlUrlResolver());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlText"></param>
        public string TransformerXmlByXsl(string xmlInputData)
        {
            using (XmlReader xReader = XmlReader.Create(new MemoryStream(Encoding.UTF8.GetBytes(xmlInputData))))
            using (StringWriter sWriter = new StringWriter())
            using (XmlWriter xWriter = XmlWriter.Create(sWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                xslCompiledTransform.Transform(xReader, xWriter);
                return sWriter.ToString();
            }
        }
    }

    /// <summary>
    /// It optimizes the sessions by following per-user-per-session service model. It improves as sessions are getting created once at the startup. It avoids any possible memory leaks as well.
    /// </summary>
    public class XsltSessionManager
    {
        /// <summary>
        /// Static dictionary to keep track client sessions
        /// </summary>
        public static Dictionary<string, XsltSession> xsltSessions = new Dictionary<string, XsltSession>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonTransformLangaugeText"></param>
        /// <returns></returns>
        public static XsltSession CreateOrGetSession(string jsonTransformLangaugeText)
        {
            var _currentMd5HashOfXslFileContent = GetMd5HashForXslFileContent(jsonTransformLangaugeText);

            #region SOAP_CONNECTOR_SESSIONS_MANAGER
            if (!xsltSessions.ContainsKey(_currentMd5HashOfXslFileContent))
            {
                var _xslt = new BackupAzureQueue.Xslt(JsonConvert.DeserializeXmlNode(jsonTransformLangaugeText).OuterXml);
                xsltSessions.Add(_currentMd5HashOfXslFileContent, new XsltSession
                {
                    currentMd5HashOfXslFileContent = _currentMd5HashOfXslFileContent,
                    xslt = _xslt
                });
            }
            #endregion

            return xsltSessions[_currentMd5HashOfXslFileContent];
        }

        /// <summary>
        /// Get Md5 Hash For XslFileContent
        /// </summary>
        /// <param name="inputTextToHash"></param>
        /// <returns></returns>
        public static string GetMd5HashForXslFileContent(string inputTextToHash)
        {
            //Calculate MD5 hash. This requires that the string is splitted into a byte[].
            var md5 = new MD5CryptoServiceProvider();
            var inputTextToHashBytes = Encoding.Default.GetBytes(inputTextToHash);
            var result = md5.ComputeHash(inputTextToHashBytes);

            //Convert result back to string.
            return BitConverter.ToString(result);

        }
    }

    /// <summary>
    /// Model class for Xslt Session
    /// </summary>
    public class XsltSession
    {
        /// <summary>
        /// Current Md5 checksum hash
        /// </summary>
        public string currentMd5HashOfXslFileContent { get; set; }

        /// <summary>
        /// Instance of xslt
        /// </summary>
        public Xslt xslt { get; set; }
    }
}
