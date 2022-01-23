using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Models.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleId { get; set; }
        private List<string> GetCredentialByLoggedInUser(string username) 
        {
            var credentials = (List<string>)HttpContext.Current.Session[CommonConstants.SESSION_CREDENTIALS];
            return credentials;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserSession)HttpContext.Current.Session[CommonConstants.USER_SESSION];
            List<string> authorizeLevels = this.GetCredentialByLoggedInUser(session.Username);
            if(session == null)
            {
                return false;
            }
            if (authorizeLevels.Contains(this.RoleId) || session.GroupId == CommonConstants.ADMIN_GROUP)
            {
                return true;
            }
            else return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };
        }
    }
}
