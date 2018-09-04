using Autofac;
using AutofacProxy;
using ClientsPlugin.AdminServicesPlugin;
using Domain.DTOModels;
using Domain.QueryModels;
using Infrastructure.Utility;
using IocCorePlugin;
using System.Linq;
using System.Web.Mvc;
namespace admin.studiocore.com.Controllers
{
    public partial class HomeController
    {

        #region 角色管理
        [HttpGet]
        public JsonResult GetPageDataRoles(RolesQueryItem queryEntity)
        {
            queryEntity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            queryEntity.IsUsable = true;
            var result= IocPlugin.IocGetPageDataRolesPlugin.GetPageDataRoles(queryEntity);
            var pageResult = result.TData;
            var pageList = pageResult.PageData.Select(m =>
            {
                return new
                {
                    displayID = m.ID,
                    displayRoleTitle = m.RoleTitle,
                    displayParentLevel = m.GetParentLevelStr,
                    displayCTime = m.CTimeStr,
                    displayOperation = GetDisplayOperationRoles(m.OpenId)
                };
            });
            return Json(new { code = 0, msg = string.Empty, count = pageResult.Totals, data = pageList }, JsonRequestBehavior.AllowGet);
        }

        private string GetDisplayOperationRoles(string OpenId)
        {
            var html = "<div class=\"layui-btn-group\">";
            var updateHtml = string.Format("<button class=\"layui-btn layui-btn-normal layui-btn-xs\" title=\"修改数据\" onclick=\"Updated('{0}')\">修改</button>", OpenId);
            var deleteHtml = string.Format("<button class=\"layui-btn layui-btn-danger layui-btn-xs\" title=\"删除数据\" onclick=\"Deleted('{0}')\">删除</button>", OpenId);
            var PllocationHtml = string.Format("<button class=\"layui-btn layui-btn-xs\" title=\"分配权限\" onclick=\"PllocationPage('{0}')\">分配权限</button>", OpenId);
            return html + updateHtml + deleteHtml + PllocationHtml + "</div>";
        }

        [HttpPost]
        public ActionResult AddDataRoles(RolesModels entity)
        {
            entity.OpenId = TempData["rolesopenId"].ToString();
            var result = IocPlugin.IocAddDataRolesPlugin.AddDataRoles(entity);
            return Json(result);
        }


        [HttpPost]
        public ActionResult DeleteDataRoles(string openId)
        {
            var result = IocPlugin.IocDeleteDataRolesPlugin.DeleteDataRoles(PluginCore.GetInstance.SafetySecretValue,true,openId);
            return Json(result);
        }
        #endregion

        #region 权限管理
        [HttpPost]
        public ActionResult SetPllocationRolesCheck(AuthRequest entity)
        {
            entity.RoleOpenId = TempData["rpowersopenId"].ToString();
            var result = IocPlugin.IocSetPllocationRolesCheckPlugin.SetPllocationRolesCheck(PluginCore.GetInstance.SafetySecretValue, true, entity);
            return Json(result);
        }
        [HttpPost]
        public ActionResult SetPllocationUsersCheck(AuthRequest entity)
        {
            entity.UserOpenId = TempData["upowersopenId"].ToString();
            var result = IocPlugin.IocSetPllocationUsersCheckPlugin.SetPllocationUsersCheck(PluginCore.GetInstance.SafetySecretValue, true, entity);
            return Json(result);
        }
        #endregion

        #region 日志管理
        [HttpGet]
        public JsonResult GetPageDataLogs(LogRecordModels queryEntity)
        {
            queryEntity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            queryEntity.IsUsable = true;
            var result = IocPlugin.IocGetPageDataLogsPlugin.GetPageDataLogs(queryEntity);
            var pageResult = result.TData;
            var pageList = pageResult.PageData.Select(m =>
            {
                return new
                {
                    displayID = m.ID,
                    displayLogTitle = m.LogTitle,
                    displayLogContents = m.LogContents,
                    displayCTime = m.CTimeStr
                };
            });
            return Json(new { code = 0, msg = string.Empty, count = pageResult.Totals, data = pageList }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
