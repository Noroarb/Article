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
    internal class ArticlesConfiguration : EntityTypeConfiguration<Articles>
    {
        internal ArticlesConfiguration()
        {
            ToTable("Articles");


            HasRequired<Category>(s => s.Categories)
               .WithMany(s => s.Articles)
               .HasForeignKey(s => s.CategoryId);

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


            Property(x => x.RejectedReason)
                      .HasColumnName("RejectedReason")
                      .HasColumnType("nvarchar")
                      .IsOptional()
                      .HasMaxLength(4000)
                      ;

            Property(x => x.Title)
                    .HasColumnName("Title")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(4000)
                    ;

            Property(x => x.State)
                   .HasColumnName("State")
                   .HasColumnType("int")
               .IsRequired();


            Property(x => x.AdditionDate)
            .HasColumnName("AdditionDate")
            .HasColumnType("datetime2")
            .IsRequired()
            ;

            Property(x => x.CategoryId)
          .HasColumnName("CategoryId")
          .HasColumnType("int")
          .IsRequired()
          ;

        }
    }
}
