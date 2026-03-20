using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENGER.Infrastructure.Data.Mappings
{
    public class BudgetStageMap : IEntityTypeConfiguration<BudgetStage>
    {
        public void Configure(EntityTypeBuilder<BudgetStage> builder)
        {
            builder.ToTable("ORCAMENTO_ETAPAS");

            builder.HasKey(s => s.StageId);

            builder.Property(s => s.StageId)
                .HasColumnName("CD_ETAPA")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.BudgetId)
                .HasColumnName("CD_ORCAMENTO")
                .IsRequired();

            builder.Property(s => s.Description)
                .HasColumnName("DS_ETAPA")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Order)
                .HasColumnName("NR_ORDEM")
                .IsRequired();

            // Relacionamentos (Navegação)

            // Uma Etapa pertence a UM Orçamento
            builder.HasOne(s => s.Budget)
                .WithMany(b => b.Stages)
                .HasForeignKey(s => s.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);
            // Cascade: Se o orçamento for excluído, as etapas somem junto.

            // Uma Etapa possui MUITOS Materiais
            builder.HasMany(s => s.Materials)
                .WithOne() // Se não criou a propriedade de volta em BudgetMaterial, deixe vazio
                .HasForeignKey(m => m.StageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Uma Etapa possui MUITAS Mãos de Obra
            builder.HasMany(s => s.Labors)
                .WithOne()
                .HasForeignKey(l => l.StageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}