using HrProject.Models;
using Microsoft.AspNetCore.Identity;

namespace HrProject.Repositories.GroupRepo
{
	public class GroupRepositroy : IGroupRepository
	{
		private readonly HrContext context;
		private readonly RoleManager<IdentityRole> roleManager;

		public GroupRepositroy(HrContext context,RoleManager<IdentityRole> roleManager)
        {
			this.context = context;
			this.roleManager = roleManager;
		}
        public void Delete(IdentityRole role)
		{
			context.Roles.Remove(role);
			context.SaveChanges();
		}

		public async Task<List<IdentityRole>> GetRolesAsync()
		{
			var allRoles =  context.Roles.ToList();
			return allRoles;
		}
	}
}
