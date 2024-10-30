namespace BloodNetworkMVC_Oct2024.Models.Reports
{
    public class BloodGroupSummary
    {
        public string BloodGroup { get; set; }
        public int TotalUnits { get; set; }
        public int ExpiringIn7Days { get; set; }
    }
}
