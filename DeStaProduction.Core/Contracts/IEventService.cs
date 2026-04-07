using DeStaProduction.Core.DTOs;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllAsync();
    Task AddAsync(AddEventDto model);
    Task DeleteAsync(Guid id);
    Task<EventDto?> GetByIdAsync(Guid id);
}