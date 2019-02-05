using SFTool.Model;
using SFTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFTool.Commands
{
    public class ConnectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public ViewModelBase viewModel { get; set; }
        public ConnectCommand(ViewModelBase viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            //if (parameter != null)
            //{
            //    Connection connect = parameter as Connection;
            //    return !connect.IsConnected;
            //}
            //return false;
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                Connection connect = parameter as Connection;
                viewModel.connectToCluster(connect);
            }

        }
    }
}
