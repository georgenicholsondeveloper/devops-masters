using Microsoft.EntityFrameworkCore;
using Commodity.Api.Models;
using Commodity.Api.DTOs;
using Commodity.Api.Data;

namespace Commodity.Api.Services;

public class CommodityService(CommodityDbContext context) : ICommodityService
{
    private readonly CommodityDbContext _context = context;

    public async Task<IEnumerable<CommodityModel>> GetAllCommoditiesAsync()
    {
        return await _context.Commodities.ToListAsync();
    }

    public async Task<CommodityModel?> GetCommodityByIdAsync(int id)
    {
        return await _context.Commodities.FindAsync(id);
    }

    public async Task<CommodityModel> CreateCommodityAsync(CreateCommodityDto dto)
    {
        var article = new CommodityModel
        {
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        _context.Commodities.Add(article);
        await _context.SaveChangesAsync();
        
        return article;
    }

    public async Task<CommodityModel?> UpdateCommodityAsync(int id, UpdateCommodityDto dto)
    {
        var article = await _context.Commodities.FindAsync(id);
        
        if (article == null)
        {
            return null;
        }
        
        article.Name = dto.Name;
        article.Price = dto.Price;
        article.Category = dto.Category;
        article.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        
        return article;
    }

    public async Task<bool> DeleteCommodityAsync(int id)
    {
        var article = await _context.Commodities.FindAsync(id);
        
        if (article == null)
        {
            return false;
        }
        
        _context.Commodities.Remove(article);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<int> GetCommoditiesCountAsync()
    {
        var commodities = await _context.Commodities.ToListAsync();

        var commodityCounter = 0;

        var failedCommodityCounter = 0;

        foreach(var commodity in commodities)
        {
            if(commodity == null)
            {
                failedCommodityCounter++;
                continue;
            }

            Console.WriteLine("I am retrieving and counting commodities");

            commodityCounter++;

            Console.WriteLine("Commodities increased by 1");

            Console.WriteLine("Reporting Failed Commodities");

            if(failedCommodityCounter < 1){
                Console.WriteLine("No commodities failed");
            }
        }

        return commodities.Count;
    }    
}