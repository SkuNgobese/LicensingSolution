using LicensingSolution.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models
{
    public class VehicleLicence
    {

        public VehicleLicence() { }
        
        [Key]
        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Vehicle Register Number")]
        public string VehRegisterNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Vehicle Licence Number")]
        [MaxLength(10)]
        public string VehLicenceNumber { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Vehicle Identification Number")]
        public string VehIdentificationNumber { get; set; }

        [Required(ErrorMessage = "Please enter the vehicle {0}")]
        [Display(Name = "Make")]
        public string VehMake { get; set; }
       
        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Registering Authority")]
        public string RegisteringAuthority { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Date of Expiry")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-mmm-yyyy-}", ApplyFormatInEditMode = true)]
        public DateTime DateOfExpiry { get; set; }

        [ForeignKey("Owner")]
        [Display(Name = "Owner's ID Number")]
        public string OwnerIDNumber { get; set; }
        public virtual Owner Owner { get; set; }

        public virtual Association Association { get; set; }
    }
}
