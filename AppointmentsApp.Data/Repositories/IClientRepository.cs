using AppointmentsApp.Data.Models;

namespace AppointmentsApp.Data.Repositories
{
    public interface IClientRepository
    {
        Task AddClientAsync(Client client);
        bool ClientExists(Guid id);
        Task DeleteClientAsync(Client client);
        Task<List<Client>> GetAllClientsAsync();
        Task<Client> GetClientByIdAsync(Guid? id);
        Task<List<Client>> GetClientsLikeNameAsync(string name);
        Task UpdateClientAsync(Client client);
    }
}