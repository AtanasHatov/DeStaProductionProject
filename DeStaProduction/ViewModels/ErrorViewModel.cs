namespace DeStaProduction.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }
    }
}
