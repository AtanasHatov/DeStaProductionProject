using DeStaProduction.Infrastucture.Entities;

public enum TicketStatus
{
    Pending,
    Approved,
    Rejected
}

public class TicketRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Count { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.Pending;

    public Guid UserId { get; set; }
    public DeStaUser User { get; set; }

    public Guid PerformanceId { get; set; }
    public Performance Performance { get; set; }
}