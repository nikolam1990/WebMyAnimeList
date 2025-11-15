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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AnimationStudio>()
            .HasMany(sa => sa.Animes)
            .WithMany(a => a.Studio);
        modelBuilder.Entity<AnimationStudio>().Property(b => b.Name).HasMaxLength(15).IsRequired();
        modelBuilder.Entity<AnimationStudio>().HasKey(b => b.StudioId);

        modelBuilder.Entity<Anime>().Property(b => b.Name).HasMaxLength(210).IsRequired();
        modelBuilder.Entity<Anime>().HasKey(b => b.AnimeId);

        modelBuilder.Entity<AnimeSeries>()
            .HasOne(s=>s.Anime)
            .WithMany(a=>a.AnimeSeries)
            .HasForeignKey(s => s.AnimeId);
        modelBuilder.Entity<AnimeSeries>()
            .HasOne(s => s.Studio)
            .WithMany(a => a.AnimeSeries)
            .HasForeignKey(s => s.StudioId);

        modelBuilder.Entity<User>().Property(b => b.FirstName).HasMaxLength(15);
        modelBuilder.Entity<User>().Property(b => b.LastName).HasMaxLength(25);
        modelBuilder.Entity<User>().HasKey(b => b.UserId);

        modelBuilder.Entity<AnimationStudio>()
            .HasMany(sa => sa.Animes)
            .WithMany(a => a.Studio);
        modelBuilder.Entity<User>()
            .HasMany(u => u.AnimeSeries);
            


    }
}
