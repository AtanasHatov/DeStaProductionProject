using DeStaProduction.Infrastucture.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeStaProduction.ViewModels
{
    public class ScheduleViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [MaxLength(500)]
        public string Notes { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public DeStaUser User { get; set; } = null!;

        [ForeignKey(nameof(Performance))]
        public Guid? PerformanceId { get; set; }

        public Performance? Performance { get; set; }
    }
}
