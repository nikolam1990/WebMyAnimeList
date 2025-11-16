using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMyAnimeList.Data.Entities;


namespace WebMyAnimeList.Data.Configurations;

public class AnimeStudioConfiguration : IEntityTypeConfiguration<AnimationStudio>
{
    public void Configure(EntityTypeBuilder<AnimationStudio> builder)
    {
        builder
            .HasMany(sa => sa.Animes)
            .WithMany(a => a.Studio);
        builder.Property(b => b.Name).HasMaxLength(15).IsRequired();
        builder.HasKey(b => b.StudioId);
    }
}