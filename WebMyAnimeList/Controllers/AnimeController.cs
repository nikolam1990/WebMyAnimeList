using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMyAnimeList.Data.Entities;
using WebMyAnimeList.Logic;
using WebMyAnimeList.Models;

namespace WebMyAnimeList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeService _animeTaitle;

        public AnimeController(AnimeService animeTaitle)
        {
            _animeTaitle = animeTaitle;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CreateAnimationRequest animeTaitle)
        {
            try
            {
                return Ok(await _animeTaitle.CreateAnime(animeTaitle));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimationResponse>>> Gets()
        {
            return Ok(await _animeTaitle.GetAnimes());
        }

        [HttpGet("Taitle")]
        public async Task<ActionResult<AnimationResponse>> GetById(int id)
        {
            try
            {
                return Ok(await _animeTaitle.GetAnime(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Rebranding")]
        public async Task<ActionResult> UpdateStudioById(UpdateAnimationRequest animeupdate)
        {
            try
            {
                await _animeTaitle.UpdateAnime(animeupdate);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("DeleteTaitle")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                await _animeTaitle.DeleteAnime(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
