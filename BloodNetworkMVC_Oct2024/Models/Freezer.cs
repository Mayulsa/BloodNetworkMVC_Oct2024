using System.ComponentModel.DataAnnotations;

namespace BloodNetworkMVC_Oct2024.Models
{
    public class Freezer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Freezer name is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Name must be between 2 and 50 characters")]
        [Display(Name = "Freezer Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Location must be between 2 and 100 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Temperature is required")]
        [Range(-80, -20, ErrorMessage = "Temperature must be between -80°C and -20°C")]
        [Display(Name = "Temperature (°C)")]
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public double Temperature { get; set; }

        [Required]
        [Display(Name = "Operational Status")]
        public bool IsOperational { get; set; }

        public virtual ICollection<Drawer> Drawers { get; set; } = new List<Drawer>();
        public virtual ICollection<BloodUnit> BloodUnits { get; set; } = new List<BloodUnit>();

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Location { get; set; }
        //public double Temperature { get; set; }
        //public bool IsOperational { get; set; }
        //public ICollection<Drawer> Drawers { get; set; } = new List<Drawer>();
        //public ICollection<BloodUnit> BloodUnits { get; set; } = new List<BloodUnit>();
    }
}
