using HrProject.Models;
using HrProject.Repositories.EmployeeRepo;
using HrProject.Repositories.GeneralSettingRepo;
using HrProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.AttendanceRepository
{
    public class AttendanceRepositary : IAttendanceRepositary
    {
        private readonly HrContext context;


        public AttendanceRepositary(HrContext context)
        {
            this.context = context;

        }

        public void Add(AttendanceViewModel attendance)
        {
            var attendanceDb = new Attendance()
            {
                Date = attendance.Date,
                ArrivalTime = attendance.ArrivalTime,
                DepartureTime = attendance.DepartureTime,
                Emp_Id = attendance.EmpId,
                Bounshour = attendance.Bounshour,
                DiscountHour = attendance.DiscountHour
            };
            context.Attendances.Add(attendanceDb);
            context.SaveChanges();
        }

        public int AttendanceDays(int? empId, DateTime targetDate)
        {
            return context.Attendances.Where(n => n.Emp_Id == empId && n.Date.Month == targetDate.Month && n.Date.Year == targetDate.Year).ToList().Count;
        }
        public int AttendanceDays(int? empId)
        {
            return context.Attendances.Where(n => n.Emp_Id == empId).ToList().Count;
        }

        public void Delete(int id)
        {
            var attendance = context.Attendances.FirstOrDefault(n => n.Id == id);
            context.Attendances.Remove(attendance);
            context.SaveChanges();
        }

        public decimal? DiscountHoursSum(int? empId)
        {
            return context.Attendances.Where(n => n.Emp_Id == empId).Select(n => n.DiscountHour).Sum();
        }

        public List<Attendance> GetAll()
        {
            return context.Attendances.Include(n => n.Employee).Where(n => n.Employee.IsDelelted == false).ToList();
        }

        public List<Attendance> GetAllAttendanceByEmployee(int? empId, DateTime targetDate)
        {
            var emptyAttendanceList = new List<Attendance>();
            if (!(targetDate.Month > DateTime.Now.Month && targetDate.Year > DateTime.Now.Year))
                return context.Attendances.Where(n => n.Emp_Id == empId && n.Date.Year == targetDate.Year && n.Date.Month == targetDate.Month).ToList();
            return emptyAttendanceList;
        }

		public List<Attendance> GetAllAttendanceByEmployeeName(string empName)
		{
			var allAttendaneList = context.Attendances.Where(e=>e.Employee.FirstName.ToLower().Contains(empName.ToLower())).ToList();
            return allAttendaneList;
		}

		public Attendance GetById(int? empId, DateTime todayDate)
        {
            return context.Attendances.FirstOrDefault(n => n.Emp_Id == empId && n.Date.Year == todayDate.Year && n.Date.Month == todayDate.Month && n.Date.Day == todayDate.Day);
        }

        public decimal? OverHoursSum(int? empId)
        {
            return context.Attendances.Where(n => n.Emp_Id == empId).Select(n => n.Bounshour).Sum();
        }

        public void Update(int id, Attendance attendance)
        {
            var oldAttendance = context.Attendances.FirstOrDefault(n => n.Id == id);
            oldAttendance.DepartureTime = attendance.DepartureTime;
            oldAttendance.Bounshour = attendance.Bounshour;
            oldAttendance.DiscountHour = attendance.DiscountHour;
            context.SaveChanges();
        }







        #region Old Attendance

        //public void delete(Attendance attendance)
        //{
        //    Attendance _attendance = GetAttendance(attendance.Id);
        //    context.Remove(_attendance);
        //}

        //public void update(Attendance attendace)
        //{
        //    Attendance _attendance = GetAttendance(attendace.Id);


        //    context.Entry(attendace).State = EntityState.Modified;
        //}

        //public List<Attendance> getAll()
        //{
        //    return context.Attendances.Include(e => e.Employee).ToList();
        //}

        //public Attendance GetAttendance(int id)
        //{
        //    Attendance attendance = context.Attendances.Include(a => a.Employee).FirstOrDefault(a => a.Id == id);
        //    return attendance;
        //}

        //public void save()
        //{
        //    context.SaveChanges();
        //}

        //public void CreatArrive(Attendance attendant)
        //{
        //    context.Add(attendant);
        //}

        //public void updateLeave(Attendance attendant)
        //{


        //    context.Entry(attendant).State = EntityState.Modified;

        //}

        //public Attendance search(string ssn)
        //{
        //    return context.Attendances.Include(e => e.Employee).SingleOrDefault(e => e.Employee.NationalId == ssn);
        //}

        ////public List<dateFormual> GetDateFormuals()
        ////{
        ////    return context.Attendances.Select(x => new dateFormual { Month = x.Date.Month, Year = x.Date.Year }).Distinct().ToList();
        ////}
        #endregion


    }
}
