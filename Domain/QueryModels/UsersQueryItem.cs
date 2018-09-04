using Domain.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.QueryModels
{
    [SugarTable("sy_users")]
    public  class UsersQueryItem : sy_users
    {
        #region sql
        public string Sql = "select * from sy_users as t where t.IsDeleted=false";
        #endregion

        #region Page
        [SugarColumn(IsIgnore = true)]
        public List<UsersQueryItem> PageData { get; set; }
        #endregion

        #region Query
        [SugarColumn(IsIgnore = true)]
        public string UserPwd { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string ValidateCode { get; set; }
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
    }
}
