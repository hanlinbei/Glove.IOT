using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Glove.IOT.WebAPI.Controllers
{
    public class UserLoginController : ApiController
    {
        public IUserInfoService UserInfoService { get; set; }

        /// <summary>
        /// 处理验证表单
        /// </summary>
        /// <returns>OK</returns>
        [HttpGet]
        public object Login(string strUser,string strPwd)
        {
            if (!ValidateUser(strUser, strPwd))
            {
                return new { BRes = false };
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, strUser, DateTime.Now,
                DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", strUser, strPwd), FormsAuthentication.FormsCookiePath);
            //返回登录结果、用户信息、用户验证票据信息
            var oUser = new LogInfo { BRes = true, UserName = strUser, Password = strPwd, Ticket = FormsAuthentication.Encrypt(ticket) };
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[strUser] = oUser;
            return oUser;
        

        }

        private bool ValidateUser(string strName, string strPwd)
        {
            var userInfo =
             UserInfoService.GetEntities(u => (u.UName == strName && u.Pwd == strPwd))
             .FirstOrDefault();
            //没有查询到用户
            if (userInfo == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public class LogInfo
        {
            public bool BRes { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Ticket { get; set; }


        }


    }
}
