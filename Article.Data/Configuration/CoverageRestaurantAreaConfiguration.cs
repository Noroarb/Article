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
    internal class CoverageRestaurantAreaConfiguration : EntityTypeConfiguration<CoverageRestaurantArea>
    {
        internal CoverageRestaurantAreaConfiguration()
        {
            ToTable("CoverageRestaurantArea");


            HasRequired<Restaurants>(s => s.Restaurant)
                 .WithMany(s => s.CoverageRestaurantAreas)
                 .HasForeignKey(s => s.RestaurantId)
                 .WillCascadeOnDelete(true);

            // End add One-to-Many relationship

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.RestaurantId)
                .HasColumnName("RestaurantId")
                .HasColumnType("int")
                .IsRequired()
                ;
           
            Property(x => x.CoverageArea)
                    .HasColumnName("CoverageArea")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(400)
                    ;

        }
    }
}
