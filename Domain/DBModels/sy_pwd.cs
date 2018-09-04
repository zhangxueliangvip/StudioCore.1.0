using System;
namespace Domain.Models
{    ///<summary>
    ///
    ///</summary>
    public partial class sy_pwd:BaseDBModel
    {
        public sy_pwd(){


           }

           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ID {get;set;}

           /// <summary>
           /// Desc:唯一标识
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OpenId {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PassWords {get;set;}

           /// <summary>
           /// Desc:密码类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int PwdType {get;set;}

           /// <summary>
           /// Desc:Yes/No(1/0)
           /// Default:b'0'
           /// Nullable:False
           /// </summary>           
           public bool IsDeleted {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CTime {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime UTime {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:关联OpenId
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string JoinOpenId {get;set;}

    }
}