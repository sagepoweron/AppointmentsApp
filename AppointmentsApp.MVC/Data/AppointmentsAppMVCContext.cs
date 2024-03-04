﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppointmentsApp.Data.Models;

namespace AppointmentsApp.MVC.Data
{
    public class AppointmentsAppMVCContext : DbContext
    {
        public AppointmentsAppMVCContext (DbContextOptions<AppointmentsAppMVCContext> options)
            : base(options)
        {
        }

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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Doctor> Doctor { get; set; } = default!;
    }
}