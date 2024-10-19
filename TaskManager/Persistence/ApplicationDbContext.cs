using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManager.Entities;

namespace TaskManager.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    private readonly DbContextOptions<ApplicationDbContext> _options = options;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    public DbSet<Entities.Task> Tasks { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
}
