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
    [HttpPost("CreateSeriec")]
    public async Task<ActionResult<int>> Create(CreateAnimeSeriesRequest animeTitleSeries)
    {
        try
        {
            //нужно ли сделать проверку на сущесвование такого анмие(по айди)
            //нужно ли сделать проверку на сущесвование такой студии(по айди)
            return Ok(await _animeTaitleSetie.CreateSeries(animeTitleSeries));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("Seriec")]
    public async Task<ActionResult<AnimationSeriecResponse>> GetById(int id)
    {
        try
        {
            return Ok(await _animeTaitleSetie.GetSeriec(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("UpdateSeriec")]
    public async Task<ActionResult> UpdateSeriecById(UpdateAnimationSeriecRequest animeTitleSeries)
    {
        try
        {
            await _animeTaitleSetie.UpdateSeriec(animeTitleSeries);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpDelete("DeleteSeriec")]
    public async Task<ActionResult> DeleteSeriecById(int id)
    {
        try
        {
            await _animeTaitleSetie.DeleteSeriec(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}
