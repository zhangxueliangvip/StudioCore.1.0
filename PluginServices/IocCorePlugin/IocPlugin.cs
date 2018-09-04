using Autofac;
using AutofacProxy;
using ClientsPlugin.AdminServicesPlugin;
using IocCoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocCorePlugin
{
   public class IocPlugin:IocRepository
    {


        #region 实例
        public static ILoginPlugin IocLoginPlugin => IocCore.Container.Resolve<ILoginPlugin>();
        public static IVerificationCodePlugin IocVerificationCodePlugin => IocCore.Container.Resolve<IVerificationCodePlugin>();
        public static IUpPwdPlugin IocUpPwdPlugin => IocCore.Container.Resolve<IUpPwdPlugin>();
        public static IGetUsersMenuPlugin IocGetUsersMenuPlugin => IocCore.Container.Resolve<IGetUsersMenuPlugin>();



        public static IAddDataRolesPlugin IocAddDataRolesPlugin => IocCore.Container.Resolve<IAddDataRolesPlugin>();
        public static IAddDataPagesPlugin IocAddDataPagesPlugin => IocCore.Container.Resolve<IAddDataPagesPlugin>();
        public static IAddDataIconsPlugin IocAddDataIconsPlugin => IocCore.Container.Resolve<IAddDataIconsPlugin>();
        public static IAddDataOperationsPlugin IocAddDataOperationsPlugin => IocCore.Container.Resolve<IAddDataOperationsPlugin>();
        public static IAddDataUsersPlugin IocAddDataUsersPlugin => IocCore.Container.Resolve<IAddDataUsersPlugin>();





        public static IGetPageDataRolesPlugin IocGetPageDataRolesPlugin => IocCore.Container.Resolve<IGetPageDataRolesPlugin>();
        public static IGetPageDataPagesPlugin IocGetPageDataPagesPlugin => IocCore.Container.Resolve<IGetPageDataPagesPlugin>();
        public static IGetPageDataLogsPlugin IocGetPageDataLogsPlugin => IocCore.Container.Resolve<IGetPageDataLogsPlugin>();
        public static IGetPageDataIconsPlugin IocGetPageDataIconsPlugin => IocCore.Container.Resolve<IGetPageDataIconsPlugin>();
        public static IGetPageDataOperationsPlugin IocGetPageDataOperationsPlugin => IocCore.Container.Resolve<IGetPageDataOperationsPlugin>();
        public static IGetPageDataUsersPlugin IocGetPageDataUsersPlugin => IocCore.Container.Resolve<IGetPageDataUsersPlugin>();



        public static IGetRolesInfoPlugin IocGetRolesInfoPlugin => IocCore.Container.Resolve<IGetRolesInfoPlugin>();
        public static IGetUsersInfoPlugin IocGetUsersInfoPlugin => IocCore.Container.Resolve<IGetUsersInfoPlugin>();
        public static IGetPagesInfoPlugin IocGetPagesInfoPlugin => IocCore.Container.Resolve<IGetPagesInfoPlugin>();
        public static IGetIconsInfoPlugin IocGetIconsInfoPlugin => IocCore.Container.Resolve<IGetIconsInfoPlugin>();
        public static IGetOperationsInfoPlugin IocGetOperationsInfoPlugin => IocCore.Container.Resolve<IGetOperationsInfoPlugin>();
        
       



        
        public static IDeleteDataRolesPlugin IocDeleteDataRolesPlugin => IocCore.Container.Resolve<IDeleteDataRolesPlugin>();
        public static IDeleteDataPagesPlugin IocDeleteDataPagesPlugin => IocCore.Container.Resolve<IDeleteDataPagesPlugin>();
        public static IDeleteDataIconsPlugin IocDeleteDataIconsPlugin => IocCore.Container.Resolve<IDeleteDataIconsPlugin>();
        public static IDeleteDataOperationsPlugin IocDeleteDataOperationsPlugin => IocCore.Container.Resolve<IDeleteDataOperationsPlugin>();
        public static IDeleteDataUsersPlugin IosDeleteDataUsersPlugin => IocCore.Container.Resolve<IDeleteDataUsersPlugin>();




        public static ISetPllocationRolesCheckPlugin IocSetPllocationRolesCheckPlugin => IocCore.Container.Resolve<ISetPllocationRolesCheckPlugin>();
        public static ISetPllocationUsersCheckPlugin IocSetPllocationUsersCheckPlugin => IocCore.Container.Resolve<ISetPllocationUsersCheckPlugin>();




        #endregion
    }
}
