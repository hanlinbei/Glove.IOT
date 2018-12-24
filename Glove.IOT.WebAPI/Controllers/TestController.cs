using Glove.IOT.IBLL;
using Glove.IOT.Model.Param;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Glove.IOT.WebAPI.Controllers
{
    public class TestController : ApiController
    {
        public IUserInfoService UserInfoService { get; set; }

       
        public <> GetAllUserInfos()
        {

            var queryParam = new UserQueryParam()
            {
                PageSize = 10,
                PageIndex = 1,
                Total = 0,
            };

            var pageData = UserInfoService.LoagUserPageData(queryParam).ToList();
            
            return data;

        }
    }
}
