using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Models;
using WebApi.Repositories.Interfaces;

namespace WebApi.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly SkillBridgeContext _db;
    public CourseRepository(SkillBridgeContext db) => _db = db;

    public async Task<IEnumerable<Course>> GetAllAsync() => await _db.Courses.ToListAsync();
    public async Task<Course?> GetByIdAsync(int id) => await _db.Courses.FindAsync(id);
    public async Task<Course> CreateAsync(Course course) { _db.Courses.Add(course); await _db.SaveChangesAsync(); return course; }
    public async Task UpdateAsync(Course course) { _db.Courses.Update(course); await _db.SaveChangesAsync(); }
    public async Task DeleteAsync(int id) { var e = await _db.Courses.FindAsync(id); if (e == null) return; _db.Courses.Remove(e); await _db.SaveChangesAsync(); }
}
