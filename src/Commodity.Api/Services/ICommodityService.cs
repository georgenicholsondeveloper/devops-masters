using Commodity.Api.DTOs;
using Commodity.Api.Models;

namespace Commodity.Api.Services;

public interface ICommodityService
{
    Task<IEnumerable<CommodityModel>> GetAllCommoditiesAsync();
    Task<CommodityModel?> GetCommodityByIdAsync(int id);
    Task<CommodityModel> CreateCommodityAsync(CreateCommodityDto dto);
    Task<CommodityModel?> UpdateCommodityAsync(int id, UpdateCommodityDto dto);
    Task<bool> DeleteCommodityAsync(int id);
}