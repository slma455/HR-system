using HrProject.Models;
namespace HrProject.Repositories.GeneralSettingRepo
{
	public interface IGeneralSettingRepository
	{
		Task AddAsync(GeneralSetting generalSetting);
		int OverTimePricePerHour();
		int DiscountTimePricePerHour();
	}
}
