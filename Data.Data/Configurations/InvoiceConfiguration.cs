using ELI.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ELI.Entity.Main;

namespace ELI.Data.Configurations
{
    public class InvoiceConfiguration
    {
        public InvoiceConfiguration(EntityTypeBuilder<Invoice> entity)
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(450);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.DiscountCode).HasMaxLength(12);

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.KeyPrice).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.RequestXml).HasColumnName("RequestXML");

            entity.Property(e => e.ResponseXml).HasColumnName("ResponseXML");

            entity.Property(e => e.RestrictedCode).HasMaxLength(12);

            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.Property(e => e.UpdatedBy).HasMaxLength(450);

            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}
