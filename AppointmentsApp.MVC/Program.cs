using AppointmentsApp.Data.Data;
using AppointmentsApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
namespace AppointmentsApp.MVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<AppointmentsAppDBContext>(options =>
				options.UseSqlite(builder.Configuration.GetConnectionString("AppointmentsAppMVCContext") ?? throw new InvalidOperationException("Connection string 'AppointmentsAppMVCContext' not found.")));

			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IClientRepository, ClientRepository>();

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

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
