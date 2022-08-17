using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Domain.Entities
{
    public class Category : IEntityBase
    {


        public int Id { get; set; }
        
        public string Name { set; get; }

        public virtual ICollection<Articles> Articles { set; get; }
      
            

    }
}
