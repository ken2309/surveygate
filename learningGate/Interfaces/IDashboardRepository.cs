using ssSystem.Models;

namespace ssSystem.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Product>> GetAllUserRaces();
        Task<List<Customer>> GetAllUserClubs();
        Task<Employee> GetUserById(string id);
        Task<Employee> GetByIdNoTracking(string id);
        bool Update(Employee user);
        bool Save();
    }
}
