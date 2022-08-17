using Article.Domain.Entities;
using Article.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Article.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        IExternalLoginRepository ExternalLoginRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IRepository<Language> LanguageRepository { get; }

        IRepository<Category> CategoryRepository { get; }
        IRepository<Articles> ArticlesRepository { get; }

        IRepository<Articles_KeyWords> Articles_KeyWordsRepository { get; }
        IRepository<UsersArticles> UsersArticlesRepository { get; }
        IRepository<Comments> CommentsRepository { get; }
        IRepository<Keywords> KeywordsRepository { get; }
        IRepository<Ratings> RatingsRepository { get; }
        IRepository<Readers> ReadersRepository { get; }

        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}
