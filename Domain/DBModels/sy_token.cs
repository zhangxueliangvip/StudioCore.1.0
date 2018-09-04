using System;
namespace Domain.Models
{    ///<summary>
    ///
    ///</summary>
    public partial class sy_token:BaseDBModel
    {
        public sy_token(){


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
           /// Desc:Token
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Token {get;set;}

           /// <summary>
           /// Desc:有效期开始时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime StartTime {get;set;}

           /// <summary>
           /// Desc:有效期结束时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime EndTime {get;set;}

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