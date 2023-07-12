using System;
using System.Collections.Generic;

namespace HrProject.Models;

public class Department
{
    public int Id { get; set; }

    public string DeptName { get; set; } = null!;

    public virtual ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    public bool IsDeleted { get; set; }
}
