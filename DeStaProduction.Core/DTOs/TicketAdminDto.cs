using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.DTOs
{
    public class TicketAdminDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Count { get; set; }
        public string PerformanceTitle { get; set; }
        public TicketStatus Status { get; set; }
    }
}
