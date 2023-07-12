using HrProject.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace HrProject.Validation
{
	public class AttendanceEmpolyeeAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var endTime = Convert.ToDateTime(value);
			var attendance = (EmployeeViewModel)validationContext.ObjectInstance;
			var arrivalTime = Convert.ToDateTime(attendance.ArrivalTime);
			if (DateTime.Compare(endTime, arrivalTime) > 0)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Leave Time Must Be After Arrival Time");
		}
	}
}
