using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookCave.CustomValidation 
{
    public class CheckboxAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!((bool)value)) { return new ValidationResult("Checkbox invalid"); }
            return ValidationResult.Success;
        }
    }
}