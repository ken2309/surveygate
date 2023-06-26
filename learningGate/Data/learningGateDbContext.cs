using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using learningGate.Models;

namespace learningGate.Data;

public class learningGateDbContext : IdentityDbContext<Employee>
{
    public string ConnectionString { get; set; }

    public learningGateDbContext(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    public learningGateDbContext(DbContextOptions<learningGateDbContext> options)
        : base(options)
    {
    }

    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
    public DbSet<Student> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Producttype> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<OrderStatus> OrderStatus { get; set; }

    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<FavoriteDetail> FavoriteDetails { get; set; }
    public DbSet<FavoriteCart> FavoriteCarts { get; set; }
}