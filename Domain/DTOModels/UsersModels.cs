using Domain.Models;
using Domain.QueryModels;
using SqlSugar;
using System.Collections.Generic;

namespace Domain.DTOModels
{
    [SugarTable("sy_users")]
    public class UsersModels : UsersQueryItem
    {
        #region Business


        [SugarColumn(IsIgnore = true)]
        public List<RolesModels> GetRolesList => DBCore.Queryable<RolesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<RolesModels>();

        public string IsRoleCheck(string userOpenId, string roleOpenId)
        {
            var list = DBCore.Queryable<sy_usersroles>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.RolesOpenId == roleOpenId && m.UsersOpenId == userOpenId).ToList() ?? new List<sy_usersroles>();
            return list.Count > 0 ? "checked=\"checked\"" : string.Empty;
        }
        [SugarColumn(IsIgnore = true)]
        public List<PagesModels> GetPagesList => DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.ParentLevel == 0).ToList() ?? new List<PagesModels>();
        public string IsPageCheck(string usersOpenId, string pageOpenId)
        {
            var list = DBCore.Queryable<sy_userspages>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.UsersOpenId == usersOpenId && m.PagesOpenId == pageOpenId).ToList() ?? new List<sy_userspages>();
            return list.Count > 0 ? "checked=\"checked\"" : string.Empty;
        }
        public string IsOperationCheck(string joinOpenId, string pageOpenId, string operationOpenId)
        {
            var list = DBCore.Queryable<sy_pagesoperations>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.JoinOpenId == joinOpenId && m.PagesOpenId == pageOpenId && m.OperationsOpenId == operationOpenId).ToList() ?? new List<sy_pagesoperations>();
            return list.Count > 0 ? "checked=\"checked\"" : string.Empty;
        }
        [SugarColumn(IsIgnore = true)]

        public List<PagesModels> GetMenu
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.OpenId))
                {
                    return new List<PagesModels>();
                }
                if (this.IsAdministrator)
                {
                    return DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false&&m.ParentLevel==0).ToList() ?? new List<PagesModels>();
                }
                return GetOnlyMenu(this.OpenId);
            }
        }

        private List<PagesModels> GetOnlyMenu(string userOpenId)
        {
            var list = new List<PagesModels>();
            var userPageList = DBCore.Queryable<sy_userspages>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.UsersOpenId == userOpenId).ToList() ?? new List<sy_userspages>();
            if (userPageList.Count > 0)
            {
                foreach (var item in userPageList)
                {
                    var tempEntity= DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == item.PagesOpenId).First()?? new PagesModels();
                    if (!string.IsNullOrWhiteSpace(tempEntity.OpenId)&&!list.Contains(tempEntity))
                    {
                        list.Add(tempEntity);
                    }

                }
            }
            var userRoleList = DBCore.Queryable<sy_usersroles>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.UsersOpenId == userOpenId).ToList() ?? new List<sy_usersroles>();
            if (userRoleList.Count > 0)
            {
                foreach (var item in userRoleList)
                {
                    var tempEntity = DBCore.Queryable<RolesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == item.RolesOpenId).First() ?? new RolesModels();
                    if (!string.IsNullOrWhiteSpace(tempEntity.OpenId))
                    {
                        var rolesPagesList= DBCore.Queryable<sy_rolespages>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.RolesOpenId == item.RolesOpenId).ToList() ??new List<sy_rolespages>();

                        if (rolesPagesList.Count > 0)
                        {
                            foreach (var items in rolesPagesList)
                            {
                                var temEntity = DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == items.PagesOpenId).First() ?? new PagesModels();
                                if (!string.IsNullOrWhiteSpace(temEntity.OpenId) && !list.Contains(temEntity))
                                {
                                    list.Add(temEntity);
                                }

                            }
                        }
                    }
                }
            }

            return list;

        }
        #endregion

    }
}
