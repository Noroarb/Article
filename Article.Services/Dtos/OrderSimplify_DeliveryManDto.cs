using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos
{

    /// <summary>
    /// this object for order but simplify for Card man
    /// </summary>
    public class OrderSimplify_CardManDto
    {
        public int OrderId { get; set; }
        /// <summary>
        /// First restaurant image
        /// </summary>
        public string RestaurantImage { get; set; }
        /// <summary>
        /// Accepted Time
        /// </summary>
        public DateTime? AcceptedTime { get; set; }
        /// <summary>
        /// عبارة عن رمز لتمييز الطلبية
        /// </summary>
        public string OrderIdenity { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        public Common.OrderStatus_Enum OrderStatus { get; set; }

    }
}
