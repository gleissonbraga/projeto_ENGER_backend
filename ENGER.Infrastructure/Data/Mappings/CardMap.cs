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
            builder.ToTable("CARTOES");

            builder.HasKey(c => c.CardId);

            builder.Property(c => c.CardId)
                .HasColumnName("CD_CARTAO")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.MercadoPagoCustomerId)
                .HasColumnName("CD_MP_CUSTOMER")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.MercadoPagoCardId)
                .HasColumnName("CD_MP_CARTAO")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastCardNumber)
                .HasColumnName("NR_FINAL_CARTAO")
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(c => c.Brand)
                .HasColumnName("NM_BANDEIRA")
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.ExpirationMonth)
                .HasColumnName("NR_MES_EXPIRACAO")
                .IsRequired();

            builder.Property(c => c.ExpirationYear)
                .HasColumnName("NR_ANO_EXPIRACAO")
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
