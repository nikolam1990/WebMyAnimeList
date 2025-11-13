using WebMyAnimeList.Data;

namespace WebMyAnimeList.Logic;

public class UserService
{

    private readonly ApplicationContext _context;

    public UserService(ApplicationContext context)
    {
        _context = context;
    }
}
