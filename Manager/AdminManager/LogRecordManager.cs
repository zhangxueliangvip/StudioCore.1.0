using Autofac;
using AutofacProxy;
using BaseRepositorys;
using Domain.DTOModels;
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
   public class LogRecordManager
    {

        #region 单例模式
        private static readonly LogRecordManager instance = new LogRecordManager();
        private LogRecordManager() { }
        public static LogRecordManager GetInstance => instance;
        #endregion

        #region 添加管理
        public bool AddLog(LogRecordModels entity)
        {
            if (string.IsNullOrWhiteSpace(entity.LogTitle))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.Creater))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.LogContents))
            {
                return false;
            }
            entity.CTime = DateTime.Now;
            entity.IsDeleted = false;
            entity.OpenId = Guid.NewGuid().ToString();
            entity.Types = (int)EnumCore.LogType.正常;
            entity.UTime = DateTime.Now;

            return IocRepository.IocLogRecordRepository.InsertBool(entity);
        }


        #endregion

        #region 日志管理
        public LogRecordQueryItem GetPageDataLogs(LogRecordQueryItem queryItem)
        {
            if (!string.IsNullOrWhiteSpace(queryItem.LogTitle))
            {
                queryItem.Sql += string.Format(" and t.LogTitle like '{0}'", queryItem.LogTitle);
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
            queryItem.PageData = PageCore<LogRecordQueryItem>.GetInstance.GetPageData(queryItem.Sql, queryItem.Page, queryItem.Limit, ref queryItem.Totals);
            return queryItem;
        }

        #endregion
    }
}
