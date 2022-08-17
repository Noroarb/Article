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
    [Authorize(Roles = "SupervisorRole,AdminRole")]
    public class AdminArticleController : ApiAuthorizeBaseController
    {
        private readonly IArticleService _ArticleService;
     
        public AdminArticleController(IArticleService IArticleService, ApplicationUserManager userManager):base(userManager)
        {
            _ArticleService = IArticleService;
        }

        /// <summary>
        /// Преобразование статуса статьи из в Одобрена   
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/AdminArticle/ChangeStateOfArticleToAccepted")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool ChangeStateOfArticleToAccepted(int ArticleId)
        {
            return _ArticleService.ChangeStateOfArticleToAccepted(ArticleId);
         
        }
        /// <summary>
        /// Преобразование статуса статьи из в черновик 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/AdminArticle/ChangeStateOfArticleToDraft")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool ChangeStateOfArticleToDraft(int ArticleId)
        {
            return _ArticleService.ChangeStateOfArticleToDraft(ArticleId,null);

        }

        /// <summary>
        /// Преобразование статуса статьи из в опубликована
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/AdminArticle/ChangeStateOfArticleToPending")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool ChangeStateOfArticleToPending(int ArticleId)
        {
            return _ArticleService.ChangeStateOfArticleToPending(ArticleId, null);

        }

        /// <summary>
        /// Преобразование статуса статьи из в отклонена
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("~/api/Article/ChangeStateOfArticleToRejected")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool ChangeStateOfArticleToRejected(int ArticleId, string RejectedReason)
        {
            return _ArticleService.ChangeStateOfArticleToRejected(ArticleId, RejectedReason);

        }
        /// <summary>
        /// Добавить ключевые слова в статью
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        //  [Authorize(Roles = "DeveloperRole,AdminRole")]

        [HttpPost]
        [Route("~/api/AdminArticle/AddKeyWordsToArticle")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool AddKeyWordsToArticle(int ArticleId, List<string> keywords)
        {
            return _ArticleService.AddKeyWordsToArticle(ArticleId, keywords,null);
        }
        /// <summary>
        /// удалить статью
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("~/api/AdminArticle/Delete")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool Delete(int ArticleId)
        {
            return _ArticleService.Delete(ArticleId);
        }
        /// <summary>
        /// Вернуть все статьи, опубликованные автором Статьи
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("~/api/AdminArticle/GetAllArticles_ForUser")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public List<ArticleDto> GetAllArticles_ForUser(Guid userId)
        {
            return _ArticleService.GetAllArticles_ForUser(userId);
        }
        /// <summary>
        /// Вернуть все опубликованные статьи
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("~/api/AdminArticle/GetAllPendingArticles")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public List<ArticleDto> GetAllPendingArticles()
        {
            return _ArticleService.GetAllPendingArticles();
        }
        /// <summary>
        /// Вернуть все отклоненные статьи
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("~/api/AdminArticle/GetAllRejectedArticles")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public List<ArticleDto> GetAllRejectedArticles()
        {
            return _ArticleService.GetAllRejectedArticles();
        }
        /// <summary>
        /// Поиск статьи 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("~/api/AdminArticle/SearchInArticle")]
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

        [HttpPost]
        [Route("~/api/AdminArticle/ReadArticle")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public string ReadArticle(int id)
        {
            return _ArticleService.ReadArticle(id,null);
        }
        
        
    }
}

