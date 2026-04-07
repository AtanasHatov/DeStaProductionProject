using DeStaProduction.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Type)
            .IsRequired();

        builder.Property(s => s.Date)
            .IsRequired();

        builder.HasOne(s => s.User)
            .WithMany(u => u.Schedules)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Performance)
            .WithMany(p => p.Schedules)
            .HasForeignKey(s => s.PerformanceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}