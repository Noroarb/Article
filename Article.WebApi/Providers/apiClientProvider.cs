using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Http;

// Add for Identity/Token Deserialization:
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin.Infrastructure;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

namespace Article.WebApi.Providers
{
    public class apiClientProvider
    {
        public static string Token(string userName)
        {
        var identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
        identity.AddClaim(new Claim(ClaimTypes.Name, userName));
        var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
        var currentUtc = new SystemClock().UtcNow;
        ticket.Properties.IssuedUtc = currentUtc;
        ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));
        var token = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
        return token;
    }

    public static void ConfigureWebApiToUseOnlyBearerTokenAuthentication(HttpConfiguration http)
    {
        http.SuppressDefaultHostAuthentication();
        http.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
    }
}
}