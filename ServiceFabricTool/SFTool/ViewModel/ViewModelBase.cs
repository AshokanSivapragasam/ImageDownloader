using Newtonsoft.Json.Linq;
using SFTool.Commands;
using SFTool.Model;
using SFTool.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFTool.ViewModel
{
    public class ViewModelBase:INotifyPropertyChanged
    {
        public Connection connection { get; set; }
        public ConnectCommand connectCommand { get; set; }
        public RemoveServiceCommand removeServiceCommand { get; set; }
        public RestartServiceCommand restartServiceCommand { get; set; }
        public UpdateServiceCommand updateServiceCommand { get; set; }
        public UpgradeApplicationCommand upgradeApplicationCommand { get; set; }
        public DeleteApplicationCommand deleteApplicationCommand { get; set; }
        public RefreshCommand refreshCommand { get; set; }
        public ObservableCollection<ApplicationType> Applications { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #endregion

        private string responseStatus;
        public string ResponseStatus
        {
            get
            {
                return responseStatus;
            }
            set
            {
                responseStatus = value;
                RaisePropertyChanged("ResponseStatus");
            }
        }

        private bool appGridEnable;
        public bool AppGridEnable
        {
            get
            {
                return appGridEnable;
            }
            set
            {
                appGridEnable = value;
                RaisePropertyChanged("AppGridEnable");
            }
        }

        private bool serviceGridEnable;
        public bool ServiceGridEnable
        {
            get
            {
                return serviceGridEnable;
            }
            set
            {
                serviceGridEnable = value;
                RaisePropertyChanged("ServiceGridEnable");
            }
        }
        public ViewModelBase()
        {
           
            connection = new Connection();
            connectCommand = new ConnectCommand(this);
            removeServiceCommand = new RemoveServiceCommand(this);
            restartServiceCommand = new RestartServiceCommand(this);
            updateServiceCommand = new UpdateServiceCommand(this);
            upgradeApplicationCommand = new UpgradeApplicationCommand(this);
            deleteApplicationCommand = new DeleteApplicationCommand(this);
            refreshCommand = new RefreshCommand(this);
            Applications = new ObservableCollection<ApplicationType>();
            AppGridEnable = false;
            ServiceGridEnable = false;
            SFPowerShell.Initialize();
            //ApplicationType ap = new ApplicationType("Id", "name", "TypeName", "version", "1");
            //Service s = new Service("SId", "SName", "SType", "SVersion", "1", "1");
            //ap.Services.Add(s);
            //Parameter p = new Parameter("keykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykeykey", "valuevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevaluevalue");
            //ap.ApplicationParameters.Add(p);
            //Parameter p2 = new Parameter("key2", "value2");
            //ap.ApplicationParameters.Add(p2);
            //Applications.Add(ap);
            //ApplicationType ap2 = new ApplicationType("Id2", "name2", "TypeName2", "version2", "1");
            //Service s2 = new Service("SId2", "SName2", "SType2", "SVersion2", "1", "1");
            //ap2.Services.Add(s2);
            //Parameter p3 = new Parameter("key2", "value2");
            //ap2.ApplicationParameters.Add(p3);
            //Applications.Add(ap2);
        }



        public void connectToCluster(Connection connect)
        {
            try
            {
                SFApi.Initialize(connect);
                //string msg = SFPowerShell.Initialize(connect);
                connect.ErrorMessage = "";
                if(RefreshApplicationList())
                    connect.IsConnected = true;
            }
            catch (Exception ex)
            {
                connect.ErrorMessage = ex.Message;
                if(ex.InnerException!=null)
                    connect.ErrorMessage = ex.InnerException.Message;
            }
            //Task.Run(()=>ScheduledApplicationRefresh()); 
        }
        public void ScheduledApplicationRefresh()
        {
            RefreshApplicationList();
            Thread.Sleep(1000 * 30);
        }

        public bool RefreshApplicationListPS()
        {
            try
            {
                var applicationList = SFPowerShell.GetApplicationList(connection);
                if (applicationList != null)
                {
                    AppGridEnable = false;
                    ServiceGridEnable = false;
                    var itemsToRemove = Applications.ToList();
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        Applications.Remove(itemToRemove);
                    }
                    foreach (var appl in applicationList)
                        Applications.Add(appl);
                }
                else
                {
                    ResponseStatus = "Unable to get Applications";
                    return false;
                }
                
                return true;
            }
            catch(Exception exp)
            {
                ResponseStatus = exp.Message;
                return false;
            }
        }
        public bool RefreshApplicationList()
        {
            try
            {
                string applicationResponse = SFApi.GetApplicationList();
                if (!string.IsNullOrWhiteSpace(applicationResponse))
                {
                    AppGridEnable = false;
                    ServiceGridEnable = false;
                    var itemsToRemove = Applications.ToList();
                    foreach (var itemToRemove in itemsToRemove)
                    {
                        Applications.Remove(itemToRemove);
                    }

                    JObject appJobj = JObject.Parse(applicationResponse);
                    foreach (var item in appJobj["Items"])
                    {
                        ApplicationType appl = new ApplicationType(item.SelectToken("Id").ToString(), item.SelectToken("Name").ToString(), item.SelectToken("TypeName").ToString(), item.SelectToken("TypeVersion").ToString(), item.SelectToken("Status").ToString());
                        foreach (var param in item["Parameters"])
                        {
                            Parameter p = new Parameter(param.SelectToken("Key").ToString(), param.SelectToken("Value").ToString());
                            appl.ApplicationParameters.Add(p);
                        }
                        string serviceResponse = SFApi.GetServiceList(appl.Id);
                        
                        if (!string.IsNullOrWhiteSpace(serviceResponse))
                        {
                            JObject serviceJobj = JObject.Parse(serviceResponse);
                            foreach (var svc in serviceJobj["Items"])
                            {
                                Service s = new Service(svc.SelectToken("Id").Value<string>(), svc.SelectToken("Name").Value<string>(), svc.SelectToken("TypeName").Value<string>(), svc.SelectToken("ManifestVersion").Value<string>(), svc.SelectToken("ServiceKind").Value<string>(), svc.SelectToken("ServiceStatus").Value<string>());
                                appl.Services.Add(s);
                            }
                        }
                        
                        Applications.Add(appl);
                    }
                    return true;
                }
                return false;
            }
            catch(Exception)
            {
                return RefreshApplicationListPS();
            }
        }

        public void RemoveService(Service service)
        {
            string response = SFPowerShell.RemoveService(service, connection);
            if (string.IsNullOrWhiteSpace(response))
            {
                ServiceGridEnable = false;
                string appname = service.ServiceName.Remove(service.ServiceName.LastIndexOf('/'));
                ApplicationType apl = Applications.Where(x => x.ApplicationName == appname).First();
                apl.Services.Remove(service);
            }
            else
                ResponseStatus = response;
        }

        public void RestartService(Service service)
        {
            string response = SFPowerShell.RestartService(service, connection);
            if (string.IsNullOrWhiteSpace(response))
            {
                ResponseStatus = "Service restarted";
            }
            else
                ResponseStatus = response;
        }
        public void UpgradeApplication(ApplicationType appl)
        {
            responseStatus = "";
            try
            {

                string responseString = SFApi.UpgradeApplicationByApplicationType(appl);
                ResponseStatus = responseString;
            }
            catch(Exception ex)
            {
                ResponseStatus = ex.Message;
                if (ex.InnerException != null)
                    ResponseStatus = ex.InnerException.Message;
            }
        }

        public void DeleteApplication(ApplicationType appl)
        {
            try
            {
                string response = SFPowerShell.DeleteApplication(appl.ApplicationName, connection);
                if (string.IsNullOrWhiteSpace(response))
                {
                    ResponseStatus = "application deleted";
                    RefreshApplicationList();
                }
                else
                    ResponseStatus = response;
            }
            catch(Exception ex)
            {
                ResponseStatus = ex.Message;
                if (ex.InnerException != null)
                    ResponseStatus = ex.InnerException.Message;
            }
        }

        public void UpdateService(Service service)
        {
            if (service.InstanceCount == 0)
                ResponseStatus = "Enter instance count ";
            else
            {
                string response = SFPowerShell.UpdateService(service, connection);
                if (string.IsNullOrWhiteSpace(response))
                {
                    ResponseStatus = "Service Updated";
                }
                else
                    ResponseStatus = response;
            }
        }

    }
}
