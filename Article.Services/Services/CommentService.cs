using AutoMapper;
using Article.Common;
using Article.Domain;
using Article.Domain.Entities;
using Article.Services.Dtos;
using Article.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Article.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region  InputComments
        /// <summary>
        /// Add new comment
        /// This function for user 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int AddNewComment(InputCommentDto dto, Guid UserId)
        {
           
            var user = _unitOfWork.UserRepository.FindById(UserId);
            if(user.IsActivated)
            {
                var model = Mapper.Map<InputCommentDto, Comments>(dto);
                model.AdditionDate = Utils.ServerNow;
                model.UserId = UserId;

                _unitOfWork.CommentsRepository.Add(model);
                _unitOfWork.SaveChanges();

                return model.Id;
            }
          
            return 0;

        }



        /// <summary>
        /// Delete comment
        /// this function for admin and User
        /// </summary>
        /// <param name="CommentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteComment(int CommentId,Guid? userId)
        {
            var models = _unitOfWork.CommentsRepository.FindBy(m => m.Id == CommentId);
            if (models.Any())
            {
                var model = models.FirstOrDefault();

                if (userId.HasValue)
                {
                    if (model.UserId == userId.Value)
                    {
                        _unitOfWork.CommentsRepository.Remove(model);
                        _unitOfWork.SaveChanges();
                    }
                }
                else
                {
                    _unitOfWork.CommentsRepository.Remove(model);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            return false;
        }

      
        #endregion


      

    }
}
