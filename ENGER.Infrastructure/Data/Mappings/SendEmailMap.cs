using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENGER.Infrastructure.Data.Mappings
{
    public class SendEmailMap : IEntityTypeConfiguration<SendEmail>
    {
        public void Configure(EntityTypeBuilder<SendEmail> builder)
        {
            builder.ToTable("ENVIA_EMAIL");
            builder.HasKey(x => x.EmailId);

            builder.Property(x => x.To).HasColumnName("DS_DESTINATARIO").IsRequired();
            builder.Property(x => x.Subject).HasColumnName("DS_ASSUNTO").IsRequired();
            builder.Property(x => x.Body).HasColumnName("DS_CORPO").HasColumnType("text");
            builder.Property(x => x.Status).HasColumnName("STATUS").HasConversion<int>(); ;
            builder.Property(x => x.SentAt).HasColumnName("DT_ENVIO").HasColumnType("timestamptz");
            builder.Property(x => x.RecordDate).HasColumnName("DT_GRAVACAO").HasColumnType("timestamptz");
            builder.Property(x => x.AddresAttachment).HasColumnName("TX_ARQUIVO").HasColumnType("timestamptz");
        }
    }
}
