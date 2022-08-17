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
    internal class BrandsConfiguration : EntityTypeConfiguration<Brands>
    {
        internal BrandsConfiguration()
        {
            ToTable("Brands");


            //HasRequired<User>(s => s.User)
            //     .WithMany(s => s.Stores)
            //     .HasForeignKey(s => s.UserId);


            //HasRequired<User>(s => s.User)
            //    .WithMany(s => s.Classifies)
            //    .HasForeignKey(s => s.UserId);

            //HasRequired<Town>(s => s.Town)
            //   .WithMany(s => s.Stores)
            //   .HasForeignKey(s => s.TownId);

            HasMany<Products>(s => s.Products)
                .WithOptional(s => s.Brands)
                .HasForeignKey(s => s.BrandId);

            HasMany<PaidAds>(s => s.PaidAds)
                .WithOptional(s => s.Brand)
                .HasForeignKey(s => s.BrandId);

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


            //Property(x => x.UserId)
            //.HasColumnName("UserId")
            //.HasColumnType("uniqueidentifier")
            //.IsRequired();



            Property(x => x.Date)
                .HasColumnName("Date")
                .HasColumnType("datetime2")
                .IsRequired()
                ;

          
            Property(x => x.BrandName)
                    .HasColumnName("BrandName")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(400)
                    .IsRequired();

            Property(x => x.BrandDescription)
                    .HasColumnName("BrandDescription")
                    .HasColumnType("nvarchar")
                    .HasMaxLength(400)
                    .IsOptional();

            //Property(x => x.)
            //        .HasColumnName("Description")
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(4000)
            //        .IsOptional();

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


            //Property(x => x.UserId)
            //    .HasColumnName("UserId")
            //    .HasColumnType("uniqueidentifier")
            //    .IsRequired()
            //    ;

            //Property(x => x.visitorsCount)
            //    .HasColumnName("visitorsCount")
            //    .HasColumnType("int")
            //    .IsRequired()
            //    ;

            Property(x => x.ImagePath)
                .HasColumnName("ImagePath")
                .HasColumnType("nvarchar")
                .HasMaxLength(400)
                .IsOptional()
                ;


            //Property(x => x.Gps_Latitude)
            //    .HasColumnName("Gps_Latitude")
            //    .HasColumnType("nvarchar")
            //    .IsOptional()
            //    .HasMaxLength(20)
            //    ;
            //Property(x => x.Gps_Longitude)
            //    .HasColumnName("Gps_Longitude")
            //    .HasColumnType("nvarchar")
            //    .IsOptional()
            //    .HasMaxLength(20)
            //    ;


            Property(x => x.Mobile1)
               .HasColumnName("Mobile1")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(15)
               ;
            Property(x => x.Mobile2)
               .HasColumnName("Mobile2")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(15)
               ;
            Property(x => x.Mobile3)
               .HasColumnName("Mobile3")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(15)
               ;


            // Phone
            Property(x => x.Phone1)
                .HasColumnName("Phone1")
                .HasColumnType("nvarchar")
                .IsOptional()
                .HasMaxLength(15)
                ;
            Property(x => x.Phone2)
               .HasColumnName("Phone2")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(15)
               ;
            Property(x => x.Phone3)
               .HasColumnName("Phone3")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(15)
               ;


            // Fax
            Property(x => x.Fax1)
               .HasColumnName("Fax1")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(15)
               ;

           

            // Email
            Property(x => x.Email1)
                .HasColumnName("Email1")
                .HasColumnType("nvarchar")
                .IsOptional()
                .HasMaxLength(70)
                ;

            // Facebook
            Property(x => x.Facebook)
               .HasColumnName("Facebook")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(300)
               ;

            // Twitter
            Property(x => x.Twitter)
               .HasColumnName("Twitter")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(300)
               ;


            // Website
            Property(x => x.Website)
               .HasColumnName("Website")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(300)
               ;

            // Instagram
            Property(x => x.Instagram)
               .HasColumnName("Instagram")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(300)
               ;

            // Snapchat
            Property(x => x.Snapchat)
               .HasColumnName("Snapchat")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(300)
               ;

            // LinkedIn
            Property(x => x.LinkedIn)
               .HasColumnName("LinkedIn")
               .HasColumnType("nvarchar")
               .IsOptional()
               .HasMaxLength(300)
               ;

           

            // IsMaster
            //Property(x => x.IsMain)
            //   .HasColumnName("IsMain")
            //   .HasColumnType("bit")
            //   .IsRequired()
            //   ;
            //Property(x => x.LocationText)
            //  .HasColumnName("LocationText")
            //  .HasColumnType("nvarchar")
            //  .IsOptional()
            //  .HasMaxLength(300)
            //  ;

        }

    }
}