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
using System.Net.Http;

namespace Article.WebApi.Controllers
{
    /// <summary>
    /// Этот раздел для писателя
    /// </summary>
    
    public class ArticleController : ApiAuthorizeBaseController
    {
        private readonly IArticleService _ArticleService;
     
        public ArticleController(IArticleService IArticleService, ApplicationUserManager userManager):base(userManager)
        {
            _ArticleService = IArticleService;
        }

        /// <summary>
        /// Добавить новую статью 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = "WriterRole")]
        [HttpPost]
        [Route("~/api/Article/AddArticleAsDraft")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public int AddArticleAsDraft(InputArticleDto dto)
        {
            var articleName = Guid.NewGuid() + ".docx";
            var filePath = HttpContext.Current.Server.MapPath(Utils.PhysicalArticle) + articleName;

            

            var userId = getCurrentUserGuid();
            
            if(dto.Users.Where(m=>m.UserId==userId).Any())
            {
                foreach (var singleuser in dto.Users)
                {
                    if (singleuser.UserId == userId)
                    {
                        singleuser.Role = "Writer";
                    }
                }
            
            }
            else
            {
                dto.Users.Add(new UsersArticlesDto
                {
                    Role = "Writer",
                    UserId = userId
                });
            }
           
            var res= _ArticleService.AddArticleAsDraft(dto, articleName);

            if (res != 0)
                File.WriteAllBytes(filePath, Convert.FromBase64String(dto.File));
            
             return res;


        }



        /// <summary>
        /// Добавить новую статью как черновик
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [Authorize(Roles = "WriterRole")]
        [HttpPost]
        [Route("~/api/Article/ChangeStateOfArticleToDraft")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool ChangeStateOfArticleToDraft(int ArticleId)
        {
            return _ArticleService.ChangeStateOfArticleToDraft(ArticleId,getCurrentUserGuid());

        }

        /// <summary>
        /// опубликовать статью
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [Authorize(Roles = "WriterRole")]
        [HttpPost]
        [Route("~/api/Article/ChangeStateOfArticleToPending")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool ChangeStateOfArticleToPending(int ArticleId)
        {
            return _ArticleService.ChangeStateOfArticleToPending(ArticleId, getCurrentUserGuid());

        }

        /// <summary>
        /// Добавить ключевые слова в статью
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>

        //  [Authorize(Roles = "DeveloperRole,AdminRole")]
        [Authorize(Roles = "WriterRole")]
        [HttpPost]
        [Route("~/api/Article/AddKeyWordsToArticle")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool AddKeyWordsToArticle(int ArticleId, List<string> keywords)
        {
            return _ArticleService.AddKeyWordsToArticle(ArticleId, keywords, getCurrentUserGuid());
        }

        /// <summary>
        /// Вернуть все статьи
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "WriterRole")]
        [HttpGet]
        [Route("~/api/Article/GetAllArticles_ForUser")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public List<ArticleDto> GetAllArticles_ForUser()
        {
            return _ArticleService.GetAllArticles_ForUser(getCurrentUserGuid());
        }


        /// <summary>
        /// Поиск статьи
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("~/api/Article/SearchInArticle")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public List<ArticleDto> SearchInArticle(SearchInArticle dto)
        {
            return _ArticleService.SearchInArticle(dto);
        }
        /// <summary>
        /// Читать статью
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Вот ссылка на статью </returns>
        [Authorize]
        [HttpPost]
        [Route("~/api/Article/ReadArticle")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public string ReadArticle(int id)
        {
            return _ArticleService.ReadArticle(id, getCurrentUserGuid());
        }
        
        
    }
}

