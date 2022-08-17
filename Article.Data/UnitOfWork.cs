using Article.Data.Repositories;
using Article.Domain;
using Article.Domain.Entities;
using Article.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private IExternalLoginRepository _externalLoginRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IRepository<Language> _languageRepository;

        private IRepository<Category> _categoryRepository;
        private IRepository<Articles> _ArticlesRepository;
        private IRepository<Articles_KeyWords> _Articles_KeyWordsRepository;
        private IRepository<Comments> _CommentsRepository;
        private IRepository<Keywords> _KeywordsRepository;
        private IRepository<Ratings> _RatingsRepository;
        private IRepository<Readers> _ReadersRepository;
        private IRepository<UsersArticles> _UsersArticlesRepository;

        #endregion

        #region Constructors
        public UnitOfWork(string nameOrConnectionString)
        {
            _context = new ApplicationDbContext(nameOrConnectionString);
        }
        #endregion

        #region IUnitOfWork Members
        public IExternalLoginRepository ExternalLoginRepository
        {
            get { return _externalLoginRepository ?? (_externalLoginRepository = new ExternalLoginRepository(_context)); }
        }
       

        public IRoleRepository RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }
     

        public IRepository<Language> LanguageRepository
        {
            get { return _languageRepository ?? (_languageRepository = new Repository<Language>(_context)); }
        }

        public IRepository<Articles> ArticlesRepository
        {
            get { return _ArticlesRepository ?? (_ArticlesRepository = new Repository<Articles>(_context)); }
        }

        public IRepository<Articles_KeyWords> Articles_KeyWordsRepository
        {
            get { return _Articles_KeyWordsRepository ?? (_Articles_KeyWordsRepository = new Repository<Articles_KeyWords>(_context)); }
        }

        public IRepository<Comments> CommentsRepository
        {
            get { return _CommentsRepository ?? (_CommentsRepository = new Repository<Comments>(_context)); }
        }
        public IRepository<Keywords> KeywordsRepository
        {
            get { return _KeywordsRepository ?? (_KeywordsRepository = new Repository<Keywords>(_context)); }
        }
        public IRepository<Ratings> RatingsRepository
        {
            get { return _RatingsRepository ?? (_RatingsRepository = new Repository<Ratings>(_context)); }
        }
        public IRepository<Readers> ReadersRepository
        {
            get { return _ReadersRepository ?? (_ReadersRepository = new Repository<Readers>(_context)); }
        }
       
        public IRepository<Category> CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new Repository<Category>(_context)); }
        }
        public IRepository<UsersArticles> UsersArticlesRepository
        {
            get { return _UsersArticlesRepository ?? (_UsersArticlesRepository = new Repository<UsersArticles>(_context)); }
        }
        
      


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _externalLoginRepository = null;
            _roleRepository = null;
            _userRepository = null;
            _context.Dispose();
        }
        #endregion
    }
}
