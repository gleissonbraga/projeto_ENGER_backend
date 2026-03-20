using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BudgetLaborMap : IEntityTypeConfiguration<BudgetLabor>
{
    public void Configure(EntityTypeBuilder<BudgetLabor> builder)
    {
        builder.ToTable("ORCAMENTO_MAO_DE_OBRA");
        builder.HasKey(l => l.BudgetLaborId);

        builder.Property(l => l.BudgetLaborId).HasColumnName("CD_ORCAMENTO_MO");
        builder.Property(l => l.RoleId).HasColumnName("CD_CARGO");
        builder.Property(l => l.PlannedHours).HasColumnName("QTD_HORAS_PREVISTAS").HasColumnType("decimal(10,2)");
        builder.Property(l => l.HourlyRate).HasColumnName("VL_HORA_CUSTO").HasColumnType("decimal(12,2)");
        builder.Property(l => l.SocialCharges).HasColumnName("PR_ENCARGOS_SOCIAIS").HasColumnType("decimal(5,2)");
    }
}