using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Thunisoft.Framework.Commands;

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Page_CheckBox.xaml 的交互逻辑
    /// </summary>
    public partial class Page_CheckBox : Page
    {
        public TFCommand<ExCommandParameter> CheckCommand { get; set; }
        public Page_CheckBox()
        {
            InitializeComponent();
            this.DataContext = this;
            CheckCommand = new TFCommand<ExCommandParameter>(CheckHandle);
        }

        private void CheckHandle(ExCommandParameter obj)
        {
            bool? isChecked = obj.Parameter as bool?;
            RoutedEventArgs eventArgs = obj.EventArgs as RoutedEventArgs;
            ToggleButton toggleButton = obj.Sender as ToggleButton;
            if (false == isChecked)
            {

            }
            else
            {
                
            }
            toggleButton.IsChecked = !isChecked;
            eventArgs.Handled = true;
        }
    }
}
