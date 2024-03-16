using AppointmentsApp.Data.Data;
using AppointmentsApp.Data.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AppointmentsApp.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppointmentsAppDBContext _context;

        public ClientRepository(AppointmentsAppDBContext context)
        {
            _context = context;
        }

        public async Task AddClientAsync(Client client)
        {
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateClientAsync(Client client)
        {
            _context.Update(client);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteClientAsync(Client client)
        {
            _context.Remove(client);
            await _context.SaveChangesAsync();
        }

        public bool ClientExists(Guid id)
        {
            return _context.Client.Any(e => e.Id == id);
        }


        public async Task<Client> GetClientByIdAsync(Guid? id)
        {
            return await _context.Client.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _context.Client.ToListAsync();
        }
        public async Task<List<Client>> GetClientsLikeNameAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                //RAW SQL
                var parameter = new SqliteParameter("comparison", $"%{name}%");
                return await _context.Client.FromSqlRaw("SELECT * FROM Client WHERE name LIKE @comparison", parameter).ToListAsync();
            }

            var clients = from client in _context.Client select client;
            return await clients.ToListAsync();
        }
    }
}
