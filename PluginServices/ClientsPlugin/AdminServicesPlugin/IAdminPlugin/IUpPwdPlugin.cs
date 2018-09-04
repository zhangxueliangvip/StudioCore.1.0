using Domain.BaseModels;
using Domain.DTOModels;
using Domain.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsPlugin.AdminServicesPlugin
{
   public interface IUpPwdPlugin
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="safetySecretKey"></param>
        /// <param name="isUsable"></param>
        /// <param name="UserPwd"></param>
        /// <param name="NewUserPwd"></param>
        /// <returns></returns>
        BaseResult UpPwd(string safetySecretKey, bool isUsable, string UserPwd,string NewUserPwd);
    }
}
