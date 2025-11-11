namespace WebMyAnimeList.Data.Entities;

public class AnimationStudio
{
    public int StudioId { get; set; }
    public string Name { get; set; }
    public int YearOfFoundation { get; set; }
    public List<Anime> Animes { get; set; }
    public List<AnimeSeries> AnimeSeries { get; set; }
}
