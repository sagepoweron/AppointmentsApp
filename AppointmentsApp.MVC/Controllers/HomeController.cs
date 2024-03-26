using AppointmentsApp.Data.Data;
using AppointmentsApp.Data.Models;
using AppointmentsApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppointmentsApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly AppointmentsAppDBContext _context;

		public HomeController(ILogger<HomeController> logger, AppointmentsAppDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






		public int GetTotalAppointments()
		{
			//RAW SQL
			return _context.Client.FromSqlRaw("SELECT * FROM Appointment").Count();
		}
		public IQueryable<Appointment> GetAppointmentsBetween(DateTime start, DateTime end)
		{
			var query = from appointment in _context.Appointment.Include(a => a.Client).Include(a => a.Doctor) select appointment;

			query = query.Where(a => a.DateTime >= start && a.DateTime <= end);

			return query;
		}
		public async Task<IActionResult> Index()
		{
			var query = GetAppointmentsBetween(DateTime.Now, DateTime.Now.AddMonths(1));

            int count = query.Count();

            switch (count)
            {
                case 0:
                    ViewData["CountText"] = "There are no upcoming appointments in the next month.";
                    break;
                case 1:
                    ViewData["CountText"] = "There is 1 upcoming appointment in the next month.";
                    break;
                case > 1:
                    ViewData["CountText"] = $"There are {count} upcoming appointments in the next month.";
                    break;
            }

            return View(await query.ToListAsync());
		}
	}
}
