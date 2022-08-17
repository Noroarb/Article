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
    internal class CardPriceHistoryConfiguration : EntityTypeConfiguration<CardPriceHistory>
    {
        internal CardPriceHistoryConfiguration()
        {
            ToTable("CardPriceHistory");

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


            Property(x => x.SoldPrice)
            .HasColumnName("SoldPrice")
            .HasColumnType("money")
            .IsOptional()
            ;

            Property(x => x.AdditionDate)
               .HasColumnName("AdditionDate")
               .HasColumnType("datetime2")
               .IsRequired()
               ;


            //Property(x => x.IsOnline)
            //    .HasColumnName("IsOnline")
            //    .HasColumnType("bit")
            //    .IsRequired()
            //    ;






        }

    }
}