﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Thunisoft.Framework.Utilities
{
    public class NumberUtils
    { 
          public static bool IsNumber(string strDest)
        {
            if (string.IsNullOrEmpty(strDest))
                return false;

            foreach (char c in strDest)
            {
                if (c >= '0' && c <= '9')
                    continue;
                else
                    return false;
            }

            return true;
        }

        public static bool IsPhoneNumber(string phoneNumber)
        {
            if (11 != phoneNumber.Length)
                return false;

            bool res = Regex.IsMatch(phoneNumber, "^(12|13|14|15|16|17|18|19)[0-9]{9}$");

            return res;
        }

        public static bool IsAuthCode(string authCode)
        {
            bool res = Regex.IsMatch(authCode, "[0-9]{6}$");

            return true;
        }

        public static bool IsIdcardNumber(string idcard)
        {
            long n = 0;
            if (idcard == "")
                return false;

            if (long.TryParse(idcard.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idcard.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idcard.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idcard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idcard.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idcard.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }

            return true;
        }
}
}
