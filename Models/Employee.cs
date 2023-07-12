using HrProject.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrProject.Models;

public class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? City { get; set; } = null!;

    public string Phone { get; set; }

    public string Gender { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string NationalId { get; set; }

    public double Salary { get; set; }

    [HireDate]
    public DateTime? HireDate { get; set; }

    public DateTime? BirthDate { get; set; }

    public TimeSpan? ArrivalTime { get; set; }

    public TimeSpan? LeaveTime { get; set; }

	[ForeignKey("Department")]
	public int Departmentid { get; set; }
    public virtual Department Department { get; set; }

    public virtual ICollection<Attendance>? Attendances { get; set; } = new List<Attendance>();
    public bool IsDelelted { get; set; }
}
