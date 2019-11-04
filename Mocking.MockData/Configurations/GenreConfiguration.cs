using InfoTracker.MockData.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoTracker.MockData.Configurations
{
    public class GenreConfiguration
    {
        public GenreConfiguration(EntityTypeBuilder<Genre> entity)
        {
            entity.Property(e => e.Name).HasMaxLength(120);
        }
    }
}
