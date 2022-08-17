using Article.Data.Configuration;
using Article.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Data
{
    internal class ApplicationDbContext : DbContext
    {
        internal ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString = "DefaultConnection")
        {
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }



        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<ExternalLogin> Logins { get; set; }
        public IDbSet<Language> Languages { get; set; }

   
        public IDbSet<Articles> Articles { set; get; }
        public IDbSet<Articles_KeyWords> Articles_KeyWords { set; get; }
        public IDbSet<Comments> Comments { set; get; }

        public IDbSet<Keywords> Keywords { set; get; }

        public IDbSet<Ratings> Ratings { set; get; }
        
        public IDbSet<Category> Categories { set; get; }
        public IDbSet<Readers> Readers { set; get; }
        public IDbSet<UsersArticles> UsersArticles { set; get; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ExternalLoginConfiguration());
            modelBuilder.Configurations.Add(new ClaimConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            
            modelBuilder.Configurations.Add(new Articles_KeyWordsConfiguration());
            modelBuilder.Configurations.Add(new ArticlesConfiguration());
            modelBuilder.Configurations.Add(new CommentsConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new KeywordsConfiguration());


            modelBuilder.Configurations.Add(new RatingsConfiguration());
            modelBuilder.Configurations.Add(new ReadersConfiguration());
            modelBuilder.Configurations.Add(new UsersArticlesConfiguration());
            

        }

      
    }
}
