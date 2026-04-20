using DeStaProduction.Core.Contracts;
using DeStaProduction.Core.DTOs;
using DeStaProduction.Infrastucture.Entities;

public class TicketService : ITicketService
{
    private readonly ApplicationDbContext context;

    public TicketService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(Guid performanceId, Guid userId, int count)
    {
        var ticket = new TicketRequest
        {
            PerformanceId = performanceId,
            UserId = userId,
            Count = count
        };

        await context.TicketRequests.AddAsync(ticket);
        await context.SaveChangesAsync();
    }

    public List<TicketAdminDto> GetAll()
    {
        return context.TicketRequests
            .Select(t => new TicketAdminDto
            {
                Id = t.Id,
                Count = t.Count,
                Status = t.Status,
                FullName = t.User.FirstName + " " + t.User.LastName,
                PerformanceTitle = t.Performance.Title
            })
            .ToList();
    }

    public async Task Approve(Guid id)
    {
        var t = context.TicketRequests.Find(id);
        t.Status = TicketStatus.Approved;
        await context.SaveChangesAsync();
    }

    public async Task Reject(Guid id)
    {
        var t = context.TicketRequests.Find(id);
        t.Status = TicketStatus.Rejected;
        await context.SaveChangesAsync();
    }
}