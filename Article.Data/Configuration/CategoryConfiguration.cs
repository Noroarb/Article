using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Article.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Article.Data.Configuration
{
    
    internal class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        internal CategoryConfiguration()
        {
            ToTable("Category");

            // Start add Many-to-Many relationship
          


            // End add Many-to-Many relationship

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

          

            Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .IsRequired();




        }
    }
}

