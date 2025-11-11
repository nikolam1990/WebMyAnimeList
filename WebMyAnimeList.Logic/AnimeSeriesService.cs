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
    public async Task<AnimationSeriecResponse> GetAnimeSeriec(int EpisodID)
    {

        var EpisodAnime = await _context.AnimeSeries.FirstOrDefaultAsync(st => st.Id == EpisodID);
        var Anime = await _context.Animes.FirstOrDefaultAsync(an => an.AnimeId == EpisodAnime.AnimeId);
        var StudioCreatedEpisode = await _context.Studios.FirstOrDefaultAsync(st => st.StudioId == EpisodAnime.StudioId);
        
        if (EpisodAnime != null)
        {
            return new AnimationSeriecResponse
            {
                EpisodeId = EpisodAnime.Id,
                NameAnime = Anime.Name,
                NameSeriec = EpisodAnime.Name,
                CuontSezon = EpisodAnime.Season,
                CuontSerios = EpisodAnime.Number,
                Studios = StudioCreatedEpisode.Name
            };
        }
        else
        {
            throw new Exception("найти такой эпизод не полоучилось");
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

    public async Task DeleteEpisode(int EpisodID)
    {
        var EpisodeFail = _context.AnimeSeries.FirstOrDefault(s => s.Id == EpisodID);
        if (EpisodeFail != null)
        {
            _context.Remove(EpisodeFail);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("аниме не найдена");
        }
    }

}
