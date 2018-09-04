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
    public class AddDataIconsPlugin : BasePluginCore, IAddDataIconsPlugin
    {
        public BaseResult AddDataIcons(IconModels entity)
        {
            this.SafetySecretKey = entity.SafetySecretKey;
            this.Usable = entity.IsUsable ? BasePluginType.Type.启用 : BasePluginType.Type.卸载;
            var result = new BaseResult()
            {
                Code = (int)EnumCore.CodeType.失败,
                Message = "系统错误",
            };
            if (string.IsNullOrWhiteSpace(this.SafetySecretKey) || this.Usable == BasePluginType.Type.卸载 || PluginCore.GetInstance.VerifySafetySecretKey(this.SafetySecretKey))
            {
                return result;
            }
            if (MenuManager.GetInstance.AddAndUpIcons(entity, out string message))
            {
                result.Code = (int)EnumCore.CodeType.成功;
            }
            result.Message = message;
            return result;
        }


    }
}
