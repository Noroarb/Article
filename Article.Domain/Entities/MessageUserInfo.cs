using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class MessageUserInfo 
    {
    //    public int Id { set; get; }
        public Guid UserId { get; set; }

        public bool IsOnline { get; set; }
        public DateTime  ConnectionDate { get; set; }
    
        public DateTime DisconnectionDate  { get; set; }
       



        public virtual User User { set; get; }
      
       // public virtual ICollection<Notification> Notifications { set; get; }
      //  public virtual ICollection<ClassifyDescription> ClassifyDescriptions { set; get; }

    }
}
