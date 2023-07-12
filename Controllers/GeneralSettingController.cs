using HrProject.Global;
using HrProject.Global.Methods;
using HrProject.Models;
using HrProject.Repositories.GeneralSettingRepo;
using HrProject.Repositories.HolidayRepo;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrProject.Controllers
{
	public class GeneralSettingController : Controller
	{
		private readonly IGeneralSettingRepository generalSettingRepository;
		private readonly IWeeklyHolidayRepository weeklyHolidayRepository;

		public GeneralSettingController(IGeneralSettingRepository generalSettingRepository,IWeeklyHolidayRepository weeklyHolidayRepository)
        {
			this.generalSettingRepository = generalSettingRepository;
			this.weeklyHolidayRepository = weeklyHolidayRepository;
		}


		[Authorize(Permissions.GeneralSetting.View)]
        public IActionResult Index()
		{
			var generalVM = new GeneralSettingViewModel();
			generalVM.Extra = generalSettingRepository.OverTimePricePerHour();
			generalVM.Discount = generalSettingRepository.DiscountTimePricePerHour();
			var SelectedDays = weeklyHolidayRepository.GetAllHolidays().Select(n => n.Holiday).ToList();
			var allDays = GlobalMethods.GetWeekDay();
			var checkedDays = allDays.Select(n => new WeekDaysViewModel { Day = n }).ToList();
			foreach (var item in checkedDays)
			{
				if (SelectedDays.Any(n => n == item.Day))
				{
					item.Checked = true;
				}
				else
				{
					item.Checked = false;
				}
			}
			generalVM.DayChecked = checkedDays;
			return View(generalVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Permissions.GeneralSetting.Add)]
		public async Task<IActionResult> Save(GeneralSettingViewModel newGeneralSetting)
		{
			
			if (ModelState.IsValid)
			{
				await weeklyHolidayRepository.DeleteAllAsync();
				var selectedDays = newGeneralSetting.DayChecked.Where(n => n.Checked).ToList();
				await weeklyHolidayRepository.AddAsync(selectedDays);
				await generalSettingRepository.AddAsync(new GeneralSetting
				{
					ValueDiscount = newGeneralSetting.Discount,
					ValueExtra = newGeneralSetting.Extra
				});
				return RedirectToAction(nameof(Index));
			}
			return View("Index", newGeneralSetting);
		}
	
	}
}
