using AppointmentsApp.Data.Models;
using AppointmentsApp.Data.Repositories;
using AppointmentsApp.Data.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApp.MVC.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _client_repository;

        public ClientsController(IClientRepository client_repository)
        {
            _client_repository = client_repository;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return View(await _client_repository.GetClientsLikeNameAsync(name));
            }

            return View(await _client_repository.GetAllClientsAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _client_repository.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email")] Client client)
        {
            var validator = new PersonValidator();
            if (!validator.Validate(client).IsValid)
            {
                return View(client);
            }

            if (ModelState.IsValid)
            {
                client.Id = Guid.NewGuid();
                await _client_repository.AddClientAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _client_repository.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Phone,Email")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _client_repository.UpdateClientAsync(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_client_repository.ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _client_repository.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = await _client_repository.GetClientByIdAsync(id);
            if (client != null)
            {
                await _client_repository.DeleteClientAsync(client);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
