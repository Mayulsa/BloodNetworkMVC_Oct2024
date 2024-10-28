namespace BloodNetworkMVC_Oct2024.Models
{
    public class Freezer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Temperature { get; set; }
        public bool IsOperational { get; set; }
        public ICollection<Drawer> Drawers { get; set; } = new List<Drawer>();
        public ICollection<BloodUnit> BloodUnits { get; set; } = new List<BloodUnit>();
    }
}
