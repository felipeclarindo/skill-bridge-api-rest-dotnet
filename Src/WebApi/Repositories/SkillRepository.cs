using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly SkillBridgeContext _db;

    public SkillRepository(SkillBridgeContext db) => _db = db;

    public async Task<IEnumerable<Skill>> GetAllAsync() => await _db.Skills.ToListAsync();

    public async Task<Skill?> GetByIdAsync(int id) => await _db.Skills.FindAsync(id);

    public async Task<Skill> CreateAsync(Skill skill)
    {
        _db.Skills.Add(skill);
        await _db.SaveChangesAsync();
        return skill;
    }

    public async Task UpdateAsync(Skill skill)
    {
        _db.Skills.Update(skill);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Skills.FindAsync(id);
        if (entity == null)
            return;
        _db.Skills.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
