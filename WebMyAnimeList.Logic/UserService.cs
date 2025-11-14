using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    }
    public async Task<List<UserResponse>> GetUsers()
    {
        List<UserResponse> result = new List<UserResponse>();
        //return await _context.Users.Include(s => s.AnimeSeries).Select(user =>    возможно нужно както так
        List<UserResponse> unverifiedResult = await _context.Users.Select(user =>  
        new UserResponse
        {
         UserId = user.UserId,
         FirstName = user.FirstName,
         LastName = user.LastName,
         Anime = user.AnimeSeries.Select(s => s.Anime.Name).ToList()
        }).ToListAsync();
        foreach (var i in unverifiedResult)
        {
            UserResponse temp = new UserResponse
            {
                UserId = i.UserId,
                FirstName = i.FirstName,
                LastName = i.LastName,
                Anime = i.Anime
            };
            if (temp.Anime == null) 
            {
                temp.Anime.Add("пока еще ничего не смотрел");
            }
            result.Add(temp);
        }
            return result;
    }
    public async Task<UserResponse> GetUser(int userId)
    {
        var User = await _context.Users.Include(s => s.AnimeSeries)
            .FirstOrDefaultAsync(us => us.UserId == userId);
        if (User != null)
        {
            UserResponse result = new UserResponse
            {
                UserId = User.UserId,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Anime = User.AnimeSeries.Select(s => s.Anime.Name).ToList()
            };
            if(result.Anime == null) 
            {
                result.Anime.Add("пока еще ничего не смотрел");
            };
            return result;
        }
        else
        {
            throw new Exception("найти такое аниме не полоучилось");
        }
    }
}
