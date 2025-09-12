using Workshop.Shared.Entities;

namespace Workshop.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;

    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckEmployeesAsync();
    }

    private async Task CheckEmployeesAsync()
    {
        if (!_context.Employees.Any())
        {
            _context.Employees.Add(new Employee
            {
                FirstName = "Camilo",
                LastName = "Gutierrez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 1000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Santiago",
                LastName = "Maya",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 2000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Juan David",
                LastName = "Villarreal",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 1000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Tomas",
                LastName = "Hernandez",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 1100000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Estefania",
                LastName = "Jimenez",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 3000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Sofia",
                LastName = "Zapata",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 1000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Angelly",
                LastName = "Yepes",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 4000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Leticia",
                LastName = "Barrientos",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 1000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Diego Leon",
                LastName = "Gil",
                IsActive = true,
                HireDate = DateTime.UtcNow,
                Salary = 4000000
            });

            _context.Employees.Add(new Employee
            {
                FirstName = "Martha",
                LastName = "Barrientos",
                IsActive = false,
                HireDate = DateTime.UtcNow,
                Salary = 2000000
            });

            await _context.SaveChangesAsync();
        }
    }
}