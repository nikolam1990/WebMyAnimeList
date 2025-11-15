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
        var result = await _context.Users.Include(a => a.AnimeSeries)
            .Select(user =>
            new UserResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Anime = user.AnimeSeries
                    .Select(s => s.Anime.Name)
                    .ToList()
            })
            .ToListAsync();

        result.ForEach(x =>
            x.Anime = x.Anime.Count == 0 
                ? x.Anime = ["пока еще ничего не смотрел"]
                : x.Anime);

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

            return result;
        }
        else
        {
            throw new Exception("найти такого пользователя не полоучилось");
        }
    }

    public async Task UpdateUser(UpdateUserRequest userUpdate)
    {
        var user = await _context.Users.FirstOrDefaultAsync(us => us.UserId == userUpdate.UserId);
        if (user != null)
        {
            var userFirstLast = await _context.Users
                .FirstOrDefaultAsync(us => us.FirstName == userUpdate.FirstName && us.LastName == userUpdate.LastName)
                ?? throw new Exception("такой пользователь уже есть");
            
            {
                user.FirstName = userUpdate.FirstName;
                user.LastName = userUpdate.LastName;
            };

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

    public async Task MarkTheEpisodeWatched(int userId, int episodeId)
    {
        var user = await _context.Users.Include(s => s.AnimeSeries)
            .FirstOrDefaultAsync(us => us.UserId == userId);
        var episode = await _context.AnimeSeries.FirstOrDefaultAsync(ep => ep.Id == userId) 
            ?? throw new Exception("такой серии нет");
        if (user != null)
        {
            user.AnimeSeries.Add(episode);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("найти такого пользователя не полоучилось");
        }
    }

    public async Task<List<EpisodeResponse>> UnwatchedEpisodes(int animeId, int userId)
    {
        var user = await _context.Users.Include(s => s.AnimeSeries)
            .FirstOrDefaultAsync(us => us.UserId == userId) 
            ?? throw new Exception("найти такого пользователя не полоучилось");
        var anime = await _context.Animes
            .Include(x => x.AnimeSeries)
                .FirstOrDefaultAsync(an => an.AnimeId == animeId)
                ?? throw new Exception("аниме не найдена");
        var fullSeries = anime.AnimeSeries.ToList();
        var watchedEpisodes = user.AnimeSeries.ToList();
        var seriesToWatch = fullSeries
            .Except(watchedEpisodes)
            .Select(x => new EpisodeResponse
            {
                Id = x.Id,
                Name = x.Name,
                Number = x.Number,
            })
            .ToList();
        return seriesToWatch;
    }
}
