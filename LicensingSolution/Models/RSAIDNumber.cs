using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LicensingSolution.Models
{
    public class RSAIDNumber : ValidationAttribute
    {

        public RSAIDNumber() : base("{0} is not a valid South African ID Number")
        {
            
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-rsaid", "Invalid RSA ID Number");
        }
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IdentityInfo idInfo = new IdentityInfo(value.ToString());

            if (!idInfo.IsValid)
            {
                return new ValidationResult("Invalid ID Number");
            }               

            return ValidationResult.Success;
        }
    }
}
