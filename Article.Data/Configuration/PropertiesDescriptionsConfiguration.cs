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
    internal class PropertiesDescriptionsConfiguration : EntityTypeConfiguration<PropertiesDescriptions>
    {
        internal PropertiesDescriptionsConfiguration()
        {
            ToTable("PropertiesDescriptions");

            // Start add One-to-Many relationship

            HasRequired<Properties>(s => s.Property)
                 .WithMany(s => s.PropertiesDescriptions)
                 .HasForeignKey(s => s.PropertyId);

            HasRequired<Language>(s => s.Language)
            .WithMany(s => s.PropertiesDescriptions)
            .HasForeignKey(s => s.LanguageId);

            // End add One-to-Many relationship

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.PropertyId)
                .HasColumnName("PropertyId")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.LanguageId)
                .HasColumnName("LanguageId")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Unit)
                .HasColumnName("Unit")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                ;


        }
    }
}
