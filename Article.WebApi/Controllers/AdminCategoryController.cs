using Microsoft.Owin.Security;
using Article.Common;
using Article.Services.Dtos;
using Article.Services.Identity;
using Article.Services.Interfaces;
using Article.WebApi.ActionFilters;
using Article.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Article.WebApi.Controllers
{
    /// <summary>
    /// Этот раздел для администратора
    /// </summary>
    [Authorize(Roles = "AdminRole")]
    public class AdminCategoryController : ApiAuthorizeBaseController
    {
        private readonly ICategoryService _CategoryService;
     
        public AdminCategoryController(ICategoryService ICategoryService, ApplicationUserManager userManager):base(userManager)
        {
            _CategoryService = ICategoryService;
        }

        /// <summary>
        /// Добавить категорию к статье
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/AdminCategory/AddCategory")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public int AddCategory(InputCategoryDto dto)
        {//LanguageHelper Language,
           //var RestaurantInfo= _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return _CategoryService.Add( dto);
            // return _languageService.Add(dto);
        }

        /// <summary>
        /// Изменить категорию статьи
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        //  [Authorize(Roles = "DeveloperRole,AdminRole")]

        [HttpPost]
        [Route("~/api/AdminCategory/EditCategory")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool EditCategory(InputCategoryDto dto)
        {
           // var RestaurantInfo = _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return _CategoryService.Edit(dto);
        }

        ///// <summary>
        ///// Notes: this function has been modified
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("~/api/AdminCategory/GetAll")]
        //public List<CategoryDto> GetAll(CategoryGetDto dto)
        //{
        //    var model = _CategoryService.GetAll_forManager(CurrentLanguage,dto);
        //    if (model.Any())
        //        return model;
        //    else
        //        throw new HttpResponseException(NotFoundMessage("لايوجد فئات"));

        //}

        ///// <summary>
        ///// This function has been modified
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <param name="ManagerRestaurantId"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("~/api/AdminCategory/GetById_formanager_Edit")]
        //public EditCategoryDto GetById_formanager_Edit(CategoryGetDto dto, Guid ManagerRestaurantId)
        //{
        //    var model = _CategoryService.GetById_formanager_Edit(dto);
        //    if (model!=null)
        //        return model;
        //    else
        //        throw new HttpResponseException(NotFoundMessage("لايوجد فئات"));

        //}
        //[HttpPost]
        //[Route("~/api/AdminCategory/GetById_formanager")]
        //public CategoryDto GetById_formanager(CategoryCityTownDto dto, Guid ManagerRestaurantId)
        //{
        //    var model = _CategoryService.GetById_formanager()
        //    if (model != null)
        //        return model;
        //    else
        //        throw new HttpResponseException(NotFoundMessage("لاتوجد فئات"));

        //}

        // DELETE api/<controller>/5
        //  [Authorize(Roles = "DeveloperRole,AdminRole")]
        // [ValidateModelAttribute]
        /// <summary>
        /// Notes: this function has been modified
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/AdminCategory/Delete")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool? Delete(int id )
        {
            var delete = _CategoryService.Delete(id);
            if (delete != null)
                return delete;
            else
                throw new HttpResponseException(NotFoundMessage("Not accepted"));
        }


        ///// <summary>
        ///// Notes: this function has been modified
        ///// </summary>
        ///// <param name="extension"></param>
        ///// <param name="CatigoryId"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("~/api/AdminCategory/TestPostImageBase64")]
        //public async Task<string> TestPostImageBase64(string extension, int CatigoryId)
        //{
        //    string PATH = HttpContext.Current.Server.MapPath(Utils.PhysicalImageCategory);
        //    string imageData = await Request.Content.ReadAsStringAsync();

        //    //byte[] bytes = Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAKBueIx4ZKCMgoy0qqC+8P//8Nzc8P//////////////////////////////////////////////////////////2wBDAaq0tPDS8P//////////////////////////////////////////////////////////////////////////////wAARCAAKAAoDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwBVUFQSOf51HRRSND//2Q==");
        //    byte[] bytes = Convert.FromBase64String(imageData);
        //    Image image;
        //    Guid g = Guid.NewGuid();
        //    extension = extension.ToLower();
        //    if (extension == "jpg" || extension == "png" || extension == "Gif")
        //    {
                
        //        if (_CategoryService.AddImageToCategory(CatigoryId, g.ToString() + "." + extension))
        //        {
        //            // int g = 1010101;
        //            using (MemoryStream ms = new MemoryStream(bytes))
        //            {
        //                image = Image.FromStream(ms);
        //                switch (extension)
        //                {
        //                    case "jpg":
        //                        image.Save(PATH + "/" + g.ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        //                        break;
        //                    case "png":
        //                        image.Save(PATH + "/" + g.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
        //                        break;
        //                    case "gif":
        //                        image.Save(PATH + "/" + g.ToString() + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
        //                        break;
        //                    default:
        //                        break;
        //                }

        //            }


        //            return Utils.ImageCategoryURL + g.ToString() + "." + extension.ToLower();
        //        }
        //        else
        //            throw new HttpResponseException(NotFoundMessage("هذا التصنيف غير موجود"));
        //    }
        //    else
        //    throw new HttpResponseException(NotFoundMessage("هذا الإمتداد غير مدعوم"));
        //    //  return string.Join(Url.Content("~/UploadImages/"),"", g.ToString() + "." + extension.ToLower());
        //}

        
    }
}

