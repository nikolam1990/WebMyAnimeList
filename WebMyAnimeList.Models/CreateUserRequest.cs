namespace WebMyAnimeList.Models
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // нужно ли при создание пользователя срузу прописывать серии которые он посмотрел
        //public List<int> AnimeSeriesId { get; set; } 
    }
}
