using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
    public class LookupTableConfiguration
    {
        public LookupTableConfiguration(EntityTypeBuilder<LookupTable> entity)
        {

            entity.Property(e => e.Description).IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }

}


