using InfoTracker.MockData.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoTracker.MockData.Configurations
{
    public class MediaTypeConfiguration
    {
        public MediaTypeConfiguration(EntityTypeBuilder<MediaType> entity)
        {
            entity.Property(e => e.Name).HasMaxLength(120);
        }
    }
}
