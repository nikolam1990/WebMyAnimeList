namespace WebMyAnimeList.Models;

public class AnimeWithEpisodes
{
    public int AnimeId { get; set; }
    public string Anime { get; set; }
    public int StudioId { get; set; }
    public string Studio { get; set; }
    public List<EpisodeResponse> Episodes { get; set; }
}