using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;

public class ScheduleService : IScheduleService
{
    private readonly ApplicationDbContext context;

    public ScheduleService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<ScheduleDto>> GetAllAsync(int month, int year, string role, Guid userId)
    {
        var query = context.Schedules
            .Include(s => s.User)
            .Include(s => s.Performance)
            .ThenInclude(p => p.Event)
            .AsQueryable();

        if (role == "Artist")
        {
            query = query.Where(s => s.UserId == userId);
        }

        query = query.Where(s => s.Date.Month == month && s.Date.Year == year);

        var data = await query.ToListAsync();

        return data.Select(s => new ScheduleDto
        {
            Id = s.Id,
            UserId = s.UserId,
            PerformanceId = s.PerformanceId,
            Date = s.Date,
            IsAvailable = s.IsAvailable,
            Type = s.Type,
            Notes = s.Notes,
            IsPublic = s.IsPublic,
            UserName = s.User?.UserName,
            PerformanceTitle = s.Performance?.Title
        });
    }

    public async Task<bool> IsUserAvailable(Guid userId, DateTime date)
    {
        var availability = await context.Schedules.FirstOrDefaultAsync(s =>
            s.UserId == userId &&
            s.Date.Date == date.Date &&
            s.Type == "Availability"
        );

        return availability == null || availability.IsAvailable;
    }

    public async Task AddTaskAsync(Schedule model)
    {
        context.Schedules.Add(model);
        await context.SaveChangesAsync();
    }

    public async Task SetAvailability(Guid userId, DateTime date, bool isAvailable)
    {
        var existing = await context.Schedules.FirstOrDefaultAsync(s =>
            s.UserId == userId &&
            s.Date.Date == date.Date &&
            s.Type == "Availability"
        );

        if (existing != null)
        {
            existing.IsAvailable = isAvailable;
        }
        else
        {
            context.Schedules.Add(new Schedule
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Date = date,
                Type = "Availability",
                IsAvailable = isAvailable,
                IsPublic = false
            });
        }

        await context.SaveChangesAsync();
    }
}
