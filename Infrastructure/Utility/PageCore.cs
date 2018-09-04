using SqlSugar;
using System.Collections.Generic;

namespace Infrastructure.Utility
{
    public class PageCore<T> where T : class, new()
    {
        #region 单例模式
        private static readonly PageCore<T> instance = new PageCore<T>();
        private PageCore() { }
        public static PageCore<T> GetInstance => instance;
        #endregion

        public SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        public List<T> GetPageData(string sql,int page,int limit ,ref int totals)
        {
            return DBCore.SqlQueryable<T>(sql).With(SqlWith.NoLock).ToPageList(page,limit, ref totals);
        }
    }
}
