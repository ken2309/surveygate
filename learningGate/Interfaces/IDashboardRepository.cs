using learningGate.Models;

namespace learningGate.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Product>> GetAllUserRaces();
        Task<List<Student>> GetAllUserClubs();
        Task<Employee> GetUserById(string id);
        Task<Employee> GetByIdNoTracking(string id);
        bool Update(Employee user);
        bool Save();
    }
}
