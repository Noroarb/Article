using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Domain.Entities
{
    public class Keywords : IEntityBase
    {
        public int Id { get; set; }

        public string Title { get; set; }
        


        public virtual ICollection<Articles_KeyWords> Articles_KeyWords { set; get; }

    }
}
