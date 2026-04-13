using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;
public class EventService : IEventService
{
    private readonly ApplicationDbContext context;

    public EventService(ApplicationDbContext _context)
    {
        context = _context;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        return await context.Events
            .Include(e => e.Type)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Duration = e.Duration,
                TypeName = e.Type.Name,
                ImagePath = e.ImagePath
            }).ToListAsync();
    }

    public async Task AddAsync(AddEventDto model)
    {
        var entity = new Event
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description,
            Duration = model.Duration,
            EventType = model.EventTypeId,
            ImagePath = model.ImagePath
        };

        await context.Events.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.Events.FindAsync(id);
        if (entity != null)
        {
            context.Events.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<EventDto?> GetByIdAsync(Guid id)
    {
        return await context.Events
            .Include(e => e.Type)
            .Include(e => e.Performances)
             .ThenInclude(p => p.Location)
            .Where(e => e.Id == id)
            .Select(e => new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                Duration = e.Duration,
                TypeName = e.Type.Name,

                Performances = e.Performances
    .Select(p => new PerformanceShortDto
    {
        Title = p.Title,
        Date = p.Date,
        Location = p.Location.Name
    })
    .ToList()
            })
            .FirstOrDefaultAsync();
    }
}