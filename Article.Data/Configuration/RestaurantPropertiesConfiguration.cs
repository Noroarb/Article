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
    internal class RestaurantPropertiesConfiguration : EntityTypeConfiguration<RestaurantProperties>
    {
        internal RestaurantPropertiesConfiguration()
        {
            ToTable("RestaurantProperties");

            HasMany<PropertyValues>(s => s.PropertyValues)
                    .WithRequired(s => s.RestaurantProperty)
                    .HasForeignKey(s => s.RestaurantPropertiesId)
                    .WillCascadeOnDelete(true);

            HasRequired<Restaurants>(s => s.Restaurant)
                 .WithMany(s => s.RestaurantProperties)
                 .HasForeignKey(s => s.RestaurantId)
                 .WillCascadeOnDelete(false)
                ;

            HasRequired<Properties>(s => s.Property)
                .WithMany(s => s.RestaurantProperties)
                .HasForeignKey(s => s.PropertyId)
                .WillCascadeOnDelete(false);

            //HasRequired<Town>(s => s.Town)
            //   .WithMany(s => s.Classifieds)
            //   .HasForeignKey(s => s.TownId);


            // Start add One-to-Many relationship

            //HasRequired<User>(s => s.User)
            //     .WithMany(s => s.Classifies)
            //     .HasForeignKey(s => s.CategoryId);

            //HasRequired<Category>(s => s.Category)
            //.WithMany(s => s.Classifies)
            //.HasForeignKey(s => s.Id);



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
            Property(x => x.PropertyId)
               .HasColumnName("PropertyId")
               .HasColumnType("int")
               .IsRequired()
               ;

            //Property(x => x.Value)
            //    .HasColumnName("Date")
            //    .HasColumnType("datetime2")
            //    ;




        }
    }
}