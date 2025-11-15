using Microsoft.AspNetCore.Mvc;
using WebMyAnimeList.Logic;
using WebMyAnimeList.Models;
using WebMyAnimeList.Logic;
using WebMyAnimeList.Data.Entities;

namespace WebMyAnimeList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeStudioController : ControllerBase
    {
        private readonly AnimeStudioService _studio;

        public AnimeStudioController(AnimeStudioService studio)
        {
            _studio = studio;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<int>> Create(CreateAnimationStudioRequest animeStudio)
        {
            try
            {
                var id = await _studio.CreateStudio(animeStudio);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<AnimationStudioResponse>>> GetAnimeStudios()
        {
            return Ok(await _studio.GetStudios());
        }

        [HttpGet("Studio")]
        public async Task<ActionResult<AnimationStudioResponse>> GetById(int id)
        {
            try 
            { 
                return Ok(await _studio.GetStudio(id)); 
            }
            catch ( Exception ex)
            {
                return NotFound(ex.Message);
            } 
            
        }

        [HttpPost("Rebranding")]
        public async Task<ActionResult> UpdateStudioById(UpdateAnimationStudioRequest studioupdate)
        {
            try
            {
                await _studio.UpdateStudio(studioupdate);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpDelete("DeleteStudio")]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                await _studio.DeleteStudio(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
