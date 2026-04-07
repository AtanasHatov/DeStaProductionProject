using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Core.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public int Capacity { get; set; }
        public string Address { get; set; } = null!;
    }
}
