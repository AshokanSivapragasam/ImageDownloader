using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System.IdentityModel.Tokens;

namespace TodoListService
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        // we inject our own multitenant validation logic
                        //ValidateIssuer = false,
                        SaveSigninToken = true,
                        ValidAudience = ConfigurationManager.AppSettings["ida:Audience"],
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                    }
                });
        }
    }
}
