using Microsoft.EntityFrameworkCore;
using WebMyAnimeList.Data;
using WebMyAnimeList.Data.Entities;
using WebMyAnimeList.Models;

namespace WebMyAnimeList.Logic;

public class UserService
{

    private readonly ApplicationContext _context;

    public UserService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<int> CreateStudio(CreateUserRequest user)
    {
        var User = await _context. .Select(x => x.Name).ToListAsync();
        if (User.Contains(user.Name))
        {
            throw new Exception("такая студия уже есть");
        }
        else
        {
            var AnimeStudio = new AnimationStudio
            {
                Name = user.Name,
                YearOfFoundation = user.YearOfFoundation
            };

            _context.Add(AnimeStudio);
            await _context.SaveChangesAsync();

            return AnimeStudio.StudioId;
        }

    }



}
