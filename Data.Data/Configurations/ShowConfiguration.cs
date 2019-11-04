using ELI.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ELI.Entity.Main;

namespace ELI.Data.Configurations
{
    class ShowConfiguration
    {
        public ShowConfiguration(EntityTypeBuilder<Show> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.EndDate).HasColumnType("date");

            entity.Property(e => e.IsNfc).HasColumnName("IsNFC");

            entity.Property(e => e.LeadsDownloadLimit).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.LeadsSequentialLimit).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Location)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.OnsiteHelpNumber).HasMaxLength(50);

            entity.Property(e => e.PreRegCode)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.ShowKey)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.Property(e => e.ShowName)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Region)
                .WithMany(p => p.Show)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("FK_Show_Region");
        }
    }
}
