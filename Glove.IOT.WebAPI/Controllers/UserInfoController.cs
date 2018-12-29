using Glove.IOT.IBLL;
using Glove.IOT.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Glove.IOT.WebAPI.Controllers
{
    /// <summary>
    /// 测试API Test Client
    /// </summary>
    [WebApiExceptionFilter]
    //[RequestAuthorize]
    public class UserInfoController : ApiController
    {
      public IUserInfoService UserInfoService { get; set; }

        //[HttpGet]
        //public IHttpActionResult GetAllChargingData()
        //{
        //    var temp = UserInfoService.GetEntities(u => u.UName == "admin");
        //    var data = temp.Select(u => new {
        //        u.UName,
        //        u.UCode,
        //        u.Pwd
        //    }).ToList();
        //    return Json(data);
        //}
        [HttpGet]
        public string GetAllChargingData()
        {
          
            return "Success";
        }
        [HttpGet]
        public string GetAll()
        {

            throw new NotImplementedException("fjafjeoifj    ");
        }
    }
}
