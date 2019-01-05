using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Glove.IOT.Common
{
    /// <summary>
    /// /web中的一些工具类
    /// </summary>
    public class WebHelper
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(int dest, int host, ref long mac, ref int length);
        [DllImport("Ws2_32.dll")]
        private static extern int inet_addr(string ip);
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;

        }

        public static string GetClientMACAddress()
        {
            string mac_dest = string.Empty;
            string strClientIp = HttpContext.Current.Request.UserHostAddress.Trim();
            int ldest = inet_addr(strClientIp);//目的地的ip
            inet_addr("");//本地服务器的ip
            long macinfo = new long();
            int len = 6;
            SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");
            if (mac_src != "0")
            {
                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }
                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        mac_dest = i == 10
                            ? mac_dest.Insert(0, mac_src.Substring(i, 2))
                            : "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }

            }
            return mac_dest;

        }

    }
}
