using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class ErrorLoggingConfiguration
    {
        public ErrorLoggingConfiguration(EntityTypeBuilder<ErrorLogging> entity)
        {
            entity.HasKey(e => e.ErrorId);

            entity.Property(e => e.ErrorId).HasColumnName("errorId");

            entity.Property(e => e.App)
                .HasColumnName("app")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Level)
                .HasColumnName("level")
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Message)
                .HasColumnName("message")
                .IsUnicode(false);

            entity.Property(e => e.TimeStamp)
                .HasColumnName("timeStamp")
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
