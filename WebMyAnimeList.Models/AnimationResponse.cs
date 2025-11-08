namespace WebMyAnimeList.Models
{
    public class AnimationResponse
    {
        public int AnimeId { get; set; }
        public string Name { get; set; }
        public int CuontSezon { get; set; }
        public int CuontSerios { get; set; }
        public string[] GenreAnime { get; set; }
        public List<string> Studios { get; set; }
    }
}
