using BaseRepositorys;
using Domain.DTOModels;
using Domain.Models;
using IRepositorys;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys
{
    public class LogRecordRepository : ILogRecordRepository
    {
        #region DBInstance
        protected SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        #endregion

        #region 添加
        public int InsertInt(LogRecordModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommand();
        }
        public long InsertLong(LogRecordModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
        }
        public LogRecordModels InsertEntity(LogRecordModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnEntity();
        }
        public bool InsertBool(LogRecordModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public int InsertList(List<LogRecordModels> list)
        {
            return DBCore.Insertable(list.ToArray()).With(SqlWith.UpdLock).ExecuteCommand();
        }
        #endregion

        #region 修改
        public int Update(LogRecordModels entity)
        {
            return DBCore.Updateable(entity).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        public int UpdateList(List<LogRecordModels> list)
        {
            return DBCore.Updateable(list).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        #endregion

        #region 删除
        public int Delete(LogRecordModels entity)
        {
            return DBCore.Deleteable(entity).With(SqlWith.RowLock).ExecuteCommand();
        }
        public int DeleteById(int id)
        {
            return DBCore.Deleteable<LogRecordModels>().With(SqlWith.RowLock).In(id).ExecuteCommand();
        }
        public int DeleteList(List<LogRecordModels> list)
        {
            return DBCore.Deleteable<LogRecordModels>().With(SqlWith.RowLock).Where(list).ExecuteCommand();
        }
        public int DeleteListByIds(List<int> ids)
        {
            return DBCore.Deleteable<LogRecordModels>().With(SqlWith.RowLock).In(ids).ExecuteCommand();
        }


        #endregion

        #region 查询
        public List<LogRecordModels> GetTopList(int num)
        {
            return DBCore.Queryable<LogRecordModels>().With(SqlWith.NoLock).Take(num).ToList() ?? new List<LogRecordModels>();
        }

        public LogRecordModels GetById(int id)
        {
            return DBCore.Queryable<LogRecordModels>().With(SqlWith.NoLock).InSingle(id) ?? new LogRecordModels();
        }

        public List<LogRecordModels> GetAll()
        {
            return DBCore.Queryable<LogRecordModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<LogRecordModels>();
        }


        public LogRecordModels GetByOpenId(string openId)
        {
            return DBCore.Queryable<LogRecordModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == openId).First() ?? new LogRecordModels();
        }



        #endregion

        #region Business
       
        #endregion

    }
}
