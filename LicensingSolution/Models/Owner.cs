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
    public class Owner
    {
        public Owner()
        {
            this.OperatingLicences = new List<OperatingLicence>();
            this.VehicleLicences = new List<VehicleLicence>();
            this.Drivers = new List<Driver>();
        }

        [Key]
        [RSAIDNumber]
        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "ID Number")]
        public string OwnerId { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please make sure there are no spaces, special characters & numbers on {0}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please make sure there are no spaces, special characters & numbers on {0}")]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please make sure there are no spaces, special characters & numbers on {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + MiddleName + " " + LastName; } }

        [EmailAddress]
        [Remote(action: "EmailExists", controller: "Owners")]        
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Phone]
        [Display( Name = "Primary Contact Number")]
        public string PrimaryContactNumber { get; set; }

        [Phone]
        [Display(Name = "Secondary Contact Number")]
        public string SecondaryContactNumber { get; set; }  

        [Required(ErrorMessage = "Please enter House number and Street!")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "{0} address cannot contain special characters")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter Suburb/Location!")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "{0} cannot contain numbers or special characters")]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }

        [Required(ErrorMessage = "Please enter City/Town!")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "{0} cannot contain numbers or special characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter Postal Code!")]
        [StringLength(4, ErrorMessage = "{0} must be {1} digits long", MinimumLength = 4)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "{0} cannot contain letters or special characters.")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [ForeignKey("Association")]
        [Display(Name = "Association")]
        [Required(ErrorMessage = "Please select {0}")]
        public int AssociationId { get; set; }
        public virtual Association Association { get; set; }

        public virtual ICollection<OperatingLicence> OperatingLicences { get; set; }
        public virtual ICollection<VehicleLicence> VehicleLicences { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
