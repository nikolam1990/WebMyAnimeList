using Microsoft.EntityFrameworkCore;
using WebMyAnimeList.Data.Entities;

namespace WebMyAnimeList.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    public DbSet<Anime> Animes => Set<Anime>();
    public DbSet<AnimationStudio> Studios => Set<AnimationStudio>();
    public DbSet<AnimeSeries> AnimeSeries => Set<AnimeSeries>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        base.OnModelCreating(modelBuilder);

    }
}
