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

public class AnimeSeriesServis
{
    private readonly ApplicationContext _context;
    public AnimeSeriesServis(ApplicationContext context)
    {
        _context = context;
    }



    //private readonly ApplicationContext _context;

    //public AnimeService(ApplicationContext context)
    //{
    //    _context = context;
    //}



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

}
