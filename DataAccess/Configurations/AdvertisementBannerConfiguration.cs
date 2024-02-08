using Entities.Concrete.TableModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class AdvertisementBannerConfiguration : IEntityTypeConfiguration<AdvertisementBanner>
    {
        public void Configure(EntityTypeBuilder<AdvertisementBanner> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Deleted).HasDefaultValue<int>(0);

            builder.HasIndex(x => new { x.Title, x.Deleted })
                   .HasDatabaseName("idx_AdvertisementBanner_Title_Deleted");
        }
    }
}
