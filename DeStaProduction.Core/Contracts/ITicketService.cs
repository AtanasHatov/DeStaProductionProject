using DeStaProduction.Core.DTOs;

namespace DeStaProduction.Core.Contracts
{
    public interface ITicketService
    {
        Task CreateAsync(Guid performanceId, Guid userId, int count);
        List<TicketAdminDto> GetAll();
        Task Approve(Guid id);
        Task Reject(Guid id);
    }
}
