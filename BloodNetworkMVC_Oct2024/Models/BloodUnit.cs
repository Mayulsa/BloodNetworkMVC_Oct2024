using BloodNetworkMVC_Oct2024.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BloodNetworkMVC_Oct2024.Models.Validations;

namespace BloodNetworkMVC_Oct2024.Models
{
    public class BloodUnit
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Blood group is required")]
        [Display(Name = "Blood Group")]
        [RegularExpression(@"^(A|B|AB|O)[+-]$",
            ErrorMessage = "Blood group must be A+, A-, B+, B-, AB+, AB-, O+, or O-")]
        [StringLength(3)]
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Collection date is required")]
        [Display(Name = "Collection Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CollectionDate { get; set; }

        [Required(ErrorMessage = "Expiration date is required")]
        [Display(Name = "Expiration Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(BloodUnitValidation), "ValidateExpirationDate")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20)]
        [RegularExpression(@"^(Available|Reserved|Discarded)$",
            ErrorMessage = "Status must be Available, Reserved, or Discarded")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Freezer is required")]
        [Display(Name = "Freezer")]
        public int FreezerId { get; set; }

        [ForeignKey("FreezerId")]
        public virtual Freezer Freezer { get; set; }

        [Required(ErrorMessage = "Drawer is required")]
        [Display(Name = "Drawer")]
        public int DrawerId { get; set; }

        [ForeignKey("DrawerId")]
        public virtual Drawer Drawer { get; set; }

        //public int Id { get; set; }
        //public string BloodGroup { get; set; }
        //public DateTime CollectionDate { get; set; }
        //public DateTime ExpirationDate { get; set; }
        //public BloodUnitStatus Status { get; set; } // Available, Reserved, Discarded
        //public int FreezerId { get; set; }
        //public Freezer Freezer { get; set; }
        //public int DrawerId { get; set; }
        //public Drawer Drawer { get; set; }
    }
}
