using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class Town : IEntityBase
    {

        public int Id { get; set; }
        public int CityId { set; get; }
        //public string Gps_Latitude { set; get; }
        //public string Gps_Longitude { set; get; }
        //public string Place_Id { set; get; }

        public virtual City City { set; get; }
        public virtual ICollection<TownDescription> TownDescriptions { set; get; }
        ////public virtual ICollection<GuideNeighborhood> GuideNeighborhoods { set; get; }
        //public virtual ICollection<Restaurants> Restaurants { set; get; } //
      //  public virtual ICollection<GuideClassify> GuideClassifieds { set; get; }


    }
}
