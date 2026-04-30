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
    public class ConstructionAttachmentMap : IEntityTypeConfiguration<ConstructionAttachment>
    {
        public void Configure(EntityTypeBuilder<ConstructionAttachment> builder)
        {
            builder.ToTable("OBRA_ANEXOS");
            builder.HasKey(a => a.ConstructionAttachmentId);

            builder.Property(a => a.ConstructionAttachmentId).HasColumnName("CD_OBRA_ANEXO").ValueGeneratedOnAdd();
            builder.Property(a => a.Description).HasColumnName("DS_ANEXO").HasMaxLength(255);
            builder.Property(a => a.ImageUrl).HasColumnName("URL_IMAGEM").HasMaxLength(500);
            builder.Property(a => a.ConstructionId).HasColumnName("CD_OBRA");

            builder.HasOne(a => a.Construction).WithMany().HasForeignKey(a => a.ConstructionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
