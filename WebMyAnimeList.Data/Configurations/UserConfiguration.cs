using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebMyAnimeList.Data.Entities;

namespace WebMyAnimeList.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.FirstName).HasMaxLength(15);
            builder.Property(b => b.LastName).HasMaxLength(25);
            builder.HasKey(b => b.UserId);
            builder.HasMany(a => a.AnimeSeries)
                   .WithMany(u => u.Users);
        }

    }
}
