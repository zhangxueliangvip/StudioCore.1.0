using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminManager;
using Domain.BaseModels;
using Domain.QueryModels;
using Infrastructure.Utility;

namespace ClientsPlugin.AdminServicesPlugin
{
    public class LoginPlugin : BasePluginCore, ILoginPlugin
    {
        public BaseResult VerifyLogin(UsersQueryItem entity)
        {
            this.SafetySecretKey = entity.SafetySecretKey;
            this.Usable = entity.IsUsable ? BasePluginType.Type.启用 : BasePluginType.Type.卸载;
            var result = new BaseResult
            {
                Code = (int)EnumCore.CodeType.失败,
                Message = "获取失败"
            };
            if (string.IsNullOrWhiteSpace(this.SafetySecretKey) || this.Usable == BasePluginType.Type.卸载 || PluginCore.GetInstance.VerifySafetySecretKey(this.SafetySecretKey))
            {
                return result;
            }
            if (UsersManager.GetInstance.VerifyUsersInfo(entity, out string message))
            {
                result.Code = (int)EnumCore.CodeType.成功;
            }
            result.Message = message;
            return result;
        }
    }
}
