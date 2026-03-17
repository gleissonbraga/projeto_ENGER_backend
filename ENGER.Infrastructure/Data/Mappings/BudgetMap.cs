using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BudgetMap : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("ORCAMENTO");

        builder.HasKey(o => o.BudgetId);

        builder.Property(o => o.BudgetId)
            .HasColumnName("CD_ORCAMENTO")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.Description)
            .HasColumnName("DS_ORCAMENTO")
            .HasMaxLength(255);

        builder.Property(o => o.CompanyId)
            .HasColumnName("CD_EMPRESA");

        builder.Property(o => o.ClientId)
            .HasColumnName("CD_CLIENTE");

        builder.Property(o => o.Status)
            .HasColumnName("STATUS_ORCAMENTO");

        builder.Property(o => o.TotalStepsValue)
            .HasColumnName("VL_TOTAL_ETAPAS")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalMaterialsValue)
            .HasColumnName("VL_TOTAL_MATERIAIS")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalValue)
            .HasColumnName("VL_TOTAL")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.Notes)
            .HasColumnName("DS_OBSERVACAO")
            .HasMaxLength(255);

        builder.Property(o => o.EntryDate)
            .HasColumnName("DT_ENTRADA")
            .HasColumnType("date");

        builder.Property(o => o.UpdatedAt)
            .HasColumnName("DT_ATUALIZACAO")
            .HasColumnType("timestamp");

        // 🔗 Relacionamentos
        builder.HasOne(o => o.Company)
            .WithMany()
            .HasForeignKey(o => o.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}