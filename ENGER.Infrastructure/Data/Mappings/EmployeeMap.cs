using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("FUNCIONARIOS");

        builder.HasKey(e => e.EmployeeId);

        builder.Property(e => e.EmployeeId)
            .HasColumnName("CD_FUNCIONARIO")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.EmployeeName)
            .HasColumnName("NM_FUNCIONARIO")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.RegistrationNumber)
            .HasColumnName("CPF")
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(e => e.NumberGeneralRegistration)
            .HasColumnName("RG")
            .HasMaxLength(11);

        builder.Property(e => e.DateOfBirth)
            .HasColumnName("DT_NASCIMENTO");

        builder.Property(e => e.AdmissionDate)
            .HasColumnName("DT_ADMISSAO");

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("NR_TELEFONE")
            .HasMaxLength(14);

        builder.Property(e => e.CellNumber)
            .HasColumnName("NR_CELULAR")
            .HasMaxLength(14);

        builder.Property(e => e.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(100);

        builder.Property(e => e.EntryDate)
            .HasColumnName("DT_ENTRADA")
            .IsRequired();

        builder.Property(e => e.UpdateDate)
            .HasColumnName("DT_ATUALIZACAO")
            .IsRequired();

        builder.Property(e => e.CompanyId)
            .HasColumnName("CD_EMPRESA")
            .IsRequired();

        builder.Property(e => e.PositionId)
            .HasColumnName("CD_CARGO")
            .IsRequired();

        builder.HasOne(e => e.Position)
            .WithMany()
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}