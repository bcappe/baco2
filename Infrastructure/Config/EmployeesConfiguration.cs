using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Config
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.JobTitle).HasMaxLength(100);
            builder.Property(p => p.Department).HasMaxLength(100);
            builder.Property(p => p.JobContractType).IsRequired().HasMaxLength(100);
            builder.Property(p => p.StartedIn).IsRequired().HasColumnType("date");
            builder.Property(p => p.RfidCode).HasMaxLength(30);
            builder.HasOne(b => b.WorkDaySchedule).WithMany().HasForeignKey(p => p.WorkDayScheduleId);
        }
    }

}