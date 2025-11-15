using WebMyAnimeList.Models;

namespace WebMyAnimeList.Data.Entities;

public class User
{
    public int UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<AnimeSeries> AnimeSeries { get; set; }

}
