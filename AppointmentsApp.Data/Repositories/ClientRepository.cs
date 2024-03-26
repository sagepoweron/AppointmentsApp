﻿using AppointmentsApp.Data.Data;
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

        public bool Exists(Guid? id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Client?> GetByIdAsync(Guid? id)
        {
            return await _context.Client.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Client.ToListAsync();
        }


        public void Add(Client client)
        {
            _context.Client.Add(client);
        }
        public void Update(Client client)
        {
            _context.Update(client);
        }
        public void Delete(Client client)
        {
            _context.Remove(client);
        }

        public List<Client> GetAll()
        {
            return _context.Client.ToList();
        }

        public Client? GetById(Guid? id)
        {
            return _context.Client.FirstOrDefault(x => x.Id == id);
        }


        public int GetTotal()
        {
            //RAW SQL
            return _context.Client.FromSqlRaw("SELECT * FROM Client").Count();
        }

        public IQueryable<Client> QueryLikeName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                //RAW SQL
                var parameter = new SqliteParameter("comparison", $"%{name}%");
                return _context.Client.FromSqlRaw("SELECT * FROM Client WHERE name LIKE @comparison", parameter);
            }

            var clients = from client in _context.Client select client;
            return clients;
        }
    }
}
