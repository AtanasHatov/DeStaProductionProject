using DeStaProduction.Infrastucture.Entities;
using System.ComponentModel.DataAnnotations;

namespace DeStaProduction.ViewModels
{
    public class LocationViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string City { get; set; } = null!;
        [Range(1, 10000)]
        public int Capacity { get; set; }

        public ICollection<Performance> Events { get; set; } = new List<Performance>();
    }
}
