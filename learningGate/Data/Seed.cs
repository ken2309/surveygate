using Microsoft.AspNetCore.Identity;
using learningGate.Models;

namespace learningGate.Data;

public class Seed
{
    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Root))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Root));
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Employee))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Employee>>();

            string adminUserEmail = "admin@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new Employee()
                {
                    UserName = "admin",
                    Email = adminUserEmail,
                    IsAdmin = true,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newAdminUser, "Admin@1234?");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                Console.Write(userManager);
            }

            string EmployeeEmail = "employee@gmail.com";

            var Employee = await userManager.FindByEmailAsync(EmployeeEmail);
            Console.WriteLine("Employee ============ ");
            if (Employee == null)
            {
                Console.WriteLine("Employee ============ after if");
                var newEmployee = new Employee()
                {
                    UserName = "Employee",
                    Email = EmployeeEmail,
                    IsAdmin = true,
                    EmailConfirmed = true
                };
                Console.WriteLine("Employee");
                Console.WriteLine(Employee);
                await userManager.CreateAsync(newEmployee, "Admin@1234?");
                await userManager.AddToRoleAsync(newEmployee, UserRoles.Employee);
            }

            string UserEmail = "user@gmail.com";

            var User = await userManager.FindByEmailAsync(UserEmail);
            if (User == null)
            {
                var newUser = new Employee()
                {
                    UserName = "app-user",
                    Email = UserEmail,
                    IsAdmin = false,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newUser, "Admin@1234?");
                await userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            string Email = "dev@gmail.com";

            var Super = await userManager.FindByEmailAsync(Email);
            if (Super == null)
            {
                var newSuper = new Employee()
                {
                    UserName = "super-admin",
                    Email = Email,
                    IsAdmin = true,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(newSuper, "SuperAdmin@1234?");
                await userManager.AddToRoleAsync(newSuper, UserRoles.Root);
            }
        }
    }

}