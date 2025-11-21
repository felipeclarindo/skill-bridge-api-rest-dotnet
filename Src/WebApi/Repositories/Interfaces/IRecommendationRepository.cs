using WebApi.Models;

namespace WebApi.Repositories.Interfaces
{
    public interface IRecommendationRepository
    {
        Task<IEnumerable<Recommendation>> GetAllAsync();
        Task<Recommendation?> GetByIdAsync(int id);
        Task<Recommendation> CreateAsync(Recommendation recommendation); // ✔
        Task UpdateAsync(Recommendation recommendation);
        Task DeleteAsync(int id);
    }
}
