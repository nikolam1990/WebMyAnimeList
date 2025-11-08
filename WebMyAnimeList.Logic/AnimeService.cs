using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;
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

    public async Task<List<AnimationResponse>> GetAnimes()
    {
        return await _context.Animes.Select(animeTaitle =>
            new AnimationResponse
            {
                AnimeId = animeTaitle.AnimeId,
                Name = animeTaitle.Name,
                CuontSezon = animeTaitle.CuontSezon,
                CuontSerios = animeTaitle.CuontSerios,
                GenreAnime = animeTaitle.GenreAnime.Select(x=>x.Description()).ToArray(),
                Studios = animeTaitle.Studio.Select(x => x.Name).ToList()
            }).ToListAsync();
    }
    public async Task<AnimationResponse> GetAnime(int animeTaitleId)
    {
        var Anime = await _context.Animes.Include(x => x.Studio)
            .FirstOrDefaultAsync(an => an.AnimeId == animeTaitleId);
        if (Anime != null)
        {
            return new AnimationResponse
            {
                AnimeId = Anime.AnimeId,
                Name = Anime.Name,
                CuontSezon = Anime.CuontSezon,
                CuontSerios = Anime.CuontSerios,
                GenreAnime = Anime.GenreAnime.Select(x => x.Description()).ToArray(),
                Studios = Anime.Studio.Select(x => x.Name).ToList()
            };
        }
        else
        {
            throw new Exception("найти такое аниме не полоучилось");
        }
    }
    public async Task UpdateAnime(UpdateAnimationRequest animeUpdate)//
    {
        var intStudioAni = animeUpdate.Studios.ToList();
        var AnimeUpadate = await _context.Animes.Include(x => x.Studio)
            .FirstOrDefaultAsync(an => an.AnimeId == animeUpdate.AnimeId);
        if (AnimeUpadate != null)
        {
            AnimeUpadate.Name = animeUpdate.Name;
            AnimeUpadate.CuontSezon = animeUpdate.CuontSezon;
            AnimeUpadate.CuontSerios = animeUpdate.CuontSerios;
            AnimeUpadate.GenreAnime = animeUpdate.GenreAnime;
            AnimeUpadate.Studio = intStudioAni;
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
