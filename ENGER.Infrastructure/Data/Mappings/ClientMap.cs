using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENGER.Infrastructure.Data.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("CLIENTES");

            builder.HasKey(c => c.ClientId);

            builder.Property(c => c.ClientId)
                .HasColumnName("CD_CLIENTE")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.ReasonName)
                .HasColumnName("NM_RAZAO")
                .HasMaxLength(100);

            builder.Property(c => c.FantasyName)
                .HasColumnName("NM_FANTASIA")
                .HasMaxLength(100);

            builder.Property(c => c.RegistrationNumber)
                .HasColumnName("CPF_CNPJ")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(c => c.RGIENumber)
                .HasColumnName("NR_IERG")
                .HasMaxLength(14);

            builder.Property(c => c.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50);

            builder.Property(c => c.Street)
                .HasColumnName("LOGRADOURO")
                .HasMaxLength(50);

            builder.Property(c => c.Number)
                .HasColumnName("NUMERO")
                .HasMaxLength(10);

            builder.Property(c => c.City)
                .HasColumnName("CIDADE")
                .HasMaxLength(40);

            builder.Property(c => c.Neighborhood)
                .HasColumnName("BAIRRO")
                .HasMaxLength(30);

            builder.Property(c => c.ZipCode)
                .HasColumnName("CEP")
                .HasMaxLength(8);

            builder.Property(c => c.FederativeUnit)
                .HasColumnName("UF")
                .HasMaxLength(2);

            builder.Property(c => c.PhoneNumber)
                .HasColumnName("NR_TELEFONE")
                .HasMaxLength(14);

            builder.Property(c => c.CellNumber)
                .HasColumnName("NR_CELULAR")
                .HasMaxLength(14);

            builder.Property(c => c.EntryDate)
                .HasColumnName("DT_ENTRADA")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnName("DT_ATUALIZACAO")
                .IsRequired();

            builder.Property(c => c.CompanyId)
                .HasColumnName("CD_EMPRESA");

            builder.HasOne(c => c.Company)
                .WithMany()
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}