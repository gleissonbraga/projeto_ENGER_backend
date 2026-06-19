using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PositionMap : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("CARGOS");

        builder.HasKey(e => e.PositionId);

        builder.Property(e => e.PositionId)
            .HasColumnName("CD_CARGO")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.DescriptionPosition)
            .HasColumnName("DS_CARGO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.CompanyId)
            .HasColumnName("CD_EMPRESA")
            .IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}