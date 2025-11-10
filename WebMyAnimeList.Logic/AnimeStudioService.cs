using Microsoft.EntityFrameworkCore;
using WebMyAnimeList.Data;
using WebMyAnimeList.Data.Entities;
using WebMyAnimeList.Models;

namespace WebMyAnimeList.Logic;

public class AnimeStudioService
{
    private readonly ApplicationContext _context;

    public AnimeStudioService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<int> CreateStudio(CreateAnimationStudioRequest animationStudio)
    {
        var Studios = await _context.Studios.Select(x => x.Name).ToListAsync();
        if (Studios.Contains(animationStudio.Name)) 
        {
            throw new Exception("такая студия уже есть");
        }
        else
        {
            var AnimeStudio = new AnimationStudio
            {
                Name = animationStudio.Name,
                YearOfFoundation = animationStudio.YearOfFoundation
            };

            _context.Add(AnimeStudio);
            await _context.SaveChangesAsync();

            return AnimeStudio.StudioId;
        }

    }
    public async Task<List<AnimationStudioResponse>> GetStudios()
    {
        return await _context.Studios.Select(studio =>
            new AnimationStudioResponse
            {
                Id = studio.StudioId,
                Name = studio.Name,
                YearOfFoundation = studio.YearOfFoundation
            }).ToListAsync();

    }
    public async Task<AnimationStudioResponse> GetStudio(int animestudioId)
    {
        var Animestudio = await _context.Studios.FirstOrDefaultAsync(st => st.StudioId == animestudioId);
        if (Animestudio != null) 
        {
            return new AnimationStudioResponse
            {
                Id = Animestudio.StudioId,
                Name = Animestudio.Name,
                YearOfFoundation = Animestudio.YearOfFoundation
            };
        }
        else
        {
            throw new Exception("найти такую студию  не полоучилось");
        }

    }
    public async Task<List<AnimationStudioResponse>> GetStudiosById(List<int> animestudioId)
    {
        var result = new List<AnimationStudioResponse>(animestudioId.Count); 
        foreach (int i in animestudioId)
        {
            var Animestudio = await _context.Studios.FirstOrDefaultAsync(st => st.StudioId == i);
            if (Animestudio != null)
            {
                AnimationStudioResponse temp = new AnimationStudioResponse
                {
                    Id = Animestudio.StudioId,
                    Name = Animestudio.Name,
                    YearOfFoundation = Animestudio.YearOfFoundation
                };
                result.Add(temp);
            }
            else
            {
                throw new Exception("найти такую студию  не полоучилось");
            }
        }
        return result;
    }

    public async Task UpdateStudio(UpdateAnimationStudioRequest studioupdate)
    {
        var AnimeStudio = await _context.Studios.FirstOrDefaultAsync(studio => studio.StudioId == studioupdate.UpdateStudioId);
        if (AnimeStudio != null)
        {
            AnimeStudio.Name = studioupdate.UpdateName;
            AnimeStudio.YearOfFoundation = studioupdate.UpdateYear;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("студия не найдена");
        }
    }
    public async Task DeleteStudio(int animestudioId)
    {
        var AnimeStudio = _context.Studios.FirstOrDefault(st => st.StudioId == animestudioId);
        if (AnimeStudio != null)
        {
            _context.Remove(AnimeStudio);
            await _context.SaveChangesAsync();
        }
        else 
        {
            throw new Exception("студия не найдена"); 
        }
        
    }

}



