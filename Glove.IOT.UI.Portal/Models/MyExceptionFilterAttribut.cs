using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.UI.Portal.Models
{
    public class MyExceptionFilterAttribut:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //自己处理异常
            //直接把错误信息写到日志文件里去
            Common.LogHelper.WriteLog(filterContext.Exception.ToString());
        }
    }
}