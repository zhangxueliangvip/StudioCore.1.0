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
   public class UsersManager
    {
        #region 单例模式
        private static readonly UsersManager instance = new UsersManager();
        private UsersManager() { }
        public static UsersManager GetInstance => instance;
        #endregion

        #region 登录/注册
        public bool VerifyUsersInfo(UsersQueryItem entity, out string message)
        {
            message = "系统错误";
            if (string.IsNullOrWhiteSpace(entity.UserName))
            {
                message = "用户名不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(entity.UserPwd))
            {
                message = "密码不能为空";
                return false;
            }

            if (string.IsNullOrWhiteSpace(entity.ValidateCode))
            {
                message = "验证码不能为空";
                return false;
            }
            if (!string.Equals(entity.ValidateCode, RedisCore.GetInstance.Get<string>(Config.SessionValidate), StringComparison.CurrentCultureIgnoreCase))
            {
                message = "验证码错误";
                return false;
            }
           var usersModel= IocRepository.IocUsersRepository.GetUsersByName(entity.UserName);
            if (string.IsNullOrWhiteSpace(usersModel.UserName))
            {
                message = "用户名错误";
                return false;
            }

            var pwdModel = IocCore.Container.Resolve<IPwdRepository>().GetEntityByPwd(MD5Core.GetStringMD5(entity.UserPwd));
            if (string.IsNullOrWhiteSpace(pwdModel.OpenId))
            {
                message = "密码错误";
                return false;
            }
            RedisCore.GetInstance.Remove(Config.SessionUserInfo);
            RedisCore.GetInstance.Set(Config.SessionUserInfo, usersModel.OpenId,600);
            message = "登录成功";
            return true;
        }


        #endregion

        #region 用户管理
        /// <summary>
        /// 添加/修改
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddAndUpUsers(UsersModels entity, out string message)
        {
            message = "系统错误";
            entity.UTime = DateTime.Now;
            entity.IsAdministrator = false;
            entity.States = 1;
            entity.Types = 1;
            if (string.IsNullOrWhiteSpace(entity.UserName))
            {
                message = "用户名错误";
                return false;
            }

            if (string.IsNullOrWhiteSpace(entity.UserPwd))
            {
                message = "密码错误";
                return false;
            }

            var pwdEntity = new PwdModels
            {
                UTime = DateTime.Now,
                PassWords = MD5Core.GetStringMD5(entity.UserPwd)
            };
            if (string.IsNullOrWhiteSpace(entity.OpenId))
            {
                entity.CTime = DateTime.Now;
                entity.IsDeleted = false;
                entity.OpenId = Guid.NewGuid().ToString();
                entity.Remark = "后台用户数据";
                pwdEntity.CTime = entity.CTime;
                pwdEntity.JoinOpenId = entity.OpenId;
                pwdEntity.IsDeleted = false;
                pwdEntity.OpenId = Guid.NewGuid().ToString();
                pwdEntity.Remark = "用户密码";
                pwdEntity.PwdType = (int)EnumCore.PwdType.后台登录;
                if (IocRepository.IocUsersRepository.InsertBool(entity) && IocRepository.IocPwdRepository.InsertBool(pwdEntity))
                {
                    message = "添加成功";
                    return true;
                }
                return false;
            }
            var upEntity = GetUsersModel(entity.OpenId) ?? new UsersModels();

            if (string.IsNullOrWhiteSpace(upEntity.OpenId))
            {
                return false;
            }
            var upPwdEntity = GetPwdByUserOpenId(upEntity.OpenId);
            if (string.IsNullOrWhiteSpace(upPwdEntity.OpenId))
            {
                return false;
            }
            upPwdEntity.PassWords = pwdEntity.PassWords;
            upPwdEntity.UTime = entity.UTime;
            upEntity.UserName = entity.UserName;
            upEntity.UTime = entity.UTime;
            if (IocRepository.IocUsersRepository.Update(upEntity) > 0 && IocRepository.IocPwdRepository.Update(upPwdEntity) > 0)
            {
                message = "修改成功";
                return true;
            }
            return false;

        }

        public PwdModels GetPwdByUserOpenId(string userOpenId)
        {
            return IocRepository.IocPwdRepository.GetEntityUserOpenId(userOpenId);
        }

        public bool DeletedUsers(out string message, string openId = "")
        {
            message = "系统错误";
            if (string.IsNullOrWhiteSpace(openId))
            {
                return false;
            }
            var deEntity = GetUsersModel(openId) ?? new UsersModels();
            if (string.IsNullOrWhiteSpace(deEntity.OpenId))
            {
                return false;
            }
            deEntity.UTime = DateTime.Now;
            deEntity.IsDeleted = true;
            if (IocRepository.IocUsersRepository.Update(deEntity) <= 0)
            {
                return false;
            }
            message = "删除成功";
            return true;

        }

        public UsersQueryItem GetPageDataUsers(UsersQueryItem queryItem)
        {
            if (!string.IsNullOrWhiteSpace(queryItem.UserName))
            {
                queryItem.Sql += string.Format(" and t.UserName like '{0}'", queryItem.UserName);
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
            queryItem.PageData = PageCore<UsersQueryItem>.GetInstance.GetPageData(queryItem.Sql, queryItem.Page, queryItem.Limit, ref queryItem.Totals);
            return queryItem;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public UsersModels GetUsersModel(string openId)
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                return new UsersModels();
            }
            return IocRepository.IocUsersRepository.GetByOpenId(openId);
        }


        public bool UpPwd(string userOpenId, string pwd, string newPwd, out string message)
        {
            message = "系统错误";
            if (string.IsNullOrWhiteSpace(userOpenId))
            {
                message = "系统错误";
                return false;
            }
            if (string.IsNullOrWhiteSpace(pwd))
            {
                message = "请输入旧密码";
                return false;
            }
            if (string.IsNullOrWhiteSpace(newPwd))
            {
                message = "请输入密码";
                return false;
            }

            var entity = IocRepository.IocUsersRepository.GetByOpenId(userOpenId);
            
            var pwdEntity = IocRepository.IocPwdRepository.GetEntityUserOpenId(entity.OpenId);
            if (string.IsNullOrWhiteSpace(entity.OpenId) || string.IsNullOrWhiteSpace(pwdEntity.OpenId))
            {
                return false;
            }
            if (!pwdEntity.PassWords.Equals(MD5Core.GetStringMD5(pwd)))
            {
                message = "旧密码错误";
                return false;
            }

            pwdEntity.PassWords = MD5Core.GetStringMD5(newPwd);
            pwdEntity.UTime = DateTime.Now;
            if (IocRepository.IocPwdRepository.Update(pwdEntity) <= 0)
            {
                message = "修改失败";
                return false;
            }

            message = "修改成功";
            return true;

        }
        #endregion
    }
}
