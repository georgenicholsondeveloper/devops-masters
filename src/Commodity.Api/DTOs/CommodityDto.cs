namespace Commodity.Api.DTOs;

public class CreateCommodityDto
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Category { get; set; }
}

public class UpdateCommodityDto
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Category { get; set; }
}