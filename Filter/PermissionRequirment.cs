using Microsoft.AspNetCore.Authorization;

namespace HrProject.Filter
{
	public class PermissionRequirment : IAuthorizationRequirement
	{
		public string Permission { get; private set; }
		public PermissionRequirment(string permission)
		{
			this.Permission = permission;
		}
	}
}
