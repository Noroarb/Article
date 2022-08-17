using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class PaidAds : IEntityBase
    {
       
        public int Id { get; set; }
        /// <summary>
        /// Facebook or web site link
        /// </summary>
        public string Link { get; set; }
        
        // public int Sort { set; get; }
        /// <summary>
        /// Ad image
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Date added ad
        /// </summary>
        public DateTime Date { get; set; }

        //public virtual Brands Brand { get; set; }


    }
}
