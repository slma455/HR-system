using System.ComponentModel.DataAnnotations;

namespace HrProject.Validation
{
	public class ArrivalLeaveTimeRange : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var time = value as TimeSpan?;
			if (time.HasValue && time.Value >= TimeSpan.FromHours(9) && time.Value <= TimeSpan.FromMinutes(17))
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("The time must be between 9:00 AM and 5:00 PM.");

		}
	}
}
