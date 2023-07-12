using HrProject.Models;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.UserRepository
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<HrUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly HrContext context;

		public UserRepository(UserManager<HrUser> userManager, RoleManager<IdentityRole> roleManager, HrContext context)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.context = context;
		}
		public async Task<IdentityResult> AddUserAsync(UserViewModel userView)
		{
			HrUser user = new HrUser()
			{
				UserName = userView.UserName,
				Email = userView.Email,
				Name = userView.Name,
			};
			return await userManager.CreateAsync(user, userView.Password);
		}

		public async Task<bool> CheckPasswordAsync(HrUser user, string password)
		{
			return await userManager.CheckPasswordAsync(user,password);
		}

		public async Task DeleteAsync(string id)
        {
			var user = await context.Users.FindAsync(id);
			context.Users.Remove(user);
			context.SaveChanges();
        }

        public async Task<List<UserDataViewModel>> GetAllUsers()
		{
			string groupName;
			var users = await context.Users.ToListAsync();
			List<UserDataViewModel> usersModel = new List<UserDataViewModel>();
			foreach (var user in users)
			{
				var roles = await userManager.GetRolesAsync(user);
				if (roles.Count != 0)
					groupName = roles[0];
				else
					groupName = "";
				usersModel.Add(new UserDataViewModel { Id = user.Id, Name = user.Name, Email = user.Email, GroupName = groupName });
			}
			return usersModel;
		}

		public async Task<HrUser> GetByEmailAsync(string email)
		{
			return await userManager.FindByEmailAsync(email);
		}

		public async Task<HrUser> GetByIdAsync(string id)
		{
			return await context.Users.FindAsync(id);
		}

        public async Task UpdateAsync(UserViewModel userVM)
        {
			var userDB =await context.Users.FindAsync(userVM.Id);
			userDB.Name = userVM.Name;
			userDB.Email = userVM.Email;
			userDB.UserName = userVM.UserName;
			context.SaveChanges();
        }
    }
}
