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
    private readonly AnimeSeriesService _animeSeriesService;

    public AnimeSeriesController(AnimeSeriesService animeSeriesService)
    {
        _animeSeriesService = animeSeriesService;
    }

    [HttpPost("CreateSeries")]
    public async Task<ActionResult<int>> Create(CreateAnimeSeriesRequest createRequest)
    {
        try
        {
            return Ok(await _animeSeriesService.CreateSeries(createRequest));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Series")]
    public async Task<ActionResult<AnimeSeriesResponse>> GetById(int animeId, int seasonId)
    {
        try
        {
            return Ok(await _animeSeriesService.GetSeriesInSeason(animeId, seasonId));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("UpdateSeries")]
    public async Task<ActionResult> UpdateSeriecById(UpdateAnimeSeriesRequest updateRequest)
    {
        try
        {
            await _animeSeriesService.UpdateSeries(updateRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("DeleteSeries")]
    public async Task<ActionResult> DeleteSeriesById(int id)
    {
        try
        {
            await _animeSeriesService.DeleteSeries(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
