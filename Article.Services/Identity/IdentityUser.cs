using Article.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Article.Services.Identity
{
    public class IdentityUser : IUser<Guid>
    {
        public IdentityUser()
        {
            this.Id = Guid.NewGuid();
        }

        public IdentityUser(string userName)
            : this()
        {
            this.UserName = userName;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FullName { get; set; }
        public DateTime CreationDate { get; set; }
        //public byte[] UserPhoto { get; set; } 

        /// <summary>
        /// يجب أن نمنع مستخدم من الطلب من خلال
        /// هذا الحقل ويعبر عن حالة هذا المستخدم إن
        /// كان مسموح له بإرسال الشكاوي والطلبات أم لا
        /// </summary>
        public bool IsActivated { get; set; }

        /// <summary>
        /// رابط الصورة الشخصية على الفيس بوك يمرر مع تسجيل الدخول
        /// </summary>
        public string ProfileImage { get; set; }

        public string FirebaseToken { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IdentityUser, Guid> manager, string authenticationType)
        {
               // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
               var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public ICollection<Role> Role
        {
            set;get;
        }
      //  public virtual ICollection<Restaurants> Restaurants { set; get; }
        //private ICollection<IdentityRole> _IdentityRole;
        //public ICollection<IdentityRole> IdentityRole
        //{
        //    get { return _IdentityRole ?? (_IdentityRole = new List<IdentityRole>()); }
        //    set { _IdentityRole = value; }
        //}
    }
}