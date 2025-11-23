namespace WebApi.Models
{
    public class ApiDescription
    {
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Desenvolvedor { get; set; } = string.Empty;
        public string Github { get; set; } = string.Empty;
        public string Environment { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
