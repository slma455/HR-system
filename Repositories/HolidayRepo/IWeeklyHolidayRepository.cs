using HrProject.Models;
using HrProject.ViewModels;

namespace HrProject.Repositories.HolidayRepo
{
	public interface IWeeklyHolidayRepository
	{
		List<EmployeeHoliday> GetAllHolidays();
		Task AddAsync(List<WeekDaysViewModel> selectedDays);
		Task DeleteAllAsync();

	}
}
