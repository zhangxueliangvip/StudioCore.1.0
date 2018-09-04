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
    public class DeleteDataIconsPlugin : BasePluginCore, IDeleteDataIconsPlugin
    {
        public BaseResult DeleteDataIcons(string safetySecretKey, bool isUsable, string openId)
        {
            this.SafetySecretKey = safetySecretKey;
            this.Usable = isUsable ? BasePluginType.Type.启用 : BasePluginType.Type.卸载;
            var result = new BaseResult()
            {
                Code = (int)EnumCore.CodeType.失败,
                Message = "系统错误"
            };
            if (string.IsNullOrWhiteSpace(this.SafetySecretKey) || this.Usable == BasePluginType.Type.卸载 || PluginCore.GetInstance.VerifySafetySecretKey(this.SafetySecretKey))
            {
                return result;
            }
            if (string.IsNullOrWhiteSpace(openId))
            {
                return result;
            }
            if (MenuManager.GetInstance.DeletedIcons(out string message, openId))
            {
                result.Code = (int)EnumCore.CodeType.成功;
            }
            result.Message = message;
            return result;
        }


    }
}
