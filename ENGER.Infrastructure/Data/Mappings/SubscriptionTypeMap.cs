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
    public class SubscriptionTypeMap : IEntityTypeConfiguration<SubscriptionType>
    {
        public void Configure(EntityTypeBuilder<SubscriptionType> builder)
        {
            builder.ToTable("TIPO_ASSINATURA"); // Nome da tabela no Postgres
            builder.HasKey(c => c.SubscriptionTypeId);

            builder.Property(c => c.SubscriptionTypeId)
                .HasColumnName("CD_TP_ASSINATURA")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.DescriptionSubscriptionType)
               .HasColumnName("DS_TP_ASSINATURA")
               .HasMaxLength(40)
               .ValueGeneratedOnAdd();

            builder.Property(c => c.SubscriptionValue)
               .HasColumnName("VL_ASSINATURA")
               .HasColumnType("numeric(18,2)")
               .ValueGeneratedOnAdd();
        }
    }
}
