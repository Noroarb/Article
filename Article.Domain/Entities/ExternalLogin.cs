using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Domain.Entities
{
    public class ExternalLogin
    {
        private User _user;

        #region Scalar Properties
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual Guid UserId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User User { set; get; }
     
        #endregion
    }
}
