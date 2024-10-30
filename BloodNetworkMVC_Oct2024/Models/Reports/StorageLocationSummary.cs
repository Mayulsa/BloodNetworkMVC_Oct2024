namespace BloodNetworkMVC_Oct2024.Models.Reports
{
    public class StorageLocationSummary
    {
        public int FreezerId { get; set; }
        public string FreezerName { get; set; }
        public int TotalCapacity { get; set; }
        public int CurrentCount { get; set; }
        public int AvailableSpace { get; set; }
        public ICollection<DrawerSummary> DrawerSummaries { get; set; }
    }
}
