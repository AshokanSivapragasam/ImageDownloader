using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFTool.Model
{
    public class Connection:INotifyPropertyChanged
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

        private string clusterURL;
        private string explorerURL;
        private string certThumbprint;
        private bool secure;
        private string errorMessage;
        private bool isConnected;

        public string ClusterURL
        {
            get { return clusterURL; }
            set
            {
                clusterURL = value;
                ExplorerURL = clusterURL.Remove(clusterURL.Length - 2) + "80";
                RaisePropertyChanged("ClusterURL");
            }
        }
        public string ExplorerURL
        {
            get { return explorerURL; }
            set
            {
                explorerURL = value;
            }
        }

        public string CertThumbprint
        {
            get { return certThumbprint; }
            set
            {
                certThumbprint = value;
                RaisePropertyChanged("CertThumbprint");
            }
        }

        public bool Secure
        {
            get { return secure; }
            set
            {
                secure = value;
                RaisePropertyChanged("Secure");
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                RaisePropertyChanged("IsConnected");
            }
        }
    }
}
