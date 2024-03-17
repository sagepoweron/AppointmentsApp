namespace AppointmentsApp.Data.Repositories
{
    public interface IPersonRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        bool Exists(Guid? id);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        T? GetById(Guid? id);
        Task<T?> GetByIdAsync(Guid? id);
        List<T> GetLikeName(string name);
        Task<List<T>> GetLikeNameAsync(string name);
        Task SaveAsync();
        void Update(T entity);
    }
}