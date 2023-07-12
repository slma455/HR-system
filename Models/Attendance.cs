using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrProject.Models;



//[PrimaryKey(nameof(Date),nameof(Emp_Id))]
/// <summary>
/// //
/// </summary>
public class Attendance
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public DateTime? DepartureTime { get; set; }
    public bool Absent { get; set; }
    public int? Bounshour { get; set; }
    public int? DiscountHour { get; set; }

    [ForeignKey("Employee")]
    public int? Emp_Id { get; set; }

    public virtual Employee Employee { get; set; }
}
