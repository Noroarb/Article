using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    
    public class SearchInArticle 
    {
        /// <summary>
        /// Вернуть статью по id
        /// </summary>
        public int? CategoryId { set; get; }

        /// <summary>
        /// Показать статью по названию статьи
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// Показать статью по рейтингу
        /// </summary>
        public bool SortByRatings { set; get; }
        /// <summary>
        /// Показать статьи по количеству прочтений
        /// </summary>
        public bool SortByReading { set; get; }
        /// <summary>
        /// Показать статью по дате публикации
        /// </summary>
        public bool SortByDate { set; get; }

      
            
    }
}


