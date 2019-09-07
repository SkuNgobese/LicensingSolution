using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string Title { get; set; }

        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string MiddleName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        public string FullName { get { return Title + " " + FirstName + " " + MiddleName + " " + LastName; } }

        [ForeignKey("Association")]
        [Display(Name = "Association")]
        public int AssociationId { get; set; }
        public virtual Association Association { get; set; }
    }
}
