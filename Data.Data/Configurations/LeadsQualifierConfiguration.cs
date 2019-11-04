using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class LeadsQualifierConfiguration
    {
        public LeadsQualifierConfiguration(EntityTypeBuilder<LeadsQualifier> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.Sduid).HasColumnName("SDUId");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Leads)
                .WithMany(p => p.LeadsQualifier)
                .HasForeignKey(d => d.LeadsId)
                .HasConstraintName("FK_LeadsQualifier_Leads");
        }
    }
}
