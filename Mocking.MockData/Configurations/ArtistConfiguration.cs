﻿using InfoTracker.MockData.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoTracker.MockData.Configurations
{
    public class ArtistConfiguration
    {
        public ArtistConfiguration(EntityTypeBuilder<Artist> entity)
        {
            entity.Property(e => e.Name).HasMaxLength(120);
        }
    }
}
