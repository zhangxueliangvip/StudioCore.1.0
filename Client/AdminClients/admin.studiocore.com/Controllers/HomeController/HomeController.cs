using Autofac;
using AutofacProxy;
using ClientsPlugin.AdminServicesPlugin;
using Domain.DTOModels;
using Infrastructure.Utility;
using IocCorePlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace admin.studiocore.com.Controllers
{
    [BaseAuthor(AllowAnonymous = false)]
    public partial class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region 角色管理
        public ActionResult RolesIndex()
        {
            return View();
        }

        public ActionResult AddRolesDataBox(string openId = "")
        {
            TempData["rolesopenId"] = string.Empty;
           var result= IocPlugin.IocGetRolesInfoPlugin.GetRolesInfo(PluginCore.GetInstance.SafetySecretValue, true, openId);
            var model = (RolesModels)result.Data;
            ViewBag.Title = "添加记录";
            if (model.ID > 0)
            {
                TempData["rolesopenId"] = model.OpenId;
                ViewBag.Title = "修改记录";
            }
            return PartialView("_PartialAddRolesDataBox", model);
        }
        #endregion

        #region 权限管理
        public ActionResult RPowersDataBox(string openId = "")
        {
            TempData["rpowersopenId"] = string.Empty;
            var result = IocPlugin.IocGetRolesInfoPlugin.GetRolesInfo(PluginCore.GetInstance.SafetySecretValue, true, openId);
            var model = (RolesModels)result.Data;
            ViewBag.Title = "分配权限";
            TempData["rpowersopenId"] = model.OpenId;
            return PartialView("_PartiaRlPllocationPageBox", model);
        }

        public ActionResult UPowersDataBox(string openId = "")
        {
            TempData["upowersopenId"] = string.Empty;
            var result = IocPlugin.IocGetUsersInfoPlugin.GetUsersInfo(PluginCore.GetInstance.SafetySecretValue, true, openId);
            var model = (RolesModels)result.Data;
            ViewBag.Title = "分配权限";
            TempData["upowersopenId"] = model.OpenId;
            return PartialView("_PartialUPllocationPageBox", model);
        }

        #endregion

        #region 日志管理
        public ActionResult LogIndex()
        {
            return View();
        }
        #endregion
    }
}
