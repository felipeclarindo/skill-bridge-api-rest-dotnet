using System.Collections.Generic;

namespace WebApi.Models.Dto
{
    public class UsuarioCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<UserSkillDto> UserSkills { get; set; } = new List<UserSkillDto>();
    }
}
