using System;
using System.Collections.Generic;
using System.IO;
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

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Interaction logic for Page_WPS.xaml
    /// </summary>
    public partial class Page_WPS : Window
    {
        int i = 0;
        public Page_WPS()
        {
            InitializeComponent();
            this.Loaded += Page_WPS_Loaded;
        }

        private void Page_WPS_Loaded(object sender, RoutedEventArgs e)
        {
            WPS_Control.Close();
            WPS_Control.Activate();
            WPS_Control.Open(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"（2020）黑07民初212号.docx"));
        }

        private void FooterInsert_Click(object sender, RoutedEventArgs e)
        {
            var filenames = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"*.png");
            foreach (var filename in filenames)
            {
                WPS_Control.InsertFooterAndBoundarySignature("1",(i++).ToString(), filename);
            }
        }
    }
}
