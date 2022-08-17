using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Card.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Reflection.Emit;

namespace Card.Data.Configuration
{

    internal class CityDescriptionConfiguration : EntityTypeConfiguration<CityDescription>
    {
        internal CityDescriptionConfiguration()
        {
            ToTable("CityDescription");

            // Start add One-to-Many relationship

            HasRequired<City>(s => s.City)
                 .WithMany(s => s.CityDescription)
                 .HasForeignKey(s => s.CityId);

            HasRequired<Language>(s => s.Language)
            .WithMany(s => s.CityDescriptions)
            .HasForeignKey(s => s.LanguageId);

            // End add One-to-Many relationship

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CityId)
                .HasColumnName("CityId")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.LanguageId)
                .HasColumnName("LanguageId")
                .HasColumnType("int")
                .IsRequired();

            Property(x => x.CityName)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();


        }
    }
}

