using Glove.IOT.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Controllers
{
    public class ActionInfoController : Controller
    {
        // GET: ActionInfo
        public IActionInfoService ActionInfoService { get; set; }

        public ActionResult Index()
        {
            ViewData.Model = ActionInfoService.GetEntities(u => true).AsEnumerable();
            return View();
        }
    }
}