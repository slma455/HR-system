using HrProject.Global;
using HrProject.Models;
using HrProject.Repositories.GroupRepo;
using HrProject.Repositories.UserRepository;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IGroupRepository groupRepository;
        private readonly UserManager<HrUser> userManager;
        private readonly IUserRepository userRepository;

        public UserController(IGroupRepository groupRepository,
            UserManager<HrUser> userManager,
            IUserRepository userRepository)
        {
            this.groupRepository = groupRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(Permissions.Permission.View)]
        public async Task<IActionResult> Index()
        {
            var allUsers = await userRepository.GetAllUsers();
            return View(allUsers);
        }

		[Authorize(Permissions.Permission.Add)]
		public async Task<IActionResult> AddUser()
        {
            var groupRoles = await groupRepository.GetRolesAsync();
            ViewData["groupRoles"] = groupRoles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permission.Add)]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var groupRoles = await groupRepository.GetRolesAsync();
                ViewData["groupRoles"] = groupRoles;
                return View(model);
            }
            else if (await userRepository.GetByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Emial", "This Email is Already Used!!");
                var groupRoles = await groupRepository.GetRolesAsync();
                ViewData["groupRoles"] = groupRoles;
                return View(model);
            }
            var result = await userRepository.AddUserAsync(model);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            var addUserToRole = await userRepository.GetByEmailAsync(model.Email);
            await userManager.AddToRoleAsync(addUserToRole, model.GroupName);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Permission.Edit)]
        public async Task<IActionResult> Edit(string id)
        {
            var groupRoles = await groupRepository.GetRolesAsync();
            ViewData["groupRoles"] = groupRoles;
            string groupName;
            var user = await userRepository.GetByIdAsync(id);
            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles != null)
            {
                groupName = userRoles[0];
            }
            else
                groupName = "";
            UserViewModel userVM = new UserViewModel
            {
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash,
                ConfirmPassword = user.PasswordHash,
                GroupName = groupName
            };
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Permission.Edit)]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var groupRoles = await groupRepository.GetRolesAsync();
            ViewData["groupRoles"] = groupRoles;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var userdb = await userRepository.GetByEmailAsync(model.Email);
                if(userdb != null && userdb.Id != model.Id)
                {
                    ModelState.AddModelError("Email", "This Email is Already Used!!");
                    return View(model);
                }
            }
            await userRepository.UpdateAsync(model);
            var user = await userRepository.GetByIdAsync(model.Id);
            var userRoles =await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);
            await userManager.AddToRoleAsync(user, model.GroupName);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Permissions.Permission.Delete)]
        public async Task<IActionResult> Delete(string id)
        {
            await userRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
