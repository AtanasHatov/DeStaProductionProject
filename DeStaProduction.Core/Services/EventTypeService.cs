using Microsoft.EntityFrameworkCore;
using DeStaProduction.Core.Contracts;
using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.Core.DTOs;

namespace DeStaProduction.Core.Services
{
    public class EventTypeService : IEventTypeService
    {
        private readonly ApplicationDbContext context;

        public EventTypeService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<EventTypeDto>> GetAllAsync()
            => await context.EventTypes
                .Select(x => new EventTypeDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

        public async Task AddAsync(string name)
        {
            await context.EventTypes.AddAsync(new EventType
            {
                Id = Guid.NewGuid(),
                Name = name
            });
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.EventTypes.FindAsync(id);
            if (entity != null)
            {
                context.EventTypes.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
