using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunisoft.Framework.ExtensionMethod
{
    public static class StringExtension
    {
        public static bool IsNumberic(this string aString)
        {
            if (aString == null || aString.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < aString.Length; i++)
            {
                if (aString[i] < '0' || aString[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsNumbericWithASCII(this string aString)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(aString);
            return bytestr.All(x => x < 48 || x > 57);
        }
    }
}
