using Domain.Models;
using Domain.QueryModels;
using SqlSugar;
using System.Collections.Generic;

namespace Domain.DTOModels
{
    [SugarTable("sy_roles")]
    public class RolesModels : RolesQueryItem
    {

        #region Business
        [SugarColumn(IsIgnore = true)]
        public List<PagesQueryItem> GetPagesList => DBCore.Queryable<PagesQueryItem>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.ParentLevel == 0).ToList() ?? new List<PagesQueryItem>();

        [SugarColumn(IsIgnore = true)]
        List<sy_roles> GetRolesLists => DBCore.Queryable<sy_roles>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.ParentLevel == 0).ToList() ?? new List<sy_roles>();
        [SugarColumn(IsIgnore = true)]
        public List<string> GetParentLevelID
        {
            get
            {
                var result = new List<string>
                {
                    string.Format("{0}_{1}", 0, "无")
                };
                var list = GetRolesLists;
                if (list.Count <= 0)
                {
                    return result;
                }
                foreach (var item in list)
                {
                    result.Add(string.Format("{0}_{1}", item.ID, item.RoleTitle));
                }
                return result;
            }
        }

        public string IsPageCheck(string roleOpenId, string pageOpenId)
        {
           var list= DBCore.Queryable<sy_rolespages>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.RolesOpenId == roleOpenId && m.PagesOpenId == pageOpenId).ToList() ?? new List<sy_rolespages>();
           return list.Count > 0 ? "checked=\"checked\"" : string.Empty;
        }

        public string IsOperationCheck(string joinOpenId,string pageOpenId, string operationOpenId)
        {
            var list = DBCore.Queryable<sy_pagesoperations>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.JoinOpenId == joinOpenId && m.PagesOpenId == pageOpenId&&m.OperationsOpenId==operationOpenId).ToList() ?? new List<sy_pagesoperations>();
            return list.Count > 0 ? "checked=\"checked\"" : string.Empty;
        }
        #endregion

    }
}
