using Domain.Models;
using Infrastructure.Utility;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.QueryModels
{
    [SugarTable("sy_pages")]
    public  class PagesQueryItem : sy_pages
    {

        #region sql
        public string Sql = "select * from sy_pages as t where t.IsDeleted=false";
        #endregion
        #region Page
        [SugarColumn(IsIgnore = true)]
        public List<PagesQueryItem> PageData { get; set; }
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
        public string GetPageTypeStr
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.OpenId) || this.PageTypes <= 0)
                {
                    return string.Empty;
                }
                if (this.PageTypes == (int)EnumCore.PagesType.后台)
                {
                    return string.Format("<span class=\"layui-badge layui-bg-green\">后台</span>");
                }
                return string.Format("<span class=\"layui-badge layui-bg-blue\">前台</span>");
            }
        }


        [SugarColumn(IsIgnore = true)]
        public string GetIconClass
        {
            get
            {
                if (string.IsNullOrWhiteSpace(OpenId) && PageIcon <= 0)
                {
                    return "layui-icon-home";
                }

                var entity = DBCore.Queryable<sy_icons>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.ID == this.PageIcon).First() ?? new sy_icons();
                return entity.IconClass;
            }
        }
    }
}
