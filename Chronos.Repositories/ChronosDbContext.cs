using Chronos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chronos.Repositories;

public sealed class ChronosDbContext : DbContext
{
    public ChronosDbContext() => Database.EnsureCreated();
    public ChronosDbContext(DbContextOptions<ChronosDbContext> options) : base(options) {}
    
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<FamilyEntity> Families { get; set; }
    public DbSet<SpentTrafficEntity> SpentTraffic { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Chronos.db");
    }
}