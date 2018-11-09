using Glove.IOT.BLL;
using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        UserInfoService UserInfoService = new UserInfoService();
        public ActionResult Index()
        {
            ViewData.Model = UserInfoService.GetEntities(u => true);
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                UserInfoService.Add(userInfo);
            }
            return RedirectToAction("Index");
        }
    }
}