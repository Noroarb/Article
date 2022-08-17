using Card.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Card.Services.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class InputOrderCardDto2
    {

        #region order

        /// Person name who is ordering
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Phon number
        /// </summary>
        public string Phone { get; set; }
        

        public string City { get; set; }

        /// <summary>
        /// طريقة تسليم النقود
        /// Status:
        /// عن طريق الهرم--> 0
        /// تسليم باليد--> 1
        /// </summary>
        public WithdrawWay_Enum WithdrawWay { get; set; }

        /// <summary>
        /// ملاحظات للطلب
        /// يستطيع فقط الأدمن إضافة ملاحظات إلى الطلب
        /// </summary>
        public string Notes { get; set; }

        #endregion

        public int Id { set; get; }
        public int CardId { get; set; }
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
        /// الحالة الخاصة بحالة بطاقة
        /// Status:
        /// Valid--> 0
        /// Invalid--> 1
        /// </summary>
        public CardStatus_Enum CardStatus { get; set; }

        public string NameOf_ImageOrFile { set; get; }

        public HttpPostedFileBase TextFilesOrImage { get; set; }

    }
}
