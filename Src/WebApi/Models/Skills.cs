namespace WebApi.Models;

public class Skill
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }

    public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}
