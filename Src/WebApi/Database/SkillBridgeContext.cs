using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Database;

public class SkillBridgeContext : DbContext
{
    public SkillBridgeContext(DbContextOptions<SkillBridgeContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Work> Works { get; set; }
    public DbSet<LearningPath> LearningPaths { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Join table config
        modelBuilder.Entity<UserSkill>()
            .HasOne(us => us.Usuario)
            .WithMany(u => u.UserSkills)
            .HasForeignKey(us => us.UsuarioId);

        modelBuilder.Entity<UserSkill>()
            .HasOne(us => us.Skill)
            .WithMany(s => s.UserSkills)
            .HasForeignKey(us => us.SkillId);

        base.OnModelCreating(modelBuilder);
    }
}
