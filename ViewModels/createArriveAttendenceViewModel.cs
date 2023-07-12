using HrProject.Models;

namespace HrProject.ViewModels
{
    public class createArriveAttendenceViewModel
    {
        public DateTime Date { get; set; }

        public string? ArrivalTime { get; set; }
        public int Emp_Id { get; set; }

        public virtual Employee? Employee { get; set; }
        public List<Employee> employees { get; set; } = new List<Employee>();
    }
}
