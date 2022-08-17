using Article.Common;
using Article.Services.Dtos;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Article.Services.Interfaces
{
    public interface ICategoryService
    {
        
        CategoryDto GetById(CategoryGetDto dto);
        
       
       
        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        int Add(InputCategoryDto dto);
        bool Edit(InputCategoryDto dto);
        /// <summary>
        /// note : if category has products this function return null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool? Delete(int id);
       
     
        /// <summary>
        /// This for get all tree for user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        List<CategoryDto> GetAll( CategoryGetDto dto);
       
    }
}
