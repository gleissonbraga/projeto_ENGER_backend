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
    public class ConstructionMap : IEntityTypeConfiguration<Construction>
    {
        public void Configure(EntityTypeBuilder<Construction> builder)
        {
            builder.ToTable("OBRAS");
            builder.HasKey(c => c.ConstructionId);

            builder.Property(c => c.ConstructionId).HasColumnName("CD_OBRA").ValueGeneratedOnAdd();
            builder.Property(c => c.Description).HasColumnName("DS_OBRA").HasMaxLength(255);
            builder.Property(c => c.BudgetId).HasColumnName("CD_ORCAMENTO");
            builder.Property(c => c.TotalPaidValue).HasColumnName("VL_TOTAL_PAGO").HasColumnType("decimal(18,2)");
            builder.Property(c => c.TotalConstructionValue).HasColumnName("VL_TOTAL_OBRA").HasColumnType("decimal(18,2)");

            // Endereço
            builder.Property(c => c.Street).HasColumnName("LOGRADOURO").HasMaxLength(255);
            builder.Property(c => c.Number).HasColumnName("NR_LOGRADOURO").HasMaxLength(50);
            builder.Property(c => c.City).HasColumnName("CIDADE").HasMaxLength(255);
            builder.Property(c => c.Neighborhood).HasColumnName("BAIRRO").HasMaxLength(255);
            builder.Property(c => c.ZipCode).HasColumnName("CEP").HasMaxLength(20);
            builder.Property(c => c.StateAbbreviation).HasColumnName("UF").HasMaxLength(2);
            builder.Property(c => c.StateDescription).HasColumnName("DS_ESTADO").HasMaxLength(255);

            builder.Property(c => c.StartDate).HasColumnName("DT_INICIO");
            builder.Property(c => c.EstimatedDeliveryDate).HasColumnName("DT_ENTREGA_PREVISTA");
            builder.Property(c => c.FinalizationDate).HasColumnName("DT_FINALIZACAO");
            builder.Property(c => c.Status).HasColumnName("STATUS_OBRA");
            builder.Property(c => c.CompanyId).HasColumnName("CD_EMPRESA");
            builder.Property(c => c.ResponsibleId).HasColumnName("CD_RESPONSAVEL");

            // Relacionamentos
            builder.HasOne(c => c.Budget).WithMany().HasForeignKey(c => c.BudgetId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(c => c.Stages).WithOne(s => s.Construction).HasForeignKey(s => s.ConstructionId);
            builder.HasMany(c => c.Employees).WithOne(e => e.Construction).HasForeignKey(e => e.ConstructionId);
            builder.HasMany(c => c.Payments).WithOne(p => p.Construction).HasForeignKey(p => p.ConstructionId);
            builder.HasMany(c => c.Presences).WithOne(p => p.Construction).HasForeignKey(p => p.ConstructionId);
            builder.HasMany(c => c.Rentals).WithOne(r => r.Construction).HasForeignKey(r => r.ConstructionId);
            builder.HasMany(c => c.Attachments).WithOne(a => a.Construction).HasForeignKey(a => a.ConstructionId);
        }
    }
}
