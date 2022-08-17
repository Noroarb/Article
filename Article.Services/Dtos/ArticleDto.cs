using Article.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    
    public class ArticleDto
    {
        /// <summary>
        /// Идентификатор статьи
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// категория статьи
        /// </summary>
        public int CategoryId { set; get; }

        /// <summary>
        ///Название статьи
        /// </summary>
        public string Title { set; get; }

        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateTime AdditionDate { set; get; }
        /// <summary>
        /// состояние
        /// </summary>
        public ArticleState State { set; get; }//
        /// <summary>
        /// Причина отклонения статьи
        /// </summary>
        public string RejectedReason { set; get; }
        /// <summary>
        /// Авторы статей
        /// </summary>
        public List<UsersArticlesDto> Users { set; get; }
        /// <summary>
        /// Количество прочтений статьи
        /// </summary>
        public int CountOfReading { set; get; }

        /// <summary>
        /// Рейтинг статей
        /// </summary>
        public double Ratings { set; get; }

        /// <summary>
        /// Комментарии к статье
        /// </summary>
        public List<CommentsDto> Comments { set; get; }

        /// <summary>
        /// Ключевые слова
        /// </summary>
        public List<KeyWordsDto> KeyWords { set; get; }

    }
}


