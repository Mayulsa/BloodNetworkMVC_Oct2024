using BloodNetworkMVC_Oct2024.Data;
using BloodNetworkMVC_Oct2024.Models;
using BloodNetworkMVC_Oct2024.Models.Reports;
using Microsoft.EntityFrameworkCore;

namespace BloodNetworkMVC_Oct2024.Services
{
    public class BloodInventoryService : IBloodInventoryService
    {
        private readonly ApplicationDbContext _context;

        public BloodInventoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BloodUnit>> GetExpiringBloodUnits(int daysThreshold)
        {
            var thresholdDate = DateTime.Now.AddDays(daysThreshold);
            return await _context.BloodUnits
                .Include(b => b.Freezer)
                .Include(b => b.Drawer)
                .Where(b => b.Status == "Available" &&
                           b.ExpirationDate <= thresholdDate &&
                           b.ExpirationDate > DateTime.Now)
                .OrderBy(b => b.ExpirationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BloodUnit>> FilterByBloodGroup(string bloodGroup)
        {
            return await _context.BloodUnits
                .Include(b => b.Freezer)
                .Include(b => b.Drawer)
                .Where(b => b.Status == "Available" &&
                           b.BloodGroup == bloodGroup)
                .OrderBy(b => b.ExpirationDate)
                .ToListAsync();
        }

        public async Task<InventoryReport> GenerateInventoryReport()
        {
            var report = new InventoryReport
            {
                GeneratedAt = DateTime.Now,
                TotalUnits = await _context.BloodUnits.CountAsync(),
                AvailableUnits = await _context.BloodUnits
                    .Where(b => b.Status == "Available")
                    .CountAsync(),
                ExpiringIn7Days = await _context.BloodUnits
                    .Where(b => b.Status == "Available" &&
                               b.ExpirationDate <= DateTime.Now.AddDays(7) &&
                               b.ExpirationDate > DateTime.Now)
                    .CountAsync(),
                BloodGroupSummary = await GetBloodGroupSummary(),
                StorageLocationSummary = await GetStorageLocationSummary()
            };

            return report;
        }

        public async Task<IEnumerable<BloodGroupSummary>> GetBloodGroupSummary()
        {
            return await _context.BloodUnits
                .Where(b => b.Status == "Available")
                .GroupBy(b => b.BloodGroup)
                .Select(g => new BloodGroupSummary
                {
                    BloodGroup = g.Key,
                    TotalUnits = g.Count(),
                    ExpiringIn7Days = g.Count(b => b.ExpirationDate <= DateTime.Now.AddDays(7) &&
                                                  b.ExpirationDate > DateTime.Now)
                })
                .ToListAsync();
        }

        private async Task<IEnumerable<StorageLocationSummary>> GetStorageLocationSummary()
        {
            return await _context.Freezers
                .Select(f => new StorageLocationSummary
                {
                    FreezerId = f.Id,
                    FreezerName = f.Name,
                    TotalCapacity = f.Drawers.Sum(d => d.Capacity),
                    CurrentCount = f.Drawers.Sum(d => d.CurrentCount),
                    AvailableSpace = f.Drawers.Sum(d => d.Capacity - d.CurrentCount),
                    DrawerSummaries = f.Drawers.Select(d => new DrawerSummary
                    {
                        DrawerId = d.Id,
                        DrawerName = d.Name,
                        Capacity = d.Capacity,
                        CurrentCount = d.CurrentCount,
                        AvailableSpace = d.Capacity - d.CurrentCount
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
