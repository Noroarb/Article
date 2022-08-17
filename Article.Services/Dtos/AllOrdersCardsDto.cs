using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos
{
    /// <summary>
    /// Card
    /// </summary>
    public class AllOrdersCardsDto
    {
        /// <summary>
        /// نوع البطاقة
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// تفاصيل البطاقة
        /// </summary>
        public List<OrdersCardsDto> OrdersCardsDto { get; set; }
      

    }
}


