namespace BloodNetworkMVC_Oct2024.Models.Reports
{
    public class InventoryReport
    {
        public DateTime GeneratedAt { get; set; }
        public int TotalUnits { get; set; }
        public int AvailableUnits { get; set; }
        public int ExpiringIn7Days { get; set; }
        public IEnumerable<BloodGroupSummary> BloodGroupSummary { get; set; }
        public IEnumerable<StorageLocationSummary> StorageLocationSummary { get; set; }
    }
}
