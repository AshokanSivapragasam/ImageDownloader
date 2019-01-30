using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Net;
using System.Configuration;

namespace SoapMessageUtility
{
    // Define a SOAP Extension that traces the SOAP request and SOAP
    // response for the XML Web service method the SOAP extension is
    // applied to.
    public class TraceExtension : SoapExtension
    {
        Stream _oldStream;
        Stream _newStream;
        string _reqFilename = @"D:\Usr\Ashok\Projects\SOAPMessageLogging\log\ReqLog.txt";
        string _resFilename = @"D:\Usr\Ashok\Projects\SOAPMessageLogging\log\ResLog.txt";
        LogType _logTypeMode = LogType.None;
        // Save the Stream representing the SOAP request or SOAP response into
        // a local memory buffer.
        public override Stream ChainStream(Stream stream)
        {
            _oldStream = stream;
            _newStream = new MemoryStream();
            return _newStream;
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
                //_logTypeMode = ((TraceExtensionAttribute)initializer).LogTypeMode;

                //_reqFilename = string.IsNullOrEmpty(((TraceExtensionAttribute)initializer).ReqFileName) ? _reqFilename : ((TraceExtensionAttribute)initializer).ReqFileName;
                //_resFilename = string.IsNullOrEmpty(((TraceExtensionAttribute)initializer).ResFileName) ? _resFilename : ((TraceExtensionAttribute)initializer).ResFileName;

                //Web.config/App.config entries override the TraceExtensionAttribute instantiation parameters values
                _logTypeMode = string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogTypeMode"]) ? _logTypeMode : (LogType)Convert.ToInt32(ConfigurationManager.AppSettings["LogTypeMode"]);
                _reqFilename = string.IsNullOrEmpty(ConfigurationManager.AppSettings["REQ_LOGFILE"]) ? _reqFilename : ConfigurationManager.AppSettings["REQ_LOGFILE"];
                _resFilename = string.IsNullOrEmpty(ConfigurationManager.AppSettings["RES_LOGFILE"]) ? _resFilename : ConfigurationManager.AppSettings["RES_LOGFILE"];
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
                    WriteOutput(message);
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    WriteInput(message);
                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
                default:
                    throw new Exception("invalid stage");
            }
        }

        public void WriteOutput(SoapMessage message)
        {
            FileStream fs;
            StreamWriter w = null; 
            
            try
            {
                if (_logTypeMode.Equals(LogType.RequestOnly) || _logTypeMode.Equals(LogType.RequestReponse))
                {
                    _newStream.Position = 0;
                    fs = new FileStream(_reqFilename, FileMode.Append,
                        FileAccess.Write);
                    w = new StreamWriter(fs);

                    string soapString = "SoapRequest";
                    w.WriteLine("-----" + soapString + " at " + DateTime.Now);
                    w.Flush();
                    Copy(_newStream, fs);
                    w.Close();

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (w != null)
                    w.Close();
                _newStream.Position = 0;
                Copy(_newStream, _oldStream);
            }
        }

        public void WriteInput(SoapMessage message)
        {
            FileStream fs;
            StreamWriter w = null; 
            try
            {
                Copy(_oldStream, _newStream);
                
                if (_logTypeMode.Equals(LogType.ResponseOnly) || _logTypeMode.Equals(LogType.RequestReponse))
                {
                    fs = new FileStream(_resFilename, FileMode.Append,
                        FileAccess.Write);
                    w = new StreamWriter(fs);

                    string soapString = "SoapResponse";
                    w.WriteLine("-----" + soapString +
                        " at " + DateTime.Now);
                    w.Flush();
                    _newStream.Position = 0;
                    Copy(_newStream, fs);
                    
                   
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (w != null)
                    w.Close();
                _newStream.Position = 0;
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
