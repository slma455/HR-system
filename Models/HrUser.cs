using Microsoft.AspNetCore.Identity;

namespace HrProject.Models
{
	public class HrUser : IdentityUser
	{
        public string? Name { get; set; }
    }
}
