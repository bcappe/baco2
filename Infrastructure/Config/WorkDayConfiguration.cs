using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config
{
    public class WorkDayConfiguration : IEntityTypeConfiguration<WorkDay>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkDay> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Date).IsRequired().HasColumnType("date");
            builder.Property(p => p.EmployeeId).IsRequired();
            builder.Property(p => p.CheckIn).HasColumnType("date");
            builder.Property(p => p.CheckOut).HasColumnType("date");
            builder.Property(p => p.LunchTimeIn).HasColumnType("date");
            builder.Property(p => p.LunchTimeOut).HasColumnType("date");
        }
    }

}
