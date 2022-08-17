using Market.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Dtos
{
    /// <summary>
    /// this object for get Restaurant as Simplify for user
    /// </summary>
    public class SimplifyRestaurantDto
    {
        /// <summary>
        /// Id Restaurant
        /// </summary>
        public int Id { set; get; }

        
        /// <summary>
        /// Restaurant name
        /// </summary>
        public string RestaurantName { set; get; }
     
        /// <summary>
        /// Image path 
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Number of products in this Restaurant
        /// </summary>
        public int ProductsCount { get; set; }
        ///// <summary>
        ///// Neighborhood Id where this Restaurant exist
        ///// </summary>
        //public int? NeighborhoodId { get; set; }

        /// <summary>
        /// Description of this Restaurant
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Order this Restaurant 
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// State of Restaurant...
        /// stop=> 0, active=> 1, pause=> 2
        /// </summary>
        public RestaurantState State { get; set; }
        ///// <summary>
        ///// List of AdminGetRestaurantPropertyDto
        ///// </summary>
        //public List<AdminGetRestaurantPropertyDto> PropertyDto { get; set; }


    }
}
