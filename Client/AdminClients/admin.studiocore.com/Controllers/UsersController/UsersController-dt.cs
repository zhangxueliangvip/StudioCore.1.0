using Autofac;
using AutofacProxy;
using ClientsPlugin.AdminServicesPlugin;
using Domain.DTOModels;
using Domain.QueryModels;
using Infrastructure.Utility;
using IocCorePlugin;
using IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace admin.studiocore.com.Controllers
{
    public partial class UsersController
    {
        #region 登录/注册

        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCodeImg()
        {
            var result = IocPlugin.IocVerificationCodePlugin.GetCodeImg(PluginCore.GetInstance.SafetySecretValue, true);
            if (result.Code == (int)EnumCore.CodeType.成功)
            {
                return File(result.ByData, @"image/jpeg");
            }
            return File(new byte[0], @"image/jpeg");
        }
        [HttpPost]
        public ActionResult LoginResult(UsersQueryItem entity)
        {
            entity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            entity.IsUsable = true;
            var result = IocPlugin.IocLoginPlugin.VerifyLogin(entity);
            return Json(result);
        }
        #endregion


        #region 个人信息/修改密码
        [HttpPost]
        public ActionResult UpPwd(string UserPwd, string NewUserPwd)
        {
            var result =IocPlugin.IocUpPwdPlugin.UpPwd(PluginCore.GetInstance.SafetySecretValue,true,UserPwd, NewUserPwd);
            return Json(result);
        }

        #endregion

        #region 用户管理
        [BaseAuthor(AllowAnonymous =false)]
        [HttpGet]
        public ActionResult GetPageDataUsers(UsersQueryItem queryEntity)
        {
            queryEntity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            queryEntity.IsUsable = true;

            var result = IocPlugin.IocGetPageDataUsersPlugin.GetPageDataUsers(queryEntity);
            var pageResult = result.TData;
            var pageList = pageResult.PageData.Select(m =>
            {
                return new
                {
                    displayID = m.ID,
                    displayUserName = m.UserName,
                    displayCTime = m.CTimeStr,
                    displayOperation = GetDisplayOperationUsers(m.OpenId, m.IsAdministrator)
                };
            });
            return Json(new { code = 0, msg = string.Empty, count = pageResult.Totals, data = pageList },JsonRequestBehavior.AllowGet);
        }

        private string GetDisplayOperationUsers(string OpenId, bool isAdministrator)
        {
            var html = "<div class=\"layui-btn-group\">";
            var detailHtml = string.Format("<button class=\"layui-btn layui-btn-xs\" title=\"查看详情\" onclick=\"Detailed('{0}')\">查看</button>", OpenId);
            var updateHtml = string.Format("<button class=\"layui-btn layui-btn-normal layui-btn-xs\" title=\"修改数据\" onclick=\"Updated('{0}')\">修改</button>", OpenId);
            var deleteHtml = string.Format("<button class=\"layui-btn layui-btn-danger layui-btn-xs\" title=\"删除数据\" onclick=\"Deleted('{0}')\">删除</button>", OpenId);
            var PllocationHtml = string.Format("<button class=\"layui-btn layui-btn-xs\" title=\"分配权限\" onclick=\"PllocationPage('{0}')\">分配权限</button>", OpenId);
            return html + detailHtml + updateHtml + (isAdministrator ? string.Empty : deleteHtml) + (isAdministrator ? string.Empty : PllocationHtml) + "</div>";
        }
        [BaseAuthor(AllowAnonymous = false)]
        [HttpPost]
        public ActionResult AddDataUsers(UsersModels entity)
        {
            entity.OpenId = TempData["usersopenId"].ToString();
            entity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            entity.IsUsable = true;
            var result = IocPlugin.IocAddDataUsersPlugin.AddDataUsers(entity);
            return Json(result);
        }

        [BaseAuthor(AllowAnonymous = false)]
        [HttpPost]
        public ActionResult DeleteDataUsers(string openId)
        {
            
            var result = IocPlugin.IosDeleteDataUsersPlugin.DeleteDataUsers(PluginCore.GetInstance.SafetySecretValue, true, openId);
            return Json(result);
        }
        #endregion
    }
}