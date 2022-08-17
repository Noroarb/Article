using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class PropertyValues : IEntityBase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RestaurantPropertiesId { get; set; }

        /// <summary>
        /// Property Value
        /// </summary>
        public string Value { get; set; }

        public virtual RestaurantProperties RestaurantProperty { set; get; }
        public virtual Products Product { set; get; }

    }
}
