namespace BloodNetworkMVC_Oct2024.Models.Reports
{
    public class DrawerSummary
    {
        public int DrawerId { get; set; }
        public string DrawerName { get; set; }
        public int Capacity { get; set; }
        public int CurrentCount { get; set; }
        public int AvailableSpace { get; set; }
    }
}
