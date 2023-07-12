using HrProject.Attributes;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace HrProject.ViewModels
{
	public class GeneralSettingViewModel
	{
        [Range(10,50,ErrorMessage = "The extra hour between 10$ and 50$")]
		[Display(Name ="Extra: ")]
        public int Extra { get; set; }

		[Range(10, 50, ErrorMessage = "The extra hour between 10$ and 50$")]
		[Display(Name ="Discount: ")]
		public int Discount { get; set; }
		
		[CheckDays(ErrorMessage ="Select One Day at least")]
        public List<WeekDaysViewModel> DayChecked { get; set; }
    }
	
}
