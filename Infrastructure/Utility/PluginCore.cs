using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility
{
    public class PluginCore
    {
        #region 单例模式
        private static readonly PluginCore instance = new PluginCore();
        private PluginCore() { }
        public static PluginCore GetInstance => instance;
        #endregion
        /// <summary>
        /// 安全秘钥值
        /// </summary>
       public string SafetySecretValue => string.Format("{0}_{1}", Guid.NewGuid().ToString(), "@#$%^&*!");
        /// <summary>
        /// 安全秘钥（Redis获取）
        /// </summary>
        string SafetySecretKey => RedisCore.GetInstance.Get<string>(Config.SafetySecretKey);
        /// <summary>
        /// 安全秘钥（Redis设置）
        /// </summary>
        /// <returns></returns>
        string SetSafetySecretKey()
        {
            RedisCore.GetInstance.Set(Config.SafetySecretKey, SafetySecretValue, 3600);
            return SafetySecretKey;
        }
        /// <summary>
        /// 获取安全秘钥
        /// </summary>
        public string GetSafetySecretKey => string.IsNullOrWhiteSpace(SafetySecretKey) ? SetSafetySecretKey() : SafetySecretKey;
        /// <summary>
        /// 验证安全秘钥
        /// </summary>
        /// <param name="safetySecretKey"></param>
        /// <returns></returns>
        public bool VerifySafetySecretKey(string safetySecretKey)
        {
            return safetySecretKey == SafetySecretKey;
        }
    }
}
