namespace HrProject.ViewModels
{
    public class SalaryReportAttendanceVM
    {
        public int EmpId { get; set; }
        public String? EmpName { get; set; }
        public double Salary { get; set; }
        public string? DeptName { get; set; }
        public int AttendanceDays { get; set; }
        public int AbsentDays { get; set; }
        public double OverTimePrice { get; set; }

        public double NumbersOverTimeHours { get; set; }


        public double DeductionTimePrice { get; set; }
        // TMD = DeductionTimeHours * Number of late hours
        public double NumbersDeductionTimeHours { get; set; }

        public double TotalMoneyDeducted { get; set; }
        //TMA = OverTimeHours * Number of OverTimeHours
        public double TotalMoneyAdded { get; set; }
        //total = salary + (TMA) - (TMD)
        public double total { get; set; }

        public dateFormual? filterdate { get; set; }

    }

    public class dateFormual
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}

