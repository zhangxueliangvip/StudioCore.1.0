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
    public class UpPwdPlugin : BasePluginCore, IUpPwdPlugin
    {

        public BaseResult UpPwd(string safetySecretKey, bool isUsable, string UserPwd, string NewUserPwd)
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

            var obj = RedisCore.GetInstance.Get(Config.SessionUserInfo);
            var userOpenId = obj == null || obj.ToString() == "null" ? string.Empty : obj.ToString();
            var message = string.Empty;
            if (UsersManager.GetInstance.UpPwd(userOpenId, UserPwd, NewUserPwd, out message))
            {
                result.Code = (int)EnumCore.CodeType.成功;
            }
            result.Message = message;
            return result;
          
        }
    }
}
