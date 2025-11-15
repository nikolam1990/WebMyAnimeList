namespace WebMyAnimeList.Models;

public class UserResponse
{
    public int UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required List<string> Anime { get; set; }
}
