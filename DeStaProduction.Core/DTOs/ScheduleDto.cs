using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.DTOs
{
    public class ScheduleDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? PerformanceId { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public string Type { get; set; } = null!;

        public string? Notes { get; set; }

        public bool IsPublic { get; set; }

        public string? UserName { get; set; }
        public string? PerformanceTitle { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LocationName { get; set; }
    }
}
