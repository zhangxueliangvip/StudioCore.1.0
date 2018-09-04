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
    public partial class MenuController
    {
        #region 页面管理
        [HttpGet]
        public JsonResult GetPageDataPages(PagesQueryItem queryEntity)
        {
            queryEntity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            queryEntity.IsUsable = true;
            var result = IocPlugin.IocGetPageDataPagesPlugin.GetPageDataPages(queryEntity);
            var pageResult = result.TData;
            var pageList = pageResult.PageData.Select(m =>
            {
                return new
                {
                    displayID = m.ID,
                    displayPageTitle = m.PageTitle,
                    displayPageIcon = m.PageIcon,
                    displayPageType = m.GetPageTypeStr,
                    displayPageSort = m.PageSort,
                    displayPageUrl = m.PagePathUrl,
                    displayPageLevel = m.ParentLevel,
                    displayCTime = m.CTimeStr,
                    displayOperation = GetDisplayOperationPages(m.OpenId)
                };
            });
            return Json(new { code = 0, msg = string.Empty, count = pageResult.Totals, data = pageList }, JsonRequestBehavior.AllowGet);
        }

        private string GetDisplayOperationPages(string OpenId)
        {
            var html = "<div class=\"layui-btn-group\">";
            //var detailHtml = string.Format("<button class=\"layui-btn layui-btn-xs\" title=\"查看详情\" onclick=\"Detailed('{0}')\">查看</button>", OpenId);
            var updateHtml = string.Format("<button class=\"layui-btn layui-btn-normal layui-btn-xs\" title=\"修改数据\" onclick=\"Updated('{0}')\">修改</button>", OpenId);
            var deleteHtml = string.Format("<button class=\"layui-btn layui-btn-danger layui-btn-xs\" title=\"删除数据\" onclick=\"Deleted('{0}')\">删除</button>", OpenId);
            return html + updateHtml + deleteHtml + "</div>";
        }

        [HttpPost]
        public ActionResult AddDataPages(PagesModels entity)
        {
            entity.OpenId = TempData["pagesopenId"].ToString();
            entity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            entity.IsUsable = true;
            var result = IocPlugin.IocAddDataPagesPlugin.AddDataPages(entity);
            return Json(result);
        }


        [HttpPost]
        public ActionResult DeleteDataPages(string openId)
        {
            var result = IocPlugin.IocDeleteDataPagesPlugin.DeleteDataPages(PluginCore.GetInstance.SafetySecretValue, true, openId);
            return Json(result);
        }
        #endregion

        #region 图标管理
        [HttpGet]
        public JsonResult GetPageDataIcons(IconModels queryEntity)
        {
            queryEntity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            queryEntity.IsUsable = true;
            var result = IocPlugin.IocGetPageDataIconsPlugin.GetPageDataIcons(queryEntity);
            var pageResult = result.TData;
            var pageList = pageResult.PageData.Select(m =>
            {
                return new
                {
                    displayID = m.ID,
                    displayIconName = m.IconName,
                    displayIconClass = m.IconClass,
                    displayCTime = m.CTimeStr,
                    displayOperation = GetDisplayOperationIcons(m.OpenId)
                };
            });
            return Json(new { code = 0, msg = string.Empty, count = pageResult.Totals, data = pageList }, JsonRequestBehavior.AllowGet);
        }

        private string GetDisplayOperationIcons(string OpenId)
        {
            var html = "<div class=\"layui-btn-group\">";
            //var detailHtml = string.Format("<button class=\"layui-btn layui-btn-xs\" title=\"查看详情\" onclick=\"Detailed('{0}')\">查看</button>", OpenId);
            var updateHtml = string.Format("<button class=\"layui-btn layui-btn-normal layui-btn-xs\" title=\"修改数据\" onclick=\"Updated('{0}')\">修改</button>", OpenId);
            var deleteHtml = string.Format("<button class=\"layui-btn layui-btn-danger layui-btn-xs\" title=\"删除数据\" onclick=\"Deleted('{0}')\">删除</button>", OpenId);
            return html + updateHtml + deleteHtml + "</div>";
        }

        [HttpPost]
        public ActionResult AddDataIcons(IconModels entity)
        {
            entity.OpenId = TempData["iconsopenId"].ToString();
            entity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            entity.IsUsable = true;
            var result = IocPlugin.IocAddDataIconsPlugin.AddDataIcons(entity);
            return Json(result);
        }


        [HttpPost]
        public ActionResult DeleteDataIcons(string openId)
        {
            var result = IocPlugin.IocDeleteDataIconsPlugin.DeleteDataIcons(PluginCore.GetInstance.SafetySecretValue,true,openId);
            return Json(result);
        }
        #endregion

        #region 操作管理
        [HttpGet]
        public JsonResult GetPageDataOperations(OperationsQueryItem queryEntity)
        {
            queryEntity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            queryEntity.IsUsable = true;
            var result = IocPlugin.IocGetPageDataOperationsPlugin.GetPageDataOperations(queryEntity);
            var pageResult = result.TData;
            var pageList = pageResult.PageData.Select(m =>
            {
                return new
                {
                    displayID = m.ID,
                    displayOperationTitle = m.OperationTitle,
                    displayCTime = m.CTimeStr,
                    displayOperation = GetDisplayOperationOperations(m.OpenId)
                };
            });
            return Json(new { code = 0, msg = string.Empty, count = pageResult.Totals, data = pageList }, JsonRequestBehavior.AllowGet);
        }

        private string GetDisplayOperationOperations(string OpenId)
        {
            var html = "<div class=\"layui-btn-group\">";
            //var detailHtml = string.Format("<button class=\"layui-btn layui-btn-xs\" title=\"查看详情\" onclick=\"Detailed('{0}')\">查看</button>", OpenId);
            var updateHtml = string.Format("<button class=\"layui-btn layui-btn-normal layui-btn-xs\" title=\"修改数据\" onclick=\"Updated('{0}')\">修改</button>", OpenId);
            var deleteHtml = string.Format("<button class=\"layui-btn layui-btn-danger layui-btn-xs\" title=\"删除数据\" onclick=\"Deleted('{0}')\">删除</button>", OpenId);
            return html + updateHtml + deleteHtml + "</div>";
        }

        [HttpPost]
        public ActionResult AddDataOperations(OperationsModels entity)
        {
            entity.OpenId = TempData["operationsopenId"].ToString();
            entity.SafetySecretKey = PluginCore.GetInstance.SafetySecretValue;
            entity.IsUsable = true;
            var result = IocPlugin.IocAddDataOperationsPlugin.AddDataOperations(entity);
            return Json(result);
        }


        [HttpPost]
        public ActionResult DeleteDataOperations(string openId)
        {
            var result = IocPlugin.IocDeleteDataOperationsPlugin.DeleteDataOperations(PluginCore.GetInstance.SafetySecretValue, true, openId);
            return Json(result);
        }
        #endregion

    }
}
