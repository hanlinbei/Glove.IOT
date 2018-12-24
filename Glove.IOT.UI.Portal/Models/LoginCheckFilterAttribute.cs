using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Glove.IOT.UI.Portal.Models
{
    public class LoginCheckFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           
            bool isAnoy = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                   filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            var identity = filterContext.HttpContext.User.Identity;
            if (!isAnoy && !identity.IsAuthenticated)
            {
               
              
                    FormsAuthentication.RedirectToLoginPage();

                
            }
        }

    }
}