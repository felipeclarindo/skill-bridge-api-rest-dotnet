namespace WebApi.Models;

public class Course
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Url { get; set; }
}
