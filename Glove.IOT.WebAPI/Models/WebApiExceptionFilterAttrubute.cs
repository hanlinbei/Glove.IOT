using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Glove.IOT.WebAPI.Models
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //重写基类的异常处理方法
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            //自己处理异常
            //直接把错误信息写到日志文件里去
            Common.LogHelper.WriteLog(actionExecutedContext.Exception.ToString());
        }
    }
}