
using System;
using System.Globalization;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;


namespace la_mia_pizzeria_crud_mvc.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    sealed public class MoreThanFiveWordsValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string fieldValue = (string)value;

            int wordsNumber = fieldValue.Trim().Split(' ').Length;

            if (wordsNumber < 5)
                return new ValidationResult("Il campo deve contenere almeno 5 parole");


            return ValidationResult.Success;

        }
    }
}
