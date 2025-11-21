using WebApi.Models;

namespace WebApi.Repositories.Interfaces;

public interface ILearningPathRepository
{
    Task<IEnumerable<LearningPath>> GetAllAsync();
    Task<LearningPath?> GetByIdAsync(int id);
    Task<LearningPath> CreateAsync(LearningPath lp);
    Task UpdateAsync(LearningPath lp);
    Task DeleteAsync(int id);
}
