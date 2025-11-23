using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SkillBridgeContext _db;

    public UserRepository(SkillBridgeContext db) => _db = db;

    public async Task<IEnumerable<Usuario>> GetAllAsync() =>
        await _db.Usuarios.Include(u => u.UserSkills).ToListAsync();

    public async Task<Usuario?> GetByIdAsync(int id) =>
        await _db.Usuarios.Include(u => u.UserSkills).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<Usuario> CreateAsync(Usuario user)
    {
        _db.Usuarios.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(Usuario user)
    {
        _db.Usuarios.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Usuarios.FindAsync(id);
        if (entity == null)
            return;
        _db.Usuarios.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
