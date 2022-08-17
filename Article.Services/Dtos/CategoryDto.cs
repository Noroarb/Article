using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    /// <summary>
    /// Category
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Category Id 
        /// </summary>
        public int Id { set; get; }
       
        
        /// <summary>
        /// Category name
        /// </summary>
        public string Name { set; get; }
      
    }
}
