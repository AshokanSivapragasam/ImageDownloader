﻿using SFTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFTool.ViewModel.Commands
{
    public class UpdateServiceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public ViewModelBase viewModel { get; set; }

        public UpdateServiceCommand(ViewModelBase viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null && parameter is Service)
                viewModel.UpdateService(parameter as Service);
        }
    }
}