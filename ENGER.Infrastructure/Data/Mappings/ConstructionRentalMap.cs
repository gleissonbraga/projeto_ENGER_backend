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
    public class ConstructionRentalMap : IEntityTypeConfiguration<ConstructionRental>
    {
        public void Configure(EntityTypeBuilder<ConstructionRental> builder)
        {
            builder.ToTable("ALUGUEIS");
            builder.HasKey(r => r.RentalId);

            builder.Property(r => r.RentalId).HasColumnName("CD_ALUGUEL").ValueGeneratedOnAdd();
            builder.Property(r => r.EquipmentDescription).HasColumnName("DS_EQUIPAMENTO").HasMaxLength(255);
            builder.Property(r => r.RentalValue).HasColumnName("VL_ALUGUEL").HasColumnType("decimal(18,2)");
            builder.Property(r => r.DaysCount).HasColumnName("NR_DIAS");
            builder.Property(r => r.EntryDate).HasColumnName("DT_ENTRADA");
            builder.Property(r => r.ExitDate).HasColumnName("DT_SAIDA");
            builder.Property(r => r.ReceivedBy).HasColumnName("NM_RECEBIDO_POR").HasMaxLength(255);
            builder.Property(r => r.ReturnedBy).HasColumnName("NM_DEVOLVIDO_POR").HasMaxLength(255);
            builder.Property(r => r.ConstructionId).HasColumnName("CD_OBRA");

            builder.HasOne(r => r.Construction).WithMany().HasForeignKey(r => r.ConstructionId);
        }
    }
}
