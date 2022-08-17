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
    internal class MessageUserInfoConfiguration : EntityTypeConfiguration<MessageUserInfo>
    {
        internal MessageUserInfoConfiguration()
        {
            ToTable("MessageUserInfo");


            HasRequired<User>(s => s.User)
                 .WithOptional(s => s.MessagingUserInfo)
                 .WillCascadeOnDelete(true);



            // Start add One-to-Many relationship

            //HasRequired<User>(s => s.User)
            //     .WithMany(s => s.Classifies)
            //     .HasForeignKey(s => s.CategoryId);

            //HasRequired<Category>(s => s.Category)
            //.WithMany(s => s.Classifies)
            //.HasForeignKey(s => s.Id);



            // End add One-to-Many relationship

            HasKey(x => x.UserId)
               .Property(x => x.UserId)
               .HasColumnName("UserId")
               .HasColumnType("uniqueidentifier")
               .IsRequired();

          

            Property(x => x.ConnectionDate)
                .HasColumnName("ConnectionDate")
                .HasColumnType("datetime2")
                .IsOptional()
                ;

            

            Property(x => x.DisconnectionDate)
                    .HasColumnName("DisconnectionDate")
                    .HasColumnType("datetime2")
                     .IsOptional()
                    ;



            Property(x => x.IsOnline)
               .HasColumnName("IsOnline")
               .HasColumnType("bit")
               .IsRequired()

               ;



        }
    }
}
