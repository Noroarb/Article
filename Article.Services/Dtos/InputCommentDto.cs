using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    
    public class InputCommentDto
    {
        /// <summary>
        /// Article Id
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// Комментарии
        /// </summary>
        public string Body { set; get; }
        

    }
}


