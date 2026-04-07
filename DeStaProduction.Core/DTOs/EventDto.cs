
using DeStaProduction.Core.DTOs;

public class EventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Duration { get; set; }
    public string TypeName { get; set; } = null!;
    public List<PerformanceShortDto> Performances { get; set; } = new();
}