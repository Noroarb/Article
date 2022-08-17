using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class RestaurantProperties : IEntityBase
    {

        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int PropertyId { get; set; }


        public virtual Restaurants Restaurant { set; get; }
        public virtual Properties Property { set; get; }
        public virtual ICollection<PropertyValues> PropertyValues { set; get; }

    }
}
