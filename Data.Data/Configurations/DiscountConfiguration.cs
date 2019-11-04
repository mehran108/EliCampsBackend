using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class DiscountConfiguration
    {
        public DiscountConfiguration(EntityTypeBuilder<Discount> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.DiscountValue)
                .HasColumnName("Discount")
                .HasColumnType("decimal(18, 0)");

            entity.Property(e => e.DiscountCode).HasMaxLength(255);

            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}
