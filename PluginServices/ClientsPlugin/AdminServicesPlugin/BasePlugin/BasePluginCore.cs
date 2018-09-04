using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsPlugin.AdminServicesPlugin
{
   public class BasePluginCore
    {
        public BasePluginCore()
        {
            this.Usable = BasePluginType.Type.卸载;
            this.SafetySecretKey = null;
        }
        public BasePluginType.Type Usable { get; set; }

        public string SafetySecretKey { get; set; }
    }
}
