using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class DatabaseConfiguration
    {
        public DatabaseConfiguration(EntityTypeBuilder<Database> entity)
        {
            entity.Property(e => e.DatabaseName)
                   .HasMaxLength(128)
                   .IsUnicode(false);

            entity.HasOne(d => d.Server)
                .WithMany(p => p.Database)
                .HasForeignKey(d => d.ServerId)
                .HasConstraintName("Database_ServerId_FK");
        }
    }
}
