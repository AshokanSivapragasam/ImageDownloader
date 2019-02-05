using Newtonsoft.Json.Linq;
using SFTool.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFTool
{
    public static class SFApi
    {
        private static X509Certificate2 certificate;
        private static Uri clusterUri;
        public static void Initialize(Connection connection)
        {
            string explorerURL;
            if (connection.Secure)
            {
                explorerURL = "https://" + connection.ExplorerURL;
                if (!GetCertificate(connection.CertThumbprint))
                    throw new UnauthorizedAccessException("Certificate not found in CurrentUser Personal store");
            }
            else
                explorerURL = "http://" + connection.ExplorerURL;
            clusterUri = new Uri(explorerURL);
        }

        private static bool GetCertificate(string thumbprint)
        {
            if (String.IsNullOrWhiteSpace(thumbprint))
                return false;
            X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            // Try to open the store.

            certStore.Open(OpenFlags.ReadOnly);
            // Find the certificate that matches the thumbprint.
            X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            certStore.Close();

            var certEnumerator = certCollection.GetEnumerator();
            if (certEnumerator.MoveNext())
            {
                certificate = certEnumerator.Current;
                return true;
            }

            return false;
        }

        private static string GetSFResponse(string urlParams)
        {
            string responseString = String.Empty;
            // Create the request and add URL parameters.  
            Uri requestUri = new Uri(clusterUri, urlParams);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            if (certificate != null)
                request.ClientCertificates.Add(certificate);
            request.Method = "GET";

            // Execute the request and obtain the response.  

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), true))
                {
                    // Capture the response string.  
                    responseString = streamReader.ReadToEnd();
                }
            }
            return responseString;
        }

        private static string PostSFResponse(string urlParams, string requestString)
        {
            string responseString = String.Empty;
            // Create the request and add URL parameters.  
            Uri requestUri = new Uri(clusterUri, urlParams);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            if (certificate != null)
                request.ClientCertificates.Add(certificate);
            request.Method = "POST";

            // Execute the request and obtain the response.  

            if (!String.IsNullOrWhiteSpace(requestString))
            {
                request.ContentType = "text/json";

                byte[] requestBodyBytes = Encoding.UTF8.GetBytes(requestString);
                request.ContentLength = requestBodyBytes.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
                    requestStream.Close();

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        responseString = response.StatusCode.ToString();
                    }
                }
            }
            return responseString;

        }

        public static string GetApplicationList()
        {
            return GetSFResponse(String.Format("/Applications?api-version={0}", "2.0"));
        }

        public static string GetServiceList(string appName)
        {
            return GetSFResponse(String.Format("/Applications/{0}/$/GetServices?api-version={1}", appName, "2.0"));
        }

        public static string UpgradeApplicationByApplicationType(ApplicationType appl)
        {

            // Create the request and add URL parameters.  
            string requestUrl = string.Format("/Applications/{0}/$/Upgrade?api-version={1}",
                appl.Id,     // Application Name  
                "1.0");                // api-version  

            string forceRestart;
            if (appl.ForceRestartEnabled == true)
                forceRestart = "true";
            else
                forceRestart = "false";

            string appParams = "";
            var enumerator = appl.ApplicationParameters.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Parameter p = enumerator.Current;
                appParams = appParams + " {\"Key\":\"" + p.Key + "\", \"Value\":\"" + p.Value + "\"},";
            }
            appParams = appParams.Remove(appParams.Length - 1);
            string requestBody = "{" + String.Format("\"Name\":\"{0}\"," +
                                 "\"TargetApplicationTypeVersion\":\"{1}\"," +
                                 "\"Parameters\":[{2}]," +
                                 "\"UpgradeKind\":1," +
                                 "\"RollingUpgradeMode\":1," +
                                 "\"UpgradeReplicaSetCheckTimeoutInSeconds\":5," +
                                 "\"ForceRestart\":{3}", appl.ApplicationName, appl.TypeVersion, appParams, forceRestart) + "}";
            return PostSFResponse(requestUrl, requestBody);
        }



        public static string DeleteApplication(ApplicationType appl)
        {
            // Create the request and add URL parameters.  
            string requestUrl = string.Format("/Applications/{0}/$/Delete?api-version={1}",
                appl.Id,     // Application Name  
                "2.0");                // api-version 

            string requestBody = "";
            return PostSFResponse(requestUrl, requestBody);
        }
    }
}
