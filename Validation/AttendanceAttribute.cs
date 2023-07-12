using HrProject.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace HrProject.Validation
{
	public class AttendanceAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var endTime = Convert.ToDateTime(value);
			var attendance = (AttendanceViewModel)validationContext.ObjectInstance;
			if(DateTime.Compare(endTime,attendance.ArrivalTime)>0)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Leave Time Must Be After Arrival Time");
		}
	}
}
