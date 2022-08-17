using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.Entities
{
    public class Brands : IEntityBase
    {

        public int Id { get; set; }
       
        /// <summary>
        /// Brand name
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// Description of this Brand
        /// </summary>
        public string BrandDescription { get; set; }
        
        ///// <summary>
        ///// Order this brand
        ///// </summary>
        //public int Sort { get; set; }
        public DateTime Date { get; set; }

        public string ImagePath { get; set; }
        // public string categoryName { set; get; }
              
        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public string Mobile1 { get; set; }

        public string Mobile2 { get; set; }

        public string Mobile3 { get; set; }

        public string Fax1 { get; set; }
       
        public string Email1 { get; set; }

        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public string Snapchat { get; set; }
     
      
        public virtual ICollection<Products> Products { set; get; }
        public virtual ICollection<PaidAds> PaidAds { set; get; }
        //public virtual Town Town { set; get; }

        //public virtual ICollection<FavoriteStore> FavoriteStores { set; get; }
        //public virtual ICollection<Classify> Classifies { set; get; }

    }
}
