using Article.Common;
using Article.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Interfaces
{
    public interface ICommentService
    {

        #region  InputComments
        /// <summary>
        /// Add new comment
        /// This function for user 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        int AddNewComment(InputCommentDto dto, Guid UserId);



        /// <summary>
        /// Delete comment
        /// this function for admin and User
        /// </summary>
        /// <param name="CommentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteComment(int CommentId, Guid? userId);


        #endregion

    }
}
