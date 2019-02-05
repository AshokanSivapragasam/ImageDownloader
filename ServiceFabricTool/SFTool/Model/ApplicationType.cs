using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTool.Model
{
    public class ApplicationType : INotifyPropertyChanged
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
        public ApplicationType()
        {
            forceRestartEnabled = false;
            ApplicationParameters = new ObservableCollection<Parameter>();
            Services = new ObservableCollection<Service>();
        }
        public ApplicationType(string Id, string applicationName, string applicationTypeName, string typeVersion, string status)
        {
            this.Id = Id;
            this.ApplicationName = applicationName;
            this.ApplicationTypeName = applicationTypeName;
            this.TypeVersion = typeVersion;
            this.Status = status;
            ForceRestartEnabled = false;
            ApplicationTypeDeleteEnabled = true;
            ApplicationParameters = new ObservableCollection<Parameter>();
            Services = new ObservableCollection<Service>();

        }
        public string Id { get; set; }
        public string ApplicationName { get; private set; }
        public string ApplicationTypeName { get; private set; }

        private string typeVersion;
        private string status;
        private bool forceRestartEnabled;
        private bool applicationTypeDeleteEnabled;
        public ObservableCollection<Parameter> ApplicationParameters { get; set; }
        public ObservableCollection<Service> Services { get; set; }

        
        public string TypeVersion
        {
            get { return typeVersion; }
            set
            {
                typeVersion = value;
                RaisePropertyChanged("TypeVersion");
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
        }
        public bool ForceRestartEnabled
        {
            get { return forceRestartEnabled; }
            set
            {
                forceRestartEnabled = value;
                RaisePropertyChanged("ForceRestartEnabled");
            }
        }

        public bool ApplicationTypeDeleteEnabled
        {
            get { return applicationTypeDeleteEnabled; }
            set
            {
                applicationTypeDeleteEnabled = value;
                RaisePropertyChanged("ApplicationTypeDeleteEnabled");
            }
        }

        public override string ToString()
        {
            return this.ApplicationName;
        }
    }

    public class Parameter : INotifyPropertyChanged
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

        public Parameter()
        {

        }
        public Parameter(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        private string key;
        public string Key
        {
            get { return key; }
            set
            {
                this.key = value;
                RaisePropertyChanged("Key");
            }
        }

        private string value;
        public string Value
        {
            get { return value; }
            set
            {
                this.value = value;
                RaisePropertyChanged("Value");
            }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
