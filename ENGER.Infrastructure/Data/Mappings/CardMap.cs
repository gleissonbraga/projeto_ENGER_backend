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
    public class CardMap: IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("CARTOES"); // Nome da tabela no Postgres
            builder.HasKey(c => c.CardId);

            builder.Property(c => c.CardId)
                .HasColumnName("CD_CARTAO")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.CardToken)
                .HasColumnName("CD_TOKEN")
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(c => c.LastCardNumber)
               .HasColumnName("NR_CARTAO")
               .IsRequired(true)
               .HasMaxLength(4);

            builder.Property(c => c.Brand)
               .HasColumnName("NM_BANDEIRA")
               .IsRequired(true)
               .HasMaxLength(30);

            builder.Property(c => c.ExpirationDateCard)
             .HasColumnName("DT_EXPIRACAO")
             .IsRequired(true)
             .HasMaxLength(8);

        }
    }
}
