using Card.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class Messaging : IEntityBase
    {
        public int Id { set; get; }
        // public string FirebaseToken { get; set; }
        ///// <summary>
        ///// Order Id, if this message for reply on order
        ///// </summary>
        //public int? OrderId { set; get; }
        //public Guid StoreManagerSenderId { get; set; }
        public Guid ReceiverId { get; set; }
        /// <summary>
        /// Minimum length 3 char, 
        /// Max length 75 char
        /// </summary>
        public string ReceiverName { get; set; }
        ///// <summary>
        ///// Name of this restaurant
        ///// </summary>
        //public string RestaurantName { set; get; }
        /// <summary>
        /// Minimum length 4 Number, 
        /// Max length 10 Number
        /// </summary>
        public string ReceiverPhoneNumber { get; set; }
        ///// <summary>
        ///// Location of user
        ///// </summary>
        //public string Location { get; set; }
        ///// <summary>
        ///// Admin
        ///// </summary>
        //public Guid ReceiverId { get; set; }
        public string Body { get; set; }

        //public bool IsSeen { get; set; }

        public DateTime SendingDate { get; set; }
        ////public int Id { set; get; }
        /////// <summary>
        /////// Ayiosh Id
        /////// </summary>
        ////public int? MessageMobileId { get; set; }
        ////public Guid SenderId { get; set; }
        ///////// <summary>
        ///////// If sender not registered, this field is required, 
        ///////// Minimum length 3 char, 
        ///////// Max length 75 char
        ///////// </summary>
        //////public string SenderName { get; set; }
        ///////// <summary>
        ///////// If sender not registered, this field is required
        ///////// Minimum length 4 Number, 
        ///////// Max length 10 Number
        ///////// </summary>
        //////public string SenderPhoneNumber { get; set; }
        ///////// <summary>
        ///////// Location of user
        ///////// </summary>
        //////public string Location { get; set; }
        /////// <summary>
        /////// Admin
        /////// </summary>
        ////public Guid ReceiverId { get; set; }
        ////public string Body { get; set; }
        ////public bool IsReceived { get; set; }
        ////public bool DeletedByAdmin { get; set; }
        ////public bool DeletedBySender { get; set; }
        ////public bool DeletedByReciever { get; set; }
        ////public bool IsSeen { get; set; }

        ////public string SenderMobileSystem { get; set; }
        ////public DateTime SendingDate { get; set; }
        ////public DateTime ReceivedDate { get; set; }

        ////public DateTime ShowingDate { get; set; }

        //public virtual User StoreManagerSender { set; get; }
        public virtual User Receiver { get; set; }

       // public virtual Orders Order { get; set; }


    }
}
