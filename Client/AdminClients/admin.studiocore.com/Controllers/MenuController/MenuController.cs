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
    public partial class MenuController : Controller
    {
        #region 页面管理
        public ActionResult PagesIndex()
        {
            return View();
        }

        public ActionResult AddPageDataBox(string openId = "")
        {
            TempData["pagesopenId"] = string.Empty;
            var result = IocPlugin.IocGetPagesInfoPlugin.GetPagesInfo(PluginCore.GetInstance.SafetySecretValue, true, openId);
            var model = result.TData;
            ViewBag.Title = "添加记录";
            if (model.ID > 0)
            {
                TempData["pagesopenId"] = model.OpenId;
                ViewBag.Title = "修改记录";
            }
            return PartialView("_PartialAddPageDataBox", model);
        }
        #endregion

        #region 图标管理
        public ActionResult IconsIndex()
        {
            return View();
        }

        public ActionResult AddIconsDataBox(string openId = "")
        {
            TempData["iconsopenId"] = string.Empty;
            var result = IocPlugin.IocGetIconsInfoPlugin.GetIconsInfo(PluginCore.GetInstance.SafetySecretValue, true, openId);
            var model = result.TData;
            ViewBag.Title = "添加记录";
            if (model.ID > 0)
            {
                TempData["iconsopenId"] = model.OpenId;
                ViewBag.Title = "修改记录";
            }
            return PartialView("_PartialAddIconsDataBox", model);
        }
        #endregion

        #region 操作管理
        public ActionResult OperationsIndex()
        {
            return View();
        }

        public ActionResult AddOperationsDataBox(string openId = "")
        {
            TempData["operationsopenId"] = string.Empty;
            var result = IocPlugin.IocGetOperationsInfoPlugin.GetIconsInfo(PluginCore.GetInstance.SafetySecretValue,true,openId);
            var model = result.TData;
            ViewBag.Title = "添加记录";
            if (model.ID > 0)
            {
                TempData["operationsopenId"] = model.OpenId;
                ViewBag.Title = "修改记录";
            }
            return PartialView("_PartialAddOperationsDataBox", model);
        }
        #endregion
    }
}
