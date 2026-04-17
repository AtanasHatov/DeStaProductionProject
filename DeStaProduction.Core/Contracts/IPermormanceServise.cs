using DeStaProduction.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.Contracts
{
    public interface IPerformanceService
    {
        Task<IEnumerable<PerformanceDto>> GetAllAsync();
        Task AddAsync(PerformanceDto model);
        Task DeleteAsync(Guid id);
        Task<List<PerformanceShortDto>> GetFilteredAsync(
    DateTime? date,
    Guid? locationId,
    Guid? eventTypeId);
    }
}
