using HrProject.Models;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HrProject.Repositories.UserRepository
{
	public interface IUserRepository
	{
		Task<IdentityResult> AddUserAsync(UserViewModel userView);
		Task<HrUser> GetByIdAsync(string id);
		Task<HrUser> GetByEmailAsync(string emial);
		Task<List<UserDataViewModel>> GetAllUsers();
		Task UpdateAsync(UserViewModel userVM);
		Task DeleteAsync(string id);
		Task<bool> CheckPasswordAsync(HrUser user, string password);
	}
}
