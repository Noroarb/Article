using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class Properties : IEntityBase
    {

        public int Id { get; set; }
        public int CategoryId { set; get; }
        //public int? ParentID { get; set; }
        /// <summary>
        /// type of this property,
        /// 0--> int, 1--> double, 2-->decimal, 3--> bool, 4--> DateTime, 5--> string, 6-->Guid 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// Type show for user display
        /// 0--> checkbox, 1--> dropdownlist, 2--> TextBox, 3--> Calender
        /// </summary>
        public int TypeShow { get; set; }
        /// <summary>
        /// Order this property
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// Type show for Manager display
        /// 0--> checkbox, 1--> dropdownlist, 2--> TextBox, 3--> Calender
        /// </summary>
        public int TypeManagerShow { get; set; }
        /// <summary>
        /// show that if this property is active or not
        /// </summary>
        public bool IsActive { get; set; }

        //public virtual ICollection<CategoryDescription> CategoryDescriptions { set; get; }
        public virtual Category Category { set; get; }
        public virtual ICollection<RestaurantProperties> RestaurantProperties { set; get; }
        public virtual ICollection<PropertiesDescriptions> PropertiesDescriptions { set; get; }


    }
}
