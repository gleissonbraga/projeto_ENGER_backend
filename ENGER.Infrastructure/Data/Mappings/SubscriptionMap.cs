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
    public class SubscriptionMap : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("ASSINATURA"); // Nome da tabela no Postgres
            builder.HasKey(c => c.SubscriptionId);

            builder.Property(c => c.SubscriptionId)
                .HasColumnName("CD_ASSINATURA")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.SubscriptionCode)
               .HasColumnName("CD_CHAVE")
               .HasMaxLength(40);

            builder.Property(c => c.ExpirationDate)
               .HasColumnName("DT_VENCIMENTO");

            builder.Property(c => c.PaymentDate)
                .HasColumnName("DT_PAGAMENTO");

            builder.Property(c => c.StartDate)
                .HasColumnName("DT_INICIO");

            builder.Property(c => c.NextBillingDate)
                .HasColumnName("DT_PROXIMA_COBRANCA");

            builder.Property(c => c.StatusSubscription)
              .HasColumnName("STATUS_ASSINATURA")
              .HasColumnType("integer");

            builder.Property(c => c.PaymentDate)
              .HasColumnName("DT_PAGAMENTO");

            builder.HasOne<SubscriptionType>()
               .WithOne()
               .HasPrincipalKey<SubscriptionType>(s => s.SubscriptionTypeId)
               .HasForeignKey<Subscription>(c => c.TypeSubscriptionId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
