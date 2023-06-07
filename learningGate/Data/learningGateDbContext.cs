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
}