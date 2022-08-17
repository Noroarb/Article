using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos
{

    /// <summary>
    /// معرفة مواقع عمال التوصيل
    /// </summary>
    public class CardManLocation_ResultDto
    {
        public Guid CardManId { get; set; }
        /// <summary>
        /// اسم عامل التوصيل 
        /// </summary>
        public string CardManName { get; set; }
        public string Gps_Latitude { get; set; }
        public string Gps_Longitude { get; set; }
        public bool IsOnline { get; set; }

        /// <summary>
        /// IsHasOrder
        /// يدل فيما اذا كان هذا العامل لديه طلب او لا..
        /// لايمكن ارسال طلب للعامل الذي لديه طلب سابق او الى العامل الذي يكون في حالة offline...
        /// </summary>
        public bool IsHasOrder { get; set; }

    }
}
