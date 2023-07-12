using HrProject.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace HrProject.Attributes
{
	public class CheckDaysAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var checkList = value as List<WeekDaysViewModel>;
			if (checkList!=null && checkList.Any(n=>n.Checked))
				return ValidationResult.Success;
			return new ValidationResult("Check one day at least");
		}
	}
}
