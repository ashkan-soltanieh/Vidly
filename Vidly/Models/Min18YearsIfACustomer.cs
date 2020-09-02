using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Domain;

namespace Vidly.Models
{
    public class Min18YearsIfACustomer : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance; //this gives access to containing class
            
            if(customer.MembershipTypeId == MembershipType.PayAsYouGo || 
               customer.MembershipTypeId == MembershipType.Unknown)
                return ValidationResult.Success;
            
            if(!customer.BirthDate.HasValue)
                return new ValidationResult("Birth Date is required.");

            int age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            if(age < 18)
                return new ValidationResult("Customer must be at least 18 years old to go on a membership.");
            
            return ValidationResult.Success;
        }
    }
}
