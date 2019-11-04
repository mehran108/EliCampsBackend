using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class CountriesConfiguration
    {
        public CountriesConfiguration(EntityTypeBuilder<Countries> entity)
        {
            entity.ToTable("countries");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Phonecode).HasColumnName("phonecode");

            entity.Property(e => e.Sortname)
                .IsRequired()
                .HasColumnName("sortname")
                .HasMaxLength(3)
                .IsUnicode(false);
        }
    }
}
