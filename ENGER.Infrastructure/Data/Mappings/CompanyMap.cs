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
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("EMPRESAS"); // Nome da tabela no Postgres
            builder.HasKey(c => c.CompanyId);

            builder.Property(c => c.CompanyId)
                .HasColumnName("CD_EMPRESA")
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
                .HasMaxLength(50);


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

            builder.Property(c => c.DateOfBirth)
              .HasColumnName("DT_NASCIMENTO");

            builder.Property(c => c.EntryDate)
                .HasColumnName("DT_ENTRADA")
                .IsRequired();

            builder.Property(c => c.UpdateDate)
                .HasColumnName("DT_ATUALIZACAO")
                .IsRequired();

            builder.Property(c => c.SubscriptionCode)
                .HasColumnName("CD_ASSINATURA")
                .IsRequired(false);

            builder.HasOne<Subscription>()
                .WithOne()
                .HasPrincipalKey<Subscription>(s => s.SubscriptionCode)
                .HasForeignKey<Company>(c => c.SubscriptionCode)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
