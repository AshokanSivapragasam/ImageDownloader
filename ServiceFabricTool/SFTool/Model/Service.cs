using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTool.Model
{
    public class Service : INotifyPropertyChanged
    {
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

        public Service()
        {

        }
        public Service(string id, string name, string typeName, string version, string serviceKind, string serviceStatus)
        {
            this.Id = id;
            this.ServiceName = name;
            this.ServiceTypeName = typeName;
            this.ManifestVersion = version;
            this.ServiceKind = serviceKind;
            this.ServiceStatus = serviceStatus;
        }
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceTypeName { get; set; }

        private string manifestVersion;
        private string serviceStatus;
        private string serviceKind;
        private int instanceCount;

        public string ServiceStatus {
            get { return serviceStatus; }
            set
            {
                serviceStatus = value;
                RaisePropertyChanged("ServiceStatus");
            }
        }

        public string ManifestVersion
        {
            get { return manifestVersion; }
            set
            {
                manifestVersion = value;
                RaisePropertyChanged("ManifestVersion");
            }
        }

        public string ServiceKind
        {
            get { return serviceKind; }
            set
            {
                serviceKind = value;
                RaisePropertyChanged("ServiceKind");
            }
        }

        public int InstanceCount
        {
            get { return instanceCount; }
            set
            {
                instanceCount = value;
                RaisePropertyChanged("InstanceCount");
            }
        }

        public override string ToString()
        {
            return this.ServiceName;
        }
    }
}
