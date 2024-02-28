﻿namespace AppointmentsApp.Data.Models
{
    public abstract class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
