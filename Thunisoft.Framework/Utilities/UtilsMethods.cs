using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using Brushes = System.Windows.Media.Brushes;
using FontFamily = System.Windows.Media.FontFamily;
using FontStyle = System.Windows.FontStyle;
using Point = System.Windows.Point;
using Size = System.Windows.Size;
using System.Management;
using System.Text.RegularExpressions;
using Thunisoft.Framework.Log;
using Microsoft.Win32;

namespace Thunisoft.Framework.Utilities
{
    public enum ImageFormatEnum
    {
        Jpeg,
        Jpg,
        Bmp,
        Png
    }

    public class UtilsMethods
    {
        private const string Key = "smkldospdosldaaa";
        private const string Iv = "0392039203920300";

        public static string Encrypt(string toEncrypt)
        {
            if(string.IsNullOrEmpty(toEncrypt))
            {
                return string.Empty;
            }
            var keyArray = Encoding.UTF8.GetBytes(Key);
            var ivArray = Encoding.UTF8.GetBytes(Iv);
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
            if (string.IsNullOrEmpty(toDecrypt)) return "";

            try
            {
                var keyArray = Encoding.UTF8.GetBytes(Key);
                var ivArray = Encoding.UTF8.GetBytes(Iv);
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
            catch (Exception e)
            {
                return toDecrypt;
            }
        }

        public static DateTime GetDateTimeFromGmt(long time)
        {
            try
            {
                var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                return startTime.AddMilliseconds(time);
            }
            catch (Exception e)
            {
                TFLogger.LogError(e);
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 获取单个子元素
        /// </summary>
        /// <typeparam name="T">子元素类型</typeparam>
        /// <param name="obj">父元素</param>
        /// <param name="name">子元素名称</param>
        /// <returns></returns>
        public static T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            for (var i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                var grandChild = GetChildObject<T>(child, name);
                if (grandChild != null)
                    return grandChild;
            }
            return null;
        }

        /// <summary>
        /// 根据类型查找子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="typename"></param>
        /// <returns></returns>
        public static List<T> GetChildObjects<T>(DependencyObject obj, Type typename) where T : FrameworkElement
        {
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).GetType() == typename))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, typename));
            }
            return childList;
        }

        public static T GetParentObject<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(obj);

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        public static string GetXyDumpPath()
        {
            string path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "XYDump");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static void PointConvertByAngle(Point bPoint, double width, double height, ConvertAngleType angle,
            ref Point point, bool isRelative, ref bool isOutOfRange)
        {
            double x = point.X;

            isOutOfRange = false;

            switch (angle)
            {
                case ConvertAngleType.ConvertAngleType_0:
                    if (isRelative)
                    {
                        point.X = point.Y;
                        point.Y = height - x;
                    }
                    else
                    {
                        point.X = x - bPoint.X;
                        point.Y = point.Y - bPoint.Y;
                    }
                    break;
                case ConvertAngleType.ConvertAngleType_90:
                    if (isRelative)
                    {
                        point.X = point.Y;
                        point.Y = height - x;
                    }
                    else
                    {
                        point.X = point.Y - bPoint.Y;
                        point.Y = bPoint.X - x;
                    }
                    break;
                case ConvertAngleType.ConvertAngleType_180:
                    if (isRelative)
                    {
                        point.X = point.Y;
                        point.Y = height - x;
                    }
                    else
                    {
                        point.X = bPoint.X - x;
                        point.Y = bPoint.Y - point.Y;
                    }
                    break;
                case ConvertAngleType.ConvertAngleType_270:
                    if (isRelative)
                    {
                        point.X = point.Y;
                        point.Y = height - x;
                    }
                    else
                    {
                        point.X = bPoint.Y - point.Y;
                        point.Y = x - bPoint.X;
                    }
                    break;
                default:
                    break;
            }

            if ((point.X >= width && point.X <= 0) || (point.Y >= height && point.Y <= 0))
            {
                isOutOfRange = true;
            }
        }

        public static Double GetTextDisplayWidth(string str, FontFamily fontFamily, FontStyle fontStyle,
            FontWeight fontWeight, FontStretch fontStretch, double FontSize)
        {
            var formattedText = new FormattedText(
                str,
                CultureInfo.CurrentUICulture,
                System.Windows.FlowDirection.LeftToRight,
                new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                FontSize,
                Brushes.Black
            );
            Size size = new Size(formattedText.Width, formattedText.Height);
            return size.Width;
        }

        public static string GetDateTimeFromGMT(long time, string format = "yyyy-MM-dd HH:mm")
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(time);

            return dt.ToString(format);
        }

        public static DateTime GetTime(long timeStamp)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(timeStamp);
            return dt;
        }

        public static string Key_FirstCamera = "SelDevFirstCamera";
        public static string Key_SubCamera = "SelDevSubCamera";
        public static string Key_Microphone = "SelDevMicrophone";
        public static string Key_Speaker = "SelDevSpeaker";

        public static void SetAppConfig(string appKey, string appValue)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

                var xNode = xDoc.SelectSingleNode("//appSettings");

                var xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
                if (xElem != null) xElem.SetAttribute("value", appValue);
                else
                {
                    var xNewElem = xDoc.CreateElement("add");
                    xNewElem.SetAttribute("key", appKey);
                    xNewElem.SetAttribute("value", appValue);
                    xNode.AppendChild(xNewElem);
                }
                xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
            }
            catch (Exception e)
            {
                Debug.WriteLine("写入设备信息失败: " + appKey + " - " + appValue + "\t" + e.ToString());
            }
        }

        public static string GetAppConfig(string appKey)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

                var xNode = xDoc.SelectSingleNode("//appSettings");

                var xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");

                if (xElem != null)
                {
                    return xElem.Attributes["value"].Value;
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                Debug.WriteLine("获取历史设备信息失败: " + appKey + "\t" + e.ToString());
                //THLog.WriteLog("获取历史设备信息失败: " + appKey + "\t" + e.ToString());
                return string.Empty;
            }
        }

        public static bool SaveToImage(FrameworkElement ui, string fileName, out string path)
        {
            try
            {
                var dirPath = AppDomain.CurrentDomain.BaseDirectory + "EvidenceScreenShot";
                if ((Directory.Exists(dirPath)) == false)
                    Directory.CreateDirectory(dirPath);
                var Path = dirPath + "\\" + fileName + ".bmp";
                var fs = new FileStream(Path, FileMode.Create);
                var bmp = new RenderTargetBitmap((int)ui.ActualWidth, (int)ui.ActualHeight, 96d, 96d,
                    PixelFormats.Pbgra32);
                bmp.Render(ui);
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();
                path = Path;
                return true;
            }
            catch (Exception ex)
            {
                TFLogger.LogError(ex);
            }
            path = "";
            return false;
        }

        public static string SavaImage(int width, int height)
        {
            string path = "";
            try
            {
                Rect rect = new Rect(0, 0, width, height);
                var image = new Bitmap((int)rect.Width, (int)rect.Height);
                var imgGrapics = Graphics.FromImage(image);
                imgGrapics.CopyFromScreen(new System.Drawing.Point((int)rect.Left, (int)rect.Top),
                    new System.Drawing.Point(0, 0),
                    new System.Drawing.Size((int)rect.Width, (int)rect.Height));
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CloudRoomPhoto");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileName = Path.Combine(path, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
                image.Save(fileName, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                TFLogger.LogError(e);
            }
            return path;
        }

        public static string SavaImage(Rect rect, string path, ImageFormatEnum format)
        {
            if (rect == null || path.Length <= 0)
                return string.Empty;

            try
            {
                using (var image = new Bitmap((int)rect.Width, (int)rect.Height))
                {
                    using (var imgGrapics = Graphics.FromImage(image))
                    {
                        imgGrapics.CopyFromScreen(new System.Drawing.Point((int)rect.Left, (int)rect.Top),
                                                  new System.Drawing.Point(0, 0),
                                                  new System.Drawing.Size((int)rect.Width, (int)rect.Height));
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        var fileName = Path.Combine(path, DateTime.Now.ToString("yyyyMMddHHmmss") + $".{format.ToString().ToLower()}");

                        switch (format)
                        {
                            case ImageFormatEnum.Bmp:
                                image.Save(fileName, ImageFormat.Bmp);
                                break;
                            case ImageFormatEnum.Jpeg:
                            case ImageFormatEnum.Jpg:
                                image.Save(fileName, ImageFormat.Jpeg);
                                break;
                            case ImageFormatEnum.Png:
                                image.Save(fileName, ImageFormat.Png);
                                break;
                            default:
                                image.Save(fileName, ImageFormat.Jpeg);
                                break;
                        }

                        path = fileName;
                    }
                }
            }
            catch (Exception e)
            {
                TFLogger.LogError(e);
            }
            return path;
        }

        public static string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append($"\\u{buffer[1]:X2}{buffer[0]:X2}");
            }
            return sb.ToString();
        }

        /// <summary>  
        /// Unicode字符串转为正常字符串  
        /// </summary>  
        /// <param name="srcText"></param>  
        /// <returns></returns>  
        public static string UnicodeToString(string srcText)
        {
            try
            {
                string dst = "";
                string src = srcText;
                int len = srcText.Length / 6;
                for (int i = 0; i <= len - 1; i++)
                {
                    string str = "";
                    str = src.Substring(0, 6).Substring(2);
                    src = src.Substring(6);
                    byte[] bytes = new byte[2];
                    bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());
                    bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
                    dst += Encoding.Unicode.GetString(bytes);
                }
                return dst;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public static string NumToChineseCharacter(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖"; //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = ""; //从原num值中取出的值 
            string str4 = ""; //数字的字符串形式 
            string str5 = ""; //人民币大写金额形式 
            int i; //循环变量 
            int j; //num的值乘以100的字符串长度 
            string ch1 = ""; //数字的汉语读法 
            string ch2 = ""; //数字位的汉字读法 
            int nzero = 0; //用来计算连续的零值是几个 
            int temp; //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2); //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString(); //将num乘100并转换成字符串形式 
            j = str4.Length; //找出最高位 
            if (j > 15)
            {
                return "溢出";
            }
            str2 = str2.Substring(15 - j); //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1); //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3); //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        public static string NumberToChinese(int number)
        {
            string res = string.Empty;
            string str = number.ToString();
            string schar = str.Substring(0, 1);
            switch (schar)
            {
                case "1":
                    res = "一";
                    break;
                case "2":
                    res = "二";
                    break;
                case "3":
                    res = "三";
                    break;
                case "4":
                    res = "四";
                    break;
                case "5":
                    res = "五";
                    break;
                case "6":
                    res = "六";
                    break;
                case "7":
                    res = "七";
                    break;
                case "8":
                    res = "八";
                    break;
                case "9":
                    res = "九";
                    break;
                default:
                    res = "零";
                    break;
            }
            if (str.Length > 1)
            {
                switch (str.Length)
                {
                    case 2:
                    case 6:
                        res += "十";
                        break;
                    case 3:
                    case 7:
                        res += "百";
                        break;
                    case 4:
                        res += "千";
                        break;
                    case 5:
                        res += "万";
                        break;
                    default:
                        res += "";
                        break;
                }
                res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
            }
            return res;
        }

        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            try
            {
                var startindex = sourse.IndexOf(startstr, StringComparison.Ordinal);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                var endindex = tmpstr.IndexOf(endstr, StringComparison.Ordinal);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                TFLogger.LogError(ex);
            }
            return result;
        }

        public static string GetMacAddress()
        {
            try
            {
                var macAddr = string.Empty;
                var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                using (var moc = mc.GetInstances())
                {
                    foreach (var o in moc)
                    {
                        var mo = (ManagementObject)o;
                        if (!(bool)mo["IPEnabled"]) continue;
                        macAddr = mo["MacAddress"].ToString();
                        break;
                    }
                }
                return macAddr;
            }
            catch
            {
                return "unknow";
            }
        }

        public static void ForbidSpechars(System.Windows.Controls.TextBox textBox)
        {
            if (Regex.IsMatch(textBox.Text.Trim(), @"((?=[\x21-\x7e]+)[^A-Za-z0-9])")
                || Regex.IsMatch(textBox.Text.Trim(), @"[\u3002|\uff1f|\uff01|\uff0c|\u3001|\uff1b|
                \uff1a|\u201c|\u201d|\u2018|\u2019|\uff08|\uff09|\u300a|\u300b|\u3008|\u3009|\u3010|
                \u3011|\u300e|\u300f|\u300c|\u300d|\ufe43|\ufe44|\u3014|\u3015|\u2026|\u2014|\uff5e|\ufe4f|\uffe5|\u00b7]"))
            {
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }


        /// <summary>
        /// 由连字符分隔的32位数字
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            var guid = new Guid();
            guid = Guid.NewGuid();
            return guid.ToString();
        }
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        [System.Runtime.InteropServices.DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);
        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;

        public static bool LocalConnectionStatus()
        {
            System.Int32 dwFlag = new Int32();
            if (!InternetGetConnectedState(ref dwFlag, 0))
            {
                Console.WriteLine("LocalConnectionStatus--未连网!");
                return false;
            }
            else
            {
                if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
                {
                    Console.WriteLine("LocalConnectionStatus--采用调制解调器上网。");
                    return true;
                }
                else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
                {
                    Console.WriteLine("LocalConnectionStatus--采用网卡上网。");
                    return true;
                }
            }
            return false;
        }

        public static byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        /// <summary>  
        /// 将 byte[] 转成 Stream  
        /// </summary>  
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static Stream TakePhoto(FrameworkElement fe)
        {
            try
            {
                if (fe == null) return null;
                var pt = fe.PointToScreen(new Point(0, 0));
                var rect = new Rect(pt.X, pt.Y, fe.ActualWidth, fe.ActualHeight);
                var image = new Bitmap((int)rect.Width, (int)rect.Height);
                var imgGrapics = Graphics.FromImage(image);
                imgGrapics.CopyFromScreen(new System.Drawing.Point((int)rect.Left, (int)rect.Top),
                    new System.Drawing.Point(0, 0),
                    new System.Drawing.Size((int)rect.Width, (int)rect.Height));
                var data = Bitmap2Byte(image);
                return BytesToStream(data);
            }
            catch (Exception e)
            {
                TFLogger.LogError(e);
                return null;
            }
        }

        public static double VersionString2Number(string SplitStr, int SplitNum)
        {
            String[] Buf = SplitStr.Split('.');
            int j = SplitNum;
            int tempNum = 0;
            int charNum = 0;
            double latestVersionNum = 0;

            for (int i = 0; i < SplitNum; i++)
            {
                if (i < Buf.Length && Buf[i].Length > 0)
                {
                    charNum = 0;
                    if (Regex.IsMatch(Buf[i], "[a-zA-Z]"))
                    {
                        charNum = char.ConvertToUtf32(Buf[i], Buf[i].Length - 1);
                        Buf[i] = Regex.Replace(Buf[i], "[a-zA-Z]", "");
                    }

                    if (int.TryParse(Buf[i], out tempNum))
                    {
                        latestVersionNum += (tempNum + charNum) * Math.Pow(10, j - 1);
                    }
                }
                j--;
            }

            return latestVersionNum;
        }

        public static string SubString(string pstr, int Num)
        {
            string StrNum = pstr;
            byte[] bytes1 = System.Text.Encoding.Default.GetBytes(StrNum.Trim());
            int icha = bytes1.Length;
            if (icha > Num)
            {
                byte[] bytes2 = System.Text.Encoding.Default.GetBytes(pstr.Trim()/*.Substring(0, Num)*/);
                string strNum1 = "";
                strNum1 = System.Text.Encoding.Default.GetString(bytes2, 0, Num);
                int len = strNum1.Length;
                string subStr = pstr.Substring(0, len);
                if (subStr != strNum1)
                {
                    StrNum = System.Text.Encoding.Default.GetString(bytes2, 0, Num - 1);
                }
                else
                {
                    StrNum = strNum1;
                }
            }

            return StrNum;
        }

        public static bool FileIsUsed(string fileFullName)
        {
            var result = false;
            if (!File.Exists(fileFullName)) return false;
            FileStream fileStream = null;
            try
            {
                fileStream = File.Open(fileFullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                result = true;
            }
            catch (Exception)
            {
                result = true;
            }
            finally
            {
                fileStream?.Close();
            }
            return result;
        }

        public static long GetTimeStamp()
        {
            //获取时间戳
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        public static string GetSafeFilename(string arbitraryString)
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars();
            var replaceIndex = arbitraryString.IndexOfAny(invalidChars, 0);
            if (replaceIndex == -1) return arbitraryString;
            var r = new StringBuilder();
            var i = 0;
            do
            {
                r.Append(arbitraryString, i, replaceIndex - i);
                switch (arbitraryString[replaceIndex])
                {
                    case '"':
                        r.Append("''");
                        break;
                    case '<':
                        r.Append('\u02c2'); // '˂' (modifier letter left arrowhead)
                        break;
                    case '>':
                        r.Append('\u02c3'); // '˃' (modifier letter right arrowhead)
                        break;
                    case '|':
                        r.Append('\u2223'); // '∣' (divides)
                        break;
                    case ':':
                        r.Append('-');
                        break;
                    case '*':
                        r.Append('\u2217'); // '∗' (asterisk operator)
                        break;
                    case '\\':
                    case '/':
                        r.Append('\u2044'); // '⁄' (fraction slash)
                        break;
                    case '\0':
                    case '\f':
                    case '?':
                        break;
                    case '\t':
                    case '\n':
                    case '\r':
                    case '\v':
                        r.Append(' ');
                        break;
                    default:
                        r.Append('_');
                        break;
                }
                i = replaceIndex + 1;
                replaceIndex = arbitraryString.IndexOfAny(invalidChars, i);
            } while (replaceIndex != -1);
            r.Append(arbitraryString, i, arbitraryString.Length - i);
            return r.ToString();
        }

        public static bool MatchPassword(string pwd)
        {
            return Regex.IsMatch(pwd, @"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[~!@#$%^&*])[\da-zA-Z~!@#$%^&*]{8,}$");
        }

        public static string ConvertBase64(string str)
        {
            var encode = System.Text.Encoding.Default;
            var bytedata = encode.GetBytes(str);
            return Convert.ToBase64String(bytedata, 0, bytedata.Length);
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="objnum"></param>
        /// <param name="numberprecision"></param>
        /// <returns></returns>
        public static double ChineseMathRound(object objnum, int numberprecision)
        {
            double returnnum = 0;
            if (objnum != null)
            {
                try
                {
                    double dblnum = double.Parse(objnum.ToString());
                    int tmpNum = dblnum > 0 ? 5 : -5;
                    double dblreturn = Math.Truncate(dblnum * Math.Pow(10, numberprecision + 1)) + tmpNum;

                    dblreturn = Math.Truncate(dblreturn / 10) / Math.Pow(10, numberprecision);
                    returnnum = dblreturn;
                }
                catch { }
            }
            return returnnum;
        }
        public static bool IsWPSIntalled()
        {
            bool _exist = false;
            try
            {
                string[] subkeyNames;
                RegistryKey hkml = Registry.CurrentUser;
                RegistryKey software = hkml.OpenSubKey("Software", true);
                RegistryKey aimdir = software.OpenSubKey("Kingsoft", true);
                subkeyNames = aimdir.GetSubKeyNames();
                foreach (string keyName in subkeyNames)
                {
                    if (keyName == "Office")
                    {
                        _exist = true;
                        return _exist;
                    }
                }
            }
            catch
            {
                //ignore
                return false;
            }
            return _exist;
        }
        public static bool IsRegisterWPSComponent()
        {
            try
            {
                RegistryKey cr = Registry.ClassesRoot;
                string[] names = cr.GetSubKeyNames();
                List<string> nameList = new List<string>(names);
                if (nameList.Contains("WPSDocFrame.Frame") || nameList.Contains("WPSDocFrame.Frame.1"))
                {
                    
                    return true;
                }
                else
                {
                    TFLogger.LogInfo($"ClassesRoot->SubKeyNames: {string.Join(";", names)}");
                    return false;
                }
                //不检查路径，只检查注册表
                //string windir = Environment.GetEnvironmentVariable("windir");
                //string sys32dir = Path.Combine(windir, @"system32\wpsdocframe.dll");
                //string sys64dir = Path.Combine(windir, @"SysWOW64\wpsdocframe.dll");
                //if (File.Exists(sys32dir) || File.Exists(sys64dir))
                //{
                //}
            }
            catch (Exception e)
            {
                TFLogger.LogError(e);
            }
            return false;
            //由于有弹窗，暂时不自动注册
            //ExecuteBat("wps.bat");
        }
        public static void ExecuteBat(string batFile)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.Verb = "runas";
            p.StartInfo.FileName = batFile;
            try
            {
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                TFLogger.LogError(e);
            }
        }
    }
    public enum ConvertAngleType
    {
        ConvertAngleType_0,
        ConvertAngleType_90,
        ConvertAngleType_180,
        ConvertAngleType_270
    }
}
