using Domain.Models;
using Domain.QueryModels;
using Infrastructure.Utility;
using SqlSugar;
using System.Collections.Generic;

namespace Domain.DTOModels
{
    [SugarTable("sy_pages")]
    public class PagesModels : PagesQueryItem
    {

        #region Business

        [SugarColumn(IsIgnore = true)]
        public List<PagesQueryItem> GetChildList
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.OpenId) || this.ParentLevel == 0)
                {
                    return new List<PagesQueryItem>();
                }
                return DBCore.Queryable<PagesQueryItem>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.ParentLevel == this.ID).OrderBy(o => o.PageSort).ToList() ?? new List<PagesQueryItem>();
            }
        }
        [SugarColumn(IsIgnore = true)]
        public List<sy_operations> GetOperations
        {
            get
            {
                if (string.IsNullOrWhiteSpace(OpenId) || ParentLevel == 0)
                {
                    return new List<sy_operations>();
                }
                return DBCore.Queryable<sy_operations>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<sy_operations>();
            }
        }

       

        [SugarColumn(IsIgnore = true)]
        List<sy_icons> GetIconLists => DBCore.Queryable<sy_icons>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<sy_icons>();

        [SugarColumn(IsIgnore = true)]
        List<sy_pages> GetParentLevelPages => DBCore.Queryable<sy_pages>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.PageTypes == (int)EnumCore.PagesType.后台 && m.ParentLevel == 0).ToList() ?? new List<sy_pages>();

        [SugarColumn(IsIgnore = true)]

        public List<string> GetIConID
        {
            get
            {
                var result = new List<string>
                {
                    string.Format("{0}_{1}", 0, "无")
                };
                var list = GetIconLists;
                if (list.Count <= 0)
                {
                    return result;
                }
                foreach (var item in list)
                {
                    result.Add(string.Format("{0}_{1}", item.ID, item.IconName));
                }
                return result;
            }
        }


        [SugarColumn(IsIgnore = true)]

        public List<string> GetParentID
        {
            get
            {
                var result = new List<string>
                {
                    string.Format("{0}_{1}", 0, "无")
                };
                var list = GetParentLevelPages;
                if (list.Count <= 0)
                {
                    return result;
                }
                foreach (var item in list)
                {
                    result.Add(string.Format("{0}_{1}", item.ID, item.PageTitle));
                }
                return result;
            }
        }
        #endregion

    }
}
