using Domain.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.QueryModels
{
    [SugarTable("sy_roles")]
    public  class RolesQueryItem : sy_roles
    {
        #region sql
        public string Sql = "select * from sy_roles as t where t.IsDeleted=false";
        #endregion
        #region Page
        [SugarColumn(IsIgnore = true)]
        public List<RolesQueryItem> PageData { get; set; }
        #endregion
        #region Formatting
        [SugarColumn(IsIgnore = true)]
        public string CTimeStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.OpenId))
                {
                    return string.Empty;
                }
                return this.CTime.ToString("yyyy-MM-dd HH:mm");
            }
        }
        [SugarColumn(IsIgnore = true)]
        public string UTimeStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.OpenId))
                {
                    return string.Empty;
                }

                return this.UTime.ToString("yyyy-MM-dd HH:mm");
            }
        }
        #endregion
        [SugarColumn(IsIgnore = true)]
        public string GetParentLevelStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.OpenId) || this.ParentLevel <= 0)
                {
                    return "无";
                }

                var entity = DBCore.Queryable<RolesQueryItem>().With(SqlWith.NoLock).InSingle(this.ParentLevel) ?? new RolesQueryItem();
                return entity.RoleTitle;

            }
        }

    }
}
