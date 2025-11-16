using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMyAnimeList.Data.Entities;

namespace WebMyAnimeList.Data.Configurations;

public class AnimeConfiguration : IEntityTypeConfiguration<Anime>
{
    public void Configure(EntityTypeBuilder<Anime> builder)
    {
        builder.Property(b => b.Name).HasMaxLength(210).IsRequired();
        builder.HasKey(b => b.AnimeId);
    }

}
