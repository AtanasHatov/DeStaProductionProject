using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.DTOs
{
    public class PerformanceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Event { get; set; } = null!;
        public string Location { get; set; } = null!;
        public Guid EventId { get; set; }
        public Guid LocationId { get; set; }
        public DateTime Date { get; set; }
    }
}
