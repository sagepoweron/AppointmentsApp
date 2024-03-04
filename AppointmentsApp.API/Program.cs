using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppointmentsApp.API.Data;

namespace AppointmentsApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppointmentsAppAPIContext>(options =>
            //options.UseSqlite("Data Source=AppointmentsAppAPIContext-03cb93f6-490c-4a83-b4e5-33b02b068fd5.db"));
                options.UseSqlite(builder.Configuration.GetConnectionString("AppointmentsAppAPIContext") ?? throw new InvalidOperationException("Connection string 'AppointmentsAppAPIContext' not found."))); ;

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
