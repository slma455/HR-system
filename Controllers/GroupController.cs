using HrProject.Global;
using HrProject.Repositories.GroupRepo;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HrProject.Controllers
{
	public class GroupController : Controller
	{
		private readonly IGroupRepository groupRepository;
		private readonly RoleManager<IdentityRole> roleManager;

		public GroupController(IGroupRepository groupRepository, RoleManager<IdentityRole> roleManager)
		{
			this.groupRepository = groupRepository;
			this.roleManager = roleManager;
		}


		[HttpGet]
		[Authorize(Permissions.Permission.View)]
		public async Task<IActionResult> Index()
		{
			var allRoles = await groupRepository.GetRolesAsync();
			return View(allRoles);
		}


		[HttpGet]
		[Authorize(Permissions.Permission.Add)]
		public IActionResult AddGroup()
		{
			var allClaims = Permissions.GenerateAllPermissions();
			var allPermissions = allClaims.Select(c => new CheckBoxViewModel { DisplayValue = c }).ToList();
			var viewModel = new PermissionFormViewModel
			{
				RoleId = "",
				RoleName = "",
				RoleClaims = allPermissions
			};
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Permissions.Permission.Add)]
		public async Task<IActionResult> AddGroup(PermissionFormViewModel model)
		{
			int counter = 0;
			foreach (var item in model.RoleClaims)
			{
				if (!item.IsSeleced)
					counter++;
			}
			if (counter == 24)
				ModelState.AddModelError("RoleClaims", "Please Select the Permissions");
			if (!ModelState.IsValid)
				return View(model);
			if (await roleManager.RoleExistsAsync(model.RoleName))
			{
				ModelState.AddModelError("RoleName", "This Group Exists!!");
				return View(model);
			}
			await roleManager.CreateAsync(new IdentityRole { Name = model.RoleName.Trim() });
			var newGroup = await roleManager.FindByNameAsync(model.RoleName);
			var selectedClaims = model.RoleClaims.Where(c => c.IsSeleced).ToList();
			foreach (var claim in selectedClaims)
			{
				await roleManager.AddClaimAsync(newGroup, new Claim("Permission", claim.DisplayValue));
			}
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		[Authorize(Permissions.Permission.Edit)]
		public async Task<IActionResult> Edit(string groupId)
		{
			var role = await roleManager.FindByIdAsync(groupId);
			if (role == null)
				return NotFound();
			var allClaims = Permissions.GenerateAllPermissions();
			var allPermissions = allClaims.Select(c => new CheckBoxViewModel { DisplayValue = c }).ToList();
			var roleClaim = roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();
			foreach (var permission in allPermissions)
			{
				if (roleClaim.Any(c => c == permission.DisplayValue))
					permission.IsSeleced = true;
			}
			var viewModel = new PermissionFormViewModel
			{
				RoleId = groupId,
				RoleName = role.Name,
				RoleClaims = allPermissions
			};

			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Permissions.Permission.Edit)]
		public async Task<IActionResult> Edit(PermissionFormViewModel model)
		{
			int counter = 0;
			foreach (var item in model.RoleClaims)
			{
				if (!item.IsSeleced)
					counter++;
			}

			if (counter == 24)
				ModelState.AddModelError("RoleClaims", "Please Select the Permissions");

			if (!ModelState.IsValid)
				return View(model);
			var groupRole = await roleManager.FindByIdAsync(model.RoleId);
			if (groupRole == null)
				return NotFound();
			var groupClaims = await roleManager.GetClaimsAsync(groupRole);
			foreach (var claim in groupClaims)
				await roleManager.RemoveClaimAsync(groupRole, claim);
			var selectedPermission = model.RoleClaims.Where(c => c.IsSeleced).ToList();
			foreach (var permission in selectedPermission)
			{
				await roleManager.AddClaimAsync(groupRole, new Claim("Permission", permission.DisplayValue));
			}
			groupRole.Name = model.RoleName;
			await roleManager.UpdateAsync(groupRole);
			return RedirectToAction("Index");
		}

		[Authorize(Permissions.Permission.Delete)]
		public async Task<IActionResult> Delete(string groupId)
		{
			var role = await roleManager.FindByIdAsync(groupId);
			if (role == null)
				return NotFound();
			groupRepository.Delete(role);
			return RedirectToAction("Index");
		}

	}
}
