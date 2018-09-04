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
    public class PagesRepository : IPagesRepository
    {
        #region DBInstance
        protected SqlSugarClient DBCore => Infrastructure.Utility.DBCore.GetInstance.GetDB;
        #endregion

        #region 添加
        public int InsertInt(PagesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommand();
        }
        public long InsertLong(PagesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
        }
        public PagesModels InsertEntity(PagesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteReturnEntity();
        }
        public bool InsertBool(PagesModels entity)
        {
            return DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }
        public int InsertList(List<PagesModels> list)
        {
            return DBCore.Insertable(list.ToArray()).With(SqlWith.UpdLock).ExecuteCommand();
        }
        #endregion

        #region 修改
        public int Update(PagesModels entity)
        {
            return DBCore.Updateable(entity).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        public int UpdateList(List<PagesModels> list)
        {
            return DBCore.Updateable(list).With(SqlWith.UpdLock).Where(true).ExecuteCommand();
        }
        #endregion

        #region 删除
        public int Delete(PagesModels entity)
        {
            return DBCore.Deleteable(entity).With(SqlWith.RowLock).ExecuteCommand();
        }
        public int DeleteById(int id)
        {
            return DBCore.Deleteable<PagesModels>().With(SqlWith.RowLock).In(id).ExecuteCommand();
        }
        public int DeleteList(List<PagesModels> list)
        {
            return DBCore.Deleteable<PagesModels>().With(SqlWith.RowLock).Where(list).ExecuteCommand();
        }
        public int DeleteListByIds(List<int> ids)
        {
            return DBCore.Deleteable<PagesModels>().With(SqlWith.RowLock).In(ids).ExecuteCommand();
        }


        #endregion

        #region 查询
        public List<PagesModels> GetTopList(int num)
        {
            return DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Take(num).ToList() ?? new List<PagesModels>();
        }

        public PagesModels GetById(int id)
        {
            return DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).InSingle(id) ?? new PagesModels();
        }

        public List<PagesModels> GetAll()
        {
            return DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false).ToList() ?? new List<PagesModels>();
        }


        public PagesModels GetByOpenId(string openId)
        {
            return DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.OpenId == openId).First() ?? new PagesModels();
        }

        #endregion

        #region Business
        public List<PagesModels> GetPagesAll()
        {
            return DBCore.Queryable<PagesModels>().With(SqlWith.NoLock).Where(m => m.IsDeleted == false && m.ParentLevel == 0).ToList() ?? new List<PagesModels>();
        }

        public void AddPagesOperations(sy_pagesoperations entity)
        {
            entity.CTime = DateTime.Now;
            entity.IsDeleted = false;
            entity.OpenId = Guid.NewGuid().ToString();
            entity.Remark = "页面操作数据";
            entity.UTime = DateTime.Now;
            DBCore.Insertable(entity).With(SqlWith.UpdLock).ExecuteCommandIdentityIntoEntity();
        }

        public void DelPagesOperations(string usersOpenId)
        {
            DBCore.Deleteable<sy_userspages>().With(SqlWith.RowLock).Where(m => m.UsersOpenId == usersOpenId).ExecuteCommand();
        }
        #endregion

    }
}
