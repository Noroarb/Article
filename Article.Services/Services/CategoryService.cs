using AutoMapper;
using Article.Domain;
using Article.Common;
using Article.Domain.Entities;
using Article.Services.Dtos;
using Article.Services.Interfaces;
using Article.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.Http.Routing;
using System.Web.Http;
using PagedList;

namespace Article.Services.Services
{
    /// <summary>
    ///  /// <summary>
    ///  تصنيف المقالات
    /// </summary>
    /// </summary>
    public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }



        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int Add(InputCategoryDto dto)
        {
              
            var model = Mapper.Map<InputCategoryDto, Category>(dto);

            _unitOfWork.CategoryRepository.Add(model);
            _unitOfWork.SaveChanges();
            return model.Id;
           
        }

        public bool Edit(InputCategoryDto dto)
        {  
            Category model1 = _unitOfWork.CategoryRepository.FindSingleBy(s => s.Id == dto.Id);
            model1.Name = dto.Name;

            _unitOfWork.CategoryRepository.Update(model1);
            _unitOfWork.SaveChanges();
            return true;
            
        }

        
      

        /// <summary>
        /// This for admin
        /// note : if category has articles this function return null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool? Delete(int id)
        {
           
            Category cat = _unitOfWork.CategoryRepository.FindBy(m=>m.Id==id).FirstOrDefault();
            if (cat == null)
            { return false; }
            else
            if (cat.Articles.Any())
            { return null; }
           
         
            _unitOfWork.CategoryRepository.Remove(cat);
            _unitOfWork.SaveChanges();
            return true;
                
        
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public CategoryDto GetById(CategoryGetDto dto)
        {
            var model2 = _unitOfWork.CategoryRepository.FindBy(m => m.Id == dto.Id);
            
            if (model2.Any())
            {
                var model = model2.FirstOrDefault();
                CategoryDto ss = Mapper.Map<Category, CategoryDto>(model);
              
                return ss;
            }
            else
                return null;
        }

   

     
        /// <summary>
        /// This for get all tree for user
        /// </summary>
        /// <param name="language"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<CategoryDto> GetAll(CategoryGetDto dto)
        {

            var model = _unitOfWork.CategoryRepository.GetAll();

            return Mapper.Map<List<Category>, List<CategoryDto>>(model);
            
        }

        

        //#region Image


        //public bool AddImageToCategory(int Id, string ImageName)
        //{
        //    var model11 = _unitOfWork.CategoryRepository.FindBy(m => m.Id == Id);
        //    if (!model11.Any())
        //        return false;
        //    else
        //    {
        //        var model = model11.Single();
        //        if (model.ImagePath!= Utils.ImageDefaultName)
        //        {
        //            var p = System.Web.HttpContext.Current.Server.MapPath(Utils.PhysicalImageCategory + model.ImagePath);
        //            p = p.Replace(Utils.OldPath, Utils.NewPath);

        //            try
        //            {
        //                File.Delete(p);
        //            }
        //            catch (Exception e1) { }
                   
        //        }
        //        model.ImagePath = ImageName;
        //        _unitOfWork.CategoryRepository.Update(model);
        //        _unitOfWork.SaveChanges();
        //        return true;
        //    }
        //}

     

        

        //#endregion

        

        
    }
}
