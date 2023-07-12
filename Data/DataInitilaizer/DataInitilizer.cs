using HrProject.Data.Enums;
using HrProject.Global;
using HrProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HrProject.Data.DataInitilaizer
{
	public static class DataInitilizer
	{

		public static async void Configure(IApplicationBuilder app)
		{
			

			// Seed the admin user and role
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<HrUser>>();

				await roleManager.SeedAdminRoleAsync();
				await userManager.SeedAdminUserAsync(roleManager);
			}

			// Other configuration code
		}

		public static async Task SeedAdminRoleAsync(this RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
		}

		public static async Task SeedAdminUserAsync(this UserManager<HrUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			var adminUser = new HrUser
			{
				UserName = "Admin@gmail.com",
				Email = "Admin@gmail.com",
				EmailConfirmed = true
			};

			var user = await userManager.FindByEmailAsync(adminUser.Email);
			if (user == null)
			{
				await userManager.CreateAsync(adminUser, "Admin@123");
				await userManager.AddToRoleAsync(adminUser, Roles.SuperAdmin.ToString());
			}
			await roleManager.SeedClaimsToAdmin();
		}

		private static async Task SeedClaimsToAdmin(this RoleManager<IdentityRole> roleManager)
		{
			var adminRole = await roleManager.FindByNameAsync(Roles.SuperAdmin.ToString());

			// Adding Claims (Permissions) To the Admin

			await roleManager.AddPermissionClaims(adminRole, "Employee");
		}

		private static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager,IdentityRole role,string module)
		{
			var allClaims = await roleManager.GetClaimsAsync(role);
			var allPermissions = Permissions.GeneratePermissionList(module);

			foreach (var permission in allPermissions)
			{
				if (!allClaims.Any(c => c.Type == "Permission" && c.Value == permission))
				await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
			}
		}
	}
}
