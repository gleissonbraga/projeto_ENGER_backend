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
    public class ConstructionPresenceMap : IEntityTypeConfiguration<ConstructionPresence>
    {
        public void Configure(EntityTypeBuilder<ConstructionPresence> builder)
        {
            builder.ToTable("OBRA_PRESENCA");
            builder.HasKey(p => p.PresenceId);

            builder.Property(p => p.PresenceId).HasColumnName("CD_PRESENCA").ValueGeneratedOnAdd();
            builder.Property(p => p.EmployeeId).HasColumnName("CD_FUNCIONARIO");
            builder.Property(p => p.ConstructionId).HasColumnName("CD_OBRA");
            builder.Property(p => p.UserId).HasColumnName("CD_USUARIO");
            builder.Property(p => p.PresenceDate).HasColumnName("DT_PRESENCA");
            builder.Property(p => p.IsPresent).HasColumnName("IN_PRESENTE"); // Mapeia bool para bit/bool no Postgres

            builder.HasOne(p => p.Employee).WithMany().HasForeignKey(p => p.EmployeeId);
            builder.HasOne(p => p.Construction).WithMany().HasForeignKey(p => p.ConstructionId);
            builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
        }
    }
}
