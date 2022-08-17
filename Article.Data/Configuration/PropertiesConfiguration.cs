using Market.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Data.Configuration
{
    internal class PropertiesConfiguration : EntityTypeConfiguration<Properties>
    {
        internal PropertiesConfiguration()
        {
            ToTable("Properties");

            // Start add One-to-Many relationship

            HasRequired<Category>(s => s.Category)
                 .WithMany(s => s.Properties)
                 .HasForeignKey(s => s.CategoryId)
                 .WillCascadeOnDelete(true);

            HasMany<PropertiesDescriptions>(s => s.PropertiesDescriptions)
                   .WithRequired(s => s.Property)
                   .HasForeignKey(s => s.PropertyId)
                   .WillCascadeOnDelete(true);

            HasMany<RestaurantProperties>(s => s.RestaurantProperties)
                  .WithRequired(s => s.Property)
                  .HasForeignKey(s => s.PropertyId)
                  .WillCascadeOnDelete(false);

            // End add One-to-Many relationship

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CategoryId)
                .HasColumnName("CategoryId")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.type)
                .HasColumnName("type")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.TypeShow)
                .HasColumnName("TypeShow")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.TypeManagerShow)
                .HasColumnName("TypeManagerShow")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.Sort)
                .HasColumnName("Sort")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
