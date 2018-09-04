using Autofac;
using AutofacProxy;
using BaseRepositorys;
using Domain.DTOModels;
using Domain.Models;
using Domain.QueryModels;
using Infrastructure.Utility;
using IocCoreRepository;
using IRepositorys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminManager
{
    public class MenuManager
    {
        #region 单例模式
        private static readonly MenuManager instance = new MenuManager();
        private MenuManager() { }
        public static MenuManager GetInstance => instance;
        #endregion

        #region 操作管理
        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddAndUpOperations(OperationsModels entity, out string message)
        {
            message = "系统错误";
            entity.UTime = DateTime.Now;
            if (string.IsNullOrWhiteSpace(entity.OperationTitle))
            {
                message = "名称错误";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.OpenId))
            {
                entity.CTime = DateTime.Now;
                entity.IsDeleted = false;
                entity.OpenId = Guid.NewGuid().ToString();
                entity.Remark = "图标数据";
                if (IocRepository.IocOperationsRepository.InsertBool(entity))
                {
                    message = "添加成功";
                    return true;
                }
                return false;
            }
            var upEntity = GetOperationsModel(entity.OpenId) ?? new OperationsModels();
            if (string.IsNullOrWhiteSpace(upEntity.OpenId))
            {
                return false;
            }
            upEntity.OperationTitle = entity.OperationTitle;
            upEntity.UTime = entity.UTime;
            if (IocRepository.IocOperationsRepository.Update(upEntity) > 0)
            {
                message = "修改成功";
                return true;
            }
            return false;

        }

        public bool DeletedOperations(out string message, string openId = "")
        {
            message = "系统错误";
            if (string.IsNullOrWhiteSpace(openId))
            {
                return false;
            }
            var deEntity = GetOperationsModel(openId) ?? new OperationsModels();
            if (string.IsNullOrWhiteSpace(deEntity.OpenId))
            {
                return false;
            }
            deEntity.UTime = DateTime.Now;
            deEntity.IsDeleted = true;
            if (IocRepository.IocOperationsRepository.Update(deEntity) <= 0)
            {
                return false;
            }
            message = "删除成功";
            return true;

        }

        public OperationsQueryItem GetPageDataOperations(OperationsQueryItem queryItem)
        {
            if (!string.IsNullOrWhiteSpace(queryItem.OperationTitle))
            {
                queryItem.Sql += string.Format(" and t.OperationTitle like '{0}'", queryItem.OperationTitle);
            }
            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && queryItem.EndCTime != null && queryItem.EndCTime.Year > 2000)
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}' and t.CTime<='{1}'", queryItem.StartCTime, queryItem.EndCTime);
            }
            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && (queryItem.EndCTime == null || queryItem.EndCTime.Year <= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}'", queryItem.StartCTime);
            }
            if ((queryItem.StartCTime == null || queryItem.StartCTime.Year <= 2000) && (queryItem.EndCTime != null && queryItem.EndCTime.Year >= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime<='{0}'", queryItem.EndCTime);
            }
            queryItem.PageData = PageCore<OperationsQueryItem>.GetInstance.GetPageData(queryItem.Sql, queryItem.Page, queryItem.Limit, ref queryItem.Totals);
            return queryItem;
        }

        public OperationsModels GetOperationsModel(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                return new OperationsModels();
            }
            return IocRepository.IocOperationsRepository.GetByOpenId(openId);
        }


        #endregion

        #region 图标管理
        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddAndUpIcons(IconModels entity, out string message)
        {
            message = "系统错误";
            entity.UTime = DateTime.Now;
            if (string.IsNullOrWhiteSpace(entity.IconName))
            {
                message = "名称错误";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.IconClass))
            {
                message = "Class错误";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.OpenId))
            {
                entity.CTime = DateTime.Now;
                entity.IsDeleted = false;
                entity.OpenId = Guid.NewGuid().ToString();
                entity.Remark = "图标数据";
                if (IocRepository.IocIconsRepository.InsertBool(entity))
                {
                    message = "添加成功";
                    return true;
                }
                return false;
            }
            var upEntity = GetIconsModel(entity.OpenId) ?? new IconModels();
            if (string.IsNullOrWhiteSpace(upEntity.OpenId))
            {
                return false;
            }
            upEntity.IconClass = entity.IconClass;
            upEntity.IconName = entity.IconName;
            upEntity.UTime = entity.UTime;
            if (IocRepository.IocIconsRepository.Update(upEntity) > 0)
            {
                message = "修改成功";
                return true;
            }
            return false;

        }

        public bool DeletedIcons(out string message, string openId = "")
        {
            message = "系统错误";
            if (string.IsNullOrWhiteSpace(openId))
            {
                return false;
            }
            var deEntity = GetIconsModel(openId) ?? new IconModels();
            if (string.IsNullOrWhiteSpace(deEntity.OpenId))
            {
                return false;
            }
            deEntity.UTime = DateTime.Now;
            deEntity.IsDeleted = true;
            if (IocRepository.IocIconsRepository.Update(deEntity) <= 0)
            {
                return false;
            }
            message = "删除成功";
            return true;

        }

        public IconQueryItem GetPageDataIcons(IconQueryItem queryItem)
        {
            if (!string.IsNullOrWhiteSpace(queryItem.IconName))
            {
                queryItem.Sql += string.Format(" and t.IconName like '{0}'", queryItem.IconName);
            }

            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && queryItem.EndCTime != null && queryItem.EndCTime.Year > 2000)
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}' and t.CTime<='{1}'", queryItem.StartCTime, queryItem.EndCTime);
            }
            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && (queryItem.EndCTime == null || queryItem.EndCTime.Year <= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}'", queryItem.StartCTime);
            }
            if ((queryItem.StartCTime == null || queryItem.StartCTime.Year <= 2000) && (queryItem.EndCTime != null && queryItem.EndCTime.Year >= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime<='{0}'", queryItem.EndCTime);
            }
            queryItem.PageData = PageCore<IconQueryItem>.GetInstance.GetPageData(queryItem.Sql, queryItem.Page, queryItem.Limit, ref queryItem.Totals);
            return queryItem;
        }

        public IconModels GetIconsModel(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                return new IconModels();
            }
            return IocRepository.IocIconsRepository.GetByOpenId(openId);
        }


        #endregion

        #region 页面管理
        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddAndUpPages(PagesModels entity, out string message)
        {
            message = "系统错误";
            entity.UTime = DateTime.Now;
            if (string.IsNullOrWhiteSpace(entity.PageTitle))
            {
                message = "名称错误";
                return false;
            }
            if (entity.ParentLevel < 0)
            {
                message = "父级Id错误";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.PagePathUrl))
            {
                entity.PagePathUrl = string.Empty;
            }
            if (entity.PageSort < 0)
            {
                message = "排序码错误";
                return false;
            }
            if (entity.PageTypes < 0)
            {
                message = "类型错误";
                return false;
            }

            if (entity.PageIcon <= 0)
            {
                entity.PageIcon = 0;
            }
            if (string.IsNullOrWhiteSpace(entity.OpenId))
            {
                entity.CTime = DateTime.Now;
                entity.IsDeleted = false;
                entity.OpenId = Guid.NewGuid().ToString();
                entity.Remark = "页面数据";
                if (IocRepository.IocPagesRepository.InsertBool(entity))
                {
                    message = "添加成功";
                    return true;
                }
                return false;
            }
            var upEntity = GetPagesModel(entity.OpenId) ?? new PagesModels();
            if (!string.IsNullOrWhiteSpace(upEntity.OpenId))
            {
                upEntity.PageIcon = entity.PageIcon;
                upEntity.PagePathUrl = entity.PagePathUrl;
                upEntity.PageSort = entity.PageSort;
                upEntity.PageTitle = entity.PageTitle;
                upEntity.PageTypes = entity.PageTypes;
                upEntity.ParentLevel = entity.ParentLevel;
                upEntity.UTime = entity.UTime;
                if (IocRepository.IocPagesRepository.Update(upEntity) > 0)
                {
                    message = "修改成功";
                    return true;
                }
            }
            return false;

        }

        public bool DeletedPages(out string message, string openId = "")
        {
            message = "系统错误";
            if (string.IsNullOrWhiteSpace(openId))
            {
                return false;
            }
            var deEntity = GetPagesModel(openId) ?? new PagesModels();
            if (string.IsNullOrWhiteSpace(deEntity.OpenId))
            {
                return false;
            }
            deEntity.UTime = DateTime.Now;
            deEntity.IsDeleted = true;
            if (IocRepository.IocPagesRepository.Update(deEntity) <= 0)
            {
                return false;
            }
            message = "删除成功";
            return true;

        }

        public PagesQueryItem GetPageDataPages(PagesQueryItem queryItem)
        {
            if (!string.IsNullOrWhiteSpace(queryItem.PageTitle))
            {
                queryItem.Sql += string.Format(" and t.PageTitle like '{0}'", queryItem.PageTitle);
            }
            if (queryItem.PageTypes > 0)
            {
                queryItem.Sql += string.Format(" and t.PageTypes={0}", queryItem.PageTypes);
            }
            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && queryItem.EndCTime != null && queryItem.EndCTime.Year > 2000)
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}' and t.CTime<='{1}'", queryItem.StartCTime, queryItem.EndCTime);
            }
            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && (queryItem.EndCTime == null || queryItem.EndCTime.Year <= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}'", queryItem.StartCTime);
            }
            if ((queryItem.StartCTime == null || queryItem.StartCTime.Year <= 2000) && (queryItem.EndCTime != null && queryItem.EndCTime.Year >= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime<='{0}'", queryItem.EndCTime);
            }
            queryItem.PageData = PageCore<PagesQueryItem>.GetInstance.GetPageData(queryItem.Sql, queryItem.Page, queryItem.Limit, ref queryItem.Totals);
            return queryItem;
        }

        public PagesModels GetPagesModel(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                return new PagesModels();
            }
            return IocRepository.IocPagesRepository.GetByOpenId(openId);
        }
        /// <summary>
        /// 获取用户菜单集合
        /// </summary>
        /// <returns></returns>
        public List<PagesModels> GetUsersMenuList(string userOpenId = "")
        {
            
            var userEntity = IocRepository.IocUsersRepository.GetByOpenId(userOpenId);
            if (string.IsNullOrWhiteSpace(userEntity.OpenId))
            {
                return new List<PagesModels>();
            }
            return userEntity.GetMenu;
        }


        #endregion

        #region 角色管理
        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddAndUpRoles(RolesModels entity, out string message)
        {
            message = "系统错误";
            entity.UTime = DateTime.Now;
            if (string.IsNullOrWhiteSpace(entity.RoleTitle))
            {
                message = "名称错误";
                return false;
            }
            if (entity.ParentLevel < 0)
            {
                entity.ParentLevel = 0;
            }
            if (string.IsNullOrWhiteSpace(entity.OpenId))
            {
                entity.CTime = DateTime.Now;
                entity.IsDeleted = false;
                entity.OpenId = Guid.NewGuid().ToString();
                entity.Remark = "角色数据";
                if (IocRepository.IocRolesRepository.InsertBool(entity))
                {
                    message = "添加成功";
                    return true;
                }
                return false;
            }
            var upEntity = GetRolesModel(entity.OpenId) ?? new RolesModels();
            if (string.IsNullOrWhiteSpace(upEntity.OpenId))
            {
                return false;
            }
            upEntity.ParentLevel = entity.ParentLevel;
            upEntity.RoleTitle = entity.RoleTitle;
            upEntity.UTime = entity.UTime;
            if (IocRepository.IocRolesRepository.Update(upEntity) > 0)
            {
                message = "修改成功";
                return true;
            }
            return false;

        }

        public bool DeletedRoles(out string message, string openId = "")
        {
            message = "系统错误";
            if (string.IsNullOrWhiteSpace(openId))
            {
                return false;
            }
            var deEntity = GetRolesModel(openId) ?? new RolesModels();
            if (string.IsNullOrWhiteSpace(deEntity.OpenId))
            {
                return false;
            }
            deEntity.UTime = DateTime.Now;
            deEntity.IsDeleted = true;
            if (IocRepository.IocRolesRepository.Update(deEntity) <= 0)
            {
                return false;
            }
            message = "删除成功";
            return true;

        }

        public RolesQueryItem GetPageDataRoles(RolesQueryItem queryItem)
        {
            if (!string.IsNullOrWhiteSpace(queryItem.RoleTitle))
            {
                queryItem.Sql += string.Format(" and t.RoleTitle like '{0}'", queryItem.RoleTitle);
            }

            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && queryItem.EndCTime != null && queryItem.EndCTime.Year > 2000)
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}' and t.CTime<='{1}'", queryItem.StartCTime, queryItem.EndCTime);
            }
            if (queryItem.StartCTime != null && queryItem.StartCTime.Year > 2000 && (queryItem.EndCTime == null || queryItem.EndCTime.Year <= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime>='{0}'", queryItem.StartCTime);
            }
            if ((queryItem.StartCTime == null || queryItem.StartCTime.Year <= 2000) && (queryItem.EndCTime != null && queryItem.EndCTime.Year >= 2000))
            {
                queryItem.Sql += string.Format(" and t.CTime<='{0}'", queryItem.EndCTime);
            }
            queryItem.PageData = PageCore<RolesQueryItem>.GetInstance.GetPageData(queryItem.Sql, queryItem.Page, queryItem.Limit, ref queryItem.Totals);
            return queryItem;
        }

        public RolesModels GetRolesModel(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                return new RolesModels();
            }
            return IocRepository.IocRolesRepository.GetByOpenId(openId);
        }


        #endregion

        #region 权限管理

        public bool SetPllocationRolesCheck(AuthRequest entity)
        {

            if (entity.MenuArray != null && entity.MenuArray.Count > 0)
            {
                SetRolesPages(entity.MenuArray, entity.RoleOpenId);
                return true;
            }
            return false;
        }
        public bool SetPllocationUsersCheck(AuthRequest entity)
        {
            if((entity.RoleArray == null || entity.RoleArray.Count<=0)&&(entity.MenuArray == null || entity.MenuArray.Count<= 0))
            {
                return false;
            }
            if (entity.RoleArray != null && entity.RoleArray.Count > 0)
            {
                SetUsersRoles(entity.RoleArray, entity.UserOpenId);
            }

            if (entity.MenuArray != null && entity.MenuArray.Count > 0)
            {
                SetUsersPages(entity.MenuArray, entity.UserOpenId);
            }

            return true;
        }
        private void SetUsersRoles(List<string> rolesList, string usersOpenId)
        {
            IocRepository.IocUsersRepository.DelUsersRoles(usersOpenId);
            foreach (var item in rolesList)
            {
                var entity = new sy_usersroles
                {
                    UsersOpenId = usersOpenId,
                    RolesOpenId = item
                };
                IocRepository.IocUsersRepository.AddUsersRoles(entity);
            }
        }
        private void SetUsersPages(List<MenuModel> list, string usersOpenId)
        {
            IocRepository.IocUsersRepository.DelUsersPages(usersOpenId);
            foreach (var item in list)
            {
                var entity = new sy_userspages
                {
                    UsersOpenId = usersOpenId,
                    PagesOpenId = item.MenuOpenId
                };
                IocRepository.IocUsersRepository.AddUsersPages(entity);

                if (item.PageArray != null && item.PageArray.Count > 0)
                {
                    foreach (var itemchild in item.PageArray)
                    {
                        var entitychild = new sy_userspages
                        {
                            UsersOpenId = usersOpenId,
                            PagesOpenId = itemchild.PageOpenId
                        };
                        IocRepository.IocUsersRepository.AddUsersPages(entitychild);
                        if (itemchild.OperationArray != null && itemchild.OperationArray.Count > 0)
                        {
                            IocRepository.IocPagesRepository.DelPagesOperations(itemchild.PageOpenId);
                            foreach (var itemOperation in itemchild.OperationArray)
                            {
                                var entityOpeartions = new sy_pagesoperations
                                {
                                    PagesOpenId = itemchild.PageOpenId,
                                    JoinOpenId = usersOpenId,
                                    OperationsOpenId = itemOperation.OperationOpenId
                                };
                                IocRepository.IocPagesRepository.AddPagesOperations(entityOpeartions);
                            }
                        }
                    }
                }
            }
        }
        private void SetRolesPages(List<MenuModel> list, string roleOpenId)
        {
            IocRepository.IocRolesRepository.DelRolesPages(roleOpenId);
            foreach (var item in list)
            {
                var entity = new sy_rolespages
                {
                    RolesOpenId = roleOpenId,
                    PagesOpenId = item.MenuOpenId
                };
                IocRepository.IocRolesRepository.AddRolesPages(entity);

                if (item.PageArray != null && item.PageArray.Count > 0)
                {
                    foreach (var itemchild in item.PageArray)
                    {
                        var entitychild = new sy_rolespages
                        {
                            RolesOpenId = roleOpenId,
                            PagesOpenId = itemchild.PageOpenId
                        };
                        
                        IocRepository.IocRolesRepository.AddRolesPages(entitychild);
                        if (itemchild.OperationArray != null && itemchild.OperationArray.Count > 0)
                        {
                            IocRepository.IocPagesRepository.DelPagesOperations(itemchild.PageOpenId);
                            foreach (var itemOperation in itemchild.OperationArray)
                            {
                                var entityOpeartions = new sy_pagesoperations
                                {
                                    PagesOpenId = itemchild.PageOpenId,
                                    JoinOpenId = roleOpenId,
                                    OperationsOpenId = itemOperation.OperationOpenId
                                };
                                IocRepository.IocPagesRepository.AddPagesOperations(entityOpeartions);
                            }
                        }
                    }
                }
            }
        }







        #endregion
    }
}
