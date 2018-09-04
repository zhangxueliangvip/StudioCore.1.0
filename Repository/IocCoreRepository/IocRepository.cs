using Autofac;
using AutofacProxy;
using IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocCoreRepository
{
   public class IocRepository
    {
        #region 实例
        public static IIconsRepository IocIconsRepository => IocCore.Container.Resolve<IIconsRepository>();
        public static ILogRecordRepository IocLogRecordRepository => IocCore.Container.Resolve<ILogRecordRepository>();
        public static IOperationsRepository IocOperationsRepository => IocCore.Container.Resolve<IOperationsRepository>();
        public static IPagesRepository IocPagesRepository => IocCore.Container.Resolve<IPagesRepository>();
        public static IPwdRepository IocPwdRepository => IocCore.Container.Resolve<IPwdRepository>();
        public static IRolesRepository IocRolesRepository => IocCore.Container.Resolve<IRolesRepository>();
        public static IUsersRepository IocUsersRepository => IocCore.Container.Resolve<IUsersRepository>();

        #endregion
    }
}
