using Card.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Data.Configuration
{
    internal class CardsConfiguration : EntityTypeConfiguration<Cards>
    {
        internal CardsConfiguration()
        {
            ToTable("Cards");


            //HasRequired<User>(s => s.User)
            //    .WithMany(s => s.FavoriteRestaurants)
            //    .HasForeignKey(s => s.UserId)
            //    .WillCascadeOnDelete(false);

            //HasRequired<Products>(s => s.Product)
            //   .WithMany(s => s.FavoriteRestaurants)
            //   .HasForeignKey(s => s.ProductId)
            //   .WillCascadeOnDelete(false);

            ////sss HasMany<OrdersCards>(s => s.Orders_Cards)
            ////  .WithRequired(s => s.Card)
            ////  .HasForeignKey(s => s.CardId)
            ////  .WillCascadeOnDelete(true);

            // End add One-to-Many relationship

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        

            Property(x => x.Name)
           .HasColumnName("Name")
           .HasColumnType("nvarchar")
           .IsRequired()
           .HasMaxLength(1000)
           ;


            Property(x => x.simpleUser_Price)
             .HasColumnName("simpleUser_Price")
             .HasColumnType("money")
             .IsOptional()
             ;

            Property(x => x.VIPUser_Price)
             .HasColumnName("VIPUser_Price")
             .HasColumnType("money")
             .IsOptional()
             ;

            Property(x => x.TimeAuto)
             .HasColumnName("TimeAuto")
             .HasColumnType("int")
             .IsOptional()
             ;

            Property(x => x.Sort)
            .HasColumnName("Sort")
            .HasColumnType("int")
            .IsRequired()
            ;

        }
    }
}
