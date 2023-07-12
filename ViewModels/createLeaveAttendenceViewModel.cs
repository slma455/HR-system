using HrProject.Models;

namespace HrProject.ViewModels
{
    public class createLeaveAttendenceViewModel
    {
        public DateTime Date { get; set; }
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
        public int Emp_Id { get; set; }

        public virtual Employee? Employee { get; set; } = null!;
    }
}
