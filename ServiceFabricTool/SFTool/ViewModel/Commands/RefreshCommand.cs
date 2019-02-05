using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFTool.ViewModel.Commands
{
    public class RefreshCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public ViewModelBase viewModel { get; set; }

        public RefreshCommand(ViewModelBase viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
                viewModel.RefreshApplicationList();
        }
    }
}
