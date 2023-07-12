using HrProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.EmployeeRepo
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly HrContext context;
		public EmployeeRepository(HrContext context)
		{
			this.context = context;
		}
		public List<Employee> GetAllEmployees()
		{
			return context.Employees.Where(n => !n.IsDelelted).ToList();
		}
		public Employee GetEmployeeById(int? id)
		{
			return context.Employees.Include(e => e.Department).FirstOrDefault(emp => emp.Id == id);
		}
		public List<Employee> GetEmployeeByName(string name)
		{
			return context.Employees
				.Where(emp =>
					(emp.FirstName ?? string.Empty + emp.LastName ?? string.Empty)
						.ToLower()
						.Contains(name.ToLower())
					)
				.ToList();
		}

		public void Insert(Employee employee)
		{
			context.Employees.Add(employee);
			context.SaveChanges();
		}
		public void Update(int id, Employee employee)
		{
			Employee existingEmployee = context.Employees.FirstOrDefault(e => e.Id == id);
			if (existingEmployee != null)
			{
				existingEmployee.FirstName = employee.FirstName;
				existingEmployee.LastName = employee.LastName;
				existingEmployee.Country = employee.Country;
				existingEmployee.City = employee.City;
				existingEmployee.Phone = employee.Phone;
				existingEmployee.Gender = employee.Gender;
				existingEmployee.Nationality = employee.Nationality;
				existingEmployee.NationalId = employee.NationalId;
				existingEmployee.Salary = employee.Salary;
				existingEmployee.HireDate = employee.HireDate;
				existingEmployee.BirthDate = employee.BirthDate;
				existingEmployee.ArrivalTime = employee.ArrivalTime;
				existingEmployee.LeaveTime = employee.LeaveTime;
				existingEmployee.Departmentid = employee.Departmentid;
				// Update any other properties that need to be changed

				context.SaveChanges();
			}
		}
		public void Delete(int id)
		{
			Employee oldEmployee = GetEmployeeById(id);
			if (oldEmployee != null)
			{
				oldEmployee.IsDelelted = true;
				//context.Employees.Remove(oldEmployee);
				context.SaveChanges();
			}
		}
		public Employee GetEmployeeByNationalId(int Id)
		{
			return context.Employees.FirstOrDefault(e => int.Parse(e.NationalId) == Id);
		}

		public double GetSalary(int? empId)
		{
			return context.Employees.Where(n => n.Id == empId).Select(n => n.Salary).FirstOrDefault();
		}

		public int? GetStartTime(int? empId)
		{
			var startTime = context.Employees.Where(n => n.Id == empId).Select(n => n.ArrivalTime).FirstOrDefault();
			return startTime?.Hours;
		}

		public int? GetLeaveTime(int? empId)
		{
			var endTime = context.Employees.Where(n => n.Id == empId).Select(n => n.LeaveTime).FirstOrDefault();
			return endTime?.Hours;
		}
	}
}
