using System;
using System.Drawing;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace Thunisoft.Framework.Utilities
{
    public class SercurityUtil
    {
        private static readonly string key = "smkldospdosldaaa";
        private static readonly string iv = "0392039203920300";

        public static string ConvertBase64(string str)
        {
            var encode = System.Text.Encoding.Default;
            var bytedata = encode.GetBytes(str);
            return Convert.ToBase64String(bytedata, 0, bytedata.Length);
        }

        public static string GetBase64FromImage(string imagefile)
        {
            string strbaser64 = "";
            try
            {
                Bitmap bmp = new Bitmap(imagefile);
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                strbaser64 = Convert.ToBase64String(arr);
            }
            catch (System.Exception e)
            {
                e.ToString();
            }

            return strbaser64;
        }

        public static string Encrypt(string toEncrypt)
        {
            var keyArray = Encoding.UTF8.GetBytes(key);
            var ivArray = Encoding.UTF8.GetBytes(iv);
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            var rDel = new RijndaelManaged
            {
                BlockSize = 128,
                KeySize = 256,
                FeedbackSize = 128,
                Padding = PaddingMode.PKCS7,
                Key = keyArray,
                IV = ivArray,
                Mode = CipherMode.CBC
            };
            var cTransform = rDel.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt)
        {
            var keyArray = Encoding.UTF8.GetBytes(key);
            var ivArray = Encoding.UTF8.GetBytes(iv);
            var toEncryptArray = Convert.FromBase64String(toDecrypt);
            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                IV = ivArray,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = rDel.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        public static string Md5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }

        public static string GetLocalMacAddr()
        {
            var macAddr = "";
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;
                if (!(bool)mo["IPEnabled"]) continue;
                macAddr = mo["MacAddress"].ToString();
                mo.Dispose();
                break;
            }
            return macAddr;
        }

        public static byte[] File2Bytes(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new byte[0];
            }

            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];

            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return buff;
        }

        public static string MD5_16(string key)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Unicode.GetBytes(key)), 4, 8).Replace("-", "");
        }

        public static string MD5_32(string key)
        {
            string security = "";
            //实例化一个md5对像
            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                security += s[i].ToString("X");
            }
            return security;
        }
    }
}
