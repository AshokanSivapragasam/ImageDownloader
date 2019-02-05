using Owin;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;

namespace TodoSPA
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // By default, the claims mapping will map claim names in the old format to accommodate older SAML applications.
            // 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role' instead of 'roles'
            // This flag ensures that the ClaimsIdentity claims collection will be built from the claims in the token
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
            new OpenIdConnectAuthenticationOptions
            {
                ClientId = "65ea8d4b-24a0-4be1-b33c-151fa39f0ff1",
                Authority = "https://login.microsoftonline.com/72f988bf-86f1-41af-91ab-2d7cd011db47/", // 72f988bf-86f1-41af-91ab-2d7cd011db47/",
                RedirectUri = "https://localhost:44326",
                PostLogoutRedirectUri = "https://localhost:44326",

                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    NameClaimType = "upn",
                    RoleClaimType = "roles",    // The claim in the Jwt token where App roles are provided.
                },

                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    AuthenticationFailed = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/Error/ShowError?signIn=true&errorMessage=" + context.Exception.Message);
                        return System.Threading.Tasks.Task.FromResult(0);
                    }
                }
            });

            /*app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Audience = ConfigurationManager.AppSettings["ida:Audience"],
                    Tenant = ConfigurationManager.AppSettings["ida:Tenant"]
                });*/
        }
    }
}
