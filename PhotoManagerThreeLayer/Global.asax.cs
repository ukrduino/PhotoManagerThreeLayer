﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using PhotoManager.BLL.Services;
using PhotoManagerModels;

namespace PhotoManagerThreeLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BllDbServices bllDbServices = new BllDbServices();
            bllDbServices.SetUpDb();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperConf>();
            });
            bllDbServices.SeedDb();
        }
    }
}
