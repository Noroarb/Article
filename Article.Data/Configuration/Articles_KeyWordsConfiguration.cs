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
    internal class Articles_KeyWordsConfiguration : EntityTypeConfiguration<Articles_KeyWords>
    {
        internal Articles_KeyWordsConfiguration()
        {
            ToTable("Articles_KeyWords");

            HasKey(x => x.Id)
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired<Articles>(s => s.Article)
               .WithMany(s => s.Articles_KeyWords)
               .HasForeignKey(s => s.ArticleId);

            HasRequired<Keywords>(s => s.Keyword)
             .WithMany(s => s.Articles_KeyWords)
             .HasForeignKey(s => s.KeyWordsId);

         

            Property(x => x.ArticleId)
                   .HasColumnName("ArticleId")
                   .HasColumnType("int")
               .IsRequired();

            

            Property(x => x.KeyWordsId)
          .HasColumnName("KeyWordsId")
          .HasColumnType("int")
          .IsRequired()
          ;

        }
    }
}
