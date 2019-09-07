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
    public class DrivingLicence
    {

        public DrivingLicence() { }

        [Key]
        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Licence Number")]
        public string LicenceNumber { get; set; }

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
    }
}
