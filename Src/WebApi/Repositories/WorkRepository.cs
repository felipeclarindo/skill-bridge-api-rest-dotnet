using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories;

public class WorkRepository : IWorkRepository
{
    private readonly SkillBridgeContext _db;
    public WorkRepository(SkillBridgeContext db) => _db = db;

    public async Task<IEnumerable<Work>> GetAllAsync() => await _db.Works.ToListAsync();
    public async Task<Work?> GetByIdAsync(int id) => await _db.Works.FindAsync(id);
    public async Task<Work> CreateAsync(Work work) { _db.Works.Add(work); await _db.SaveChangesAsync(); return work; }
    public async Task UpdateAsync(Work work) { _db.Works.Update(work); await _db.SaveChangesAsync(); }
    public async Task DeleteAsync(int id) { var e = await _db.Works.FindAsync(id); if (e == null) return; _db.Works.Remove(e); await _db.SaveChangesAsync(); }
}
