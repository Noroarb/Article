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
    internal class MessageConnectionGroupsConfiguration : EntityTypeConfiguration<MessageConnectionGroups>
    {
        internal MessageConnectionGroupsConfiguration()
        {
            ToTable("MessageConnectionGroups");


            HasRequired<User>(s => s.User)
                 .WithMany(s => s.MessageConnectionGroups)
                 .HasForeignKey(s => s.UserId)
                 ;

          


       
     

        //public string IMEI { get; set; }


        //public virtual User User { set; get; }

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

            Property(x => x.UserId)
                 .HasColumnName("UserId")
                 .HasColumnType("uniqueidentifier")
                 .IsRequired();

            Property(x => x.ConnectionId)
                .HasColumnName("ConnectionId")
                .HasColumnType("nvarchar")
                .IsRequired();


            Property(x => x.GroupName)
                  .HasColumnName("GroupName")
                  .HasColumnType("nvarchar")
                  .IsRequired()
                  .HasMaxLength(4000);

           
            Property(x => x.IMEI)
                  .HasColumnName("IMEI")
                  .HasColumnType("nvarchar")
                  .IsRequired()
                  ;

            
            //.HasColumnType("uniqueidentifier")
            //Property(x => x.Body)
            //        .HasColumnName("Body")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(500)
            //        ;



        }
    }
}
