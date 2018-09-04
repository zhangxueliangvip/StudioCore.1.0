using Domain.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.QueryModels
{
    [SugarTable("sy_logrecord")]
    public  class LogRecordQueryItem: sy_logrecord
    {
        #region sql
        public string Sql = "select * from sy_logrecord as t where t.IsDeleted=false";
        #endregion
        #region Page
        [SugarColumn(IsIgnore = true)]
        public List<LogRecordQueryItem> PageData { get; set; }
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
