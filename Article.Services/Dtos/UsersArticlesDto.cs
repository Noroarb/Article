using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersArticlesDto
    {
        /// <summary>
        /// For add article this filed is not important
        /// </summary>
        public int ArticleId { get; set; }

        public Guid UserId { set; get; }

        public string NameOfUser { set; get; }

        public string Role { set; get; }

    }
}


