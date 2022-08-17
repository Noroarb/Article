using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
   public class CardPriceHistory:IEntityBase
    {
        public int Id { set; get; }
        public int CardId { set; get; }
        /// <summary>
        /// سعر المبيع للبطاقة في هذا اليوم
        /// VIP
        /// </summary>
        public decimal? SoldPrice { get; set; }
        public DateTime AdditionDate { get; set; }


        public virtual Cards Card { set; get; }


    }
}
