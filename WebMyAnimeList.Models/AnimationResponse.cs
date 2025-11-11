namespace WebMyAnimeList.Models
{
    public class AnimeResponse
    {
        public int AnimeId { get; set; }
        public string Name { get; set; }
        public int CountSeason { get; set; }
        public int CountSeries { get; set; }
        public string[] GenreAnime { get; set; }
        public List<string> Studios { get; set; }
    }
}
