using Glove.IOT.IBLL;
using Glove.IOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home

        /// <summary>
        /// 主页起始视图
        /// </summary>
        /// <returns>主页视图</returns>
        public ActionResult Index(string UserName,string Ticket)
        {
            ViewBag.UserName = UserName;
            ViewBag.Ticket = Ticket;
            return View();

        }

        public ActionResult TreeIndex()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

    }
}