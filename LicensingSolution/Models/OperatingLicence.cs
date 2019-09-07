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
    public class OperatingLicence: IValidatableObject
    {

        public OperatingLicence() { }

        [Key]
        [Required(ErrorMessage = "Please enter the {0}")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Operating Licence Number")]
        public string OperatingLicenceNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [StringLength(10, ErrorMessage = "{0} can not exceed 10 charachers")]
        [Display(Name = "Application Number")]
        public string ApplicationNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [Display(Name = "Association Name")]
        public string AssociationName { get; set; }              

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Vehicle Registration Number")]
        [MaxLength(10, ErrorMessage ="{0} can not exceed {1} characters")]
        public string VehRegistrationNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Engine Number")]
        public string EngineNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Gross Vehicle Mass")]
        public int VehMass { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Vehicle Description")]
        public string VehDescription { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Passengers")]
        public int Passengers { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Year of Registration")]
        public int YearOfReg { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Valid From Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-mmm-yyyy-}", ApplyFormatInEditMode = true)]
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Valid Until Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-mmm-yyyy-}", ApplyFormatInEditMode = true)]
        public DateTime ValidUntil { get; set; }

        [ForeignKey("Owner")]
        [Display(Name = "Owner's ID")]
        public string OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Association Association { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (YearOfReg > DateTime.Today.Year)
            {
                yield return new ValidationResult(
                    $"Year of Registration can not be future year.",
                    new[] { "YearOfReg" });
            }
            if (YearOfReg < 1980)
            {
                yield return new ValidationResult(
                    $"Please enter valid Registration Year.",
                    new[] { "YearOfReg" });
            }
            if (ValidFrom.Date > DateTime.Today.AddDays(2))
            {
                yield return new ValidationResult(
                    $"Valid From Date can not be future date.",
                    new[] { "ValidFrom" });
            }
            if (ValidUntil.Date < DateTime.Today || ValidUntil.Date < ValidFrom.Date)
            {
                yield return new ValidationResult(
                    $"You can not enter a past date or date before Valid From Date",
                    new[] { "ValidUntil" });
            }
        }
    }
}
