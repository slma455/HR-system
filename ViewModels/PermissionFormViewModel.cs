using System.ComponentModel.DataAnnotations;

namespace HrProject.ViewModels
{
	public class PermissionFormViewModel
	{
        public string? RoleId { get; set; }
        [Required(ErrorMessage ="Group Name is Required")]
        public string RoleName { get; set; }
        public List<CheckBoxViewModel> RoleClaims { get; set; }
    }
}
