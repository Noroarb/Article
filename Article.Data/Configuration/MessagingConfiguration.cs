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
    internal class MessagingConfiguration : EntityTypeConfiguration<Messaging>
    {
        internal MessagingConfiguration()
        {
            ToTable("Messaging");


            //HasRequired<User>(s => s.StoreManagerSender)
            //     .WithMany(s => s.MessagingSenders)
            //     .HasForeignKey(s => s.StoreManagerSenderId)
            //     .WillCascadeOnDelete(false);

            HasRequired<User>(s => s.Receiver)
                .WithMany(s => s.MessagingReceivers)
                .HasForeignKey(s => s.ReceiverId)
                .WillCascadeOnDelete(false);


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

            //Property(x => x.SenderName)
            //     .HasColumnName("SenderName")
            //     .HasColumnType("nvarchar")
            //     .HasMaxLength(80);

            Property(x => x.ReceiverName)
                 .HasColumnName("ReceiverName")
                 .HasColumnType("nvarchar");

        //Property(x => x.RestaurantName)
        //         .HasColumnName("RestaurantName")
        //         .HasColumnType("nvarchar");

            Property(x => x.Body)
                  .HasColumnName("Body")
                  .HasColumnType("nvarchar")
                  .HasMaxLength(4000);

            Property(x => x.ReceiverId)
                  .HasColumnName("ReceiverId")
                  .HasColumnType("uniqueidentifier")
                  .IsRequired();

            //Property(x => x.StoreManagerSenderId)
            //      .HasColumnName("StoreManagerSenderId")
            //      .HasColumnType("uniqueidentifier")
            //      .IsRequired();

         Property(x => x.ReceiverPhoneNumber)
                  .HasColumnName("ReceiverPhoneNumber")
                  .HasColumnType("nvarchar")
                   .HasMaxLength(20);


            //Property(x => x.Location)
            //   .HasColumnName("Location")
            //   .HasColumnType("nvarchar")
            //   .HasMaxLength(256)
            //   ;

            //.HasColumnType("uniqueidentifier")
            //Property(x => x.Body)
            //        .HasColumnName("Body")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(500)
            //        ;


            Property(x => x.SendingDate)
                    .HasColumnName("SendingDate")
                    .HasColumnType("dateTime2")
                     .IsOptional()
                    ;

        //    Property(x => x.FirebaseToken)
        //.HasColumnName("FirebaseToken")
        //.HasColumnType("nvarchar")
        //.IsRequired();

            //Property(x => x.IsSeen)
            //  .HasColumnName("IsSeen")
            //  .HasColumnType("bit")
            //  .IsRequired()
            //  ;

            //Property(x => x.SenderMobileSystem)
            //   .HasColumnName("SenderMobileSystem")
            //   .HasColumnType("nvarchar")
            //   .IsOptional()
            //   .HasMaxLength(500);


            //.HasColumnType("uniqueidentifier")
            //Property(x => x.Body)
            //        .HasColumnName("Body")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(500)
            //        ;

            //Property(x => x.ReceivedDate)
            //    .HasColumnName("ReceivedDate")
            //    .HasColumnType("dateTime2")
            //    .IsOptional()
            //    ;
            //Property(x => x.MessageMobileId)
            //   .HasColumnName("MessageMobileId")
            //   .HasColumnType("int")
            //   .IsOptional()
            //   ;


            //Property(x => x.ShowingDate)
            //       .HasColumnName("ShowingDate")
            //       .HasColumnType("dateTime2")
            //        .IsOptional()
            //       ;


            //Property(x => x.IsReceived)
            //    .HasColumnName("IsReceived")
            //    .HasColumnType("bit")
            //    .IsRequired()
            //    ;

            //Property(x => x.DeletedByAdmin)
            //   .HasColumnName("DeletedByAdmin")
            //   .HasColumnType("bit")
            //   .IsRequired()
            //   ;
            //Property(x => x.DeletedByReciever)
            //    .HasColumnName("DeletedByReciever")
            //    .HasColumnType("bit")
            //    .IsRequired()
            //    ;
            //Property(x => x.DeletedBySender)
            //  .HasColumnName("DeletedBySender")
            //  .HasColumnType("bit")
            //  .IsRequired()
            //  ;

        }
    }
}
