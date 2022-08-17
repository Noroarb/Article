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
    internal class CommentsConfiguration : EntityTypeConfiguration<Comments>
    {
        internal CommentsConfiguration()
        {
            ToTable("Comments");


            HasRequired<Articles>(s => s.Article)
               .WithMany(s => s.Comments)
               .HasForeignKey(s => s.ArticleId);

            HasRequired<User>(s => s.User)
              .WithMany(s => s.Comments)
              .HasForeignKey(s => s.UserId);

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        

            Property(x => x.Body)
           .HasColumnName("Body")
           .HasColumnType("nvarchar")
           .HasMaxLength(4000)
           ;


         

            Property(x => x.ArticleId)
                   .HasColumnName("ArticleId")
                   .HasColumnType("int")
               .IsRequired();


            Property(x => x.AdditionDate)
            .HasColumnName("AdditionDate")
            .HasColumnType("datetime2")
            .IsRequired()
            ;

         Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

        }
    }
}
