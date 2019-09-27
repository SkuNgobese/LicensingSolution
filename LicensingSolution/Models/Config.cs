using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models
{
    public class Config
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the {0}")]
        public string Value { get; set; }
    }
}
