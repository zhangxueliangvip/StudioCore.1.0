using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminManager;
using Domain.BaseModels;
using Domain.DTOModels;
using Domain.QueryModels;
using Infrastructure.Utility;

namespace ClientsPlugin.AdminServicesPlugin
{
    public class GetUsersChildMenuPlugin : BasePluginCore, IGetUsersChildMenuPlugin
    {
        public TBaseResult<PagesQueryItem> GetUsersChildMenuList(string safetySecretKey, bool isUsable, int id)
        {
            this.SafetySecretKey = safetySecretKey;
            this.Usable = isUsable ? BasePluginType.Type.启用 : BasePluginType.Type.卸载;
            var result = new TBaseResult<PagesQueryItem>()
            {
                Code = (int)EnumCore.CodeType.失败,
                Message = "系统错误",
                TList = new List<PagesQueryItem>()
            };
            if (string.IsNullOrWhiteSpace(this.SafetySecretKey) || this.Usable == BasePluginType.Type.卸载 || PluginCore.GetInstance.VerifySafetySecretKey(this.SafetySecretKey))
            {
                return result;
            }
            var list = MenuManager.GetInstance.GetPagesChildList(id);
            result.Code = (int)EnumCore.CodeType.成功;
            result.TList = list;
            if (list.Count <= 0)
            {
                result.Message = "暂无数据";
                return result;
            }
            result.Message = "获取成功";
            return result;
        }

    }
}
