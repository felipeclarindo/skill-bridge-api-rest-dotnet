using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly SkillBridgeContext _context;

        public RecommendationRepository(SkillBridgeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recommendation>> GetAllAsync()
        {
            return await _context.Recommendations.ToListAsync();
        }

        public async Task<Recommendation?> GetByIdAsync(int id)
        {
            return await _context.Recommendations.FindAsync(id);
        }

        public async Task<Recommendation> CreateAsync(Recommendation recommendation)
        {
            _context.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();
            return recommendation;
        }

        public async Task UpdateAsync(Recommendation recommendation)
        {
            _context.Recommendations.Update(recommendation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var recommendation = await GetByIdAsync(id);
            if (recommendation != null)
            {
                _context.Recommendations.Remove(recommendation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
