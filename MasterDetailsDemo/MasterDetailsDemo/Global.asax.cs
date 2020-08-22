using MasterDetailsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MasterDetailsDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            //Teacher


            //Session["TeacherRoll"] = login.First().AddmissionNo;

            //Session["IdNo"] = login.Status;


            //Session["Name"] = new UserInfoReviwer();
            //Session["Id"] = new UserInfoReviwer();
            //Session["Email"] = new UserInfoReviwer();


            //Session["Login"] = "";
            //Session["Msg"] = "";
            //Session["TeacherIdH"] = new UserInfoReviwer();
            //Session["TeacherClass"] = new UserInfoReviwer();



            Session["AdminLogin"] = "";
            Session["AdminName"] = new AdminUser();
            Session["AdminMsg"] = "";

        }
    }
}
