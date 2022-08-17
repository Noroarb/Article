using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Domain.Entities
{
    public class Comments : IEntityBase
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }
        
        public Guid UserId { set; get; }

        public string Body { set; get; }

        public DateTime AdditionDate { set; get; }


        public virtual Articles Article { set; get; }

        public virtual User User { set; get; }

    }
}
