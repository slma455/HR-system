using HrProject.Data.DataInitilaizer;
using HrProject.Filter;
using HrProject.Models;
using HrProject.Repositories.AttendanceRepository;
using HrProject.Repositories.DepartmentRepo;
using HrProject.Repositories.EmployeeRepo;
using HrProject.Repositories.GeneralSettingRepo;
using HrProject.Repositories.GroupRepo;
using HrProject.Repositories.HolidayRepo;
using HrProject.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HrProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			

			var builder = WebApplication.CreateBuilder(args);
			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<HrContext>(
				option => option.UseSqlServer(builder.Configuration.GetConnectionString("hrConnection")));
            
            builder.Services.AddIdentity<HrUser, IdentityRole>(
				option =>
				{
					option.Password.RequireNonAlphanumeric = false;
					option.Password.RequiredLength = 5;
				}).AddEntityFrameworkStores<HrContext>();

			// Authorization Services
			builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
			builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
			builder.Services.Configure<SecurityStampValidatorOptions>(options =>
			{
				options.ValidationInterval = TimeSpan.Zero;
			});



			builder.Services.AddScoped<IGroupRepository, GroupRepositroy>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IGeneralSettingRepository, GeneralSetiingRepository>();
			builder.Services.AddScoped<IWeeklyHolidayRepository, WeeklyHolidayRepository>();
			builder.Services.AddScoped<IAttendanceRepositary, AttendanceRepositary>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Login}/{id?}");

			DataInitilizer.Configure(app);

			// Handling Access Denied Page to Redirect to my custome error page
            app.UseStatusCodePagesWithReExecute("/Error/NotAllowed");
            app.Run();
		}
	}
}