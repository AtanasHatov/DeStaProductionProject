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
                Date = model.Date
            });

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Performances.FindAsync(id);
            if (entity != null)
            {
                context.Performances.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
