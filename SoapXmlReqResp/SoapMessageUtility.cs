using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Net;
using System.Configuration;
using Microsoft.IT.RelationshipManagement.Interchange.Platform.Azure.Services.Common;
using System.Xml;

namespace SoapMessageUtility
{
    // Define a SOAP Extension that traces the SOAP request and SOAP
    // response for the XML Web service method the SOAP extension is
    // applied to.
    public class TraceExtension : SoapExtension
    {
        Stream oldStream;
        Stream newStream;
        string requestFilename;
        string responseFilename;
        LogType logTypeMode = LogType.None;
        // Save the Stream representing the SOAP request or SOAP response into
        // a local memory buffer.
        public override Stream ChainStream(Stream stream)
        {
            oldStream = stream;
            newStream = new MemoryStream();
            return newStream;
        }

        // When the SOAP extension is accessed for the first time, the XML Web
        // service method it is applied to is accessed to store the file
        // name passed in, using the corresponding SoapExtensionAttribute.    
        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return attribute;
        }

        // The SOAP extension was configured to run using a configuration file
        // instead of an attribute applied to a specific XML Web service
        // method.
        public override object GetInitializer(Type WebServiceType)
        {
            return null;
        }

        // Receive the file names,logtypemode stored by GetInitializer and store it in a
        // member variables for this specific instance.
        public override void Initialize(object initializer)
        {
            try
            {
                //Web.config/App.config entries override the TraceExtensionAttribute instantiation parameters values
                logTypeMode = string.IsNullOrEmpty(CloudConfiguration.GetConfigurationSettingValue("SoapExtnLogTypeMode")) ? logTypeMode : (LogType)Convert.ToInt32(CloudConfiguration.GetConfigurationSettingValue("SoapExtnLogTypeMode"));
                requestFilename = string.IsNullOrEmpty(CloudConfiguration.GetConfigurationSettingValue("SoapExtnRequestLogFile")) ? requestFilename : CloudConfiguration.GetConfigurationSettingValue("SoapExtnRequestLogFile");
                responseFilename = string.IsNullOrEmpty(CloudConfiguration.GetConfigurationSettingValue("SoapExtnResponseLogFile")) ? responseFilename : CloudConfiguration.GetConfigurationSettingValue("SoapExtnResponseLogFile");
            }
            catch (Exception ex)
            {
            }
        }

        //  If the SoapMessageStage is such that the SoapRequest or
        //  SoapResponse is still in the SOAP format to be sent or received,
        //  save it out to a file.
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:
                    WriteRequest(message);
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    WriteResponse(message);
                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
                default:
                    throw new Exception("invalid stage");
            }
        }

        public void WriteRequest(SoapMessage message)
        {
            var xDoc = new XmlDocument();
            try
            {
                if (logTypeMode.Equals(LogType.RequestOnly) || logTypeMode.Equals(LogType.RequestReponse))
                {
                    newStream.Position = 0;
                    TextReader textReader = new StreamReader(newStream);
                    var soapEnvelope = textReader.ReadToEnd();
                    xDoc.LoadXml(soapEnvelope);

                    foreach (XmlNode xmlNode in xDoc)
                        if (xmlNode.NodeType == XmlNodeType.XmlDeclaration)
                        {
                            xDoc.RemoveChild(xmlNode);
                            break;
                        }
                    
                    DataAccess.AddSoapRequestResponse(connectionString: InterchangeConfiguration.SoapInterchangeDBConnectionString, direction: "Request", soapEnvelope: xDoc.OuterXml);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (xDoc != null)
                    xDoc.RemoveAll();
                xDoc = null;
                newStream.Position = 0;
                Copy(newStream, oldStream);
            }
        }

        public void WriteResponse(SoapMessage message)
        {
            var xDoc = new XmlDocument();
            try
            {
                Copy(oldStream, newStream);
                
                if (logTypeMode.Equals(LogType.ResponseOnly) || logTypeMode.Equals(LogType.RequestReponse))
                {
                    newStream.Position = 0;
                    TextReader textReader = new StreamReader(newStream);
                    var soapEnvelope = textReader.ReadToEnd();    
                    xDoc.LoadXml(soapEnvelope);

                    foreach (XmlNode xmlNode in xDoc)
                        if (xmlNode.NodeType == XmlNodeType.XmlDeclaration)
                        {
                            xDoc.RemoveChild(xmlNode);
                            break;
                        }

                    DataAccess.AddSoapRequestResponse(connectionString: InterchangeConfiguration.SoapInterchangeDBConnectionString, direction: "Response", soapEnvelope: xDoc.OuterXml);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (xDoc != null)
                    xDoc.RemoveAll();
                xDoc = null;
                newStream.Position = 0;
            }
        }

        void Copy(Stream from, Stream to)
        {
            TextReader reader = new StreamReader(from);
            TextWriter writer = new StreamWriter(to);
            writer.WriteLine(reader.ReadToEnd());
            writer.Flush();
        }
    }
}
