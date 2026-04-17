using Microsoft.EntityFrameworkCore;
using DeStaProduction.Core.Contracts;
using DeStaProduction.Infrastucture.Entities;
using DeStaProduction.Core.DTOs;

namespace DeStaProduction.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext context;

        public LocationService(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<LocationDto>> GetAllAsync()
            => await context.Locations
                .Select(x => new LocationDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    City = x.City,
                    Capacity = x.Capacity,
                    Address = x.Address
                }).ToListAsync();

        public async Task AddAsync(LocationDto model)
        {
            await context.Locations.AddAsync(new Location
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                City = model.City,
                Address = model.Address,
                Capacity = model.Capacity
            });
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Locations.FindAsync(id);
            if (entity != null)
            {
                context.Locations.Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<LocationDto?> GetByIdAsync(Guid id)
        {
            return await context.Locations
                .Where(x => x.Id == id)
                .Select(x => new LocationDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(LocationDto dto)
        {
            var location = await context.Locations.FindAsync(dto.Id);

            if (location == null)
                return;

            location.Name = dto.Name;
            location.Address = dto.Address;

            await context.SaveChangesAsync();
        }
    }
}
