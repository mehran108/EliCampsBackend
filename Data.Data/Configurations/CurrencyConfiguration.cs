using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
  public  class CurrencyConfiguration
    {
        public CurrencyConfiguration(EntityTypeBuilder<Currency> entity)
        {
            entity.HasKey(e => e.Iso);

            entity.Property(e => e.Iso)
                .HasColumnName("ISO")
                .HasColumnType("nchar(3)")
                .ValueGeneratedNever();

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.Name).HasMaxLength(200);
        }
    }
}
