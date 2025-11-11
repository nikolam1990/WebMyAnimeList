using WebMyAnimeList.Models;

namespace WebMyAnimeList.Data.Entities;

public class Anime
{
    public int AnimeId { get; set; }
    public string Name { get; set; }
    public int CuontSezon { get; set; }
    public int CuontSerios { get; set; }
    public Genre[] GenreAnime { get; set; }
    public List<AnimeSeries> AnimeSeries {get;set;}
    public List<AnimationStudio> Studio { get; set; }
}

