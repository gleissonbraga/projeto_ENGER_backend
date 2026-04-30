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
    public class ConstructionEmployeeMap : IEntityTypeConfiguration<ConstructionEmployee>
    {
        public void Configure(EntityTypeBuilder<ConstructionEmployee> builder)
        {
            builder.ToTable("OBRAS_FUNCIONARIOS");
            builder.HasKey(ce => ce.ConstructionEmployeeId);

            builder.Property(ce => ce.ConstructionEmployeeId).HasColumnName("CD_OBRA_FUNCIONARIO").ValueGeneratedOnAdd();
            builder.Property(ce => ce.EmployeeId).HasColumnName("CD_FUNCIONARIO");
            builder.Property(ce => ce.ConstructionId).HasColumnName("CD_OBRA");

            builder.HasOne(ce => ce.Construction).WithMany().HasForeignKey(ce => ce.ConstructionId);
            builder.HasOne(ce => ce.Employee).WithMany().HasForeignKey(ce => ce.EmployeeId);
        }
    }
}
