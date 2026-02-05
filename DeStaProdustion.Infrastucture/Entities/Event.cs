using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeStaProduction.Infrastucture.Entities
{
    public class Event
    {
        //Event = спектакъл / проект / репетиция
        //Performance = конкретна дата + място + екип

        [Key]
        public Guid Id { get; set; }

        public ICollection<Performance>? Performances { get; set; }

        [ForeignKey(nameof(Type))]
        public Guid EventType { get; set; }

        public EventType Type { get; set; } = null!;

        public List<DeStaUser>? Users { get; set; }
        [Range(1, 1440)]
        public int Duration { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

		
		
	}
}
