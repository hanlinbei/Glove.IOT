using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }
        short delflagNormal = (short)Glove.IOT.Model.Enum.DelFlagEnum.Normal;

        public ActionResult Index()
        {
            ViewBag.AllMenu = LoadUserMenu();
          //最新的主框架
            return View();
            //老式的主框架
            //return View("TreeIndex");

        }
        public List<ActionInfo> LoadUserMenu()
        {
            //拿到当前用户
            int userId = this.LoginUser.Id;
            var user = UserInfoService.GetEntities(u => u.Id == userId).FirstOrDefault();
            //拿到当前用户所有的权限【必须是菜单类型权限】
            var allRole = user.RoleInfo;
            var allRoleActionIds = (from r in allRole
                                   from a in r.ActionInfo
                                   select a.Id).ToList();
            var allDenyActionIds = (from r in user.R_UserInfo_ActionInfo
                                    where r.HasPermission == false
                                    select r.ActionInfoId).ToList();

            //var allActionIds = allRoleActionIds.Where(u => !allDenyActionIds.Contains(u));
            var allActionIds = (from a in allRoleActionIds
                                where !allDenyActionIds.Contains(a)
                                select a).ToList();
            var allUserActionIds = (from t in user.R_UserInfo_ActionInfo
                                    where t.HasPermission == true
                                    select t.ActionInfoId).ToList();
            //当前用户的所有的权限拿到
             allActionIds.AddRange(allUserActionIds.AsEnumerable());
            //去重操作
             allActionIds = allActionIds.Distinct().ToList();

            var actionList = ActionInfoService.GetEntities(a => allActionIds.Contains(a.Id) 
                                                            && a.IsMenu == true && a.DelFlag == delflagNormal).ToList();
            //{ icon: '/Content/Images/Home/3DSMAX.png', title: '用户列表', url: '/UserInfo/Index' }
            //var data = from a in actionList
            //           select new { icon = a.MenuIcon, title = a.ActionName, url = a.Url };
            //return Json(data, JsonRequestBehavior.AllowGet);
            return actionList;
        }
    }
}