using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Infrastucture.Entities
{
    public class Location
    {
        [Key]
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
