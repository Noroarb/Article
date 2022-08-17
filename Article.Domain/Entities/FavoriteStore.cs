using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class FavoriteRestaurant : IEntityBase
    {
        public int Id { set; get; }
        public Guid UserId { get; set; }
        public int ProductId { set; get; }
        public DateTime Date { set; get; }
       

        public virtual User User { set; get; }
     
        public virtual Products Product { set; get; }
        
        //  public virtual ICollection<ClassifyDescription> ClassifyDescriptions { set; get; }

    }
}
