using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config
{
    public class WorkDayScheduleConfiguration : IEntityTypeConfiguration<WorkDaySchedule>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkDaySchedule> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.CheckIn).IsRequired();
            builder.Property(p => p.CheckOut).IsRequired();
            builder.Property(p => p.LunchTimeIn).IsRequired();
            builder.Property(p => p.LunchTimeOut).IsRequired();
        }
    }

}
