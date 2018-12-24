using Glove.IOT.Common;
using Glove.IOT.Common.Md5;
using Glove.IOT.IBLL;
using Glove.IOT.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Glove.IOT.UI.Portal.Controllers
{
    [AllowAnonymous]
    public class UserLoginController : Controller
    {
    
        // GET: UserLogin
        public IUserInfoService UserInfoService { get; set; }
        public IMd5Helper Md5Helper { get; set; }
     
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns>文件流</returns>
        /// 
        public ActionResult ShowVCode()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);
            //把验证码放到Session里面去
            Session["VCode"] = strCode;

            byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);

            return File(imgBytes, "image/jpeg");
        }


        /// <summary>
        /// 处理验证表单
        /// </summary>
        /// <returns>OK</returns>
        public ActionResult ProcessLogin()
        {

            //拿到表单中的验证码
            string strCode = Request["vCode"];
            //拿到Session中的验证码
            string sessionCode = Session["VCode"] as string;
            Session["VCode"] = null;
            //第二步：处理验证用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];

            //pwd = Md5Helper.GetMd5(pwd);
            var userInfo =
                UserInfoService.GetEntities(u => u.UName == name)
                .FirstOrDefault();

            if (userInfo == null)//没有查询出数据来
            {
                return Content("无效的用户！");
            }
            else
            {
                if (userInfo.Pwd != pwd)
                {
                    return Content("登录密码错误!");
                }
                else if (userInfo.IsDeleted)
                {
                    return Content("用户已被删除!");
                }
                else if (userInfo.StatusFlag == false)
                {
                    return Content("用户状态无效!");
                }
                else if ((string.IsNullOrEmpty(sessionCode)) || (strCode != sessionCode))
                {
                    return Content("验证码错误！");
                }
                else
                {
                    //写入注册信息
                    DateTime expiration = DateTime.Now.Add(FormsAuthentication.Timeout);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                        name,
                        DateTime.Now,
                        expiration,
                        true,
                        userInfo.Id.ToString(),
                        FormsAuthentication.FormsCookiePath);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
                    {
                        HttpOnly = true,
                        Expires = expiration

                    };

                    HttpContext.Response.Cookies.Add(cookie);
                    return Content("OK");

                }
            }

        }




        public ActionResult Index()
        {
            return View();
        }

    }
}