using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    /// <summary>
    /// Input category
    /// </summary>
    public class InputCategoryDto
    {
        /// <summary>
        /// Идентификатор требуется для редактирования, а не для вставки
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// Вот название категории
        /// </summary>
        public string Name { set; get; }

    }
}
