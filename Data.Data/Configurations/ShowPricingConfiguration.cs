using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
  public  class ShowPricingConfiguration
    {
        public ShowPricingConfiguration(EntityTypeBuilder<ShowPricing> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Pricing)
                .WithMany(p => p.ShowPricing)
                .HasForeignKey(d => d.PricingId)
                .HasConstraintName("FK_ShowPricing_Pricing");

            entity.HasOne(d => d.Show)
                .WithMany(p => p.ShowPricing)
                .HasForeignKey(d => d.ShowId)
                .HasConstraintName("FK_ShowPricing_Show");
        }
    }
}
