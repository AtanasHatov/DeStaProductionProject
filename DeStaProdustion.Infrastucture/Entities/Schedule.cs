using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Infrastucture.Entities
{
    public class Schedule
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public DeStaUser User { get; set; } = null!;

        [Required]
        public Guid PerformanceId { get; set; }
        public Performance Performance { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; } = true;

        [MaxLength(500)]
        public string? Notes { get; set; }

        public string Type { get; set; } = null!;

        public bool IsPublic { get; set; }
    }
}
