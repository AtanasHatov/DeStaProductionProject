using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeStaProduction.Core.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly ApplicationDbContext context;

        public PerformanceService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<PerformanceDto>> GetAllAsync()
            => await context.Performances
                .Include(p => p.Event)
                .Include(p => p.Location)
                .Select(p => new PerformanceDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Event = p.Event.Title,
                    Location = p.Location.Name,
                    Date = p.Date
                }).ToListAsync();

        public async Task AddAsync(PerformanceDto model)
        {
            await context.Performances.AddAsync(new Performance
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description, 
                Date = model.Date,
                EventId = model.EventId,
                LocationId = model.LocationId
            });

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var performance = await context.Performances.FindAsync(id);

            if (performance != null)
            {
                var schedules = context.Schedules
                    .Where(s => s.PerformanceId == id);

                context.Schedules.RemoveRange(schedules);

                context.Performances.Remove(performance);

                await context.SaveChangesAsync();
            }
        }
    }
}
