using DeStaProduction.Infrastucture.Entities;
using System.ComponentModel.DataAnnotations;

namespace DeStaProduction.ViewModels
{
    public class EventTypeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
