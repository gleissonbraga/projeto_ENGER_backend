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
            builder.HasKey(c => c.CodigoEmpresa);

            builder.Property(c => c.CodigoEmpresa)
                .HasColumnName("CD_EMPRESA")
                .ValueGeneratedOnAdd(); // Identidade no banco

            builder.Property(c => c.DataEntrada)
                .HasColumnName("DT_ENTRADA")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                .HasColumnName("DT_ATUALIZACAO")
                .IsRequired();

            builder.Property(c => c.CodigoAssinatura)
                .HasColumnName("CD_ASSINATURA")
                .IsRequired();
        }
    }
}
