using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Domain.Entities
{
    public class User
    {
        #region Fields
        private ICollection<Claim> _claims;
        private ICollection<ExternalLogin> _externalLogins;
        private ICollection<Role> _roles;

      
        public virtual ICollection<Readers> Readers { set; get; }
        public virtual ICollection<UsersArticles> Articles { set; get; }
        public virtual ICollection<Ratings> Ratings { set; get; }
        public virtual ICollection<Comments> Comments { set; get; }
        #endregion

        #region Scalar Properties
        public Guid UserId { get; set; }
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
        public string Location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActivated { get; set; }
        

        public string FirebaseToken { get; set; }
        /// <summary>
        /// Facebook link
        /// </summary>
        public string Facebook { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProfileImage { get; set; }

        #endregion

        #region Navigation Properties
        public virtual ICollection<Claim> Claims
        {
            get { return _claims ?? (_claims = new List<Claim>()); }
            set { _claims = value; }
        }

        public virtual ICollection<ExternalLogin> Logins
        {
            get
            {
                return _externalLogins ??
                    (_externalLogins = new List<ExternalLogin>());
            }
            set { _externalLogins = value; }
        }

        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        }
        #endregion
    }
}
