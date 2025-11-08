using System.Diagnostics.Metrics;
using Workshop.Shared.DTOs;
using Workshop.Shared.Entities;
using Workshop.Shared.Responses;

namespace Workshop.Backend.UnitsOfWork.Interfaces;

public interface IEmployeesUnitOfWork
{
    Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Employee>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Employee>>> GetAsync();

    Task<IEnumerable<Employee>> GetComboAsync();
}