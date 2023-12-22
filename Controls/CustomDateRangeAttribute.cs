using System;
using System.ComponentModel.DataAnnotations;

public class CustomDateRangeAttribute : ValidationAttribute
{
    // Minimum Date in Range Variable 
    private readonly DateTime _minDate;
    // Maximum Date in Range Variable
    private readonly DateTime _maxDate;

    // Constructor With Validation Error, Maximum Date, Minimum Date Initialization 
    public CustomDateRangeAttribute() : base("Дата производства должна быть не раньше 1900 года и не больше текущего года.")
    {
        _minDate = new DateTime(1900, 1, 1);
        _maxDate = DateTime.Now.Date;
    }

    // Validation Method That Check Input Value 
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        Console.WriteLine($"Input Value: {value}");

        // Validation Check Input Value Is DateTime 
        if (value is DateTime dateValue)
        {
            // Validation Failed 
            if (dateValue < _minDate || dateValue > _maxDate)
            {
                Console.WriteLine($"Error Value: {value}");

                // Return Error Message Result
                return new ValidationResult(ErrorMessage);
            }
        }

        // Validation Success 
        Console.WriteLine($"Success Value: {value}");

        // Return Validation Success Result
        return ValidationResult.Success;
    }
}
