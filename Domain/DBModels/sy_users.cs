using System;
namespace Domain.Models
{    ///<summary>
    ///
    ///</summary>
    public partial class sy_users:BaseDBModel
    {
        public sy_users(){


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
           /// Desc:用户名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string UserName {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int States {get;set;}

           /// <summary>
           /// Desc:类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Types {get;set;}

           /// <summary>
           /// Desc:是否管理员
           /// Default:b'0'
           /// Nullable:False
           /// </summary>           
           public bool IsAdministrator {get;set;}

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

    }
}