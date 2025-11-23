using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories;

public class LearningPathRepository : ILearningPathRepository
{
    private readonly SkillBridgeContext _db;

    public LearningPathRepository(SkillBridgeContext db) => _db = db;

    public async Task<IEnumerable<LearningPath>> GetAllAsync() =>
        await _db.LearningPaths.ToListAsync();

    public async Task<LearningPath?> GetByIdAsync(int id) => await _db.LearningPaths.FindAsync(id);

    public async Task<LearningPath> CreateAsync(LearningPath lp)
    {
        _db.LearningPaths.Add(lp);
        await _db.SaveChangesAsync();
        return lp;
    }

    public async Task UpdateAsync(LearningPath lp)
    {
        _db.LearningPaths.Update(lp);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var e = await _db.LearningPaths.FindAsync(id);
        if (e == null)
            return;
        _db.LearningPaths.Remove(e);
        await _db.SaveChangesAsync();
    }
}
