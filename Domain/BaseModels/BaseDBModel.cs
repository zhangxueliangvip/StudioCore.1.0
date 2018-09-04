using SqlSugar;
using System;

namespace Domain.Models
{
    public class BaseDBModel
    {
        #region Page
        [SugarColumn(IsIgnore = true)]
        public int Page { get; set; }
        [SugarColumn(IsIgnore = true)]
        public int Limit { get; set; }
        public int Totals = 0;
        #endregion

        #region DBInstance
        [SugarColumn(IsIgnore = true)]
        public SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        #endregion

        #region Query
        [SugarColumn(IsIgnore = true)]
        public DateTime StartCTime { get; set; }
        [SugarColumn(IsIgnore = true)]
        public DateTime EndCTime { get; set; }
        #endregion

        #region Safety
        [SugarColumn(IsIgnore = true)]
        public string SafetySecretKey { get; set; }
        [SugarColumn(IsIgnore = true)]
        public bool IsUsable { get; set; }
        #endregion
    }
}
