using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class SduactivationConfiguration
    {
        public SduactivationConfiguration(EntityTypeBuilder<Sduactivation> entity)
        {
            entity.ToTable("SDUActivation");

            entity.Property(e => e.SduactivationId).HasColumnName("SDUActivationId");

            entity.Property(e => e.ActivationTime).HasColumnType("datetime");

            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Activation)
                .WithMany(p => p.Sduactivation)
                .HasForeignKey(d => d.ActivationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SDUActiva__Activ__5D60DB10");



        }
    }
}
