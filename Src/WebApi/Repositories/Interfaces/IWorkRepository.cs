using WebApi.Models;

namespace WebApi.Repositories.Interfaces;

public interface IWorkRepository
{
    Task<IEnumerable<Work>> GetAllAsync();
    Task<Work?> GetByIdAsync(int id);
    Task<Work> CreateAsync(Work work);
    Task UpdateAsync(Work work);
    Task DeleteAsync(int id);
}
