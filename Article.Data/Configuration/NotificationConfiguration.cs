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
    internal class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        internal NotificationConfiguration()
        {
            ToTable("Notification");

            //HasOptional<Products>(s => s.Product)
            //   .WithMany(s => s.Notifications)
            //   .HasForeignKey(s => s.ProductId);

            HasOptional<User>(s => s.Client)
              .WithMany(s => s.Notifications)
              .HasForeignKey(s => s.ClientId);

            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            

            Property(x => x.NotifyDate)
                .HasColumnName("NotifyDate")
                .HasColumnType("datetime2")
                ;

            Property(x => x.Title)
                    .HasColumnName("Title")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(70)
                    ;


            //Property(x => x.)
            //    .HasColumnName("ClassifyName")
            //    .HasColumnType("nvarchar")
            //    .HasMaxLength(70)
            //    ;
            //Property(x => x.Descripe)
            //    .HasColumnName("Descripe")
            //    .HasColumnType("nvarchar")
            //    .HasMaxLength(4000)
            //    ;
            //Property(x => x.City)
            //    .HasColumnName("City")
            //    .HasColumnType("nvarchar")
            //    ;


            Property(x => x.Discription)
                .HasColumnName("Discription")
                .HasColumnType("nvarchar")
                ;

            Property(x => x.Image)
                .HasColumnName("Image")
                .HasColumnType("nvarchar")
                .IsOptional()
                .HasMaxLength(100)
                ;

            Property(x => x.Type)
                .HasColumnName("Type")
                .HasColumnType("int")
                .IsOptional()
                ;

            

            Property(x => x.Update)
             .HasColumnName("Update")
             .HasColumnType("nvarchar")
             .IsOptional()
             ;

            Property(x => x.ProductId)
             .HasColumnName("ProductId")
             .HasColumnType("int")
             .IsOptional()
             ;



        }
    }
}
