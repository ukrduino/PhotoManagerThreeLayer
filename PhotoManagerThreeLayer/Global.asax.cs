using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using PhotoManager.BLL.Services;
using PhotoManager.BLL.DTOModels;
using PhotoManagerThreeLayer.Controllers;
using PhotoManagerThreeLayer.ViewModels;

namespace PhotoManagerThreeLayer
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            BllDbServices bllDbServices = new BllDbServices();
            bllDbServices.SetUpDb();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperViewProfile>();
                cfg.AddProfile<AutoMapperBllProfile>();
            });
            bllDbServices.SeedDb();
        }
        //  >>>>> For routing debug

        public override void Init()
        {
            base.Init();
            this.AcquireRequestState += showRouteValues;
        }

        protected void showRouteValues(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            if (context == null)
                return;
            // put breakpoint here and inspect routeData
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(context));
        }
        // For routing debug end <<<<<<<

        protected void Application_Error(object sender, EventArgs e)
        {
            // Do whatever you want to do with the error

            //Show the custom error page...
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";

            if ((Context.Server.GetLastError() is HttpException) && ((Context.Server.GetLastError() as HttpException).GetHttpCode() != 404))
            {
                routeData.Values["action"] = "Index";
            }
            else
            {
                // Handle 404 error and response code
                Response.StatusCode = 404;
                routeData.Values["action"] = "NotFound404";
            }
            IController errorsController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(Context);
            var rc = new RequestContext(wrapper, routeData);
            errorsController.Execute(rc);
        }
    }
}
