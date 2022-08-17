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
    public interface IArticleService
    {

        #region Input Article
        /// <summary>
        /// Add new article as a draft
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ArticlePath"></param>
        /// <returns></returns>
        int AddArticleAsDraft(InputArticleDto dto, string ArticlePath);



        /// <summary>
        /// This function for writer and for admin
        /// Add kwywords to article
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="keywords"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool AddKeyWordsToArticle(int ArticleId, List<string> keywords, Guid? userId);

        /// <summary>
        /// Change state of article from draft to pending
        /// only writer can use this function and admin
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ChangeStateOfArticleToPending(int ArticleId, Guid? userId);

        /// <summary>
        /// only Supervisor and admin can use this function 
        /// Change state of article from pending to accepted
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        bool ChangeStateOfArticleToAccepted(int ArticleId);

        /// <summary>
        /// only Supervisor and admin can use this function 
        /// Change state of article to rejected
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        bool ChangeStateOfArticleToRejected(int ArticleId, string RejectedReason);

        /// <summary>
        /// Change state of article to draft
        /// only writer can use this function and admin
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ChangeStateOfArticleToDraft(int ArticleId, Guid? userId);

        /// <summary>
        /// For delete an article by id
        /// </summary>
        /// <param name="Id">Article Id</param>
        /// <returns></returns>
        bool Delete(int Id);

        #endregion

        #region Get Article
        /// <summary>
        /// For admin and supervisor
        /// Get All  Articles in waiting state
        /// 
        /// </summary>
        /// <returns></returns>
        List<ArticleDto> GetAllPendingArticles();

        /// <summary>
        /// For admin and supervisor
        /// Get All  Articles in rejected state
        /// 
        /// </summary>
        /// <returns></returns>
        List<ArticleDto> GetAllRejectedArticles();

        /// <summary>
        /// For admin and supervisor
        /// Get All  Articles for specific user
        /// 
        /// </summary>
        /// <returns></returns>
        List<ArticleDto> GetAllArticles_ForUser(Guid userId);

        List<ArticleDto> SearchInArticle(SearchInArticle dto);

        /// <summary>
        /// Read article
        /// For admin
        /// </summary>
        /// <returns>Article as URL</returns>
        string ReadArticle(int id, Guid? userId);

        #endregion

    }
}
