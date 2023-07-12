using HrProject.Global;
using HrProject.Repositories.EmployeeRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using HrProject.Repositories.DepartmentRepo;
using HrProject.ViewModels;

namespace HrProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;


        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;   
        }
        // GET: EmployeeController
        [Authorize(Permissions.Employee.View)]
        public ActionResult Index()
        {
            var departments = _departmentRepository.GetAllDepartments();
            ViewBag.DepartmentList = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.Id.ToString()
            });

            var employees = _employeeRepository.GetAllEmployees();
            return View(employees);
        }

		// GET: EmployeeController/Details/5
		[Authorize(Permissions.Employee.View)]
		public ActionResult Details(int id)
        {
            var emp = _employeeRepository.GetEmployeeById(id);
            return View(emp);
        }

        // GET: EmployeeController/Create
        //[Authorize(Permissions.Employee.Add)]
        [Authorize(Permissions.Employee.Add)]
        public ActionResult Create()
        {
            var departments = _departmentRepository.GetAllDepartments();
            ViewBag.DepartmentList = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.Id.ToString()
            });
            ViewBag.Cities = new List<string> { "Cairo", "Alexandria", "Giza", "Tanta", "Damanhour", "Menoufia", "Mansoura", "Qena", "Luxor", "Aswan", "Other"};

            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Permissions.Employee.Add)]
		public ActionResult Create(IFormCollection collection)
        {
            var newEmp = new Models.Employee
            {
                FirstName = collection.FirstOrDefault(x => x.Key == "FirstName").Value,
                LastName = collection.FirstOrDefault(x => x.Key == "LastName").Value,
                Country = collection.FirstOrDefault(x => x.Key == "Country").Value,
                City = collection.FirstOrDefault(x => x.Key == "City").Value,
                Phone = collection.FirstOrDefault(x => x.Key == "Phone").Value,
                Gender = collection.FirstOrDefault(x => x.Key == "Gender").Value,
                Nationality = collection.FirstOrDefault(x => x.Key == "Nationality").Value,
                NationalId = collection.FirstOrDefault(x => x.Key == "NationalId").Value,
                Salary = Convert.ToInt32(collection.FirstOrDefault(x => x.Key == "Salary").Value),
                HireDate = Convert.ToDateTime(collection.FirstOrDefault(x => x.Key =="HireDate").Value),
                BirthDate = Convert.ToDateTime(collection.FirstOrDefault(x => x.Key =="BirthDate").Value),
                ArrivalTime = TimeSpan.Parse(collection.FirstOrDefault(x => x.Key == "ArrivalTime").Value),
                LeaveTime = TimeSpan.Parse(collection.FirstOrDefault(x => x.Key == "LeaveTime").Value),
                Departmentid = Convert.ToInt32(collection.FirstOrDefault(x => x.Key == "DepartmentList").Value),
            };
            _employeeRepository.Insert(newEmp);

            return RedirectToAction("Index");
        }

        // GET: EmployeeController/Edit/5
        [Authorize(Permissions.Employee.Edit)]
        public ActionResult Edit(int id)
        {
            var emp = _employeeRepository.GetEmployeeById(id);
            var departments = _departmentRepository.GetAllDepartments();
            ViewBag.DepartmentList = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.Id.ToString(),
                Selected = x.Id == emp.Departmentid
            });
            var empVM = new EmployeeViewModel() {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Country = emp.Country,
                City = emp.City,
                Phone = emp.Phone,
                Gender = emp.Gender,
                Nationality = emp.Nationality,
                NationalId = emp.NationalId,
                Salary = emp.Salary,
                HireDate = emp.HireDate,
                BirthDate = emp.BirthDate,
                ArrivalTime = emp.ArrivalTime,
                LeaveTime = emp.LeaveTime,
            };
            return View(empVM);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Permissions.Employee.Edit)]
		public ActionResult Edit(int id, IFormCollection collection)
        {
            var newEmp = new Models.Employee
            {
                FirstName = collection.FirstOrDefault(x => x.Key == "FirstName").Value,
                LastName = collection.FirstOrDefault(x => x.Key == "LastName").Value,
                Country = collection.FirstOrDefault(x => x.Key == "Country").Value,
                City = collection.FirstOrDefault(x => x.Key == "City").Value,
                Phone = collection.FirstOrDefault(x => x.Key == "Phone").Value,
                Gender = collection.FirstOrDefault(x => x.Key == "Gender").Value,
                Nationality = collection.FirstOrDefault(x => x.Key == "Nationality").Value,
                NationalId = collection.FirstOrDefault(x => x.Key == "NationalId").Value,
                Salary = Convert.ToInt32(collection.FirstOrDefault(x => x.Key == "Salary").Value),
                HireDate = Convert.ToDateTime(collection.FirstOrDefault(x => x.Key == "HireDate").Value),
                BirthDate = Convert.ToDateTime(collection.FirstOrDefault(x => x.Key == "BirthDate").Value),
                ArrivalTime = TimeSpan.Parse(collection.FirstOrDefault(x => x.Key == "ArrivalTime").Value),
                LeaveTime = TimeSpan.Parse(collection.FirstOrDefault(x => x.Key == "LeaveTime").Value),
                Departmentid = Convert.ToInt32(collection.FirstOrDefault(x => x.Key == "DepartmentList").Value),
            };
            _employeeRepository.Update(id, newEmp);
            return RedirectToAction("Index");
        }

        // GET: EmployeeController/Delete/5
        //[Authorize(Permissions.Employee.Delete)]
        //public ActionResult Delete(int id)
        //{
        //    var emp = _employeeRepository.GetEmployeeById(id);
        //    var departments = _departmentRepository.GetAllDepartments();
        //    ViewBag.DepartmentList = departments.Select(x => new SelectListItem
        //    {
        //        Text = x.DeptName,
        //        Value = x.Id.ToString(),
        //        Selected = x.Id == emp.Departmentid
        //    });
        //    return View(emp);
        //}

        // POST: EmployeeController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
		[Authorize(Permissions.Employee.Delete)]
		public ActionResult Delete(int id)
        {
            _employeeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
