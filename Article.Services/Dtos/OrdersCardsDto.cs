using Card.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos
{

    /// <summary>
    /// البطاقات ضمن الطلب الواحد
    /// </summary>
    public class OrdersCardsDto
    {
        public int Id { set; get; }
        public int CardId { get; set; }
        /// <summary>
        /// نوع البطاقة
        /// </summary>
        public string CardType { get; set; }
        //public int OrderId { get; set; }
        public int OrderId { set; get; }
        /// <summary>
        /// كود البطاقة 
        /// </summary>
        public string URL_Or_Code { get; set; }
        /// <summary>
        /// كمية البطاقة
        /// </summary>
        public double Amount { set; get; }
        /// <summary>
        /// السعر الافرادي للبطاقة
        /// </summary>
        public decimal? CardPrice_Unit { set; get; }
        /// <summary>
        /// السعر الاجمالي للبطاقة 
        /// </summary>
        public decimal? Total_CardPricePrice { set; get; }
        /// <summary>
        /// الملاحظات الخاصة بهذه البطاقة
        /// حقل احتياطي غير مستخدم حاليا
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// اسم الزبون
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// رقم هاتف الزبون 
        /// </summary>
        public string PhoneNumber { set; get; }
        /// <summary>
        /// الحالة الخاصة بحالة بطاقة
        /// Status:
        /// Valid--> 0
        /// Invalid--> 1
        /// </summary>
        public CardStatus_Enum CardStatus { get; set; }

        public string NameOf_ImageOrFile { set; get; }
    }
}
