using LicensingSolution.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models
{
    public class DrivingLicence : IValidatableObject
    {

        public DrivingLicence() { }

        [Key]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Please make sure {0} does not have spaces or special characters")]
        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Vehicle Registration")]
        public string LicenceNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Sticker Number")]
        public string VehicleStickerNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Licence Expiry Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-mmm-yyyy-}", ApplyFormatInEditMode = true)]
        public DateTime LicenceExpiryDate { get; set; }
        
        [Display(Name = "PDP Expiry Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-mmm-yyyy-}", ApplyFormatInEditMode = true)]
        public DateTime PDPExpiryDate { get; set; }

        [ForeignKey("Driver")]
        [Display(Name = "Driver's ID")]
        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LicenceExpiryDate.Date <= DateTime.Today)
            {
                yield return new ValidationResult(
                    $"You can not enter today's date or past date",
                    new[] { "LicenceExpiryDate" });
            }
            if (PDPExpiryDate.Date <= DateTime.Today)
            {
                yield return new ValidationResult(
                    $"You can not enter today's date or past date",
                    new[] { "PDPExpiryDate" });
            }
        }
    }
}
