using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
    public class ActivationConfiguration
    {
        public ActivationConfiguration(EntityTypeBuilder<Activation> entity)
        {
            entity.Property(e => e.ActivationKey).HasMaxLength(15);

            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.ActivationType)
                .WithMany(p => p.Activation)
                .HasForeignKey(d => d.ActivationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Activatio__Activ__4BAC3F29");


  
        }
    }
}
