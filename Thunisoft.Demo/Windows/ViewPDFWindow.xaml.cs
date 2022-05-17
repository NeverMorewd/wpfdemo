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
using System.Windows.Shapes;

namespace Thunisoft.Demo.Windows
{
    /// <summary>
    /// Interaction logic for ViewPDFWindow.xaml
    /// </summary>
    public partial class ViewPDFWindow : Window
    {
        public ViewPDFWindow()
        {
            InitializeComponent();
            PDFViewer.Loaded += PDFViewer_Loaded;
        }

        private void PDFViewer_Loaded(object sender, RoutedEventArgs e)
        {
            //OpenPDF("temp（2020）湘3366民终26号.pdf");
            OpenPDF("pdftest.pdf");
        }

        private void OpenPDF(string aFileName)
        {
            PDFViewer.OpenFile(aFileName);
        }
    }
}
