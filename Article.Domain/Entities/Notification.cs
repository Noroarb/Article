using Card.Common;
using Card.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class Notification : IEntityBase
    {
        public int Id { set; get; }

        public Guid? ClientId { get; set; }
        /// <summary>
        /// determine type of notification if update=0, product=1
        /// </summary>
        public NotificationType Type { get; set; }

        #region Notify
        /// <summary>
        /// id of this Product
        /// </summary>
        public int? ProductId { get; set; }
        /// <summary>
        /// image path 
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Title of this product
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Description of this product
        /// </summary>
        public string Discription { get; set; }

        #endregion

    

        #region Update
        /// <summary>
        /// If type equal to zero, this field contains URL of new version
        /// </summary>
        public string Update { get; set; }
        #endregion

        public DateTime NotifyDate { set; get; }

        //public TimeSpan Time { set; get; }
        /// <summary>
        /// this value for show if this notify is sent or no
        /// </summary>
        //public bool IsSent { get; set; }

       public virtual User Client { set; get; }

    }
}
