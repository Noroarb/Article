using AutoMapper;
using Article.Common;
using Article.Domain.Entities;
using Article.Services.Dtos;
using Article.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services
{
    public static class DtoMappings
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                // ENTITY TO DTO
                #region ENTITY TO DTO
                cfg.CreateMap<User, IdentityUser>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.UserId));
                cfg.CreateMap< IdentityUser, User>()
                   .ForMember(dest => dest.UserId,
                       opts => opts.MapFrom(src => src.Id));

           
                cfg.CreateMap<User, UserDto>()
                  .ForMember(dest => dest.Role,
                        opts => opts.MapFrom(src => src.Roles.FirstOrDefault().Name));
                
             


            cfg.CreateMap<Articles, ArticleDto>()
                    .ForMember(dest => dest.CountOfReading,
                    opts => opts.MapFrom(src => src.Readers.Count))
                    .ForMember(dest => dest.Ratings,
                    opts => opts.MapFrom(src => src.Ratings.Sum(m => m.Stars) / (src.Ratings.Count==0?1: src.Ratings.Count)));

                cfg.CreateMap<Comments, CommentsDto>()
                 .ForMember(dest => dest.UserName,
                    opts => opts.MapFrom(src => src.User.FullName));

                cfg.CreateMap<Articles_KeyWords, KeyWordsDto>()
                .ForMember(dest => dest.Title,
                   opts => opts.MapFrom(src => src.Keyword.Title));

                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Category, InputCategoryDto>();
                cfg.CreateMap<Category, EditCategoryDto>();
              

                cfg.CreateMap<UsersArticles, UsersArticlesDto>()
                .ForMember(dest => dest.NameOfUser,
                    opts => opts.MapFrom(src => src.User.FullName));
               

                #endregion

                // DTO TO ENTITY
                #region DTO TO ENTTY
                cfg.CreateMap<IdentityUser, User>()
                    .ForMember(dest => dest.UserId,
                        opts => opts.MapFrom(src => src.Id));
                
   
              
                cfg.CreateMap<CategoryDto, Category>();
                cfg.CreateMap<InputCategoryDto, Category>();
                cfg.CreateMap<InputArticleDto, Articles>();
                cfg.CreateMap<InputCommentDto, Comments>();
                cfg.CreateMap<ArticleDto, Articles>();
               
              
                #endregion
            });

        }
    }
}
