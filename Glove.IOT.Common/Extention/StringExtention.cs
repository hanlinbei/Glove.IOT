using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Glove.IOT.Common.Extention
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtention
    {
        /// <summary>
        /// 用于判断是否为空字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsBlank(this string s)
        {
            return s == null || (s.Trim().Length == 0);
        }
        /// <summary>
        /// 用于判断是否不为空字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotBlank(this string s)
        {
            return !s.IsBlank();
        }
        /// <summary>
        /// 判断是否为有效的Email地址
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string s)
        {
            if (!s.IsBlank())
            {
                const string pattern= @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
                return Regex.IsMatch(s, pattern);
            }
            return false;
        }
        /// <summary>
        /// 验证是否是合法的电话号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidPhone(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^\+?((\d{2,4}(-)?)|(\(\d{2,4}\)))*(\d{0,16})*$");
            }
            return true;
        }
        /// <summary>
        /// 验证是否是合法的手机号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidMobile(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"^\+?\d{0,4}?[1][3-8]\d{9}$");
            }
            return false;
        }
        /// <summary>
        /// 验证是否是合法的邮编
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidZipCode(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"[1-9]\d{5}(?!\d)");
            }
            return true;
        }
        /// <summary>
        /// 将字符串转换成MD5加密字符串
        /// </summary>
        /// <param name="orgStr"></param>
        /// <returns></returns>
        public static string ToMD5(this string orgStr)
        {
            MD5 md5Hasher = MD5.Create();
            //Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(orgStr));
            // Create a new Stringbulider to collect the bytes
            //and create a string
            StringBuilder sBuilder = new StringBuilder();
            //Loop through each byte of the hashed data
            //and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            //Retrun the hexadecimal string
            return sBuilder.ToString();
        }
        /// <summary>
        /// 验证是否是合法的传真
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidFax(this string s)
        {
            if (!s.IsBlank())
            {
                return Regex.IsMatch(s, @"(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)");
            }
            return true;
        }

        public static bool IsInt32(this string val)
        {
            if (IsBlank(val))
                return false;
            Int32 k;
            return Int32.TryParse(val, out k);

        }
    }
}
