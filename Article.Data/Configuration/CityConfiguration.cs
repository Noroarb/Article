using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Card.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace Card.Data.Configuration
{

    internal class CityConfiguration : EntityTypeConfiguration<City>
    {
        internal CityConfiguration()
        {
            ToTable("City");

            // Start add One-to-Many relationship
            HasMany<CityDescription>(s => s.CityDescription)
                    .WithRequired(s => s.City)
                    .HasForeignKey(s => s.CityId);

            //HasMany<Classify>(s => s.Classifieds)
            //       .WithRequired(s => s.City)
            //       .HasForeignKey(s => s.CityId);


            // End add One-to-Many relationship

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(x => x.Sort)
                 .HasColumnName("Sort")
                  .HasColumnType("int")
                 .IsRequired();


        }
    }
}

