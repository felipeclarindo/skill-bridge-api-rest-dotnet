using WebApi.Models;

namespace WebApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario> CreateAsync(Usuario user);
    Task UpdateAsync(Usuario user);
    Task DeleteAsync(int id);
}
