using DeStaProduction.Infrastucture.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeStaProduction.ViewModels
{
    public class PerformanceViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [ForeignKey(nameof(Location))]
        public Guid LocationId { get; set; }

        public Location Location { get; set; } = null!;

        public DateTime Date { get; set; }

        [ForeignKey(nameof(Event))]
        public Guid EventId { get; set; }

        public Event? Event { get; set; }

        public ICollection<DeStaUser> Participants { get; set; } = new List<DeStaUser>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    }
}
