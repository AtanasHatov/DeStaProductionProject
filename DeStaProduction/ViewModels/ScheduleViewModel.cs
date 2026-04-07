using System.ComponentModel.DataAnnotations;

namespace DeStaProduction.ViewModels
{
    public class ScheduleViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public string Notes { get; set; } = "";

        public Guid UserId { get; set; }

        public string UserName { get; set; } = ""; 

        public Guid? PerformanceId { get; set; }

        public string PerformanceTitle { get; set; } = ""; 

        public string Type { get; set; } = "";
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string LocationName { get; set; } = "";
    }
}