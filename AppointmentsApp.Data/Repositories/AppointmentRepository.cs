using AppointmentsApp.Data.Data;
using AppointmentsApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApp.Data.Repositories
{
    /// <summary>
    /// not used in project
    /// </summary>
    public class AppointmentRepository
    {
        private readonly AppointmentsAppDBContext _context;

        public AppointmentRepository(AppointmentsAppDBContext context)
        {
            _context = context;
        }

        public bool Exists(Guid? id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }



        public async Task<Appointment?> GetByIdAsync(Guid? id)
        {
            return await _context.Appointment
                .Include(a => a.Client)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);

        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointment.ToListAsync();
        }

        public async Task<List<Appointment>> GetAllLikeName(string client_name, string doctor_name)
        {
            var appointments = from appointment in _context.Appointment.Include(a => a.Client).Include(a => a.Doctor) select appointment;

            if (!string.IsNullOrEmpty(client_name))
            {
                appointments = appointments.Where(s => s.Client.Name.ToLower().Contains(client_name.ToLower()));
            }

            if (!string.IsNullOrEmpty(doctor_name))
            {
                appointments = appointments.Where(s => s.Doctor.Name.ToLower().Contains(doctor_name.ToLower()));
            }

            return await appointments.ToListAsync();
        }

        public void Add(Appointment entity)
        {
            _context.Appointment.Add(entity);
        }
        public void Update(Appointment entity)
        {
            _context.Update(entity);
        }
        public void Delete(Appointment entity)
        {
            _context.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
