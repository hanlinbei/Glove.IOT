﻿using Glove.IOT.WebAPI.Models;
using System.Web;
using System.Web.Mvc;

namespace Glove.IOT.WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
