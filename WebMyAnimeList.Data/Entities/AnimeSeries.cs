namespace WebMyAnimeList.Data.Entities
{
    public class AnimeSeries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public int AnimeId { get; set; }
        public int StudioId { get; set; }
        public AnimationStudio Studio { get; set; }
        public Anime Anime { get; set; }
    }
}
