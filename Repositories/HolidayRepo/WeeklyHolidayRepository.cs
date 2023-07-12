using HrProject.Models;
using HrProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HrProject.Repositories.HolidayRepo
{
	public class WeeklyHolidayRepository : IWeeklyHolidayRepository
	{
		private readonly HrContext context;

		public WeeklyHolidayRepository(HrContext context)
		{
			this.context = context;
		}

		public async Task AddAsync(List<WeekDaysViewModel> selectedDays)
		{
			foreach (var item in selectedDays)
			{
				await context.EmployeeHolidays.AddAsync(new EmployeeHoliday { Genral_Id = 1, Holiday = item.Day });
			}
		}

		public async Task DeleteAllAsync()
		{
			var allHolidays = context.EmployeeHolidays.ToList();

			context.EmployeeHolidays.RemoveRange(allHolidays);
			await context.SaveChangesAsync();
		}

		public List<EmployeeHoliday> GetAllHolidays()
		{
			return context.EmployeeHolidays.ToList();
		}
	}
}
