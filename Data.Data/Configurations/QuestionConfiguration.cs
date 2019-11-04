using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELI.Data.Configurations
{
    public class QuestionConfiguration
    {
        public QuestionConfiguration(EntityTypeBuilder<Question> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            //entity.HasOne(d => d.QuestionType)
            //    .WithMany(p => p.Question)
            //    .HasForeignKey(d => d.QuestionTypeId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Question__Questi__5535A963");
        }
    }
}
