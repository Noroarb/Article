using Article.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Domain.Entities
{
    public class Articles : IEntityBase
    {


        public int Id { get; set; }

        public int CategoryId { set; get; }
        
        public string Title { set; get; }
      
        public string Body { set; get; }
        
        public DateTime AdditionDate { set; get; }
      
        public ArticleState State { set; get; }//
      
        public string RejectedReason { set; get; }
        

        public virtual Category Categories { set; get; }

        public virtual ICollection<Articles_KeyWords> Articles_KeyWords { set; get; }

        public virtual ICollection<UsersArticles> UsersArticles { set; get; }

        public virtual ICollection<Readers> Readers { set; get; }

        public virtual ICollection<Ratings> Ratings { set; get; }

        public virtual ICollection<Comments> Comments { set; get; }
        // 

    }
}
