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

    internal class TownConfiguration : EntityTypeConfiguration<Town>
    {
        internal TownConfiguration()
        {
            ToTable("Town");

            // Start add One-to-Many relationship
            HasMany<TownDescription>(s => s.TownDescriptions)
                    .WithRequired(s => s.Town)
                    .HasForeignKey(s => s.TownId);

            //HasMany<GuideNeighborhood>(s => s.GuideNeighborhoods)
            //       .WithRequired(s => s.Town)
            //       .HasForeignKey(s => s.TownId);

            HasRequired<City>(s => s.City)
                .WithMany(s => s.Towns)
                .HasForeignKey(s => s.CityId);

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


            //Property(x => x.Gps_Latitude)
            //    .HasColumnName("Gps_Latitude")
            //    .HasColumnType("nvarchar")
            //    .IsOptional()
            //    .HasMaxLength(20)
            //    ;
            //Property(x => x.Gps_Longitude)
            //    .HasColumnName("Gps_Longitude")
            //    .HasColumnType("nvarchar")
            //    .IsOptional()
            //    .HasMaxLength(20)
            //    ;

            //Property(x => x.Place_Id)
            //    .HasColumnName("Place_Id")
            //    .HasColumnType("nvarchar")
            //    .IsRequired()
            //    .HasMaxLength(100)
            //    ;



        }
    }
}

