using HrProject.Validation;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HrProject.ViewModels
{
	public class AttendanceViewModel
	{
		[Required(ErrorMessage ="Please Choose Date")]
		//[HireDate]
		[AttendanceDate]
        public DateTime Date { get; set; }

		[Required(ErrorMessage ="Please Signin")]
		public DateTime ArrivalTime { get; set; }

		[Required(ErrorMessage ="Please Chechout")]
		[Attendance]
		public DateTime DepartureTime { get; set; }

		[Required(ErrorMessage ="Please Select Employee")]
        public int? EmpId { get; set; }

        //-------------------

        public int? Bounshour { get; set; }
        public int? DiscountHour { get; set; }
    }
}
