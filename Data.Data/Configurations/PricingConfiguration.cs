using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class PricingConfiguration
    {
        public PricingConfiguration(EntityTypeBuilder<Pricing> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.CurrencyIso)
                .HasColumnName("CurrencyISO")
                .HasColumnType("nchar(3)");

            entity.Property(e => e.EquivalentKeyAmount).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.EquivalentKeyAmountAdditional).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.EquivalentKeyAmountOffer).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.IsDefault).HasDefaultValueSql("((0))");

            entity.Property(e => e.KeyAmount).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.KeyAmountAdditional).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.KeyAmountOffer).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.TaxName).HasMaxLength(450);

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CurrencyIsoNavigation)
                .WithMany(p => p.Pricing)
                .HasForeignKey(d => d.CurrencyIso)
                .HasConstraintName("FK_Pricing_Currency");

            entity.HasOne(d => d.Region)
                .WithMany(p => p.Pricing)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK_Pricing_Region");
        }
    }
}
