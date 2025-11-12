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

    public async Task<int> CreateSeries(CreateAnimeSeriesRequest animeSeriesTitle)
    {
        //нужно ли сделать проверку на сущесвование такого анмие(по айди)
        //нужно ли сделать проверку на сущесвование такой студии(по айди)
        var animeTitle = new AnimeSeries
        {
            Name = animeSeriesTitle.Name,
            Season = animeSeriesTitle.Season,
            Number = animeSeriesTitle.Number,
            AnimeId = animeSeriesTitle.AnimeId,
            StudioId = animeSeriesTitle.StudioId
        };
        _context.Add(animeTitle);
        await _context.SaveChangesAsync();
        return animeTitle.Id;
    }
    public async Task<AnimeSeriesResponse> GetSeries(int episodeId)
    {
        var episodeAnime = await _context.AnimeSeries.FirstOrDefaultAsync(st => st.Id == episodeId);
        if (episodeAnime != null)
        {
            return new AnimeSeriesResponse
            {
                EpisodeId = episodeAnime.Id,
                NameAnime = episodeAnime.Anime.Name,
                NameSeriec = episodeAnime.Name,
                CuontSezon = episodeAnime.Season,
                CuontSerios = episodeAnime.Number,
                Studios = episodeAnime.Studio.Name,
            };
        }
        else
        {
            throw new Exception("найти такой эпизод не полоучилось");
        }
    }

    public async Task UpdateSeries(UpdateAnimeSeriesRequest updateAnimeSeries)
    {
        var episode = await _context.AnimeSeries.FirstOrDefaultAsync(st => st.Id == updateAnimeSeries.EpisodeId);
        if (episode != null)
        {
            episode.Name = updateAnimeSeries.Name;
            episode.Season = updateAnimeSeries.CuontSezon;
            episode.Number = updateAnimeSeries.CuontSerios;
            episode.AnimeId = updateAnimeSeries.AnimeId;
            episode.StudioId = updateAnimeSeries.Studios;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("найти такой эпизод не полоучилось");
        }
    }

    public async Task DeleteSeries(int episodeId)
    {
        var episode = _context.AnimeSeries.FirstOrDefault(s => s.Id == episodeId);
        if (episode != null)
        {
            _context.Remove(episode);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("аниме серия не найдена");
        }
    }
}
