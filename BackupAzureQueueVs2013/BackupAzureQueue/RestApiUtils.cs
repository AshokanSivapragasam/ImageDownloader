using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Configuration;

namespace BackupAzureQueue
{
    public class RestApiUtils
    {
        private static string deployementId;
        private const string serviceName = "ServiceName";
        private const string deployments = "Deployments";
        private const string deployment = "Deployment";
        private const string privateID = "PrivateID";
        private const string location = "Location";
        private const string affinityGroup = "AffinityGroup";

        private static readonly X509Certificate2 certificate;
        private static ObjectCache cache = MemoryCache.Default;
        private static readonly string subscriptionId;

        static RestApiUtils()
        {
            certificate = GetCertificate();
            subscriptionId = ConfigurationManager.AppSettings.Get("subscriptionId");
        }

        /// <summary>
        /// Gets Certificate from Certificate Store FindFromGivenStore
        /// </summary
        private static X509Certificate2 GetCertificate()
        {
            X509Certificate2 _certificate;

            foreach (var name in new[] { StoreName.My, StoreName.Root })
            {
                foreach (var location in new[] { StoreLocation.CurrentUser, StoreLocation.LocalMachine })
                {
                    _certificate = FindFromGivenStore(ConfigurationManager.AppSettings.Get("trafficMgrCertificateName"), name, location);
                    if (_certificate != null)
                    {
                        return _certificate;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Describes attributes associated with an event ID
        /// </summary>
        private static CacheItemPolicy GetCachePolicy()
        {
            CacheItemPolicy policy = new CacheItemPolicy();

            policy.Priority = CacheItemPriority.Default;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(Convert.ToDouble(86400));

            return policy;
        }

        /// <summary>
        /// Checks if cache object is value is empty or not.           
        /// </summary>
        /// <param name="deploymentId">Deployment ID of the solution</param>
        /// <returns>Datacenter Name</returns>
        public string GetDataCenterLocation(string deploymentId)
        {
            if (deploymentId == null)
                throw new ArgumentNullException("deploymentId");

            try
            {
                CheckCacheValueBasedOnKey(deploymentId);
            }
            catch (NullReferenceException)
            {
                cache.Add("CacheKey", string.Empty, GetCachePolicy());
                CheckCacheValueBasedOnKey(deploymentId);
            }

            return cache.Get("CacheKey").ToString();
        }

        /// <summary>
        /// Checks if cache object is value is empty or not. Based on that, the
        /// data center location is computed        
        /// </summary>
        /// <param name="deploymentId">Deployment ID of the solution</param>
        /// <returns></returns>
        private static void CheckCacheValueBasedOnKey(string deploymentId)
        {
            if (string.IsNullOrEmpty(cache.Get("CacheKey").ToString()))
                cache.Set("CacheKey", ComputeDatacenterLocation(deploymentId), GetCachePolicy());
        }

        /// <summary>
        /// retrives certificate from store based on thumbprint          
        /// </summary>
        /// <param name="thumbprint">Traffic Manager cretificate thumbprint</param>
        /// /// <param name="StoreName" >Certificate Store Name</param>
        /// /// <param name="StoreLocation">Certificate Store Location</param>
        /// <returns>X509Certificate</returns>
        private static X509Certificate2 FindFromGivenStore(string subjectName, StoreName name, StoreLocation location)
        {
            var certificateStore = new X509Store(name, location);

            certificateStore.Open(OpenFlags.ReadOnly);

            var certificateCollection = certificateStore.Certificates.Find(X509FindType.FindBySubjectName, subjectName, false);

            certificateStore.Close();

            if (certificateCollection.Count > 0)
            {
                return certificateCollection[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Loads the http response into XElement object          
        /// </summary>
        /// <param name="uri" />
        /// <param name="certificate">Traffic Manager Certificate</param>      
        /// <returns>XElement object</returns>
        private static XElement PerformGetOperation(string uri, X509Certificate2 certificate)
        {
            XElement responseBody = null;
            Uri requestUri = new Uri(uri);
            HttpWebRequest httpWebRequest = CreateHttpWebRequest(requestUri, certificate, "GET");

            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                responseBody = XElement.Load(responseStream);
            }

            return responseBody;
        }

        /// <summary>
        /// Gets the http response based on URI provided         
        /// </summary>
        /// <param name="uri" />
        /// <param name="certificate">Traffic Manager Certificate</param> 
        ///  <param name="httpWebRequestMethod">HttpWebRequest object</param>   
        /// <returns>HttpWebRequest object</returns>
        private static HttpWebRequest CreateHttpWebRequest(Uri uri, X509Certificate2 certificate, String httpWebRequestMethod)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);

            httpWebRequest.Method = httpWebRequestMethod;
            httpWebRequest.Headers.Add("x-ms-version", "2012 - 03 - 01");
            httpWebRequest.ClientCertificates.Add(certificate);
            httpWebRequest.ContentType = "application/xml";

            return httpWebRequest;
        }

        /// <summary>
        /// Gets the list of hosted services in the SubscriptionID        
        /// </summary>
        /// <param name="certificate">Treffic Manager Certificate</param>       
        /// <returns>Listof hosted services</returns>
        private static List<string> GetHostedServices(X509Certificate2 certificate)
        {
            List<string> hostedServiceNames = new List<string>();

            string uri = string.Format(ConfigurationManager.AppSettings.Get("etHostSvcUrlTemplate"), subscriptionId);

            XElement xe = PerformGetOperation(uri, certificate);

            if (xe != null)
            {
                var serviceNameElements = xe.Elements().Elements(XName.Get(serviceName, "http://schemas.microsoft.com/windowsazure"));

                foreach (var serviceElement in serviceNameElements)
                {
                    hostedServiceNames.Add(serviceElement.Value);
                }
            }

            return hostedServiceNames;
        }

        /// <summary>
        /// Checks if deploymentID provided matches the deploymentId of service     
        /// </summary>
        /// <param name="certificate">Traffic Manager Certificate</param> 
        /// <param name="ServiceName">Name of the hosted service</param>     
        /// <returns>Service name</returns>
        private static string GetHostedServiceNameBasedOnDeployementID(X509Certificate2 certificate, string ServiceName)
        {
            string uri = string.Format(ConfigurationManager.AppSettings.Get("getHostedServicePropertyOperationDetailedUrlTemplate"), subscriptionId, ServiceName);

            XElement xe = PerformGetOperation(uri, certificate);

            if (xe != null)
            {
                var deploymentXElements = xe.Elements(XName.Get(deployments, "http://schemas.microsoft.com/windowsazure")).Elements(XName.Get(deployment, "http://schemas.microsoft.com/windowsazure")).ToList();

                // Loops through the deployments in a hosted service and checks if the deployment id matches
                // any deployed service's id.
                if (deploymentXElements != null && deploymentXElements.Count > 0)
                {
                    foreach (var singleDeployment in deploymentXElements)
                    {

                        string currentDeploymentId = singleDeployment.Element(XName.Get(privateID, "http://schemas.microsoft.com/windowsazure")).Value;
                        if (currentDeploymentId == deployementId)
                        {
                            return (string)ServiceName;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Computes the name of the Datacenter   
        /// </summary>
        /// <param name="deploymentId">Deployment ID of the solution hosted on Azure Cloud</param>          
        /// <returns>datacenter Name</returns>
        private static string ComputeDatacenterLocation(string _deployementId)
        {
            string hostedServiceName = string.Empty;

            deployementId = _deployementId;


            List<string> hostedServices = GetHostedServices(certificate);
            foreach (var hostedService in hostedServices)
            {
                hostedServiceName = GetHostedServiceNameBasedOnDeployementID(certificate, (string)hostedService);

                if (!string.IsNullOrEmpty(hostedServiceName))
                {
                    break;
                }
            }

            if (!string.IsNullOrEmpty(hostedServiceName))
            {
                return GetLocationOfHostedService(hostedServiceName);
            }


            return "No hosted service.";
        }

        /// <summary>
        /// Computes the name of the Datacenter where service is hosted  
        /// </summary>
        /// <param name="hostedServiceName">hosted Service Name</param>          
        /// <returns>datacenter Name</returns>
        private static string GetLocationOfHostedService(string hostedServiceName)
        {
            string dataCenterLocation = string.Empty;

            XmlDocument detailsResponse = new XmlDocument();

            try
            {
                string responseXml = PerformGetOperation(string.Format(ConfigurationManager.AppSettings.Get("getHostedServicePropertyOperationDetailedUrlTemplate"), subscriptionId, hostedServiceName), certificate).ToString();

                detailsResponse.LoadXml(responseXml);

                return detailsResponse.GetElementsByTagName(location).Item(0).InnerText;
            }
            catch (NullReferenceException)
            {
                string affinityGroupLabel = detailsResponse.GetElementsByTagName(affinityGroup).Item(0).InnerText;
                return GetLocationFromAffinityLabel(certificate, affinityGroupLabel);
            }

        }

        /// <summary>
        /// Computes the name of the Datacenter where service is hosted based on Affinity group 
        /// </summary>
        /// <param name="certificate">Traffic Manager Certificate</param>  
        /// <param name="label">Xml string that contains the Affinity group details</param> 
        /// <returns>datacenter Name</returns>
        private static string GetLocationFromAffinityLabel(X509Certificate2 certificate, string label)
        {
            string uri = string.Format(ConfigurationManager.AppSettings.Get("getAffinityGroupsUrlTemplate"), subscriptionId, label);


            XElement xe = PerformGetOperation(uri, certificate);

            if (xe != null)
            {
                return xe.Element(XName.Get(location, "http://schemas.microsoft.com/windowsazure")).Value;
            }

            return null;
        }
    }
}
