using HrProject.Models;
using HrProject.Validation;
using System.ComponentModel.DataAnnotations;

namespace HrProject.ViewModels
{
    public class DepartmentViewModel
    {
        [MinLength(3)]
        public string DeptName { get; set; }

    }
}
    

