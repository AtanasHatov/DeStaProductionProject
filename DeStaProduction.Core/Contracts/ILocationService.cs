using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;

namespace DeStaProduction.Core.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllAsync();
        Task AddAsync(LocationDto model);
        Task DeleteAsync(Guid id);
        Task<LocationDto?> GetByIdAsync(Guid id);
        Task UpdateAsync(LocationDto dto);
    }
}
