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

        builder.Property(o => o.KeyBudget)
            .HasColumnName("CHAVE_ORCAMENTO")
            .IsRequired();

        builder.Property(o => o.CompanyId)
            .HasColumnName("CD_EMPRESA");

        builder.Property(o => o.ClientId)
            .HasColumnName("CD_CLIENTE");

        builder.Property(o => o.UserId)
            .HasColumnName("CD_USUARIO");

        builder.Property(x => x.Status)
            .HasColumnName("STATUS_ORCAMENTO")
            .HasConversion<int>();

        builder.Property(o => o.TotalStepsValue)
            .HasColumnName("VL_TOTAL_ETAPAS")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalMaterialsValue)
            .HasColumnName("VL_TOTAL_MATERIAIS")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalValue)
            .HasColumnName("VL_TOTAL")
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.Observation)
            .HasColumnName("DS_OBSERVACAO")
            .HasMaxLength(255);

        builder.Property(o => o.Street)
            .HasColumnName("DS_LOGRADOURO")
            .HasMaxLength(255);

        builder.Property(o => o.Number)
            .HasColumnName("NR_LOGRADOURO")
            .HasMaxLength(20);

        builder.Property(o => o.City)
            .HasColumnName("NM_CIDADE")
            .HasMaxLength(150);

        builder.Property(o => o.Neighborhood)
            .HasColumnName("NM_BAIRRO")
            .HasMaxLength(150);

        builder.Property(o => o.ZipCode)
            .HasColumnName("NR_CEP")
            .HasMaxLength(10);

        builder.Property(o => o.StateAbbreviation)
            .HasColumnName("SG_UF")
            .HasMaxLength(2);

        builder.Property(o => o.StateDescription)
            .HasColumnName("NM_ESTADO")
            .HasMaxLength(100);

        builder.Property(o => o.EntryDate)
            .HasColumnName("DT_ENTRADA")
            .HasColumnType("timestamptz");

        builder.Property(o => o.UpdatedAt)
            .HasColumnName("DT_ATUALIZACAO")
            .HasColumnType("timestamptz");

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