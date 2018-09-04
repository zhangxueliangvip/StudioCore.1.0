using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace admin.studiocore.com.Controllers
{
    public partial class UsersController : Controller
    {
        #region 登录/注册
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        #endregion
        #region 用户管理
        [BaseAuthor(AllowAnonymous = false)]
        public ActionResult UsersIndex()
        {
            return View();
        }
        [BaseAuthor(AllowAnonymous = false)]
        public ActionResult AddUsersDataBox(string openId = "")
        {
            TempData["usersopenId"] = string.Empty;
            var result = IocCorePlugin.IocPlugin.IocGetUsersInfoPlugin.GetUsersInfo(PluginCore.GetInstance.SafetySecretValue, true, openId);
            var model = result.TData;
            ViewBag.Title = "添加记录";
            if (model.ID > 0)
            {
                TempData["usersopenId"] = model.OpenId;
                ViewBag.Title = "修改记录";
            }
            return PartialView("_PartialAddUsersDataBox", model);
        }
        #endregion

        #region 修改密码/注销
        [BaseAuthor(AllowAnonymous = false)]
        public ActionResult SetPwd()
        {
            return PartialView("_PartialSetPwdBox");
        }
        [BaseAuthor(AllowAnonymous = false)]
        public ActionResult LoginOff()
        {
            RedisCore.GetInstance.Remove(Config.SessionUserInfo);
            return RedirectPermanent("/");
        }
        #endregion
    }
}