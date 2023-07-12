using System.ComponentModel.DataAnnotations;

namespace HrProject.Validation
{
	public class AttendanceDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var hiredate = value as DateTime?;
			if (hiredate.HasValue && hiredate.Value.Year > 2008)
				return ValidationResult.Success;
			return new ValidationResult("Attendance Date Can not be before 2008");
		}
	}
}
