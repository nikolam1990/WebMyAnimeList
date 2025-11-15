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
        var unverifiedResult = await _context.Users.Include(a => a.AnimeSeries).Select(user =>
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
            //if(result.Anime == null) 
            //{
            //    result.Anime.Add("пользователь пока еще ничего не смотрел");                  нужно ли эта проверка?
            //    throw new Exception("найти такого пользователя не полоучилось");  эксепшен наверное нельзя
            //     или создать особую серию заглшку у которой нет аниме, нет студии и её "пользователь пока еще ничего не смотрел"
            //};  
            return result;
        }
        else
        {
            throw new Exception("найти такого пользователя не полоучилось");
        }
    }

    public async Task UpdateUser(UpdateUserRequest userUpdate)
    {
        var User = await _context.Users.FirstOrDefaultAsync(us => us.UserId == userUpdate.UserId);
        if (User != null)
        {
            if (User.LastName == userUpdate.LastName && User.FirstName == userUpdate.FirstName)
            {
                throw new Exception("такой пользователь уже есть");
            }

            {
                User.FirstName = userUpdate.FirstName;
                User.LastName = userUpdate.LastName;
            }
            ;
        }
        else
        {
            throw new Exception("найти такого пользователя не полоучилось");
        }
    }

    public async Task DeleteUser(int userId)
    {
        var User = _context.Users.FirstOrDefault(us => us.UserId == userId);
        if (User != null)
        {
            _context.Remove(User);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("пользователь не найден");
        }
    }

    public async Task MarkTheEpisodeWatched(int userId, int episodeID)
    {
        var User = await _context.Users.Include(s => s.AnimeSeries)
            .FirstOrDefaultAsync(us => us.UserId == userId);
        var Episode = await _context.AnimeSeries.FirstOrDefaultAsync(ep => ep.Id == userId);
        if (Episode == null)
        {
            throw new Exception("такой серии нет");
        }

        if (User != null)
        {
            User.AnimeSeries.Add(Episode);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("найти такого пользователя не полоучилось");
        }
    }

    public async Task<List<EpisodeResponse>> UnwatchedEpisodes(int animeId, int userId)
    {
        var User = await _context.Users.Include(s => s.AnimeSeries)
            .FirstOrDefaultAsync(us => us.UserId == userId);
        if (User == null)
        {
            throw new Exception("найти такого пользователя не полоучилось");
        }
        var Anime = await _context.Animes
            .Include(x => x.AnimeSeries)
                .ThenInclude(x => x.Studio)
            .FirstOrDefaultAsync(an => an.AnimeId == animeId);
        if (Anime == null)
        {
            throw new Exception("аниме не найдена");
        }

        var FullSeries = Anime.AnimeSeries.ToList();
        var WatchedEpisodes = User.AnimeSeries.ToList();
        var Temp = FullSeries.Except(WatchedEpisodes);
        List<EpisodeResponse> SeriesToWatch = Temp.Select(x => new EpisodeResponse
        {
            Id = x.Id,
            Name = x.Name,
            Number = x.Number,
        }).ToList();
        return SeriesToWatch;
    }


}
