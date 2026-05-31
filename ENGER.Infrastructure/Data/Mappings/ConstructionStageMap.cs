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
    public class ConstructionStageMap
    {
        public void Configure(EntityTypeBuilder<ConstructionStage> builder)
        {
            builder.ToTable("OBRA_ETAPAS");
            builder.HasKey(s => s.StageId);

            builder.Property(s => s.StageId).HasColumnName("CD_ETAPA").ValueGeneratedOnAdd();
            builder.Property(s => s.Description).HasColumnName("DS_ETAPA").HasMaxLength(255);
            builder.Property(s => s.Order).HasColumnName("NR_ORDEM");
            builder.Property(s => s.ConstructionId).HasColumnName("CD_OBRA");
            builder.Property(s => s.Status).HasColumnName("STATUS_ETAPA");

            builder.HasOne(s => s.Construction).WithMany().HasForeignKey(s => s.ConstructionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
