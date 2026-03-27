using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.Contracts
{
    public interface IEventTypeService
    {
        Task<IEnumerable<EventTypeDto>> GetAllAsync();
        Task AddAsync(string name);
        Task DeleteAsync(Guid id);
    }
}
