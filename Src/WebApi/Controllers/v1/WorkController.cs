using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/works")]
public class WorkController : ControllerBase
{
    private readonly IWorkRepository _repo;
    public WorkController(IWorkRepository repo) => _repo = repo;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok((await _repo.GetAllAsync()).Select(w => new { w.Id, w.Titulo, _links = new { self = $"/api/v1/works/{w.Id}" } }));
    [HttpGet("{id:int}")] public async Task<IActionResult> GetById(int id) { var it = await _repo.GetByIdAsync(id); if (it==null) return NotFound(); return Ok(new { it.Id, it.Titulo, it.Descricao, _links = new { self = $"/api/v1/works/{it.Id}" } }); }
    [HttpPost] public async Task<IActionResult> Create(Work m) { var c = await _repo.CreateAsync(m); return CreatedAtAction(nameof(GetById), new { id = c.Id }, c); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id, Work m) { var e = await _repo.GetByIdAsync(id); if (e==null) return NotFound(); e.Titulo = m.Titulo; e.Descricao = m.Descricao; await _repo.UpdateAsync(e); return NoContent(); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return NoContent(); }
}
