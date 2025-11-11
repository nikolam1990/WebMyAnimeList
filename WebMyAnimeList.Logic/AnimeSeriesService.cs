using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMyAnimeList.Data;
using WebMyAnimeList.Data.Entities;
using WebMyAnimeList.Models;

namespace WebMyAnimeList.Logic;

public class AnimeSeriesService
{
    private readonly ApplicationContext _context;
    public AnimeSeriesService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAnimeSeries (CreateAnimeSeriesRequest animeSeriesTail)
    {
        var AnimeTaitle = new AnimeSeries
        {
            Name = animeSeriesTail.Name,
            Season = animeSeriesTail.Season,
            Number = animeSeriesTail.Number,
            AnimeId = animeSeriesTail.AnimeId,
            StudioId = animeSeriesTail.StudioId
        };
        _context.Add(AnimeTaitle);
        await _context.SaveChangesAsync();
        return AnimeTaitle.Id;
    }
    public async Task<AnimationSeriecResponse> GetAnimeSeriec(int animeTaitleIdSeries)
    {
        var Anime = await _context.Animes.Include(x => x.Studio)
            .FirstOrDefaultAsync(an => an.AnimeId == animeTaitleIdSeries);


        var AnimeTaitleIdSeries = await _context.AnimeSeries.
            FirstOrDefaultAsync(anse => anse.AnimeId == animeTaitleIdSeries);
            

        if (AnimeTaitleIdSeries != null)
        {
            return new AnimationSeriecResponse
            {
                //AnimeId = Anime.AnimeId,
                //Name = Anime.Name,
                //CuontSezon = Anime.CuontSezon,
                //CuontSerios = Anime.CuontSerios,
                //GenreAnime = Anime.GenreAnime.Select(x => x.Description()).ToArray(),
                //Studios = Anime.Studio.Select(x => x.Name).ToList()
            };
        }
        else
        {
            throw new Exception("найти такое аниме не полоучилось");
        }
    }

    //public async Task UpdateAnime(UpdateAnimationSeriecRequest animeUpdate)
    //{
    //    var animationStudios = await _context.Studios.Where(x => animeUpdate.Studios.Contains(x.StudioId)).ToListAsync();
    //    var intStudioAni = animeUpdate.Studios.ToList();
    //    var AnimeUpdate = await _context.Animes.Include(x => x.Studio)
    //        .FirstOrDefaultAsync(an => an.AnimeId == animeUpdate.AnimeId);
    //    if (AnimeUpdate != null)
    //    {
    //        AnimeUpdate.Name = animeUpdate.Name;
    //        AnimeUpdate.CuontSezon = animeUpdate.CuontSezon;
    //        AnimeUpdate.CuontSerios = animeUpdate.CuontSerios;
    //        AnimeUpdate.GenreAnime = animeUpdate.GenreAnime;
    //        AnimeUpdate.Studio = animationStudios;
    //        await _context.SaveChangesAsync();
    //    }
    //    else
    //    {
    //        throw new Exception("аниме не найдена");
    //    }
    //}

    //public async Task DeleteAnime(int animestudioId)
    //{
    //    var Animefail = _context.Animes.FirstOrDefault(st => st.AnimeId == animestudioId);
    //    if (Animefail != null)
    //    {
    //        _context.Remove(Animefail);
    //        await _context.SaveChangesAsync();
    //    }
    //    else
    //    {
    //        throw new Exception("аниме не найдена");
    //    }
    //}

}
