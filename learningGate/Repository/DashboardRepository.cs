using Microsoft.EntityFrameworkCore;
using ssSystem.Interfaces;
using ssSystem.Data;
using ssSystem.Models;
using ssSystem.Data;

namespace ssSystem.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ssDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ssDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<List<Product>> GetAllUserRaces()
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> GetAllUserClubs()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        Task<Employee> IDashboardRepository.GetByIdNoTracking(string id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Employee user)
        {
            throw new NotImplementedException();
        }

        Task<Employee> IDashboardRepository.GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
