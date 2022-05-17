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
using Thunisoft.Demo.Windows;
using Thunisoft.Framework.Log;
using Thunisoft.Framework.Utilities;

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Interaction logic for Page_Button.xaml
    /// </summary>
    public partial class Page_Button : Page
    {
        public Page_Button()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TFLogger.LogError(new Exception("exception"));
            TFLogger.LogDebug("debug");
            TFLogger.LogInfo("info");
            TFLogger.LogWarning("warning");
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Test.Content = await Task.Run<string>(()=> 
            {
                return "hha";
            });
        }
        private async Task TestExecute()
        {
            await Task.Run(() => Test.Content = "Test").ConfigureAwait(false);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new ViewPDFWindow().Show();
        }
        public int[] TwoSum(int[] nums, int target)
        {
            var n = nums.FirstOrDefault(x => nums.Contains(target - x));
            return new int[] { Array.IndexOf(nums,n), Array.IndexOf(nums, target - n) };
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(testTextBox.Text))
            {
                resultTextBox.Text = UtilsMethods.Decrypt(testTextBox.Text);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new Window().ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string[] tests = new string[]
            {
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7", 
            };
            List<string> testList = new List<string>();
            testList = tests.ToList();
            var r = testList.OrderBy(t=>t != "5");           
        }
        public class testobject
        {
            public int i
            {
                get;
                set;
            }
            public string j
            {
                get;
                set;
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            test(new testobject 
            {
                i = 11,
                j = "11"
            });
        }
        public void test(testobject o)
        {
            lock (this)
            {
                if (o.i > 10)
                {
                    o.i--;
                    Console.WriteLine(o.i);
                    test(o);
                }
            }
        }
    }
}
