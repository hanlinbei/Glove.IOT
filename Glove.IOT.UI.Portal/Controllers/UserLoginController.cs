using Glove.IOT.Common;
using Glove.IOT.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public IUserInfoService UserInfoService { get; set; }
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
            short delNormal = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;
            var userInfo=
                UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal)
                .FirstOrDefault();

            if (userInfo == null)//没有查询出数据来
            {
                return Content("用户名密码错误！会登录吗");
            }
            Session["loginUser"] = userInfo;
            //如果正确跳转到首页
            return Content("OK");
        }
        #endregion

    }
}