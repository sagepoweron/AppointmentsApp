using AppointmentsApp.Data.Data;
using AppointmentsApp.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApp.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppointmentsAppDBContext _context;

        public DoctorRepository(AppointmentsAppDBContext context)
        {
            _context = context;
        }

        public bool Exists(Guid? id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }


        public async Task<Doctor?> GetByIdAsync(Guid? id)
        {
            return await _context.Doctor.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctor.ToListAsync();
        }
        public async Task<List<Doctor>> GetLikeNameAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                //RAW SQL
                var parameter = new SqliteParameter("comparison", $"%{name}%");
                return await _context.Doctor.FromSqlRaw("SELECT * FROM Doctor WHERE name LIKE @comparison", parameter).ToListAsync();
            }

            var doctors = from doctor in _context.Doctor select doctor;
            return await doctors.ToListAsync();
        }



        public void Add(Doctor doctor)
        {
            _context.Doctor.Add(doctor);
        }
        public void Update(Doctor doctor)
        {
            _context.Update(doctor);
        }
        public void Delete(Doctor doctor)
        {
            _context.Remove(doctor);
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctor.ToList();
        }

        public Doctor? GetById(Guid? id)
        {
            return _context.Doctor.FirstOrDefault(x => x.Id == id);
        }

        public List<Doctor> GetLikeName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                //RAW SQL
                var parameter = new SqliteParameter("comparison", $"%{name}%");
                return _context.Doctor.FromSqlRaw("SELECT * FROM Doctor WHERE name LIKE @comparison", parameter).ToList();
            }

            var doctors = from doctor in _context.Doctor select doctor;
            return doctors.ToList();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
