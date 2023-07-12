using HrProject.Models;

namespace HrProject.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        Department GetDepartmentById(int id);
        public List<Department> GetAllDepartments();
        Department GetDepartmentByName(string name);
        void Insert(Department department);
        void Update(int id, Department department);
        void Delete(int id);
    }
}
