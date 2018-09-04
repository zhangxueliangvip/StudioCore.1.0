using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using System.Web.Mvc;

namespace admin.studiocore.com
{
    public class BaseAuthor : AuthorizeAttribute
    {
        public bool AllowAnonymous { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (AllowAnonymous)
                {
                    return;
                }
                var obj = RedisCore.GetInstance.Get(Config.SessionUserInfo);
                var openid = obj == null || obj.ToString() == "null" ? string.Empty : obj.ToString();
                if (!string.IsNullOrWhiteSpace(openid))
                {
                    return;
                }
                RedirectLogin(filterContext);
            }
            catch (Exception ex)
            {
                RedirectError(filterContext);
                LOGCore.Trace(LOGCore.ST.Day, "【BaseAuth】", ex.ToString());
            }

        }
        private void RedirectLogin(AuthorizationContext context)
        {
            context.Result = new RedirectResult(Config.GetLoginUrl);
        }

        private void RedirectError(AuthorizationContext context)
        {
            context.Result = new RedirectResult(Config.GetErrorUrl);
        }

    }
}