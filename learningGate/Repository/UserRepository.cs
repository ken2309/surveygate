using Microsoft.EntityFrameworkCore;
using learningGate.Interfaces;
using learningGate.Data;
using learningGate.Models;

namespace learningGate.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly learningGateDbContext _context;

        public UserRepository(learningGateDbContext context)
        {
            _context = context;
        }

        public bool Add(Employee user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Employee user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Employee> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Employee user)
        {
            _context.Update(user);
            return Save();
        }
    }
}