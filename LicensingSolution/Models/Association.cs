using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models
{
    public class Association
    {
        public Association()
        {
            Users = new List<User>();
            Owners = new List<Owner>();
        }
        [Key]
        [Display(Name = "Association Id")]
        public int AssociationId { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Owner> Owners { get; set; }
    }
}
