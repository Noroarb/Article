using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos
{
    /// <summary>
    /// لمعرفة احداثيات عامل التوصيل بشكل مستمر
    /// </summary>
    public class CardManLocationDto
    {
        /// <summary>
        /// Gps Latitude location
        /// </summary>
        public string Gps_Latitude { get; set; }
        /// <summary>
        /// Gps Longitude location
        /// </summary>
        public string Gps_Longitude { get; set; }

    }
}
