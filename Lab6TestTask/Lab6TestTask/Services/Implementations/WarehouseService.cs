using Lab6TestTask.Data;
using Lab6TestTask.Enums;
using Lab6TestTask.Models;
using Lab6TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab6TestTask.Services.Implementations;

/// <summary>
/// WarehouseService.
/// Implement methods here.
/// </summary>
public class WarehouseService : IWarehouseService
{
    private readonly ApplicationDbContext _dbContext;

    public WarehouseService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse> GetWarehouseAsync()
    {
        return await _dbContext.Warehouses
            .OrderByDescending(w => w.Products
                .Where(p => p.Status == ProductStatus.ReadyForDistribution)
                .Sum(p => p.Quantity * p.Price))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        return await _dbContext.Warehouses
            .Where(w => w.Products
                .Any(p =>
                    p.ReceivedDate.Year == 2025 &&
                    4 <= p.ReceivedDate.Month &&
                    p.ReceivedDate.Month <= 6))
            .ToListAsync();
    }
}
