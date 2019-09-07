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
    public class Driver
    {

        public Driver() { }

        [Key]
        [RSAIDNumber]
        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "ID Number")]
        public string DriverId { get; set; }

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

        [Display(Name = "Driver Photo Path")]
        public string ImgPath { get; set; }

        [ForeignKey("Owner")]
        [Required(ErrorMessage = "Please enter {0}")]
        [Display(Name = "Owner's ID")]
        public string OwnerId { get; set; }
        public virtual Owner Owner { get; set; }  
        
        public virtual DrivingLicence DrivingLicence { get; set; }
    }
}
