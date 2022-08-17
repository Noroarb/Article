using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    /// <summary>
    /// this table to deny client from join to group more than on time
    /// </summary>
    public class MessageConnectionGroups : IEntityBase
    {
        public int Id { set; get; }
        public Guid UserId { get; set; }
        public string ConnectionId { get; set; }
        public string  GroupName { get; set; }
        public string  IMEI { get; set; }


        public virtual User User { set; get; }
      
       // public virtual ICollection<Notification> Notifications { set; get; }
      //  public virtual ICollection<ClassifyDescription> ClassifyDescriptions { set; get; }

    }
}
