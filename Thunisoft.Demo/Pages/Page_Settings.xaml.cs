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

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Page_Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Page_Settings : Page
    {
        public List<ItemContainer> Items { get; set; }
        public Page_Settings()
        {
            InitializeComponent();
            this.DataContext = this;
            Items = new List<ItemContainer>
            {
                new ItemContainer
                {
                     Evidences = new List<string>{ "1","2","3","4"},
                      name = "1",
                },
                new ItemContainer
                {
                     Evidences = new List<string>{ "1","2","3","4"},
                      name = "1",
                },
                new ItemContainer
                {
                     Evidences = new List<string>{ "1","2","3","4"},
                      name = "1",
                },
                new ItemContainer
                {
                     Evidences = new List<string>{ "1","2","3","4"},
                      name = "1",
                },
                new ItemContainer
                {
                     Evidences = new List<string>{ "1","2","3","4"},
                      name = "1",
                },
                new ItemContainer
                {
                     Evidences = new List<string>{ "1","2","3","4"},
                      name = "1",
                },
            };           
        }
        public class ItemContainer
        { 
            public string name { get; set; }
            public List<string> Evidences { get; set; }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            ScrollViewer scrollViewer = sender as ScrollViewer;
            scrollViewer.RaiseEvent(eventArg);
        }
    }
}
