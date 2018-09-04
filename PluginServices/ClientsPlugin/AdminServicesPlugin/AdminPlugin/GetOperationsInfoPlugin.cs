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
    public class GetOperationsInfoPlugin : BasePluginCore, IGetOperationsInfoPlugin
    {

        TBaseResult<OperationsModels> IGetOperationsInfoPlugin.GetIconsInfo(string safetySecretKey, bool isUsable, string openId)
        {
            this.SafetySecretKey = safetySecretKey;
            this.Usable = isUsable ? BasePluginType.Type.启用 : BasePluginType.Type.卸载;
            var result = new TBaseResult<OperationsModels>()
            {
                Code = (int)EnumCore.CodeType.失败,
                Message = "系统错误",
                TData = new OperationsModels()
            };
            if (string.IsNullOrWhiteSpace(this.SafetySecretKey) || this.Usable == BasePluginType.Type.卸载 || PluginCore.GetInstance.VerifySafetySecretKey(this.SafetySecretKey))
            {
                return result;
            }
            if (string.IsNullOrWhiteSpace(openId))
            {
                return result;
            }
            var model = MenuManager.GetInstance.GetOperationsModel(openId);
            if (string.IsNullOrWhiteSpace(model.OpenId))
            {
                return result;
            }
            result.Code = (int)EnumCore.CodeType.成功;
            result.Message = "获取成功";
            result.TData = model;
            return result;
        }
    }
}
