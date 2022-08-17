using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class CommentsDto
    {
        /// <summary>
        /// Идентификатор комментария
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ID статьи
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid UserId { set; get; }
        /// <summary>
        /// Имя пользователя, добавившего комментарий
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// комментарий
        /// </summary>
        public string Body { set; get; }
        /// <summary>
        /// Дата комментария
        /// </summary>
        public DateTime AdditionDate { set; get; }

    }
}
