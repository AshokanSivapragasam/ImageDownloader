using System;
using System.Web;
using System.Web.Mvc;

namespace TodoListService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    /*[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        /// <summary>
        /// By Default, MVC returns a 401 Unauthorized when a user's roles do not meet the AuthorizeAttribute requirements.
        /// This initializes a re-authentication request to our identity provider.  Since the user is already logged in, 
        /// AAD returns to the same page, which then issues another 401, creating a redirect loop.
        /// Here, we override the AuthorizeAttribute's HandleUnauthorizedRequest method to show something that makes 
        /// sense in the context of our application.
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }*/
}
