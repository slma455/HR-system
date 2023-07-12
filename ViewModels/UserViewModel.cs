using System.ComponentModel.DataAnnotations;

namespace HrProject.ViewModels
{
	public class UserViewModel
	{
        public string? Id { get; set; }

        [MinLength(6,ErrorMessage ="Enter Full Name")]
        public string Name { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(6)]
        [DataType(DataType.Password,ErrorMessage ="Password must be at least 6 characters with at one upperCase Letter and one number")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The Confirm Password must Match the Password")]
        public string? ConfirmPassword { get; set; }
        public string GroupName { get; set; }
    }
}
