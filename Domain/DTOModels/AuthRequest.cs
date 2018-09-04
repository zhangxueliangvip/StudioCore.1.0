using System.Collections.Generic;

namespace Domain.DTOModels
{
    public class AuthRequest
    {
        public string UserOpenId { get; set; }
        public string RoleOpenId { get; set; }
        public List<string> RoleArray { get; set; }
        public List<MenuModel> MenuArray { get; set; }
    }

    public class MenuModel
    {
        public string MenuTitle { get; set; }

        public string MenuOpenId { get; set; }
        public List<PageData> PageArray { get; set; }
    }

    public class PageData
    {
        public string PageTitle { get; set; }

        public string PageOpenId { get; set; }
        public List<OperationData> OperationArray { get; set; }
    }

    public class OperationData
    {
        public string OperationTitle { get; set; }

        public string OperationOpenId { get; set; }
    }

}
