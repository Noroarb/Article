using Article.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Data.Configuration
{
    internal class UsersArticlesConfiguration : EntityTypeConfiguration<UsersArticles>
    {
        internal UsersArticlesConfiguration()
        {
            ToTable("UsersArticles");


            HasRequired<Articles>(s => s.Article)
               .WithMany(s => s.UsersArticles)
               .HasForeignKey(s => s.ArticleId);

            HasRequired<User>(s => s.User)
              .WithMany(s => s.Articles)
              .HasForeignKey(s => s.UserId);

            HasKey(x => x.Id)
              .Property(x => x.Id)
              .HasColumnName("Id")
              .HasColumnType("int")
              .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.UserId)
                 .HasColumnName("UserId")
                 .HasColumnType("uniqueidentifier")
                 .IsRequired();

            Property(x => x.ArticleId)
                .HasColumnName("ArticleId")
                .HasColumnType("int")
            .IsRequired();

            Property(x => x.Role)
                      .HasColumnName("Role")
                      .HasColumnType("nvarchar")
                      .IsRequired()
                      .HasMaxLength(4000)
                      ;

        }
    }
}
