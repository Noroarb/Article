using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Article.Services.Identity
{
    public class ApplicationSignInManager : SignInManager<IdentityUser, Guid>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            
        }
      
        public override Guid ConvertIdFromString(string id)
        {
            if (string.IsNullOrEmpty(id)) return Guid.Empty;

            return new Guid(id);
        }
        public override string ConvertIdToString(Guid id)
        {
            if (id.Equals(Guid.Empty)) return string.Empty;

            return id.ToString();
        }
        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var res= base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
            return res;
        }

    }
}