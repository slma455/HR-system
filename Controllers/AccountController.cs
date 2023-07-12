using HrProject.Models;
using HrProject.Repositories.UserRepository;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HrProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserRepository userRepository;
		private readonly SignInManager<HrUser> signInManager;

		public AccountController(IUserRepository userRepository, SignInManager<HrUser> signInManager)
		{
			this.userRepository = userRepository;
			this.signInManager = signInManager;
		}
		
		[AllowAnonymous]
		public async Task<IActionResult> Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			var user = await userRepository.GetByEmailAsync(model.Email);
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			if (user == null)
			{
				ModelState.AddModelError("", "This Email Not Found");
			}
			else if (await userRepository.CheckPasswordAsync(user, model.Password))
			{
				await signInManager.SignInAsync(user, model.RememberMe);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ModelState.AddModelError("Password", "InValid Password");
				return View(model);
			}
			return RedirectToAction("Index", "Home");

		}

		[AllowAnonymous]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}

	}
}
