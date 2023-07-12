using HrProject.Models;

namespace HrProject.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int? id);
        List<Employee> GetEmployeeByName(string name);
        void Insert(Employee employee);
        void Update(int id, Employee employee);
        void Delete(int id);
        Employee GetEmployeeByNationalId(int Id);
        double GetSalary(int? id);
        int? GetStartTime(int? empId);
        int? GetLeaveTime(int? empId);
    }
}
