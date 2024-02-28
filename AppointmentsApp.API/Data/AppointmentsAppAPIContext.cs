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
        public DbSet<Appointment> Appointment { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Client client1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Client1"
            };

            Client client2 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Client2"
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

            //Appointment appointment1 = new()
            //{
            //    Id = Guid.NewGuid(),
            //    ClientId = client1.Id,
            //    DoctorId = doctor1.Id,
            //    DateTime = DateTime.Now.AddDays(1)
            //};

            //modelBuilder.Entity<Appointment>().HasData(appointment1);

            base.OnModelCreating(modelBuilder);
        }
    }
}
