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
    internal class CardManStatusConfiguration : EntityTypeConfiguration<CardManStatus>
    {
        internal CardManStatusConfiguration()
        {
            ToTable("CardManStatus");

            //HasMany<OrderProduct>(s => s.OrderProducts)
            //  .WithRequired(s => s.Order)
            //  .HasForeignKey(s => s.OrderId);

            //HasMany<Messaging>(s => s.Messages)
            //   .WithRequired(s => s.Order)
            //   .HasForeignKey(s => s.OrderId)
            //   .WillCascadeOnDelete(true);

            HasRequired<User>(s => s.CardMan)
                 .WithOptional(s => s.CardManStatus)
                 .WillCascadeOnDelete(true);

            //HasRequired<Restaurants>(s => s.Restaurant)
            //    .WithMany(s => s.CardManStatus)
            //    .HasForeignKey(s => s.RestaurantId);




            // End add One-to-Many relationship

            HasKey(x => x.CardManId)
               .Property(x => x.CardManId)
               .HasColumnName("CardManId")
               .HasColumnType("uniqueidentifier")
               .IsRequired();

            


            Property(x => x.Gps_Latitude)
               .HasColumnName("Gps_Latitude")
               .HasColumnType("nvarchar")
               .IsRequired()
               .HasMaxLength(100)
               ;

            Property(x => x.Gps_Longitude)
                .HasColumnName("Gps_Longitude")
                .HasColumnType("nvarchar")
                .IsRequired()
                .HasMaxLength(100)
                ;

            Property(x => x.IsOnline)
                .HasColumnName("IsOnline")
                .HasColumnType("bit")
                .IsRequired()
                ;
            Property(x => x.IsHasOrder)
                .HasColumnName("IsHasOrder")
                .HasColumnType("bit")
                .IsRequired()
                ;

            Property(x => x.DeleveryManKind)
             .HasColumnName("DeleveryManKind")
             .HasColumnType("int")
             .IsRequired()
             ;

            Property(x => x.LastRefresh)
                .HasColumnName("LastRefresh")
                .HasColumnType("dateTime2")
                .IsRequired()
                ;





        }

    }
}