using learningGate.Models;

namespace learningGate.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Employee>> GetAllUsers();
        Task<Employee> GetUserById(string id);
        bool Add(Employee user);
        bool Update(Employee user);
        bool Delete(Employee user);
        bool Save();
        Task<IEnumerable<Order>> UserOrders();

    }
}
