using SFTool.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SFTool
{
    public static class SFPowerShell
    {
        private static PowerShell psInstance;
        private static Runspace rs;
        private static bool isConnected = false;
        public static string Initialize()
        {
            rs = RunspaceFactory.CreateRunspace();
            rs.Open();
            psInstance = PowerShell.Create();
            psInstance.AddScript("Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force -Scope CurrentUser");
            psInstance.AddScript(" Write -Verbose \"Import Azure Powershell cmdlets\" \n Import-Module -Name \"C:\\Program Files\\Microsoft SDKs\\Service Fabric\\Tools\\PSModule\\ServiceFabricSDK\"");
            Collection<PSObject> PSOutput = psInstance.Invoke();
            if (psInstance.HadErrors)
            {
                var errors = psInstance.Streams.Error;
                string errorString = "";
                foreach (var error in errors)
                {
                    errorString += error.Exception.Message + "\n";
                }
                return errorString;
            }
            return null;
        }
        public static string ConnectCluster(Connection clusterConnect)
        { 
            string connectServiceFabric = String.Format("Connect-ServiceFabricCluster -ConnectionEndpoint {0} ", clusterConnect.ClusterURL);
            if (clusterConnect.Secure)
                connectServiceFabric = connectServiceFabric + String.Format("-KeepAliveIntervalInSec 10 -X509Credential -ServerCertThumbprint {0} -FindType FindByThumbprint -FindValue {1} -StoreLocation CurrentUser -StoreName My", clusterConnect.CertThumbprint , clusterConnect.CertThumbprint);
            clearPSInstance();
            psInstance.AddScript(connectServiceFabric);

            Collection<PSObject> PSOutput = psInstance.Invoke();
            if (psInstance.HadErrors)
            {
                var errors=psInstance.Streams.Error;
                string errorString = "";
                foreach(var error in errors)
                {
                    errorString += error.Exception.Message+"\n";
                }
                return errorString;
            }

            if (PSOutput.First().ToString() == "True")
            {
                isConnected = true;
                return null;
            }
            return "Unable to connect to cluster";
        }

        public static List<ApplicationType> GetApplicationList(Connection clusterConnect)
        {
            string errorString = "";
            if (!isConnected)
            {
                errorString = ConnectCluster(clusterConnect);
                if (!string.IsNullOrWhiteSpace(errorString))
                    return null;
            }
            List<ApplicationType> appList = new List<ApplicationType>();
            clearPSInstance();
            psInstance.AddScript("Get-ServiceFabricApplication");
            Collection<PSObject> PSOutput = psInstance.Invoke();
            foreach (PSObject outputItem in PSOutput)
            {
                string appName = outputItem.Properties["ApplicationName"].Value.ToString();
                string appTypeName = outputItem.Properties["ApplicationTypeName"].Value.ToString();
                string appTypeVersion = outputItem.Properties["ApplicationTypeVersion"].Value.ToString();
                string appStatus= outputItem.Properties["ApplicationStatus"].Value.ToString();
                string appParams= outputItem.Properties["ApplicationParameters"].Value.ToString();
                ApplicationType app = new ApplicationType(null, appName, appTypeName, appTypeVersion, appStatus);

                string pattern = string.Format("{0}({1}){2}",Regex.Escape("\""),".+?",Regex.Escape("\""));
                var matches = Regex.Matches(appParams, pattern);
                int i=0;
                while(i<matches.Count)
                {
                    string key = matches[i++].Groups[1].Value;
                    string value = matches[i++].Groups[1].Value;
                    app.ApplicationParameters.Add(new Parameter(key, value));
                }
                List<Service> services = GetServicesList(app.ApplicationName);
                foreach (var service in services)
                    app.Services.Add(service);
                appList.Add(app);
            }
            return appList;
        }

        public static List<Service> GetServicesList(string applicationName)
        {
            List<Service> serviceList = new List<Service>();
            clearPSInstance();
            psInstance.AddScript("Get-ServiceFabricService -ApplicationName "+applicationName);
            Collection<PSObject> PSOutput = psInstance.Invoke();
            foreach (PSObject outputItem in PSOutput)
            {
                string serviceName = outputItem.Properties["ServiceName"].Value.ToString();
                string serviceTypeName = outputItem.Properties["ServiceTypeName"].Value.ToString();
                string serviceManifestVersion = outputItem.Properties["ServiceManifestVersion"].Value.ToString();
                string serviceStatus = outputItem.Properties["ServiceStatus"].Value.ToString();
                string serviceKind = outputItem.Properties["ServiceKind"].Value.ToString();
                Service service= new Service(null, serviceName, serviceTypeName, serviceManifestVersion, serviceKind, serviceStatus);
                
                serviceList.Add(service);
            }
            return serviceList;
        }

        public static string DeleteApplication(string appName,Connection clusterConnect)
        {
            string errorString="";
            if (!isConnected)
            {
                errorString = ConnectCluster(clusterConnect);
                if (!string.IsNullOrWhiteSpace(errorString))
                    return errorString;                    
            }
            clearPSInstance();
            psInstance.AddScript(String.Format("Remove-ServiceFabricApplication -ApplicationName {0} -Force -ForceRemove",appName));
            Collection<PSObject> PSOutput = psInstance.Invoke();
            if (psInstance.HadErrors)
            {
                var errors = psInstance.Streams.Error;
                foreach (var error in errors)
                {
                    errorString += error.Exception.Message + "\n";
                }
                return errorString;
            }
            return null;
        }

        public static string RemoveService(Service service,Connection clusterConnect)
        {
            string errorString = "";
            if (!isConnected)
            {
                errorString = ConnectCluster(clusterConnect);
                if (!string.IsNullOrWhiteSpace(errorString))
                    return errorString;
            }
            clearPSInstance();
            psInstance.AddScript(String.Format("Remove-ServiceFabricService -ServiceName {0} -Force -ForceRemove", service.ServiceName));
            Collection<PSObject> PSOutput = psInstance.Invoke();
            if (psInstance.HadErrors)
            {
                var errors = psInstance.Streams.Error;
                foreach (var error in errors)
                {
                    errorString += error.Exception.Message + "\n";
                }
                return errorString;
            }
            return null;
        }

        public static string RestartService(Service service, Connection clusterConnect)
        {
            string errorString = "";
            if (!isConnected)
            {
                errorString = ConnectCluster(clusterConnect);
                if (!string.IsNullOrWhiteSpace(errorString))
                    return errorString;
            }
            clearPSInstance();
            if(service.ServiceKind=="1")
                psInstance.AddScript(String.Format("Restart-ServiceFabricPartition -ServiceName {0} -PartitionKindSingleton -RestartPartitionMode AllReplicasOrInstances", service.ServiceName));
            else
                psInstance.AddScript(String.Format("Restart-ServiceFabricPartition -ServiceName {0} -PartitionKindUniformInt64 -PartitionKey \"0\" -RestartPartitionMode AllReplicasOrInstances", service.ServiceName));
          
            Collection<PSObject> PSOutput = psInstance.Invoke();
            if (psInstance.HadErrors)
            {
                var errors = psInstance.Streams.Error;
                foreach (var error in errors)
                {
                    errorString += error.Exception.Message + "\n";
                }
                return errorString;
            }
            return null;
        }

        public static string UpdateService(Service service, Connection clusterConnect)
        {

            string errorString = "";
            if (!isConnected)
            {
                errorString = ConnectCluster(clusterConnect);
                if (!string.IsNullOrWhiteSpace(errorString))
                    return errorString;
            }
            clearPSInstance();
            if(service.ServiceKind=="1")
                psInstance.AddScript(String.Format("Update-ServiceFabricService -Stateless {0} -InstanceCount {1} -force",service.ServiceName,service.InstanceCount));
            else
                psInstance.AddScript(String.Format("Update-ServiceFabricService -Stateful {0} -MinReplicaSetSize {1} -TargetReplicaSetSize {1} -force", service.ServiceName, service.InstanceCount));
            Collection <PSObject> PSOutput = psInstance.Invoke();
            if (psInstance.HadErrors)
            {
                var errors = psInstance.Streams.Error;
                foreach (var error in errors)
                {
                    errorString += error.Exception.Message + "\n";
                }
                return errorString;
            }
            return null;
        }

        private static void clearPSInstance()
        {
            psInstance.Commands.Clear();
            psInstance.Streams.ClearStreams();
        }
    }
}
