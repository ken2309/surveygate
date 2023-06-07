using Microsoft.EntityFrameworkCore;
using ssSystem.Interfaces;
using ssSystem.Data;
using ssSystem.Models;

namespace ssSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ssDbContext _context;

        public UserRepository(ssDbContext context)
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