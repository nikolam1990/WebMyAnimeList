namespace WebMyAnimeList.Models;

public class CreateAnimationRequest
{
    public string Name { get; set; }
    public int CountSeason { get; set; }
    public int CountSeries { get; set; }
    public Genre[] GenreAnime { get; set; }
    public List<int> Studios { get; set; }
}
