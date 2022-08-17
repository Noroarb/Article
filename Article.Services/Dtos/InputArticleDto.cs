using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    
    public class InputArticleDto
    {
        /// <summary>
        /// ID статьи
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Категория статьи
        /// </summary>

        public int CategoryId { set; get; }

        /// <summary>
        /// Название статьи
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// Файл статьи имеет кодировку base 64.  
        /// </summary>
        public string File { set; get; }

        /// <summary>
        /// Авторы статей
        /// </summary>
        public List<UsersArticlesDto> Users { set; get; }
            
    }
}


