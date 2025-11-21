namespace WebApi.Models;

public class Recommendation
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Texto { get; set; } = string.Empty;
}
