using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunisoft.Demo.Forms;

namespace Thunisoft.Main
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new TestForm().ShowDialog();
            Console.WriteLine();
        }
    }
}
