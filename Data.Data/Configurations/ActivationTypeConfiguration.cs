using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{

    public class ActivationTypeConfiguration
    {
        public ActivationTypeConfiguration(EntityTypeBuilder<ActivationType> entity)
        {
            entity.Property(e => e.ActivationTypeName)
                        .HasMaxLength(15)
                        .IsUnicode(false);

            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}
