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
using Thunisoft.Framework.Proxy;

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Interaction logic for Page_Test.xaml
    /// </summary>
    public partial class Page_Test : Page
    {
        public Page_Test()
        {
            InitializeComponent();
        }

        private void Proxy_Click(object sender, RoutedEventArgs e)
        {
            IFun c = (IFun)new DynamicProxy<IFun>(new C1()).GetTransparentProxy();
        }
    }
    public interface IFun
    {
        string Fun(string aFunContent);
    }

    public class C1 : IFun
    {
        public const int R1 = 105;
        public string Fun(string aFunContent)
        {
            return aFunContent;
        }
    }
}
