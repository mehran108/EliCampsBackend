using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class LeadsConfiguration
    {
        public LeadsConfiguration(EntityTypeBuilder<Leads> entity)
        {
            entity.Property(e => e.Address).HasMaxLength(250);

            entity.Property(e => e.Barcode).HasMaxLength(250);

            entity.Property(e => e.Company).HasMaxLength(250);

            entity.Property(e => e.Country).HasMaxLength(250);

            entity.Property(e => e.CountryCode).HasMaxLength(250);

            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Designation).HasMaxLength(250);

            entity.Property(e => e.Email).HasMaxLength(250);

            entity.Property(e => e.FirstName).HasMaxLength(250);

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.Phone).HasMaxLength(250);

            entity.Property(e => e.Sduid).HasColumnName("SDUId");

            entity.Property(e => e.SurName).HasMaxLength(250);

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}
