using AutofacProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace admin.studiocore.com
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册Ioc组件
            IocCore.Instance.Register(new string[] { "Repositorys", "AdminPlugin" });
            //日常日志
            BaseCore.AddLog("系统", "系统启动");
        }
    }
}
