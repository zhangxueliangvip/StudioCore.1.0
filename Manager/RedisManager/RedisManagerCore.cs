using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisManager
{
  public partial class RedisManagerCore
    {
        #region 单例模式
        private static readonly RedisManagerCore instance = new RedisManagerCore();
        private RedisManagerCore() { }
        public static RedisManagerCore GetInstance => instance;
        #endregion
    }
}
