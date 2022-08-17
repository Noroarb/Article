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
    internal class OrdersCardsConfiguration : EntityTypeConfiguration<OrdersCards>
    {
        internal OrdersCardsConfiguration()
        {
            ToTable("OrdersCards");

            //HasMany<OrderProduct>(s => s.OrderProducts)
            //  .WithRequired(s => s.Order)
            //  .HasForeignKey(s => s.OrderId);

            //HasMany<Messaging>(s => s.Messages)
            //   .WithRequired(s => s.Order)
            //   .HasForeignKey(s => s.OrderId)
            //   .WillCascadeOnDelete(true);

            //HasRequired<Orders>(s => s.Order)
            //     .WithMany(s => s.Orders_Cards)

            //     .WillCascadeOnDelete(true);

            //HasRequired<Cards>(s => s.Card)
            //    .WithMany(s => s.Orders_Cards)
            //    .WillCascadeOnDelete(true);

            //HasRequired<Restaurants>(s => s.Restaurant)
            //    .WithMany(s => s.OrderStatus)
            //    .HasForeignKey(s => s.RestaurantId);


            // End add One-to-Many relationship
            HasKey(x => x.Id)
              .Property(x => x.Id)
              .HasColumnName("Id")
              .HasColumnType("int")
              .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // HasKey(x => x.CardId)
            Property(x => x.CardId)
               .HasColumnName("CardId")
               .HasColumnType("int")
               .IsRequired();

          //  HasKey(x => x.OrderId)
              Property(x => x.OrderId)
              .HasColumnName("OrderId")
              .HasColumnType("int")
              .IsRequired();
            //HasKey(x => x.CardManId)
            //   .Property(x => x.CardManId)
            //   .HasColumnName("CardManId")
            //   .HasColumnType("uniqueidentifier")
            //   .IsRequired();

            //
            Property(x => x.URL_Or_Code)
             .HasColumnName("URL_Or_Code")
             .HasColumnType("nvarchar")
             .IsRequired()
             .HasMaxLength(4000)
             ;


            Property(x => x.Amount)
             .HasColumnName("Amount")
             .HasColumnType("float")
             .IsRequired()
             ;

            Property(x => x.CardStatus)
             .HasColumnName("CardStatus")
             .HasColumnType("int")
             .IsRequired()
             ;

            Property(x => x.CardPrice_Unit)
             .HasColumnName("CardPrice_Unit")
             .HasColumnType("money")
             .IsOptional()
             ;

            Property(x => x.Total_CardPricePrice)
            .HasColumnName("Total_CardPricePrice")
            .HasColumnType("money")
            .IsOptional()
            ;

            Property(x => x.Notes)
            .HasColumnName("Notes")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(4000)
            ;

            
            //Property(x => x.IsOnline)
            //    .HasColumnName("IsOnline")
            //    .HasColumnType("bit")
            //    .IsRequired()
            //    ;
            //Property(x => x.IsHasOrder)
            //    .HasColumnName("IsHasOrder")
            //    .HasColumnType("bit")
            //    .IsRequired()
            //    ;







        }

    }
}