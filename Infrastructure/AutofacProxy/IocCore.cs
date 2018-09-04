using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutofacProxy
{
   public class IocCore
    {
        #region 单例模式
        static readonly IocCore instance = new IocCore();
        IocCore() { }
        public static IocCore Instance => instance;
        #endregion
        static ContainerBuilder builder = new ContainerBuilder();
        static IContainer container;
        public void Register(string[] namespaceStrs)
        {
            
            foreach (var item in namespaceStrs)
            {
                builder.RegisterTypes(Assembly.Load(item).GetTypes()).AsImplementedInterfaces();
            }
            
            container = builder.Build();
        }

        /// <summary>
        /// 获取container
        /// </summary>
        public static IContainer Container
        {
            get
            {
                return container;
            }
        }


    }
}
