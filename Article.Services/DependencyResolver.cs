using Article.Data;
using Article.Domain;
using Article.Resolver;
using Article.Services.Dtos;
using Article.Services.Dtos.Validators;
using Article.Services.Identity;
using Article.Services.Interfaces;
using Article.Services.Services;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Article.Services
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterTypeWithInjectedConstructor<IUnitOfWork, UnitOfWork>("Article");
            registerComponent.RegisterTypeWithTransientLifetimeManager<IUserStore<IdentityUser, Guid>, UserStore>();
            registerComponent.RegisterTypeWithTransientLifetimeManager<IRoleStore<IdentityRole,Guid>,RoleStore>();

            // Services
            registerComponent.RegisterType<IUserService,UserService>();
            
            registerComponent.RegisterType<ICategoryService, CategoryService>();
            registerComponent.RegisterType<IArticleService, ArticleService>();
            registerComponent.RegisterType<ICommentService, CommentService>();
           


            // Validators
          
            registerComponent.RegisterType<IValidator<RegisterUserDto>, RegisterUserValidator>();
        }
    }
}
