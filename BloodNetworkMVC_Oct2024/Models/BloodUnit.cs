using BloodNetworkMVC_Oct2024.Models.Enums;

namespace BloodNetworkMVC_Oct2024.Models
{
    public class BloodUnit
    {
        public int Id { get; set; }
        public string BloodGroup { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public BloodUnitStatus Status { get; set; } // Available, Reserved, Discarded
        public int FreezerId { get; set; }
        public Freezer Freezer { get; set; }
        public int DrawerId { get; set; }
        public Drawer Drawer { get; set; }
    }
}
