using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class ServerConfiguration
    {
        public ServerConfiguration(EntityTypeBuilder<Server> entity)
        {
            entity.Property(e => e.ServerName)
                   .HasMaxLength(255)
                   .IsUnicode(false);

            entity.Property(e => e.ServerUrl)
                .HasMaxLength(1000)
                .IsUnicode(false);
        }
    }
}
