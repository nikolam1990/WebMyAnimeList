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
    public async Task<int> CreateUser(CreateUserRequest user)
    {
        var Users = await _context.Users.Select(fn => fn.LastName).ToListAsync();
        if (Users.Contains(user.LastName) && Users.Contains(user.FirstName))
        {
            throw new Exception("такой пользователь уже есть");
        }
        else
        {
            var User = new User
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
            };
            _context.Add(User);
            await _context.SaveChangesAsync();
            return User.UserId;
        }
        public async Task<List<AnimationStudioResponse>> GetStudios()
        {
        return await _context.Studios.Select(studio =>
            new AnimationStudioResponse
            {
                Id = studio.StudioId,
                Name = studio.Name,
                YearOfFoundation = studio.YearOfFoundation
            }).ToListAsync();

        }




}



}
