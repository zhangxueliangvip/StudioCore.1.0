using System;
namespace Domain.Models
{    ///<summary>
    ///
    ///</summary>
    public partial class sy_roles:BaseDBModel
    {
        public sy_roles(){


           }

           /// <summary>
           /// Desc:
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
           /// Desc:角色名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RoleTitle {get;set;}

           /// <summary>
           /// Desc:父级ID
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int ParentLevel {get;set;}

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