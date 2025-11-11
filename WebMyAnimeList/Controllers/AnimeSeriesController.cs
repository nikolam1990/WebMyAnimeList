using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMyAnimeList.Data.Entities;
using WebMyAnimeList.Models;
using WebMyAnimeList.Logic;

namespace WebMyAnimeList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimeSeriesController : ControllerBase
{
    private readonly AnimeSeriesService _animeTaitleSetie;

    public AnimeSeriesController(AnimeSeriesService animeTaitleSeries)
    {
        _animeTaitleSetie = animeTaitleSeries;
    }
    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create(CreateAnimeSeriesRequest animeTitleSeries)
    {
        try
        {
            return Ok(await _animeTaitleSetie.CreateAnimeSeries(animeTitleSeries));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
