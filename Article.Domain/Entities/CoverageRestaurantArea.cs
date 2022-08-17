using Market.Common;
using Market.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class CoverageRestaurantArea : IEntityBase
    {
       
        public int Id { set; get; }
       
        /// <summary>
        /// Restaurant Id
        /// </summary>
        public int RestaurantId { set; get; }
        /// <summary>
        /// The areas served by this restaurant
        /// </summary>
        public string CoverageArea { get; set; }

        //public virtual User User { set; get; }
        //public virtual Town Town { set; get; }
        public virtual Restaurants Restaurant { set; get; }
        //public virtual Brands Brands { set; get; }
       
    }
}
