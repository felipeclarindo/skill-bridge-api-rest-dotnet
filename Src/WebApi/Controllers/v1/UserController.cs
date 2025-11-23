using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Dto;
using WebApi.Repositories.Interfaces;

namespace WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = (await _repo.GetAllAsync()).Select(u => new
        {
            u.Id,
            u.Nome,
            u.Email,
            _links = new { self = $"/api/v1/users/{u.Id}" },
        });
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        return Ok(
            new
            {
                item.Id,
                item.Nome,
                item.Email,
                _links = new { self = $"/api/v1/users/{item.Id}" },
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuarioCreateDto modelDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Map DTO para model EF
        var model = new Usuario
        {
            Nome = modelDto.Nome,
            Email = modelDto.Email,
            UserSkills = modelDto
                .UserSkills.Select(usDto => new UserSkill
                {
                    UsuarioId = usDto.UsuarioId,
                    SkillId = usDto.SkillId,
                })
                .ToList(),
        };

        var created = await _repo.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioCreateDto modelDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existing = await _repo.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        // Atualiza campos básicos
        existing.Nome = modelDto.Nome;
        existing.Email = modelDto.Email;

        // Atualiza UserSkills
        existing.UserSkills.Clear();
        existing.UserSkills = modelDto
            .UserSkills.Select(usDto => new UserSkill
            {
                UsuarioId = usDto.UsuarioId,
                SkillId = usDto.SkillId,
            })
            .ToList();

        await _repo.UpdateAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}
