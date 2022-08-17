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
using PagedList;

/// <summary>
/// / int Add(LanguageHelper Language, InputCityDto dto);
////bool Edit(LanguageHelper Language, InputCityDto dto);
////bool Delete(int Id);
/// </summary>
namespace Article.Services.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArticleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        #region Input Article
        /// <summary>
        /// Add new article as a draft
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="ArticlePath"></param>
        /// <returns></returns>
        public int AddArticleAsDraft(InputArticleDto dto,string ArticlePath)
        {
            
            var model = Mapper.Map<InputArticleDto, Articles>(dto);
         
            model.AdditionDate = Utils.ServerNow;
            model.Body = ArticlePath;
            model.State = ArticleState.Draft;
            model.UsersArticles = new List<UsersArticles>();
            model.Readers = new List<Readers>();
            model.Comments = new List<Comments>();
            model.Ratings = new List<Ratings>();

            foreach (var user_ in dto.Users)
            {
                var user_db = _unitOfWork.UserRepository.FindById(user_.UserId);
                if (!user_db.IsActivated)
                    return 0;
                model.UsersArticles.Add(new UsersArticles()
                {
                    UserId = user_.UserId,
                    Role = user_.Role
                    
                });
            }


            _unitOfWork.ArticlesRepository.Add(model);
            _unitOfWork.SaveChanges();

            return model.Id;
        }



        /// <summary>
        /// This function for writer and for admin
        /// Add kwywords to article
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="keywords"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddKeyWordsToArticle(int ArticleId, List<string> keywords, Guid? userId)
        {
            List<Articles> article_model;
            if (userId.HasValue)
            {
                article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId && m.UsersArticles.Where(q => q.UserId == userId).Any());

            }
            else
            {
                article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId);
            }

            if (article_model.Any())
            {
                var article_ = article_model.FirstOrDefault();
                if(article_.Articles_KeyWords ==null)
                    article_.Articles_KeyWords = new List<Articles_KeyWords>();

                foreach(var keyword in keywords)
                {
                    var keymodel = _unitOfWork.KeywordsRepository.FindBy(m => m.Title == keyword);
                    if(keymodel.Any())
                    {
                        var keyword_db = keymodel.FirstOrDefault();
                        if (!article_.Articles_KeyWords.Where(m=>m.KeyWordsId== keyword_db.Id).Any())
                        {
                            article_.Articles_KeyWords.Add(new Articles_KeyWords
                            {
                                KeyWordsId = keyword_db.Id
                            });

                            _unitOfWork.ArticlesRepository.Update(article_);
                        }
                        
                    }
                    else
                    {
                        var newKeyword = new Keywords()
                        {
                            Title = keyword,
                            
                        };
                        newKeyword.Articles_KeyWords = new List<Articles_KeyWords>();
                        newKeyword.Articles_KeyWords.Add(new Articles_KeyWords
                        {
                            ArticleId = ArticleId
                        });

                        _unitOfWork.KeywordsRepository.Add(newKeyword);
                        
                    }
                    _unitOfWork.SaveChanges();
                }

                return true;
            }

           return false;
        }

        /// <summary>
        /// Change state of article from draft to pending
        /// only writer can use this function and admin
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ChangeStateOfArticleToPending(int ArticleId, Guid? userId)
        {
            List<Articles> article_model;
            if(userId.HasValue)
            {
                article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId && m.UsersArticles.Where(q => q.UserId == userId).Any());

            }
            else
            {
                article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId);
            }
            if(article_model.Any())
            {
                var article_ = article_model.FirstOrDefault();
                if (!article_.UsersArticles.Where(m=>m.User.IsActivated).Any())
                    return false;
                if (article_.State==ArticleState.Draft)
                {
                    article_.State = ArticleState.Pending;
                    article_.AdditionDate = Utils.ServerNow;

                    _unitOfWork.ArticlesRepository.Update(article_);
                    _unitOfWork.SaveChanges();

                    return true;
                }
               
            }
            
            return false;
        }

        /// <summary>
        /// only Supervisor and admin can use this function 
        /// Change state of article from pending to accepted
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public bool ChangeStateOfArticleToAccepted(int ArticleId)
        {
            List<Articles> article_model;
           
            article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId);
            
            if (article_model.Any())
            {
                var article_ = article_model.FirstOrDefault();
                if (article_.State == ArticleState.Pending)
                {
                    article_.State = ArticleState.Accepted;
                    article_.AdditionDate = Utils.ServerNow;

                    _unitOfWork.ArticlesRepository.Update(article_);
                    _unitOfWork.SaveChanges();

                    return true;
                }

            }

            return false;
        }

        /// <summary>
        /// only Supervisor and admin can use this function 
        /// Change state of article to rejected
        /// 
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public bool ChangeStateOfArticleToRejected(int ArticleId,string RejectedReason)
        {
            List<Articles> article_model;

            article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId);

            if (article_model.Any())
            {
                var article_ = article_model.FirstOrDefault();
               
                article_.State = ArticleState.Rejected;
                article_.RejectedReason = RejectedReason;

                _unitOfWork.ArticlesRepository.Update(article_);
                _unitOfWork.SaveChanges();

                return true;
                
            }

            return false;
        }

        /// <summary>
        /// Change state of article to draft
        /// only writer can use this function and admin
        /// </summary>
        /// <param name="ArticleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ChangeStateOfArticleToDraft(int ArticleId, Guid? userId)
        {
            List<Articles> article_model;
            if (userId.HasValue)
            {
                article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId && m.UsersArticles.Where(q => q.UserId == userId).Any());

            }
            else
            {
                article_model = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == ArticleId);
            }
            if (article_model.Any())
            {
                var article_ = article_model.FirstOrDefault();
               
                article_.State = ArticleState.Pending;
                
                _unitOfWork.ArticlesRepository.Update(article_);
                _unitOfWork.SaveChanges();

                return true;
                

            }

            return false;
        }

        /// <summary>
        /// For delete an article by id
        /// </summary>
        /// <param name="Id">Article Id</param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            var c1 = _unitOfWork.ArticlesRepository.FindBy(m=>m.Id==Id);
            if(!c1.Any())
            {
                return false;
            }
            else
            {
                _unitOfWork.ArticlesRepository.Remove(c1.FirstOrDefault());
                _unitOfWork.SaveChanges();
                return true;
            }
        }

        #endregion

        #region Get Article
        /// <summary>
        /// For admin and supervisor
        /// Get All  Articles in waiting state
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ArticleDto> GetAllPendingArticles()
        {
            var model = _unitOfWork.ArticlesRepository.FindBy(m=>m.State==ArticleState.Pending).OrderByDescending(m=>m.AdditionDate).ToList();

            var modelDto = Mapper.Map<List<Articles>, List<ArticleDto>>(model);

            int index = 0;
            foreach(var md in modelDto)
            {
                md.Comments = Mapper.Map<List<Comments>, List<CommentsDto>>(model[index].Comments.ToList());
                md.Users = Mapper.Map<List<UsersArticles>, List<UsersArticlesDto>>(model[index].UsersArticles.ToList());
                md.KeyWords = Mapper.Map<List<Articles_KeyWords>, List<KeyWordsDto>>(model[index].Articles_KeyWords.ToList());
                index++;
            }
            
            return modelDto;
        }

        /// <summary>
        /// For admin and supervisor
        /// Get All  Articles in rejected state
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ArticleDto> GetAllRejectedArticles()
        {
            var model = _unitOfWork.ArticlesRepository.FindBy(m => m.State == ArticleState.Rejected).OrderByDescending(m => m.AdditionDate).ToList();

            var modelDto = Mapper.Map<List<Articles>, List<ArticleDto>>(model);

            int index = 0;
            foreach (var md in modelDto)
            {
                md.Comments = Mapper.Map<List<Comments>, List<CommentsDto>>(model[index].Comments.ToList());
                md.Users = Mapper.Map<List<UsersArticles>, List<UsersArticlesDto>>(model[index].UsersArticles.ToList());
                md.KeyWords = Mapper.Map<List<Articles_KeyWords>, List<KeyWordsDto>>(model[index].Articles_KeyWords.ToList());
                index++;
            }

            return modelDto;
        }

        /// <summary>
        /// For admin and supervisor
        /// Get All  Articles for specific user
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ArticleDto> GetAllArticles_ForUser(Guid userId)
        {
            var model = _unitOfWork.ArticlesRepository.FindBy(m => m.UsersArticles.Where(q=>q.UserId== userId).Any()).OrderByDescending(m => m.AdditionDate).ToList();

            var modelDto = Mapper.Map<List<Articles>, List<ArticleDto>>(model);

            int index = 0;
            foreach (var md in modelDto)
            {
                md.Comments = Mapper.Map<List<Comments>, List<CommentsDto>>(model[index].Comments.ToList());
                md.Users = Mapper.Map<List<UsersArticles>, List<UsersArticlesDto>>(model[index].UsersArticles.ToList());
                md.KeyWords = Mapper.Map<List<Articles_KeyWords>, List<KeyWordsDto>>(model[index].Articles_KeyWords.ToList());
                index++;
            }

            return modelDto;
        }

        public List<ArticleDto> SearchInArticle(SearchInArticle dto)
        {
            int index = 0;
            List<Articles> model;

            model = _unitOfWork.ArticlesRepository.FindBy(m => (dto.CategoryId.HasValue? m.CategoryId == dto.CategoryId.Value:true)&& (dto.Title!="" ? m.Title.Contains(dto.Title):true) && m.State == ArticleState.Accepted);

            List<ArticleDto> modelDto;
            if (dto.SortByRatings)
            {
                model = model.OrderByDescending(m => m.Ratings.Sum(q => q.Stars)).ToList();
                modelDto = Mapper.Map<List<Articles>, List<ArticleDto>>(model);

                index = 0;
                foreach (var md in modelDto)
                {
                    md.Comments = Mapper.Map<List<Comments>, List<CommentsDto>>(model[index].Comments.ToList());
                    md.Users = Mapper.Map<List<UsersArticles>, List<UsersArticlesDto>>(model[index].UsersArticles.ToList());
                    md.KeyWords = Mapper.Map<List<Articles_KeyWords>, List<KeyWordsDto>>(model[index].Articles_KeyWords.ToList());
                    index++;
                }

                return modelDto;
            }
           // , 

            if (dto.SortByReading)
            {
                model = model.OrderByDescending(m => m.Readers.Count).ToList();
                modelDto = Mapper.Map<List<Articles>, List<ArticleDto>>(model);
                index = 0;
                foreach (var md in modelDto)
                {
                    md.Comments = Mapper.Map<List<Comments>, List<CommentsDto>>(model[index].Comments.ToList());
                    md.Users = Mapper.Map<List<UsersArticles>, List<UsersArticlesDto>>(model[index].UsersArticles.ToList());
                    md.KeyWords = Mapper.Map<List<Articles_KeyWords>, List<KeyWordsDto>>(model[index].Articles_KeyWords.ToList());
                    index++;
                }

                return modelDto;
            }

            model = model.OrderByDescending(m => m.AdditionDate).ToList();
            modelDto= Mapper.Map<List<Articles>, List<ArticleDto>>(model);

            index = 0;
            foreach (var md in modelDto)
            {
                md.Comments = Mapper.Map<List<Comments>, List<CommentsDto>>(model[index].Comments.ToList());
                md.Users = Mapper.Map<List<UsersArticles>, List<UsersArticlesDto>>(model[index].UsersArticles.ToList());
                index++;
            }

            return modelDto;
        }

        /// <summary>
        /// Read article
        /// For admin
        /// </summary>
        /// <returns>Article as URL</returns>
        public string ReadArticle(int id,Guid? userId)
        {
            List<Articles> model1;
            model1 = _unitOfWork.ArticlesRepository.FindBy(m => m.Id == id);
            if(model1.Any())
            {
                
                var article_model = model1.FirstOrDefault();
                if (userId.HasValue)
                {
                    if (article_model.State==ArticleState.Accepted)
                    {
                    if (article_model.Readers == null)
                        article_model.Readers = new List<Readers>();
                    if(article_model.Readers.Where(m=>m.UserId==userId.Value).Any())
                        return Utils.ArticleURL + article_model.Body;
                    article_model.Readers.Add(new Readers()
                    {
                        ArticleId = article_model.Id,
                        UserId = userId.Value
                    });

                    _unitOfWork.ArticlesRepository.Update(article_model);
                    _unitOfWork.SaveChanges();

                    return Utils.ArticleURL + article_model.Body;
                     }
                
                    if(article_model.UsersArticles.Where(m=>m.UserId==userId.Value).Any())
                    {
                        return Utils.ArticleURL + article_model.Body;
                    }
                }
                else
                {
                    return Utils.ArticleURL + article_model.Body;
                }
            }

            return null;

        }
    
        #endregion

    }
}

