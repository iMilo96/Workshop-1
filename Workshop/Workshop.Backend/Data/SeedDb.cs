using Microsoft.EntityFrameworkCore;
using Workshop.Backend.UnitsOfWork.Interfaces;
using Workshop.Shared.Entities;
using Workshop.Shared.Enums;

namespace Workshop.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IUsersUnitOfWork _usersUnitOfWork;

    public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
    {
        _context = context;
        _usersUnitOfWork = usersUnitOfWork;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesAsync();
        await CheckRolesAsync();
        await CheckUserAsync("1010", "Camilo", "Gutiérrez", "camilogu13@yopmail.com", "3024353131", "Calle 55 #40-54", UserType.Admin);
    }

    private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
    {
        var user = await _usersUnitOfWork.GetUserAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                UserType = userType,
            };

            await _usersUnitOfWork.AddUserAsync(user, "camilo20041307");
            await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());
        }

        return user;
    }

    private async Task CheckRolesAsync()
    {
        await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
        await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
    }

    private async Task CheckEmployeesAsync()
    {
        if (!_context.Employees.Any())
        {
            var employeesSQLScript = File.ReadAllText("Data\\Employees.sql");
            await _context.Database.ExecuteSqlRawAsync(employeesSQLScript);
        }
    }
}