using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Article.WebApi.Providers;
using Article.WebApi.Models;
using System.Web.Mvc;
using Article.Services.Identity;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;

namespace Article.WebApi
{

    public partial class Startup
    {

        //  public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
       

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static Func<UserManager<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>> UserManagerFactory { get; set; }
        public static string PublicClientId { get; private set; }
       

// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());
            // Enable the application to use a cookie to Restaurant information for the signed in user
            // and to use a cookie to temporarily Restaurant information about a user logging in with a third party login provider

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenFormat = new TicketDataFormat(app.CreateDataProtector(
               typeof(OAuthAuthorizationServerMiddleware).Namespace,
               "Access_Token", "v1")),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(336),

                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };
         
            //////////////app.UseOAuthBearerAuthentication(OAuthBearerOptions);///////////////////////////////
            // Enable the application to use bearer tokens to authenticate users
            //app.UseOAuthBearerTokens(OAuthOptions);
            ////////////app.UseCookieAuthentication(new CookieAuthenticationOptions());
            ////////////app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            ////////////// Configure the application for OAuth based flow
            //////////////PublicClientId = "self";
            //////////////OAuthOptions = new OAuthAuthorizationServerOptions
            //////////////{
            //////////////    TokenEndpointPath = new PathString("/Token"),

            //////////////    Provider = new ApplicationOAuthProvider(PublicClientId),
            //////////////    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //////////////    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //////////////    // In production mode set AllowInsecureHttp = false
            //////////////    AllowInsecureHttp = true
            //////////////};
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
           
            OAuthBearerOptions.AccessTokenProvider = OAuthOptions.AccessTokenProvider;
            OAuthBearerOptions.AuthenticationMode = OAuthOptions.AuthenticationMode;
            OAuthBearerOptions.AuthenticationType = OAuthOptions.AuthenticationType;
            OAuthBearerOptions.Description = OAuthOptions.Description;
            OAuthBearerOptions.Provider = new CustomBearerAuthenticationProvider();
            OAuthBearerOptions.SystemClock = OAuthOptions.SystemClock;
            OAuthBearerOptions.AccessTokenFormat = OAuthOptions.AccessTokenFormat;
            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            ////////////app.UseOAuthAuthorizationServer(OAuthOptions);
            ////////////app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "521667885220-nm8nj70i0n8q0mdfthgio0ast0fepie9.apps.googleusercontent.com",
            //    ClientSecret = "qy1tFRbE0m84zcXiSpsJsNEZ"
            //});
        }


    }
}
