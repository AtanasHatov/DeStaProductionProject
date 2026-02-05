using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DeStaProduction.Infrastucture.Entities
{
    public class DeStaUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [MaxLength(1000)]
        public string? Biography { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }

        public ICollection<Performance>? Performances { get; set; } = new List<Performance>();

        public ICollection<Event>? Events { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }
}
