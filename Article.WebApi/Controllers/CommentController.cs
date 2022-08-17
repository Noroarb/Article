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

   
    public class CommentController : ApiAuthorizeBaseController
    {
        private readonly ICommentService _CommentService;
     
        public CommentController(ICommentService ICommentService, ApplicationUserManager userManager):base(userManager)
        {
            _CommentService = ICommentService;
        }

        /// <summary>
        ///Добавить новый комментарий 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("~/api/Comment/AddNewComment")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public int AddNewComment(InputCommentDto dto)
        {
            return _CommentService.AddNewComment( dto,getCurrentUserGuid());
        }




        
        /// <summary>
        /// Удалить комментарий 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("~/api/Comment/DeleteComment")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool? DeleteComment(int CommentId)
        {
            
            var delete = _CommentService.DeleteComment(CommentId,getCurrentUserGuid());
            if (delete != null)
                return delete;
            else
                throw new HttpResponseException(NotFoundMessage("Not accepted"));
        }

        /// <summary>
        /// Удалить комментарий администратором
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        [Authorize(Roles = "AdminRole")]
        [HttpPost]
        [Route("~/api/Comment/DeleteCommentThisForAdmin")]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotAcceptable, HttpStatusCode.Unauthorized)]
        public bool? DeleteCommentThisForAdmin(int CommentId)
        {

            var delete = _CommentService.DeleteComment(CommentId,null);
            if (delete != null)
                return delete;
            else
                throw new HttpResponseException(NotFoundMessage("Not accepted"));
        }


    }
}

