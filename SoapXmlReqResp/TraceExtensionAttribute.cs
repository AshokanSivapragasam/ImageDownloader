using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using System.Configuration;

namespace SoapMessageUtility
{
    // Create a SoapExtensionAttribute for the SOAP Extension that can be
    // applied to an XML Web service method.
    [AttributeUsage(AttributeTargets.Method)]
    public class TraceExtensionAttribute : SoapExtensionAttribute
    {
        private int priority;
        private LogType _logTypeMode = LogType.None;

        private string _reqFilename;
        private string _resFilename;

        public override Type ExtensionType
        {
            get { return typeof(TraceExtension); }
        }

        public override int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public TraceExtensionAttribute()
        {
            _logTypeMode = LogType.None;
            _resFilename = string.Empty;
            _reqFilename = string.Empty;
        }

        public TraceExtensionAttribute(LogType LogTypeMode,string RequestFileName,string ResponseFileName)
        {
            _logTypeMode = LogTypeMode;
            _resFilename = ResponseFileName;
            _reqFilename = RequestFileName;
        }

        public LogType LogTypeMode
        {
            get { return _logTypeMode; }
        }

        public string ReqFileName
        {
            get 
            {
                return (LogTypeMode.Equals(LogType.RequestOnly) || LogTypeMode.Equals(LogType.RequestReponse)) ? _reqFilename : string.Empty;   
            }
        }

        public string ResFileName
        {
            get
            {
                return (LogTypeMode.Equals(LogType.ResponseOnly) || LogTypeMode.Equals(LogType.RequestReponse)) ? _resFilename : string.Empty;
            }
        }
    
    }

    public enum LogType
    {
        None = 0,
        RequestOnly = 1,
        ResponseOnly = 2,
        RequestReponse = 3
    }
}
