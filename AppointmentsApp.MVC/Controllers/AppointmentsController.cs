﻿using AppointmentsApp.Data.Data;
using AppointmentsApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApp.MVC.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppointmentsAppDBContext _context;

        public AppointmentsController(AppointmentsAppDBContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index(string client_name, string doctor_name)
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

            return View(await appointments.ToListAsync());
        }



        // GET: Appointments
        //public async Task<IActionResult> Index()
        //{
        //    var appointmentsAppMVCContext = _context.Appointment.Include(a => a.Client).Include(a => a.Doctor);
        //    return View(await appointmentsAppMVCContext.ToListAsync());
        //}

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Client)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateTime,ClientId,DoctorId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.Id = Guid.NewGuid();
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", appointment.ClientId);
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name", appointment.DoctorId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", appointment.ClientId);
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name", appointment.DoctorId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateTime,ClientId,DoctorId")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", appointment.ClientId);
            ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name", appointment.DoctorId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Client)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(Guid id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }


        //private readonly AppointmentRepository _repository;

        //public AppointmentsController(AppointmentRepository repository)
        //{
        //    _repository = repository;
        //}

        //// GET: Appointments
        //public async Task<IActionResult> Index(string client_name, string doctor_name)
        //{
        //    return View(await _repository.GetAllLikeName(client_name, doctor_name));
        //}

        //// GET: Appointments/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var appointment = await _repository.GetByIdAsync(id);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(appointment);
        //}

        //// GET: Appointments/Create
        //public IActionResult Create()
        //{
        //    ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
        //    ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name");
        //    return View();
        //}

        //// POST: Appointments/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,DateTime,ClientId,DoctorId")] Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        appointment.Id = Guid.NewGuid();
        //        _repository.Add(appointment);
        //        await _repository.SaveAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", appointment.ClientId);
        //    ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name", appointment.DoctorId);
        //    return View(appointment);
        //}

        //// GET: Appointments/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var appointment = await _repository.GetByIdAsync(id);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", appointment.ClientId);
        //    ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name", appointment.DoctorId);
        //    return View(appointment);
        //}

        //// POST: Appointments/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateTime,ClientId,DoctorId")] Appointment appointment)
        //{
        //    if (id != appointment.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(appointment);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AppointmentExists(appointment.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", appointment.ClientId);
        //    ViewData["DoctorId"] = new SelectList(_context.Doctor, "Id", "Name", appointment.DoctorId);
        //    return View(appointment);
        //}

        //// GET: Appointments/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var appointment = await _context.Appointment
        //        .Include(a => a.Client)
        //        .Include(a => a.Doctor)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(appointment);
        //}

        //// POST: Appointments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var appointment = await _context.Appointment.FindAsync(id);
        //    if (appointment != null)
        //    {
        //        _context.Appointment.Remove(appointment);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AppointmentExists(Guid id)
        //{
        //    return _context.Appointment.Any(e => e.Id == id);
        //}
    }
}
