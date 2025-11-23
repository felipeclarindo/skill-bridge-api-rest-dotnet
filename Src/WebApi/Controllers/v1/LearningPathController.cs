using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/learning-paths")]
public class LearningPathController : ControllerBase
{
    private readonly ILearningPathRepository _repo;

    public LearningPathController(ILearningPathRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(
            (await _repo.GetAllAsync()).Select(lp => new
            {
                lp.Id,
                lp.Nome,
                _links = new { self = $"/api/v1/learning-paths/{lp.Id}" },
            })
        );

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var it = await _repo.GetByIdAsync(id);
        if (it == null)
            return NotFound();
        return Ok(
            new
            {
                it.Id,
                it.Nome,
                _links = new { self = $"/api/v1/learning-paths/{it.Id}" },
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(LearningPath m)
    {
        var c = await _repo.CreateAsync(m);
        return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, LearningPath m)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null)
            return NotFound();
        e.Nome = m.Nome;
        await _repo.UpdateAsync(e);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return NoContent();
    }
}
