using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMyAnimeList.Data;
using WebMyAnimeList.Data.Entities;
using WebMyAnimeList.Models;
using System.Text;
using System.Threading.Tasks;


namespace WebMyAnimeList.Data.Configurations;

public class IEntityTypeConfigurationAnimeStudio : IEntityTypeConfiguration<AnimationStudio>
{
    ////base : IEntityTypeConfiguration<AnimationStudio>;
    //public IEntityTypeConfigurationAnimeStudio() { }

    public void Configure(EntityTypeBuilder<AnimationStudio> builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AnimationStudio>()
            .HasMany(sa => sa.Animes)
            .WithMany(a => a.Studio);
        builder.Entity<AnimationStudio>().Property(b => b.Name).HasMaxLength(15).IsRequired();
        builder.Entity<AnimationStudio>().HasKey(b => b.StudioId);
    }



    //public class ApplicationContext : DbContext 
    //{
    //    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    //    public DbSet<AnimationStudio> Studios => Set<AnimationStudio>();
    //    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    //    {
    //        base.OnModelCreating(modelBuilder);
    //        modelBuilder.Entity<AnimationStudio>()
    //            .HasMany(sa => sa.Animes)
    //            .WithMany(a => a.Studio);
    //        modelBuilder.Entity<AnimationStudio>().Property(b => b.Name).HasMaxLength(15).IsRequired();
    //        modelBuilder.Entity<AnimationStudio>().HasKey(b => b.StudioId);
    //    }
    //}
}