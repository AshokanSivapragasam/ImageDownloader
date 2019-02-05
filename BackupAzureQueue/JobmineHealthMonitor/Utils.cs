using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace JobmineHealthMonitor
{
    public class Utils
    {
        #region PRIVATE_VARIABLES
        private static ILog _logger;
        private static ILog _mailer;
        private static string _logappendername;
        private static string _mailappendername;
        #endregion

        #region PUBLIC_METHODS
        /// <summary>
        /// detect the encoding of the file using byte order mark (BOM).
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Encoding dtctEncoding(string filename)
        {
            byte[] data = new byte[3];
            StreamReader r = new StreamReader(filename);
            r.BaseStream.Read(data, 0, data.Length);
            r.Close();

            if (data[0] == 0xEF && data[1] == 0xBB && data[2] == 0xBF)
                return Encoding.UTF8;
            else if (data[0] == 0xFE && data[1] == 0xFF)
                return Encoding.BigEndianUnicode;
            else if (data[0] == 0xFF && data[1] == 0xFE)
                return Encoding.Unicode;
            else if (data[0] == 0x2B && data[1] == 0x2F && data[2] == 0x76)
                return Encoding.UTF7;
            else
                return Encoding.Default;
        }
        #endregion

        #region PUBLIC_PROPERTIES
        public ILog logger
        {
            get
            {
                return _logger;
            }
            set
            {
                _logger = value;
            }
        }

        public ILog mailer
        {
            get
            {
                return _mailer;
            }
            set
            {
                _mailer = value;
            }
        }

        public string logappendername
        {
            get
            {
                return _logappendername;
            }
            set
            {
                _logappendername = value;
            }
        }

        public string mailappendername
        {
            get
            {
                return _mailappendername;
            }
            set
            {
                _mailappendername = value;
            }
        }
        #endregion
    }
}
