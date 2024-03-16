using AppointmentsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApp.Data.Data
{
    public class AppointmentsAppDBContext : DbContext
    {
        public AppointmentsAppDBContext(DbContextOptions<AppointmentsAppDBContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Client client1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Bill",
                Phone = "555-555-1000",
                Email = "bill@gmail.com"
            };

            Client client2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Jeff",
                Phone = "555-555-2000",
                Email = "jeff@outlook.com"
            };

            modelBuilder.Entity<Client>().HasData(client1, client2);

            Doctor doctor1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Doctor1"
            };

            Doctor doctor2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Doctor2"
            };

            modelBuilder.Entity<Doctor>().HasData(doctor1, doctor2);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Doctor> Doctor { get; set; } = default!;
        public DbSet<Client> Client { get; set; } = default!;
        public DbSet<Appointment> Appointment { get; set; } = default!;
    }
}
