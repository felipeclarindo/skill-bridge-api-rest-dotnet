using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _repo;

    public CourseController(ICourseRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(
            (await _repo.GetAllAsync()).Select(c => new
            {
                c.Id,
                c.Nome,
                _links = new { self = $"/api/v1/courses/{c.Id}" },
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
                it.Url,
                _links = new { self = $"/api/v1/courses/{it.Id}" },
            }
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course m)
    {
        var c = await _repo.CreateAsync(m);
        return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Course m)
    {
        var e = await _repo.GetByIdAsync(id);
        if (e == null)
            return NotFound();
        e.Nome = m.Nome;
        e.Url = m.Url;
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
