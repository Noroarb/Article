using Article.Common;
using Article.Services.Dtos;
using Article.Services.Interfaces;
using Article.WebApi.ActionFilters;
using Article.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Article.Services.Dtos.Validators;
//using Article.Common.Resources;

namespace Article.WebApi.Controllers
{
    /// <summary>
    /// Этот раздел для пользователя
    /// </summary>
    [Authorize]
    public class CategoryController : ApiBaseController
    {
        
        private readonly ICategoryService _CategoryService;

        public CategoryController(ICategoryService ICategoryService)
        {
            _CategoryService = ICategoryService;
        }


        // GET api/<controller>/5
        ///// <summary>
        ///// 
        ///// 
        ///// </summary>
        ///// <param name="dto"> Category Id </param>
        ///// <returns>CategoryDto</returns>
        //[HttpPost]
        //[Route("~/api/Category/Get")]
        //public CategoryDto Get(CategoryGetDto dto)
        //{
        //    var model = _CategoryService.GetById(dto);
        //    if (model != null)
        //        return model;
        //    throw new HttpResponseException(NotFoundMessage("Not catrgories"));

        //}



        /// <summary>
        /// Показать категории статей
        /// </summary>
        /// <returns>CategoryDto</returns>
        [HttpPost]
        [Route("~/api/Category/GetAll")]
        public List<CategoryDto> GetAll(CategoryGetDto dto)
        {
            var model = _CategoryService.GetAll(dto);
            if (model.Any())
                return model;
            else
                throw new HttpResponseException(NotFoundMessage("Not catrgories"));

        }


    }
}