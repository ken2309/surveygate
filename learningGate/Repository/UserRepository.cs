using Microsoft.EntityFrameworkCore;
using learningGate.Interfaces;
using learningGate.Data;
using learningGate.Models;
using Microsoft.AspNetCore.Identity;

namespace learningGate.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly learningGateDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Employee> _userManager;

        public UserRepository(learningGateDbContext context, IHttpContextAccessor httpContextAccessor,UserManager<Employee> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
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
        public async Task<IEnumerable<Order>> UserOrders()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            var orders = await _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductType)
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Images)
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return orders;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}