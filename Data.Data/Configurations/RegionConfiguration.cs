using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
    public class RegionConfiguration
    {
        public RegionConfiguration(EntityTypeBuilder<Region> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.CurrencyIso)
                .HasColumnName("CurrencyISO")
                .HasColumnType("nchar(3)");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.VisitorsAPIURL).HasColumnName("VisitorsAPIURL");
            entity.Property(e => e.UserManualURL).HasColumnName("UserManualURL");
            entity.Property(e => e.PrivacyURL).HasColumnName("PrivacyURL");


            entity.HasOne(d => d.CurrencyIsoNavigation)
                .WithMany(p => p.Region)
                .HasForeignKey(d => d.CurrencyIso)
                .HasConstraintName("FK_Region_Currency");
        }
    }
}
