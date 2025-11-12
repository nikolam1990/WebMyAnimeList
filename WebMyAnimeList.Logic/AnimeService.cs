using Microsoft.EntityFrameworkCore;
using WebMyAnimeList.Data;
using WebMyAnimeList.Data.Entities;
using WebMyAnimeList.Models;

namespace WebMyAnimeList.Logic;

public class AnimeService
{
    private readonly ApplicationContext _context;

    public AnimeService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<int> CreateAnime(CreateAnimationRequest anime)
    {
        var animeTaitleName = await _context.Animes.Select(x => x.Name).ToListAsync();
        if (animeTaitleName.Contains(anime.Name))
        {
            throw new Exception("такое аниме уже есть");
        }
        else
        {
            var animationStudios = await _context.Studios.Where(x => anime.Studios.Contains(x.StudioId)).ToListAsync();
            var AnimeTaitle = new Anime
            {
                Name = anime.Name,
                CuontSezon = anime.CountSeason,
                CuontSerios = anime.CountSeries,
                GenreAnime = anime.GenreAnime,
                Studio = animationStudios
            };

            _context.Add(AnimeTaitle);
            await _context.SaveChangesAsync();

            return AnimeTaitle.AnimeId;
        }
    }

    public async Task<List<AnimeResponse>> GetAnimes()
    {
        return await _context.Animes.Select(animeTaitle =>
            new AnimeResponse
            {
                AnimeId = animeTaitle.AnimeId,
                Name = animeTaitle.Name,
                CountSeason = animeTaitle.CuontSezon,
                CountSeries = animeTaitle.CuontSerios,
                GenreAnime = animeTaitle.GenreAnime.Select(x => x.Description()).ToArray(),
                Studios = animeTaitle.Studio.Select(x => x.Name).ToList()
            }).ToListAsync();
    }

    public async Task<AnimeWithEpisodes> GetSeriesInSeason(int animeId, int season)
    {
        var anime = await _context.Animes
            .Include(x => x.AnimeSeries)
                .ThenInclude(x => x.Studio)
            .FirstOrDefaultAsync(an => an.AnimeId == animeId);
        if (anime != null)
        {
            var result = new AnimeWithEpisodes()
            {
                AnimeId = animeId,
                Anime = anime.Name,
                Studio = anime.AnimeSeries.Where(ep => ep.Season == season).First().Studio.Name,
                StudioId = anime.AnimeSeries.Where(ep => ep.Season == season).First().StudioId,
                Episodes = anime.AnimeSeries.Where(ep => ep.Season == season).Select(x => new EpisodeResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Number = x.Number,
                }).ToList()
            };
            return result;
        }
        else
        {
            throw new Exception("аниме не найдена");
        }
    }

    public async Task<AnimeResponse> GetAnime(int animeTaitleId)
    {
        var Anime = await _context.Animes.Include(x => x.Studio)
            .FirstOrDefaultAsync(an => an.AnimeId == animeTaitleId);
        if (Anime != null)
        {
            return new AnimeResponse
            {
                AnimeId = Anime.AnimeId,
                Name = Anime.Name,
                CountSeason = Anime.CuontSezon,
                CountSeries = Anime.CuontSerios,
                GenreAnime = Anime.GenreAnime.Select(x => x.Description()).ToArray(),
                Studios = Anime.Studio.Select(x => x.Name).ToList()
            };
        }
        else
        {
            throw new Exception("найти такое аниме не полоучилось");
        }
    }

    public async Task UpdateAnime(UpdateAnimationRequest animeUpdate)
    {
        var animationStudios = await _context.Studios.Where(x => animeUpdate.Studios.Contains(x.StudioId)).ToListAsync();
        var intStudioAni = animeUpdate.Studios.ToList();
        var AnimeUpdate = await _context.Animes.Include(x => x.Studio)
            .FirstOrDefaultAsync(an => an.AnimeId == animeUpdate.AnimeId);
        if (AnimeUpdate != null)
        {
            AnimeUpdate.Name = animeUpdate.Name;
            AnimeUpdate.CuontSezon = animeUpdate.CuontSezon;
            AnimeUpdate.CuontSerios = animeUpdate.CuontSerios;
            AnimeUpdate.GenreAnime = animeUpdate.GenreAnime;
            AnimeUpdate.Studio = animationStudios;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("аниме не найдена");
        }
    }

    public async Task DeleteAnime(int animestudioId)
    {
        var Animefail = _context.Animes.FirstOrDefault(st => st.AnimeId == animestudioId);
        if (Animefail != null)
        {
            _context.Remove(Animefail);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("аниме не найдена");
        }
    }
}
