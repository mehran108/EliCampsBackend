using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
    public class LookupValueConfiguration
    {
        public LookupValueConfiguration(EntityTypeBuilder<LookupValue> entity)
        {

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description).HasColumnName("description");

            entity.Property(e => e.LookupTableId).HasColumnName("lookupTableId");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.LookupTable)
                .WithMany(p => p.LookupValue)
                .HasForeignKey(d => d.LookupTableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LookupValue_LookupTable");
        }
    }
}
