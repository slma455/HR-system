using HrProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HrContext context;
        public DepartmentRepository(HrContext context)
        {
            this.context = context;
        }

        public List<Department> GetAllDepartments()
        {
            return context.Departments.Where(n => n.IsDeleted == false).ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return context.Departments.FirstOrDefault(dep => dep.Id == id);
        }
        public Department GetDepartmentByName(string name)
        {
            return context.Departments.Include(dep => dep.DeptName).FirstOrDefault(dep => dep.DeptName == name);

        }

        public void Insert(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }
        public void Update(int id, Department department)
        {
            Department existingDepartment = context.Departments.FirstOrDefault(d => d.Id == id);
            if (existingDepartment != null)
            {
                existingDepartment.DeptName = department.DeptName;
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            Department oldDepartment = GetDepartmentById(id);
            if (oldDepartment != null)
            {
                oldDepartment.IsDeleted = true;
                context.SaveChanges();
            }
        }
    }
}
