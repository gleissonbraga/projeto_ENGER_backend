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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USUARIOS"); // Nome da tabela no Postgres
            builder.HasKey(c => c.UserId);

            builder.Property(c => c.UserId)
                .HasColumnName("CD_USUARIO")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Username)
                .HasColumnName("NM_USUARIO")
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasColumnName("EMAIL")
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(c => c.Password)
               .HasColumnName("SENHA")
               .HasMaxLength(60)
               .IsRequired();

            builder.Property(c => c.Admin)
               .HasColumnName("ADMIN")
               .HasColumnType("integer")
               .IsRequired();

            builder.Property(c => c.EntryDate)
                .HasColumnName("DT_ENTRADA")
                .HasMaxLength(50);


            builder.Property(c => c.UpdateDate)
                .HasColumnName("DT_ATUALIZACAO")
                .HasMaxLength(50);

            builder.HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
