using ENGER.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ENGER.Infrastructure.Data.Mappings
{
    public class SendEmailMap : IEntityTypeConfiguration<SendEmail>
    {
        public void Configure(EntityTypeBuilder<SendEmail> builder)
        {
            builder.ToTable("ENVIA_EMAIL");

            builder.HasKey(x => x.EmailId);
            builder.Property(x => x.EmailId).HasColumnName("ID_EMAIL");

            builder.Property(x => x.To).HasColumnName("DS_DESTINATARIO").IsRequired().HasMaxLength(250);
            builder.Property(x => x.Subject).HasColumnName("DS_ASSUNTO").IsRequired().HasMaxLength(200);

            builder.Property(x => x.Body).HasColumnName("DS_CORPO").HasColumnType("text").IsRequired();

            builder.Property(x => x.Status).HasColumnName("STATUS").HasConversion<int>();

            builder.Property(x => x.SentAt).HasColumnName("DT_ENVIO").HasColumnType("timestamptz");
            builder.Property(x => x.RecordDate).HasColumnName("DT_GRAVACAO").HasColumnType("timestamptz");

            // AJUSTE: Mapeando os bytes do anexo e o nome do arquivo
            builder.Property(x => x.AttachmentContent)
                .HasColumnName("BL_ANEXO")
                .HasColumnType("bytea"); // Tipo binário do PostgreSQL

            builder.Property(x => x.FileName)
                .HasColumnName("NM_ARQUIVO")
                .HasMaxLength(150);
        }
    }
}
