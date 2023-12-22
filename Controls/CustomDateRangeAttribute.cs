using System;
using System.ComponentModel.DataAnnotations;

public class CustomDateRangeAttribute : ValidationAttribute
{
    private readonly DateTime _minDate;
    private readonly DateTime _maxDate;

    public CustomDateRangeAttribute() : base("Дата производства должна быть не раньше 1900 года и не больше текущего года.")
    {
        _minDate = new DateTime(1900, 1, 1);
        _maxDate = DateTime.Now.Date;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        Console.WriteLine($"Input Value: {value}");
        if (value is DateTime dateValue)
        {
            if (dateValue < _minDate || dateValue > _maxDate)
            {
                Console.WriteLine($"Error Value: {value}");
                return new ValidationResult(ErrorMessage);
            }
        }
        Console.WriteLine($"Success Value: {value}");
        return ValidationResult.Success;
    }
}
