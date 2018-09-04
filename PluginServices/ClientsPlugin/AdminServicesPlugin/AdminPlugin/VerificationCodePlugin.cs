using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BaseModels;
using Infrastructure.Utility;

namespace ClientsPlugin.AdminServicesPlugin
{
    public class VerificationCodePlugin : BasePluginCore, IVerificationCodePlugin
    {
        public BaseResult GetCodeImg(string safetySecretKey, bool usable)
        {
            this.SafetySecretKey = safetySecretKey;
            this.Usable = usable ? BasePluginType.Type.启用 : BasePluginType.Type.卸载;
            var result = new BaseResult
            {
                Code = (int)EnumCore.CodeType.失败,
                Message = "获取失败"
            };
            if (string.IsNullOrWhiteSpace(this.SafetySecretKey) || this.Usable == BasePluginType.Type.卸载 || PluginCore.GetInstance.VerifySafetySecretKey(this.SafetySecretKey))
            {
                return result;
            }
            var validateCode = string.Empty;
            var bytes = ValidateCode.CreateValidateGraphic(out validateCode, Config.Validate, Config.ValidateWidth, Config.ValidateHeight, Config.ValidateFontsize);
            RedisCore.GetInstance.Set(Config.SessionValidate, validateCode, 3000);
            result.ByData = bytes;
            result.Code = (int)EnumCore.CodeType.成功;
            result.Message = "获取成功";
            return result;

        }

    }
}
