using ssSystem.Models;

namespace ssSystem.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Employee>> GetAllUsers();
        Task<Employee> GetUserById(string id);
        bool Add(Employee user);
        bool Update(Employee user);
        bool Delete(Employee user);
        bool Save();
    }
}
