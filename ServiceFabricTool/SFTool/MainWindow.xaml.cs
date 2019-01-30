using SFTool.Model;
using SFTool.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SFTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpgradeStatusLabel.Content = "";
            ViewModelBase vw=(ViewModelBase) this.FindResource("viewModel");
            if (treeView.SelectedItem is ApplicationType)
            {
                AppGrid.DataContext = treeView.SelectedItem;
                vw.AppGridEnable = true;
                vw.ServiceGridEnable = false;
            }
            else if (treeView.SelectedItem is Service)
            {
                ServiceGrid.DataContext = treeView.SelectedItem;
                vw.AppGridEnable = false;
                vw.ServiceGridEnable = true;
            }
        }
    }
}
