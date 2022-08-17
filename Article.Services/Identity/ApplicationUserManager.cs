using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;
using System.Net.Mail;
using Article.Services.Utilities;

namespace Article.Services.Identity
{
    public class ApplicationUserManager : UserManager<IdentityUser, Guid>
    {
        public ApplicationUserManager(IUserStore<IdentityUser, Guid> Store)
            : base(Store) {
                var manager = this;
                // Configure validation logic for usernames
                manager.UserValidator = new UserValidator<IdentityUser, Guid>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = true,
                    RequireLowercase = false,
                    RequireUppercase = false,
                };

                // Configure user lockout defaults
                manager.UserLockoutEnabledByDefault = true;
                manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(10);
                manager.MaxFailedAccessAttemptsBeforeLockout = 5;

                // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
                // You can write your own provider and plug it in here.
                manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<IdentityUser, Guid>
                {
                    MessageFormat = "Your security code is {0}"
                });
                manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<IdentityUser, Guid>
                {
                    Subject = "Security Code",
                    BodyFormat = "Your security code is {0}"
                });
                manager.EmailService = new EmailService();
                manager.SmsService = new SmsService();

            var provider = new MachineKeyProtectionProvider();//new DpapiDataProtectionProvider("Article"); 
            manager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser, Guid>(provider.Create("UserToken"))
                    as IUserTokenProvider<IdentityUser, Guid>;
            //this.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser, Guid>(provider.Create("EmailConfirmation"))
            //{
            //    TokenLifespan = TimeSpan.FromHours(24),
            //};
            //var dataProtectionProvider = options.DataProtectionProvider;
            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<IdentityUser, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}


        }
        //public override Task<IdentityResult> AddToRoleAsync(Guid userId, string role)
        //{
        //    base.ad
        //    return base.AddToRoleAsync(userId, role);
        //}
        //public override Task<IdentityUser> FindAsync(string userName, string password)
        //{
        //    return base.FindAsync(userName, password);
        //}

        public async override Task<IdentityResult> AccessFailedAsync(Guid userId)
        {
           var user_=await base.FindByIdAsync(userId);
            user_.AccessFailedCount++;
            user_.LockoutEnabled = true;
            if (user_.AccessFailedCount >= this.MaxFailedAccessAttemptsBeforeLockout)
            {
                user_.LockoutEndDateUtc = DateTime.Now.Add(this.DefaultAccountLockoutTimeSpan);
            }
            await base.UpdateAsync(user_);
            return base.AccessFailedAsync(userId).Result;
        }
        
    }
}