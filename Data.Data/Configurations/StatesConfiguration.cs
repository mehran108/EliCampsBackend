using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class StatesConfiguration
    {
        public StatesConfiguration(EntityTypeBuilder<States> entity)
        {
            entity.ToTable("states");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CountryId)
                .HasColumnName("countryid")
                .HasDefaultValueSql("('1')");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Country)
                .WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__states__country___50FB042B");


        }
    }
}
