using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class CitiesConfiguration
    {
        public CitiesConfiguration(EntityTypeBuilder<Cities> entity)
        {
            entity.ToTable("cities");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.StateId).HasColumnName("stateid");

            entity.HasOne(d => d.State)
                .WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cities__state_id__5006DFF2");
        }
    }
}
