namespace AppointmentsApp.API.Models
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateTime { get; set; }
        public Client Client { get; set; }
        public Doctor Doctor { get; set; }

    }
}
