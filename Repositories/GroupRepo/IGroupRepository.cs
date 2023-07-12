using Microsoft.AspNetCore.Identity;

namespace HrProject.Repositories.GroupRepo
{
	public interface IGroupRepository
	{
		Task<List<IdentityRole>> GetRolesAsync();
		void Delete(IdentityRole role);
	}
}
