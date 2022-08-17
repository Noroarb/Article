using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    /// <summary>
    /// Category for admin
    /// </summary>
    public class EditCategoryDto
    {
        /// <summary>
        /// Id required for edit not for insert
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// The Id for the parent of this category 
        /// </summary>
        public int? ParentID { set; get; }
        /// <summary>
        /// Category sort 
        /// </summary>
        public int Sort { set; get; }
        /// <summary>
        /// رقم الواتس للشخص المسؤول عن هذه الخدمة
        /// </summary>
        public string WhatsAppNumber { set; get; }

        /// <summary>
        /// Category name in arabic
        /// </summary>
        public string ArabicName { set; get; }
        /// <summary>
        /// Category name in english
        /// </summary>
        public string EnglishName { set; get; }

        /// <summary>
        /// Image category path , 
        /// This is for show when edit
        /// </summary>
        [UIHint("Image")]
        public string ImagePath { set; get; }
        public bool IsActive { get; set; }

        /// <summary>
        /// This for Get not for input,  
        /// list of this category's children
        /// </summary>
        public List<EditCategoryDto> CategoryChildren { set; get; }
        /// <summary>
        /// This for Get not for input,  
        /// Show that this category has children or not
        /// </summary>
        public bool HasChildren { set; get; }
    }
}
