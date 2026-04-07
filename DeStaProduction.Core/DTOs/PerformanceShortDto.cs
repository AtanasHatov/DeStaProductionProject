using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.DTOs
{
    public class PerformanceShortDto
    {
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
    }
}
