using BloodNetworkMVC_Oct2024.Models;
using BloodNetworkMVC_Oct2024.Models.Reports;


namespace BloodNetworkMVC_Oct2024.Services
{
    public interface IBloodInventoryService
    {
        Task<IEnumerable<BloodUnit>> GetExpiringBloodUnits(int daysThreshold);
        Task<IEnumerable<BloodUnit>> FilterByBloodGroup(string bloodGroup);
        Task<InventoryReport> GenerateInventoryReport();
        Task<IEnumerable<BloodGroupSummary>> GetBloodGroupSummary();
    }
}
