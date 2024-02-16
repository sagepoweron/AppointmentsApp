using AppointmentsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApp.API.Data
{
    public class AppointmentsAppAPIContext : DbContext
    {
        public AppointmentsAppAPIContext(DbContextOptions<AppointmentsAppAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; } = default!;
        public DbSet<Doctor> Doctor { get; set; } = default!;
        public DbSet<AppointmentsApp.Data.Models.Appointment> Appointment { get; set; } = default!;
    }
}
