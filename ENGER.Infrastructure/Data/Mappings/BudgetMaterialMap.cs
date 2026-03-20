using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BudgetMaterialMap : IEntityTypeConfiguration<BudgetMaterial>
{
    public void Configure(EntityTypeBuilder<BudgetMaterial> builder)
    {
        builder.ToTable("ORCAMENTO_MATERIAIS");
        builder.HasKey(m => m.BudgetMaterialId);

        builder.Property(m => m.BudgetMaterialId).HasColumnName("CD_ORCAMENTO_MATERIAL");
        builder.Property(m => m.Description).HasColumnName("DS_MATERIAL").HasMaxLength(255);
        builder.Property(m => m.Unit).HasColumnName("CD_UNIDADE_MEDIDA").HasMaxLength(10);
        builder.Property(m => m.PlannedQuantity).HasColumnName("QTD_PREVISTA").HasColumnType("decimal(12,3)");
        builder.Property(m => m.UnitCost).HasColumnName("VL_UNITARIO_CUSTO").HasColumnType("decimal(12,2)");
        builder.Property(m => m.IsClientProvided).HasColumnName("IN_FORNECIDO_CLIENTE");
    }
}