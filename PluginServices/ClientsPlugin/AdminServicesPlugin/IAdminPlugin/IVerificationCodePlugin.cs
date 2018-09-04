using Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsPlugin.AdminServicesPlugin
{
   public interface IVerificationCodePlugin
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        BaseResult GetCodeImg(string safetySecretKey, bool usable);
    }
}
