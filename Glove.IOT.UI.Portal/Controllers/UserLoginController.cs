using Glove.IOT.Common;
using Glove.IOT.Common.Md5;
using Glove.IOT.IBLL;
using Glove.IOT.UI.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    [LoginCheckFilter(IsCheck =false)]
    public class UserLoginController : Controller
    {
    
        // GET: UserLogin
        public IUserInfoService UserInfoService { get; set; }
        public IMd5Helper Md5Helper { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 验证码
        public ActionResult ShowVCode()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);
            //把验证码放到Session里面去
            Session["VCode"] = strCode;

            byte[] imgBytes = validateCode.CreateValidateGraphic(strCode);

            return File(imgBytes, "image/jpeg");
        }
        #endregion

        #region 处理登录的表单
        public ActionResult ProcessLogin()
        {
            //第一步：处理验证码
            //拿到表单中的验证码
            #region 验证码
            string strCode = Request["vCode"];
            //拿到Session中的验证码
            string sessionCode = Session["VCode"] as string;
            Session["VCode"] = null;
            if ((string.IsNullOrEmpty(sessionCode)) || (strCode != sessionCode))

            {
                return Content("验证码错误！");
            }
            #endregion
            //第二步：处理验证用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];
           
            //pwd = Md5Helper.GetMd5(pwd);
            short delNormal = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;
            var userInfo =
                UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal)
                .FirstOrDefault();
            
            if (userInfo == null)//没有查询出数据来
            {
                return Content("用户名密码错误！会登录吗");
            }
            //Session["loginUser"] = userInfo;
            //用memcache+cookies代替之
            //立即分配一个标志，Guid把标志作为mm存储数据的key,把用户对象放到mm，把guid写到客户端cookies里面去
            string userLoginId = Guid.NewGuid().ToString();
            //把用户数据写到mm
            Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));
            //往客户端写入cookie
            Response.Cookies["userLoginId"].Value = userLoginId;

            //如果正确跳转到首页
            //return RedirectToAction("Index", "Home");
            return Content("OK");
        }
        #endregion

    }
}