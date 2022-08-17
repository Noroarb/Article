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
    internal class PaidAdsConfiguration : EntityTypeConfiguration<PaidAds>
    {
        internal PaidAdsConfiguration()
        {

            ToTable("PaidAds");

            //HasOptional(x => x.Brand)
            //.WithMany(x => x.PaidAds)
            //.HasForeignKey(x => x.BrandId)
            //;

            HasKey(x => x.Id)
              .Property(x => x.Id)
              .HasColumnName("Id")
              .HasColumnType("int")
              .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.ImagePath)
                .HasColumnName("ImagePath")
                .HasColumnType("nvarchar")
                .IsRequired()
                ;
            Property(x => x.Link)
                .HasColumnName("Link")
                .HasColumnType("nvarchar")
                .IsOptional()
                ;
            Property(x => x.Date)
                     .HasColumnName("Date")
                     .HasColumnType("datetime2")
                      .IsRequired()
                     ;

        }
    }
}
