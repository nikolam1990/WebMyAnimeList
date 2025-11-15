using Microsoft.AspNetCore.Mvc;
using WebMyAnimeList.Logic;
using WebMyAnimeList.Models;
using WebMyAnimeList.Logic;
using WebMyAnimeList.Data.Entities;

namespace WebMyAnimeList.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _user;

    public UserController(UserService user)
    {
        _user = user;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create(CreateUserRequest user)
    {
        try
        {
            var id = await _user.CreateUser(user);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        return Ok(await _user.GetUsers());
    }

    [HttpGet("Viewer")]
    public async Task<ActionResult<UserResponse>> GetById(int id)
    {
        try 
        { 
            return Ok(await _user.GetUser(id)); 
        }
        catch ( Exception ex)
        {
            return NotFound(ex.Message);
        } 
        
    }
    [HttpPost("Rebranding")]
    public async Task<ActionResult> UpdateUserById(UpdateUserRequest userUpdate)
    {
        try
        {
            await _user.UpdateUser(userUpdate);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

    }
    [HttpDelete("DeleteUser")]
    public async Task<ActionResult> DeleteById(int id)
    {
        try
        {
            await _user.DeleteUser(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("Add a watched episode")]
    public async Task<ActionResult> MarkTheEpisodeWatched(int userId, int episodeID)
    {
        try
        {
            await _user.MarkTheEpisodeWatched(userId, episodeID);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("Unwatched episodes")]
    public async Task<ActionResult<EpisodeResponse>> UnwatchedEpisodes(int animeId, int userId)
    {
        try
        {
            return Ok(await _user.UnwatchedEpisodes(animeId, userId));
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
