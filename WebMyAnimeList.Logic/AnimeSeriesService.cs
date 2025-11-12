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

    public async Task<int> CreateSeries (CreateAnimeSeriesRequest animeSeriesTail)
    {
        //нужно ли сделать проверку на сущесвование такого анмие(по айди)
        //нужно ли сделать проверку на сущесвование такой студии(по айди)
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
    public async Task<AnimationSeriecResponse> GetSeriec(int EpisodID)
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

    public async Task UpdateSeriec(UpdateAnimationSeriecRequest UpdateAnimeSeriec)
    {
        
        var UpdateEpisodAnime = await _context.AnimeSeries.FirstOrDefaultAsync(st => st.Id == UpdateAnimeSeriec.EpisodeId);
       
        if (UpdateEpisodAnime != null)
        {
            {
                UpdateEpisodAnime.Name = UpdateAnimeSeriec.Name;
                UpdateEpisodAnime.Season = UpdateAnimeSeriec.CuontSezon;
                UpdateEpisodAnime.Number = UpdateAnimeSeriec.CuontSerios;
                UpdateEpisodAnime.AnimeId = UpdateAnimeSeriec.AnimeId;
                UpdateEpisodAnime.StudioId = UpdateAnimeSeriec.Studios;
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            throw new Exception("найти такой эпизод не полоучилось");
        }

    }

    public async Task DeleteSeriec(int EpisodID)
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
