using DeStaProduction.Core.DTOs;

namespace DeStaProduction.ViewModels
{
    public class PerformanceFilterViewModel
    {
        public DateTime? Date { get; set; }

        public Guid? LocationId { get; set; }

        public Guid? EventTypeId { get; set; }

        public List<PerformanceShortDto> Performances { get; set; } = new();

        public List<LocationDto> Locations { get; set; } = new();

        public List<EventTypeDto> EventTypes { get; set; } = new();
        public string? SearchTerm { get; set; }
    }
}
