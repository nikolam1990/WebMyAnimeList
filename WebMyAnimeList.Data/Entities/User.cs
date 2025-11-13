using WebMyAnimeList.Models;

namespace WebMyAnimeList.Data.Entities;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<AnimeSeries> AnimeSeries { get; set; }

}
