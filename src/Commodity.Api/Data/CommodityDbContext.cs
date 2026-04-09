using Microsoft.EntityFrameworkCore;
using Commodity.Api.Models;

namespace Commodity.Api.Data;

public class CommodityDbContext(DbContextOptions<CommodityDbContext> options) : DbContext(options)
{
    public DbSet<CommodityModel> Commodities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommodityModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Category).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });

        base.OnModelCreating(modelBuilder);
    }
}