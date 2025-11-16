using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMyAnimeList.Data.Entities;

namespace WebMyAnimeList.Data.Configurations;

public class AnimeSeriesCongiguration : IEntityTypeConfiguration<AnimeSeries>
{
    public void Configure(EntityTypeBuilder<AnimeSeries> builder)
    {
        builder.HasOne(s => s.Anime)
               .WithMany(a => a.AnimeSeries)
               .HasForeignKey(s => s.AnimeId);
        builder.HasOne(s => s.Studio)
               .WithMany(a => a.AnimeSeries)
               .HasForeignKey(s => s.StudioId);
    }
}
