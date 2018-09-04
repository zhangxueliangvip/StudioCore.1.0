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
    public class SetPllocationUsersCheckPlugin : BasePluginCore, ISetPllocationUsersCheckPlugin
    {

        public BaseResult SetPllocationUsersCheck(string safetySecretKey, bool isUsable,AuthRequest entity)
        {
            this.SafetySecretKey = safetySecretKey;
            this.Usable = isUsable ? BasePluginType.Type.启用 : BasePluginType.Type.卸载;
            var result = new BaseResult()
            {
                Code = (int)EnumCore.CodeType.失败,
                Message = "系统错误",
                Data = new RolesModels()
            };
            if (string.IsNullOrWhiteSpace(this.SafetySecretKey) || this.Usable == BasePluginType.Type.卸载 || PluginCore.GetInstance.VerifySafetySecretKey(this.SafetySecretKey))
            {
                return result;
            }
            if (!MenuManager.GetInstance.SetPllocationUsersCheck(entity))
            {
                return result;
            }
            result.Code = (int)EnumCore.CodeType.成功;
            result.Message = "设置成功";
            return result;
        }
    }
}
