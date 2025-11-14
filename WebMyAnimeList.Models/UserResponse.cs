namespace WebMyAnimeList.Models;

public class UserResponse
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Anime { get; set; }
}
