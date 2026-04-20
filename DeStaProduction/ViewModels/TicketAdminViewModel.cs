namespace DeStaProduction.ViewModels
{
    public class TicketAdminViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int Count { get; set; }
        public string PerformanceTitle { get; set; }
        public TicketStatus Status { get; set; }
    }
}
