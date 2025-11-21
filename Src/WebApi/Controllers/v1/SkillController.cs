using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/skills")]
public class SkillController : ControllerBase
{
    private readonly ISkillRepository _repo;
    public SkillController(ISkillRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok((await _repo.GetAllAsync()).Select(s => new { s.Id, s.Nome, _links = new { self = $"/api/v1/skills/{s.Id}" } }));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(new { item.Id, item.Nome, item.Descricao, _links = new { self = $"/api/v1/skills/{item.Id}" } });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Skill model) { var created = await _repo.CreateAsync(model); return CreatedAtAction(nameof(GetById), new { id = created.Id }, created); }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Skill model) { var e = await _repo.GetByIdAsync(id); if (e == null) return NotFound(); e.Nome = model.Nome; e.Descricao = model.Descricao; await _repo.UpdateAsync(e); return NoContent(); }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
