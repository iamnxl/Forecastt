using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        private int role;
        public CustomAuthorizeAttribute(int role)
        {
            this.role = role;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IPrincipal user = httpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                if (DBContext.getRoleByUserName(user.Identity.Name) == role)
                {
                    return true;
                }
            }
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ViewResult() { ViewName = "Unauthorize" };
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}