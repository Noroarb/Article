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
    internal class KeywordsConfiguration : EntityTypeConfiguration<Keywords>
    {
        internal KeywordsConfiguration()
        {
            ToTable("Keywords");



            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        

            Property(x => x.Title)
           .HasColumnName("Body")
           .HasColumnType("nvarchar")
           .HasMaxLength(4000)
           ;


        

        }
    }
}
