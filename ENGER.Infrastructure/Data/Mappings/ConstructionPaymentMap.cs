using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Data.Mappings
{
    public class ConstructionPaymentMap : IEntityTypeConfiguration<ConstructionPayment>
    {
        public void Configure(EntityTypeBuilder<ConstructionPayment> builder)
        {
            builder.ToTable("OBRAS_PAGAMENTO");
            builder.HasKey(p => p.ConstructionPaymentId);

            builder.Property(p => p.ConstructionPaymentId).HasColumnName("CD_PAG_OBRA").ValueGeneratedOnAdd();
            builder.Property(p => p.PaymentDate).HasColumnName("DT_PAGAMENTO");
            builder.Property(p => p.PaymentTypeId).HasColumnName("CD_TP_PAGAMENTO");
            builder.Property(p => p.ConstructionId).HasColumnName("CD_OBRA");
            builder.Property(p => p.StageId).HasColumnName("CD_ETAPA");
            builder.Property(p => p.PaymentValue).HasColumnName("VL_PAGAMENTO").HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Construction).WithMany().HasForeignKey(p => p.ConstructionId);
            builder.HasOne(p => p.Construction).WithMany(c => c.Payments).HasForeignKey(p => p.ConstructionId);
        }
    }
}
