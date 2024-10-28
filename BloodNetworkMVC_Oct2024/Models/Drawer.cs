namespace BloodNetworkMVC_Oct2024.Models
{
    public class Drawer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int CurrentCount { get; set; }
        public int FreezerId { get; set; }
        public Freezer Freezer { get; set; }
        public ICollection<BloodUnit> BloodUnits { get; set; } = new List<BloodUnit>();
    }
}
