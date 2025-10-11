using Microsoft.EntityFrameworkCore;
using Workshop.Backend.Data;
using Workshop.Backend.Helpers;
using Workshop.Backend.Repositories.Interfaces;
using Workshop.Shared.DTOs;
using Workshop.Shared.Entities;
using Workshop.Shared.Responses;

namespace Workshop.Backend.Repositories.Implementations;

public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
{
    private readonly DataContext _context;

    public EmployeesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            var filter = pagination.Filter.ToLower().Trim();
            queryable = queryable.Where(x =>
                (x.FirstName + " " + x.LastName).ToLower().Contains(filter) ||
                x.FirstName.ToLower().Contains(filter) ||
                x.LastName.ToLower().Contains(filter)
            );
        }

        var employees = await queryable
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .Paginate(pagination)
            .ToListAsync();

        return new ActionResponse<IEnumerable<Employee>>
        {
            WasSuccess = true,
            Result = employees
        };
    }

    public override async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            var filter = pagination.Filter.ToLower().Trim();
            queryable = queryable.Where(x =>
                x.FirstName.ToLower().Contains(filter) ||
                x.LastName.ToLower().Contains(filter)
            );
        }

        var count = await queryable.CountAsync();

        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = count
        };
    }

    public override async Task<ActionResponse<IEnumerable<Employee>>> GetAsync()
    {
        var employees = await _context.Employees
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .ToListAsync();

        return new ActionResponse<IEnumerable<Employee>>
        {
            WasSuccess = true,
            Result = employees
        };
    }

    public override async Task<ActionResponse<Employee>> GetAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (employee == null)
        {
            return new ActionResponse<Employee>
            {
                Message = "Empleado no encontrado."
            };
        }

        return new ActionResponse<Employee>
        {
            WasSuccess = true,
            Result = employee
        };
    }
}