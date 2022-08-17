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
    public class InputOrderCardDto
    {
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
        /// الملاحظات الخاصة بهذه البطاقة
        /// حقل احتياطي غير مستخدم حاليا
        /// </summary>
        public string Notes { get; set; }

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
