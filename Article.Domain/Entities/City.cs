using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class City : IEntityBase
    {
       
        public int Id { get; set; }
        public int Sort { set; get; }
        public virtual ICollection<CityDescription> CityDescription { set; get; }
        public virtual ICollection<Town> Towns { set; get; }
     //   public virtual ICollection<Classify> Classifieds { set; get; }



    }
}
