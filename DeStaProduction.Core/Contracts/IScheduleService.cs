using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.Contracts
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetAllAsync(int month, int year, string role, Guid userId);

        Task AddTaskAsync(Schedule model);

        Task<bool> IsUserAvailable(Guid userId, DateTime date);

        Task SetAvailability(Guid userId, DateTime date, bool isAvailable);
    }
}
