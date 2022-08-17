using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Domain.Entities
{
    public class Articles_KeyWords 
    {
        public int Id { set; get; }
        public int ArticleId { get; set; }

        public int KeyWordsId { get; set; }
        

        public virtual Articles Article { set; get; }

        public virtual Keywords Keyword { set; get; }

    }
}
