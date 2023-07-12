using HrProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HrProject.Repositories.GeneralSettingRepo
{
	public class GeneralSetiingRepository : IGeneralSettingRepository
	{
		private readonly HrContext context;

		public GeneralSetiingRepository(HrContext context)
        {
			this.context = context;
		}
        public async Task AddAsync(GeneralSetting NewGeneralSetting)
		{
			var generalSettingDb =await context.GeneralSettings.FirstOrDefaultAsync(g => g.Id == 1);
			if(generalSettingDb == null)
			{
				NewGeneralSetting.Id = 1;
				await context.GeneralSettings.AddAsync(NewGeneralSetting);
			}
			else
			{
				generalSettingDb.ValueDiscount = NewGeneralSetting.ValueDiscount;
				generalSettingDb.ValueExtra = NewGeneralSetting.ValueExtra;
				context.GeneralSettings.Update(generalSettingDb);
				//context.Entry(NewGeneralSetting).State = EntityState.Modified;
			}
			await context.SaveChangesAsync();
		}

		public int DiscountTimePricePerHour()
		{
			return context.GeneralSettings.Select(g => g.ValueDiscount).FirstOrDefault();
		}

		public int OverTimePricePerHour()
		{
			return context.GeneralSettings.Select(g => g.ValueExtra).FirstOrDefault();
		}
	}
}
