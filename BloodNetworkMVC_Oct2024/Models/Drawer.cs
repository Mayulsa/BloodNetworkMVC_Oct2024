using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BloodNetworkMVC_Oct2024.Models.Validations;

namespace BloodNetworkMVC_Oct2024.Models
{
    public class Drawer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Drawer name is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Name must be between 2 and 50 characters")]
        [Display(Name = "Drawer Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100 units")]
        public int Capacity { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Current count must be between 0 and capacity")]
        [CustomValidation(typeof(DrawerValidation), "ValidateCurrentCount")]
        [Display(Name = "Current Count")]
        public int CurrentCount { get; set; }
        //public int CurrentCount { get => BloodUnits?.Count ?? 0;}

        [Required(ErrorMessage = "Freezer is required")]
        [Display(Name = "Freezer")]
        public int FreezerId { get; set; }

        [ForeignKey("FreezerId")]
        public virtual Freezer Freezer { get; set; }

        public virtual ICollection<BloodUnit> BloodUnits { get; set; } = new List<BloodUnit>();

        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int Capacity { get; set; }
        //public int CurrentCount { get; set; }
        //public int FreezerId { get; set; }
        //public Freezer Freezer { get; set; }
        //public ICollection<BloodUnit> BloodUnits { get; set; } = new List<BloodUnit>();
    }
}
