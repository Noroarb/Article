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
    internal class PropertyValuesConfiguration : EntityTypeConfiguration<PropertyValues>
    {
        internal PropertyValuesConfiguration()
        {
            ToTable("PropertyValues");


            HasRequired<Products>(s => s.Product)
                 .WithMany(s => s.PropertyValues)
                 .HasForeignKey(s => s.ProductId)
                 .WillCascadeOnDelete(false);

            HasRequired<RestaurantProperties>(s => s.RestaurantProperty)
                .WithMany(s => s.PropertyValues)
                .HasForeignKey(s => s.RestaurantPropertiesId);

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

            Property(x => x.RestaurantPropertiesId)
                .HasColumnName("RestaurantPropertiesId")
                .HasColumnType("int")
                .IsRequired()
                ;
            Property(x => x.ProductId)
               .HasColumnName("ProductId")
               .HasColumnType("int")
               .IsRequired()
               
               ;

            //Property(x => x.Value)
            //    .HasColumnName("Date")
            //    .HasColumnType("datetime2")
            //    ;



            Property(x => x.Value)
                    .HasColumnName("Value")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(70)
                    .IsRequired();


        }
    }
}
