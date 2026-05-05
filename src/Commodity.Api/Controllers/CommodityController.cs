using Microsoft.AspNetCore.Mvc;
using Commodity.Api.Models;
using Commodity.Api.Services;
using Commodity.Api.DTOs;

namespace Commodity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommodityController(ICommodityService commodityService) : ControllerBase
{
    private readonly ICommodityService _commodityService = commodityService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommodityModel>>> GetAllCommodities()
    {
        var commodities = await _commodityService.GetAllCommoditiesAsync();
        return Ok(commodities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommodityModel>> GetCommodity(int id)
    {
        var article = await _commodityService.GetCommodityByIdAsync(id);
        
        if (article == null)
        {
            return NotFound();
        }
        
        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<CommodityModel>> CreateCommodity(CreateCommodityDto dto)
    {
        var article = await _commodityService.CreateCommodityAsync(dto);
        return CreatedAtAction(nameof(GetCommodity), new { id = article.Id }, article);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CommodityModel>> UpdateCommodity(int id, UpdateCommodityDto dto)
    {
        var article = await _commodityService.UpdateCommodityAsync(id, dto);
        
        if (article == null)
        {
            return NotFound();
        }
        
        return Ok(article);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCommodity(int id)
    {
        var success = await _commodityService.DeleteCommodityAsync(id);
        
        if (!success)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}