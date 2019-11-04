using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
   public class QualifierUsersConfiguration
    {
        public QualifierUsersConfiguration(EntityTypeBuilder<QualifierUsers> entity)
        {
            entity.HasKey(e => e.QualifierUserId);
        }
    }
}
